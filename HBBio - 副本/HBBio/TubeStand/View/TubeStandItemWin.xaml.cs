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

namespace HBBio.TubeStand
{
    /// <summary>
    /// TubeStandItemWin.xaml 的交互逻辑
    /// </summary>
    public partial class TubeStandItemWin : Window
    {
        public TubeStandItem MItem { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        public TubeStandItemWin(Window parent)
        {
            InitializeComponent();

            this.Owner = parent;
            //this.ShowInTaskbar = false;
        }

        /// <summary>
        /// 初始加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (null == MItem)
            {
                MItem = new TubeStandItem();
            }
            else
            {
                doubleVolume.IsReadOnly = true;
                intCollCount.IsReadOnly = true;
                intRow.IsReadOnly = true;
                intCol.IsReadOnly = true;
            }

            foreach (FrameworkElement it in grid.Children)
            {
                if (it is TextBlock)
                {
                    continue;
                }
                it.DataContext = MItem;
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            TubeStandManager manager = new TubeStandManager();
            if (true == doubleVolume.IsReadOnly)
            {
                //编辑
                string error = manager.UpdateItem(MItem);
                if (null == error)
                {
                    AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, this.labName.Text + this.txtName.Text);

                    DialogResult = true;
                }
                else
                {
                    Share.MessageBoxWin.Show(error);
                }
            }
            else
            {
                //添加
                string error = manager.InsertItem(MItem);
                if (null == error)
                {
                    AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, this.labName.Text + this.txtName.Text);

                    DialogResult = true;
                }
                else
                {
                    Share.MessageBoxWin.Show(error);
                }
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
