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

        public MessageBoxButton MButton
        {
            set
            {
                switch(value)
                {
                    case MessageBoxButton.OK:
                        grid.RowDefinitions[3].Height = new GridLength(0);
                        break;
                    case MessageBoxButton.YesNo:
                        grid.RowDefinitions[2].Height = new GridLength(0);
                        break;
                }
            }
        }

        public MessageBoxWin()
        {
            InitializeComponent();
        }

        public MessageBoxWin(string text)
        {
            InitializeComponent();

            MText = text;
            MButton = MessageBoxButton.OK;
        }

        public static MessageBoxResult Show(string messageBoxText)
        {
            MessageBoxWin win = new MessageBoxWin();
            win.MText = messageBoxText;
            win.MButton = MessageBoxButton.OK;
            win.Show();

            return MessageBoxResult.OK;
        }

        public static MessageBoxResult Show(string messageBoxText, string title)
        {
            MessageBoxWin win = new MessageBoxWin();
            win.MText = messageBoxText;
            if (!string.IsNullOrEmpty(title))
            {
                win.MTitle = title;
            }
            win.MButton = MessageBoxButton.OK;
            win.Show();

            return MessageBoxResult.OK;
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
            this.Visibility = Visibility.Hidden;
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
