﻿using MyNet.Client.Public;
using MyNet.Components;
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

namespace MyNet.Client.Pages
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : BasePage
    {
        public MainPage()
            : base()
        {
            InitializeComponent();

            //lblWelcome.Content = string.Format("欢迎进入{0}", MyContext.SysName);

            frameMain.Source = new Uri(AppSettingHelper.Get("maincontent"), UriKind.Relative);
        }
    }
}