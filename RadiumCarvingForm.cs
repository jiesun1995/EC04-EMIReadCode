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
            _stopwatch.Restart();
            var data = "NG";
            try
            {
                _socketClient.Send(SN);
                data = _socketClient.Receive();
            }
            catch (Exception ex)
            {
                LogManager.Logs.Error(ex);
            }
            _stopwatch.Stop();
            var result = data == "OK";
            if (lblSn.IsHandleCreated)
            {
                Invoke(new Action(() =>
                {
                    lblSn.Text = $"产品SN:{SN}";
                    if (result)
                    {
                        lblResult.Text = $"镭雕结果:{result}";
                        lblResult.BackColor = Color.Gray;
                    }
                    else
                    {
                        lblResult.Text = $"镭雕结果:{result}";
                        lblResult.BackColor = Color.Red;
                    }
                    lblTime.Text = $"镭雕耗时:{_stopwatch.Elapsed.TotalMilliseconds}ms";
                }));
            }
            else
            {
                lblSn.Text = $"产品SN:{SN}";
                if (result)
                {
                    lblResult.Text = $"镭雕结果:{result}";
                    lblResult.BackColor = Color.Gray;
                }
                else
                {
                    lblResult.Text = $"镭雕结果:{result}";
                    lblResult.BackColor = Color.Red;
                }
                lblTime.Text = $"镭雕耗时:{_stopwatch.Elapsed.TotalMilliseconds}ms";
               
            }
               
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
