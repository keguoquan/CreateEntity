using System;
using System.Collections.Generic;
using System.Xml;
using System.Globalization;
using DBFrame;

namespace CreateEntity
{
    /// <summary>
    /// 从pdm中读取信息
    /// </summary>
    public class PdmReader
    {
        //数据库类型路径
        private static string _dbTypePath = "Model/o:RootObject/c:Children/o:Model/c:DBMS/o:Shortcut/a:Name";
        //表路径
        private static string _tbPath = "Model/o:RootObject/c:Children/o:Model/c:Tables/o:Table";
        //xml读取 命名空间
        private static XmlNamespaceManager _xmlnsManager = null;

        /// <summary>
        /// 从pdm中读取表信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<TableInfo> ReaderPdm(string path)
        {
            //加载xml
            XmlDocument xml = ReadPdmToXml(path);
            _xmlnsManager = new XmlNamespaceManager(xml.NameTable);
            _xmlnsManager.AddNamespace("a", "attribute");
            _xmlnsManager.AddNamespace("c", "collection");
            _xmlnsManager.AddNamespace("o", "object");

            //获取数据库类型
            DataBaseType dbType = ReadDBType(xml);

            List<TableInfo> list = new List<TableInfo>();
            //获取表
            XmlNodeList nodes = xml.SelectNodes(_tbPath, _xmlnsManager);
            foreach (XmlNode item in nodes)
            {
                //读取表信息
                TableInfo table = new TableInfo();
                table.Name = GetNodeText(item, "a:Code");
                table.Desc = TBFieldHelper.FormatDesc(GetNodeText(item, "a:Name"));
                table.Fields = new List<TBField>();

                //获取主键字段Id
                List<string> primaryKey_oids = ReadPrimaryKey(item);
                //获取字段
                XmlNodeList fields = item.SelectNodes("c:Columns//o:Column", _xmlnsManager);
                foreach (XmlNode xnitem in fields)
                {
                    TBField field = new TBField();
                    if (primaryKey_oids.Find(x=>x==GetNodeAttrVal(xnitem, "Id")) != null)
                    {
                        field.IsPrimaryKey = true;
                    }
                    field.Name = GetNodeText(xnitem, "a:Code");
                    field.Length = GetNodeText<int>(xnitem, "a:Length");
                    field.Precision = GetNodeText<int>(xnitem, "a:Precision");
                    field.IsRequired = GetNodeText<int>(xnitem, "a:Column.Mandatory") == 1 ? true : false;
                    field.DataType = GetNodeText(xnitem, "a:DataType");
                    field.Default = GetNodeText(xnitem, "a:DefaultValue");
                    field.FieldType = TBFieldHelper.GetFieldType(
                        field.Name,
                        GetNodeText(xnitem, "a:DataType"),
                        dbType,
                        () => { return field.Length; },
                        () => { return field.Precision; });
                    field.Desc = TBFieldHelper.FormatDesc(GetNodeText(xnitem, "a:Name"));
                    TBFieldHelper.DealEnum(field, GetNodeText(xnitem, "a:Comment"));
                    table.Fields.Add(field);
                }
                list.Add(table);
            }
            return list;
        }

        /// <summary>
        /// 读取pdm到Xml
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static XmlDocument ReadPdmToXml(string path)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(path);
                return xml;
            }
            catch (Exception ex)
            {
                throw new Exception("从pdm中加载Xml失败。" + ex.Message);
            }
        }

        /// <summary>
        /// 获取数据库类型
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private static DataBaseType ReadDBType(XmlDocument xml)
        {
            XmlNode xmlNode = xml.SelectSingleNode(_dbTypePath, _xmlnsManager);
            if (xmlNode != null && !string.IsNullOrWhiteSpace(xmlNode.InnerText))
            {
                if (xmlNode.InnerText.ToUpper().IndexOf("ORACLE") >= 0)
                {
                    return DataBaseType.Oracle;
                }
                else if (xmlNode.InnerText.ToUpper().IndexOf("SQL SERVER") >= 0)
                {
                    return DataBaseType.SQLServer;
                }
                else if (xmlNode.InnerText.ToUpper().IndexOf("MYSQL") >= 0)
                {
                    return DataBaseType.MySQL;
                }
            }
            throw new Exception("pdm未知的数据库类型，目前只支持ORACLE,SQL Server,MySql");
        }

        /// <summary>
        /// 取表主键
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static List<string> ReadPrimaryKey(XmlNode node)
        {
            //获取主键key
            string primaryKey_oid = GetNodeAttrVal(node, "c:PrimaryKey//o:Key", "Ref");
            List<string> primaryKeys = new List<string>();

            //根据主键key，获取对应的字段
            foreach (XmlNode item in node.SelectNodes("c:Keys//o:Key", _xmlnsManager))
            {
                if (primaryKey_oid == GetNodeAttrVal(item, "Id"))
                {
                    XmlNodeList xn = item.SelectNodes("c:Key.Columns//o:Column", _xmlnsManager);
                    if (xn != null)
                    {
                        foreach (XmlNode cNode in xn)
                        {
                            primaryKeys.Add(GetNodeAttrVal(cNode, "Ref"));
                        }
                    }
                }
            }
            return primaryKeys;
        }

        #region xml 取值操作

        /// <summary>
        /// 获取子节点值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string GetNodeText(XmlNode node, string path)
        {
            XmlNode xn = node.SelectSingleNode(path, _xmlnsManager);
            if (xn != null)
            {
                return xn.InnerText;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取子节点值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private static T GetNodeText<T>(XmlNode node, string path)
        {
            XmlNode xn = node.SelectSingleNode(path, _xmlnsManager);
            if (xn != null && (!string.IsNullOrWhiteSpace(xn.InnerText)))
            {
                return (T)Convert.ChangeType(xn.InnerText, typeof(T), CultureInfo.InvariantCulture);
            }
            return default(T);
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        private static string GetNodeAttrVal(XmlNode node, string attrName)
        {
            XmlAttribute attr = node.Attributes[attrName];
            if (attr != null)
            {
                return attr.Value;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取子节点属性值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="path">子节点路径</param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        private static string GetNodeAttrVal(XmlNode node, string path, string attrName)
        {
            XmlNode xn = node.SelectSingleNode(path, _xmlnsManager);
            if (xn != null)
            {
                return GetNodeAttrVal(xn, attrName);
            }
            return string.Empty;
        }

        #endregion
    }
}
