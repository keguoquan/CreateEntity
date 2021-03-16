namespace CreateEntity
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpPdm = new System.Windows.Forms.TabPage();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtpdmPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.butPdmPath = new System.Windows.Forms.Button();
            this.tpDB = new System.Windows.Forms.TabPage();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDBPwd = new System.Windows.Forms.TextBox();
            this.txtDBName = new System.Windows.Forms.TextBox();
            this.txtDBUserName = new System.Windows.Forms.TextBox();
            this.txtDBServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbDataBaseType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.butSave = new System.Windows.Forms.Button();
            this.txtNameSpace = new System.Windows.Forms.TextBox();
            this.txtSavePath = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tpPdm.SuspendLayout();
            this.tpDB.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpPdm);
            this.tabControl1.Controls.Add(this.tpDB);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(403, 147);
            this.tabControl1.TabIndex = 0;
            // 
            // tpPdm
            // 
            this.tpPdm.BackColor = System.Drawing.SystemColors.Control;
            this.tpPdm.Controls.Add(this.checkBox1);
            this.tpPdm.Controls.Add(this.txtpdmPath);
            this.tpPdm.Controls.Add(this.label1);
            this.tpPdm.Controls.Add(this.butPdmPath);
            this.tpPdm.Location = new System.Drawing.Point(4, 22);
            this.tpPdm.Name = "tpPdm";
            this.tpPdm.Padding = new System.Windows.Forms.Padding(3);
            this.tpPdm.Size = new System.Drawing.Size(395, 121);
            this.tpPdm.TabIndex = 0;
            this.tpPdm.Text = "从pdm中生成";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(53, 84);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(96, 16);
            this.checkBox1.TabIndex = 43;
            this.checkBox1.Text = "生成验证规则";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // txtpdmPath
            // 
            this.txtpdmPath.Location = new System.Drawing.Point(53, 56);
            this.txtpdmPath.Name = "txtpdmPath";
            this.txtpdmPath.Size = new System.Drawing.Size(288, 21);
            this.txtpdmPath.TabIndex = 42;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 41;
            this.label1.Text = "pdm路径：";
            // 
            // butPdmPath
            // 
            this.butPdmPath.Location = new System.Drawing.Point(340, 56);
            this.butPdmPath.Name = "butPdmPath";
            this.butPdmPath.Size = new System.Drawing.Size(48, 23);
            this.butPdmPath.TabIndex = 40;
            this.butPdmPath.Text = "浏览...";
            this.butPdmPath.UseVisualStyleBackColor = true;
            this.butPdmPath.Click += new System.EventHandler(this.butPdmPath_Click);
            // 
            // tpDB
            // 
            this.tpDB.BackColor = System.Drawing.SystemColors.Control;
            this.tpDB.Controls.Add(this.txtPort);
            this.tpDB.Controls.Add(this.label9);
            this.tpDB.Controls.Add(this.txtDBPwd);
            this.tpDB.Controls.Add(this.txtDBName);
            this.tpDB.Controls.Add(this.txtDBUserName);
            this.tpDB.Controls.Add(this.txtDBServer);
            this.tpDB.Controls.Add(this.label4);
            this.tpDB.Controls.Add(this.label3);
            this.tpDB.Controls.Add(this.label2);
            this.tpDB.Controls.Add(this.label6);
            this.tpDB.Controls.Add(this.cmbDataBaseType);
            this.tpDB.Controls.Add(this.label8);
            this.tpDB.Location = new System.Drawing.Point(4, 22);
            this.tpDB.Name = "tpDB";
            this.tpDB.Padding = new System.Windows.Forms.Padding(3);
            this.tpDB.Size = new System.Drawing.Size(395, 121);
            this.tpDB.TabIndex = 1;
            this.tpDB.Text = "从数据库中生成";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(258, 24);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(121, 21);
            this.txtPort.TabIndex = 42;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(209, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 41;
            this.label9.Text = "端  口：";
            // 
            // txtDBPwd
            // 
            this.txtDBPwd.Location = new System.Drawing.Point(258, 78);
            this.txtDBPwd.Name = "txtDBPwd";
            this.txtDBPwd.Size = new System.Drawing.Size(121, 21);
            this.txtDBPwd.TabIndex = 40;
            // 
            // txtDBName
            // 
            this.txtDBName.Location = new System.Drawing.Point(78, 78);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Size = new System.Drawing.Size(121, 21);
            this.txtDBName.TabIndex = 39;
            // 
            // txtDBUserName
            // 
            this.txtDBUserName.Location = new System.Drawing.Point(258, 50);
            this.txtDBUserName.Name = "txtDBUserName";
            this.txtDBUserName.Size = new System.Drawing.Size(121, 21);
            this.txtDBUserName.TabIndex = 38;
            // 
            // txtDBServer
            // 
            this.txtDBServer.Location = new System.Drawing.Point(78, 50);
            this.txtDBServer.Name = "txtDBServer";
            this.txtDBServer.Size = new System.Drawing.Size(121, 21);
            this.txtDBServer.TabIndex = 37;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 35;
            this.label4.Text = "数据库：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(209, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 34;
            this.label3.Text = "密　码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(209, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 33;
            this.label2.Text = "用户名：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 32;
            this.label6.Text = "服务器：";
            // 
            // cmbDataBaseType
            // 
            this.cmbDataBaseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataBaseType.FormattingEnabled = true;
            this.cmbDataBaseType.Location = new System.Drawing.Point(78, 22);
            this.cmbDataBaseType.Name = "cmbDataBaseType";
            this.cmbDataBaseType.Size = new System.Drawing.Size(121, 20);
            this.cmbDataBaseType.TabIndex = 36;
            this.cmbDataBaseType.SelectedIndexChanged += new System.EventHandler(this.cmbDataBaseType_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(29, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 31;
            this.label8.Text = "数据库：";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(138, 293);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 37;
            this.button3.Text = "重  置";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(57, 293);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 36;
            this.button2.Text = "生成c#实体";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 154);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 35;
            this.label7.Text = "命名空间：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 184);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 34;
            this.label5.Text = "保存路径：";
            // 
            // butSave
            // 
            this.butSave.Location = new System.Drawing.Point(344, 180);
            this.butSave.Name = "butSave";
            this.butSave.Size = new System.Drawing.Size(48, 23);
            this.butSave.TabIndex = 33;
            this.butSave.Text = "浏览...";
            this.butSave.UseVisualStyleBackColor = true;
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // txtNameSpace
            // 
            this.txtNameSpace.Location = new System.Drawing.Point(60, 150);
            this.txtNameSpace.Name = "txtNameSpace";
            this.txtNameSpace.Size = new System.Drawing.Size(285, 21);
            this.txtNameSpace.TabIndex = 38;
            this.txtNameSpace.Text = "Model";
            // 
            // txtSavePath
            // 
            this.txtSavePath.Location = new System.Drawing.Point(60, 180);
            this.txtSavePath.Name = "txtSavePath";
            this.txtSavePath.Size = new System.Drawing.Size(285, 21);
            this.txtSavePath.TabIndex = 39;
            this.txtSavePath.Text = "C:\\Users\\Administrator\\Desktop\\新建文件夹 ";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "pdm文件|*.pdm|所有文件|*.*";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(60, 209);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(331, 78);
            this.txtLog.TabIndex = 40;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1, 212);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 41;
            this.label10.Text = "生成日志：";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 325);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtSavePath);
            this.Controls.Add(this.txtNameSpace);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.butSave);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "实体生成工具";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpPdm.ResumeLayout(false);
            this.tpPdm.PerformLayout();
            this.tpDB.ResumeLayout(false);
            this.tpDB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpPdm;
        private System.Windows.Forms.TabPage tpDB;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button butSave;
        private System.Windows.Forms.TextBox txtNameSpace;
        private System.Windows.Forms.TextBox txtSavePath;
        private System.Windows.Forms.TextBox txtpdmPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button butPdmPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbDataBaseType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDBPwd;
        private System.Windows.Forms.TextBox txtDBName;
        private System.Windows.Forms.TextBox txtDBUserName;
        private System.Windows.Forms.TextBox txtDBServer;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

