using HBBio.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// ValveWin.xaml 的交互逻辑
    /// </summary>
    public partial class ValveWin : Window
    {
        public int MIndex { get; set; }
        public string MOper { get; set; }
        public Visibility MRealDelayVisibility
        {
            set
            {
                rbtnReal.Visibility = value;
                rbtnDelay.Visibility = value;
            }
        }
        public bool MIsDelay
        {
            get
            {
                return true == rbtnDelay.IsChecked;
            }
        }

        public CollectionValve MCollectionValve { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="strTitle"></param>
        /// <param name="listName"></param>
        /// <param name="index"></param>
        public ValveWin(Window parent, string strTitle, string[] listName, int index)
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
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (null == MCollectionValve || 0 == MCollectionValve.MList.Count || !MCollectionValve.MSignal)
            {
                btnIntervene.Visibility = Visibility.Collapsed;
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
                DialogResult = true;
            }
            else
            {
                DialogResult = false;
            }
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