﻿using P117_EMIReadCode.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P117_EMIReadCode
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LogManager.Init(null);
            SystemHelper.OnlyRun("EC04_EMIReadCode", () =>
            {
                LogManager.Logs.Debug("开始加载配置");
                DataContent.LoadConfig();
                LogManager.Logs.Debug("加载配置完成");
                LogManager.PLCLogs.Debug("PLC日志初始化成功");
                LogManager.RadiumCarvingLogs.Debug("镭雕日志初始化成功");
                LogManager.BurnLogs.Debug("烧录日志初始化成功");
                Application.Run(new FrmMain());
            });
        }
    }
}
