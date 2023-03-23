using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using Cognex.VisionPro.ToolBlock;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P117_EMIReadCode.Comm
{
    public class VisionHelper
    {
        private readonly CogToolBlock _cogToolBlock;
        public VisionHelper(string vppFile) 
        {
            if(!File.Exists(vppFile)) { throw new Exception($"Vpp执行文件不存在：{vppFile}"); }
            _cogToolBlock = CogSerializer.LoadObjectFromFile(vppFile) as CogToolBlock;
        }
        public void SetInput(string name,object val)
        {
            _cogToolBlock.Inputs[name].Value = val;
        }
        public string GetOutput(string name)
        {
            return _cogToolBlock.Outputs[name].Value.ToString();
        }
        public void DisPaly(CogRecordDisplay cogDisplay)
        {
            cogDisplay.Record = _cogToolBlock.CreateLastRunRecord().SubRecords[1];
            cogDisplay.Fit();
        }

        public void Run()
        {
            _cogToolBlock.Run();
        }
    }
}
