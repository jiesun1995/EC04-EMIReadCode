using P117_EMIReadCode.Comm;
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

namespace P117_EMIReadCode
{
    /// <summary>
    /// 镭雕工站
    /// </summary>
    public partial class FrmRadiumCarving : Form
    {
        //private readonly SocketClient _socketClient;
        private readonly SocketServer _socketServer;
        private readonly Stopwatch _stopwatch;
        private string _code = string.Empty;
        private List<Socket> _socketClients;
        private byte[] buffer = new byte[1024 * 1024 * 2];
        private readonly MesService _mesService;

        public FrmRadiumCarving(string ip, int port, MesService mesService, string title = "烧录")
        {
            _mesService = mesService;
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
                }, client, TaskCreationOptions.LongRunning);
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
        private string CodeParse(string sn)
        {
            if (!string.IsNullOrWhiteSpace(sn) && sn != "NG")
            {
                if (DataContent.RadiumCarving)
                {
                    if (DataContent.RadiumCarvingSN)
                        return sn;
                }
                else
                {
                    if (sn.Length != DataContent.SystemConfig.CodeLength)
                    {
                        LogManager.RadiumCarvingLogs.Warn("长度不符！");
                    }
                    else
                    {
                        if (_mesService.QueryStation(sn))
                        {
                            if (_mesService.PassStation(sn))
                                return sn;
                        }
                    }
                }
            }
            else
            {
                LogManager.RadiumCarvingLogs.Warn($"SN:{sn}为空或者ng！");
            }
            return BuildEmpty(DataContent.SystemConfig.CodeLength);
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
        private void ShowUI(bool leftResult, bool rigthResult, bool leftMesResult, bool rigthMesResult, string leftSN, string rigthSN)
        {
            SystemHelper.UIShow(btnRigth, () =>
            {
                btnLeft.BackColor = leftResult ? Color.YellowGreen : Color.Red;
                btnRigth.BackColor = rigthResult ? Color.YellowGreen : Color.Red;
                btnLeftMes.BackColor = leftMesResult ? Color.YellowGreen : Color.Red;
                btnRigthMes.BackColor = rigthMesResult ? Color.YellowGreen : Color.Red;
                tbxLeftSN.Text = leftSN;
                tbxRigthSN.Text = rigthSN;
                lblTime.Text = $"烧录耗时:{_stopwatch.Elapsed.TotalMilliseconds}ms";
            });
        }
        public Tuple<bool, bool> SendMsg(string leftSN, string rigthSN)
        {
            _stopwatch.Restart();
            var leftCode = CodeParse(leftSN);
            var rigthCode = CodeParse(rigthSN);
            var leftMesResult = leftCode == leftSN;
            var rigthMesResult = rigthCode == rigthSN;
            var result = Receive($"{leftCode}{rigthCode}");
            _stopwatch.Stop();
            ShowUI(result, result, leftMesResult, rigthMesResult, leftSN, rigthSN);
            return new Tuple<bool, bool>(leftMesResult, rigthMesResult);
        }

        public string LeftSN { get { return SystemHelper.GetUIVal(tbxLeftSN, () => { return tbxLeftSN.Text; }); } }
        public string RigthSN { get { return SystemHelper.GetUIVal(tbxRigthSN, () => { return tbxRigthSN.Text; }); } }

        private void BurnForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _socketServer.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblState.BackColor = _socketServer != null && _socketClients.Count > 0 ? Color.GreenYellow : Color.Red;
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
            SendMsg(leftSN, rightSN);
        }

        private void cbxDoWork_CheckedChanged(object sender, EventArgs e)
        {
            DataContent.RadiumCarving = cbxDoWork.Checked;
        }

        private void btnLeftMes_Click(object sender, EventArgs e)
        {
            //_mesService.GetCurrStation(tbxLeftSN.Text);
            //_mesService.QueryStation(tbxLeftSN.Text);
            if (_mesService.QueryStation(tbxLeftSN.Text))
            {
                _mesService.PassStation(tbxLeftSN.Text);
            }
        }

        private void btnRigthMes_Click(object sender, EventArgs e)
        {
            if (_mesService.QueryStation(tbxRigthSN.Text))
            {
                _mesService.PassStation(tbxRigthSN.Text);
            }
        }

        private void cbxSN_CheckedChanged(object sender, EventArgs e)
        {
            DataContent.RadiumCarvingSN = cbxSN.Checked;
        }
    }
}
