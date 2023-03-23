using P117_EMIReadCode.Comm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P117_EMIReadCode
{
    /// <summary>
    /// 烧录工站
    /// </summary>
    public partial class BurnForm : Form
    {
        private readonly SocketServer _socketServer;
        private readonly Stopwatch _stopwatch;
        private Dictionary<string,Socket> _socketClients;
        private byte[] buffer = new byte[1024 * 1024 * 2];

        public BurnForm(string ip,int port,string title="烧录")
        {
            InitializeComponent();
            _socketClients = new Dictionary<string, Socket>();
            _socketServer = new SocketServer(ip, port,client=>
            {
                client.ReceiveTimeout = DataContent.SystemConfig.SocketTimeout;
                var length = client.Receive(buffer);
                //var data = Encoding.UTF8.GetString(buffer, 0, length);
                var data = ((IPEndPoint)client.RemoteEndPoint).Address.ToString();
                if (_socketClients.ContainsKey(data))
                    _socketClients.Remove(data);
                _socketClients.Add(data,client);
            });
            _socketServer.StartListen();
            _stopwatch = new Stopwatch();
            gbxTitle.Text = title;
            timer1.Start();
        }
        private bool ValidateCode(string sn)
        {
            if (!string.IsNullOrWhiteSpace(sn) && sn != "NG")
            {
                if (sn.Length != DataContent.SystemConfig.CodeLength)
                {
                    LogManager.BurnLogs.Warn("长度不符！");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                LogManager.BurnLogs.Warn($"SN:{sn}为空或者ng！");
                return false;
            }
        }
        private bool Send(string clientName, string code)
        {
            var result = false;

            if (string.IsNullOrWhiteSpace(code))
            {
                return false;
            }
            LogManager.BurnLogs.Info($"烧录数据{code}");
            if (!_socketClients.ContainsKey(clientName))
            {
                LogManager.BurnLogs.Error($"不存在{clientName}客户端！");
                return false;
            }
            var client = _socketClients[clientName];
            if (client.Connected)
            {
                try
                {
                    LogManager.BurnLogs.Debug($"向客户端{client.RemoteEndPoint.ToString()}，发送消息{code}");
                    client.Send(Encoding.UTF8.GetBytes(code));
                    var length = client.Receive(buffer);
                    var data = Encoding.UTF8.GetString(buffer, 0, length);
                    LogManager.BurnLogs.Debug($"接收客户端{ client.RemoteEndPoint.ToString()},消息{data}");
                    if (string.IsNullOrEmpty(data))
                    {
                        client.Shutdown(SocketShutdown.Both);
                        client.Close();
                    }
                    result = data.ToUpper()=="OK";
                }
                catch(SocketException ex)
                {
                    if (ex.ErrorCode == 10060)///超时
                    {

                    }
                    else if(ex.ErrorCode == 10053)///连接断开
                    {
                        _socketClients.Remove(clientName);
                    }
                    LogManager.BurnLogs.Error(ex);
                }
                catch (Exception ex)
                {
                    LogManager.BurnLogs.Error(ex);
                }
            }
            else
            {
                _socketClients.Remove(clientName);
            }
            return result;
        }
        private void ShowUI(bool leftResult,bool rigthResult,string leftSN,string rigthSN)
        {
            SystemHelper.UIShow(btnRigthBurn, () =>
            {
                btnRigthBurn.BackColor= rigthResult ? Color.YellowGreen : Color.Red;
                btnLeftBurn.BackColor= leftResult ? Color.YellowGreen : Color.Red;
                tbxLeftSN.Text = leftSN;
                tbxRightSN.Text = rigthSN;
                lblTime.Text = $"烧录耗时:{_stopwatch.Elapsed.TotalMilliseconds}ms";
            });
        }
        public Tuple<bool,bool> SendMsg(string leftSN,string rigthSN)
        {
            _stopwatch.Restart();
            var leftTask = new TaskFactory().StartNew<bool>(obj =>
            {
                var code = obj.ToString();
                if (ValidateCode(code))
                {
                    var result = Send(DataContent.SystemConfig.LeftClientName, code);
                    return result;
                }
                return false;
            },leftSN);
            var rigthTask = new TaskFactory().StartNew<bool>(obj =>
            {
                var code = obj.ToString();
                if (ValidateCode(code))
                {
                    var result = Send(DataContent.SystemConfig.RigthClientName, code);
                    return result;
                }
                return false;
            }, rigthSN);
            Task.WaitAll(leftTask, rigthTask);
            _stopwatch.Stop();
            var burnResult = leftTask.Result && rigthTask.Result ? true : false;
            ShowUI(leftTask.Result,rigthTask.Result,leftSN,rigthSN);
            return new Tuple<bool, bool>(leftTask.Result,rigthTask.Result);
        }

        private void BurnForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _socketServer.Close();
            if (_socketClients .Count > 0)
            {
                foreach (var client in _socketClients)
                {
                    client.Value.Shutdown(SocketShutdown.Both);
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblRigthState.BackColor = _socketServer != null && _socketClients.ContainsKey(DataContent.SystemConfig.RigthClientName) ? Color.GreenYellow: Color.Red;
            lblLeftState.BackColor= _socketServer != null && _socketClients.ContainsKey(DataContent.SystemConfig.LeftClientName) ? Color.GreenYellow: Color.Red;
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
            string leftSN = tbxLeftSN.Text;
            string rigthSN = tbxRightSN.Text;
            SendMsg(leftSN, rigthSN);
        }

        private void cbxDoWork_CheckedChanged(object sender, EventArgs e)
        {
            DataContent.Burn = cbxDoWork.Checked;
        }

        private void btnLeftBurn_Click(object sender, EventArgs e)
        {
            var leftTask = new TaskFactory().StartNew<bool>(obj =>
            {
                var code = obj.ToString();
                if (ValidateCode(code))
                {
                    var result = Send(DataContent.SystemConfig.LeftClientName, code);
                    return result;
                }
                return false;
            }, tbxLeftSN.Text);
            btnLeftBurn.BackColor = leftTask.Result ? Color.YellowGreen : Color.Red;
        }

        private void btnRigthBurn_Click(object sender, EventArgs e)
        {
            var rigthTask = new TaskFactory().StartNew<bool>(obj =>
            {
                var code = obj.ToString();
                if (ValidateCode(code))
                {
                    var result = Send(DataContent.SystemConfig.RigthClientName, code);
                    return result;
                }
                return false;
            }, tbxRightSN.Text);
            btnRigthBurn.BackColor = rigthTask.Result ? Color.YellowGreen : Color.Red;
        }
    }
}
