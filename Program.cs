using EC04_EMIReadCode.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EC04_EMIReadCode
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
                LogManager.Debug("开始加载配置");
                DataContent.LoadConfig();
                LogManager.Debug("加载配置完成");
            });
            Application.Run(new FrmMain());
        }
    }
}
