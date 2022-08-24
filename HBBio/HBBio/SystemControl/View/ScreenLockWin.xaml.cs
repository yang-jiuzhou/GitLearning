using HBBio.Administration;
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
using System.Windows.Shapes;

namespace HBBio.SystemControl
{
    /// <summary>
    /// ScreenLockWin.xaml 的交互逻辑
    /// </summary>
    public partial class ScreenLockWin : Window
    {
        /// <summary>
        /// 自定义事件，更新紫外设置时触发
        /// </summary>
        public static readonly RoutedEvent MLockEvent =
             EventManager.RegisterRoutedEvent("MScreenLock", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ScreenLockWin));
        public event RoutedEventHandler MScreenLock
        {
            add { AddHandler(MLockEvent, value); }
            remove { RemoveHandler(MLockEvent, value); }
        }


        public ScreenLockWin()
        {
            InitializeComponent();
        }

        private void labLockScreen_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UserLockWin dlg = new UserLockWin(this);
            if (true == dlg.ShowDialog())
            {
                this.Hide();

                RoutedEventArgs args = new RoutedEventArgs(MLockEvent, null);
                RaiseEvent(args);
            }
        }
    }
}
