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

namespace HBBio.Chromatogram
{
    /// <summary>
    /// MarkerAdd.xaml 的交互逻辑
    /// </summary>
    public partial class MarkerAddWin : Window
    {
        public MarkerInfo MMarkerInfo { get; set; }
        public string MLogOper
        {
            get
            {
                return (true == rbtnNow.IsChecked ? rbtnNow.Content.ToString() : doubleTVCV.Value + labTVCV.Text) + " " + txtName.Text;
            }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="unit"></param>
        public MarkerAddWin(string unit)
        {
            InitializeComponent();

            labTVCV.Text = unit;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (true == rbtnNow.IsChecked)
            {
                MMarkerInfo = new MarkerInfo(txtName.Text);
            }
            else
            {
                MMarkerInfo = new MarkerInfo(txtName.Text, (double)doubleTVCV.Value);
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
