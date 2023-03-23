using Cognex.VisionPro.ToolBlock;
using P117_EMIReadCode.Comm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P117_EMIReadCode
{
    public partial class FrmVisionSetting : Form
    {
        private readonly FrmVisionUpdate _leftFrmVisionUpdate;
        private readonly FrmVisionUpdate _rightFrmVisionUpdate;
        public FrmVisionSetting(Func<CogToolBlock,Task> leftcallback, Func<CogToolBlock,Task> rightcallback)
        {
            InitializeComponent();
            _leftFrmVisionUpdate = new FrmVisionUpdate(DataContent.SystemConfig.LeftVppPath,"B", DataContent.SystemConfig.LeftCamera,(vpppath,cameraConfig, toolblock) =>
            {
                DataContent.SystemConfig.LeftVppPath = vpppath;
                DataContent.SystemConfig.LeftCamera = cameraConfig;
                leftcallback(toolblock).Wait();
            });
            _rightFrmVisionUpdate = new FrmVisionUpdate(DataContent.SystemConfig.RigthVppPath,"A", DataContent.SystemConfig.RigthCamera, (vpppath, cameraConfig, toolblock) =>
            {
                DataContent.SystemConfig.RigthVppPath = vpppath;
                DataContent.SystemConfig.RigthCamera = cameraConfig;
                rightcallback(toolblock).Wait();
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
