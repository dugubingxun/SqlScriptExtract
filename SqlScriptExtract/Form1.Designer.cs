
namespace SqlScriptExtract
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkmemo = new System.Windows.Forms.CheckBox();
            this.chkdel = new System.Windows.Forms.CheckBox();
            this.btnclear = new System.Windows.Forms.Button();
            this.btnstop = new System.Windows.Forms.Button();
            this.btnextract = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.cmbdatabase = new System.Windows.Forms.ComboBox();
            this.txtsql = new System.Windows.Forms.TextBox();
            this.txtpwd = new System.Windows.Forms.TextBox();
            this.txtuser = new System.Windows.Forms.TextBox();
            this.txtserver = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.prgbar = new System.Windows.Forms.ProgressBar();
            this.txtscrip = new System.Windows.Forms.TextBox();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbdatatype = new System.Windows.Forms.ComboBox();
            this.txtport = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtfieldflt = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtport);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.btnclear);
            this.groupBox1.Controls.Add(this.btnstop);
            this.groupBox1.Controls.Add(this.btnextract);
            this.groupBox1.Controls.Add(this.btnLogin);
            this.groupBox1.Controls.Add(this.cmbdatabase);
            this.groupBox1.Controls.Add(this.txtsql);
            this.groupBox1.Controls.Add(this.txtpwd);
            this.groupBox1.Controls.Add(this.txtuser);
            this.groupBox1.Controls.Add(this.txtserver);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(663, 308);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "连接选项";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtfieldflt);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.cmbdatatype);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.chkmemo);
            this.groupBox3.Controls.Add(this.chkdel);
            this.groupBox3.Location = new System.Drawing.Point(257, 20);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(256, 144);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "选项";
            // 
            // chkmemo
            // 
            this.chkmemo.AutoSize = true;
            this.chkmemo.Checked = true;
            this.chkmemo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkmemo.Location = new System.Drawing.Point(19, 43);
            this.chkmemo.Name = "chkmemo";
            this.chkmemo.Size = new System.Drawing.Size(72, 16);
            this.chkmemo.TabIndex = 1;
            this.chkmemo.Text = "简单注释";
            this.chkmemo.UseVisualStyleBackColor = true;
            // 
            // chkdel
            // 
            this.chkdel.AutoSize = true;
            this.chkdel.Checked = true;
            this.chkdel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkdel.Location = new System.Drawing.Point(19, 21);
            this.chkdel.Name = "chkdel";
            this.chkdel.Size = new System.Drawing.Size(96, 16);
            this.chkdel.TabIndex = 0;
            this.chkdel.Text = "生成删除脚本";
            this.chkdel.UseVisualStyleBackColor = true;
            // 
            // btnclear
            // 
            this.btnclear.Location = new System.Drawing.Point(540, 124);
            this.btnclear.Name = "btnclear";
            this.btnclear.Size = new System.Drawing.Size(75, 23);
            this.btnclear.TabIndex = 13;
            this.btnclear.Text = "清除";
            this.btnclear.UseVisualStyleBackColor = true;
            this.btnclear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // btnstop
            // 
            this.btnstop.Location = new System.Drawing.Point(540, 86);
            this.btnstop.Name = "btnstop";
            this.btnstop.Size = new System.Drawing.Size(75, 23);
            this.btnstop.TabIndex = 12;
            this.btnstop.Text = "停止";
            this.btnstop.UseVisualStyleBackColor = true;
            this.btnstop.Click += new System.EventHandler(this.btnstop_Click);
            // 
            // btnextract
            // 
            this.btnextract.Location = new System.Drawing.Point(540, 48);
            this.btnextract.Name = "btnextract";
            this.btnextract.Size = new System.Drawing.Size(75, 23);
            this.btnextract.TabIndex = 11;
            this.btnextract.Text = "抽取";
            this.btnextract.UseVisualStyleBackColor = true;
            this.btnextract.Click += new System.EventHandler(this.btnextract_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(540, 10);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 9;
            this.btnLogin.Text = "登陆";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // cmbdatabase
            // 
            this.cmbdatabase.FormattingEnabled = true;
            this.cmbdatabase.Location = new System.Drawing.Point(85, 142);
            this.cmbdatabase.Name = "cmbdatabase";
            this.cmbdatabase.Size = new System.Drawing.Size(155, 20);
            this.cmbdatabase.TabIndex = 8;
            // 
            // txtsql
            // 
            this.txtsql.Location = new System.Drawing.Point(6, 170);
            this.txtsql.Multiline = true;
            this.txtsql.Name = "txtsql";
            this.txtsql.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtsql.Size = new System.Drawing.Size(651, 132);
            this.txtsql.TabIndex = 7;
            // 
            // txtpwd
            // 
            this.txtpwd.Location = new System.Drawing.Point(85, 114);
            this.txtpwd.Name = "txtpwd";
            this.txtpwd.Size = new System.Drawing.Size(155, 21);
            this.txtpwd.TabIndex = 6;
            this.txtpwd.TextChanged += new System.EventHandler(this.txtpwd_TextChanged);
            // 
            // txtuser
            // 
            this.txtuser.Location = new System.Drawing.Point(85, 86);
            this.txtuser.Name = "txtuser";
            this.txtuser.Size = new System.Drawing.Size(155, 21);
            this.txtuser.TabIndex = 5;
            this.txtuser.Text = "sa";
            this.txtuser.TextChanged += new System.EventHandler(this.txtuser_TextChanged);
            // 
            // txtserver
            // 
            this.txtserver.Location = new System.Drawing.Point(85, 30);
            this.txtserver.Name = "txtserver";
            this.txtserver.Size = new System.Drawing.Size(155, 21);
            this.txtserver.TabIndex = 4;
            this.txtserver.Text = "127.0.0.1";
            this.txtserver.TextChanged += new System.EventHandler(this.txtserver_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "数据库";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "密  码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "用户名";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.prgbar);
            this.groupBox2.Controls.Add(this.txtscrip);
            this.groupBox2.Location = new System.Drawing.Point(12, 326);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(663, 294);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "生成脚本";
            // 
            // prgbar
            // 
            this.prgbar.Location = new System.Drawing.Point(6, 15);
            this.prgbar.Name = "prgbar";
            this.prgbar.Size = new System.Drawing.Size(651, 13);
            this.prgbar.TabIndex = 9;
            // 
            // txtscrip
            // 
            this.txtscrip.Location = new System.Drawing.Point(6, 34);
            this.txtscrip.MaxLength = 0;
            this.txtscrip.Multiline = true;
            this.txtscrip.Name = "txtscrip";
            this.txtscrip.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtscrip.Size = new System.Drawing.Size(651, 241);
            this.txtscrip.TabIndex = 8;
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.WorkerSupportsCancellation = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "数据库类型";
            // 
            // cmbdatatype
            // 
            this.cmbdatatype.FormattingEnabled = true;
            this.cmbdatatype.Location = new System.Drawing.Point(86, 66);
            this.cmbdatatype.Name = "cmbdatatype";
            this.cmbdatatype.Size = new System.Drawing.Size(121, 20);
            this.cmbdatatype.TabIndex = 3;
            this.cmbdatatype.SelectedIndexChanged += new System.EventHandler(this.cmbdatatype_SelectedIndexChanged);
            // 
            // txtport
            // 
            this.txtport.Location = new System.Drawing.Point(85, 58);
            this.txtport.Name = "txtport";
            this.txtport.Size = new System.Drawing.Size(155, 21);
            this.txtport.TabIndex = 15;
            this.txtport.Text = "sa";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "端口号";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 97);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(185, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "下面写过滤掉的字段英文逗号隔开";
            // 
            // txtfieldflt
            // 
            this.txtfieldflt.Location = new System.Drawing.Point(7, 116);
            this.txtfieldflt.Name = "txtfieldflt";
            this.txtfieldflt.Size = new System.Drawing.Size(243, 21);
            this.txtfieldflt.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 632);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "脚本抽取";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnclear;
        private System.Windows.Forms.Button btnstop;
        private System.Windows.Forms.Button btnextract;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.ComboBox cmbdatabase;
        private System.Windows.Forms.TextBox txtsql;
        private System.Windows.Forms.TextBox txtpwd;
        private System.Windows.Forms.TextBox txtuser;
        private System.Windows.Forms.TextBox txtserver;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtscrip;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkmemo;
        private System.Windows.Forms.CheckBox chkdel;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.ProgressBar prgbar;
        private System.Windows.Forms.ComboBox cmbdatatype;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtport;
        private System.Windows.Forms.TextBox txtfieldflt;
        private System.Windows.Forms.Label label7;
    }
}

