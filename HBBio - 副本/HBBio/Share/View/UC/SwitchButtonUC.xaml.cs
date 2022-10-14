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

namespace HBBio.Share
{
    /// <summary>
    /// SwitchButtonUC.xaml 的交互逻辑
    /// </summary>
    public partial class SwitchButtonUC : UserControl
    {
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register("IsChecked", typeof(bool), typeof(SwitchButtonUC), new PropertyMetadata(default(bool), OnIsCheckedChanged));
        public event RoutedEventHandler Checked;
        public event RoutedEventHandler UnChecked;

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SwitchButtonUC()
        {
            InitializeComponent();
        }

        private static void OnIsCheckedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            (obj as SwitchButtonUC).OnIsCheckedChanged(args);
        }

        private void OnIsCheckedChanged(DependencyPropertyChangedEventArgs args)
        {
            gridOn.Visibility = IsChecked ? Visibility.Visible : Visibility.Collapsed;
            gridOff.Visibility = IsChecked ? Visibility.Collapsed : Visibility.Visible;
            if (IsChecked && Checked != null)
            {
                Checked(this, new RoutedEventArgs());
            }
            if (!IsChecked && UnChecked != null)
            {
                UnChecked(this, new RoutedEventArgs());
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs args)
        {
            //args.Handled = true;
            IsChecked ^= true; 
            base.OnMouseLeftButtonDown(args);
        }
    }
}
