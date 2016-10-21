using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyNet.Components.NFC
{
    public class NFCCardReaderHelper
    {
        int _nHandle = -1;
        Action<string> _cardNumberReceiver;
        Action<string> _msgHandler;
        LotusCardParamStruct sttLotusCardParam;

        public NFCCardReaderHelper(Action<string> cardNumberReceiver, Action<string> msgHandler)
        {
            _cardNumberReceiver = cardNumberReceiver;
            _msgHandler = msgHandler;

            Init();
        }

        void Init()
        {
            sttLotusCardParam = new LotusCardParamStruct();
            sttLotusCardParam.arrCardNo = new byte[8];
            sttLotusCardParam.arrBuffer = new byte[64];
            sttLotusCardParam.arrKeys = new byte[64];
            sttLotusCardParam.arrCosResultBuffer = new byte[256];

            sttLotusCardParam.arrBuffer[0] = 0x00;
            sttLotusCardParam.arrBuffer[1] = 0x01;
            sttLotusCardParam.arrBuffer[2] = 0x02;
            sttLotusCardParam.arrBuffer[3] = 0x03;
            sttLotusCardParam.arrBuffer[4] = 0x04;
            sttLotusCardParam.arrBuffer[5] = 0x05;
            sttLotusCardParam.arrBuffer[6] = 0x06;
            sttLotusCardParam.arrBuffer[7] = 0x07;
            sttLotusCardParam.arrBuffer[8] = 0x08;
            sttLotusCardParam.arrBuffer[9] = 0x09;
            sttLotusCardParam.arrBuffer[10] = 0x0a;
            sttLotusCardParam.arrBuffer[11] = 0x0b;
            sttLotusCardParam.arrBuffer[12] = 0x0c;
            sttLotusCardParam.arrBuffer[13] = 0x0d;
            sttLotusCardParam.arrBuffer[14] = 0x0e;
            sttLotusCardParam.arrBuffer[15] = 0x0f;
        }

        public bool OpenDevice()
        {
            _nHandle = CLotusCardDriver.LotusCardOpenDevice("", 0, 0, 0, 0, null);
            if (-1 == _nHandle)
            {
                NotifyMessage("设备打开失败，请确认读卡器是否连接到电脑。");
                return false;
            }
            return true;
        }

        public void CloseDevice()
        {
            if (_nHandle != -1)
            {
                CLotusCardDriver.LotusCardCloseDevice(_nHandle);
            }
            _nHandle = -1;
        }

        public bool ReadM1Card(bool beep, string oldNumber = "")
        {
            Thread.Sleep(2000);//等待2s

            if (_nHandle == -1)
            {
                return false;
            }

            int bResult = 0;
            int nRequestType = CLotusCardDriver.RT_NOT_HALT;
            long lngCardNo = 0;
            String strLog;

            //开启声音提醒
            if (beep)
            {
                bResult = CLotusCardDriver.LotusCardBeep(_nHandle, 10);
                if (bResult != 1)
                {
                    NotifyMessage("设置读卡器声音提醒失败！");
                    return false;
                }
            }

            try
            {
                bResult = CLotusCardDriver.LotusCardGetCardNo(_nHandle, nRequestType, ref sttLotusCardParam);
            }
            catch (Exception ex)
            {
                return false;
            }
            if (bResult != 1)
            {
                //NotifyM1CardNumber(string.Empty);
                NotifyMessage("寻卡失败，请将一卡通放置在读卡器上！");
                return false;
            }

            lngCardNo = (sttLotusCardParam.arrCardNo[3] << 24 | sttLotusCardParam.arrCardNo[2] << 16 | sttLotusCardParam.arrCardNo[1] << 8 | sttLotusCardParam.arrCardNo[0]) & 0xffffffff;
            strLog = Convert.ToString(lngCardNo, 16).ToUpper();//卡号
            //strLog = "test";
            if (oldNumber != strLog || string.IsNullOrEmpty(oldNumber))
            {
                NotifyM1CardNumber(strLog);
            }
            NotifyMessage("读卡成功");
            return true;
        }
        void NotifyMessage(string msg)
        {
            if (_msgHandler != null)
            {
                _msgHandler(msg);
            }
        }

        void NotifyM1CardNumber(string cardNumber)
        {
            if (_cardNumberReceiver != null)
            {
                _cardNumberReceiver(cardNumber);
            }
        }
    }
}
