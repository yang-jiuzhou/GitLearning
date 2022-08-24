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

namespace HBBio.Administration
{
    /// <summary>
    /// UserLockWin.xaml 的交互逻辑
    /// </summary>
    public partial class UserLockWin : Window
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        public UserLockWin(Window parent)
        {
            InitializeComponent();

            this.Owner = parent;
            this.ShowInTaskbar = false;
        }

        /// <summary>
        /// 加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.MinHeight = this.ActualHeight;
            this.MinWidth = this.ActualWidth;
            this.MaxHeight = this.ActualHeight;
            this.MaxWidth = this.ActualWidth;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string error = AdministrationStatic.Instance().Login(txtNameCurr.Text, pwdPwdCurr.Password);
            if (null == error)
            {
                AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, this.labNameCurr.Text + this.txtNameCurr.Text);

                DialogResult = true;
            }
            else
            {
                Share.MessageBoxWin.Show(Share.ReadXaml.GetResources("A_ErrorCurrPwd"));
            }
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
