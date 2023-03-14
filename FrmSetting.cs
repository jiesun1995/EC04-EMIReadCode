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
        //private readonly FrmInternetConfig _leftBurn;
        //private readonly FrmInternetConfig _rightBurn;
        private readonly FrmInternetConfig _radiumCarving;
        //private readonly FrmInternetConfig _rightRadiumCarving;
        private readonly FrmInternetConfig _plcConfig;
        public FrmSetting()
        {
            _systemConfig = DataContent.SystemConfig;
            InitializeComponent();
            //_leftBurn = new FrmInternetConfig(_systemConfig.LeftBurn.IP, _systemConfig.LeftBurn.Port);
            //_rightBurn = new FrmInternetConfig(_systemConfig.RightBurn.IP, _systemConfig.RightBurn.Port);
            _radiumCarving = new FrmInternetConfig(_systemConfig.RadiumCarving.IP, _systemConfig.RadiumCarving.Port,"镭雕服务配置");
            //_rightRadiumCarving = new FrmInternetConfig(_systemConfig.RightRadiumCarving.IP, _systemConfig.RightRadiumCarving.Port);
            _plcConfig = new FrmInternetConfig(_systemConfig.PLCConfig.IP, _systemConfig.PLCConfig.Port,"PLC网络配置");
            nunCodeLength.Value = _systemConfig.CodeLength;
            //LoadFrm(tableLayoutPanel1, _leftBurn, 0, 0);
            //LoadFrm(tableLayoutPanel1, _rightBurn, 1, 0);
            LoadFrm(tableLayoutPanel1, _radiumCarving, 0, 0);
            //LoadFrm(tableLayoutPanel1, _rightRadiumCarving, 0, 1);
            LoadFrm(tableLayoutPanel1, _plcConfig, 1, 1);
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
            var json = JsonHelper.SerializeObject(_systemConfig);
            var systemConfig = JsonHelper.DeserializeObject<SystemConfig>(json);
            //systemConfig.LeftBurn.IP = _leftBurn?.IP;
            //systemConfig.LeftBurn.Port = _leftBurn.Port;
            //systemConfig.RightBurn.IP = _rightBurn?.IP;
            //systemConfig.RightBurn.Port = _rightBurn.Port;
            systemConfig.RadiumCarving.IP = _radiumCarving.IP;
            systemConfig.RadiumCarving.Port = _radiumCarving.Port;
            //systemConfig.RightRadiumCarving.IP = _rightRadiumCarving.IP;
            //systemConfig.RightRadiumCarving.Port = _rightRadiumCarving.Port;
            systemConfig.PLCConfig.IP = _plcConfig.IP;
            systemConfig.PLCConfig.Port = _plcConfig.Port;
            systemConfig.CodeLength = Convert.ToInt32(nunCodeLength.Value);

            DataContent.SetConfig(systemConfig);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
