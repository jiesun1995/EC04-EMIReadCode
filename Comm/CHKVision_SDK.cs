using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using MvCamCtrl.NET;
//using HalconDotNet;

namespace FT210C_MylarTEST
{
    class CHKVision_SDK
    {
        MyCamera m_pMyCamera;

        /// <summary>
        /// 此项方法一个程序只需调用一次,多次调用会出现相机打开失败
        /// </summary>
        /// <param name="DeviceList"></param>
        /// <returns></returns>
        public int DeviceListAcq(ref MyCamera.MV_CC_DEVICE_INFO_LIST DeviceList)
        {
            try
            {
                int nRet;

                System.GC.Collect();
                nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref DeviceList);

                if (0 != nRet)
                    return -1;
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 根据相机名称打开指定相机
        /// </summary>
        /// <param name="DeviceList"></param>
        /// <param name="CameraName"></param>
        /// <returns></returns>
        public int OpenCamera(MyCamera.MV_CC_DEVICE_INFO_LIST DeviceList,string CameraName)
        {
            try
            {
                int nRet;

                for (int CamIndex = 0; CamIndex < DeviceList.nDeviceNum; CamIndex++)
                {
                    MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(DeviceList.pDeviceInfo[CamIndex], typeof(MyCamera.MV_CC_DEVICE_INFO));
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stGigEInfo, 0);
                    MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                    if (gigeInfo.chUserDefinedName == CameraName)
                    {
                        if (null == m_pMyCamera)
                        {
                            m_pMyCamera = new MyCamera();
                            if (null == m_pMyCamera)
                            {
                                return -1;
                            }
                        }

                        nRet = m_pMyCamera.MV_CC_CreateDevice_NET(ref device);
                        if (MyCamera.MV_OK != nRet)
                        {
                            return -1;
                        }

                        nRet = m_pMyCamera.MV_CC_OpenDevice_NET();
                        if (MyCamera.MV_OK != nRet)
                        {
                            m_pMyCamera.MV_CC_DestroyDevice_NET();
                            return -1;
                        }

                        m_pMyCamera.MV_CC_SetEnumValue_NET("AcquisitionMode", 2);
                        m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerMode", 0);
                        m_pMyCamera.MV_CC_SetEnumValue_NET("ExposureAuto", 0);
                        m_pMyCamera.MV_CC_SetEnumValue_NET("GainAuto", 0);
                        m_pMyCamera.MV_CC_SetCommandValue_NET("TriggerSoftware");
                        m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerSource", 7);
                        
                        //开始采集
                        m_pMyCamera.MV_CC_StartGrabbing_NET();

                        return 0;
                    }
                }

                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 关闭相机
        /// </summary>
        /// <returns></returns>
        public int CloseCamera()
        {
            try
            {
                int nRet;

                nRet = m_pMyCamera.MV_CC_StopGrabbing_NET();
                if (nRet != MyCamera.MV_OK)
                    return -1;

                nRet = m_pMyCamera.MV_CC_CloseDevice_NET();
                if (MyCamera.MV_OK != nRet)
                    return -1;

                nRet = m_pMyCamera.MV_CC_DestroyDevice_NET();
                if (MyCamera.MV_OK != nRet)
                    return -1;

                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 抓取一副VisionPro CogImage图片
        /// </summary>
        /// <param name="Image"></param>
        /// <returns></returns>
        public int GrabImageToCogImg(out Cognex.VisionPro.ICogImage Image)
        {
            Image = null;

            try
            {
                UInt32 nPayloadSize = 0;

                MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
                m_pMyCamera.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);

                nPayloadSize = stParam.nCurValue;

                IntPtr pData = Marshal.AllocHGlobal((int)nPayloadSize);

                MyCamera.MV_FRAME_OUT_INFO_EX stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();
                m_pMyCamera.MV_CC_GetOneFrameTimeout_NET(pData, nPayloadSize, ref stFrameInfo, 1000);

                Cognex.VisionPro.CogImage8Root Image8Root = new Cognex.VisionPro.CogImage8Root();
                Image8Root.Initialize((int)stFrameInfo.nWidth, (int)stFrameInfo.nHeight, pData, (int)stFrameInfo.nWidth,null);
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


        //public int GrabImageToCogImgAndHalconImg(out Cognex.VisionPro.ICogImage Image,out HObject ho_Image)
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
        ///// <summary>
        ///// 抓取一幅Halcon图片
        ///// </summary>
        ///// <param name="Image"></param>
        ///// <returns></returns>
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
        public int SetExposureTime(string Val)
        {
            try
            {
                m_pMyCamera.MV_CC_SetFloatValue_NET("ExposureTimeAbs", float.Parse(Val));

                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// 设置增益
        /// </summary>
        /// <param name="Val"></param>
        /// <returns></returns>
        public int SetGain(string Val)
        {
            try
            {
                m_pMyCamera.MV_CC_SetFloatValue_NET("Gain", float.Parse(Val));

                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

    }
}
