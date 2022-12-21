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
    /// SignerWin.xaml 的交互逻辑
    /// </summary>
    public partial class SignerWin : Window
    {
        private string m_pwd = null;    //传入参数


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="title"></param>
        /// <param name="signer"></param>
        /// <param name="pwd"></param>
        public SignerWin(Window parent, string title, string signer, string pwd)
        {
            InitializeComponent();

            this.Owner = parent;
            //this.ShowInTaskbar = false;

            this.Title = this.Title + "-" + title;
            this.txtSigner.Text = signer;
            m_pwd = pwd;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (m_pwd.Equals(pwdSignerPwd.Password))
            {
                AuditTrails.AuditTrailsStatic.Instance().InsertRowSignerReviewer(this.Title,
                    this.labSigner.Text + this.txtSigner.Text + "\n"
                    + this.labNote.Text + this.txtNote.Text);
                DialogResult = true;
            }
            else
            {
                Share.MessageBoxWin.Show(Share.ReadXaml.GetResources("A_ErrorPwdSign"));
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
