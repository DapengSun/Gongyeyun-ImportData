namespace ImportData
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
            this.ShowSQL = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.EnterpriseData = new System.Windows.Forms.Button();
            this.EnterpriseAccountData = new System.Windows.Forms.Button();
            this.EnterpriseSoftware = new System.Windows.Forms.Button();
            this.EnterpriseTurnOver = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.PlatApply = new System.Windows.Forms.Button();
            this.ImportIndustryType = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ShowSQL
            // 
            this.ShowSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ShowSQL.Location = new System.Drawing.Point(0, 0);
            this.ShowSQL.Multiline = true;
            this.ShowSQL.Name = "ShowSQL";
            this.ShowSQL.Size = new System.Drawing.Size(609, 440);
            this.ShowSQL.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(622, 466);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "导入数据SQL";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ShowSQL);
            this.panel1.Location = new System.Drawing.Point(7, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(609, 440);
            this.panel1.TabIndex = 1;
            // 
            // EnterpriseData
            // 
            this.EnterpriseData.Location = new System.Drawing.Point(20, 559);
            this.EnterpriseData.Name = "EnterpriseData";
            this.EnterpriseData.Size = new System.Drawing.Size(97, 23);
            this.EnterpriseData.TabIndex = 2;
            this.EnterpriseData.Text = "企业数据";
            this.EnterpriseData.UseVisualStyleBackColor = true;
            // 
            // EnterpriseAccountData
            // 
            this.EnterpriseAccountData.Location = new System.Drawing.Point(183, 559);
            this.EnterpriseAccountData.Name = "EnterpriseAccountData";
            this.EnterpriseAccountData.Size = new System.Drawing.Size(97, 23);
            this.EnterpriseAccountData.TabIndex = 3;
            this.EnterpriseAccountData.Text = "企业账户数据";
            this.EnterpriseAccountData.UseVisualStyleBackColor = true;
            // 
            // EnterpriseSoftware
            // 
            this.EnterpriseSoftware.Location = new System.Drawing.Point(350, 559);
            this.EnterpriseSoftware.Name = "EnterpriseSoftware";
            this.EnterpriseSoftware.Size = new System.Drawing.Size(97, 23);
            this.EnterpriseSoftware.TabIndex = 4;
            this.EnterpriseSoftware.Text = "企业软件应用";
            this.EnterpriseSoftware.UseVisualStyleBackColor = true;
            // 
            // EnterpriseTurnOver
            // 
            this.EnterpriseTurnOver.Location = new System.Drawing.Point(525, 559);
            this.EnterpriseTurnOver.Name = "EnterpriseTurnOver";
            this.EnterpriseTurnOver.Size = new System.Drawing.Size(97, 23);
            this.EnterpriseTurnOver.TabIndex = 5;
            this.EnterpriseTurnOver.Text = "企业营业额";
            this.EnterpriseTurnOver.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(350, 588);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(97, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "单项应用使用";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(183, 588);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(97, 23);
            this.button5.TabIndex = 7;
            this.button5.Text = "工业电商使用";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // PlatApply
            // 
            this.PlatApply.Location = new System.Drawing.Point(20, 588);
            this.PlatApply.Name = "PlatApply";
            this.PlatApply.Size = new System.Drawing.Size(97, 23);
            this.PlatApply.TabIndex = 6;
            this.PlatApply.Text = "工业云平台应用";
            this.PlatApply.UseVisualStyleBackColor = true;
            // 
            // ImportIndustryType
            // 
            this.ImportIndustryType.Location = new System.Drawing.Point(170, 20);
            this.ImportIndustryType.Name = "ImportIndustryType";
            this.ImportIndustryType.Size = new System.Drawing.Size(97, 23);
            this.ImportIndustryType.TabIndex = 9;
            this.ImportIndustryType.Text = "导入报表分类";
            this.ImportIndustryType.UseVisualStyleBackColor = true;
            this.ImportIndustryType.Click += new System.EventHandler(this.ImportIndustryType_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.ImportIndustryType);
            this.groupBox2.Location = new System.Drawing.Point(13, 485);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(622, 52);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "导入基础数据";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "导入报表数据";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.GetImportData);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 629);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.PlatApply);
            this.Controls.Add(this.EnterpriseTurnOver);
            this.Controls.Add(this.EnterpriseSoftware);
            this.Controls.Add(this.EnterpriseAccountData);
            this.Controls.Add(this.EnterpriseData);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "工业云数据导入";
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox ShowSQL;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button EnterpriseData;
        private System.Windows.Forms.Button EnterpriseAccountData;
        private System.Windows.Forms.Button EnterpriseSoftware;
        private System.Windows.Forms.Button EnterpriseTurnOver;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button PlatApply;
        private System.Windows.Forms.Button ImportIndustryType;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
    }
}

