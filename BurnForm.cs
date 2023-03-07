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
        private readonly SocketClient _socketClient;
        private readonly Stopwatch _stopwatch;
        private string _code=string.Empty;

        public BurnForm(string ip,int port,string title="烧录")
        {
            InitializeComponent();
            _socketClient = new SocketClient(ip, port);
            _stopwatch = new Stopwatch();
            gbxTitle.Text = title;
            timer1.Start();
        }
        private string QueryCode(string sn)
        {
            try
            {
                SystemHelper.UIShow(tbxSN, () => { tbxSN.Text = sn; btnMesCode.BackColor = Color.Green; });
            }
            catch (Exception ex)
            {
                LogManager.MesLogs.Error(ex);
                SystemHelper.UIShow(tbxSN, () => { tbxSN.Text = sn; btnMesCode.BackColor = Color.Red; });
            }
            return sn;
            
        }
        private bool Receive(string code)
        {
            var data = "NG";
            try
            {

                _socketClient.Send(code);
                data = _socketClient.Receive();
                SystemHelper.UIShow(btnBurn, () => { btnBurn.BackColor = Color.Green; });
            }
            catch (Exception ex)
            {
                LogManager.Logs.Error(ex);
                SystemHelper.UIShow(btnBurn, () => { btnBurn.BackColor = Color.Red; });
            }
            var result = data == "OK";
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
        public bool SendMsg(string sn)
        {
            _stopwatch.Restart();
            var code = QueryCode(sn);
            var result = Receive(code);
            _stopwatch.Stop();
            ShowUI(result);
            return result;
        }

        private void BurnForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _socketClient.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblState.BackColor = _socketClient != null && _socketClient.Connected ? Color.Green : Color.Red;
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
            var sn = tbxSN.Text;
            _stopwatch.Restart();
            var code = QueryCode(sn);
            var result = Receive(code);
            _stopwatch.Stop();
            ShowUI(result);
        }

        private void btnMesCode_Click(object sender, EventArgs e)
        {
            var sn = tbxSN.Text;
            _code=QueryCode(sn);
        }

        private void btnBurn_Click(object sender, EventArgs e)
        {
            var result = Receive(_code);
            ShowUI(result);
        }
    }
}
