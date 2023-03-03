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
            this.lblResult = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblSn = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.gbxTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxTitle
            // 
            this.gbxTitle.Controls.Add(this.lblState);
            this.gbxTitle.Controls.Add(this.lblResult);
            this.gbxTitle.Controls.Add(this.lblTime);
            this.gbxTitle.Controls.Add(this.lblSn);
            this.gbxTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxTitle.Font = new System.Drawing.Font("宋体", 12F);
            this.gbxTitle.Location = new System.Drawing.Point(0, 0);
            this.gbxTitle.Name = "gbxTitle";
            this.gbxTitle.Size = new System.Drawing.Size(341, 209);
            this.gbxTitle.TabIndex = 0;
            this.gbxTitle.TabStop = false;
            this.gbxTitle.Text = "烧录";
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(13, 179);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(99, 20);
            this.lblResult.TabIndex = 2;
            this.lblResult.Text = "烧录结果:";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(13, 109);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(99, 20);
            this.lblTime.TabIndex = 1;
            this.lblTime.Text = "烧录耗时:";
            // 
            // lblSn
            // 
            this.lblSn.AutoSize = true;
            this.lblSn.Font = new System.Drawing.Font("宋体", 13F);
            this.lblSn.Location = new System.Drawing.Point(12, 36);
            this.lblSn.Name = "lblSn";
            this.lblSn.Size = new System.Drawing.Size(87, 22);
            this.lblSn.TabIndex = 0;
            this.lblSn.Text = "产品SN:";
            // 
            // lblState
            // 
            this.lblState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblState.BackColor = System.Drawing.Color.GreenYellow;
            this.lblState.Location = new System.Drawing.Point(300, 26);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(35, 27);
            this.lblState.TabIndex = 3;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // BurnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 209);
            this.Controls.Add(this.gbxTitle);
            this.Name = "BurnForm";
            this.Text = "BurnForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BurnForm_FormClosed);
            this.gbxTitle.ResumeLayout(false);
            this.gbxTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxTitle;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblSn;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Timer timer1;
    }
}