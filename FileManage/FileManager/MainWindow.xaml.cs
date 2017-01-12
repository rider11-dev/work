using Microsoft.Win32;
using MyNet.Components.Extensions;
using MyNet.Components.WPF.Extension;
using MyNet.Components.WPF.Windows;
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

namespace FileManager
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : BaseWindow
    {
        MainViewModel _model;
        public MainWindow()
        {
            _model = new MainViewModel();
            this.DataContext = _model;
            InitializeComponent();

            dg.ShowRowNumber();

            dg.MouseDoubleClick += Dg_MouseDoubleClick;
        }

        private void Dg_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!dg.CurrentCell.Column.Header.ToString().Contains("logo"))
            {
                return;
            }

            e.Handled = true;
            OpenFileDialog dia = new OpenFileDialog();
            dia.Filter = "Image Files|*.jpg;*.png;*.bmp";
            var rst = dia.ShowDialog();
            if (rst.HasValue == false || rst == false)
            {
                return;
            }
            (dg.CurrentCell.Item as FileContent).logo = dia.FileName;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (_model.MngFile.IsNotEmpty())
            {
                _model.ParseMngFileCmd.Execute(null);
            }
        }


        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                base.Resize();
                e.Handled = true;
            }
        }
    }
}
