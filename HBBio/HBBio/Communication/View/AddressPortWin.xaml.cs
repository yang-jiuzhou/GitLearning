using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// AddressPortWin.xaml 的交互逻辑
    /// </summary>
    public partial class AddressPortWin : Window
    {
        public ObservableCollection<MString> MListAddress { get; set; }
        public ObservableCollection<MString> MListPort { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public AddressPortWin()
        {
            InitializeComponent();

            MListAddress = new ObservableCollection<MString>();
            MListPort = new ObservableCollection<MString>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dgvAddress.ItemsSource = MListAddress;
            dgvPort.ItemsSource = MListPort;
        }

        private void btnAddAddress_Click(object sender, RoutedEventArgs e)
        {
            if (0 == MListAddress.Count)
            {
                MListAddress.Add(new MString("192.168.1.250"));
            }
            else
            {
                try
                {
                    string[] arrLastName = MListAddress.Last().MName.Split('.');
                    arrLastName[3] = (Convert.ToInt32(arrLastName[3]) + 1).ToString();
                    StringBuilderSplit sb = new StringBuilderSplit(".");
                    foreach (var it in arrLastName)
                    {
                        sb.Append(it);
                    }
                    string nowName = sb.ToString();
                    nowName = nowName.Remove(nowName.Length - 1, 1);
                    MListAddress.Add(new MString(nowName));
                }
                catch { }
            }
        }

        private void btnAddPort_Click(object sender, RoutedEventArgs e)
        {
            if (0 == MListPort.Count)
            {
                MListPort.Add(new MString("1030"));
            }
            else
            {
                try
                {
                    MListPort.Add(new MString((Convert.ToInt32(MListPort.Last().MName) + 1).ToString()));
                }
                catch { }
            }
        }

        private void btnDelAddress_Click(object sender, RoutedEventArgs e)
        {
            if (0 < MListAddress.Count)
            {
                MListAddress.RemoveAt(MListAddress.Count - 1);
            }
        }

        private void btnDelPort_Click(object sender, RoutedEventArgs e)
        {
            if (0 < MListPort.Count)
            {
                MListPort.RemoveAt(MListPort.Count - 1);
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
