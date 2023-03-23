using ICTCommunication.ModBus;
using ICTCommunication.Profinet.Omron;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace P117_EMIReadCode.Comm
{
    public class PLCHelper
    {
        private readonly OmronFinsUdp _omronFinsUdp;
        private bool _isConnect = false;
        private readonly Dictionary<int, string> _addressDec;
        public PLCHelper(string ip, int port)
        {
            try
            {
                _addressDec = new Dictionary<int, string>();
                _omronFinsUdp = new OmronFinsUdp(ip, port);
            }
            catch (Exception ex)
            {
                throw new Exception("PLC初始化失败", ex);
            }
        }
        public PLCHelper(string ip, int port,Dictionary<int,string> addressDec)
        {
            try
            {
                _addressDec = addressDec;
                _omronFinsUdp = new OmronFinsUdp(ip, port);
                
            }
            catch (Exception ex)
            {
                throw new Exception("PLC初始化失败", ex);
            }
        }
        public bool IsConnect { get => _isConnect; }

        public int Read(int address)
        {
            var result = _omronFinsUdp.ReadUInt16($"D{address}");
            _isConnect = result.IsSuccess;
            return result.Content;
        }

        public bool Write(int address, int value)
        {
            var result = _omronFinsUdp.Write($"D{address}", (ushort)value);
            return result.IsSuccess;
        }
        public void Write(int address, PLCResult val)
        {
            if (_addressDec != null && _addressDec.ContainsKey(address))
                LogManager.PLCLogs.Debug($"往plc写入:{_addressDec[address]}:{val}");
            else
                LogManager.PLCLogs.Debug($"往plc写入:D{address}:{val}");
            Write(address, (ushort)val);
        }
        public void Close()
        {
        }
    }
    public enum PLCResult
    {
        /// <summary>
        /// 读码成功
        /// </summary>
        OK = 2,
        /// <summary>
        /// 读码失败
        /// </summary>
        NG = 3,
        /// <summary>
        /// 执行成功
        /// </summary>
        nodata = 5,
        /// <summary>
        /// 执行失败
        /// </summary>
        data = 0,
    }
}
