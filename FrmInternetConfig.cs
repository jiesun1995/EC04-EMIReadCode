using EC04_EMIReadCode.Comm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EC04_EMIReadCode
{
    public partial class FrmInternetConfig : Form
    {
        private StationData _stationData;
        public FrmInternetConfig(string ip,int port,string title="网络配置")
        {
            _stationData = new StationData
            {
                IP=ip,
                Port=port,
            };
            InitializeComponent();
            tbxIp.DataBindings.Add(new Binding("Text", _stationData, "IP"));
            tbxPort.DataBindings.Add(new Binding("Text", _stationData, "Port"));
            gbxTitle.Text = title;
        }
        public string IP { get { return _stationData.IP; } }
        public int Port { get { return _stationData.Port; } }
        public StationData StationData { get { return _stationData; } }
    }
}
