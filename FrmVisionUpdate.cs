using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro;
using EC04_EMIReadCode.Comm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EC04_EMIReadCode
{
    public partial class FrmVisionUpdate : Form
    {
        private string _vppPath;
        private CameraConfig _cameraConfig;
        private readonly Action<string,CameraConfig, CogToolBlock> _callBack;
        public FrmVisionUpdate(string vppPath,CameraConfig cameraConfig,Action<string,CameraConfig, CogToolBlock> CallBack)
        {
            _callBack=CallBack;
            _vppPath = vppPath;
            _cameraConfig = cameraConfig;
            InitializeComponent();
            if(!string.IsNullOrWhiteSpace(_vppPath) && File.Exists(_vppPath))
            {
                cogToolBlockEditV21.Subject= CogSerializer.LoadObjectFromFile(_vppPath) as CogToolBlock;
            }
            else
            {
                cogToolBlockEditV21.Subject = new CogToolBlock();
            }
            tbxName.Text = _cameraConfig.Name;
            nunExposureTime.Text = _cameraConfig.ExposureTime;
            nunGain.Text = _cameraConfig.Gain;
            tbxVppPath.Text= _vppPath;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog=new OpenFileDialog();
            openFileDialog.Filter = "(*.vpp)|";
            openFileDialog.Title = "选择运行的vpp程序";
            openFileDialog.InitialDirectory = File.Exists(_vppPath) ? _vppPath : Environment.CurrentDirectory;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _vppPath=openFileDialog.FileName;
                tbxVppPath.Text=openFileDialog.FileName;
                cogToolBlockEditV21.Subject = CogSerializer.LoadObjectFromFile(_vppPath) as CogToolBlock;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(_vppPath) && File.Exists(_vppPath))
                {
                    CameraConfig cameraConfig = new CameraConfig
                    {
                        Name=tbxName.Text,
                        ExposureTime=nunExposureTime.Value.ToString(),
                        Gain=nunGain.Value.ToString(),
                    };
                    this.Cursor = Cursors.WaitCursor;
                    CogSerializer.SaveObjectToFile(cogToolBlockEditV21.Subject, _vppPath);
                    this.Cursor = Cursors.Default;
                    _callBack(_vppPath,cameraConfig,cogToolBlockEditV21.Subject);
                    DataContent.SetConfig(DataContent.SystemConfig);
                    MessageBox.Show("VPP程序保存成功!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("VPP程序保存失败" + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ICogImage cogImage = null;
            try
            {
                var camera = CameraHelper.Instance.Open(tbxName.Text);
                CameraHelper.Instance.SetExposureTime(camera, nunExposureTime.Value.ToString());
                CameraHelper.Instance.SetGain(camera, nunGain.Value.ToString());
                CameraHelper.Instance.GrabImageToCogImg(camera, out cogImage);
                cogToolBlockEditV21.Subject.Inputs["IN_Image"].Value = cogImage;
                cogToolBlockEditV21.Subject.Run();
            }
            catch (Exception ex)
            {
                LogManager.Logs.Error(ex);
            }
        }
    }
}
