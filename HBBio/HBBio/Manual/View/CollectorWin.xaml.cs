using HBBio.Collection;
using HBBio.Communication;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HBBio.Manual
{
    /// <summary>
    /// CollectorWin.xaml 的交互逻辑
    /// </summary>
    public partial class CollectorWin : Window
    {
        private List<string> m_tubeName = new List<string>();       //试管架列表
        private bool m_cboxFlag = false;                            //正在操作收集口下拉框
        private bool m_btnFlag = false;                             //正在操作状态按钮

        public CollectorItem MItemShow { get; set; }
        public List<string> MTubeNames
        {
            get
            {
                return m_tubeName;
            }
            set
            {
                value.ForEach(i => m_tubeName.Add(i));
            }
        }
        public CollectionCollector MCollectionCollector { get; set; }


        /// <summary>
        /// 自定义事件，更新紫外设置时触发
        /// </summary>
        public static readonly RoutedEvent MUpdateEvent =
             EventManager.RegisterRoutedEvent("MUpdateCollector", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CollectorWin));
        public event RoutedEventHandler MUpdateCollector
        {
            add { AddHandler(MUpdateEvent, value); }
            remove { RemoveHandler(MUpdateEvent, value); }
        }

        //创建一个自定义委托，用于审计跟踪
        public delegate void MAuditTrailsDdelegate(object desc, object oper);
        //声明一个审计跟踪事件
        public MAuditTrailsDdelegate MAuditTrailsHandler;


        /// <summary>
        /// 构造函数
        /// </summary>
        public CollectorWin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 拖拽窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// 加载窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cboxIndexShow.ItemsSource = StringInt.GetItemsSource(MTubeNames);
            cboxIndex.ItemsSource = StringInt.GetItemsSource(MTubeNames);

            if (null != MItemShow)
            {
                cboxIndexShow.DataContext = MItemShow;
                sbtnStatusShow.DataContext = MItemShow;

                cboxIndex.DataContext = MItemShow;
                cboxIndex.Text = MItemShow.MIndexSet;
                sbtnStatus.DataContext = MItemShow;
                sbtnStatus.IsChecked = MItemShow.MStatusSet;
            }

            if (null == MCollectionCollector || 0 == MCollectionCollector.MList.Count || !MCollectionCollector.MSignal)
            {
                groupIntervene.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 鼠标进入收集口下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboxIndex_MouseEnter(object sender, MouseEventArgs e)
        {
            m_cboxFlag = true;
        }

        /// <summary>
        /// 鼠标离开收集口下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboxIndex_MouseLeave(object sender, MouseEventArgs e)
        {
            m_cboxFlag = false;
        }

        /// <summary>
        /// 收集口下拉框切换收集口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboxIndex_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (m_cboxFlag)
            {
                AuditTrails.AuditTrailsStatic.Instance().InsertRowManual(this.Title, this.labIndex.Text + cboxIndex.SelectedValue.ToString());
                
                if (true == rbtnReal.IsChecked)
                {
                    RoutedEventArgs args = new RoutedEventArgs(MUpdateEvent, "");
                    RaiseEvent(args);

                    MItemShow.MIndexSet = cboxIndex.SelectedValue.ToString();
                }
                else
                {
                    RoutedEventArgs args = new RoutedEventArgs(MUpdateEvent, cboxIndex.SelectedValue.ToString());
                    RaiseEvent(args);
                }
                
                if (sbtnStatus.IsChecked)
                {
                    MAuditTrailsHandler?.Invoke(ReadXamlCollection.C_CollMarkM, cboxIndex.SelectedValue.ToString());
                }
            } 
        }

        /// <summary>
        /// 鼠标进入状态按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnStatus_MouseEnter(object sender, MouseEventArgs e)
        {
            m_btnFlag = true;
        }

        /// <summary>
        /// 鼠标离开状态按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnStatus_MouseLeave(object sender, MouseEventArgs e)
        {
            m_btnFlag = false;
        }

        /// <summary>
        /// 状态按钮切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnStatus_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (m_btnFlag)
            {
                AuditTrails.AuditTrailsStatic.Instance().InsertRowManual(this.Title, this.labStatus.Text + (sbtnStatus.IsChecked ? ReadXamlCollection.S_CollColl : ReadXamlCollection.S_CollWaste));
                
                if (true == rbtnReal.IsChecked)
                {
                    RoutedEventArgs args = new RoutedEventArgs(MUpdateEvent, "");
                    RaiseEvent(args);

                    MItemShow.MStatusSet = sbtnStatus.IsChecked;
                }
                else
                {
                    RoutedEventArgs args = new RoutedEventArgs(MUpdateEvent, sbtnStatus.IsChecked ? "True" : "False");
                    RaiseEvent(args);
                }

                if (sbtnStatus.IsChecked)
                {
                    MAuditTrailsHandler?.Invoke(ReadXamlCollection.C_CollMarkM, cboxIndex.SelectedValue.ToString());
                }
                else
                {
                    MAuditTrailsHandler?.Invoke(ReadXamlCollection.C_CollMarkM, "WASTE");
                }
            }
        }

        /// <summary>
        /// 前一个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFront_Click(object sender, RoutedEventArgs e)
        {
            AuditTrails.AuditTrailsStatic.Instance().InsertRowManual(this.Title, this.gboxShortcut.Header.ToString() + this.btnFront.ToolTip.ToString());
            
            if (true == rbtnReal.IsChecked)
            {
                RoutedEventArgs args = new RoutedEventArgs(MUpdateEvent, "");
                RaiseEvent(args);

                MItemShow.MIndexSet = "-1";
            }
            else
            {
                RoutedEventArgs args = new RoutedEventArgs(MUpdateEvent, "-1");
                RaiseEvent(args);
            }

            if (0 < cboxIndex.SelectedIndex)
            {
                cboxIndex.SelectedIndex -= 1;
            }
            else
            {
                cboxIndex.SelectedIndex = cboxIndex.Items.Count - 1;
            }

            if (sbtnStatus.IsChecked)
            {
                MAuditTrailsHandler?.Invoke(ReadXamlCollection.C_CollMarkM, cboxIndex.SelectedValue.ToString());
            }
        }

        /// <summary>
        /// 后一个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AuditTrails.AuditTrailsStatic.Instance().InsertRowManual(this.Title, this.gboxShortcut.Header.ToString() + this.btnBack.ToolTip.ToString());
            
            if (true == rbtnReal.IsChecked)
            {
                RoutedEventArgs args = new RoutedEventArgs(MUpdateEvent, "");
                RaiseEvent(args);

                MItemShow.MIndexSet = "+1";
            }
            else
            {
                RoutedEventArgs args = new RoutedEventArgs(MUpdateEvent, "+1");
                RaiseEvent(args);
            }

            if (cboxIndex.Items.Count - 1 > cboxIndex.SelectedIndex)
            {
                cboxIndex.SelectedIndex += 1;
            }
            else
            {
                cboxIndex.SelectedIndex = 0;
            }

            if (sbtnStatus.IsChecked)
            {
                MAuditTrailsHandler?.Invoke(ReadXamlCollection.C_CollMarkM, cboxIndex.SelectedValue.ToString());
            }
        }

        /// <summary>
        /// 收集干预
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIntervene_Click(object sender, RoutedEventArgs e)
        {
            if (0 < MCollectionCollector.MList.Count)
            {
                CollectionItemWin win = new CollectionItemWin();
                win.MTubeNameList = EnumCollectorInfo.NameList;
                win.MItem = MCollectionCollector.MList[MCollectionCollector.MIndex - 1];
                if (true == win.ShowDialog())
                {
                    MCollectionCollector.MList[MCollectionCollector.MIndex - 1] = win.MItem;
                }
            }
        }
    }
}
