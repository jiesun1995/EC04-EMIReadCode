namespace EC04_EMIReadCode
{
    partial class RadiumCarvingForm
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
            this.lblSn = new System.Windows.Forms.Label();
            this.gbxTitle = new System.Windows.Forms.GroupBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.gbxTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSn
            // 
            this.lblSn.AutoSize = true;
            this.lblSn.Font = new System.Drawing.Font("宋体", 13F);
            this.lblSn.Location = new System.Drawing.Point(12, 26);
            this.lblSn.Name = "lblSn";
            this.lblSn.Size = new System.Drawing.Size(87, 22);
            this.lblSn.TabIndex = 0;
            this.lblSn.Text = "产品SN:";
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
            this.gbxTitle.Size = new System.Drawing.Size(341, 202);
            this.gbxTitle.TabIndex = 1;
            this.gbxTitle.TabStop = false;
            this.gbxTitle.Text = "镭雕";
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(13, 169);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(99, 20);
            this.lblResult.TabIndex = 2;
            this.lblResult.Text = "镭雕结果:";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(13, 99);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(99, 20);
            this.lblTime.TabIndex = 1;
            this.lblTime.Text = "镭雕耗时:";
            // 
            // lblState
            // 
            this.lblState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblState.BackColor = System.Drawing.Color.GreenYellow;
            this.lblState.Location = new System.Drawing.Point(300, 26);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(35, 27);
            this.lblState.TabIndex = 4;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // RadiumCarvingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 202);
            this.Controls.Add(this.gbxTitle);
            this.Name = "RadiumCarvingForm";
            this.Text = "RadiumCarvingForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RadiumCarvingForm_FormClosed);
            this.gbxTitle.ResumeLayout(false);
            this.gbxTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSn;
        private System.Windows.Forms.GroupBox gbxTitle;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Timer timer1;
    }
}