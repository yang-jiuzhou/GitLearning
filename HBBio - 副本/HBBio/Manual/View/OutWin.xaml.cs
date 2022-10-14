using HBBio.AuditTrails;
using HBBio.Collection;
using HBBio.Result;
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
    /// OutWin.xaml 的交互逻辑
    /// </summary>
    public partial class OutWin : Window
    {
        /// <summary>
        /// 自定义事件，阀位切换时触发
        /// </summary>
        public static readonly RoutedEvent MSingleEvent = EventManager.RegisterRoutedEvent("MSingle", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(OutWin));
        public event RoutedEventHandler MSingle
        {
            add { AddHandler(MSingleEvent, value); }
            remove { RemoveHandler(MSingleEvent, value); }
        }

        /// <summary>
        /// 自定义事件，阀位循环开始时触发
        /// </summary>
        public static readonly RoutedEvent MMultipleStartEvent = EventManager.RegisterRoutedEvent("MMultipleStart", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(OutWin));
        public event RoutedEventHandler MMultipleStart
        {
            add { AddHandler(MMultipleStartEvent, value); }
            remove { RemoveHandler(MMultipleStartEvent, value); }
        }

        /// <summary>
        /// 自定义事件，阀位循环结束时触发
        /// </summary>
        public static readonly RoutedEvent MMultipleStopEvent = EventManager.RegisterRoutedEvent("MMultipleStop", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(OutWin));
        public event RoutedEventHandler MMultipleStop
        {
            add { AddHandler(MMultipleStopEvent, value); }
            remove { RemoveHandler(MMultipleStopEvent, value); }
        }

        public int MIndex { get; set; }
        public string MOper { get; set; }
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
        public static bool s_multipleDelay = false;
        public static int s_multipleIndex = 0;
        public static double s_multipleVol = 0.01;

        public CollectionValve MCollectionValve { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="strTitle"></param>
        /// <param name="listName"></param>
        /// <param name="index"></param>
        public OutWin(Window parent, string strTitle, string[] listName, int index)
        {
            InitializeComponent();

            this.Owner = parent;

            title.Text = strTitle;
            MIndex = index;

            for (int i = 0; i < listName.Length; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10) });

                Button btn = new Button { Content = listName[i] };
                btn.Click += new RoutedEventHandler(btnValve_Click);
                Grid.SetColumn(btn, i * 2);

                if (i == index)
                {
                    btn.Foreground = Brushes.Blue;
                }

                grid.Children.Add(btn);
            }

            if (s_multipleDelay)
            {
                rbtnDelayMultiple.IsChecked = true;
            }
            else
            {
                rbtnRealMultiple.IsChecked = true;
            }
            for (int i = 1; i < listName.Length; i++)
            {
                cboxSelect.Items.Add(listName[i]);
            }
            cboxSelect.SelectedIndex = s_multipleIndex;
            doubleVol.Value = s_multipleVol;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (null == MCollectionValve || 0 == MCollectionValve.MList.Count || !MCollectionValve.MSignal)
            {
                if (1 == cboxSelect.Items.Count)
                {
                    groupMultipleSelect.Visibility = Visibility.Collapsed;
                }
                btnIntervene.Visibility = Visibility.Collapsed;
            }
            else
            {
                groupMultipleSelect.Visibility = Visibility.Collapsed;
            }
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
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 选中阀位并且关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnValve_Click(object sender, RoutedEventArgs e)
        {
            if (MIndex != grid.Children.IndexOf((Button)sender))
            {
                AuditTrails.AuditTrailsStatic.Instance().InsertRowManual(this.Title, this.title.Text + " : " + ((Button)grid.Children[MIndex]).Content.ToString() + " -> " + ((Button)sender).Content.ToString());
                MIndex = grid.Children.IndexOf((Button)sender);

                if (true == rbtnDelaySingle.IsChecked)
                {
                    RoutedEventArgs args = new RoutedEventArgs(MSingleEvent, MIndex);
                    RaiseEvent(args);

                    DialogResult = false;
                }
                else
                {
                    DialogResult = true;
                } 
            }
            else
            {
                DialogResult = false;
            }
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

            AuditTrailsStatic.Instance().InsertRowColl(ReadXamlCollection.S_CollMarkA, Title + btnStart.Content.ToString());

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

            AuditTrailsStatic.Instance().InsertRowColl(ReadXamlCollection.S_CollMarkA, Title + btnStop.Content.ToString());

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
            if (0 < MCollectionValve.MList.Count)
            {
                CollectionItemWin win = new CollectionItemWin();
                List<string> tmp = Communication.EnumOutInfo.NameList.ToList();
                tmp.RemoveAt(0);
                win.MTubeNameList = tmp;
                win.MItem = MCollectionValve.MList[MCollectionValve.MIndex - 1];
                if (true == win.ShowDialog())
                {
                    MCollectionValve.MList[MCollectionValve.MIndex - 1] = win.MItem;
                }
            }

            DialogResult = false;
        }
    }
}
