using MyNet.Components.Handlers;
using MyNet.Components.IDCard;
using MyNet.Components.Misc;
using MyNet.Components.WPF.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace MyNet.Components.WPF.Controls
{
    public class IdcardReaderViewModel : BaseModel
    {
        private string IDCardInfoFilePrefix = Environment.CurrentDirectory + "\\Idcard_";
        private string _idcard;
        public string Idcard
        {
            get { return _idcard; }
            set
            {
                if (_idcard != value)
                {
                    _idcard = value;
                    base.RaisePropertyChanged("Idcard");
                }
            }
        }
        private string _msg;
        public string Msg
        {
            get { return _msg; }
            set
            {
                if (_msg != value)
                {
                    _msg = value;
                    base.RaisePropertyChanged("Msg");
                }
            }
        }

        private IDCardReaderHelper_SS cardReaderHelper;
        public event ValueCangedHandler OnReadCardSucceed;

        private Thread threadReadCard = null;

        private DispatcherTimer timerRead = new DispatcherTimer();

        public IdcardReaderViewModel(int readInverval = 5)
        {
            SetInterval(readInverval);
            timerRead.Tick += (o, e) => { ReadCmd.Execute(null); };
        }

        private ICommand _readCmd;
        public ICommand ReadCmd
        {
            get
            {
                if (_readCmd == null)
                {
                    _readCmd = new DelegateCommand(ReadOnceAction);
                }
                return _readCmd;
            }
        }

        private void ReadOnceAction(object obj)
        {
            DisposeThread();
            threadReadCard = new Thread(() =>
            {
                cardReaderHelper = GetCardReaderHelper();
                if (cardReaderHelper.Open())
                {
                    var oldIdcard = Idcard;
                    bool succeed = cardReaderHelper.ReadIDCard(Idcard);
                    if (succeed && OnReadCardSucceed != null)
                    {
                        OnReadCardSucceed(oldIdcard, Idcard);
                    }
                    cardReaderHelper.Close();
                }
            });
            threadReadCard.IsBackground = true;
            threadReadCard.Start();
        }

        private IDCardReaderHelper_SS GetCardReaderHelper()
        {
            Action<IDCardData_SS> cardDataReceiver = _data =>
            {
                Idcard = _data.CardNo;
            };
            Action<string> msgHandler = _msg =>
            {
                Msg = _msg;
            };

            var helper = new IDCardReaderHelper_SS(cardDataReceiver, msgHandler, IDCardInfoFilePrefix);
            return helper;
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

        public void AutoRead()
        {
            if (!timerRead.IsEnabled)
            {
                ReadCmd.Execute(null);
                timerRead.Start();
            }
        }

        public void SetInterval(int interval)
        {
            timerRead.Interval = new TimeSpan(0, 0, 0, interval);
        }
    }
}
