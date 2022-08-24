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
    /// UVWin.xaml 的交互逻辑
    /// </summary>
    public partial class UVWin : Window
    {
        /// <summary>
        /// 自定义事件，更新紫外设置时触发
        /// </summary>
        public static readonly RoutedEvent MUpdateEvent =
             EventManager.RegisterRoutedEvent("MUpdateUV", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UVWin));
        public event RoutedEventHandler MUpdateUV
        {
            add { AddHandler(MUpdateEvent, value); }
            remove { RemoveHandler(MUpdateEvent, value); }
        }

        private UVValue m_item = null;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="uvValue"></param>
        public UVWin(Window parent, UVValue uvValue, UVItem uvItem)
        {
            InitializeComponent();

            this.Owner = parent;

            m_item = uvValue;
            ucUV.DataContext = new UVValueVM() { MItem = Share.DeepCopy.DeepCopyByXml(uvValue) };

            foreach (FrameworkElement it in gridRead.Children)
            {
                if (it is TextBlock)
                {
                    continue;
                }
                it.DataContext = uvItem;
            }

            if (Visibility.Visible == StaticValue.s_waveVisible3)
            {
                gridRead.RowDefinitions[2].Height = new GridLength(0);
            }
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
            string log = ucUV.GetLog(m_item, true);
            if (!string.IsNullOrEmpty(log))
            {
                RoutedEventArgs args = new RoutedEventArgs(MUpdateEvent, m_item);
                RaiseEvent(args);

                AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, log);
            }
        }
    }
}
