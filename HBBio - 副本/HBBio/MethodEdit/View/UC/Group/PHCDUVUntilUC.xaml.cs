using HBBio.Communication;
using HBBio.Share;
using System;
using System.Collections;
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

namespace HBBio.MethodEdit
{
    /// <summary>
    /// WashUntilUC.xaml 的交互逻辑
    /// </summary>
    public partial class PHCDUVUntilUC : UserControl
    {
        public static readonly DependencyProperty TextHeaderProperty = DependencyProperty.Register("TextHeader", typeof(string), typeof(PHCDUVUntilUC), new PropertyMetadata(""));
        public string TextHeader
        {
            get
            {
                return (string)GetValue(TextHeaderProperty);
            }
            set
            {
                SetValue(TextHeaderProperty, value);
            }
        }

        public static readonly DependencyProperty VisibilityHeaderProperty = DependencyProperty.Register("VisibilityHeader", typeof(Visibility), typeof(PHCDUVUntilUC), new PropertyMetadata(Visibility.Collapsed));
        public Visibility VisibilityHeader
        {
            get
            {
                return (Visibility)GetValue(VisibilityHeaderProperty);
            }
            set
            {
                SetValue(VisibilityHeaderProperty, value);
            }
        }

        public PHCDUVUntilUC()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cboxMonitor.ItemsSource = EnumMonitorInfo.NameList;

            cboxJudge.ItemsSource = Share.ReadXaml.GetEnumList<EnumJudge>();
        }
    }
}
