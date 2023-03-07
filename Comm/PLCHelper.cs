using ICTCommunication.ModBus;
using ICTCommunication.Profinet.Omron;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EC04_EMIReadCode.Comm
{
    public class PLCHelper
    {
        private readonly OmronFinsUdp _omronFinsUdp;
        private bool _isConnect = false;
        public PLCHelper(string ip, int port)
        {
            try
            {
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
            LogManager.PLCLogs.Debug($"往plc写入:D{address}:{value}");
            var result = _omronFinsUdp.Write($"D{address}", (ushort)value);
            return result.IsSuccess;
        }
        public void Write(int address, PLCResult val)
        {
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
        ReadCodeOK = 2,
        /// <summary>
        /// 读码失败
        /// </summary>
        ReadCodeNG = 3,
        /// <summary>
        /// 执行成功
        /// </summary>
        ResultOK = 4,
        /// <summary>
        /// 执行失败
        /// </summary>
        ResultNG = 5,
    }
}
