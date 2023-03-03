﻿using Cognex.VisionPro.ToolBlock;
using EC04_EMIReadCode.Comm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EC04_EMIReadCode
{
    public partial class FrmVisionSetting : Form
    {
        private CameraConfig _leftCameraConfig;
        private CameraConfig _rightCameraConfig;
        private readonly FrmVisionUpdate _leftFrmVisionUpdate;
        private readonly FrmVisionUpdate _rightFrmVisionUpdate;
        public FrmVisionSetting(Action<CogToolBlock> leftcallback, Action<CogToolBlock> rightcallback)
        {
            InitializeComponent();
            _leftFrmVisionUpdate = new FrmVisionUpdate(DataContent.SystemConfig.LeftVppPath, DataContent.SystemConfig.LeftCamera,(vpppath,cameraConfig, toolblock) =>
            {
                DataContent.SystemConfig.LeftVppPath = vpppath;
                DataContent.SystemConfig.LeftCamera = cameraConfig;
                leftcallback(toolblock);
            });
            _rightFrmVisionUpdate = new FrmVisionUpdate(DataContent.SystemConfig.RightVppPath, DataContent.SystemConfig.RightCamera, (vpppath, cameraConfig, toolblock) =>
            {
                DataContent.SystemConfig.RightVppPath = vpppath;
                DataContent.SystemConfig.RightCamera = cameraConfig;
                rightcallback(toolblock);
            });

            _leftFrmVisionUpdate.TopLevel = false;
            _leftFrmVisionUpdate.Dock = DockStyle.Fill;
            _leftFrmVisionUpdate.FormBorderStyle = FormBorderStyle.None;
            _leftFrmVisionUpdate.Show();
            tabPage1.Controls.Add(_leftFrmVisionUpdate);

            _rightFrmVisionUpdate.TopLevel = false;
            _rightFrmVisionUpdate.Dock = DockStyle.Fill;
            _rightFrmVisionUpdate.FormBorderStyle = FormBorderStyle.None;
            _rightFrmVisionUpdate.Show();
            tabPage2.Controls.Add(_rightFrmVisionUpdate);
        }

        private void FrmVisionSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("请确认保存", "退出保存询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    
                }
                else
                {
                    e.Cancel = true;
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("窗体退出失败" + ex.ToString());
            }
        }
    }
}