using HBBio.Share;
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

namespace HBBio.Chromatogram
{
    /// <summary>
    /// MarkerDelWin.xaml 的交互逻辑
    /// </summary>
    public partial class MarkerDelWin : Window
    {
        private List<StringBool> m_list = null;


        public MarkerDelWin(List<StringBool> list)
        {
            InitializeComponent();

            m_list = list;
            for (int i = 0; i < list.Count; i++)
            {
                CheckBox item = new CheckBox();
                item.Content = list[i].MName;
                listBox.Items.Add(item);
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < m_list.Count; i++)
            {
                m_list[i].MCheck = true == ((CheckBox)listBox.Items[i]).IsChecked;
            }

            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
