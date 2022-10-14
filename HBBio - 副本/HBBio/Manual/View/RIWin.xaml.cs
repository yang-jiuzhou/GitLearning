using HBBio.Communication;
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

namespace HBBio.Manual
{
    /// <summary>
    /// RIWin.xaml 的交互逻辑
    /// </summary>
    public partial class RIWin : Window
    {
        /// <summary>
        /// 自定义事件，更新紫外设置时触发
        /// </summary>
        public static readonly RoutedEvent MUpdateEvent =
             EventManager.RegisterRoutedEvent("MUpdateRI", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(RIWin));
        public event RoutedEventHandler MUpdateRI
        {
            add { AddHandler(MUpdateEvent, value); }
            remove { RemoveHandler(MUpdateEvent, value); }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="uvValue"></param>
        public RIWin(Window parent, RIValue riValue)
        {
            InitializeComponent();

            this.Owner = parent;

            ucRI.MRIValueOld = riValue;
        }

        /// <summary>
        /// 拖拽窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 应用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            string log = ucRI.GetLog();
            if (!string.IsNullOrEmpty(log))
            {
                RoutedEventArgs args = new RoutedEventArgs(MUpdateEvent, ucRI.MRIValueNew);
                RaiseEvent(args);

                AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, log);
            }
        }
    }
}
