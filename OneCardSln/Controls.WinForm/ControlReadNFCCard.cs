using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Configuration;
using OneCardSln.Components.NFC;

namespace OneCardSln.Components.Controls.WinForm
{
    public partial class ControlReadNFCCard : UserControl
    {
        /// <summary>
        /// 读卡成功事件
        /// </summary>
        public event ValueCangedHandler OnReadCardSucceed;
        NFCCardReaderHelper readM1CardHelper = null;
        Thread threadReadCard = null;
        /// <summary>
        /// 读卡时是否响铃提示
        /// </summary>
        bool Beep = false;
        /// <summary>
        /// 读卡间隔
        /// </summary>
        const int ReadCardInterval = 1000;
        public bool ReadM1CardSucceed
        {
            get
            {
                return txtCardNumber.Text.Length > 0;
            }
        }

        string oldCardNumber = string.Empty;
        public ControlReadNFCCard(bool beep = false)
        {
            InitializeComponent();

            this.Beep = beep;
        }

        public void ReadM1Card()
        {
            DisposeThread();
            txtCardNumber.Text = "";
            threadReadCard = new Thread(() =>
            {
                Action<string> cardNumberReceiver = number => { txtCardNumber.Text = number; };
                Action<string> msgHandler = msg => { lblMsg.Text = msg; };
                readM1CardHelper = new NFCCardReaderHelper((cardNum) =>
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(cardNumberReceiver, cardNum);
                    }
                    else
                    {
                        cardNumberReceiver(cardNum);
                    }
                }, (msg) =>
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(msgHandler, msg);
                    }
                    else
                    {
                        msgHandler(msg);
                    }
                });
                if (readM1CardHelper.OpenDevice())
                {
                    while (true)
                    {
                        if (Disposing)
                        {
                            return;
                        }
                        bool succeed = readM1CardHelper.ReadM1Card(Beep, GetCurrentCardNumber());
                        if (succeed && OnReadCardSucceed != null)
                        {
                            OnReadCardSucceed(oldCardNumber, GetCurrentCardNumber());
                        }
                        oldCardNumber = GetCurrentCardNumber();//更新旧值
                        if (!ReadM1CardSucceed)
                        {
                            Thread.Sleep(ReadCardInterval);
                        }
                    }
                }
            });
            threadReadCard.IsBackground = true;
            threadReadCard.Start();
        }


        private string GetCurrentCardNumber()
        {
            Func<string> funcGetCurrentCardNumber = new Func<string>(() =>
            {
                return txtCardNumber.Text;
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

        private void DisposeThread()
        {
            if (threadReadCard != null)
            {
                threadReadCard.Abort();
                threadReadCard = null;
            }
            if (readM1CardHelper != null)
            {
                readM1CardHelper.CloseDevice();
            }
        }

        private void ControlReadCard_Load(object sender, EventArgs e)
        {

        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            ReadM1Card();
        }
    }
}
