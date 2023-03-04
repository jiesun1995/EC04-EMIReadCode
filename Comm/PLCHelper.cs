using ICTCommunication.ModBus;
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
        private readonly ModbusTcpNet plcNet = new ModbusTcpNet();
        private bool _isConnect = false;
        public PLCHelper(string ip, int port = 502, byte station = 1)
        {
            plcNet = new ModbusTcpNet(ip, port, station);
            //首地址不从零开始
            plcNet.AddressStartWithZero = false;
            //字符串颠倒
            plcNet.IsStringReverse = false;
            plcNet.DataFormat = ICTCommunication.Core.DataFormat.CDAB;
            //编码格式 暂定不使用
            plcNet.ByteTransform.DataFormat = ICTCommunication.Core.DataFormat.CDAB;
            //长链接模式
            plcNet.ConnectServer();
        }

        public bool IsConnect { get { return _isConnect; } }

        public ushort Read(string address)
        {
            var result = plcNet.ReadUInt16(address);
            _isConnect = result.IsSuccess;
            if (result.IsSuccess)
            {
                return result.Content;
            }
            else
            {
                throw new Exception($"读取地址[{address}]失败;");
            }
        }
        public void Write(string address, ushort val)
        {
            LogManager.PLCLogs.Debug($"往地址[{address}:{val}]写入");
            var result = plcNet.Write(address, val);
            _isConnect = result.IsSuccess;
            if (!result.IsSuccess)
            {
                throw new Exception($"写入地址[{address}:{val}]失败;");
            }
        }
        public void Write(string address, PLCResult val)
        {
            Write(address,(ushort)val);
        }
        public void Close()
        {
            if(plcNet!= null)
            {
                plcNet.ConnectClose();
                plcNet.Dispose();
            }
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
