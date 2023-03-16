using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EC04_EMIReadCode.Comm
{
    public class SocketServer
    {
        private readonly string _ip = string.Empty;
        private readonly int _port = 0;
        private Socket _socket = null;
        private byte[] buffer = new byte[1024 * 1024 * 2];
        private Action<Socket> _connnectClientAct;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip">监听的IP</param>
        /// <param name="port">监听的端口</param>
        public SocketServer(string ip, int port,Action<Socket> connnectClientAct)
        {
            this._ip = ip;
            this._port = port;
            this._connnectClientAct = connnectClientAct;
        }
        public SocketServer(int port)
        {
            this._ip = "0.0.0.0";
            this._port = port;
        }

        public void StartListen()
        {
            try
            {
                //1.0 实例化套接字(IP4寻找协议,流式协议,TCP协议)
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //2.0 创建IP对象
                IPAddress address = IPAddress.Parse(_ip);
                //3.0 创建网络端口,包括ip和端口
                IPEndPoint endPoint = new IPEndPoint(address, _port);
                //4.0 绑定套接字
                _socket.Bind(endPoint);
                //5.0 设置最大连接数
                _socket.Listen(int.MaxValue);
                LogManager.Logs.Debug($"监听{_socket.LocalEndPoint.ToString()}消息成功");
                //Console.WriteLine("监听{0}消息成功", _socket.LocalEndPoint.ToString());
                //6.0 开始监听
                Thread thread = new Thread(ListenClientConnect);
                thread.Start();

            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// 监听客户端连接
        /// </summary>
        private void ListenClientConnect()
        {
            try
            {
                while (true)
                {
                    //Socket创建的新连接
                    Socket clientSocket = _socket.Accept();
                    //clientSocket.Send(Encoding.UTF8.GetBytes("服务端发送消息:"));
                    //clients.Add(clientSocket);
                    //Thread thread = new Thread(ReceiveMessage);
                    //thread.Start(clientSocket);
                    _connnectClientAct(clientSocket);
                }
            }
            catch (Exception)
            {
            }
        }

        //public int ClientCount { get => clients.Count; }
        //public void SendMessage(string msg)
        //{
        //    for (int i = clients.Count-1; i <= 0; i++)
        //    {
        //        var client = clients[i];
        //        if (client.Connected)
        //        {
        //            try
        //            {
        //                LogManager.Logs.Debug($"向客户端{client.RemoteEndPoint.ToString()}，发送消息{msg}");
        //                client.Send(Encoding.UTF8.GetBytes(msg));
        //            }
        //            catch (Exception ex)
        //            {
        //                LogManager.Logs.Error(ex);
        //            }
        //        }
        //        else
        //        {
        //            clients.Remove(client);
        //        }
        //    }
        //}
        /// <summary>
        /// 接收客户端消息
        /// </summary>
        /// <param name="socket">来自客户端的socket</param>
        private void ReceiveMessage(object socket)
        {
            Socket clientSocket = (Socket)socket;
            while (clientSocket.Connected)
            {
                Thread.Sleep(10);
                try
                {
                    //获取从客户端发来的数据
                    int length = clientSocket.Receive(buffer);
                    var data = Encoding.UTF8.GetString(buffer, 0, length);
                    if (string.IsNullOrEmpty(data)){
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Close();
                        //clients.Remove(clientSocket);
                        break;
                    }
                    LogManager.Logs.Debug($"接收客户端{ clientSocket.RemoteEndPoint.ToString()},消息{data}");
                    //Console.WriteLine("接收客户端{0},消息{1}", clientSocket.RemoteEndPoint.ToString(), Encoding.UTF8.GetString(buffer, 0, length));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    break;
                }
            }
        }
        public void Close()
        {
            if (_socket != null)
            {
                //if (clients.Count > 0)
                //{
                //    foreach (var client in clients)
                //    {
                //        client.Shutdown(SocketShutdown.Both);
                //        //client.Close();
                //    }
                //}
                //_socket.Disconnect(false);
                _socket.Close();
                _socket.Dispose();
            }
        }
    }
    public class SocketClient
    {
        private string _ip = string.Empty;
        private int _port = 0;
        private Socket _socket = null;
        private byte[] buffer = new byte[1024 * 1024 * 2];

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip">连接服务器的IP</param>
        /// <param name="port">连接服务器的端口</param>
        public SocketClient(string ip, int port,int timeOut=5*1000)
        {
            this._ip = ip;
            this._port = port;
            try
            {
                //1.0 实例化套接字(IP4寻址地址,流式传输,TCP协议)
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //2.0 创建IP对象
                IPAddress address = IPAddress.Parse(_ip);
                //3.0 创建网络端口包括ip和端口
                IPEndPoint endPoint = new IPEndPoint(address, _port);
                //4.0 建立连接
                _socket.Connect(endPoint);
                _socket.ReceiveTimeout = timeOut;
                _socket.SendTimeout = timeOut;
            }
            catch (Exception ex)
            {
                if(_socket.Connected)
                {
                    _socket.Shutdown(SocketShutdown.Both);
                    _socket.Close();
                }
            }
        }
        public bool Connected { get => _socket.Connected; }
        public void Send(string msg)
        {
            _socket.Send(Encoding.UTF8.GetBytes(msg));
        }
        public string Receive()
        {
            int length = _socket.Receive(buffer);
            var msg = Encoding.UTF8.GetString(buffer, 0, length);
            return msg;
        }
        public void Close()
        {
            if (_socket != null && _socket.Connected)
            {
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();
            }
        }
    }
}
