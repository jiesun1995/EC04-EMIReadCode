using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace P117_EMIReadCode.Comm
{
    public static class DataContent
    {
        public static SystemConfig SystemConfig { private set; get; } = new SystemConfig();

        public static CacheData CacheData { private set; get; } = new CacheData();

        public static string User { set; get; }
        /// <summary>
        /// 屏蔽镭雕工站
        /// </summary>
        public static bool RadiumCarving { get; set; }
        /// <summary>
        /// 固定镭雕SN
        /// </summary>
        public static bool RadiumCarvingSN { get; set; }
        /// <summary>
        /// 屏蔽烧录工站
        /// </summary>
        public static bool Burn { get; set; }
       

        public static void SetCache(CacheData cacheData)
        {
            CacheData = cacheData;
            var json = JsonConvert.SerializeObject(CacheData);
            File.WriteAllText("data.json", json);
        }

        public static void SetConfig(SystemConfig systemConfig)
        {
            SystemConfig = systemConfig;
            var json = JsonConvert.SerializeObject(SystemConfig);
            File.WriteAllText("System.Config", json);
        }

        public static void LoadConfig()
        {
            if (File.Exists("System.Config"))
            {
                var result = File.ReadAllText("System.Config");
                var config = JsonConvert.DeserializeObject<SystemConfig>(result);
                DataContent.SetConfig(config);
            }
            if (File.Exists("data.json"))
            {
                var result = File.ReadAllText("data.json");
                var cache = JsonConvert.DeserializeObject<CacheData>(result);
                DataContent.SetCache(cache);
            }
        }
    }
    public class SystemConfig
    {
        /// <summary>
        /// 光源控制串口
        /// </summary>
        public string PortName { get; set; } = "COM1";
        /// <summary>
        /// 光源控制串口波特率
        /// </summary>
        public int BaudRate { get; set; } = 115200;

        /// <summary>
        /// mes地址
        /// </summary>
        public string MesUrl { get; set; } = "http://192.168.16.30/Bobcat/sfc_response.aspx";
        /// <summary>
        /// Mes站点名称
        /// </summary>
        public string StationName { get; set; } = "EMI打标";
        /// <summary>
        /// mes站点ID
        /// </summary>
        public string StationId { get; set; } = "A06-3FT-01_2_EMI打标";
        public string SystemPassWord { get; set; } = "jajqr168";
        public string PassWord { get; set; } = "888888";
        public int CodeLength { get; set; } = 17;
        public string LeftVppPath { set; get; }
        public string RigthVppPath { set; get; }
        public int SocketTimeout { set; get; } = 5 * 1000;
        public CameraConfig LeftCamera { get; set; }=new CameraConfig();
        public CameraConfig RigthCamera { get; set; }=new CameraConfig();

        public PLCConfig PLCConfig { get; set; }=new PLCConfig();
        /// <summary>
        /// 左烧录工位数
        /// </summary>
        public StationData Burn { get; set; } = new StationData();
        /// <summary>
        /// 镭雕工位
        /// </summary>
        public StationData RadiumCarving { get; set; } = new StationData();
        /// <summary>
        /// 左烧录位socket名称
        /// </summary>
        public string LeftClientName { get; set; } = "L";
        /// <summary>
        /// 左烧录位socket名称
        /// </summary>
        public string RigthClientName { get; set; } = "R";

        ///// <summary>
        ///// 右镭雕工位
        ///// </summary>
        //public StationData RightRadiumCarving { get; set; } = new StationData();
        
    }

    public class CacheData
    {
        [JsonProperty]
        private Queue<string> BurnCodes { set; get; } = new Queue<string>();
        public void AddBurnCode(string key)
        {
            if (BurnCodes.Count >= 4)
            {
                BurnCodes.Dequeue();
            }
            BurnCodes.Enqueue(key);
            DataContent.SetCache(this);
        }
        public bool ContainsBurnCode(string key)
        {
            return BurnCodes.Contains(key);
        }
    } 

    public class CameraConfig
    {
        public string Name { get; set; }
        public string ExposureTime { get; set; }
        public string Gain { get; set; }
    }
    public class PLCConfig
    {
        public string IP { get; set; }
        public int Port { get; set; }
    }
    /// <summary>
    /// 工站数据
    /// </summary>
    public class StationData
    {
        /// <summary>
        /// socket ip配置
        /// </summary>
        public string IP { get; set; } = "0.0.0.0";
        /// <summary>
        /// socket 端口配置
        /// </summary>
        public int Port { get; set; }
    }
}
