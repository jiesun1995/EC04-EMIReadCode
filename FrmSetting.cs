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
    public partial class FrmSetting : Form
    {
        private SystemConfig _systemConfig;
        private readonly FrmInternetConfig _burn;
        private readonly FrmInternetConfig _radiumCarving;
        private readonly FrmInternetConfig _plcConfig;
        public FrmSetting()
        {
            _systemConfig = new SystemConfig();
            var json = JsonHelper.SerializeObject(DataContent.SystemConfig);
            _systemConfig = JsonHelper.DeserializeObject<SystemConfig>(json);
            InitializeComponent();
            _burn = new FrmInternetConfig(_systemConfig.Burn.IP, _systemConfig.Burn.Port,"烧录机服务配置");
            _radiumCarving = new FrmInternetConfig(_systemConfig.RadiumCarving.IP, _systemConfig.RadiumCarving.Port,"镭雕服务配置");
            _plcConfig = new FrmInternetConfig(_systemConfig.PLCConfig.IP, _systemConfig.PLCConfig.Port,"PLC网络配置");
            LoadFrm(tableLayoutPanel1, _burn, 1, 0);
            LoadFrm(tableLayoutPanel1, _radiumCarving, 0, 0);
            LoadFrm(tableLayoutPanel1, _plcConfig, 1, 1);
            tbxLeftCode.DataBindings.Add(new Binding("Text", _systemConfig, "LeftClientName"));
            tbxrigthCode.DataBindings.Add(new Binding("Text", _systemConfig, "RigthClientName"));
            nunCodeLength.DataBindings.Add(new Binding("Value", _systemConfig, "CodeLength"));
            tbxTimeOut.DataBindings.Add(new Binding("Text", _systemConfig, "SocketTimeout"));
        }
        private TableLayoutPanel LoadFrm(TableLayoutPanel tableLayout, Form form, int col, int row)
        {
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / tableLayout.ColumnCount));
            tableLayout.RowStyles.Add(new ColumnStyle(SizeType.Percent, 100 / tableLayout.RowCount));
            var panel = new Panel();
            panel.Dock = DockStyle.Fill;
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;
            panel.Controls.Add(form);
            form.Show();
            tableLayout.Controls.Add(panel, col, row);
            return tableLayout;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _systemConfig.Burn = _burn.StationData;
            _systemConfig.RadiumCarving = _radiumCarving.StationData;
            _systemConfig.PLCConfig.IP = _plcConfig.StationData.IP;
            _systemConfig.PLCConfig.Port = _plcConfig.StationData.Port;
            DataContent.SetConfig(_systemConfig);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
