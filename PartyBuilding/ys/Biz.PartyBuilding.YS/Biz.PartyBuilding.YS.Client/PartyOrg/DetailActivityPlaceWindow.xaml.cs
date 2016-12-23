using Biz.PartyBuilding.YS.Client.Daily;
using Biz.PartyBuilding.YS.Client.Models;
using Biz.PartyBuilding.YS.Client.PartyOrg.Models;
using Microsoft.Win32;
using MyNet.Client.Public;
using MyNet.Components;
using MyNet.Components.Http;
using MyNet.Components.Result;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Extension;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Windows;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Biz.PartyBuilding.YS.Client.PartyOrg
{
    /// <summary>
    /// DetailActivityPlaceWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DetailActivityPlaceWindow : BaseWindow
    {
        PartyActAreaModel _model;
        private DetailActivityPlaceWindow()
        {
            InitializeComponent();

            _model = this.DataContext as PartyActAreaModel;

            //ctlImage.ImgFile = "pack://application:,,,/Biz.PartyBuilding.YS.Client;component/Resources/村级活动场所.jpg";
        }

        public DetailActivityPlaceWindow(InfoOptType type, PartyActAreaModel area = null)
            : this()
        {
            _type = type;

            if (area != null)
            {
                area.CopyTo(_model);
                //设置图片
                if (area.pic != null && area.pic.Count > 0)
                {
                    System.Drawing.Image img = ImageUtils.Base64Decode(area.pic[0]);
                    string file = AppDomain.CurrentDomain.BaseDirectory + "temp.jpg";
                    img.Save(file, ImageFormat.Jpeg);
                    img.Dispose();
                    img = null;

                    ctlImage.ImgFile = file;
                }
            }
        }

        ICommand _saveCmd;
        public ICommand SaveCmd
        {
            get
            {
                if (_saveCmd == null)
                {
                    _saveCmd = new DelegateCommand(SaveAction);
                }
                return _saveCmd;
            }
        }

        InfoOptType _type;
        void SaveAction(object parameter)
        {
            if (this._type == InfoOptType.View)
            {
                base.CloseCmd.Execute(null);
                return;
            }
            if (_type == InfoOptType.InsertOrUpdate)
            {
                _model.pic = new List<string>();
                var data = ctlImage.Base64Data;
                if (!string.IsNullOrEmpty(data))
                {
                    _model.pic.Add(data);
                }
            }
            //保存
            var url = ApiUtils.GetApiUrl(PartyBuildingApiKeys.AreaSave, PartyBuildingApiKeys.Key_ApiProvider_Party);
            var rst = HttpUtils.PostResult(url, _model);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Save, rst.msg);
                return;
            }
            this.DialogResult = true;
            base.CloseCmd.Execute(null);
        }
    }
}
