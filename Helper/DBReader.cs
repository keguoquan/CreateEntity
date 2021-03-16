using System;
using System.Collections.Generic;
using System.Globalization;
using DBFrame;

namespace CreateEntity.Helper
{
    /// <summary>
    /// 数据库读取表信息
    /// </summary>
    public class DBReader
    {
        /// <summary>
        /// 从数据库中去读表信息
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="dbServer"></param>
        /// <param name="dbUser"></param>
        /// <param name="dbName"></param>
        /// <param name="dbPwd"></param>
        /// <returns></returns>
        public static List<TableInfo> Reader(DataBaseType dbType, string dbServer, string port, string dbUser, string dbName, string dbPwd)
        {
            string key = string.Format("{0}_{1}_{2}_{3}_{4}", (int)dbType, dbServer, port, dbUser, dbName);

            if (DBSession.GetDBContextByKey(key) == null)
            {
                string connstring = GetConnectionStr(dbType, dbServer, port, dbUser, dbName, dbPwd);
                DBContext db = new DBContext(key, dbType, connstring);
                DBSession.InitDBSession(db, null);
            }
            DBSession.DefaultDBKey = key;

            if (dbType == DataBaseType.Oracle)
                return ReaderOracle();
            else if (dbType == DataBaseType.SQLServer)
                return ReaderSQLServer();
            else if (dbType == DataBaseType.MySQL)
                return ReaderMySQL(dbName);

            throw new Exception("不支持的数据库类型");
        }

        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="dbServer"></param>
        /// <param name="dbUser"></param>
        /// <param name="dbName"></param>
        /// <param name="dbPwd"></param>
        /// <returns></returns>
        private static string GetConnectionStr(DataBaseType dbType, string dbServer, string port, string dbUser, string dbName, string dbPwd)
        {
            if (dbType == DataBaseType.Oracle)
            {
                if (string.IsNullOrWhiteSpace(port))
                    port = "1521";
                return string.Format(@"Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = {0})(PORT = {1})))(CONNECT_DATA =(SERVICE_NAME = {2})));User Id={3};Password={4};", dbServer, port, dbName, dbUser, dbPwd);
            }
            else if (dbType == DataBaseType.SQLServer)
            {
                if (string.IsNullOrWhiteSpace(port))
                    port = "1433";
                return string.Format("Data Source={0},{1};Initial Catalog={2};User ID={3};pwd={4}", dbServer, port, dbName, dbUser, dbPwd);
            }
            else if (dbType == DataBaseType.MySQL)
            {
                if (string.IsNullOrWhiteSpace(port))
                    port = "3306";
                return string.Format("server={0};Port={1};user id={2};password={3};database={4};Charset=utf8;", dbServer, port, dbUser, dbPwd, dbName);
            }
            throw new Exception("不支持的数据库类型");
        }

        /// <summary>
        /// 读取Oracle
        /// </summary>
        /// <returns></returns>
        private static List<TableInfo> ReaderOracle()
        {
            #region sql
            string sql = @"
select x.*,(case when y.COLUMN_NAME is null then 0 else 1 end)IsPrimaryKey from
(
select A.COLUMN_NAME,a.data_type,a.data_length,a.data_precision,a.data_scale,B.COMMENTS
from user_tab_cols a left join user_col_comments b on a.COLUMN_NAME=B.COLUMN_NAME
where a.TABLE_NAME='{0}' and b.TABLE_NAME='{0}'
)x left join
(
  select COLUMN_NAME from user_constraints a left join user_cons_columns b on A.CONSTRAINT_NAME=B.CONSTRAINT_NAME
   where a.CONSTRAINT_TYPE = 'P' and a.table_name='{0}'
)y on x.COLUMN_NAME=y.COLUMN_NAME";
            #endregion

            using (DBSession db = DBSession.TryGet())
            {
                //获取所有table
                dynamic dytables = db.GetDynamicList("select TABLE_NAME,COMMENTS from user_tab_comments");
                List<TableInfo> tables = new List<TableInfo>();
                foreach (dynamic item in dytables)
                {
                    TableInfo table = new TableInfo();
                    table.Name = item.TABLE_NAME;
                    table.Desc = TBFieldHelper.FormatDesc(item.COMMENTS);
                    table.Fields = new List<TBField>();
                    //获取字段
                    dynamic fields = db.GetDynamicList(string.Format(sql, table.Name.ToUpper()));
                    foreach (dynamic dyF in fields)
                    {
                        TBField field = new TBField();
                        field.Name = dyF.COLUMN_NAME;
                        field.Desc = TBFieldHelper.FormatDesc(dyF.COMMENTS);
                        field.IsPrimaryKey = dyF.ISPRIMARYKEY == 1 ? true : false;
                        string typestr = dyF.DATA_TYPE;
                        //获取数据类型
                        field.FieldType = TBFieldHelper.GetFieldType(field.Name, typestr, DataBaseType.Oracle,
                            () => { return Getdynamic<int>(dyF.DATA_PRECISION); },
                            () => { return Getdynamic<int>(dyF.DATA_SCALE); });                        
                        table.Fields.Add(field);
                    }
                    tables.Add(table);
                }
                return tables;
            }
        }

