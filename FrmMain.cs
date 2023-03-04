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
        private BurnForm _leftBurn;
        private BurnForm _rightBurn;
        private RadiumCarvingForm _leftRadiumCarving;
        private RadiumCarvingForm _rightRadiumCarving;
        private readonly PLCHelper _plchelper;
        private FrmVisionDisplay _radiumCarvingCamera;
        private FrmVisionDisplay _burnCamera;
        private readonly Stopwatch _stopwatch;

        private const string _leftReadCodeAddress = "D11";
        private const string _rightReadCodeAddress = "D12";
        private const string _leftBurnAddress = "D13";
        private const string _rightBurnAddress = "D14";
        private const string _leftRadiumCarvingAddress = "D21";
        private const string _rightRadiumCarvingAddress = "D22";
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
            tslPLC.BackColor = _plchelper.IsConnect ? Color.Gray : Color.Red;
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
            //if (row == 0)
            //{
            //    tableLayout.RowStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            //}
            //else
            //{
            //    tableLayout.RowStyles.Add(new ColumnStyle(SizeType.Percent, 70));
            //}
            //tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / tableLayout.ColumnCount));
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Show();
            tableLayout.Controls.Add(form, col, row);
            return tableLayout;
        }
        private bool CodeParse(string code)
        {
            if (!string.IsNullOrEmpty(code) || code != "NG")
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
            tableLayoutPanel.ColumnCount = 4;
            tableLayoutPanel.Dock = DockStyle.Fill;
            try
            {
                tableLayoutPanel.RowStyles.Add(new ColumnStyle(SizeType.Percent, 35));
                tableLayoutPanel.RowStyles.Add(new ColumnStyle(SizeType.Percent, 65));
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / tableLayoutPanel.ColumnCount));
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / tableLayoutPanel.ColumnCount));
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / tableLayoutPanel.ColumnCount));
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / tableLayoutPanel.ColumnCount));
                _leftBurn = new BurnForm(DataContent.SystemConfig.LeftBurn.IP, DataContent.SystemConfig.LeftBurn.Port, "左烧录工站");
                tableLayoutPanel = LoadFrm(tableLayoutPanel, _leftBurn, 0, 0);
                _rightBurn = new BurnForm(DataContent.SystemConfig.RightBurn.IP, DataContent.SystemConfig.RightBurn.Port, "右烧录工站");
                tableLayoutPanel = LoadFrm(tableLayoutPanel, _rightBurn, 1, 0);
                _leftRadiumCarving = new RadiumCarvingForm(DataContent.SystemConfig.RightRadiumCarving.IP, DataContent.SystemConfig.RightRadiumCarving.Port, "左镭雕工站");
                tableLayoutPanel = LoadFrm(tableLayoutPanel, _leftRadiumCarving, 2, 0);
                _rightRadiumCarving = new RadiumCarvingForm(DataContent.SystemConfig.RightRadiumCarving.IP, DataContent.SystemConfig.RightRadiumCarving.Port, "右镭雕工站");
                tableLayoutPanel = LoadFrm(tableLayoutPanel, _rightRadiumCarving, 3, 0);

                _burnCamera = new FrmVisionDisplay(DataContent.SystemConfig.LeftVppPath,DataContent.SystemConfig.LeftCamera.Name);
                tableLayoutPanel = LoadFrm(tableLayoutPanel, _burnCamera, 0, 1);
                tableLayoutPanel.SetColumnSpan(_burnCamera, 2);
                _radiumCarvingCamera = new FrmVisionDisplay(DataContent.SystemConfig.RightVppPath, DataContent.SystemConfig.RightCamera.Name);
                tableLayoutPanel = LoadFrm(tableLayoutPanel, _radiumCarvingCamera, 2, 1);
                tableLayoutPanel.SetColumnSpan(_radiumCarvingCamera, 2);
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
                    if (_plchelper.Read(_leftReadCodeAddress) == 1)
                        continue;
                    string leftSN = string.Empty;
                    string rightSN = string.Empty;
                    ///todo 扫码
                    var result = _burnCamera.Run(DataContent.SystemConfig.LeftCamera.ExposureTime, DataContent.SystemConfig.LeftCamera.Gain);
                    leftSN = result.Item1;
                    rightSN = result.Item2;

                    var leftTask = new TaskFactory().StartNew<bool>(obj =>
                    {
                        var code = obj.ToString();
                        if (!CodeParse(code))
                        {
                            _plchelper.Write(_leftReadCodeAddress, PLCResult.ReadCodeNG);
                            return false;
                        }
                        else
                            _plchelper.Write(_leftReadCodeAddress, PLCResult.ReadCodeOK);
                        if (_leftBurn.SendMsg(code))
                        {
                            _plchelper.Write(_leftBurnAddress, PLCResult.ReadCodeOK);
                        }
                        else
                        {
                            _plchelper.Write(_leftBurnAddress, PLCResult.ReadCodeNG);
                            return false;
                        }
                        return true;
                    }, leftSN);
                    var rightTask = new TaskFactory().StartNew<bool>(obj =>
                    {
                        var code = obj.ToString();
                        if (!CodeParse(code))
                        {
                            _plchelper.Write(_rightReadCodeAddress, PLCResult.ReadCodeNG);
                            return false;
                        }
                        else
                            _plchelper.Write(_rightReadCodeAddress, PLCResult.ReadCodeOK);
                        if (_leftBurn.SendMsg(code))
                        {
                            _plchelper.Write(_rightBurnAddress, PLCResult.ReadCodeOK);
                        }
                        else
                        {
                            _plchelper.Write(_rightBurnAddress, PLCResult.ReadCodeNG);
                            return false;
                        }
                        return true;
                    }, rightSN);
                    Task.WaitAll(leftTask, rightTask);
                    DataContent.SystemConfig.AddBurnCode(leftTask.Result ? "" : leftSN);
                    DataContent.SystemConfig.AddBurnCode(rightTask.Result ? "" : rightSN);
                }
            },TaskCreationOptions.LongRunning);
            new TaskFactory().StartNew(() =>
            {
                while (true)
                {
                    if (_plchelper.Read(_leftRadiumCarvingAddress) != 1)
                        continue;
                    string leftSN = string.Empty;
                    string rightSN = string.Empty;
                    ///todo 扫码
                    var result = _burnCamera.Run(DataContent.SystemConfig.LeftCamera.ExposureTime, DataContent.SystemConfig.LeftCamera.Gain);
                    leftSN = result.Item1;
                    rightSN = result.Item2;
                    ///烧录 
                    var leftTask = new TaskFactory().StartNew(obj =>
                    {
                        var code = obj.ToString();
                        if (CodeParse(code) && DataContent.SystemConfig.ContainsBurnCode(code))
                        {
                            _plchelper.Write(_leftRadiumCarvingAddress, PLCResult.ReadCodeOK);
                            _leftRadiumCarving.SendMsg(code);
                        }
                        else
                        {
                            _plchelper.Write(_leftRadiumCarvingAddress, PLCResult.ReadCodeNG);
                        }
                    }, leftSN);
                    var rightTask = new TaskFactory().StartNew(obj =>
                    {
                        var code = obj.ToString();
                        if (CodeParse(code) && DataContent.SystemConfig.ContainsBurnCode(code))
                        {
                            _plchelper.Write(_rightRadiumCarvingAddress, PLCResult.ReadCodeOK);
                            _leftRadiumCarving.SendMsg(code);
                        }
                        else
                        {
                            _plchelper.Write(_rightRadiumCarvingAddress, PLCResult.ReadCodeNG);
                        }
                    }, rightSN);
                    Task.WaitAll(leftTask, rightTask);
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
            _plchelper.Close();
        }
    }
}
