namespace P117_EMIReadCode
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
            this.lblLeftState = new System.Windows.Forms.Label();
            this.btnLock = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxRightSN = new System.Windows.Forms.TextBox();
            this.tbxLeftSN = new System.Windows.Forms.TextBox();
            this.lblSn = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLeftBurn = new System.Windows.Forms.Button();
            this.cbxDoWork = new System.Windows.Forms.CheckBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnRigthBurn = new System.Windows.Forms.Button();
            this.lblRigthState = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.gbxTitle.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxTitle
            // 
            this.gbxTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gbxTitle.Controls.Add(this.lblLeftState);
            this.gbxTitle.Controls.Add(this.btnLock);
            this.gbxTitle.Controls.Add(this.panel1);
            this.gbxTitle.Controls.Add(this.groupBox1);
            this.gbxTitle.Controls.Add(this.lblRigthState);
            this.gbxTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxTitle.Font = new System.Drawing.Font("宋体", 12F);
            this.gbxTitle.Location = new System.Drawing.Point(0, 0);
            this.gbxTitle.Margin = new System.Windows.Forms.Padding(2);
            this.gbxTitle.Name = "gbxTitle";
            this.gbxTitle.Padding = new System.Windows.Forms.Padding(2);
            this.gbxTitle.Size = new System.Drawing.Size(396, 148);
            this.gbxTitle.TabIndex = 0;
            this.gbxTitle.TabStop = false;
            this.gbxTitle.Text = "烧录";
            // 
            // lblLeftState
            // 
            this.lblLeftState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLeftState.BackColor = System.Drawing.Color.GreenYellow;
            this.lblLeftState.Location = new System.Drawing.Point(340, 0);
            this.lblLeftState.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLeftState.Name = "lblLeftState";
            this.lblLeftState.Size = new System.Drawing.Size(26, 22);
            this.lblLeftState.TabIndex = 6;
            this.lblLeftState.Text = "左";
            this.lblLeftState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLock
            // 
            this.btnLock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLock.Font = new System.Drawing.Font("宋体", 9F);
            this.btnLock.Location = new System.Drawing.Point(292, -1);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(39, 23);
            this.btnLock.TabIndex = 0;
            this.btnLock.Text = "解锁";
            this.btnLock.UseVisualStyleBackColor = true;
            this.btnLock.Click += new System.EventHandler(this.btnLock_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbxRightSN);
            this.panel1.Controls.Add(this.tbxLeftSN);
            this.panel1.Controls.Add(this.lblSn);
            this.panel1.Controls.Add(this.lblTime);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(2, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(290, 125);
            this.panel1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 8F);
            this.label2.Location = new System.Drawing.Point(20, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 11);
            this.label2.TabIndex = 6;
            this.label2.Text = "右:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 8F);
            this.label1.Location = new System.Drawing.Point(20, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 11);
            this.label1.TabIndex = 5;
            this.label1.Text = "左:";
            // 
            // tbxRightSN
            // 
            this.tbxRightSN.Location = new System.Drawing.Point(51, 66);
            this.tbxRightSN.Name = "tbxRightSN";
            this.tbxRightSN.Size = new System.Drawing.Size(227, 26);
            this.tbxRightSN.TabIndex = 4;
            // 
            // tbxLeftSN
            // 
            this.tbxLeftSN.Location = new System.Drawing.Point(51, 34);
            this.tbxLeftSN.Name = "tbxLeftSN";
            this.tbxLeftSN.Size = new System.Drawing.Size(227, 26);
            this.tbxLeftSN.TabIndex = 3;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLeftBurn);
            this.groupBox1.Controls.Add(this.cbxDoWork);
            this.groupBox1.Controls.Add(this.btnTest);
            this.groupBox1.Controls.Add(this.btnRigthBurn);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(292, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(102, 125);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "测试";
            // 
            // btnLeftBurn
            // 
            this.btnLeftBurn.Font = new System.Drawing.Font("宋体", 8F);
            this.btnLeftBurn.Location = new System.Drawing.Point(6, 88);
            this.btnLeftBurn.Name = "btnLeftBurn";
            this.btnLeftBurn.Size = new System.Drawing.Size(36, 23);
            this.btnLeftBurn.TabIndex = 5;
            this.btnLeftBurn.Text = "左";
            this.btnLeftBurn.UseVisualStyleBackColor = true;
            this.btnLeftBurn.Click += new System.EventHandler(this.btnLeftBurn_Click);
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
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(6, 59);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(88, 23);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "测试";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnRigthBurn
            // 
            this.btnRigthBurn.Font = new System.Drawing.Font("宋体", 8F);
            this.btnRigthBurn.Location = new System.Drawing.Point(62, 88);
            this.btnRigthBurn.Name = "btnRigthBurn";
            this.btnRigthBurn.Size = new System.Drawing.Size(32, 23);
            this.btnRigthBurn.TabIndex = 0;
            this.btnRigthBurn.Text = "右";
            this.btnRigthBurn.UseVisualStyleBackColor = true;
            this.btnRigthBurn.Click += new System.EventHandler(this.btnRigthBurn_Click);
            // 
            // lblRigthState
            // 
            this.lblRigthState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRigthState.BackColor = System.Drawing.Color.GreenYellow;
            this.lblRigthState.Location = new System.Drawing.Point(370, 0);
            this.lblRigthState.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRigthState.Name = "lblRigthState";
            this.lblRigthState.Size = new System.Drawing.Size(26, 22);
            this.lblRigthState.TabIndex = 3;
            this.lblRigthState.Text = "右";
            this.lblRigthState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // BurnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 148);
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
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblSn;
        private System.Windows.Forms.Label lblRigthState;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbxLeftSN;
        private System.Windows.Forms.Button btnLock;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnRigthBurn;
        private System.Windows.Forms.CheckBox cbxDoWork;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxRightSN;
        private System.Windows.Forms.Label lblLeftState;
        private System.Windows.Forms.Button btnLeftBurn;
    }
}