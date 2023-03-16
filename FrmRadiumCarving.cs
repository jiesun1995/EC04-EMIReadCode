using EC04_EMIReadCode.Comm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EC04_EMIReadCode
{
    /// <summary>
    /// 镭雕工站
    /// </summary>
    public partial class FrmRadiumCarving : Form
    {
        //private readonly SocketClient _socketClient;
        private readonly SocketServer _socketServer;
        private readonly Stopwatch _stopwatch;
        private string _code=string.Empty;
        private List<Socket> _socketClients;
        private byte[] buffer = new byte[1024 * 1024 * 2];

        public FrmRadiumCarving(string ip,int port,string title="烧录")
        {
            _socketClients = new List<Socket>();
            InitializeComponent();
            _socketServer = new SocketServer(ip, port, client =>
            {
                _socketClients.Add(client);
                new TaskFactory().StartNew(obj =>
                {
                    Socket clientSocket = (Socket)obj;
                    while (clientSocket.Connected)
                    {
                        Thread.Sleep(10);
                        try
                        {
                            //获取从客户端发来的数据
                            int length = clientSocket.Receive(buffer);
                            var data = Encoding.UTF8.GetString(buffer, 0, length);
                            if (string.IsNullOrEmpty(data))
                            {
                                clientSocket.Shutdown(SocketShutdown.Both);
                                clientSocket.Close();
                                //clients.Remove(clientSocket);
                                break;
                            }
                            LogManager.RadiumCarvingLogs.Debug($"接收客户端{ clientSocket.RemoteEndPoint.ToString()},消息{data}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            clientSocket.Shutdown(SocketShutdown.Both);
                            clientSocket.Close();
                            break;
                        }
                    }
                },client,TaskCreationOptions.LongRunning);
            });
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
        private bool CodeParse(string sn)
        {
            if (!string.IsNullOrWhiteSpace(sn) && sn != "NG")
            {
                if (sn.Length != DataContent.SystemConfig.CodeLength)
                {
                    LogManager.RadiumCarvingLogs.Warn("长度不符！");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                LogManager.RadiumCarvingLogs.Warn($"SN:{sn}为空或者ng！");
                return false;
            }
        }
        private string QueryCode(string leftSN, string rightSN)
        {
            try
            {
                var leftcode = CodeParse(leftSN) ? leftSN : BuildEmpty(DataContent.SystemConfig.CodeLength);
                var rightcode = CodeParse(rightSN) ? rightSN : BuildEmpty(DataContent.SystemConfig.CodeLength);
                if (leftcode.Length != DataContent.SystemConfig.CodeLength)
                {
                    LogManager.RadiumCarvingLogs.Warn("左边镭雕长度不符！");
                    return string.Empty;
                }
                if (rightcode.Length != DataContent.SystemConfig.CodeLength)
                {
                    LogManager.RadiumCarvingLogs.Warn("右边镭雕长度不符！");
                }
                return $"{leftcode}{rightcode}";
            }
            catch (Exception ex)
            {
                LogManager.RadiumCarvingLogs.Error(ex);
            }
            return string.Empty;
        }
        private void SendClientMsg(string msg)
        {
            
            for (int i = _socketClients.Count - 1; i <= 0; i++)
            {
                var client = _socketClients[i];
                if (client.Connected)
                {
                    try
                    {
                        LogManager.RadiumCarvingLogs.Debug($"向客户端{client.RemoteEndPoint.ToString()}，发送消息{msg}");
                        client.Send(Encoding.UTF8.GetBytes(msg));
                    }
                    catch (Exception ex)
                    {
                        LogManager.RadiumCarvingLogs.Error(ex);
                    }
                }
                else
                {
                    _socketClients.Remove(client);
                }
            }
        }
        private bool Receive(string code)
        {
            var data = false;
            try
            {
                if (string.IsNullOrWhiteSpace(code))
                {
                    LogManager.RadiumCarvingLogs.Warn($"镭雕数据为空：{code}");
                    return false;
                }
                LogManager.RadiumCarvingLogs.Info($"镭雕数据{code}");
                SendClientMsg(code);
                data = true;
            }
            catch (Exception ex)
            {
                LogManager.RadiumCarvingLogs.Error(ex);
            }
            var result = data;
            return result;
        }
        private void ShowUI(bool leftResult,bool rigthResult,string leftSN,string rigthSN)
        {
            SystemHelper.UIShow(btnRigth, () =>
            {
                btnLeft.BackColor = leftResult ? Color.YellowGreen : Color.Red;
                btnRigth.BackColor = rigthResult ? Color.YellowGreen : Color.Red;
                tbxLeftSN.Text = leftSN;
                tbxRigthSN.Text = rigthSN;
                lblTime.Text = $"烧录耗时:{_stopwatch.Elapsed.TotalMilliseconds}ms";
            });
        }
        public bool SendMsg(string leftSN,string rightSN)
        {
            _stopwatch.Restart();
            var code = QueryCode(leftSN,rightSN);
            var result = Receive(code);
            _stopwatch.Stop();
            ShowUI(result,result,leftSN,rightSN);
            return result;
        }

        private void BurnForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _socketServer.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblState.BackColor = _socketServer != null && _socketClients.Count>0 ? Color.GreenYellow: Color.Red;
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
            string rightSN = tbxRigthSN.Text;
            _stopwatch.Restart();
            var code = QueryCode(leftSN,rightSN);
            var result = Receive(code);
            _stopwatch.Stop();
            ShowUI(result,result,leftSN,rightSN);
        }

        private void cbxDoWork_CheckedChanged(object sender, EventArgs e)
        {
            DataContent.RadiumCarving = cbxDoWork.Checked;
        }
    }
}
