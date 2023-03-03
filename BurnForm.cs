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

        public BurnForm(string ip,int port,string title="烧录")
        {
            InitializeComponent();
            _socketClient = new SocketClient(ip, port);
            _stopwatch = new Stopwatch();
            gbxTitle.Text = title;
            timer1.Start();
        }
        public bool SendMsg(string SN)
        {
            lblSn.Text = $"产品SN:{SN}";
            _stopwatch.Restart();
            _socketClient.Send(SN);
            var data = _socketClient.Receive();
            var result = data == "OK";
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
            _stopwatch.Stop();
            lblTime.Text = $"烧录耗时:{_stopwatch.Elapsed.TotalMilliseconds}ms";
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
    }
}
