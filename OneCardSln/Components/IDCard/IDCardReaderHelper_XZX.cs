using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OneCardSln.Components.IDCard
{
    public class IDCardReaderHelper_XZX
    {
        Action<IDCardData_XZX> _cardDataReceiver;
        Action<string> _msgHandler;
        IDCardData_XZX CardMsg = new IDCardData_XZX();
        int _port = 0;
        public IDCardReaderHelper_XZX(Action<IDCardData_XZX> cardDataReceiver, Action<string> msgHandler)
        {
            _cardDataReceiver = cardDataReceiver;
            _msgHandler = msgHandler;
        }

        public bool ReadIDCard(string oldID = null)
        {
            if (_port == 0)
            {
                if (!Open())
                {
                    return false;
                }
            }

            byte[] pucIIN = new byte[4];
            byte[] pucSN = new byte[8];
            int nRet = IDCardDriver_XZX.Syn_OpenPort(_port);
            if (nRet == 0)
            {
                nRet = IDCardDriver_XZX.Syn_GetSAMStatus(_port, 0);
                nRet = IDCardDriver_XZX.Syn_StartFindIDCard(_port, ref pucIIN[0], 0);
                nRet = IDCardDriver_XZX.Syn_SelectIDCard(_port, ref pucSN[0], 0);
                nRet = IDCardDriver_XZX.Syn_ReadMsg(_port, 0, ref CardMsg);
                if (nRet == 0)
                {
                    //如果新旧id一致，不再通知
                    if (_cardDataReceiver != null && (CardMsg.IDCardNo != oldID || string.IsNullOrEmpty(oldID)))
                    {
                        _cardDataReceiver(CardMsg);
                    }
                    IDCardDriver_XZX.Syn_DelPhotoFile();
                }
                else
                {
                    OnReadFailed(nRet);
                    return false;
                }
            }
            else
            {
                OnReadFailed(nRet);
                return false;
            }
            Close();
            return true;
        }

        public bool Open()
        {
            for (_port = 1001; _port < 1017; _port++)
            {
                var result = IDCardDriver_XZX.Syn_OpenPort(_port);
                if (result == 0)
                {
                    result = IDCardDriver_XZX.Syn_GetSAMStatus(_port, 0);
                    if (result == 0)
                    {
                        //连接成功
                        NotifyMessage("读卡器连接成功");
                        return true;
                    }
                }
            }
            OnReadFailed("读卡器未连接");
            return false;
        }

        public void Close()
        {
            IDCardDriver_XZX.Syn_ClosePort(_port);
        }

        private void OnReadFailed(int nResult)
        {
            OnReadFailed(IDCardDriver_XZX.MsgDictionary[nResult]);
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
