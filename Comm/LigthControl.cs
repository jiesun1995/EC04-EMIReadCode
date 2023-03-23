using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P117_EMIReadCode.Comm
{
    public class LigthControl
    {
        private static LigthControl _ligthControl;
        private SerialPort _serialPort;
        private readonly System.Windows.Forms.Timer timer;
        private Dictionary<string, Stopwatch> _ligthCache;

        private LigthControl(string portName, int baudRate)
        {
            _serialPort = new SerialPort();
            _serialPort.PortName = portName;
            _serialPort.BaudRate = baudRate;
            try
            {
                _serialPort.Open();
                _serialPort.Write("OK");
            }
            catch (Exception ex)
            {
                LogManager.Logs.Error(ex);
            }
            _ligthCache = new Dictionary<string, Stopwatch>();
            timer = new System.Windows.Forms.Timer();
            timer.Tick += (s, e) =>
            {
                for (int i = _ligthCache.Count-1; i >= 0; i--)
                {
                    var key = _ligthCache.Keys.ToList()[i];
                    if (_ligthCache[key].Elapsed.TotalSeconds >= 10)
                    {
                        Of(key);
                        _ligthCache.Remove(key);
                    }
                }
            };
            timer.Start();
        }
        public static LigthControl Instance(string portName, int baudRate)
        {
            if (_ligthControl == null)
            {
                _ligthControl = new LigthControl(portName, baudRate);
            }
            return _ligthControl;
        }
        public void On(string ch)
        {
            if (_ligthCache.ContainsKey(ch))
            {
                _ligthCache[ch].Restart();
                return;
            }
            if (!_serialPort.IsOpen)
            {
                try
                {
                    _serialPort.Open();
                }
                catch (Exception ex)
                {
                    LogManager.Logs.Error(ex);
                }
            }
            _serialPort.Write($"{ch}ONOK");
            Thread.Sleep(300);
            LogManager.Logs.Debug($"{ch}光源开启");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            _ligthCache.Add(ch, stopwatch);
        }
        private void Of(string ch)
        {
            if (!_serialPort.IsOpen)
            {
                try
                {
                    _serialPort.Open();
                }
                catch (Exception ex)
                {
                    LogManager.Logs.Error(ex);
                }
            }
            _serialPort.Write($"{ch}OFOK");
            LogManager.Logs.Debug($"{ch}光源关闭");
        }

        public void Close()
        {
            foreach (var item in _ligthCache)
            {
                Of(item.Key);
            }
            _ligthCache.Clear();
            if (_serialPort != null && _serialPort.IsOpen)
                _serialPort.Close();
        }
    }
}
