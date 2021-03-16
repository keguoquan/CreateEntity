using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CreateEntity
{
    public class Creater
    {
        /// <summary>
        /// 生成实体
        /// </summary>
        /// <param name="tables"></param>
        /// <param name="nameSpace"></param>
        /// <param name="saveDir"></param>
        public static int CreateEntity(List<TableInfo> tables, string nameSpace, string saveDir, Action<string> OutputLog, bool isVail = false)
        {
            int cont = 0;
            foreach (TableInfo table in tables)
            {
                if (string.IsNullOrWhiteSpace(table.Name))
                    continue;
                try
                {
                    StringBuilder context = new StringBuilder("using System;\r\n");
                    context.Append("using DBFrame.DBMapAttr;\r\n");
                    if (isVail)
                    {
                        context.Append("using System.ComponentModel.DataAnnotations;\r\n\r\n");
                    }
                    context.Append("namespace " + nameSpace + "\r\n");
                    context.Append("{ \r\n");
                    context.AppendFormat("    /// <summary>\r\n");
                    context.AppendFormat("    ///{0}\r\n", table.Desc);
                    context.AppendFormat("    /// </summary>\r\n");
                    context.AppendFormat("    [DBTable(\"{0}\")]\r\n", table.Name);
                    context.AppendFormat("    public class {0} \r\n", table.Name);
                    context.Append("    {\r\n");

                    foreach (TBField ta in table.Fields)
                    {
                        context.AppendFormat("        /// <summary>\r\n");
                        context.AppendFormat("        ///{0}\r\n", ta.Desc);
                        context.AppendFormat("        /// </summary>\r\n");
                        if (isVail)
                        {
                            context.AppendFormat("        [Display(Name = \"{0}\")]\r\n", ta.Desc);
                            if (ta.IsRequired)
                            {
                                context.AppendFormat("        [Required(ErrorMessage = \"{0}\")]\r\n", string.Format("{0}不能为空", ta.Desc));
                            }
                            if (ta.FieldType.ToLower() == "string" && ta.Length > 0)
                            {
                                context.AppendFormat("        [StringLength({0},ErrorMessage = \"{1}\")]\r\n", ta.Length, string.Format("{0}最大长度{1}", ta.Desc, ta.Length));
                            }
                            else if (ta.Length > 0)
                            {
                                Int64 max = GetLenghtMaxVal(ta.Length);
                                if (max > 0)
                                {
                                    context.AppendFormat("        [Range(0,{0},ErrorMessage = \"{1}\")]\r\n", max,
                                        string.Format("{0}值范围0-{1}", ta.Desc, max));
                                }
                            }
                        }
                        context.AppendFormat("        [{0}(\"{1}\"", ta.IsPrimaryKey ? "DBPrimaryKey" : "DBColumn", ta.Name);
                        if (isVail)
                        {
                            context.AppendFormat(", \"{0}\"", ta.DataType);
                        }
                        if (isVail && !string.IsNullOrWhiteSpace(ta.Default))
                        {
                            context.AppendFormat(", Default = \"{0}\"", ta.Default);
                        }
                        if (isVail && ta.IsRequired)
                        {
                            context.AppendFormat(", NotNull = {0}", ta.IsRequired.ToString().ToLower());
                        }
                        context.AppendFormat(")]\r\n");
                        context.AppendFormat("        public {0} {1} {2} get; set; {3}\r\n\r\n", ta.FieldType, ta.Name, "{", "}");
                    }
                    context.Append("    }\r\n}");

                    string path = Path.Combine(saveDir, string.Format("{0}.cs", table.Name));
                    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                    {
                        byte[] buffer = new UTF8Encoding(true).GetBytes(context.ToString());
                        fs.Write(buffer, 0, buffer.Length);
                    }
                    cont++;
                }
                catch (Exception exx)
                {
                    if (OutputLog != null)
                        OutputLog(string.Format("表:{0}  错误：{1}", table, exx.Message));
                }
            }

            //生成枚举
            CreateEnum(nameSpace, saveDir);

            return cont;
        }

        /// <summary>
        /// 生成枚举
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="saveDir"></param>
        private static void CreateEnum(string nameSpace, string saveDir)
        {
            if (TBFieldHelper.EnumList.Count == 0) return;

            StringBuilder context = new StringBuilder("using System;\r\n");
            context.Append("\r\n");
            context.Append("namespace " + nameSpace + "\r\n");
            context.Append("{ \r\n");

            foreach (EnumModel model in TBFieldHelper.EnumList)
            {
                context.AppendFormat("    /// <summary>\r\n");
                context.AppendFormat("    ///{0}\r\n", model.Desc);
                context.AppendFormat("    /// </summary>\r\n");
                context.AppendFormat("    public enum {0} \r\n", model.EnumName);
                context.Append("    {\r\n");
                context.AppendFormat("      {0}", model.EnumContent);
                context.Append("    \r\n}\r\n\r\n");
            }
            context.Append("    }");

            string path = Path.Combine(saveDir, string.Format("Enum.cs"));
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                byte[] buffer = new UTF8Encoding(true).GetBytes(context.ToString());
                fs.Write(buffer, 0, buffer.Length);
            }
        }

        /// <summary>
        /// 获取Number length表示的最大值
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static Int64 GetLenghtMaxVal(int length)
        {
            if (length > 18) return 0;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append("9");
            }
            return Int64.Parse(sb.ToString());
        }
    }
}
