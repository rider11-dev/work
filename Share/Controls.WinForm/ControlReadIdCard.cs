using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyNet.Components;
using System.Threading;
using System.IO;
using MyNet.Components.IDCard;

namespace MyNet.Components.WinForm
{
    public partial class ControlReadIdCard : UserControl
    {
        IDCardReaderHelper_SS cardReaderHelper;
        Thread threadReadCard = null;
        /// <summary>
        /// 读卡成功事件
        /// </summary>
        public event ValueCangedHandler OnReadCardSucceed;
        string oldCardNumber = string.Empty;
        /// <summary>
        /// 读卡间隔
        /// </summary>
        const int ReadCardInterval = 5000;
        public bool ReadCardSucceed
        {
            get
            {
                return txtIDCard.Text.Length > 0;
            }
        }

        string IDCardInfoFilePrefix
        {
            get
            {
                return System.Environment.CurrentDirectory + "\\IDCard_";
            }
        }

        public ControlReadIdCard()
        {
            InitializeComponent();

        }

        private IDCardReaderHelper_SS GetCardReaderHelper()
        {
            Action<IDCardData_SS> cardDataReceiver = _data =>
            {
                txtIDCard.Text = _data.CardNo;
                txtName.Text = _data.Name;

                if (picIDCard.Image != null)
                {
                    picIDCard.Image.Dispose();
                    picIDCard.Image = null;
                }

                using (MemoryStream ms = new MemoryStream(_data.ArrPhotoByte))
                {
                    using (Image img = Image.FromStream(ms))
                    {
                        picIDCard.Image = ImageUtils.CopyImage(img, img.Width, img.Height);
                    }
                }
            };
            Action<string> msgHandler = _msg =>
            {
                lblMsgIDCard.Text = _msg;
            };
            var helper = new IDCardReaderHelper_SS(data =>
              {

                  if (this.InvokeRequired)
                  {
                      this.Invoke(cardDataReceiver, data);
                  }
                  else
                  {
                      cardDataReceiver(data);
                  }
              }, msg =>
              {
                  if (this.InvokeRequired)
                  {
                      this.Invoke(msgHandler, msg);
                  }
                  else
                  {
                      msgHandler(msg);
                  }
              }, IDCardInfoFilePrefix);
            return helper;
        }

        private string GetCurrentCardNumber()
        {
            Func<string> funcGetCurrentCardNumber = new Func<string>(() =>
            {
                return txtIDCard.Text;
            });
            if (this.InvokeRequired)
            {
                var rst = this.Invoke(funcGetCurrentCardNumber);
                return Convert.ToString(rst);
            }
            else
            {
                return funcGetCurrentCardNumber();
            }
        }

        private void btnReadIDCard_Click(object sender, EventArgs e)
        {
            Reset();
            DisposeThread();
            threadReadCard = new Thread(() =>
            {
                cardReaderHelper = GetCardReaderHelper();
                if (cardReaderHelper.Open())
                {
                    //while (true)
                    {
                        bool succeed = cardReaderHelper.ReadIDCard(GetCurrentCardNumber());
                        if (succeed && OnReadCardSucceed != null)
                        {
                            OnReadCardSucceed(oldCardNumber, GetCurrentCardNumber());
                        }
                        oldCardNumber = GetCurrentCardNumber();//更新旧值
                        //Thread.Sleep(ReadCardInterval);
                    }
                    cardReaderHelper.Close();
                }
            });
            threadReadCard.IsBackground = true;
            threadReadCard.Start();
        }

        public void Reset()
        {
            txtIDCard.Text = "";
            txtName.Text = "";
            if (picIDCard.Image != null)
            {
                picIDCard.Image.Dispose();
                picIDCard.Image = null;
            }
            lblMsgIDCard.Text = "";
        }

        private void DisposeThread()
        {
            if (threadReadCard != null)
            {
                threadReadCard.Abort();
                threadReadCard = null;
            }
            if (cardReaderHelper != null)
            {
                cardReaderHelper.Close();
            }
        }
    }
}
