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

namespace HBBio.Communication
{
    /// <summary>
    /// CalOEMPHWin.xaml 的交互逻辑
    /// </summary>
    public partial class CalOEMPHWin : Window
    {
        public TCPPHCDOEM MItemTCP { get; set; }
        public ComPHCDOEM MItemCom { get; set; }
        private System.Windows.Threading.DispatcherTimer m_timer = new System.Windows.Threading.DispatcherTimer();
        private bool m_flag = false;


        /// <summary>
        /// 构造函数
        /// </summary>
        public CalOEMPHWin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (null != MItemTCP || null != MItemCom)
            {
                m_timer.Interval = TimeSpan.FromMilliseconds(500);
                m_timer.Tick += timer1_Tick;
                m_timer.Start();
            }
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            m_timer.Stop();
        }

        /// <summary>
        /// 定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (null != MItemCom)
            {
                doublePH.Value = MItemCom.MpHItem.m_pHGet;
                doubleTT.Value = MItemCom.MTTItem.m_tempGet;
            }
            else if (null != MItemTCP)
            {
                doublePH.Value = MItemTCP.MpHItem.m_pHGet;
                doubleTT.Value = MItemTCP.MTTItem.m_tempGet;
            }   
        }

        /// <summary>
        /// 校准点1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnS1_Click(object sender, RoutedEventArgs e)
        {
            btnS1.IsEnabled = false;
            btnS2.IsEnabled = true;
            btnS3.IsEnabled = false;

            if (null != MItemCom)
            {
                MItemCom.MPHVal = (double)doubleSS1.Value;
            }
            else if (null != MItemTCP)
            {
                MItemTCP.MPHVal = (double)doubleSS1.Value;
            }
        }

        /// <summary>
        /// 校准点2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnS2_Click(object sender, RoutedEventArgs e)
        {
            btnS1.IsEnabled = false;
            btnS2.IsEnabled = false;
            btnS3.IsEnabled = true;

            if (null != MItemCom)
            {
                MItemCom.MPHVal = (double)doubleSS2.Value;
            }
            else if (null != MItemTCP)
            {
                MItemTCP.MPHVal = (double)doubleSS2.Value;
            }
        }

        /// <summary>
        /// 校准点3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnS3_Click(object sender, RoutedEventArgs e)
        {
            btnS1.IsEnabled = false;
            btnS2.IsEnabled = false;
            btnS3.IsEnabled = false;

            if (null != MItemCom)
            {
                MItemCom.MPHVal = (double)doubleSS3.Value;
            }
            else if (null != MItemTCP)
            {
                MItemTCP.MPHVal = (double)doubleSS3.Value;
            }
        }

        /// <summary>
        /// 开始校准
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalStart_Click(object sender, RoutedEventArgs e)
        {
            btnS1.IsEnabled = true;
            btnS2.IsEnabled = false;
            btnS3.IsEnabled = false;

            m_flag = true;
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = m_flag;
        }
    }
}
