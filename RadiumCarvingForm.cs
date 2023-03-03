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
    public partial class RadiumCarvingForm : Form
    {
        private readonly SocketClient _socketClient ;
        private readonly Stopwatch _stopwatch;
        public RadiumCarvingForm(string ip, int port,string title="镭雕")
        {
            InitializeComponent();
            _socketClient = new SocketClient(ip,port);
            _stopwatch=new Stopwatch();
            gbxTitle.Text = title;
            timer1.Start();
        }

        public void SendMsg(string SN)
        {
            lblSn.Text = $"产品SN:{SN}";
            _stopwatch.Restart();
            _socketClient.Send(SN);
            var result = _socketClient.Receive();
            if(string.IsNullOrEmpty(result)) 
            {
                lblResult.Text = $"镭雕结果:{result}";
                lblResult.BackColor = Color.Gray;
            }
            else
            {
                lblResult.Text = $"镭雕结果:{result}";
                lblResult.BackColor = Color.Red;
            }
            _stopwatch.Stop();
            lblTime.Text =$"镭雕耗时:{_stopwatch.Elapsed.TotalMilliseconds}ms" ;
        }

        private void RadiumCarvingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _socketClient.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            lblState.BackColor=_socketClient!=null&& _socketClient.Connected? Color.Green : Color.Red;
        }
    }
}
