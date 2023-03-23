namespace P117_EMIReadCode
{
    partial class FrmInternetConfig
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
            this.gbxTitle = new System.Windows.Forms.GroupBox();
            this.tbxPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxIp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbxTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxTitle
            // 
            this.gbxTitle.Controls.Add(this.tbxPort);
            this.gbxTitle.Controls.Add(this.label2);
            this.gbxTitle.Controls.Add(this.tbxIp);
            this.gbxTitle.Controls.Add(this.label1);
            this.gbxTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxTitle.Location = new System.Drawing.Point(0, 0);
            this.gbxTitle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbxTitle.Name = "gbxTitle";
            this.gbxTitle.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbxTitle.Size = new System.Drawing.Size(250, 103);
            this.gbxTitle.TabIndex = 1;
            this.gbxTitle.TabStop = false;
            this.gbxTitle.Text = "网络配置";
            // 
            // tbxPort
            // 
            this.tbxPort.Location = new System.Drawing.Point(57, 54);
            this.tbxPort.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxPort.Name = "tbxPort";
            this.tbxPort.Size = new System.Drawing.Size(164, 21);
            this.tbxPort.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 62);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "端口：";
            // 
            // tbxIp
            // 
            this.tbxIp.Location = new System.Drawing.Point(57, 19);
            this.tbxIp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbxIp.Name = "tbxIp";
            this.tbxIp.Size = new System.Drawing.Size(164, 21);
            this.tbxIp.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP：";
            // 
            // FrmInternetConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 103);
            this.Controls.Add(this.gbxTitle);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmInternetConfig";
            this.Text = "FrmInternetConfig";
            this.gbxTitle.ResumeLayout(false);
            this.gbxTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxTitle;
        private System.Windows.Forms.TextBox tbxPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxIp;
        private System.Windows.Forms.Label label1;
    }
}