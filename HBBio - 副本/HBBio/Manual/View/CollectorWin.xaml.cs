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
        /// <summary>
        /// 自定义事件，收集位切换时触发
        /// </summary>
        public static readonly RoutedEvent MSingleEvent = EventManager.RegisterRoutedEvent("MSingle", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CollectorWin));
        public event RoutedEventHandler MSingle
        {
            add { AddHandler(MSingleEvent, value); }
            remove { RemoveHandler(MSingleEvent, value); }
        }

        /// <summary>
        /// 自定义事件，阀位循环开始时触发
        /// </summary>
        public static readonly RoutedEvent MMultipleStartEvent = EventManager.RegisterRoutedEvent("MMultipleStart", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CollectorWin));
        public event RoutedEventHandler MMultipleStart
        {
            add { AddHandler(MMultipleStartEvent, value); }
            remove { RemoveHandler(MMultipleStartEvent, value); }
        }

        /// <summary>
        /// 自定义事件，阀位循环结束时触发
        /// </summary>
        public static readonly RoutedEvent MMultipleStopEvent = EventManager.RegisterRoutedEvent("MMultipleStop", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CollectorWin));
        public event RoutedEventHandler MMultipleStop
        {
            add { AddHandler(MMultipleStopEvent, value); }
            remove { RemoveHandler(MMultipleStopEvent, value); }
        }

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
        public Visibility MRealDelayVisibility
        {
            get
            {
                return rbtnRealSingle.Visibility;
            }
            set
            {
                rbtnRealSingle.Visibility = value;
                rbtnDelaySingle.Visibility = value;
                rbtnRealMultiple.Visibility = value;
                rbtnDelayMultiple.Visibility = value;
            }
        }
        public Visibility MMultipleVisibility
        {
            get
            {
                return groupMultipleSelect.Visibility;
            }
            set
            {
                groupMultipleSelect.Visibility = value;
            }
        }

        public bool MMultipleFlag
        {
            get
            {
                return true == btnStop.IsEnabled;
            }
            set
            {
                if (value)
                {
                    btnStart.IsEnabled = false;
                    btnStop.IsEnabled = true;
                }
                else
                {
                    btnStart.IsEnabled = true;
                    btnStop.IsEnabled = false;
                }
            }
        }
        public static bool s_singleDelay = false;
        public static bool s_multipleDelay = false;
        public static int s_multipleIndex = 0;
        public static double s_multipleVol = 0.01;

        public CollectionCollector MCollectionCollector { get; set; }


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

            if (s_singleDelay)
            {
                rbtnDelaySingle.IsChecked = true;
            }
            else
            {
                rbtnRealSingle.IsChecked = true;
            }
            if (s_multipleDelay)
            {
                rbtnDelayMultiple.IsChecked = true;
            }
            else
            {
                rbtnRealMultiple.IsChecked = true;
            }
            cboxSelect.ItemsSource = StringInt.GetItemsSource(MTubeNames);
            cboxSelect.SelectedIndex = s_multipleIndex;
            doubleVol.Value = s_multipleVol;

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
                btnIntervene.Visibility = Visibility.Collapsed;
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

                s_singleDelay = true == rbtnDelaySingle.IsChecked;

                RoutedEventArgs args = new RoutedEventArgs(MSingleEvent, new CollTextIndex(cboxIndex.SelectedValue.ToString(), sbtnStatus.IsChecked));
                RaiseEvent(args);
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

                s_singleDelay = true == rbtnDelaySingle.IsChecked;

                RoutedEventArgs args = new RoutedEventArgs(MSingleEvent, new CollTextIndex(MItemShow.MIndexSet, sbtnStatus.IsChecked));
                RaiseEvent(args);
            }
        }

        /// <summary>
        /// 前一个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFront_Click(object sender, RoutedEventArgs e)
        {
            AuditTrails.AuditTrailsStatic.Instance().InsertRowManual(this.Title, this.btnFront.ToolTip.ToString());

            s_singleDelay = true == rbtnDelaySingle.IsChecked;

            RoutedEventArgs args = new RoutedEventArgs(MSingleEvent, MItemShow.GetDel());
            RaiseEvent(args);
        }

        /// <summary>
        /// 后一个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AuditTrails.AuditTrailsStatic.Instance().InsertRowManual(this.Title, this.btnBack.ToolTip.ToString());

            s_singleDelay = true == rbtnDelaySingle.IsChecked;

            RoutedEventArgs args = new RoutedEventArgs(MSingleEvent, MItemShow.GetAdd());
            RaiseEvent(args);
        }

        /// <summary>
        /// 阀位循环开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;

            s_multipleDelay = true == rbtnDelayMultiple.IsChecked;
            s_multipleIndex = cboxSelect.SelectedIndex;
            s_multipleVol = (double)doubleVol.Value;

            AuditTrails.AuditTrailsStatic.Instance().InsertRowColl(ReadXamlCollection.S_CollMarkA, Title + btnStart.Content.ToString());

            RoutedEventArgs args = new RoutedEventArgs(MMultipleStartEvent, null);
            RaiseEvent(args);

            DialogResult = false;
        }

        /// <summary>
        /// 阀位循环取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;

            AuditTrails.AuditTrailsStatic.Instance().InsertRowColl(ReadXamlCollection.S_CollMarkA, Title + btnStop.Content.ToString());

            RoutedEventArgs args = new RoutedEventArgs(MMultipleStopEvent, null);
            RaiseEvent(args);

            DialogResult = false;
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
