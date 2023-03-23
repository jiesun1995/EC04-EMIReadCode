using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P117_EMIReadCode.Comm
{
    public class MesService
    {
        /// <summary>
        /// mes过站请求
        /// </summary>
        /// <param name="SN">产品sn</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool PassStation(string SN)
        {
            var result = string.Empty;
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("result", "PASS");
            dict.Add("c", "ADD_RECORD");
            dict.Add("product", "B1037");
            dict.Add("test_station_name", DataContent.SystemConfig.StationName);
            dict.Add("station_id", DataContent.SystemConfig.StationId);
            dict.Add("audit_mode", "0");
            dict.Add("start_time", DateTime.Now.ToString());
            dict.Add("stop_time", DateTime.Now.ToString());
            dict.Add("sn", SN);
            dict.Add("list_of_failing_tests", "原因");
            dict.Add("failure_message", "描述");
            LogManager.Logs.Info($"过站请求参数:{JsonHelper.SerializeObject(dict)}");
            var data = HttpHelper.PostHandle(DataContent.SystemConfig.MesUrl, dict);
            LogManager.Logs.Info($"过站请求结果：{data}");
            if (!data.StartsWith("0 SFC_OK"))
                return false;
            return true;
        }

        /// <summary>
        /// mes UOP卡关
        /// </summary>
        /// <param name="SN">产品sn</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool QueryStation(string SN)
        {
            var result = string.Empty;
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("c", "QUERY_RECORD");
            dict.Add("sn", SN);
            dict.Add("tsid", DataContent.SystemConfig.StationId);
            dict.Add("p", "unit_process_check");
            LogManager.Logs.Info($"获取当前站点:{JsonHelper.SerializeObject(dict)}");
            var data = HttpHelper.PostHandle(DataContent.SystemConfig.MesUrl, dict);
            LogManager.Logs.Info($"当前站点结果：{data}");
            if (data.StartsWith("0 SFC_OK") && data.Contains("unit_process_check=OK"))
                return true;
            return false;
        }

        /// <summary>
        /// 通过SN获取当前站点
        /// </summary>
        /// <param name="SN">产品sn</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string GetCurrStation(string SN)
        {
            var result = string.Empty;
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("c", "QUERY_HISTORY");
            dict.Add("sn", SN);
            dict.Add("p", "Get_CurrStation");
            LogManager.Logs.Info($"获取当前站点:{JsonHelper.SerializeObject(dict)}");
            var data = HttpHelper.PostHandle(DataContent.SystemConfig.MesUrl, dict);
            LogManager.Logs.Info($"当前站点结果：{data}");
            if (!data.StartsWith("0 SFC_OK"))
                throw new Exception($"处理Mes结果异常 :{data}");
            var ss = data.Split('\n');
            foreach (var s in ss)
            {
                var start = "Get_CurrStation=";
                var startIndex = s.IndexOf(start);
                if (startIndex >= 0)
                {
                    result = s.Substring(startIndex + start.Length, s.Length - (startIndex + start.Length));
                }
            }
            return result;
        }
    }
}
