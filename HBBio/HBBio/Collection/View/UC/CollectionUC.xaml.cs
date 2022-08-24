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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HBBio.Collection
{
    /// <summary>
    /// CollectionUC.xaml 的交互逻辑
    /// </summary>
    public partial class CollectionUC : UserControl
    {
        private CollectionValve m_collectionValve = null;
        private CollectionCollector m_collectionCollector = null;

        public EnumCollectionType MType 
        { 
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
                switch (value)
                {
                    case EnumCollectionType.Waste:
                        Clear();
                        break;
                    default:
                        InitListBox();
                        break;
                }
            }
        }
        private EnumCollectionType m_type = EnumCollectionType.Waste;
        private List<string> MNameList
        {
            get
            {
                switch (MType)
                {
                    case EnumCollectionType.Collector:
                        return Communication.EnumCollectorInfo.NameList;
                    default:
                        List<string> tmp = Communication.EnumOutInfo.NameList.ToList();
                        tmp.RemoveAt(0);
                        return tmp;
                }
            }
        }
        private List<CollectionItem> MList
        {
            get
            {
                switch (MType)
                {
                    case EnumCollectionType.Collector:
                        return MCollectionCollector.MList;
                    default:
                        return MCollectionValve.MList;
                }
            }
        }
        private CollectionItem MCopyItem { get; set; }
        public CollectionValve MCollectionValve
        {
            get
            {
                return m_collectionValve;
            }
            set
            {
                m_collectionValve = value;
            }
        }
        public CollectionCollector MCollectionCollector
        {
            get
            {
                return m_collectionCollector;
            }
            set
            {
                m_collectionCollector = value;
            }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        public CollectionUC()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            listbox.Items.Clear();
        }

        private void InitListBox()
        {
            listbox.Items.Clear();
            foreach (var it in MList)
            {
                listbox.Items.Add(new TextBlock() { Height = 35, Text = it.MShowInfo, ToolTip = it.MShowInfo });
            }
        }

        /// <summary>
        /// 加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (-1 != listbox.SelectedIndex)
            {
                CollectionItemWin win = new CollectionItemWin();
                win.MTubeNameList = MNameList;
                win.MItem = MList[listbox.SelectedIndex];
                if (true == win.ShowDialog())
                {
                    MList[listbox.SelectedIndex] = win.MItem;
                    listbox.Items[listbox.SelectedIndex] = new TextBlock() { Height = 35, Text = win.MItem.MShowInfo, ToolTip = win.MItem.MShowInfo };
                }
            }
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            if (-1 != listbox.SelectedIndex)
            {
                MCopyItem = Share.DeepCopy.DeepCopyByXml(MList[listbox.SelectedIndex]);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (null == MCopyItem)
            {
                CollectionItemWin win = new CollectionItemWin();
                win.MTubeNameList = MNameList;
                if (true == win.ShowDialog())
                {
                    MList.Add(win.MItem);
                    listbox.Items.Add(new TextBlock() { Height = 35, Text = win.MItem.MShowInfo, ToolTip = win.MItem.MShowInfo });
                    listbox.SelectedIndex = listbox.Items.Count - 1;
                }
            }
            else
            {
                CollectionItem item = Share.DeepCopy.DeepCopyByXml(MCopyItem);
                MList.Add(item);
                listbox.Items.Add(new TextBlock() { Height = 35, Text = item.MShowInfo, ToolTip = item.MShowInfo });
                listbox.SelectedIndex = listbox.Items.Count - 1;
            }
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            int index = listbox.SelectedIndex;
            if (-1 == index)
            {
                index = listbox.Items.Count - 1;
            }
            if (-1 == index)
            {
                return;
            }

            if (null == MCopyItem)
            {
                CollectionItemWin win = new CollectionItemWin();
                win.MTubeNameList = MNameList;
                if (true == win.ShowDialog())
                {
                    MList.Insert(index, win.MItem);
                    listbox.Items.Insert(index, new TextBlock() { Height = 35, Text = win.MItem.MShowInfo, ToolTip = win.MItem.MShowInfo });
                    listbox.SelectedIndex = index;
                }
            }
            else
            {
                CollectionItem item = Share.DeepCopy.DeepCopyByXml(MCopyItem);
                MList.Insert(index, item);
                listbox.Items.Insert(index, new TextBlock() { Height = 35, Text = item.MShowInfo, ToolTip = item.MShowInfo });
                listbox.SelectedIndex = index;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (-1 != listbox.SelectedIndex)
            {
                MList.RemoveAt(listbox.SelectedIndex);
                listbox.Items.RemoveAt(listbox.SelectedIndex);
            }
        }

        /// <summary>
        /// 双击编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listbox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnEdit_Click(null, null);
        }
    }
}
