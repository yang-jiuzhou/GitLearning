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

namespace HBBio.Share
{
    /// <summary>
    /// MessageBoxWin.xaml 的交互逻辑
    /// </summary>
    public partial class MessageBoxWin : Window
    {
        public string MText
        {
            set
            {
                txtInfo.Text = value;
            }
        }

        public string MTitle
        {
            set
            {
                group.Header = value;
            }
        }

        private MessageBoxButton m_button = MessageBoxButton.OKCancel;
        public MessageBoxButton MButton
        {
            get
            {
                return m_button;
            }
            set
            {
                m_button = value;
                switch(value)
                {
                    case MessageBoxButton.OKCancel:
                    case MessageBoxButton.OK:
                        grid.RowDefinitions[2].Height = GridLength.Auto;
                        grid.RowDefinitions[3].Height = new GridLength(0);
                        break;
                    case MessageBoxButton.YesNo:
                        grid.RowDefinitions[2].Height = new GridLength(0);
                        grid.RowDefinitions[3].Height = GridLength.Auto;
                        break;
                }
            }
        }

        private System.Windows.Threading.DispatcherTimer m_timer = new System.Windows.Threading.DispatcherTimer();
        public bool MEnabledTimer { get; set; }
        public int MSleep { get; set; }

        public MessageBoxWin()
        {
            InitializeComponent();

            MEnabledTimer = false;

            MButton = MessageBoxButton.OKCancel;
        }

        public MessageBoxWin(string text)
        {
            InitializeComponent();

            MEnabledTimer = false;

            MText = text;
            MButton = MessageBoxButton.OK;
        }

        public static void Show(string messageBoxText)
        {
            MessageBoxWin win = new MessageBoxWin();
            win.MText = messageBoxText;
            win.Show();
        }

        public static void Show(string messageBoxText, int sleep)
        {
            MessageBoxWin win = new MessageBoxWin();
            win.MEnabledTimer = true;
            win.MSleep = sleep;
            win.MText = messageBoxText;
            win.Show();
        }

        public static void Show(string messageBoxText, string title)
        {
            MessageBoxWin win = new MessageBoxWin();
            win.MText = messageBoxText;
            if (!string.IsNullOrEmpty(title))
            {
                win.MTitle = title;
            }
            win.Show();
        }

        public static MessageBoxResult Show(string messageBoxText, string title, MessageBoxButton button, MessageBoxImage icon)
        {
            MessageBoxWin win = new MessageBoxWin();
            win.MText = messageBoxText;
            if (!string.IsNullOrEmpty(title))
            {
                win.MTitle = title;
            }
            win.MButton = MessageBoxButton.YesNo;
            if (true == win.ShowDialog())
            {
                return MessageBoxResult.Yes;
            }
            else
            {
                return MessageBoxResult.No;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            switch (MButton)
            {
                case MessageBoxButton.OK:
                    this.Visibility = Visibility.Hidden;
                    break;
                default:
                    Close();
                    break;
            }
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (MEnabledTimer)
            {
                m_timer.Interval = TimeSpan.FromMilliseconds(MSleep);
                m_timer.Tick += timer1_Tick;
                m_timer.Start();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBoxButton.OK == MButton && Visibility.Hidden != this.Visibility)
            {
                this.Visibility = Visibility.Hidden;
                e.Cancel = true;
            }
        }

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
            Close();
        }
    }
}
