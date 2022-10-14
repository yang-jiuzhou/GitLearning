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

namespace HBBio.PassDog
{
    /// <summary>
    /// CheckSNWin.xaml 的交互逻辑
    /// </summary>
    public partial class CheckSNWin : Window
    {
        public PassDogValue MValue { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public CheckSNWin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (null != MValue)
            {
                txtHB.Text = MValue.MHB;
                txtName.Text = MValue.MName;
                txtMode.Text = MValue.MMode;
                txtSN.Text = MValue.MSN;
            }
        }

        /// <summary>
        /// 校准
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            string errorInfo = "";
            if (CSentinel.CompareMemery(txtHB.Text + "-" + txtName.Text + "-" + txtMode.Text + "-" + txtSN.Text, ref errorInfo))
            {
                MessageBoxWin.Show(Share.ReadXaml.GetResources("PassDog_CheckSuccess"));
                btnOk.IsEnabled = true;
            }
            else
            {
                MessageBoxWin.Show(Share.ReadXaml.GetResources("PassDog_CheckFail") +"\r\n" + errorInfo);
                btnOk.IsEnabled = false;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string dogStr = txtHB.Text + "-" +
                            txtName.Text + "-" +
                            txtMode.Text + "-" +
                            txtSN.Text;
            PassDogValue item = new PassDogValue();
            item.MHB = txtHB.Text;
            item.MName = txtName.Text;
            item.MMode = txtMode.Text;
            item.MSN = txtSN.Text;

            if (null == CSentinel.SetDogInfo(item))
            {
                MessageBoxWin.Show(Share.ReadXaml.GetResources("PassDog_UpdateSuccess"));
            }
            else
            {
                MessageBoxWin.Show(Share.ReadXaml.GetResources("PassDog_UpdateFail"));
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
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
