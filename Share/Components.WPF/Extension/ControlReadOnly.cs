using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyNet.Components.WPF.Extension
{
    public class ButtonCanReadOnly : DependencyObject
    {
        public static readonly DependencyProperty NameProperty = DependencyProperty.Register("ReadOnly", typeof(bool), typeof(ButtonCanReadOnly), new PropertyMetadata(false));
    }
}
