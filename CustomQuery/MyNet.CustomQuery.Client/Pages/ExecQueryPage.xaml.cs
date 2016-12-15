﻿using MyNet.Client.Pages;
using MyNet.Components.Extensions;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Extension;
using MyNet.Components.WPF.Windows;
using MyNet.CustomQuery.Client.Models;
using MyNet.CustomQuery.Client.Models.ExecQuery;
using MyNet.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace MyNet.CustomQuery.Client.Pages
{
    /// <summary>
    /// ExecQueryPage.xaml 的交互逻辑
    /// </summary>
    public partial class ExecQueryPage : BasePage
    {
        ExecQueryModel model = null;
        public ExecQueryPage()
        {
            InitializeComponent();

            model = this.DataContext as ExecQueryModel;
            model.ViewSrcBaseFields = this.FindResource("viewSrcBaseFields") as CollectionViewSource;

            model.TableSelector.ViewRelFields = this.FindResource("viewRelFields") as CollectionViewSource;

            model.SelFieldsSelector.ViewSrcSelFields = this.FindResource("viewSrcSelFields") as CollectionViewSource;
            model.SelFieldsSelector.ViewSelectedFields = this.FindResource("viewSelectedFields") as CollectionViewSource;

            model.FilterFieldsSelector.ViewSrcFilterFields = this.FindResource("viewSrcFilterFields") as CollectionViewSource;
            model.FilterFieldsSelector.DgColConditionType = dgColConditionType;
            model.FilterFieldsSelector.DgColFieldType = dgColFieldType;
            model.FilterFieldsSelector.DgColValue = dgColValue;

            model.SortFieldsSelector.ViewSrcSortFields = this.FindResource("viewSrcSortFields") as CollectionViewSource;
            model.SortFieldsSelector.ViewSortFields = this.FindResource("viewSortFields") as CollectionViewSource;

            model.QueryExecutor.DgResults = dgResults;
            model.QueryExecutor.CtlPage = ctlPagination;


        }

        private void ExecQueryPage_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private void Init()
        {
            dgResults.CanClearSort();

            dgColValue.CellEditingTemplate = this.FindResource("txtCellEditorTemplate") as DataTemplate;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //datagrid的行改变时也会触发该事件，故这里过滤一下
            if (e.OriginalSource is TabControl)
            {
                if (!e.AddedItems.IsEmpty())
                {
                    var tab = e.AddedItems[0] as TabItem;
                    if (tab == tabSelField || tab == tabFilter || tab == tabSort)
                    {
                        model.FilterBaseFieldsSrc();
                    }
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dd = dgRelFields.ContextMenu;
        }
    }
}
