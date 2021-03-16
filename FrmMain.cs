using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using CreateEntity.Helper;
using DBFrame;
using System.Threading.Tasks;
namespace CreateEntity
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();

            //加载下拉框
            String[] EnumFuncTitle = Enum.GetNames(typeof(DataBaseType));
            foreach (string intValue in EnumFuncTitle)
            {
                cmbDataBaseType.Items.Add(intValue);
            }
            cmbDataBaseType.SelectedIndex = 0;
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                txtpdmPath.Text = System.Configuration.ConfigurationManager.AppSettings["pdmPath"];

                txtDBServer.Text = System.Configuration.ConfigurationManager.AppSettings["dbServer"];
                txtDBUserName.Text = System.Configuration.ConfigurationManager.AppSettings["dbUserName"];
                txtDBName.Text = System.Configuration.ConfigurationManager.AppSettings["dbName"];
                txtDBPwd.Text = System.Configuration.ConfigurationManager.AppSettings["dbPwd"];

                txtNameSpace.Text = System.Configuration.ConfigurationManager.AppSettings["nameSpace"];
                txtSavePath.Text = System.Configuration.ConfigurationManager.AppSettings["savePath"];

                cmbDataBaseType.SelectedIndex = int.Parse(System.Configuration.ConfigurationManager.AppSettings["dbType"]) - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 生成实体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //验证输入
            if (!Check()) return;
            //异步开始
            bool isPdm = true;
            if (tabControl1.SelectedTab == this.tpPdm)//从pdm中读取 表信息
                isPdm = true;
            else
                isPdm = false;

            DataBaseType dbType = (DataBaseType)Enum.Parse(typeof(DataBaseType), cmbDataBaseType.Text);
            Task.Factory.StartNew(() =>
            {
                try
                {
                    SetButton(false);
                    TBFieldHelper.EnumList = new List<EnumModel>();//清空枚举项
                    List<TableInfo> tables = null;
                    if (isPdm)//从pdm中读取 表信息
                    {
                        tables = PdmReader.ReaderPdm(txtpdmPath.Text.Trim());
                    }
                    else//从数据库中读取表信息
                    {
                        tables = DBReader.Reader(dbType, txtDBServer.Text.Trim(), txtPort.Text.Trim(),
                            txtDBUserName.Text.Trim(), txtDBName.Text.Trim(), txtDBPwd.Text.Trim());
                    }

                    if (tables != null && tables.Count > 0)
                    {
                        if (!Directory.Exists(txtSavePath.Text.Trim()))
                        {
                            Directory.CreateDirectory(txtSavePath.Text.Trim());
                        }
                        int cont = Creater.CreateEntity(tables, txtNameSpace.Text.Trim(), txtSavePath.Text.Trim(), WriteLog, checkBox1.Checked);
                        WriteLog(string.Format("生成完成，共{0}个，成功{1}个，失败{2}个", tables.Count, cont, tables.Count - cont));
                    }
                    else
                    {
                        WriteLog("未读取到任何表信息!");
                    }
                }
                catch (Exception ex)
                {
                    WriteLog("异常：" + ex.Message);
                }
                finally
                {
                    SetButton(true);
                }
            });
        }

        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="log"></param>
        public void WriteLog(string log)
        {
            if (txtLog.InvokeRequired)
            {
                Action<string> action = new Action<string>(WriteLog);
                txtLog.Invoke(action, log);
            }
            else
            {
                txtLog.AppendText(log + "\r\n");
                txtLog.ScrollToCaret();
            }
        }

        public void SetButton(bool enable)
        {
            if (button2.InvokeRequired)
            {
                Action<bool> action = new Action<bool>(SetButton);
                button2.Invoke(action, enable);
            }
            else
            {
                button2.Enabled = enable;
            }
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        public bool Check()
        {
            if (tabControl1.SelectedTab == this.tpPdm)
            {
                if (txtpdmPath.Text == "")
                {
                    MessageBox.Show("请你输入或选择pdm路径", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (!File.Exists(txtpdmPath.Text))
                {
                    throw new Exception("不存在的pdm文件");
                }
            }
            else
            {
                if (txtDBServer.Text == "")
                {
                    MessageBox.Show("请你输入数据库服务器", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (txtDBUserName.Text == "")
                {
                    MessageBox.Show("请你输入数据库用户名", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (txtDBName.Text == "")
                {
                    MessageBox.Show("请你输入数据库名", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (txtDBPwd.Text == "")
                {
                    MessageBox.Show("请你输入数据库密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (txtNameSpace.Text == "")
            {
                MessageBox.Show("请你输入命名空间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (txtSavePath.Text == "")
            {
                MessageBox.Show("请你输入或选择保存路径", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == this.tpPdm)
            {
                txtpdmPath.Text = "";
            }
            else
            {
                txtDBName.Text = "";
                txtDBPwd.Text = "";
                txtDBServer.Text = "";
                txtDBUserName.Text = "";
            }
            txtNameSpace.Text = "";
            txtSavePath.Text = "";
            txtLog.Text = "";
        }

        /// <summary>
        /// pdm选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butPdmPath_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.ShowDialog();
            this.txtpdmPath.Text = this.openFileDialog1.FileName;
        }

        /// <summary>
        /// 保存路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butSave_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.ShowDialog();
            this.txtSavePath.Text = this.folderBrowserDialog1.SelectedPath;
        }


        private void cmbDataBaseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataBaseType dbType = (DataBaseType)Enum.Parse(typeof(DataBaseType), cmbDataBaseType.Text);
                if (dbType == DataBaseType.MySQL) txtPort.Text = "3306";
                else if (dbType == DataBaseType.Oracle) txtPort.Text = "1521";
                else if (dbType == DataBaseType.SQLServer) txtPort.Text = "1433";
            }
            catch (Exception) { }
        }

    }
}
