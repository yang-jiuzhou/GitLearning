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

namespace HBBio.ProjectManager
{
    /// <summary>
    /// RunResultWin.xaml 的交互逻辑
    /// </summary>
    public partial class RunResultWin : Window
    {
        public string MResultName { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public RunResultWin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (!TextLegal.FileNameLegal(txtName.Text))
            {
                MessageBoxWin.Show(Share.ReadXaml.S_ErrorIllegalName);
                return;
            }

            MResultName = txtName.Text;

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
