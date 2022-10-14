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
    /// CountdownWin.xaml 的交互逻辑
    /// </summary>
    public partial class CountdownWin : Window
    {
        private double m_start = -1;
        private double m_length = -1;
        public double MLength
        {
            set
            {
                m_length = value;
            }
        }

        private System.Windows.Threading.DispatcherTimer m_timer = new System.Windows.Threading.DispatcherTimer();


        public CountdownWin()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            m_start = SystemControlManager.m_curveStatic.MV;

            m_timer.Interval = TimeSpan.FromMilliseconds(500);
            m_timer.Tick += timer1_Tick;
            m_timer.Start();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            m_timer.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (SystemControlManager.s_comconfStatic.m_totalFlow < Math.Abs(Share.DlyBase.DOUBLE))
            {
                txt.Text = "*";
            }
            else
            {
                int temp = Convert.ToInt32((m_length - (SystemControlManager.m_curveStatic.MV - m_start)) / SystemControlManager.s_comconfStatic.m_totalFlow * 60);
                if (temp <= 0)
                {
                    this.Close();
                }
                else
                {
                    txt.Text = temp.ToString();
                }
            }
        }  
    }
}
