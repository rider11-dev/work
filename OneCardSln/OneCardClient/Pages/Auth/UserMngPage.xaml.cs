using Newtonsoft.Json;
using OneCardSln.OneCardClient.Models.Auth;
using System;
using System.Collections.Generic;
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
using OneCardSln.Components.WPF.Extension;
using OneCardSln.Components;
using OneCardSln.OneCardClient.Public;
using OneCardSln.OneCardClient.Models;
using OneCardSln.Components.Result;
using Newtonsoft.Json.Linq;
using OneCardSln.Components.WPF.Controls;

namespace OneCardSln.OneCardClient.Pages.Auth
{
    /// <summary>
    /// UserMngPage.xaml 的交互逻辑
    /// </summary>
    public partial class UserMngPage : Page
    {
        public UserMngPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitDataGrid();
            //LoadData();
        }

        private void InitDataGrid()
        {
            //TODO是否有必要继承DataGrid，默认包含下面设置
            dgUsers.AddCheckBoxCol();
            dgUsers.ShowRowNumber();

            ctlPagination.QueryHandler = (e) =>
            {
                LoadData(e);
            };
            ctlPagination.PageIndex = 1;
            ctlPagination.Bind();
        }

        private void LoadData(PagingArgs page)
        {
#if DEBUG
            LoadTestData();
#endif
            var rst = HttpHelper.GetResultByPost(ApiHelper.GetApiUrl(ApiKeys.GetUsrByPage),
                new
                {
                    pageIndex = page.PageIndex,
                    pageSize = page.PageSize
                },
                Context.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.GetUsrs, rst.msg);
                return;
            }
            if (rst.data != null && rst.data.total != null)
            {
                page.RecordsCount = (int)rst.data.total;
                if (page.RecordsCount == 0)
                {
                    page.PageCount = 0;
                    page.PageIndex = 1;
                    return;
                }
                page.PageCount = Convert.ToInt32(Math.Ceiling(page.RecordsCount * 1.0 / page.PageSize));
                var usrs = JsonConvert.DeserializeObject<IEnumerable<UserViewModel>>(((JArray)rst.data.rows).ToString());
                dgUsers.ItemsSource = usrs;
            }
        }

        private void LoadTestData()
        {
            string json = "[{\"user_id\":\"test\",\"user_name\":\"test\",\"user_pwd\":null,\"user_idcard\":\"111111111111111111\",\"user_truename\":\"测试\",\"user_regioncode\":\"372924\",\"user_remark\":\"阿斯蒂芬\",\"user_creator\":\"admin\"},{\"user_id\":\"admin\",\"user_name\":\"admin\",\"user_pwd\":null,\"user_idcard\":\"372924198708265138\",\"user_truename\":\"张鹏飞\",\"user_regioncode\":\"372924\",\"user_remark\":\"管理员\r\n继续加油！！！\",\"user_creator\":null},{\"user_id\":\"06a4946690e649709cbb7258e4d20298\",\"user_name\":\"zpf1\",\"user_pwd\":null,\"user_idcard\":\"222222222222222222\",\"user_truename\":\"张鹏飞2\",\"user_regioncode\":\"372924\",\"user_remark\":null,\"user_creator\":\"admin\"}]";
            var data = JsonConvert.DeserializeObject<IEnumerable<UserViewModel>>(json);
            dgUsers.ItemsSource = data;
        }
    }
}
