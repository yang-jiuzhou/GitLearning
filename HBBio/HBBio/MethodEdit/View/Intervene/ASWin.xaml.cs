using HBBio.Communication;
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

namespace HBBio.MethodEdit
{
    /// <summary>
    /// ASWin.xaml 的交互逻辑
    /// </summary>
    public partial class ASWin : Window
    {
        private ASMethodPara m_item = null;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="asValue"></param>
        public ASWin(Window parent, ASMethodPara item)
        {
            InitializeComponent();

            this.Owner = parent;

            m_item = item;
            asParaUC.DataContext = new ASMethodParaVM(Share.DeepCopy.DeepCopyByXml(item));
        }

        /// <summary>
        /// 移动对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// 关闭对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 应用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            string log = asParaUC.GetLog(m_item, true);
            if (!string.IsNullOrEmpty(log))
            {
                AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, log);
  
                DialogResult = true;
            }
        }
    }
}
