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
    /// ReviewerWin.xaml 的交互逻辑
    /// </summary>
    public partial class ReviewerWin : Window
    {
        private string m_reviewer = "";
        public string MReviewer
        {
            get
            {
                return m_reviewer;
            }
        }
        private string m_pwd = null;    //传入参数


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        public ReviewerWin(Window parent, string title, string signer, string pwd)
        {
            InitializeComponent();

            this.Owner = parent;
            //this.ShowInTaskbar = false;

            this.Title = this.Title + "-" + title;
            this.txtSigner.Text = signer;
            m_pwd = pwd;

            List<UserInfo> userList = null;
            AdministrationManager manager = new AdministrationManager();
            if (null == manager.GetUserInfoListSignerReviewer(out userList))
            {
                for (int i = 0; i < userList.Count; i++)
                {
                    if (userList[i].MUserName.Equals(signer) && 1 != userList[i].MID)
                    {
                        //去除自己
                        userList.RemoveAt(i);
                        break;
                    }
                }
                if (userList.Count > 0)
                {
                    this.cboxReviewer.ItemsSource = userList;
                    this.cboxReviewer.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (null == this.cboxReviewer.ItemsSource)
            {
                return;
            }

            if (!m_pwd.Equals(this.pwdSignerPwd.Password))
            {
                Share.MessageBoxWin.Show(labSigner.Text + Share.ReadXaml.GetResources("A_ErrorPwdSign"));
                return;
            }

            if (!((UserInfo)this.cboxReviewer.SelectedItem).MPwdSign.Equals(this.pwdReviewerPwd.Password))
            {
                Share.MessageBoxWin.Show(labReviewer.Text + Share.ReadXaml.GetResources("A_ErrorPwdSign"));
                return;
            }

            m_reviewer = this.cboxReviewer.Text;

            AuditTrails.AuditTrailsStatic.Instance().InsertRowSignerReviewer(this.Title,
                this.labSigner.Text + this.txtSigner.Text + "\n"
                + this.labReviewer.Text + this.cboxReviewer.Text + "\n"
                + this.labNote.Text + this.txtNote.Text);

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
