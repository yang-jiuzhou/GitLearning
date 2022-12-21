using HBBio.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HBBio.AuditTrails
{
    /// <summary>
    /// AuditTrailsUC.xaml 的交互逻辑
    /// </summary>
    public partial class AuditTrailsUC : UserControl
    {
        private LogColumnVisibility m_visible = new LogColumnVisibility();
        private EnumBase m_axis = EnumBase.T;


        /// <summary>
        /// 属性，设置资源
        /// </summary>
        public System.Collections.IEnumerable ItemsSource
        {
            set
            {
                dgv.ItemsSource = value;
            }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        public AuditTrailsUC()
        {
            InitializeComponent();

            ((ICollectionView)this.dgv.Items).CollectionChanged += QueryBusinessLevelWindow_CollectionChanged;
        }

        /// <summary>
        /// 设置各列显隐
        /// </summary>
        /// <param name="item"></param>
        public void SetColumnVisible(LogColumnVisibility item)
        {
            if (null != item)
            {
                m_visible = item;

                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    dgv.Columns[i].Visibility = item.MArrVisib[i] ? Visibility.Visible : Visibility.Collapsed;
                }

                SetColumnAxisVisible(m_axis);
            }
        }

        /// <summary>
        /// 设置显隐
        /// </summary>
        /// <param name="axis"></param>
        public void SetColumnAxisVisible(EnumBase axis)
        {
            m_axis = axis;

            switch (axis)
            {
                case EnumBase.T:
                    dgv.Columns[(int)EnumLog.BatchT].Visibility = m_visible.MArrVisib[(int)EnumLog.BatchT] ? Visibility.Visible : Visibility.Collapsed;
                    dgv.Columns[(int)EnumLog.BatchV].Visibility = Visibility.Collapsed;
                    dgv.Columns[(int)EnumLog.BatchCV].Visibility = Visibility.Collapsed;
                    break;
                case EnumBase.V:
                    dgv.Columns[(int)EnumLog.BatchT].Visibility = Visibility.Collapsed;
                    dgv.Columns[(int)EnumLog.BatchV].Visibility = m_visible.MArrVisib[(int)EnumLog.BatchV] ? Visibility.Visible : Visibility.Collapsed;
                    dgv.Columns[(int)EnumLog.BatchCV].Visibility = Visibility.Collapsed;
                    break;
                case EnumBase.CV:
                    dgv.Columns[(int)EnumLog.BatchT].Visibility = Visibility.Collapsed;
                    dgv.Columns[(int)EnumLog.BatchV].Visibility = Visibility.Collapsed;
                    dgv.Columns[(int)EnumLog.BatchCV].Visibility = m_visible.MArrVisib[(int)EnumLog.BatchCV] ? Visibility.Visible : Visibility.Collapsed;
                    break;
            }
        }

        private void dgv_Loaded(object sender, RoutedEventArgs e)
        {
            if (0 != this.dgv.Items.Count)
            {
                this.dgv.ScrollIntoView(this.dgv.Items[this.dgv.Items.Count - 1]);
            }
        }

        private void QueryBusinessLevelWindow_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (null != e.NewItems)//新增数据时才出发滚动
            {
                this.dgv.ScrollIntoView(this.dgv.Items[this.dgv.Items.Count - 1]);
            }   
        }
    }
}
