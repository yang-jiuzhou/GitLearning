using HBBio.Administration;
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
    /// TubeStandWin.xaml 的交互逻辑
    /// </summary>
    public partial class TubeStandWin : Window, WindowPermission
    {
        public TubeStandWin(Window parent)
        {
            InitializeComponent();

            this.Owner = parent;

            InitDGV();
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
        /// 设置各模块是否可用
        /// </summary>
        /// <param name="info"></param>
        public bool SetPermission(PermissionInfo info)
        {
            if (info.MList[(int)EnumPermission.TubeStand])
            {
                btnAdd.IsEnabled = info.MList[(int)EnumPermission.TubeStand_Edit];
                btnEdit.IsEnabled = info.MList[(int)EnumPermission.TubeStand_Edit];
                btnDel.IsEnabled = info.MList[(int)EnumPermission.TubeStand_Edit];

                return true;
            }
            else
            {
                this.Close();

                return false;
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!AdministrationStatic.Instance().ShowSignerReviewerWin(this, EnumSignerReviewer.TubeStand_Edit))
            {
                return;
            }

            TubeStandItemWin win = new TubeStandItemWin(this);
            if (true == win.ShowDialog())
            {
                InitDGV();
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (-1 != dgv.SelectedIndex)
            {
                if (!AdministrationStatic.Instance().ShowSignerReviewerWin(this, EnumSignerReviewer.TubeStand_Edit))
                {
                    return;
                }

                TubeStandItemWin win = new TubeStandItemWin(this);
                win.MItem = (TubeStandItem)dgv.SelectedItem;
                if (true == win.ShowDialog())
                {
                    InitDGV();

                    Communication.EnumCollectorInfo.ReSetBottleCollVol();
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (-1 != dgv.SelectedIndex)
            {
                if (MessageBoxResult.Yes == Share.MessageBoxWin.Show(Share.ReadXaml.S_Continue, Title + btnDel.Content, MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    if (!AdministrationStatic.Instance().ShowSignerReviewerWin(this, EnumSignerReviewer.TubeStand_Edit))
                    {
                        return;
                    }

                    TubeStandManager manager = new TubeStandManager();
                    if (null == manager.DelItem((TubeStandItem)dgv.SelectedItem))
                    {
                        AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(Title + btnDel.Content, ((TubeStandItem)dgv.SelectedItem).MName);

                        InitDGV();
                    }
                }
            }
        }

        /// <summary>
        /// 双击选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvPermission_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnEdit_Click(null, null);
        }

        /// <summary>
        /// 加载列表
        /// </summary>
        private void InitDGV()
        {
            List<TubeStandItem> list = null;
            TubeStandManager manager = new TubeStandManager();
            if (null == manager.GetList(out list))
            {
                dgv.ItemsSource = list;
            }
        }
    }
}