        /// <summary>
        /// 读取SQL Server
        /// </summary>
        /// <returns></returns>
        private static List<TableInfo> ReaderSQLServer()
        {
            #region sql

            /// <summary>
            /// 获取字段信息以及注释等sql语句
            /// </summary>
            string sql = @"select sysobjects.name as tablename,syscolumns.name as filedname,systypes.name as datatype,
                        ispk= case when 
                        exists(select 1 from sysobjects where xtype='PK' and parent_obj=syscolumns.id and name in
                         (select name from sysindexes where indid 
                        in(select indid from sysindexkeys where id = syscolumns.id AND colid=syscolumns.colid)))
                         then 1 else 0 end,columndescription= isnull(sys.extended_properties.[value],''),
                        tabledescription=case when (select count(*) from sys.extended_properties 
                        where major_id=sysobjects.id and minor_id=0)=1 then 
                        (select [value] from sys.extended_properties where major_id=sysobjects.id and minor_id=0) else '' end 
                        from sysobjects inner join syscolumns on sysobjects.id = syscolumns.id 
                        left join systypes  on syscolumns.xtype = systypes.xusertype  
                        left join sys.extended_properties on syscolumns.id = sys.extended_properties.major_id 
                        and syscolumns.colid = sys.extended_properties.minor_id 
                        where (sysobjects.xtype='U' or sysobjects.xtype='V') and sysobjects.name <>'sysdiagrams' and sysobjects.id in (?)";

            #endregion

            using (DBSession db = DBSession.TryGet())
            {
                //获取所有table
                dynamic dyTables = db.GetDynamicList("select name,id from sysobjects where (xtype='U' or xtype='V')");

                List<TableInfo> tables = new List<TableInfo>();
                foreach (dynamic item in dyTables)
                {
                    TableInfo table = new TableInfo();
                    table.Name = item.NAME;
                    table.Fields = new List<TBField>();

                    //获取字段
                    dynamic fields = db.GetDynamicList(sql, item.ID);
                    foreach (dynamic dyF in fields)
                    {
                        table.Desc = TBFieldHelper.FormatDesc(dyF.TABLEDESCRIPTION);
                        TBField field = new TBField();
                        field.Name = dyF.FILEDNAME;
                        field.Desc = TBFieldHelper.FormatDesc(dyF.COLUMNDESCRIPTION);
                        field.IsPrimaryKey = dyF.ISPK == 1 ? true : false;
                        //获取数据类型
                        field.FieldType = TBFieldHelper.GetFieldType(field.Name, dyF.DATATYPE, DataBaseType.SQLServer);                        
                        table.Fields.Add(field);
                    }
                    tables.Add(table);
                }
                return tables;
            }
        }

        /// <summary>
        /// 读取My Sql
        /// </summary>
        /// <returns></returns>
        private static List<TableInfo> ReaderMySQL(string dbName)
        {
            using (DBSession db = DBSession.TryGet())
            {
                //获取所有table
                dynamic dytables = db.GetDynamicList("select table_name,table_comment from information_schema.TABLES where table_schema=?", dbName);
                List<TableInfo> tables = new List<TableInfo>();
                foreach (dynamic item in dytables)
                {
                    TableInfo table = new TableInfo();
                    table.Name = item.TABLE_NAME;
                    table.Desc = TBFieldHelper.FormatDesc(item.TABLE_COMMENT);
                    table.Fields = new List<TBField>();
                    //获取字段
                    string sql = string.Format(@"
select column_name,data_type,column_key,column_comment from information_schema.columns
where table_schema=? and table_name=? order by column_key desc");
                    dynamic fields = db.GetDynamicList(sql, dbName, table.Name);
                    foreach (dynamic dyF in fields)
                    {
                        TBField field = new TBField();
                        field.Name = dyF.COLUMN_NAME;
                        field.Desc = TBFieldHelper.FormatDesc(dyF.COLUMN_COMMENT);
                        field.IsPrimaryKey = dyF.COLUMN_KEY == "PRI" ? true : false;
                        string typestr = dyF.DATA_TYPE;
                        //获取数据类型
                        field.FieldType = TBFieldHelper.GetFieldType(field.Name, typestr, DataBaseType.MySQL);                        
                        table.Fields.Add(field);
                    }
                    tables.Add(table);
                }
                return tables;
            }
        }

        /// <summary>
        /// 获取子节点值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private static T Getdynamic<T>(dynamic val)
        {
            if (val == null)
                return default(T);
            return (T)Convert.ChangeType(val, typeof(T), CultureInfo.InvariantCulture);
        }
    }
}
