using Biz.PartyBuilding.YS.Client.Daily.Models;
using Biz.PartyBuilding.YS.Client.Models;
using MyNet.Client.Pages;
using MyNet.Client.Public;
using MyNet.Components;
using MyNet.Components.Extensions;
using MyNet.Components.Http;
using MyNet.Components.Result;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Biz.PartyBuilding.YS.Client.Daily
{
    /// <summary>
    /// InfoReleasePage.xaml 的交互逻辑
    /// </summary>
    public partial class InfoReleasePage : BasePage
    {
        public InfoReleasePage()
        {
            InitializeComponent();

            CmbModel model = cmbInfoState.DataContext as CmbModel;
            model.Bind(DailyContext.notice_state);
        }

        ICommand _searchCmd;
        public ICommand SearchCmd
        {
            get
            {
                if (_searchCmd == null)
                {
                    _searchCmd = new DelegateCommand(SearchAction);
                }

                return _searchCmd;
            }
        }

        void SearchAction(object parameter)
        {

        }

        ICommand _viewCmd_Rec;
        ICommand _viewCmd_Sent;
        ICommand _addCmd_Sent;
        ICommand _editCmd_Sent;
        ICommand _issueCmd_Sent;
        ICommand _viewAttachCmd;

        public ICommand ViewCmd_Rec
        {
            get
            {
                if (_viewCmd_Rec == null)
                {
                    _viewCmd_Rec = new DelegateCommand(ViewCmd_RecAction);
                }

                return _viewCmd_Rec;
            }
        }

        public ICommand ViewCmd_Sent
        {
            get
            {
                if (_viewCmd_Sent == null)
                {
                    _viewCmd_Sent = new DelegateCommand(View_SentAction);
                }

                return _viewCmd_Sent;
            }
        }
        public ICommand AddCmd_Sent
        {
            get
            {
                if (_addCmd_Sent == null)
                {
                    _addCmd_Sent = new DelegateCommand(Add_SentAction);
                }

                return _addCmd_Sent;
            }
        }
        public ICommand EditCmd_Sent
        {
            get
            {
                if (_editCmd_Sent == null)
                {
                    _editCmd_Sent = new DelegateCommand(Edit_SentAction);
                }

                return _editCmd_Sent;
            }
        }
        public ICommand IssueCmd_Sent
        {
            get
            {
                if (_issueCmd_Sent == null)
                {
                    _issueCmd_Sent = new DelegateCommand(Issue_SentAction);
                }

                return _issueCmd_Sent;
            }
        }
        public ICommand ViewAttachCmd
        {
            get
            {
                if (_viewAttachCmd == null)
                {
                    _viewAttachCmd = new DelegateCommand(ViewAttachAction);
                }

                return _viewAttachCmd;
            }
        }
        void ViewAttachAction(object parameter)
        {
            var notice = (InfoEntity)parameter;
            if (notice == null || string.IsNullOrEmpty(notice.attach))
            {
                return;
            }

            var fullPath = "";
            if (FileExtension.GetFileFullPath(AppDomain.CurrentDomain.BaseDirectory, notice.attach, out fullPath))
            {
                Process.Start(fullPath);
            }
        }
        void Issue_SentAction(object parameter)
        {
            var info = dg_Sent.SelectedItem as InfoModel;
            if (info == null)
            {
                return;
            }
            var url = ApiUtils.GetApiUrl(PartyBuildingApiKeys.InfoRelease, PartyBuildingApiKeys.Key_ApiProvider_Party);
            var rst = HttpUtils.PostResult(url, new { id = info.id });
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Save, rst.msg);
                return;
            }
            GetInfos();
        }

        void Edit_SentAction(object parameter)
        {
            ShowDetailWindow(InfoOptType.InsertOrUpdate);
        }
        void Add_SentAction(object parameter)
        {
            ShowDetailWindow(InfoOptType.InsertOrUpdate);

            GetInfos();
        }
        void View_SentAction(object parameter)
        {
            var info = dg_Sent.SelectedItem as InfoModel;
            if (info == null)
            {
                return;
            }
            ShowDetailWindow(InfoOptType.View, info);
        }
        void ViewCmd_RecAction(object parameter)
        {
            var info = dg_Rec.SelectedItem as InfoModel;
            if (info == null)
            {
                return;
            }
            ShowDetailWindow(InfoOptType.View, info);
            var url = ApiUtils.GetApiUrl(PartyBuildingApiKeys.InfoRead, PartyBuildingApiKeys.Key_ApiProvider_Party);
            var rst = HttpUtils.PostResult(url, new { id = info.id });
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Save, rst.msg);
                return;
            }
            GetInfos();
        }

        void ShowDetailWindow(InfoOptType type, InfoModel info = null)
        {
            bool rst = (bool)new DetailInfoWindow(type, info).ShowDialog();
            if (rst == true)
            {
                GetInfos();
            }
        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            GetInfos();
        }


        void GetInfos()
        {
            var rst = HttpUtils.GetResult(ApiUtils.GetApiUrl(PartyBuildingApiKeys.InfoGet, PartyBuildingApiKeys.Key_ApiProvider_Party));
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Search, rst.msg);
                return;
            }
            if (rst.data != null && rst.data.infos != null)
            {
                var infos = JsonConvert.DeserializeObject<IEnumerable<InfoModel>>(((JArray)rst.data.infos).ToString());
                if (infos != null && infos.Count() > 0)
                {
                    dg_Rec.ItemsSource = infos.Where(i => i.state == "已发布");
                    dg_Sent.ItemsSource = infos;
                }
            }
        }
        private void btnExport_Rec_Click(object sender, RoutedEventArgs e)
        {
            MyNet.Components.WPF.Misc.ExcelHelper.Export(dg_Rec, "已收信息");
        }

        private void btnExport_Sent_Click(object sender, RoutedEventArgs e)
        {
            MyNet.Components.WPF.Misc.ExcelHelper.Export(dg_Sent, "已发信息");

        }
    }
}
