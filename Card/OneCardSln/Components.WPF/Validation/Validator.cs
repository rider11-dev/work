using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace MyNet.Components.WPF.Validation
{
    public abstract class Validator : FrameworkElement
    {
        static Validator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Validator), new FrameworkPropertyMetadata(typeof(Validator)));
        }

        public virtual string ErrorMessage { get { return string.Empty; } }
        public abstract bool InitialValidation();
        public FrameworkElement ElementName
        {
            get { return (FrameworkElement)GetValue(ElementNameProperty); }
            set { SetValue(ElementNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ElementName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ElementNameProperty =
            DependencyProperty.Register("ElementName", typeof(FrameworkElement), typeof(Validator), new PropertyMetadata(null));


        public object Source
        {
            get { return (object)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(object), typeof(Validator), new UIPropertyMetadata(new PropertyChangedCallback(ValidPropertyPropertyChanged)));

        private static void ValidPropertyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var validator = d as Validator;
            if (validator != null)
                validator.SetSourceFromProperty();
            if (string.IsNullOrEmpty(e.NewValue.ToString()))
            {
                if (validator != null)
                {
                    validator.IsValid = validator.InitialValidation();
                    if (validator.ElementName.DataContext != null)
                        validator.ShowToolTip();
                    validator.IsValid = false;
                }
            }
        }

        private void ShowToolTip()
        {
            if (IsValid)
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1.5);
                _toolTip = new ToolTip();
                _toolTip.StaysOpen = true;
                _toolTip.PlacementTarget = ElementName;
                _toolTip.Placement = PlacementMode.Right;

                _toolTip.Content = ErrorMessage;
                _toolTip.IsOpen = true;
                timer.Tick += (sender, args) =>
                {
                    _toolTip.IsOpen = false;
                    timer.Stop();
                };
                timer.Start();
            }

        }
        private void SetSourceFromProperty()
        {
            var expression = this.GetBindingExpression(SourceProperty);
            if (expression != null && this.ElementName == null)
                this.SetValue(Validator.ElementNameProperty, expression.DataItem as FrameworkElement);

        }

        private ToolTip _toolTip;
        private DispatcherTimer timer;

        public bool IsValid { get; set; }
    }
}
