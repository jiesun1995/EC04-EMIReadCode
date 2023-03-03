using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace EC04_EMIReadCode.Comm
{
    public static class DataContent
    {
        public static SystemConfig SystemConfig { private set; get; } = new SystemConfig();

        public static string User { set; get; }

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
        }
    }
    public class SystemConfig
    {
        public string SystemPassWord { get; set; } = "jajqr168";
        public string PassWord { get; set; } = "888888";
        public string LeftVppPath { set; get; }
        public string RightVppPath { set; get; }
        public CameraConfig LeftCamera { get; set; }=new CameraConfig();
        public CameraConfig RightCamera { get; set; }=new CameraConfig();

        public PLCConfig PLCConfig { get; set; }=new PLCConfig();
        /// <summary>
        /// 左烧录工位数
        /// </summary>
        public StationData LeftBurn { get; set; } = new StationData();
        /// <summary>
        /// 右烧录工位数
        /// </summary>
        public StationData RightBurn { get; set; } = new StationData();
        /// <summary>
        /// 左镭雕工位
        /// </summary>
        public StationData LeftRadiumCarving { get; set; } = new StationData();
        /// <summary>
        /// 右镭雕工位
        /// </summary>
        public StationData RightRadiumCarving { get; set; } = new StationData();
        [JsonProperty]
        private Queue<string> BurnCodes { set; get; } =new Queue<string>();
        public void AddBurnCode(string key)
        {
            if (BurnCodes.Count > 4)
            {
                BurnCodes.Dequeue();
            }
            BurnCodes.Enqueue(key);
            DataContent.SetConfig(this);
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
        public string IP { get; set; }
        /// <summary>
        /// socket 端口配置
        /// </summary>
        public int Port { get; set; }
    }
}
