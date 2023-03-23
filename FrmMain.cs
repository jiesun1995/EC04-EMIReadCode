using P117_EMIReadCode.Comm;
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

namespace P117_EMIReadCode
{
    public partial class FrmMain : Form
    {
        private BurnForm _frmBurn;
        private FrmRadiumCarving _frmRadiumCarving;
        private readonly PLCHelper _plchelper;
        private FrmVisionDisplay _radiumCarvingCamera;
        private FrmVisionDisplay _burnCamera;
        private readonly Stopwatch _stopwatch;

        private const int _leftBurnAddress = 11;
        private const int _rightBurnAddress = 12;
        private const int _leftBurnResultAddress = 13;
        private const int _rightBurnResultAddress = 15;
        private const int _leftRadiumCarvingAddress = 21;
        private const int _rightRadiumCarvingAddress = 22;
        private const int _leftRadiumCarvingResultAddress = 23;
        private const int _rightRadiumCarvingResultAddress = 25;
        private const int _radiumCarvingAddress = 20;
        public FrmMain()
        {
            
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            try
            {
                Dictionary<int, string> addressDec = new Dictionary<int, string>
                {
                    {_leftBurnAddress,"左烧录扫码" },
                    {_rightBurnAddress,"右烧录扫码" },
                    {_leftBurnResultAddress,"左烧录结果" },
                    {_rightBurnResultAddress,"右烧录结果" },
                    {_leftRadiumCarvingAddress,"左镭雕扫码" },
                    {_rightRadiumCarvingAddress,"右镭雕扫码" },
                    {_leftRadiumCarvingResultAddress,"左镭雕结果" },
                    {_rightRadiumCarvingResultAddress,"右镭雕结果" },
                    {_radiumCarvingAddress,"镭雕空跑" },
                };
                _plchelper = new PLCHelper(DataContent.SystemConfig.PLCConfig.IP, DataContent.SystemConfig.PLCConfig.Port, addressDec);
            }
            catch (Exception ex)
            {
                LogManager.Logs.Error(ex);
            }
            InitializeComponent();
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            tslSystemDate.Text = $"系统时间:{DateTime.Now.ToString()}  |";
            tslRunDate.Text = $"运行时间:{_stopwatch.Elapsed}  |";
            if (_plchelper != null)
            {
                tslPLC.Text = $"PLC状态:{(_plchelper.IsConnect ? "已连接" : "未连接")}   |";
                tslPLC.BackColor = _plchelper.IsConnect ? Color.GreenYellow: Color.Red;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DataContent.User))
            {
                FrmSetting frmSetting = new FrmSetting();
                frmSetting.ShowDialog();
            }
            else
            {
                MessageBox.Show("请先登录");
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (btnLogin.Text == "退出权限")
            {
                DataContent.User = string.Empty;
                btnLogin.Text = "权限登陆";
            }
            else
            {
                FrmLogin frmLogin = new FrmLogin();
                frmLogin.ShowDialog();
                if (!string.IsNullOrEmpty(DataContent.User))
                {
                    btnLogin.Text = "退出权限";
                }
            }
        }
        private TableLayoutPanel LoadFrm(TableLayoutPanel tableLayout, Form form,int col,int row)
        {
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Show();
            tableLayout.Controls.Add(form, col, row);
            return tableLayout;
        }
        private bool CodeParse(string code)
        {
            if (!string.IsNullOrWhiteSpace(code) && code != "NG")
                return true;
            else
                return false;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            
            tabPage1.Controls.Clear();
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.Dock = DockStyle.Fill;
            try
            {
                tableLayoutPanel.RowStyles.Add(new ColumnStyle(SizeType.Percent, 30));
                tableLayoutPanel.RowStyles.Add(new ColumnStyle(SizeType.Percent, 70));
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / tableLayoutPanel.ColumnCount));
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / tableLayoutPanel.ColumnCount));
                var mesService = new MesService();
                _frmRadiumCarving = new FrmRadiumCarving(DataContent.SystemConfig.RadiumCarving.IP, DataContent.SystemConfig.RadiumCarving.Port, mesService, "镭雕工站");
                tableLayoutPanel = LoadFrm(tableLayoutPanel, _frmRadiumCarving, 0, 0);
                _radiumCarvingCamera = new FrmVisionDisplay(DataContent.SystemConfig.RigthVppPath, DataContent.SystemConfig.RigthCamera.Name,"A", "镭雕相机");
                tableLayoutPanel = LoadFrm(tableLayoutPanel, _radiumCarvingCamera, 0, 1);
                _frmBurn = new BurnForm(DataContent.SystemConfig.Burn.IP, DataContent.SystemConfig.Burn.Port, "烧录工站");
                tableLayoutPanel = LoadFrm(tableLayoutPanel, _frmBurn, 1, 0);
                _burnCamera = new FrmVisionDisplay(DataContent.SystemConfig.LeftVppPath, DataContent.SystemConfig.LeftCamera.Name,"B", "烧录相机");
                tableLayoutPanel = LoadFrm(tableLayoutPanel, _burnCamera, 1, 1);
            }
            catch (Exception ex)
            {
                LogManager.Logs.Error(ex);
            }

            tabPage1.Controls.Add(tableLayoutPanel);

            if (_plchelper != null)
            {
                new TaskFactory().StartNew(() =>
                {
                    while (true)
                    {
                        if (_plchelper.Read(_leftBurnAddress) != 1)
                            continue;
                        if (DataContent.Burn)
                        {
                            _plchelper.Write(_leftBurnAddress, PLCResult.OK);
                            _plchelper.Write(_rightBurnAddress, PLCResult.OK);
                            _plchelper.Write(_leftBurnResultAddress, PLCResult.OK);
                            _plchelper.Write(_rightBurnResultAddress, PLCResult.OK);
                            LogManager.Logs.Warn("跳过烧录功能！");
                            continue;
                        }
                        Task.Delay(400).Wait();
                        LogManager.PLCLogs.Info("读取到烧录机启动信号");
                        string leftSN = string.Empty;
                        string rightSN = string.Empty;
                        ///todo 扫码
                        var result = _burnCamera.Run(DataContent.SystemConfig.LeftCamera.ExposureTime, DataContent.SystemConfig.LeftCamera.Gain);
                        leftSN = result.Item1;
                        rightSN = result.Item2;
                        ///数据验证
                        ///
                        if (CodeParse(leftSN))
                        {
                            _plchelper.Write(_leftBurnAddress, PLCResult.OK);
                        }
                        else
                        {
                            leftSN = "NG";
                            _plchelper.Write(_leftBurnAddress, PLCResult.NG);
                            LogManager.BurnLogs.Error($"左产品烧录数据验证失败");
                        }
                        if (CodeParse(rightSN))
                        {
                            _plchelper.Write(_rightBurnAddress, PLCResult.OK);
                        }
                        else
                        {
                            rightSN = "NG";
                            _plchelper.Write(_rightBurnAddress, PLCResult.NG);
                            LogManager.BurnLogs.Error($"右产品烧录数据验证失败");
                        }
                        ///烧录
                        var burnResult = _frmBurn.SendMsg(leftSN, rightSN);
                        _plchelper.Write(_leftBurnResultAddress, burnResult.Item1 ? PLCResult.OK : PLCResult.NG);
                        _plchelper.Write(_rightBurnResultAddress, burnResult.Item2 ? PLCResult.OK : PLCResult.NG);

                        DataContent.CacheData.AddBurnCode(burnResult.Item1 ? "" : leftSN);
                        DataContent.CacheData.AddBurnCode(burnResult.Item2 ? "" : rightSN);
                    }
                }, TaskCreationOptions.LongRunning);
                new TaskFactory().StartNew(() =>
                {
                    while (true)
                    {
                        if (_plchelper.Read(_leftRadiumCarvingAddress) != 1)
                            continue;
                        LogManager.PLCLogs.Info("读取到镭雕机启动信号");
                        if (DataContent.RadiumCarving)
                        {
                            _plchelper.Write(_leftRadiumCarvingAddress, PLCResult.OK);
                            _plchelper.Write(_rightRadiumCarvingAddress, PLCResult.OK);
                            _plchelper.Write(_leftRadiumCarvingResultAddress, PLCResult.OK);
                            _plchelper.Write(_rightRadiumCarvingResultAddress, PLCResult.OK);

                            var leftCode = _frmRadiumCarving.LeftSN;
                            var rigthCode = _frmRadiumCarving.RigthSN;
                            _frmRadiumCarving.SendMsg(leftCode, rigthCode);
                            _plchelper.Write(_radiumCarvingAddress, PLCResult.nodata);
                            LogManager.Logs.Warn("跳过镭雕功能！");
                            continue;
                        }
                        _plchelper.Write(_radiumCarvingAddress, PLCResult.data);
                        Task.Delay(400).Wait();
                        string leftSN = string.Empty;
                        string rightSN = string.Empty;
                        ///todo 扫码
                        var result = _radiumCarvingCamera.Run(DataContent.SystemConfig.LeftCamera.ExposureTime, DataContent.SystemConfig.LeftCamera.Gain);
                        leftSN = result.Item1;
                        rightSN = result.Item2;
                        ///数据验证
                        if (!CodeParse(leftSN))
                        {
                            LogManager.RadiumCarvingLogs.Error($"左产品码镭雕数据验证失败{leftSN}");
                            leftSN = "NG";
                            _plchelper.Write(_leftRadiumCarvingAddress, PLCResult.NG);
                        }
                        else if (DataContent.CacheData.ContainsBurnCode(leftSN))
                        {
                            LogManager.RadiumCarvingLogs.Error($"左产品{leftSN}烧录站ng,不进行镭雕");
                            leftSN = "NG";
                            _plchelper.Write(_leftRadiumCarvingAddress, PLCResult.NG);
                        }
                        else
                        {
                            _plchelper.Write(_leftRadiumCarvingAddress, PLCResult.OK);
                        }
                        if (!CodeParse(rightSN))
                        {
                            LogManager.RadiumCarvingLogs.Error($"右产品镭雕码数据验证失败{leftSN}");
                            rightSN = "NG";
                            _plchelper.Write(_rightRadiumCarvingAddress, PLCResult.NG);
                        }
                        else if (DataContent.CacheData.ContainsBurnCode(rightSN))
                        {
                            LogManager.RadiumCarvingLogs.Error($"右产品{rightSN}烧录站ng,不进行镭雕");
                            rightSN = "NG";
                            _plchelper.Write(_rightRadiumCarvingAddress, PLCResult.NG);
                        }
                        else
                        {
                            _plchelper.Write(_rightRadiumCarvingAddress, PLCResult.OK);
                        }

                        ///镭雕
                        var radiumCarvingResult = _frmRadiumCarving.SendMsg(leftSN, rightSN);

                        _plchelper.Write(_leftRadiumCarvingResultAddress, radiumCarvingResult.Item1 ? PLCResult.OK : PLCResult.NG);
                        _plchelper.Write(_rightRadiumCarvingAddress, radiumCarvingResult.Item2 ? PLCResult.OK : PLCResult.NG);
                    }
                }, TaskCreationOptions.LongRunning);
            }
           
            LogManager.Init(lvLogs);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DataContent.User))
            {
                FrmVisionSetting frmVisionSetting=new FrmVisionSetting(
                    tool => _radiumCarvingCamera.LoadVision(DataContent.SystemConfig.LeftVppPath, DataContent.SystemConfig.LeftCamera.Name), 
                    tool => _burnCamera.LoadVision(DataContent.SystemConfig.RigthVppPath, DataContent.SystemConfig.RigthCamera.Name));
                frmVisionSetting.ShowDialog();
            }
            else
            {
                MessageBox.Show("请先登录");
            }
        }
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            _burnCamera.Close();
            _radiumCarvingCamera.Close();
            _plchelper?.Close();
            _frmBurn.Close();
            _frmRadiumCarving.Close();
            LigthControl.Instance(DataContent.SystemConfig.PortName, DataContent.SystemConfig.BaudRate).Close();
        }
    }
}
