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
    public partial class RunLocationResultWin : Window
    {
        public int MProjectID { get; set; }
        public string MResultName { get; set; }
        public string MResultPath { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="projectId"></param>
        public RunLocationResultWin(int projectId, bool showManual)
        {
            InitializeComponent();

            projectTreeUC.MDefaultId = projectId;
            projectTreeUC.MShowManual = showManual;
            projectTreeUC.MSelectItem += TreeItemSelected;
        }

        /// <summary>
        /// 指定文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeItemSelected(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is TreeNode)
            {
                MProjectID = ((TreeNode)e.OriginalSource).MId;

                btnOK.IsEnabled = true;
            }
            else
            {
                btnOK.IsEnabled = false;
            }
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
            MResultPath = projectTreeUC.MSelectPath + "/" + MResultName;

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
