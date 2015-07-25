namespace FrmTest
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtContent1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtContent2 = new System.Windows.Forms.TextBox();
            this.txtContent3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRedirect = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnPickUp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // txtContent1
            // 
            this.txtContent1.Location = new System.Drawing.Point(60, 10);
            this.txtContent1.Name = "txtContent1";
            this.txtContent1.Size = new System.Drawing.Size(278, 21);
            this.txtContent1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "label2";
            // 
            // txtContent2
            // 
            this.txtContent2.Location = new System.Drawing.Point(61, 38);
            this.txtContent2.Name = "txtContent2";
            this.txtContent2.Size = new System.Drawing.Size(277, 21);
            this.txtContent2.TabIndex = 3;
            // 
            // txtContent3
            // 
            this.txtContent3.Location = new System.Drawing.Point(61, 66);
            this.txtContent3.Name = "txtContent3";
            this.txtContent3.Size = new System.Drawing.Size(277, 21);
            this.txtContent3.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "label3";
            // 
            // btnRedirect
            // 
            this.btnRedirect.Location = new System.Drawing.Point(263, 93);
            this.btnRedirect.Name = "btnRedirect";
            this.btnRedirect.Size = new System.Drawing.Size(75, 23);
            this.btnRedirect.TabIndex = 6;
            this.btnRedirect.Text = "转到Test窗体";
            this.btnRedirect.UseVisualStyleBackColor = true;
            this.btnRedirect.Click += new System.EventHandler(this.btnRedirect_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(101, 93);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 7;
            this.btnTest.Text = "测试";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnPickUp
            // 
            this.btnPickUp.Location = new System.Drawing.Point(182, 93);
            this.btnPickUp.Name = "btnPickUp";
            this.btnPickUp.Size = new System.Drawing.Size(75, 23);
            this.btnPickUp.TabIndex = 8;
            this.btnPickUp.Text = "配置Xml";
            this.btnPickUp.UseVisualStyleBackColor = true;
            this.btnPickUp.Click += new System.EventHandler(this.btnPickUp_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 123);
            this.Controls.Add(this.btnPickUp);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnRedirect);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtContent3);
            this.Controls.Add(this.txtContent2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtContent1);
            this.Controls.Add(this.label1);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "主窗体";
            this.Activated += new System.EventHandler(this.Form_Activated);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtContent1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtContent2;
        private System.Windows.Forms.TextBox txtContent3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRedirect;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnPickUp;
    }
}

