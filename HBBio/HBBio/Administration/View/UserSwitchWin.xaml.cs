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
    /// UserSwitchWin.xaml 的交互逻辑
    /// </summary>
    public partial class UserSwitchWin : Window
    {
        /// <summary>
        /// 当前用户名
        /// </summary>
        private string m_userName = "";
        /// <summary>
        /// 当前用户密码
        /// </summary>
        private string m_pwd = "";


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        public UserSwitchWin(Window parent, string userName, string pwd)
        {
            InitializeComponent();

            this.Owner = parent;
            //this.ShowInTaskbar = false;

            m_userName = userName;
            m_pwd = pwd;

            txtNameCurr.Text = userName;
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
            //this.MaxHeight = this.ActualHeight;
            //this.MaxWidth = this.ActualWidth;
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            if (null != this.Owner)
            {
                this.Owner.Focus();
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labPwd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AdministrationManager manager = new AdministrationManager();
            UserInfo item = null;
            string error = manager.GetUser(txtNameSwitch.Text, out item);
            if (null == error)
            {
                UserPwdWin win = new UserPwdWin(this, item, AdministrationStatic.Instance().MTacticsInfo);
                win.ShowDialog();
            }
            else
            {
                AuditTrails.AuditTrailsStatic.Instance().InsertRowError(this.Title + labPwd.Text, labNameSwitch.Text + txtNameSwitch.Text + "\n" + error);

                MessageBoxWin.Show(error);
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (pwdPwdCurr.Password.Equals(m_pwd))
            {
                string error = AdministrationStatic.Instance().Login(txtNameSwitch.Text, pwdPwdSwitch.Password);
                if (null == error)
                {
                    AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title,
                    this.labNameCurr.Text + this.txtNameCurr.Text + "\n" +
                    this.labNameSwitch.Text + this.txtNameSwitch.Text);

                    DialogResult = true;
                    return;
                }
                else
                {
                    AuditTrails.AuditTrailsStatic.Instance().InsertRowError(this.Title,
                    this.labNameCurr.Text + this.txtNameCurr.Text + "\n" +
                    this.labNameSwitch.Text + this.txtNameSwitch.Text + "\n" +
                    error);
                    Share.MessageBoxWin.Show(error);
                }
            }
            else
            {
                AuditTrails.AuditTrailsStatic.Instance().InsertRowError(this.Title,
                    this.labNameCurr.Text + this.txtNameCurr.Text + "\n" +
                    this.labNameSwitch.Text + this.txtNameSwitch.Text + "\n" +
                    Share.ReadXaml.GetResources("A_ErrorCurrPwd"));
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
