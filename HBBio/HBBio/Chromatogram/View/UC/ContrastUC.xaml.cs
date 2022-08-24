using HBBio.Share;
using HBBio.SystemControl;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HBBio.Chromatogram
{
    /// <summary>
    /// ContrastUC.xaml 的交互逻辑
    /// </summary>
    public partial class ContrastUC : UserControl
    {
        private ContrastManager m_chromatogram = new ContrastManager();
        private Point m_clickPt = new Point(0, 0);
        private bool m_isPress = false;
        private bool m_isClicked = false;
        private bool m_isRulerOddMove = false;      //单标尺信号
        private bool m_isRulerEvenMove = false;     //双标尺信号

        private Thread m_drawThread;                //画谱图文件线程
        private volatile bool m_draw = true;        //画谱图文件线程的信号位


        /// <summary>
        /// 构造函数
        /// </summary>
        public ContrastUC()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化数据框架
        /// </summary>
        /// <param name="listName"></param>
        /// <param name="m_listContrast"></param>
        /// <param name="listCurveSet"></param>
        public void InitDataFrame(List<string> listName, List<bool> listContrast, List<CurveSet> listCurveSet)
        {
            for (int i = 0; i < listName.Count; i++)
            {
                MenuItem item = new MenuItem();
                item.Header = listName[i];
                item.Tag = i;
                item.IsCheckable = true;
                item.IsChecked = listContrast[i];
                item.Click += menuSelect_Click;

                menuSelect.Items.Add(item);
            }

            foreach(var it in listCurveSet)
            {
                m_chromatogram.MListLines.Add(it);
            }
            m_chromatogram.m_listContrast = listContrast;

            CalItemListShow();
        }

        /// <summary>
        /// 新增信号数据
        /// </summary>
        /// <param name="info"></param>
        public void AddLineItemData(int index)
        {
            m_chromatogram.MListLines[index].RestoreLineItemData();
        }

        public List<List<double>> MListList(int index)
        {
            return m_chromatogram.MListLines[index].MListList;
        }

        /// <summary>
        /// 设置标记数据
        /// </summary>
        /// <param name="markerInfo"></param>
        public void SetMarkerInfo(string markerInfo)
        {
            m_chromatogram.MMarkerInfo = markerInfo;
        }

        /// <summary>
        /// 新增标记数据
        /// </summary>
        /// <param name="item"></param>
        public void AddMarker(MarkerInfo item)
        {
            m_chromatogram.AddMarker(item);
            UpdateDraw();
        }

        /// <summary>
        /// 删除标记数据
        /// </summary>
        /// <param name="index"></param>
        private void DelMarker(int index)
        {
            m_chromatogram.DelMarker(index);
            UpdateDraw();
        }

        /// <summary>
        /// 重新计算各曲线的最值
        /// </summary>
        public void CalMaxMin()
        {
            m_chromatogram.CalMaxMin();
        }

        /// <summary>
        /// 画图信号置为1
        /// </summary>
        public void UpdateDraw()
        {
            m_draw = true;
        }

        /// <summary>
        /// 启动线程
        /// </summary>
        public void StartThread()
        {
            UpdateDraw();

            if (null == m_drawThread)
            {
                m_drawThread = new Thread(ThreadDrawFun);
                m_drawThread.IsBackground = true;
                m_drawThread.Start();
            }
        }

        /// <summary>
        /// 更新谱图曲线基本信息
        /// </summary>
        private void CalItemListShow()
        {
            m_chromatogram.CalItemListShow();

            //更新颜色按钮面板
            stackPanel.Children.Clear();
            for (int i = 0; i < m_chromatogram.m_listContrast.Count; i++)
            {
                if (m_chromatogram.m_listContrast[i])
                {
                    for (int j = 0; j < m_chromatogram.MListLines.Count; j++)
                    {
                        ColorButton btn = new ColorButton(i * m_chromatogram.MListLines.Count + j, m_chromatogram.MListLines[j].MItemList[i].MName, m_chromatogram.MListLines[j].MItemList[i].MColor);
                        btn.Click += new RoutedEventHandler(SignalBtn_Click);
                        stackPanel.Children.Add(btn);
                    }
                }
            }

            UpdateDraw();
        }

        /// <summary>
        /// 谱图文件运行的线程函数
        /// </summary>
        private void ThreadDrawFun()
        {
            while (true)
            {
                try
                {
                    if (m_draw)
                    {
                        m_draw = false;
                        image.Dispatcher.Invoke(new Action(delegate
                        {
                            image.Source = m_chromatogram.DrawBitmap();
                        }));
                    }
                    else
                    {
                        Thread.Sleep(DlyBase.c_sleep1);
                    }
                }
                catch (Exception ex)
                {
                    SystemLog.SystemLogManager.LogWrite(ex);
                }
            }
        }


        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            m_chromatogram.InitdpiXY(this);    
        }

        /// <summary>
        /// 切换当前选中颜色按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignalBtn_Click(object sender, RoutedEventArgs e)
        {
            m_chromatogram.MSelectIndex = ((ColorButton)sender).MIndex % m_chromatogram.MListLines.Count;
            m_chromatogram.MLines.MSelectIndex = ((ColorButton)sender).MIndex / m_chromatogram.MListLines.Count;
            UpdateDraw();
        }

        /// <summary>
        /// 窗体尺寸变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //70，40的数字为自定义
            m_chromatogram.UpdateCanvas(image, (int)canvas.ActualWidth, (int)canvas.ActualHeight, 70, 40);
            lineRulerOdd.Y2 = canvas.ActualHeight - 40;
            lineRulerEven.Y2 = lineRulerOdd.Y2;
            txtRulerInfo.SetValue(Canvas.LeftProperty, canvas.ActualWidth - txtRulerInfo.Width);
            txtRulerInfo.SetValue(Canvas.TopProperty, 0.0);
            UpdateDraw();
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MouseButtonState.Pressed == e.RightButton)
            {
                btnMenu_Click(null, null);
                return;
            }

            m_isPress = true;

            Point pt = e.GetPosition(canvas);

            if ((Visibility.Visible == lineRulerOdd.Visibility || Visibility.Visible == lineRulerEven.Visibility) && Math.Abs(pt.X - lineRulerOdd.X1) < 10)//移动单标尺
            {
                m_isRulerOddMove = true;
            }
            else if (Visibility.Visible == lineRulerEven.Visibility && Math.Abs(pt.X - lineRulerEven.X1) < 10)//移动双标尺
            {
                m_isRulerEvenMove = true;
            }
            else if (m_chromatogram.IsSwitchAxisX(pt))//切换X轴
            {
                UpdateDraw();
            }
            else//放大
            {
                m_clickPt = pt;
                m_isClicked = true;
                Canvas.SetLeft(rectZoom, m_clickPt.X);
                Canvas.SetTop(rectZoom, m_clickPt.Y);
            }
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!m_isPress)
            {
                return;
            }

            Point pt = e.GetPosition(canvas);

            if (m_isRulerOddMove)//移动单标尺
            {
                if (pt.X >= m_chromatogram.MChartLeft && pt.X <= m_chromatogram.MChartRight)
                {
                    lineRulerOdd.X1 = pt.X;
                    lineRulerOdd.X2 = lineRulerOdd.X1;
                }

                if (Visibility.Visible == lineRulerEven.Visibility)
                {
                    txtRulerInfo.Text = m_chromatogram.GetRulerEven(lineRulerOdd.X1, lineRulerEven.X1);
                }
                else
                {
                    txtRulerInfo.Text = m_chromatogram.GetRulerOdd(lineRulerOdd.X1);
                }
            }
            else if (m_isRulerEvenMove)//移动双标尺
            {
                if (pt.X >= m_chromatogram.MChartLeft && pt.X <= m_chromatogram.MChartRight)
                {
                    lineRulerEven.X1 = pt.X;
                    lineRulerEven.X2 = lineRulerEven.X1;
                }
                
                txtRulerInfo.Text = m_chromatogram.GetRulerEven(lineRulerOdd.X1, lineRulerEven.X1);
            }
            else if (m_isClicked)//放大
            {
                if (pt.X - m_clickPt.X > 5 && pt.Y - m_clickPt.Y > 5)
                {
                    rectZoom.Visibility = Visibility.Visible;
                    rectZoom.Width = pt.X - m_clickPt.X;
                    rectZoom.Height = pt.Y - m_clickPt.Y;
                }
            }
        }

        /// <summary>
        /// 鼠标弹起
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            m_isPress = false;

            if (m_isRulerOddMove)//移动单标尺
            {
                m_isRulerOddMove = false;
            }
            else if (m_isRulerEvenMove)//移动双标尺
            {
                m_isRulerEvenMove = false;
            }
            else if (m_isClicked)//放大
            {
                m_isClicked = false;

                rectZoom.Visibility = Visibility.Collapsed;
                m_chromatogram.AddScale(new System.Drawing.Rectangle((int)m_clickPt.X, (int)m_clickPt.Y, (int)e.GetPosition(canvas).X - (int)m_clickPt.X, (int)e.GetPosition(canvas).Y - (int)m_clickPt.Y));
                UpdateDraw();
            }
        }

        /// <summary>
        /// 自定义按钮禁用菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMenu_Initialized(object sender, EventArgs e)
        {
            this.btnMenu.ContextMenu = null;
        }

        /// <summary>
        /// 自定义按钮弹出菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            menuUndoZoom.IsEnabled = m_chromatogram.MEnabledZoom;
            menuResetZoom.IsEnabled = m_chromatogram.MEnabledZoom;

            //目标
            this.menu.PlacementTarget = this.btnMenu;
            //位置
            this.menu.Placement = System.Windows.Controls.Primitives.PlacementMode.Right;
            //显示菜单
            this.menu.IsOpen = true;
        }

        /// <summary>
        /// 取消放大
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuUndoZoom_Click(object sender, RoutedEventArgs e)
        {
            m_chromatogram.RemoveScale();
            UpdateDraw();
        }

        /// <summary>
        /// 重置缩放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuResetZoom_Click(object sender, RoutedEventArgs e)
        {
            m_chromatogram.ClearScale();
            UpdateDraw();
        }

        /// <summary>
        /// 网格线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuGrid_Click(object sender, RoutedEventArgs e)
        {
            m_chromatogram.MShowGrid = menuGrid.IsChecked;
            UpdateDraw();
        }

        /// <summary>
        /// 单标尺
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuRulerOdd_Click(object sender, RoutedEventArgs e)
        {
            if (menuRulerOdd.IsChecked)
            {
                menuRulerEven.IsChecked = false;

                lineRulerOdd.Visibility = Visibility.Visible;
                lineRulerEven.Visibility = Visibility.Collapsed;
                txtRulerInfo.Visibility = Visibility.Visible;
                lineRulerOdd.X1 = 110;
                lineRulerOdd.X2 = lineRulerOdd.X1;

                txtRulerInfo.Text = m_chromatogram.GetRulerOdd(lineRulerOdd.X1);
            }
            else
            {
                lineRulerOdd.Visibility = Visibility.Collapsed;
                lineRulerEven.Visibility = Visibility.Collapsed;
                txtRulerInfo.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 双标尺
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuRulerEven_Click(object sender, RoutedEventArgs e)
        {
            if (menuRulerEven.IsChecked)
            {
                menuRulerOdd.IsChecked = false;

                lineRulerOdd.Visibility = Visibility.Visible;
                lineRulerEven.Visibility = Visibility.Visible;
                txtRulerInfo.Visibility = Visibility.Visible;
                lineRulerOdd.X1 = 110;
                lineRulerOdd.X2 = lineRulerOdd.X1;
                lineRulerEven.X1 = 130;
                lineRulerEven.X2 = lineRulerEven.X1;

                txtRulerInfo.Text = m_chromatogram.GetRulerEven(lineRulerOdd.X1, lineRulerEven.X1);
            }
            else
            {
                lineRulerOdd.Visibility = Visibility.Collapsed;
                lineRulerEven.Visibility = Visibility.Collapsed;
                txtRulerInfo.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 标记添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuMarkerAdd_Click(object sender, RoutedEventArgs e)
        {
            MarkerAddWin win = new MarkerAddWin(m_chromatogram.MLines.MUnit);
            win.ShowInTaskbar = false;
            if (true == win.ShowDialog())
            {
                AddMarker(win.MMarkerInfo);
            }
        }

        /// <summary>
        /// 标记删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuMarkerDel_Click(object sender, RoutedEventArgs e)
        {
            List<StringBool> markerList = new List<StringBool>();
            foreach (var it in m_chromatogram.MMarkerList)
            {
                markerList.Add(new StringBool(it.GetValByBase(m_chromatogram.MLines.MBase) + "    " + m_chromatogram.MLines.MUnit + "    " + it.MType, true));
            }

            MarkerDelWin win = new MarkerDelWin(markerList);
            win.ShowInTaskbar = false;
            if (true == win.ShowDialog())
            {
                for (int i = markerList.Count - 1; i >= 0; i--)
                {
                    if (markerList[i].MCheck)
                    {
                        DelMarker(i);
                    }
                }
            }
        }

        /// <summary>
        /// 曲线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuCurve_Click(object sender, RoutedEventArgs e)
        {
            CurveSetStyle style = new CurveSetStyle();
            style.MAxisScale = m_chromatogram.MLines.MAxisScale;
            style.MMin = m_chromatogram.MLines.MMin;
            style.MMax = m_chromatogram.MLines.MMax;
            for (int i = 0; i < m_chromatogram.m_listContrast.Count; i++)
            {
                if (m_chromatogram.m_listContrast[i])
                {
                    foreach (var it in m_chromatogram.MListLines)
                    {
                        CurveStyle chromatogramY = new CurveStyle();
                        chromatogramY.MName = it.MItemList[i].MName;
                        chromatogramY.MUnit = it.MItemList[i].MUnit;
                        chromatogramY.MBrush = new SolidColorBrush(Share.ValueTrans.DrawToMedia(it.MItemList[i].MColor));
                        chromatogramY.MShow = it.MItemList[i].MShow;
                        chromatogramY.MAxisScale = it.MItemList[i].MAxisScale;
                        chromatogramY.MMin = it.MItemList[i].MMin;
                        chromatogramY.MMax = it.MItemList[i].MMax;
                        style.MList.Add(chromatogramY);
                    }
                }
            }

            CurveColorWin win = new CurveColorWin();
            win.DataContext = new CurveSetStyleVM(style);
            if (true == win.ShowDialog())
            {
                if (m_chromatogram.MLines.MAxisScale != style.MAxisScale)
                {
                    m_chromatogram.MLines.MAxisScale = style.MAxisScale;
                }

                switch (m_chromatogram.MLines.MAxisScale)
                {
                    case EnumAxisScale.Fixed:
                        m_chromatogram.MLines.MMinFix = style.MMin;
                        m_chromatogram.MLines.MMaxFix = style.MMax;
                        break;
                }

                int ij = 0;
                for (int i = 0; i < m_chromatogram.m_listContrast.Count; i++)
                {
                    if (m_chromatogram.m_listContrast[i])
                    {
                        for (int j = 0; j < m_chromatogram.MListLines.Count; j++)
                        {
                            m_chromatogram.MListLines[j].MItemList[i].MColor = Share.ValueTrans.MediaToDraw(((SolidColorBrush)style.MList[ij].MBrush).Color);
                            m_chromatogram.MListLines[j].MItemList[i].MAxisScale = style.MList[ij].MAxisScale;
                            if (EnumAxisScale.Fixed == style.MList[i].MAxisScale)
                            {
                                m_chromatogram.MListLines[j].MItemList[i].MMinFix = style.MList[ij].MMin;
                                m_chromatogram.MListLines[j].MItemList[i].MMaxFix = style.MList[ij].MMax;
                            }
                            ij++;
                        }
                    }
                }

                //for (int j = 0; j < m_chromatogram.MListLines.Count; j++)
                //{
                //    for (int i = 0; i < m_chromatogram.MListLines[j].MItemList.Count; i++)
                //    {
                //        m_chromatogram.MListLines[j].MItemList[i].MColor = Share.ValueTrans.MediaToDraw(((SolidColorBrush)style.MList[j].MBrush).Color);
                //    }
                //}

                //for (int i = 0; i < style.MList.Count; i++)
                //{
                //    m_chromatogram.MListLines[i].MSelectItem.MAxisScale = style.MList[i].MAxisScale;
                //    if (EnumAxisScale.Fixed == style.MList[i].MAxisScale)
                //    {
                //        m_chromatogram.MListLines[i].MSelectItem.MMinFix = style.MList[i].MMin;
                //        m_chromatogram.MListLines[i].MSelectItem.MMaxFix = style.MList[i].MMax;
                //    }
                //}

                CalItemListShow();
            }
        }

        private void menuSelect_Click(object sender, RoutedEventArgs e)
        {
            m_chromatogram.m_listContrast[Convert.ToInt32(((MenuItem)sender).Tag)] = ((MenuItem)sender).IsChecked;

            for (int i = 0; i < m_chromatogram.m_listContrast.Count; i++)
            {
                if (m_chromatogram.m_listContrast[i])
                {
                    for (int j = 0; j < m_chromatogram.m_listContrast.Count; j++)
                    {
                        SystemControlManager.s_comconfStatic.m_signalList[j].MContrastOld = m_chromatogram.m_listContrast[j];
                    }
                    SystemControlManager.s_comconfStatic.UpdateSignalList();

                    CalItemListShow();
                    return;
                }
            }

            ((MenuItem)sender).IsChecked = !((MenuItem)sender).IsChecked;
        }
    }
}
