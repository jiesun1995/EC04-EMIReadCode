using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EC04_EMIReadCode.Comm
{
    public class SystemHelper
    {
        public static void OnlyRun(string name,Action action)
        {
            bool running = false;
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, name,out running);
            try
            {
                if (running)
                    action();
            }
            catch (Exception ex)
            {
                LogManager.Logs.Fatal(ex);
            }
            if (!running)
            {
                MessageBox.Show("已经运行了一个实例（或旧实例尚未完全关闭），为避免发生异常请不要重复运行程序!", "提示", MessageBoxButtons.OK);
                System.Environment.Exit(0);
            }
            //mutex.ReleaseMutex();
        }


        public static void UIShow(Control control,Action act)
        {
            if (control.IsHandleCreated)
            {
                control.Invoke(act);
            }
            else
            {
                act();
            }
        }
    }
}
