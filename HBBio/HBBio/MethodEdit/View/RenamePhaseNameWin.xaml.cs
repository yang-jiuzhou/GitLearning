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

namespace HBBio.MethodEdit
{
    /// <summary>
    /// RenamePhaseNameWin.xaml 的交互逻辑
    /// </summary>
    public partial class RenamePhaseNameWin : Window
    {
        public string MName { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public RenamePhaseNameWin()
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
            txtOld.Text = MName;
            txtNew.Text = MName;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNew.Text))
            {
                Share.MessageBoxWin.Show(Share.ReadXaml.S_ErrorIllegalName);
                return;
            }
            MName = txtNew.Text;
            DialogResult = true;
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
