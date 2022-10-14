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
    /// StringBoolWin.xaml 的交互逻辑
    /// </summary>
    public partial class StringBoolWin : Window
    {
        private List<StringBool> m_list = new List<StringBool>();


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="header"></param>
        public StringBoolWin(string header, bool visible = false)
        {
            InitializeComponent();

            this.Title = header;
            colColor.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
        }

        public void AddItem(StringBool item)
        {
            m_list.Add(item);
        }

        public StringBool GetItem(int index)
        {
            return m_list[index];
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dgvRunData.ItemsSource = m_list;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < m_list.Count; i++)
            {
                m_list[i].MCheck = false;
            }
        }

        private void btnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < m_list.Count; i++)
            {
                m_list[i].MCheck = true;
            }
        }

        /// <summary>
        /// 切换勾选状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRunData_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (null != dgvRunData.CurrentCell && 2 == dgvRunData.CurrentCell.Column.DisplayIndex)
            {
                m_list[dgvRunData.SelectedIndex].MCheck = !m_list[dgvRunData.SelectedIndex].MCheck;
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

        private void newColor_Click(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.ColorDialog dlg = new System.Windows.Forms.ColorDialog();
            dlg.Color = Share.ValueTrans.MediaToDraw(((SolidColorBrush)m_list[dgvRunData.SelectedIndex].MBrush).Color);
            if (System.Windows.Forms.DialogResult.OK == dlg.ShowDialog())
            {
                m_list[dgvRunData.SelectedIndex].MBrush = new SolidColorBrush(Share.ValueTrans.DrawToMedia(dlg.Color));
                MApp.DoEvents();
            }
        }
    }
}
