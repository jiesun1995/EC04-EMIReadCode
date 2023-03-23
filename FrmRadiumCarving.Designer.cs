namespace P117_EMIReadCode
{
    partial class FrmRadiumCarving
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxRigthSN = new System.Windows.Forms.TextBox();
            this.tbxLeftSN = new System.Windows.Forms.TextBox();
            this.lblSn = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxDoWork = new System.Windows.Forms.CheckBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnRigth = new System.Windows.Forms.Button();
            this.lblState = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnLeftMes = new System.Windows.Forms.Button();
            this.btnRigthMes = new System.Windows.Forms.Button();
            this.cbxSN = new System.Windows.Forms.CheckBox();
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
            this.gbxTitle.Size = new System.Drawing.Size(394, 162);
            this.gbxTitle.TabIndex = 0;
            this.gbxTitle.TabStop = false;
            this.gbxTitle.Text = "镭雕 ";
            // 
            // btnLock
            // 
            this.btnLock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLock.Font = new System.Drawing.Font("宋体", 9F);
            this.btnLock.Location = new System.Drawing.Point(324, 0);
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
            this.panel1.Controls.Add(this.tbxRigthSN);
            this.panel1.Controls.Add(this.tbxLeftSN);
            this.panel1.Controls.Add(this.lblSn);
            this.panel1.Controls.Add(this.lblTime);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(2, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(275, 139);
            this.panel1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 8F);
            this.label2.Location = new System.Drawing.Point(16, 74);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 11);
            this.label2.TabIndex = 6;
            this.label2.Text = "右:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 8F);
            this.label1.Location = new System.Drawing.Point(16, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 11);
            this.label1.TabIndex = 5;
            this.label1.Text = "左:";
            // 
            // tbxRigthSN
            // 
            this.tbxRigthSN.Location = new System.Drawing.Point(45, 66);
            this.tbxRigthSN.Name = "tbxRigthSN";
            this.tbxRigthSN.Size = new System.Drawing.Size(216, 26);
            this.tbxRigthSN.TabIndex = 4;
            // 
            // tbxLeftSN
            // 
            this.tbxLeftSN.Location = new System.Drawing.Point(45, 34);
            this.tbxLeftSN.Name = "tbxLeftSN";
            this.tbxLeftSN.Size = new System.Drawing.Size(216, 26);
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
            this.groupBox1.Controls.Add(this.cbxSN);
            this.groupBox1.Controls.Add(this.btnRigthMes);
            this.groupBox1.Controls.Add(this.btnLeftMes);
            this.groupBox1.Controls.Add(this.cbxDoWork);
            this.groupBox1.Controls.Add(this.btnTest);
            this.groupBox1.Controls.Add(this.btnLeft);
            this.groupBox1.Controls.Add(this.btnRigth);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(277, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(115, 139);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "测试";
            // 
            // cbxDoWork
            // 
            this.cbxDoWork.AutoSize = true;
            this.cbxDoWork.Font = new System.Drawing.Font("宋体", 9F);
            this.cbxDoWork.Location = new System.Drawing.Point(6, 25);
            this.cbxDoWork.Name = "cbxDoWork";
            this.cbxDoWork.Size = new System.Drawing.Size(48, 16);
            this.cbxDoWork.TabIndex = 3;
            this.cbxDoWork.Text = "屏蔽";
            this.cbxDoWork.UseVisualStyleBackColor = true;
            this.cbxDoWork.CheckedChanged += new System.EventHandler(this.cbxDoWork_CheckedChanged);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(6, 49);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(103, 23);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "测试";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Enabled = false;
            this.btnLeft.Font = new System.Drawing.Font("宋体", 9F);
            this.btnLeft.Location = new System.Drawing.Point(6, 78);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(47, 23);
            this.btnLeft.TabIndex = 1;
            this.btnLeft.Text = "左";
            this.btnLeft.UseVisualStyleBackColor = true;
            // 
            // btnRigth
            // 
            this.btnRigth.Enabled = false;
            this.btnRigth.Font = new System.Drawing.Font("宋体", 9F);
            this.btnRigth.Location = new System.Drawing.Point(59, 78);
            this.btnRigth.Name = "btnRigth";
            this.btnRigth.Size = new System.Drawing.Size(50, 23);
            this.btnRigth.TabIndex = 0;
            this.btnRigth.Text = "右";
            this.btnRigth.UseVisualStyleBackColor = true;
            // 
            // lblState
            // 
            this.lblState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblState.BackColor = System.Drawing.Color.GreenYellow;
            this.lblState.Location = new System.Drawing.Point(368, 0);
            this.lblState.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(26, 22);
            this.lblState.TabIndex = 3;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnLeftMes
            // 
            this.btnLeftMes.Font = new System.Drawing.Font("宋体", 8F);
            this.btnLeftMes.Location = new System.Drawing.Point(6, 107);
            this.btnLeftMes.Name = "btnLeftMes";
            this.btnLeftMes.Size = new System.Drawing.Size(47, 23);
            this.btnLeftMes.TabIndex = 4;
            this.btnLeftMes.Text = "左mes";
            this.btnLeftMes.UseVisualStyleBackColor = true;
            this.btnLeftMes.Click += new System.EventHandler(this.btnLeftMes_Click);
            // 
            // btnRigthMes
            // 
            this.btnRigthMes.Font = new System.Drawing.Font("宋体", 8F);
            this.btnRigthMes.Location = new System.Drawing.Point(59, 107);
            this.btnRigthMes.Name = "btnRigthMes";
            this.btnRigthMes.Size = new System.Drawing.Size(50, 23);
            this.btnRigthMes.TabIndex = 5;
            this.btnRigthMes.Text = "右mes";
            this.btnRigthMes.UseVisualStyleBackColor = true;
            this.btnRigthMes.Click += new System.EventHandler(this.btnRigthMes_Click);
            // 
            // cbxSN
            // 
            this.cbxSN.AutoSize = true;
            this.cbxSN.Font = new System.Drawing.Font("宋体", 9F);
            this.cbxSN.Location = new System.Drawing.Point(52, 25);
            this.cbxSN.Name = "cbxSN";
            this.cbxSN.Size = new System.Drawing.Size(60, 16);
            this.cbxSN.TabIndex = 6;
            this.cbxSN.Text = "固定SN";
            this.cbxSN.UseVisualStyleBackColor = true;
            this.cbxSN.CheckedChanged += new System.EventHandler(this.cbxSN_CheckedChanged);
            // 
            // FrmRadiumCarving
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 162);
            this.Controls.Add(this.gbxTitle);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmRadiumCarving";
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
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbxLeftSN;
        private System.Windows.Forms.Button btnLock;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnRigth;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.CheckBox cbxDoWork;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxRigthSN;
        private System.Windows.Forms.Button btnRigthMes;
        private System.Windows.Forms.Button btnLeftMes;
        private System.Windows.Forms.CheckBox cbxSN;
    }
}