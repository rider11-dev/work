using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MyNet.Components;

namespace MyNet.Components.IDCard
{
    /// <summary>
    /// 身份证读卡器辅助类：神思
    /// </summary>
    public class IDCardReaderHelper_SS
    {
        Action<IDCardData_SS> _cardDataReceiver;
        Action<string> _msgHandler;
        bool InitSucceed = false;
        string _saveFilePath;

        public const byte CMD_INIT_PORT = 0X41;    //初始化端口
        public const byte CMD_CLOSE_PORT = 0X42;   //关闭端口
        public const byte CMD_VERIFY_CARD = 0X43;     //验证卡
        public const byte CMD_READ_BASE_INFO = 0X44;     //读取基本信息
        public const byte CMD_READ_NEW_ADDR = 0X45;   //读取最新地址
        public const byte CMD_READ_CHARACTER = 0X46;    //读取文字信息
        public const byte CMD_READ_BASE_INFO_NO_IMAGE = 0X47; //读取基本信息但不进行图像解码
        public const int CODE_SUCCESS_ID1 = 62171;//没有指纹的身份证
        public const int CODE_SUCCESS_ID2 = 62172;//有指纹的身份证
        static byte[] PARAM_PAEG2 = { 0x02, 0x27, 0x00, 0x00 };

        public IDCardReaderHelper_SS(Action<IDCardData_SS> cardDataReceiver, Action<string> msgHandler, string saveFilePath)
        {
            _cardDataReceiver = cardDataReceiver;
            _msgHandler = msgHandler;
            _saveFilePath = saveFilePath;
        }

        /// <summary>
        /// 初始化串口
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            //固定值====
            int parg0;
            int parg1;
            GetPargs(out parg0, out parg1);
            //==========
            Byte pCmd = CMD_INIT_PORT;
            int result = 0;
            try
            {
                result = IDCardDriver_SS.UCommand1(ref pCmd, ref parg0, ref parg1, PARAM_PAEG2);
            }
            catch (Exception ex)
            {
                OnReadFailed("设备连接失败," + ex.Message);
                return false;
            }
            InitSucceed = result == CODE_SUCCESS_ID1;
            if (!InitSucceed)
            {
                string errorMsg = string.Empty;
                if (IDCardDriver_SS.ErrorMsg.ContainsKey(result))
                {
                    errorMsg = IDCardDriver_SS.ErrorMsg[result];
                }
                OnReadFailed("连接设备失败：" + errorMsg);
            }
            return InitSucceed;
        }

        /// <summary>
        /// 获取身份证信息
        /// </summary>
        /// <returns></returns>
        public bool ReadIDCard(string oldID = null)
        {
            if (!InitSucceed)
            {
                Open();
                if (!InitSucceed)
                {
                    OnReadFailed("初始化读卡器失败");
                    return false;
                }
            }
            try
            {
                //验证卡
                int nRet = VerifyIDCard();
                //读取卡信息
                nRet = ReadCardInner();// '读卡内信息

                if (nRet != CODE_SUCCESS_ID1 && nRet != CODE_SUCCESS_ID2)
                {
                    //当读有指纹数据的身份证时，返回62172为成功，
                    //读没有指纹数据的身份证时，返回62171为成功。
                    OnReadFailed(nRet);
                    return false;
                }

                IDCardData_SS info = null;
                try
                {
                    info = ReadIDCardData_SSFromFile();
                    //
                    //如果新旧id一致，不再通知
                    if (_cardDataReceiver != null && (info.CardNo != oldID || string.IsNullOrEmpty(oldID)))
                    {
                        _cardDataReceiver(info);
                    }
                }
                catch (Exception ex)
                {
                    OnReadFailed("文件解析失败：" + ex.Message);
                    return false;
                }
            }
            catch (Exception ex)
            {
                OnReadFailed(ex.Message);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 验证卡
        /// </summary>
        /// <returns></returns>
        private int VerifyIDCard()
        {
            byte bCmd = CMD_VERIFY_CARD;//验证信息
            int parg0;
            int parg1;
            GetPargs(out parg0, out parg1);
            return IDCardDriver_SS.UCommand1(ref bCmd, ref parg0, ref parg1, PARAM_PAEG2);// 验证卡
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="parg0"></param>
        /// <param name="parg1"></param>
        private void GetPargs(out int parg0, out int parg1)
        {
            parg0 = 0;
            parg1 = 8811;
        }

        /// <summary>
        /// 读取卡内信息，保存到本地文件
        /// </summary>
        /// <returns></returns>
        private int ReadCardInner()
        {
            //读取卡信息
            byte bCmd = CMD_READ_BASE_INFO;// '0x44 读卡内信息
            int parg0;
            int parg1;
            GetPargs(out parg0, out parg1);
            byte[] parg2 = System.Text.Encoding.Default.GetBytes(_saveFilePath);

            return IDCardDriver_SS.UCommand1(ref bCmd, ref parg0, ref parg1, parg2);// '读卡内信息
        }

        /// <summary>
        /// 从文件中读取卡信息
        /// </summary>
        /// <param name="sSavePath"></param>
        /// <returns></returns>
        private IDCardData_SS ReadIDCardData_SSFromFile()
        {
            IDCardData_SS objIDCardData_SS = new IDCardData_SS();
            using (StreamReader objStreamReader = new StreamReader(_saveFilePath + "wx.txt", System.Text.Encoding.Default))
            {
                objIDCardData_SS.Name = objStreamReader.ReadLine();
                objIDCardData_SS.Sex = objStreamReader.ReadLine();
                objIDCardData_SS.Nation = objStreamReader.ReadLine();
                string birth = objStreamReader.ReadLine();
                //objIDCardData_SS.Birthday = new DateTime(Convert.ToInt32(birth.Substring(0, 4)), Convert.ToInt32(birth.Substring(4, 2)), Convert.ToInt32(birth.Substring(6, 2)));
                objIDCardData_SS.Birthday = birth;
                objIDCardData_SS.Address = objStreamReader.ReadLine();
                //objIDCardData_SS.CardNo = objStreamReader.ReadLine();
                objIDCardData_SS.CardNo = objStreamReader.ReadLine();
                objIDCardData_SS.Department = objStreamReader.ReadLine();
                objIDCardData_SS.StartDate = objStreamReader.ReadLine();
                objIDCardData_SS.EndDate = objStreamReader.ReadLine();
                objIDCardData_SS.AddressEx = objStreamReader.ReadLine();
                objIDCardData_SS.PhotoPath = _saveFilePath + @"zp.bmp";
                objIDCardData_SS.ArrPhotoByte = ImageUtils.ImageToByteArray(objIDCardData_SS.PhotoPath);

                return objIDCardData_SS;
            }
        }

        public void Close()
        {
            //固定值====
            int parg0;
            int parg1;
            GetPargs(out parg0, out parg1);
            //==========
            Byte pCmd = CMD_CLOSE_PORT;
            try
            {
                IDCardDriver_SS.UCommand1(ref pCmd, ref parg0, ref parg1, PARAM_PAEG2);
            }
            catch { }
        }

        private void OnReadFailed(int nResult)
        {
            string msg = "";
            try
            {
                msg = IDCardDriver_SS.ErrorMsg[nResult];
            }
            catch
            {
                msg = "请检查设备是否连接正常或卡是否放置正确";
            }
            OnReadFailed(msg);
        }

        private void OnReadFailed(string msg)
        {
            NotifyMessage(msg);
            Close();
        }

        void NotifyMessage(string msg)
        {
            if (_msgHandler != null)
            {
                _msgHandler(msg);
            }
        }
    }
}
