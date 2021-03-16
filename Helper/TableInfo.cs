using System;
using System.Collections.Generic;
using DBFrame;

namespace CreateEntity
{
    /// <summary>
    /// 表信息
    /// </summary>
    public class TableInfo
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 表注释
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 字段列表
        /// </summary>
        public List<TBField> Fields { get; set; }
    }

    /// <summary>
    /// 表字段信息
    /// </summary>
    public class TBField
    {
        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 字段注释
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 数据库中的数据类型
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// 字段默认值
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        /// 字段数据类型(C#中的类型)
        /// </summary>
        public string FieldType { get; set; }

        /// <summary>
        /// 是否必须
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 浮点数 小数位长度
        /// </summary>
        public int Precision { get; set; }
    }

    /// <summary>
    /// 表字段帮助类
    /// </summary>
    public class TBFieldHelper
    {
        #region 获取数据类型

        /// <summary>
        /// 获取数据类型
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="typestr">数据类型 字符串</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="GetLen">获取改字段数据类型 长度的方法</param>
        /// <param name="GetPrecision">获取小数位长度的方法</param>
        /// <returns></returns>
        public static string GetFieldType(string fieldName, string typestr, DataBaseType dbType,
            Func<int> GetLen = null, Func<int> GetPrecision = null)
        {
            if (dbType == DataBaseType.Oracle)
                return GetOracleFieldType(fieldName, typestr, GetLen, GetPrecision);
            else if (dbType == DataBaseType.SQLServer)
                return GetSQLServerFieldType(typestr);
            else if (dbType == DataBaseType.MySQL)
                return GetMySQLFieldType(typestr);
            else
                return typestr;
        }

        /// <summary>
        /// 获取oralce数据类型
        /// </summary>
        /// <param name="typestr"></param>
        /// <returns></returns>
        private static string GetOracleFieldType(string fieldName, string typestr, Func<int> GetLen = null, Func<int> GetPrecision = null)
        {
            typestr = typestr.ToUpper();
            if (typestr.IndexOf("CHAR") >= 0)//包括VARCHAR,VARCHAR2,NVARCHAR,NVARCHAR2,CHAR,NCHAR
            {
                return "string";
            }
            else if (typestr.IndexOf("CLOB") >= 0)
            {
                return "string";
            }
            else if (typestr.IndexOf("NUMBER") >= 0)
            {
                int len = GetLen();
                int pre = GetPrecision();

                //如果小数位大于0，直接为decimal类型
                if (pre > 0) return "decimal";
                else if (len == 1 && (fieldName.ToUpper().IndexOf("IS") >= 0)) return "bool";
                else if (len < 4) return "Int16";
                else if (len < 10) return "Int32";
                else if (len <= 18) return "Int64";
                else return "decimal";
            }
            else if (typestr.IndexOf("DATE") >= 0)
            {
                return "DateTime";
            }
            else if (typestr.IndexOf("INT") >= 0)
            {
                return "int";
            }
            else if (typestr.IndexOf("FLOAT") >= 0)
            {
                return "float";
            }
            else if (typestr.IndexOf("BLOB") >= 0)
            {
                return "byte[]";
            }
            else if (typestr.IndexOf("XML") >= 0)
            {
                return "XmlDocument";
            }
            return typestr;
        }

        /// <summary>
        /// 获取SQL Server数据类型
        /// </summary>
        /// <param name="typestr"></param>
        /// <returns></returns>
        private static string GetSQLServerFieldType(string typestr)
        {
            typestr = typestr.ToUpper();
            if (typestr.IndexOf("CHAR") >= 0 || typestr.IndexOf("TEXT") >= 0)//包括VARCHAR,VARCHAR2,NVARCHAR,NVARCHAR2,CHAR,NCHAR,TEXT,NTEXT
            {
                return "string";
            }
            else if (typestr.IndexOf("SMALLINT") >= 0 || typestr.IndexOf("TINYINT") >= 0)
            {
                return "Int16";
            }
            else if (typestr.IndexOf("BIGINT") >= 0)
            {
                return "Int64";
            }
            else if (typestr.IndexOf("INT") >= 0)//包括int integer 以及其他包含int的类型
            {
                return "Int32";
            }
            else if (typestr.IndexOf("FLOAT") >= 0)
            {
                return "float";
            }
            else if (typestr.IndexOf("DATETIME") >= 0)//包括smalldatetime
            {
                return "DateTime";
            }
            else if (typestr.IndexOf("MONEY") >= 0)//包括smallmoney
            {
                return "decimal";
            }
            else if (typestr.IndexOf("BIT") >= 0)
            {
                return "bool";
            }
            else if (typestr.IndexOf("NUMERIC") >= 0 || typestr.IndexOf("DECIMAL") >= 0)
            {
                return "decimal";
            }
            else if (typestr.IndexOf("UNIQUEIDENTIFIER") >= 0)
            {
                return "Guid";
            }
            else if (typestr.IndexOf("IMAGE") >= 0)
            {
                return "byte[]";
            }
            else if (typestr.IndexOf("XML") >= 0)
            {
                return "XmlDocument";
            }
            return typestr;
        }

        /// <summary>
        /// 获取mySQL数据类型
        /// </summary>
        /// <param name="typestr"></param>
        /// <returns></returns>
        private static string GetMySQLFieldType(string typestr)
        {
            typestr = typestr.ToUpper();
            if (typestr.IndexOf("CHAR") >= 0 || typestr.IndexOf("TEXT") >= 0)//包括VARCHAR,VARCHAR2,NVARCHAR,NVARCHAR2,CHAR,NCHAR,TEXT,NTEXT
            {
                return "string";
            }
            else if (typestr.IndexOf("SMALLINT") >= 0 || typestr.IndexOf("TINYINT") >= 0)
            {
                return "Int16";
            }
            else if (typestr.IndexOf("BIGINT") >= 0)
            {
                return "Int64";
            }
            else if (typestr.IndexOf("INT") >= 0)//包括int integer 以及其他包含int的类型
            {
                return "Int32";
            }
            else if (typestr.IndexOf("FLOAT") >= 0)
            {
                return "float";
            }
            else if (typestr.IndexOf("DOUBLE") >= 0)
            {
                return "double";
            }
            else if (typestr.IndexOf("DATE") >= 0)//包括Date datetime
            {
                return "DateTime";
            }
            else if (typestr.IndexOf("BOOL") >= 0)
            {
                return "bool";
            }
            else if (typestr.IndexOf("NUMERIC") >= 0 || typestr.IndexOf("DECIMAL") >= 0)
            {
                return "decimal";
            }
            else if (typestr.IndexOf("BLOB") >= 0)
            {
                return "byte[]";
            }
            return typestr;
        }

        #endregion

        /// <summary>
        /// 格式化备注
        /// </summary>
        /// <param name="desc"></param>
        /// <returns></returns>
        public static string FormatDesc(string desc)
        {
            if (string.IsNullOrWhiteSpace(desc))
                return string.Empty;

            return desc.Replace("\r", " ").Replace("\n", " ");
        }

        public static List<EnumModel> EnumList { get; set; }
        /// <summary>
        /// 处理枚举
        /// 枚举条件：有换行符，以及有等号，并且数据类型包含int
        /// </summary>
        /// <param name="field">对应字段信息</param>
        /// <param name="comment">pdm中的comment</param>
        public static void DealEnum(TBField field, string comment)
        {
            if (comment.IndexOf("\n") >= 0)
            {
                if (comment.IndexOf("=") >= 0 && field.FieldType.ToLower().IndexOf("int") >= 0)//枚举处理
                {
                    if (EnumList == null)
                        EnumList = new List<EnumModel>();

                    field.FieldType = field.Name;//枚举名称，默认为字段名称

                    //去重
                    if (EnumList.Find(x => x.EnumName == field.Name) == null)
                        EnumList.Add(new EnumModel(field.Name, comment, field.Desc));
                }
            }
        }
    }

    public class EnumModel
    {
        public EnumModel() { }

        public EnumModel(string eName, string eContent, string desc)
        {
            this.EnumName = eName;
            this.EnumContent = eContent;
            this.Desc = desc;
        }

        /// <summary>
        /// 枚举名称
        /// </summary>
        public string EnumName { get; set; }

        /// <summary>
        /// 枚举内容
        /// </summary>
        public string EnumContent { get; set; }

        /// <summary>
        /// 枚举注释
        /// </summary>
        public string Desc { get; set; }
    }
}
