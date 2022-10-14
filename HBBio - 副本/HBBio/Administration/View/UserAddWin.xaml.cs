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
    /// UserAddWin.xaml 的交互逻辑
    /// </summary>
    public partial class UserAddWin : Window
    {
        /// <summary>
        /// 审计跟踪信息
        /// </summary>
        public string MOper { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        private UserInfoVM MUserInfoVM { get; set; }
        /// <summary>
        /// 新建用户依赖的安全策略（传入）
        /// </summary>
        private TacticsInfo m_tacticsInfo = null;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="tacticsInfo"></param>
        public UserAddWin(Window parent, TacticsInfo tacticsInfo)
        {
            InitializeComponent();

            this.Owner = parent;
            this.ShowInTaskbar = false;

            MUserInfoVM = new UserInfoVM();
            m_tacticsInfo = tacticsInfo;
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

            MUserInfoVM.MUserInfo = new UserInfo(permissionList[0].MID);
            MUserInfoVM.MListPermissionInfo = permissionList;
            foreach (FrameworkElement it in grid.Children)
            {
                if (it is TextBlock)
                {
                    continue;
                }

                it.DataContext = MUserInfoVM;
            }
        }

        /// <summary>
        /// 数据检查
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            //用户名复杂度
            if (1 == m_tacticsInfo.NameReg)
            {
                if (!TextLegal.NameLegal(txtName.Text))
                {
                    Share.MessageBoxWin.Show(Share.ReadXaml.S_ErrorIllegalName);
                    return false;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    Share.MessageBoxWin.Show(Share.ReadXaml.S_ErrorIllegalName);
                    return false;
                }
            }

            //登录密码
            if (pwdPwd.Password.Equals(pwdPwdConfirm.Password))
            {
                //登录密码复杂度
                if (1 == m_tacticsInfo.PwdReg)
                {
                    if (!TextLegal.PwdLegal(pwdPwd.Password, txtName.Text))
                    {
                        Share.MessageBoxWin.Show(Share.ReadXaml.GetResources("A_ErrorIllegalPwd"));
                        return false;
                    }
                }

                if (pwdPwd.Password.Length < m_tacticsInfo.PwdLength)
                {
                    Share.MessageBoxWin.Show(Share.ReadXaml.GetResources("A_ErrorIllegalPwd"));
                    return false;
                }
            }
            else
            {
                Share.MessageBoxWin.Show(Share.ReadXaml.GetResources("A_ErrorPwdConfirm"));
                return false;
            }

            //签名密码
            if (!pwdPwdSign.Password.Equals(pwdPwdSignConfirm.Password))
            {
                Share.MessageBoxWin.Show(Share.ReadXaml.GetResources("A_ErrorPwdSignConfirm"));
                return false;
            }

            return true;
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
            UserInfo item = ((UserInfoVM)txtName.DataContext).MUserInfo;
            item.MPwd = pwdPwd.Password;
            item.MPwdSign = pwdPwdSign.Password;
            item.MPwdDay = m_tacticsInfo.PwdMaxTime;
            string error = manager.AddUser(item);
            if (null == error)
            {
                StringBuilderSplit sb = new StringBuilderSplit();
                sb.Append(this.labName.Text + this.txtName.Text);
                sb.Append(this.labPermission.Text + ((PermissionInfo)this.cboxPermission.SelectedItem).MName);
                sb.Append(this.labNote.Text + this.txtNote.Text);

                MOper = sb.ToString();

                DialogResult = true;
            }
            else
            {
                Share.MessageBoxWin.Show(error);
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
