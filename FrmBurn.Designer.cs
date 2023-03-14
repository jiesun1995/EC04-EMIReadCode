namespace EC04_EMIReadCode
{
    partial class BurnForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gbxTitle = new System.Windows.Forms.GroupBox();
            this.btnLock = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbxSN = new System.Windows.Forms.TextBox();
            this.lblSn = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.lblState = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnBurn = new System.Windows.Forms.Button();
            this.cbxDoWork = new System.Windows.Forms.CheckBox();
            this.gbxTitle.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxTitle
            // 
            this.gbxTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gbxTitle.Controls.Add(this.btnLock);
            this.gbxTitle.Controls.Add(this.panel1);
            this.gbxTitle.Controls.Add(this.groupBox1);
            this.gbxTitle.Controls.Add(this.lblState);
            this.gbxTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxTitle.Font = new System.Drawing.Font("宋体", 12F);
            this.gbxTitle.Location = new System.Drawing.Point(0, 0);
            this.gbxTitle.Margin = new System.Windows.Forms.Padding(2);
            this.gbxTitle.Name = "gbxTitle";
            this.gbxTitle.Padding = new System.Windows.Forms.Padding(2);
            this.gbxTitle.Size = new System.Drawing.Size(390, 205);
            this.gbxTitle.TabIndex = 0;
            this.gbxTitle.TabStop = false;
            this.gbxTitle.Text = "烧录";
            // 
            // btnLock
            // 
            this.btnLock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLock.Font = new System.Drawing.Font("宋体", 9F);
            this.btnLock.Location = new System.Drawing.Point(320, 0);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(39, 23);
            this.btnLock.TabIndex = 0;
            this.btnLock.Text = "解锁";
            this.btnLock.UseVisualStyleBackColor = true;
            this.btnLock.Click += new System.EventHandler(this.btnLock_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbxSN);
            this.panel1.Controls.Add(this.lblSn);
            this.panel1.Controls.Add(this.lblTime);
            this.panel1.Controls.Add(this.lblResult);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(2, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 182);
            this.panel1.TabIndex = 4;
            // 
            // tbxSN
            // 
            this.tbxSN.Location = new System.Drawing.Point(12, 34);
            this.tbxSN.Multiline = true;
            this.tbxSN.Name = "tbxSN";
            this.tbxSN.Size = new System.Drawing.Size(249, 62);
            this.tbxSN.TabIndex = 3;
            // 
            // lblSn
            // 
            this.lblSn.AutoSize = true;
            this.lblSn.Font = new System.Drawing.Font("宋体", 13F);
            this.lblSn.Location = new System.Drawing.Point(9, 12);
            this.lblSn.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSn.Name = "lblSn";
            this.lblSn.Size = new System.Drawing.Size(71, 18);
            this.lblSn.TabIndex = 0;
            this.lblSn.Text = "产品SN:";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("宋体", 9F);
            this.lblTime.Location = new System.Drawing.Point(10, 99);
            this.lblTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(35, 12);
            this.lblTime.TabIndex = 1;
            this.lblTime.Text = "耗时:";
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("宋体", 9F);
            this.lblResult.Location = new System.Drawing.Point(10, 136);
            this.lblResult.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(35, 12);
            this.lblResult.TabIndex = 2;
            this.lblResult.Text = "结果:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxDoWork);
            this.groupBox1.Controls.Add(this.btnTest);
            this.groupBox1.Controls.Add(this.btnBurn);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(286, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(102, 182);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "测试";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(6, 59);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(85, 23);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "测试";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // lblState
            // 
            this.lblState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblState.BackColor = System.Drawing.Color.GreenYellow;
            this.lblState.Location = new System.Drawing.Point(364, 0);
            this.lblState.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(26, 22);
            this.lblState.TabIndex = 3;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnBurn
            // 
            this.btnBurn.Font = new System.Drawing.Font("宋体", 9F);
            this.btnBurn.Location = new System.Drawing.Point(43, 88);
            this.btnBurn.Name = "btnBurn";
            this.btnBurn.Size = new System.Drawing.Size(48, 23);
            this.btnBurn.TabIndex = 0;
            this.btnBurn.Text = "烧录";
            this.btnBurn.UseVisualStyleBackColor = true;
            this.btnBurn.Click += new System.EventHandler(this.btnBurn_Click);
            // 
            // cbxDoWork
            // 
            this.cbxDoWork.AutoSize = true;
            this.cbxDoWork.Location = new System.Drawing.Point(6, 34);
            this.cbxDoWork.Name = "cbxDoWork";
            this.cbxDoWork.Size = new System.Drawing.Size(59, 20);
            this.cbxDoWork.TabIndex = 4;
            this.cbxDoWork.Text = "屏蔽";
            this.cbxDoWork.UseVisualStyleBackColor = true;
            this.cbxDoWork.CheckedChanged += new System.EventHandler(this.cbxDoWork_CheckedChanged);
            // 
            // BurnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 205);
            this.Controls.Add(this.gbxTitle);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "BurnForm";
            this.Text = "BurnForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BurnForm_FormClosed);
            this.gbxTitle.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxTitle;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblSn;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbxSN;
        private System.Windows.Forms.Button btnLock;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnBurn;
        private System.Windows.Forms.CheckBox cbxDoWork;
    }
}