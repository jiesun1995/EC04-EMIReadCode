using EC04_EMIReadCode.Comm;
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
        public FrmMain()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            _plchelper = new PLCHelper(DataContent.SystemConfig.PLCConfig.IP, DataContent.SystemConfig.PLCConfig.Port);
            InitializeComponent();
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            tslSystemDate.Text = $"系统时间:{DateTime.Now.ToString()}  |";
            tslRunDate.Text = $"运行时间:{_stopwatch.Elapsed}  |";
            tslPLC.Text = $"PLC状态:{(_plchelper.IsConnect?"已连接":"未连接")}   |";
            tslPLC.BackColor = _plchelper.IsConnect ? Color.Green : Color.Red;
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
            LogManager.Init(lvLogs);
            tabPage1.Controls.Clear();
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.Dock = DockStyle.Fill;
            try
            {
                tableLayoutPanel.RowStyles.Add(new ColumnStyle(SizeType.Percent, 35));
                tableLayoutPanel.RowStyles.Add(new ColumnStyle(SizeType.Percent, 65));
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / tableLayoutPanel.ColumnCount));
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / tableLayoutPanel.ColumnCount));
                _frmBurn = new BurnForm(DataContent.SystemConfig.LeftBurn.IP, DataContent.SystemConfig.LeftBurn.Port, "烧录工站");
                tableLayoutPanel = LoadFrm(tableLayoutPanel, _frmBurn, 0, 0);
                _frmRadiumCarving = new FrmRadiumCarving(DataContent.SystemConfig.RadiumCarving.IP, DataContent.SystemConfig.RadiumCarving.Port, "左镭雕工站");
                tableLayoutPanel = LoadFrm(tableLayoutPanel, _frmRadiumCarving, 1, 0);

                _burnCamera = new FrmVisionDisplay(DataContent.SystemConfig.LeftVppPath, DataContent.SystemConfig.LeftCamera.Name,"烧录相机");
                tableLayoutPanel = LoadFrm(tableLayoutPanel, _burnCamera, 0, 1);
                _radiumCarvingCamera = new FrmVisionDisplay(DataContent.SystemConfig.RightVppPath, DataContent.SystemConfig.RightCamera.Name,"打标相机");
                tableLayoutPanel = LoadFrm(tableLayoutPanel, _radiumCarvingCamera, 1, 1);
            }
            catch (Exception ex)
            {
                LogManager.Logs.Error(ex);
            }

            tabPage1.Controls.Add(tableLayoutPanel);

            new TaskFactory().StartNew(() =>
            {
                while (true)
                {
                    if (_plchelper.Read(_leftBurnAddress) != 1)
                        continue;
                    if (DataContent.RadiumCarving)
                    {
                        _plchelper.Write(_leftBurnAddress, PLCResult.ReadCodeOK);
                        _plchelper.Write(_rightBurnAddress, PLCResult.ReadCodeOK);
                        _plchelper.Write(_leftBurnResultAddress, PLCResult.ReadCodeOK);
                        _plchelper.Write(_rightBurnResultAddress, PLCResult.ReadCodeOK);
                        LogManager.Logs.Info("跳过烧录功能！");
                        continue;
                    }
                    LogManager.Logs.Info("读取到烧录机启动信号");
                    string leftSN = string.Empty;
                    string rightSN = string.Empty;
                    ///todo 扫码
                    var result = _burnCamera.Run(DataContent.SystemConfig.LeftCamera.ExposureTime, DataContent.SystemConfig.LeftCamera.Gain);
                    leftSN = result.Item1;
                    rightSN = result.Item2;
                    ///烧录
                    if (CodeParse(leftSN) && DataContent.SystemConfig.ContainsBurnCode(leftSN))
                    {
                        _plchelper.Write(_leftBurnAddress, PLCResult.ReadCodeOK);
                    }
                    else
                        _plchelper.Write(_leftBurnAddress, PLCResult.ReadCodeNG);

                    if (CodeParse(rightSN) && DataContent.SystemConfig.ContainsBurnCode(rightSN))
                    {
                        _plchelper.Write(_rightBurnAddress, PLCResult.ReadCodeOK);
                    }
                    else
                        _plchelper.Write(_rightBurnAddress, PLCResult.ReadCodeNG);
                    ///烧录
                    var burnResult = _frmBurn.SendMsg(leftSN, rightSN);
                    if (burnResult)
                    {
                        _plchelper.Write(_leftBurnResultAddress, CodeParse(leftSN) ? PLCResult.ResultOK : PLCResult.ResultNG);
                        _plchelper.Write(_rightBurnResultAddress, CodeParse(rightSN) ? PLCResult.ResultOK : PLCResult.ResultNG);
                    }
                    else
                    {
                        _plchelper.Write(_leftBurnResultAddress, PLCResult.ResultNG);
                        _plchelper.Write(_rightBurnResultAddress, PLCResult.ResultNG);
                       
                    }
                    DataContent.SystemConfig.AddBurnCode(burnResult ? "" : leftSN);
                    DataContent.SystemConfig.AddBurnCode(burnResult ? "" : rightSN);
                }
            }, TaskCreationOptions.LongRunning);
            new TaskFactory().StartNew(() =>
            {
                while (true)
                {
                    if (_plchelper.Read(_leftRadiumCarvingAddress) != 1)
                        continue;
                    LogManager.Logs.Info("读取到镭雕机启动信号");
                    if (DataContent.RadiumCarving)
                    {
                        _plchelper.Write(_leftRadiumCarvingAddress, PLCResult.ReadCodeOK);
                        _plchelper.Write(_rightRadiumCarvingAddress, PLCResult.ReadCodeOK);
                        _plchelper.Write(_leftRadiumCarvingResultAddress, PLCResult.ReadCodeOK);
                        _plchelper.Write(_rightRadiumCarvingResultAddress, PLCResult.ReadCodeOK);
                        LogManager.Logs.Info("跳过镭雕功能！");
                        continue;
                    }
                    
                    string leftSN = string.Empty;
                    string rightSN = string.Empty;
                    ///todo 扫码
                    var result = _radiumCarvingCamera.Run(DataContent.SystemConfig.LeftCamera.ExposureTime, DataContent.SystemConfig.LeftCamera.Gain);
                    leftSN = result.Item1;
                    rightSN = result.Item2;
                    ///镭雕
                    if (CodeParse(leftSN) && DataContent.SystemConfig.ContainsBurnCode(leftSN))
                    {
                        _plchelper.Write(_leftRadiumCarvingAddress, PLCResult.ReadCodeOK);
                    }else
                        _plchelper.Write(_leftRadiumCarvingAddress, PLCResult.ReadCodeNG);

                    if(CodeParse(rightSN) && DataContent.SystemConfig.ContainsBurnCode(rightSN))
                    {
                        _plchelper.Write(_rightRadiumCarvingAddress, PLCResult.ReadCodeOK);
                    }
                    else
                        _plchelper.Write(_rightRadiumCarvingAddress, PLCResult.ReadCodeNG);
                    ///镭雕
                    var radiumCarvingResult = _frmRadiumCarving.SendMsg(leftSN,rightSN);
                    if (radiumCarvingResult)
                    {
                        _plchelper.Write(_leftRadiumCarvingResultAddress, CodeParse(leftSN) ? PLCResult.ResultOK : PLCResult.ResultNG);
                        _plchelper.Write(_rightRadiumCarvingAddress,CodeParse(rightSN)?PLCResult.ResultOK: PLCResult.ResultNG);
                    }
                    else
                    {
                        _plchelper.Write(_leftRadiumCarvingResultAddress, PLCResult.ResultNG);
                        _plchelper.Write(_rightRadiumCarvingResultAddress, PLCResult.ResultNG);
                    }
                }
            },TaskCreationOptions.LongRunning);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DataContent.User))
            {
                FrmVisionSetting frmVisionSetting=new FrmVisionSetting(
                    tool => _burnCamera.LoadVision(DataContent.SystemConfig.LeftVppPath, DataContent.SystemConfig.LeftCamera.Name), 
                    tool => _burnCamera.LoadVision(DataContent.SystemConfig.RightVppPath, DataContent.SystemConfig.RightCamera.Name));
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
            _plchelper.Close();
            _frmBurn.Close();
            _frmRadiumCarving.Close();
        }
    }
}
