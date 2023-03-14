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
    /// <summary>
    /// 烧录工站
    /// </summary>
    public partial class BurnForm : Form
    {
        //private readonly SocketClient _socketClient;
        private readonly SocketServer _socketServer;
        private readonly Stopwatch _stopwatch;
        private string _code=string.Empty;

        public BurnForm(string ip,int port,string title="烧录")
        {
            InitializeComponent();
            _socketServer = new SocketServer(ip, port);
            _socketServer.StartListen();
            _stopwatch = new Stopwatch();
            gbxTitle.Text = title;
            timer1.Start();
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
        private bool CodeParse(string code)
        {
            if (!string.IsNullOrWhiteSpace(code) && code != "NG")
                return true;
            else
                return false;
        }
        private string QueryCode(string leftSN, string rightSN)
        {

            try
            {
                var leftcode = CodeParse(leftSN) ? leftSN : BuildEmpty(DataContent.SystemConfig.CodeLength);
                var rightcode = CodeParse(rightSN) ? rightSN : BuildEmpty(DataContent.SystemConfig.CodeLength);
                if (leftcode.Length != DataContent.SystemConfig.CodeLength)
                {
                    LogManager.BurnLogs.Error("左边烧录长度不符！");
                    return string.Empty;
                }
                if (rightcode.Length != DataContent.SystemConfig.CodeLength)
                {
                    LogManager.BurnLogs.Error("右边烧录长度不符！");
                }
                return $"{leftcode}{rightcode}";
            }
            catch (Exception ex)
            {
                LogManager.BurnLogs.Error(ex);
            }
            return string.Empty;
        }
        private bool Receive(string code)
        {
            var data = false;
            try
            {
                if (string.IsNullOrWhiteSpace(code))
                {
                    SystemHelper.UIShow(btnBurn, () => { btnBurn.BackColor = Color.Red; });
                    return false;
                }
                LogManager.BurnLogs.Info($"烧录数据{code}");
                _socketServer.SendMessage(code);
                data = true;
                SystemHelper.UIShow(btnBurn, () => { btnBurn.BackColor = Color.Green; });
            }
            catch (Exception ex)
            {
                LogManager.BurnLogs.Error(ex);
                SystemHelper.UIShow(btnBurn, () => { btnBurn.BackColor = Color.Red; });
            }
            var result = data;
            return result;
        }
        private void ShowUI(bool result)
        {
            SystemHelper.UIShow(btnBurn, () =>
            {
                if (result)
                {
                    lblResult.Text = $"烧录结果:{result}";
                    lblResult.BackColor = Color.Gray;
                }
                else
                {
                    lblResult.Text = $"烧录结果:{result}";
                    lblResult.BackColor = Color.Red;
                }

                lblTime.Text = $"烧录耗时:{_stopwatch.Elapsed.TotalMilliseconds}ms";
            });
        }
        public bool SendMsg(string leftSN,string rightSN)
        {
            _stopwatch.Restart();
            var code = QueryCode(leftSN,rightSN);
            var result = Receive(code);
            _stopwatch.Stop();
            ShowUI(result);
            return result;
        }

        private void BurnForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _socketServer.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblState.BackColor = _socketServer != null && _socketServer.ClientCount>0 ? Color.Green : Color.Red;
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DataContent.User))
            {
                panel1.Enabled = btnLock.Text == "解锁" ? true : false;
                groupBox1.Enabled = btnLock.Text == "解锁" ? true : false;
                btnLock.Text = btnLock.Text == "解锁" ? "锁定" : "解锁";
            }
            else
            {
                MessageBox.Show("请先登录！");
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string leftSN = string.Empty;
            string rightSN = string.Empty;
            var sns = tbxSN.Text.Split('\n');
            leftSN = sns.Length > 0 ? sns[0].Replace("\r", "") : "NG";
            rightSN = sns.Length > 1 ? sns[1].Replace("\r", "") : "NG";
            _stopwatch.Restart();
            var code = QueryCode(leftSN,rightSN);
            var result = Receive(code);
            _stopwatch.Stop();
            ShowUI(result);
        }

        private void btnBurn_Click(object sender, EventArgs e)
        {
            string leftSN = string.Empty;
            string rightSN = string.Empty;
            var sns = tbxSN.Text.Split('\n');
            leftSN = sns.Length > 0 ? sns[0].Replace("\r", "") : "NG";
            rightSN = sns.Length > 1 ? sns[1].Replace("\r", "") : "NG";
            _stopwatch.Restart();
            var code = QueryCode(leftSN, rightSN);
            var result = Receive(code);
            _stopwatch.Stop();
            ShowUI(result);
        }

        private void cbxDoWork_CheckedChanged(object sender, EventArgs e)
        {
            DataContent.Burn = cbxDoWork.Checked;
        }
    }
}
