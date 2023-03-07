using log4net;
using log4net.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EC04_EMIReadCode.Comm
{
    public class LogManager
    {
        /// <summary>
        /// 全局日志对象
        /// </summary>
        public static MyLog Logs { get; private set; }
        /// <summary>
        /// PLC日志对象
        /// </summary>
        public static MyLog PLCLogs { get; private set; }
        /// <summary>
        /// MES日志对象
        /// </summary>
        public static MyLog MesLogs { get; private set; }
        private static ListView _listView;
        private static ConcurrentQueue<Tuple<string, Color>> _queue = new ConcurrentQueue<Tuple<string, Color>>();
        /// <summary>
        /// 日志框架初始化
        /// </summary>
        public static void Init(ListView listView)
        {
            log4net.Config.XmlConfigurator.Configure();
            var logs = log4net.LogManager.GetLogger("Logs");
            Logs = new MyLog(logs, UIShow);
            MesLogs = new MyLog( log4net.LogManager.GetLogger("MesLogs"),UIShow, logs);
            PLCLogs = new MyLog(log4net.LogManager.GetLogger("PLCLogs"), UIShow, logs);
            if (listView != null)
            {
                _listView = listView;
                _listView.Columns.Clear();
                ColumnHeader col = new ColumnHeader()
                {
                    Text = "时间",
                };
                ColumnHeader col1 = new ColumnHeader()
                {
                    Text = "详细信息",
                    TextAlign = HorizontalAlignment.Left,
                    Width = 500,
                };
                _listView.Columns.Add(new ColumnHeader() { Width = 0 });
                _listView.Columns.Add(col);
                _listView.Columns.Add(col1);

                for (int i = _queue.Count; i > 0; i--)
                {
                    Tuple<string, Color> val = null;
                    if (_queue.TryDequeue(out val))
                    {
                        _listView.BeginUpdate();
                        ListViewItem listViewItem = new ListViewItem();
                        listViewItem.SubItems.Add(DateTime.Now.ToString("HH:mm:ss"));
                        listViewItem.SubItems.Add(val.Item1);
                        listViewItem.BackColor = val.Item2;
                        _listView.Items.Add(listViewItem);

                        if (_listView.Items.Count >= 100)
                        {
                            _listView.Items.RemoveAt(0);
                            _listView.Items[_listView.Items.Count - 1].EnsureVisible();
                        }
                        _listView.EndUpdate();
                    }
                }
            }
        }

        private static void UIShow(string message, Color color)
        {
            _queue.Enqueue(new Tuple<string, Color>(message, color));
            if (_listView == null)
                return;
            var task = Task.Factory.StartNew(() =>
            {
                if (_queue.Count <= 0)
                    return;
                Tuple<string, Color> val = null;
                if (_queue.TryDequeue(out val))
                {
                    _listView.Invoke((EventHandler)delegate
                    {
                        _listView.BeginUpdate();
                        ListViewItem listViewItem = new ListViewItem();
                        listViewItem.SubItems.Add(DateTime.Now.ToString("HH:mm:ss"));
                        listViewItem.SubItems.Add(val.Item1);
                        listViewItem.BackColor = val.Item2;
                        _listView.Items.Add(listViewItem);

                        if (_listView.Items.Count >= 100)
                        {
                            _listView.Items.RemoveAt(0);
                            _listView.Items[_listView.Items.Count - 1].EnsureVisible();
                        }
                        _listView.EnsureVisible(_listView.Items.Count - 1);
                        _listView.EndUpdate();
                    });
                }
            });
        }
    }

    public class MyLog
    {
        private readonly log4net.ILog _log;
        private readonly log4net.ILog _baselog;
        private readonly Action<string,Color> _action;
        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="log">当前log4net对象</param>
        /// <param name="action">写入日志后的展示函数</param>
        /// <param name="baselog">全局日志对象</param>
        public MyLog(ILog log, Action<string, Color> action,ILog baselog=null)
        {
            _log = log;
            _action = action;
            _baselog = baselog;
        }
        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="mesage"></param>
        public void Debug(object mesage)
        {
            _baselog?.Debug(mesage);
            _log.Debug(mesage);
            _action(mesage.ToString(), Color.White);
        }
        /// <summary>
        /// 一般信息
        /// </summary>
        /// <param name="mesage"></param>
        public void Info(object mesage)
        {
            _baselog?.Info(mesage);
            _log.Info(mesage);
            _action(mesage.ToString(), Color.Green);
        }
        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="mesage"></param>
        public void Warn(object mesage)
        {
            _baselog?.Warn(mesage);
            _log.Warn(mesage);
            _action(mesage.ToString(), Color.Yellow);
        }
        /// <summary>
        /// 一般错误
        /// </summary>
        /// <param name="mesage"></param>
        public void Error(object mesage)
        {
            _baselog?.Error(mesage);
            _log.Error(mesage);
            _action(mesage.ToString(), Color.Red);
        }
        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="mesage"></param>
        public void Fatal(object mesage)
        {
            _baselog?.Fatal(mesage);
            _log.Fatal(mesage);
            _action(mesage.ToString(), Color.Brown);
        }

    }
}
