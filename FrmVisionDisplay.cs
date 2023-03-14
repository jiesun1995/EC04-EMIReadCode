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
        private ICogImage _cogImage;
        public FrmVisionDisplay(string vppPath,string cameraName,string title="相机")
        {
            _stopwatch=new Stopwatch();
            _cameraName = cameraName;
            InitializeComponent();
            LoadVision(vppPath, cameraName).Wait();
            gbxTitle.Text = title;
            lblState.BackColor = _camera != null ? Color.GreenYellow : Color.Red;
        }
        private string BuildEmpty(int length)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append(" ");
            }
            return sb.ToString();
        }
        public Task LoadVision(string vppPath,string cameraName) 
        {
            var task = new TaskFactory().StartNew(() =>
            {
                try
                {
                    _visionHelper = new VisionHelper(vppPath);
                    _camera = CameraHelper.Instance.Open(cameraName);
                }
                catch (Exception ex)
                {
                    LogManager.Logs.Error(ex);
                }
            });
            return task;
        }
        private ICogImage RunCamera(string exposureTime="", string gain = "")
        {
            ICogImage cogImage=null;
            try
            {
                if (!string.IsNullOrWhiteSpace(exposureTime))
                    CameraHelper.Instance.SetExposureTime(_camera, exposureTime);
                if (!string.IsNullOrWhiteSpace(gain))
                    CameraHelper.Instance.SetGain(_camera, gain);
                CameraHelper.Instance.GrabImageToCogImg(_camera, out cogImage);
                if (cogImage == null)
                    throw new ArgumentNullException();
                //cogImage.ToBitmap
                SystemHelper.UIShow(btnCamera, () =>
                {
                    btnCamera.BackColor = Color.Green;
                    lblState.BackColor = Color.Green;
                });
            }
            catch (Exception ex)
            {
                LogManager.Logs.Error(ex);
                SystemHelper.UIShow(btnCamera, () =>
                {
                    btnCamera.BackColor = Color.Red;
                    btnCamera.BackColor = Color.Red;
                });
            }
            return cogImage;
        }
        private Tuple<Tuple<bool, string>, Tuple<bool, string>> RunReadCode(ICogImage cogImage)
        {
            try
            {
                _visionHelper.SetInput("IN_Image", cogImage);
                _visionHelper.Run();
                
                var leftResult = bool.Parse(_visionHelper.GetOutput("LeftResult"));
                var rightResult = bool.Parse(_visionHelper.GetOutput("RightResult"));
                var leftCode = leftResult? _visionHelper.GetOutput("LeftCode"):"NG";
                var rightCode = rightResult? _visionHelper.GetOutput("RightCode"):"NG";

                SystemHelper.UIShow(btnReadCode, () =>
                {
                    btnReadCode.BackColor = Color.Green;
                    _visionHelper.DisPaly(cogRecordDisplay1);
                });
                return new Tuple<Tuple<bool, string>, Tuple<bool, string>>
                    (new Tuple<bool, string>(leftResult, leftCode), new Tuple<bool, string>(rightResult, rightCode));
            }
            catch (Exception ex)
            {
                LogManager.Logs.Error(ex);
                SystemHelper.UIShow(btnReadCode, () =>
                 {
                     btnReadCode.BackColor = Color.Red;
                 });
                return null;
            }
        }
        private void ShowUI(Tuple<Tuple<bool, string>, Tuple<bool, string>> data)
        {
            var leftResult = data.Item1.Item1;
            var rightResult = data.Item2.Item1;
            var leftCode = data.Item1.Item2;
            var rightCode = data.Item2.Item2;
            LogManager.Logs.Info($"读取到产品sn[{leftCode}:{rightCode}]");
            SystemHelper.UIShow(lblResult, () =>
             {
                 lblResult.Text = $"结果：\r\n左：{leftCode}\r\n右：{rightCode}";
                 lblResultColor.BackColor = leftResult && rightResult ? Color.Green : Color.Red;
                 lblTime.Text = $"耗时：\r\n{_stopwatch.Elapsed.TotalMilliseconds}ms";
             });
        }
        public Tuple<string,string> Run(string exposureTime,string gain)
        {
            _stopwatch.Restart();
            ICogImage cogImage;
            cogImage = RunCamera(exposureTime, gain);
            var data = RunReadCode(cogImage);
            _stopwatch.Stop();
            ShowUI(data);
            return new Tuple<string, string>(data.Item1.Item2, data.Item2.Item2);
        }
        
        private void FrmVisionDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_camera != null)
                CameraHelper.Instance.Close(_cameraName, _camera);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            Run(string.Empty, string.Empty);
        }

        private void btnCamera_Click(object sender, EventArgs e)
        {
           _cogImage = RunCamera();
        }

        private void btnReadCode_Click(object sender, EventArgs e)
        {
            var data = RunReadCode(_cogImage);
            ShowUI(data);
        }
    }
}
