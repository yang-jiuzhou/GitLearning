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
    /// UserEditWin.xaml 的交互逻辑
    /// </summary>
    public partial class UserEditWin : Window
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfo MUserInfo { get; set; }
        /// <summary>
        /// 审计跟踪信息
        /// </summary>
        public string MOper { get; set; }

        /// <summary>
        /// 用户信息(对比)
        /// </summary>
        private UserInfoVM MUserInfoVM { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="item"></param>
        public UserEditWin(Window parent)
        {
            InitializeComponent();

            this.Owner = parent;
            this.ShowInTaskbar = false;

            MUserInfoVM = new UserInfoVM();
        }

        /// <summary>
        /// 加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<PermissionInfo> permissionList = null;
            AdministrationManager manager = new AdministrationManager();
            if (null == manager.GetPermissionInfoList(out permissionList))
            {
                cboxPermission.ItemsSource = permissionList;
            }

            MUserInfoVM.MUserInfo = DeepCopy.DeepCopyByXml(MUserInfo);
            MUserInfoVM.MListPermissionInfo = permissionList;
            foreach (FrameworkElement it in grid.Children)
            {
                if (it is TextBlock)
                {
                    continue;
                }

                it.DataContext = MUserInfoVM;
            }

            if (1 == MUserInfo.MID)
            {
                //系统用户权限不可编辑
                this.btnOK.IsEnabled = false;
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            AdministrationManager manager = new AdministrationManager();
            string error = null;
            StringBuilderSplit sb = new StringBuilderSplit();

            if (MUserInfo.MPermissionNameID != MUserInfoVM.MUserInfo.MPermissionNameID)
            {
                sb.Append(labPermission.Text + MUserInfo.MPermissionName + " -> " + cboxPermission.Text);

                //修改权限名
                MUserInfo.MPermissionNameID = MUserInfoVM.MUserInfo.MPermissionNameID;
                error += manager.EditUserPermission(MUserInfo);
            }

            if (!MUserInfo.MNote.Equals(MUserInfoVM.MUserInfo.MNote))
            {
                sb.Append(labNote.Text + MUserInfo.MNote + " -> " + MUserInfoVM.MUserInfo.MNote);

                //修改备注
                MUserInfo.MNote = MUserInfoVM.MUserInfo.MNote;
                error += manager.EditUserNote(MUserInfo);
            }

            if (MUserInfo.MEnabled != MUserInfoVM.MUserInfo.MEnabled)
            {
                if (MUserInfoVM.MUserInfo.MEnabled)
                {
                    sb.Append(labStatic.Text + rbtnDisActive.Content + " -> " + rbtnActive.Content);
                }
                else
                {
                    sb.Append(labStatic.Text + rbtnActive.Content + " -> " + rbtnDisActive.Content);
                }

                //修改状态
                MUserInfo.MEnabled = MUserInfoVM.MUserInfo.MEnabled;
                error += manager.EditUserEnabled(MUserInfo);
            }

            if (MUserInfoVM.MUnlock)
            {
                sb.Append(labStatic.Text + chboxUnlock.Content);

                //解锁
                MUserInfo.MErrorNum = 0;
                error += manager.EditUserErrorNum(MUserInfo);
            }

            if (MUserInfoVM.MResetPwd)
            {
                sb.Append(labPwd.Text + chboxResetPwdLogin.Content);

                //重置登录密码
                error += manager.EditUserPwdReset(MUserInfo, MUserInfoVM.MResetPwdStr);
            }

            if (MUserInfoVM.MResetPwdSign)
            {
                sb.Append(labPwdSign.Text + chboxResetPwdSign.Content);

                //重置签名密码
                error += manager.EditUserPwdSignReset(MUserInfo, MUserInfoVM.MResetPwdSignStr);
            }

            string modify = sb.ToString();
            if (!string.IsNullOrEmpty(modify))
            {
                if (string.IsNullOrEmpty(error))
                {
                    MOper = labName.Text + txtName.Text + "\n" + sb.ToString();
                    
                    DialogResult = true;
                }
                else
                {
                    Share.MessageBoxWin.Show(error);
                }
            }
            else
            {
                DialogResult = true;
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