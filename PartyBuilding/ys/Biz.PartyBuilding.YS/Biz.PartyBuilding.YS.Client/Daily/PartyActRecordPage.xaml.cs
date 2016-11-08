﻿using Biz.PartyBuilding.YS.Client.Daily.Models;
using MyNet.Client.Pages;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Models;
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
    /// PartyActRecordPage.xaml 的交互逻辑
    /// </summary>
    public partial class PartyActRecordPage : BasePage
    {
        public PartyActRecordPage()
        {
            InitializeComponent();

            CmbModel model = cmbActType.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.task_priority);
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ShowDetail();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            ShowDetail();


        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {

        }

        void ShowDetail()
        {
            new DetailActRecordWindow().ShowDialog();
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            ShowDetail();
        }
    }
}
