using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

namespace Client
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ILoginViewModel model;
        public MainWindow()
        {
            InitializeComponent();

            //model = DynamicViewModelBuilder.GetInstance<ILoginViewModel>(typeof(BaseModel), () =>
            //{
            //    var jsonFile = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('/', '\\') + "/vmLoginAttributes.json";
            //    var data = File.ReadAllText(jsonFile);
            //    return JsonConvert.DeserializeObject<IEnumerable<PropCustomAttrUnit>>(data);
            //});

            model = new LoginModel();

            this.DataContext = model;

            model.username = "test";
            model.CanValidate = true;
        }
    }
}
