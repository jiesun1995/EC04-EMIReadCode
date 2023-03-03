using Cognex.VisionPro;
using EC04_EMIReadCode.Comm;
using MvCamCtrl.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EC04_EMIReadCode
{
    public partial class FrmVisionDisplay : Form
    {
        private MyCamera _camera;
        private VisionHelper _visionHelper;
        private readonly Stopwatch _stopwatch;
        private string _cameraName;
        public FrmVisionDisplay(string vppPath,string cameraName)
        {
            _stopwatch=new Stopwatch();
            _cameraName = cameraName;
            InitializeComponent();
            LoadVision(vppPath, cameraName);

            lblState.BackColor = _camera != null ? Color.GreenYellow : Color.Red;
        }
        public void LoadVision(string vppPath,string cameraName) 
        {
            try
            {
                _visionHelper = new VisionHelper(vppPath);
                _camera = CameraHelper.Instance.Open(cameraName);
            }
            catch (Exception ex)
            {
                LogManager.Error(ex);
            }
        }

        public Tuple<string,string> Run(string ExposureTime,string Gain)
        {
            ICogImage cogImage ;
            CameraHelper.Instance.SetExposureTime(_camera, ExposureTime);
            CameraHelper.Instance.SetGain(_camera, Gain);
            CameraHelper.Instance.GrabImageToCogImg(_camera,out cogImage);
            _visionHelper.SetInput("IN_Image", cogImage);
            _stopwatch.Restart();
            _visionHelper.Run();
            _stopwatch.Stop();
            _visionHelper.DisPaly(cogRecordDisplay1);
            var result = bool.Parse(_visionHelper.GetOutput("Result"));
            var leftCode = _visionHelper.GetOutput("LeftCode");
            var rightCode = _visionHelper.GetOutput("RightCode");
            lblResult.Text = $"结果：\r\n左：{leftCode}\r\n右：{rightCode}";
            lblResultColor.BackColor=result? Color.Gray : Color.Red;
            lblTime.Text = $"耗时：\r\n{_stopwatch.Elapsed.TotalMilliseconds}ms";
            return new Tuple<string, string>(leftCode, rightCode);
        }

        private void FrmVisionDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            CameraHelper.Instance.Close(_cameraName, _camera);
        }

    }
}
