using FT210C_MylarTEST;
using MvCamCtrl.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EC04_EMIReadCode.Comm
{
    public class CameraHelper
    {
        private MyCamera.MV_CC_DEVICE_INFO_LIST _deviceList;
        private static CameraHelper _cameraHelper;
        private static Dictionary<string, MyCamera> _cameras;
        private CameraHelper() {
            var nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref _deviceList);
            _cameras = new Dictionary<string, MyCamera>();
            if (0 != nRet)
                throw new Exception("初始化相机失败");
        }  
        public static CameraHelper Instance {
            get
            {
                if (_cameraHelper == null) { _cameraHelper = new CameraHelper(); }
                return _cameraHelper;
            }
        }

        public MyCamera Open(string cameraName)
        {
            int nRet;
            MyCamera myCamera= new MyCamera();
            if (_cameras.ContainsKey(cameraName))
                return _cameras[cameraName];
            try
            {
                for (int CamIndex = 0; CamIndex < _deviceList.nDeviceNum; CamIndex++)
                {
                    MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(_deviceList.pDeviceInfo[CamIndex], typeof(MyCamera.MV_CC_DEVICE_INFO));
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stGigEInfo, 0);
                    MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                    if (gigeInfo.chUserDefinedName == cameraName)
                    {
                        nRet = myCamera.MV_CC_CreateDevice_NET(ref device);
                        if (MyCamera.MV_OK != nRet)
                        {
                            throw new Exception("创建相机链接失败");
                        }

                        nRet = myCamera.MV_CC_OpenDevice_NET();
                        if (MyCamera.MV_OK != nRet)
                        {
                            myCamera.MV_CC_DestroyDevice_NET();
                            throw new Exception("打开相机失败");
                        }

                        myCamera.MV_CC_SetEnumValue_NET("AcquisitionMode", 2);
                        myCamera.MV_CC_SetEnumValue_NET("TriggerMode", 0);
                        myCamera.MV_CC_SetEnumValue_NET("ExposureAuto", 0);
                        myCamera.MV_CC_SetEnumValue_NET("GainAuto", 0);
                        myCamera.MV_CC_SetCommandValue_NET("TriggerSoftware");
                        myCamera.MV_CC_SetEnumValue_NET("TriggerSource", 7);

                        //开始采集
                        myCamera.MV_CC_StartGrabbing_NET();
                        _cameras.Add(cameraName,myCamera);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return myCamera;
        }
        public bool Close(string cameraName, MyCamera myCamera)
        {
            try
            {
                int nRet;

                nRet = myCamera.MV_CC_StopGrabbing_NET();
                if (nRet != MyCamera.MV_OK)
                    throw new Exception("停止采集失败");

                nRet = myCamera.MV_CC_CloseDevice_NET();
                if (MyCamera.MV_OK != nRet)
                    throw new Exception("关闭链接失败");

                nRet = myCamera.MV_CC_DestroyDevice_NET();
                if (MyCamera.MV_OK != nRet)
                    throw new Exception("摧毁链接失败");
                if (_cameras.ContainsKey(cameraName))
                    _cameras.Remove(cameraName);
                return true;
            }
            catch (Exception ex)
            {
                LogManager.Logs.Error(ex);
                return false;
            }
        }
        /// <summary>
        /// 抓取一副VisionPro CogImage图片
        /// </summary>
        /// <param name="Image"></param>
        /// <returns></returns>
        public int GrabImageToCogImg(MyCamera myCamera,out Cognex.VisionPro.ICogImage Image)
        {
            Image = null;

            try
            {
                UInt32 nPayloadSize = 0;

                MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
                myCamera.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);

                nPayloadSize = stParam.nCurValue;

                IntPtr pData = Marshal.AllocHGlobal((int)nPayloadSize);

                MyCamera.MV_FRAME_OUT_INFO_EX stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();
                myCamera.MV_CC_GetOneFrameTimeout_NET(pData, nPayloadSize, ref stFrameInfo, 1000);

                Cognex.VisionPro.CogImage8Root Image8Root = new Cognex.VisionPro.CogImage8Root();
                Image8Root.Initialize((int)stFrameInfo.nWidth, (int)stFrameInfo.nHeight, pData, (int)stFrameInfo.nWidth, null);
                Cognex.VisionPro.CogImage8Grey Image8Grey = new Cognex.VisionPro.CogImage8Grey();
                Image8Grey.SetRoot(Image8Root);
                Image = Image8Grey.ScaleImage((int)stFrameInfo.nWidth, (int)stFrameInfo.nHeight);
                Image8Root = null;
                Image8Grey = null;
                GC.Collect();
                Marshal.FreeHGlobal(pData);

                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        //public int GrabImageToCogImgAndHalconImg(MyCamera myCamera, out Cognex.VisionPro.ICogImage Image, out HObject ho_Image)
        //{
        //    Image = null;
        //    ho_Image = null;
        //    HObject ho_TempImg = null;
        //    try
        //    {
        //        UInt32 nPayloadSize = 0;

        //        MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
        //        m_pMyCamera.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);

        //        nPayloadSize = stParam.nCurValue;

        //        IntPtr pData = Marshal.AllocHGlobal((int)nPayloadSize);

        //        MyCamera.MV_FRAME_OUT_INFO_EX stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();
        //        m_pMyCamera.MV_CC_GetOneFrameTimeout_NET(pData, nPayloadSize, ref stFrameInfo, 1000);

        //        Cognex.VisionPro.CogImage8Root Image8Root = new Cognex.VisionPro.CogImage8Root();
        //        Image8Root.Initialize((int)stFrameInfo.nWidth, (int)stFrameInfo.nHeight, pData, (int)stFrameInfo.nWidth, null);
        //        Cognex.VisionPro.CogImage8Grey Image8Grey = new Cognex.VisionPro.CogImage8Grey();
        //        Image8Grey.SetRoot(Image8Root);
        //        Image = Image8Grey.ScaleImage((int)stFrameInfo.nWidth, (int)stFrameInfo.nHeight);
        //        Image8Root = null;
        //        Image8Grey = null;

        //        HOperatorSet.GenImage1Extern(out ho_TempImg, "byte", stFrameInfo.nWidth, stFrameInfo.nHeight, pData, IntPtr.Zero);
        //        HOperatorSet.CopyImage(ho_TempImg, out ho_Image);

        //        GC.Collect();
        //        Marshal.FreeHGlobal(pData);

        //        return 0;
        //    }
        //    catch (Exception)
        //    {
        //        return -1;
        //    }
        //}

        /// <summary>
        /// 抓取一幅Halcon图片
        /// </summary>
        /// <param name="Image"></param>
        /// <returns></returns>
        //public int GrabImageToHImg(out HObject ho_Image)
        //{
        //    ho_Image = null;
        //    HObject ho_TempImg = null;

        //    try
        //    {
        //        UInt32 nPayloadSize = 0;

        //        MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
        //        m_pMyCamera.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);

        //        nPayloadSize = stParam.nCurValue;

        //        IntPtr pData = Marshal.AllocHGlobal((int)nPayloadSize);

        //        MyCamera.MV_FRAME_OUT_INFO_EX stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();
        //        m_pMyCamera.MV_CC_GetOneFrameTimeout_NET(pData, nPayloadSize, ref stFrameInfo, 1000);

        //        HOperatorSet.GenImage1Extern(out ho_TempImg, "byte", stFrameInfo.nWidth, stFrameInfo.nHeight, pData, IntPtr.Zero);
        //        HOperatorSet.CopyImage(ho_TempImg, out ho_Image);

        //        GC.Collect();
        //        Marshal.FreeHGlobal(pData);
        //        ho_TempImg = null;

        //        return 0;
        //    }
        //    catch (Exception)
        //    {
        //        return -1;
        //    }
        //}

        /// <summary>
        /// 设置相机曝光
        /// </summary>
        /// <param name="Val"></param>
        /// <returns></returns>
        public bool SetExposureTime(MyCamera myCamera,string Val)
        {
            try
            {
                myCamera.MV_CC_SetFloatValue_NET("ExposureTimeAbs", float.Parse(Val));
                return true;
            }
            catch (Exception ex)
            {
                LogManager.Logs.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// 设置增益
        /// </summary>
        /// <param name="Val"></param>
        /// <returns></returns>
        public bool SetGain(MyCamera myCamera,string Val)
        {
            try
            {
                myCamera.MV_CC_SetFloatValue_NET("Gain", float.Parse(Val));
                return true;
            }
            catch (Exception ex)
            {
                LogManager.Logs.Error(ex);
                return false;
            }
        }
    }
}
