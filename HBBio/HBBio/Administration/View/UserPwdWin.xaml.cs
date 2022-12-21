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
    /// UserPwdWin.xaml 的交互逻辑
    /// </summary>
    public partial class UserPwdWin : Window
    {
        private UserInfo m_userInfo = null;
        private TacticsInfo m_tacticsInfo = null;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo"></param>
        public UserPwdWin(Window parent, UserInfo userInfo, TacticsInfo tacticsInfo)
        {
            InitializeComponent();

            this.Owner = parent;
            //this.ShowInTaskbar = false;

            m_userInfo = userInfo;
            m_tacticsInfo = tacticsInfo;

            txtName.Text = userInfo.MUserName;
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
            if (true == chboxLogin.IsChecked)
            {
                if (!m_userInfo.MPwd.Equals(pwdOld.Password))
                {
                    //登录密码错误
                    Share.MessageBoxWin.Show(Share.ReadXaml.GetResources("A_ErrorOldPwd"));
                    return;
                }

                if (pwd.Password.Equals(pwdOld.Password))
                {
                    //新旧登录密码相同
                    Share.MessageBoxWin.Show(Share.ReadXaml.GetResources("A_ErrorSamePwd"));
                    return;
                }

                if (!pwd.Password.Equals(pwdConfirm.Password))
                {
                    //两次输入密码不一样
                    Share.MessageBoxWin.Show(Share.ReadXaml.GetResources("A_ErrorPwdConfirm"));
                    return;
                }

                if (pwd.Password.Length < m_tacticsInfo.PwdLength)
                {
                    Share.MessageBoxWin.Show(Share.ReadXaml.GetResources("A_ErrorIllegalPwd"));
                    return;
                }
                if (1 == m_tacticsInfo.PwdReg)
                {
                    if (!Share.TextLegal.PwdLegal(pwd.Password, txtName.Text))
                    {
                        Share.MessageBoxWin.Show(Share.ReadXaml.GetResources("A_ErrorIllegalPwd"));
                        return;
                    }
                }

                m_userInfo.MPwd = pwd.Password;

                AdministrationManager manager = new AdministrationManager();
                manager.EditUserPwd(m_userInfo);

                AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title,
                    this.labName.Text + this.txtName.Text + "\n" + chboxLogin.Content);
            }

            if (true == chboxSign.IsChecked)
            {
                if (!m_userInfo.MPwdSign.Equals(pwdSignOld.Password))
                {
                    Share.MessageBoxWin.Show(Share.ReadXaml.GetResources("A_ErrorOldPwdSign"));
                    return;
                }

                if (pwdSign.Password.Equals(pwdSignOld.Password))
                {
                    Share.MessageBoxWin.Show(Share.ReadXaml.GetResources("A_ErrorSamePwdSign"));
                    return;
                }

                if (!pwdSign.Password.Equals(pwdSignConfirm.Password))
                {
                    Share.MessageBoxWin.Show(Share.ReadXaml.GetResources("A_ErrorPwdSignConfirm"));
                    return;
                }

                m_userInfo.MPwdSign = pwdSign.Password;

                AdministrationManager manager = new AdministrationManager();
                manager.EditUserPwdSign(m_userInfo);

                AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title,
                    this.labName.Text + this.txtName.Text + "\n" + chboxSign.Content);
            }

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