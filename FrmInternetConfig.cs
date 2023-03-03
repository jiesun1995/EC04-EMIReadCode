﻿using System;
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
        public FrmInternetConfig(string ip,int port)
        {
           
            InitializeComponent();
            tbxIp.Text = ip;
            tbxPort.Text = port.ToString();
        }
        public string IP { get { return tbxIp.Text; } }
        public int Port { get { return int.Parse( tbxPort.Text);} }

    }
}