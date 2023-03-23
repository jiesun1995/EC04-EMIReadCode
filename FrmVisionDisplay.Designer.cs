namespace P117_EMIReadCode
{
    partial class FrmVisionDisplay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVisionDisplay));
            this.gbxTitle = new System.Windows.Forms.GroupBox();
            this.lblState = new System.Windows.Forms.Label();
            this.cogRecordDisplay1 = new Cognex.VisionPro.CogRecordDisplay();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblResultColor = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnReadCode = new System.Windows.Forms.Button();
            this.btnCamera = new System.Windows.Forms.Button();
            this.gbxTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cogRecordDisplay1)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxTitle
            // 
            this.gbxTitle.Controls.Add(this.lblState);
            this.gbxTitle.Controls.Add(this.cogRecordDisplay1);
            this.gbxTitle.Controls.Add(this.panel1);
            this.gbxTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxTitle.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold);
            this.gbxTitle.Location = new System.Drawing.Point(0, 0);
            this.gbxTitle.Margin = new System.Windows.Forms.Padding(2);
            this.gbxTitle.Name = "gbxTitle";
            this.gbxTitle.Padding = new System.Windows.Forms.Padding(2);
            this.gbxTitle.Size = new System.Drawing.Size(436, 306);
            this.gbxTitle.TabIndex = 1;
            this.gbxTitle.TabStop = false;
            this.gbxTitle.Text = "相机";
            // 
            // lblState
            // 
            this.lblState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblState.BackColor = System.Drawing.Color.GreenYellow;
            this.lblState.Location = new System.Drawing.Point(410, -3);
            this.lblState.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(26, 22);
            this.lblState.TabIndex = 4;
            // 
            // cogRecordDisplay1
            // 
            this.cogRecordDisplay1.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.cogRecordDisplay1.ColorMapLowerRoiLimit = 0D;
            this.cogRecordDisplay1.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.cogRecordDisplay1.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.cogRecordDisplay1.ColorMapUpperRoiLimit = 1D;
            this.cogRecordDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cogRecordDisplay1.DoubleTapZoomCycleLength = 2;
            this.cogRecordDisplay1.DoubleTapZoomSensitivity = 2.5D;
            this.cogRecordDisplay1.Location = new System.Drawing.Point(2, 22);
            this.cogRecordDisplay1.Margin = new System.Windows.Forms.Padding(2);
            this.cogRecordDisplay1.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.cogRecordDisplay1.MouseWheelSensitivity = 1D;
            this.cogRecordDisplay1.Name = "cogRecordDisplay1";
            this.cogRecordDisplay1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("cogRecordDisplay1.OcxState")));
            this.cogRecordDisplay1.Size = new System.Drawing.Size(306, 282);
            this.cogRecordDisplay1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(308, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(126, 282);
            this.panel1.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblResultColor);
            this.groupBox2.Controls.Add(this.lblResult);
            this.groupBox2.Controls.Add(this.lblTime);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(0, 85);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(126, 197);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "结果";
            // 
            // lblResultColor
            // 
            this.lblResultColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblResultColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblResultColor.Location = new System.Drawing.Point(2, 19);
            this.lblResultColor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblResultColor.Name = "lblResultColor";
            this.lblResultColor.Size = new System.Drawing.Size(122, 73);
            this.lblResultColor.TabIndex = 2;
            this.lblResultColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("宋体", 7F);
            this.lblResult.Location = new System.Drawing.Point(-2, 103);
            this.lblResult.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(35, 10);
            this.lblResult.TabIndex = 1;
            this.lblResult.Text = "结果：";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("宋体", 7F);
            this.lblTime.Location = new System.Drawing.Point(-2, 142);
            this.lblTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(35, 10);
            this.lblTime.TabIndex = 0;
            this.lblTime.Text = "耗时：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnTest);
            this.groupBox3.Controls.Add(this.btnReadCode);
            this.groupBox3.Controls.Add(this.btnCamera);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(126, 85);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "相机";
            // 
            // btnTest
            // 
            this.btnTest.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.btnTest.Location = new System.Drawing.Point(6, 19);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(99, 27);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "测 试";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnReadCode
            // 
            this.btnReadCode.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.btnReadCode.Location = new System.Drawing.Point(58, 52);
            this.btnReadCode.Name = "btnReadCode";
            this.btnReadCode.Size = new System.Drawing.Size(47, 27);
            this.btnReadCode.TabIndex = 1;
            this.btnReadCode.Text = "扫 码";
            this.btnReadCode.UseVisualStyleBackColor = true;
            this.btnReadCode.Click += new System.EventHandler(this.btnReadCode_Click);
            // 
            // btnCamera
            // 
            this.btnCamera.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.btnCamera.Location = new System.Drawing.Point(5, 52);
            this.btnCamera.Name = "btnCamera";
            this.btnCamera.Size = new System.Drawing.Size(47, 27);
            this.btnCamera.TabIndex = 0;
            this.btnCamera.Text = "拍 照";
            this.btnCamera.UseVisualStyleBackColor = true;
            this.btnCamera.Click += new System.EventHandler(this.btnCamera_Click);
            // 
            // FrmVisionDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 306);
            this.Controls.Add(this.gbxTitle);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmVisionDisplay";
            this.Text = "FrmVisionDisplay";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmVisionDisplay_FormClosing);
            this.gbxTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cogRecordDisplay1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbxTitle;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblResultColor;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblTime;
        private Cognex.VisionPro.CogRecordDisplay cogRecordDisplay1;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnReadCode;
        private System.Windows.Forms.Button btnCamera;
    }
}