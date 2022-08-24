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

namespace HBBio.Administration
{
    /// <summary>
    /// RegisterWin.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterWin : Window
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        public RegisterWin(Window parent)
        {
            InitializeComponent();

            this.Owner = parent;
            this.ShowInTaskbar = false;
        }

        /// <summary>
        /// 数据检查
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            if (TextLegal.NameLegal(txtName.Text) && TextLegal.NameLegal(txtPermission.Text))
            {
                if (TextLegal.NameLegal(txtPermission.Text))
                {
                    if (pwdPwd.Password.Equals(pwdPwdConfirm.Password))
                    {
                        if (pwdPwdSign.Password.Equals(pwdPwdSignConfirm.Password))
                        {
                            return true;
                        }
                        else
                        {
                            MessageBoxWin.Show(Share.ReadXaml.GetResources("A_ErrorPwdSignConfirm"));
                            return false;
                        }
                    }
                    else
                    {
                        MessageBoxWin.Show(Share.ReadXaml.GetResources("A_ErrorPwdConfirm"));
                        return false;
                    }
                }
                else
                {
                    MessageBoxWin.Show(Share.ReadXaml.S_ErrorIllegalName);
                    return false;
                }
            }
            else
            {
                MessageBoxWin.Show(Share.ReadXaml.S_ErrorIllegalName);
                return false;
            }
        }

        /// <summary>
        /// 禁止复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        /// <summary>
        /// 禁止复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.Handled = true;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckData())
            {
                return;
            }

            AdministrationManager manager = new AdministrationManager();
            PermissionInfo permissionItem = null;
            string error = manager.GetPermission(txtPermission.Text, out permissionItem);
            if (null != error || null == permissionItem)
            {
                permissionItem = new PermissionInfo(-1, 0, txtPermission.Text, txtPermissionNote.Text, true);
                error = manager.AddPermission(permissionItem);
            }
            if (null == error)
            {
                UserInfo item = new UserInfo(txtName.Text, permissionItem.MID, txtUserNote.Text, pwdPwd.Password, pwdPwdSign.Password);
                error = manager.AddUser(item);
                if (null == error)
                {
                    AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title,
                    this.labUserName.Text + this.txtName.Text + "\n" +
                    this.labUserNote.Text + this.txtUserNote.Text + "\n" +
                    this.labPermissionName.Text + this.txtPermission.Text + "\n" +
                    this.labPermissionNote.Text + this.txtPermissionNote.Text);

                    DialogResult = true;
                    return;
                }
            }

            MessageBoxWin.Show(error);
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
