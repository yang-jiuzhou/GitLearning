using HBBio.Share;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// ChromatogramUC.xaml 的交互逻辑
    /// </summary>
    public partial class ChromatogramUC : UserControl
    {
        private ChromatogramManager m_chromatogram = new ChromatogramManager();
        private Point m_clickPt = new Point(0, 0);
        private bool m_isClicked = false;
        private bool m_isPeakStartMove = false;     //峰前虚线信号
        private bool m_isPeakEndMove = false;       //峰后虚线信号
        private bool m_isRulerOddMove = false;      //单标尺信号
        private bool m_isRulerEvenMove = false;     //双标尺信号
        private bool m_isDrag = false;              //拖拽信号
        private bool m_isDragIng = false;           //是否正在拖拽

        private Thread m_drawThread;                //画谱图文件线程
        private volatile bool m_draw = true;        //画谱图文件线程的信号位

        private DateTime m_canvasMouseDownTime = DateTime.Now;  //鼠标按下的时间
        private Point m_canvasMouseDownPt = new Point(0, 0);    //鼠标按下的位置

        /// <summary>
        /// 属性，是否实时谱图
        /// </summary>
        private bool m_isReal = true;
        public bool IsReal
        {
            get
            {
                return m_isReal;
            }
            set
            {
                m_isReal = value;

                if (value)
                {
                    menuBG.Visibility = Visibility.Visible;
                }
                else
                {
                    menuBG.Visibility = Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        /// 属性，设置曲线属性设置是否可用
        /// </summary>
        public bool CurveSetIsEnabled
        {
            get
            {
                return menuCurve.IsEnabled;
            }
            set
            {
                menuCurve.IsEnabled = value;
            }
        }

        /// <summary>
        /// 属性，设置峰前虚线
        /// </summary>
        public bool MVisiblePeakStart
        {
            set
            {
                linePeakStart.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 属性，设置峰后虚线
        /// </summary>
        public bool MVisiblePeakEnd
        {
            set
            {
                linePeakEnd.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 属性，标记列表
        /// </summary>
        public List<MarkerInfo> MListMarker
        {
            get
            {
                return m_chromatogram.MMarkerList;
            }
        }

        public EnumBase MEnumBase
        {
            get
            {
                return m_chromatogram.MLines.MBase;
            }
        }

        public string MStrBase
        {
            get
            {
                return m_chromatogram.MLines.MUnit;
            }
        }

        public List<string> MListCurveName
        {
            get
            {
                return m_chromatogram.MLines.MListCurveName;
            }
        }
        public List<List<double>> MListList
        {
            get
            {
                return m_chromatogram.MLines.MListList;
            }
        }
        public List<List<double>> MBGListList
        {
            get
            {
                return m_chromatogram.MBGLines.MListList;
            }
        }

        /// <summary>
        /// 自定义事件，修改曲线时触发
        /// </summary>
        public static readonly RoutedEvent MUpdateCurveEvent =
             EventManager.RegisterRoutedEvent("MUpdateCurve", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ChromatogramUC));
        public event RoutedEventHandler MUpdateCurve
        {
            add { AddHandler(MUpdateCurveEvent, value); }
            remove { RemoveHandler(MUpdateCurveEvent, value); }
        }

        /// <summary>
        /// 自定义事件，修改曲线最值时触发
        /// </summary>
        public static readonly RoutedEvent MUpdateAxisScaleEvent =
             EventManager.RegisterRoutedEvent("MUpdateAxisScale", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ChromatogramUC));
        public event RoutedEventHandler MUpdateAxisScale
        {
            add { AddHandler(MUpdateAxisScaleEvent, value); }
            remove { RemoveHandler(MUpdateAxisScaleEvent, value); }
        }

        /// <summary>
        /// 自定义事件，修改标记时触发
        /// </summary>
        public static readonly RoutedEvent MUpdateMarkerEvent =
             EventManager.RegisterRoutedEvent("MUpdateMarker", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ChromatogramUC));
        public event RoutedEventHandler MUpdateMarker
        {
            add { AddHandler(MUpdateMarkerEvent, value); }
            remove { RemoveHandler(MUpdateMarkerEvent, value); }
        }

        /// <summary>
        /// 自定义事件，修改选中曲线时触发
        /// </summary>
        public static readonly RoutedEvent MUpdateSelectEvent =
             EventManager.RegisterRoutedEvent("MUpdateSelect", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ChromatogramUC));
        public event RoutedEventHandler MUpdateSelect
        {
            add { AddHandler(MUpdateSelectEvent, value); }
            remove { RemoveHandler(MUpdateSelectEvent, value); }
        }

        /// <summary>
        /// 自定义事件，修改X坐标单位时触发
        /// </summary>
        public static readonly RoutedEvent MUpdateAxisEvent =
             EventManager.RegisterRoutedEvent("MUpdateAxis", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ChromatogramUC));
        public event RoutedEventHandler MUpdateAxis
        {
            add { AddHandler(MUpdateAxisEvent, value); }
            remove { RemoveHandler(MUpdateAxisEvent, value); }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ChromatogramUC()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 数据清零
        /// </summary>
        public void Clear()
        {
            m_chromatogram.Clear();
        }

        /// <summary>
        /// 初始化谱图曲线基本信息
        /// </summary>
        /// <param name="list"></param>
        public void InitDataFrame(List<Curve> list, List<Curve> listBG, AxisScale axisScale)
        {
            m_chromatogram.InitLineList(list, listBG, axisScale);

            //初始化颜色按钮面板
            stackPanel.Children.Clear();
            int index = 0;
            foreach (var it in m_chromatogram.MLines.MItemList)
            {
                ColorButton btn = new ColorButton(index, it.MName, it.MColor);
                btn.Click += new RoutedEventHandler(SignalBtn_Click);
                stackPanel.Children.Add(btn);

                ++index;
            }

            CalItemListShow(true);
        }

        /// <summary>
        /// 新增信号数据(实时)
        /// </summary>
        /// <param name="info"></param>
        public void AddLineItemData(double t, double v, double cv, List<double> dataList)
        {
            m_chromatogram.AddLineItemData(t, v, cv, dataList);
        }

        /// <summary>
        /// 还原信号数据(历史)
        /// </summary>
        public void RestoreLineItemData()
        {
            m_chromatogram.RestoreLineItemData();
        }

        /// <summary>
        /// 新增背景信号数据
        /// </summary>
        /// <param name="info"></param>
        public void RestoreBGLineItemData()
        {
            m_chromatogram.RestoreBGLineItemData();
        }
        public void ClearBGLineItemData()
        {
            m_chromatogram.ClearBGLineItemData();
        }
        public void SetLineAndBGLine(bool flag)
        {
            m_chromatogram.SetLineAndBGLine(flag);
        }

        /// <summary>
        /// 初始化判峰所需源数据
        /// </summary>
        /// <param name="listPeak"></param>
        public void CalPeak(List<Evaluation.PeakValue> listPeak, double ch)
        {
            m_chromatogram.MLines.CalPeak(listPeak, ch);
        }

        public WriteableBitmap GetBitmapUp(int width, int height)
        {
            m_chromatogram.UpdateCanvas(image, width, height, 70, 40);
            WriteableBitmap bmp = m_chromatogram.DrawCurveNames();
            return bmp;
        }

        public WriteableBitmap GetBitmapDown(int width, int height)
        {
            WriteableBitmap bmp = m_chromatogram.DrawBitmap();
            m_chromatogram.UpdateCanvas(image, (int)canvas.ActualWidth, (int)canvas.ActualHeight, 70, 40);
            return bmp;
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

            if (m_isReal)
            {
                RoutedEventArgs args = new RoutedEventArgs(MUpdateMarkerEvent, m_chromatogram.MMarkerInfo);
                RaiseEvent(args);
            }
        }

        /// <summary>
        /// 还原标记数据
        /// </summary>
        /// <param name="item"></param>
        private void RestoreMarker(MarkerInfo item)
        {
            m_chromatogram.AddMarker(item);
        }

        /// <summary>
        /// 删除标记数据
        /// </summary>
        /// <param name="index"></param>
        private void DelMarker(int index)
        {
            m_chromatogram.DelMarker(index);
            UpdateDraw();

            if (m_isReal)
            {
                RoutedEventArgs args = new RoutedEventArgs(MUpdateMarkerEvent, m_chromatogram.MMarkerInfo);
                RaiseEvent(args);
            }
        }

        /// <summary>
        /// 新增收集数据
        /// </summary>
        /// <param name="item"></param>
        public void AddCollM(MarkerInfo item)
        {
            m_chromatogram.AddCollM(item);
            UpdateDraw();
        }
        public void AddCollA(MarkerInfo item)
        {
            m_chromatogram.AddCollA(item);
            UpdateDraw();
        }

        /// <summary>
        /// 还原收集数据
        /// </summary>
        /// <param name="item"></param>
        public void RestoreCollM(MarkerInfo item)
        {
            m_chromatogram.AddCollM(item);
        }
        public void RestoreCollA(MarkerInfo item)
        {
            m_chromatogram.AddCollA(item);
        }

        /// <summary>
        /// 新增切阀数据
        /// </summary>
        /// <param name="item"></param>
        public void AddValve(MarkerInfo item)
        {
            m_chromatogram.AddValve(item);
            UpdateDraw();
        }

        /// <summary>
        /// 还原切阀数据
        /// </summary>
        /// <param name="item"></param>
        public void RestoreValve(MarkerInfo item)
        {
            m_chromatogram.AddValve(item);
        }

        /// <summary>
        /// 新增方法行数据
        /// </summary>
        /// <param name="item"></param>
        public void AddPhase(MarkerInfo item)
        {
            m_chromatogram.AddPhase(item);
            UpdateDraw();
        }

        /// <summary>
        /// 还原方法行数据
        /// </summary>
        /// <param name="item"></param>
        public void RestorePhase(MarkerInfo item)
        {
            m_chromatogram.AddPhase(item);
        }

        /// <summary>
        /// 曲线设置
        /// </summary>
        public void CurveSet()
        {
            menuCurve_Click(null, null);
        }

        public int GetPeakStartIndex()
        {
            return m_chromatogram.GetIndex(linePeakStart.X1);
        }

        public int GetPeakEndIndex()
        {
            return m_chromatogram.GetIndex(linePeakEnd.X1);
        }

        public bool GetCurveVisible(int index)
        {
            return m_chromatogram.MLines.MItemList[index].MShow;
        }

        public void SetPeakIndex(int index)
        {
            m_chromatogram.MLines.MSelectPeakIndex = index;
            UpdateDraw();
        }

        /// <summary>
        /// 画图信号置为1
        /// </summary>
        public void UpdateDraw()
        {
            m_draw = true;
        }

        /// <summary>
        /// 更新谱图曲线基本信息
        /// </summary>
        private void CalItemListShow(bool findFirst)
        {
            m_chromatogram.CalItemListShow();

            for (int i = 0; i < m_chromatogram.MLines.MItemList.Count; i++)
            {
                stackPanel.Children[i].Visibility = m_chromatogram.MLines.MItemList[i].MShow ? Visibility.Visible : Visibility.Collapsed;
                ((ColorButton)stackPanel.Children[i]).MName = m_chromatogram.MLines.MItemList[i].MName;
                ((ColorButton)stackPanel.Children[i]).MColor = m_chromatogram.MLines.MItemList[i].MColor;
                if (findFirst && m_chromatogram.MLines.MItemList[i].MShow)
                {
                    findFirst = false;
                    m_chromatogram.MLines.MSelectIndex = i;
                    SignalBtn_Click(((ColorButton)stackPanel.Children[m_chromatogram.MLines.MSelectIndex]), null);
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
            m_chromatogram.MBackgroundInfo = m_chromatogram.GetBackgroundColor();
            
            m_drawThread = new Thread(ThreadDrawFun);
            m_drawThread.IsBackground = true;
            m_drawThread.Start();

            m_chromatogram.InitdpiXY(this);    
        }

        /// <summary>
        /// 切换当前选中颜色按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignalBtn_Click(object sender, RoutedEventArgs e)
        {
            if (-1 != m_chromatogram.MLines.MSelectIndex)
            {
                ((ColorButton)stackPanel.Children[m_chromatogram.MLines.MSelectIndex]).Background = new SolidColorBrush(Colors.Transparent);
            }
            ((ColorButton)sender).Background = new SolidColorBrush(Color.FromArgb(50, 0, 0, 255));

            m_chromatogram.MLines.MSelectIndex = ((ColorButton)sender).MIndex;

            RoutedEventArgs args = new RoutedEventArgs(MUpdateSelectEvent, ((ColorButton)sender).MIndex);
            RaiseEvent(args);

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
            linePeakStart.Y2 = lineRulerOdd.Y2;
            linePeakEnd.Y2 = lineRulerOdd.Y2;
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

            Point pt = e.GetPosition(canvas);

            TimeSpan secondSpan = new TimeSpan(DateTime.Now.Ticks - m_canvasMouseDownTime.Ticks);
            if (secondSpan.TotalSeconds < 0.5 && Math.Abs(pt.X - m_canvasMouseDownPt.X) < 5 && Math.Abs(pt.Y - m_canvasMouseDownPt.Y) < 5)
            {
                m_canvasMouseDownTime = DateTime.Now;
                m_canvasMouseDownPt = pt;

                m_chromatogram.ClearScale();
                UpdateDraw();

                return;
            }
            else
            {
                m_canvasMouseDownTime = DateTime.Now;
                m_canvasMouseDownPt = pt;
            }

            if ((Visibility.Visible == linePeakStart.Visibility || Visibility.Visible == linePeakEnd.Visibility) && Math.Abs(pt.X - linePeakStart.X1) < 10)//移动峰前虚线
            {
                m_isPeakStartMove = true;
            }
            else if((Visibility.Visible == linePeakStart.Visibility || Visibility.Visible == linePeakEnd.Visibility) && Math.Abs(pt.X - linePeakEnd.X1) < 10)//移动单标尺
            {
                m_isPeakEndMove = true;
            }
            else if ((Visibility.Visible == lineRulerOdd.Visibility || Visibility.Visible == lineRulerEven.Visibility) && Math.Abs(pt.X - lineRulerOdd.X1) < 10)//移动单标尺
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

                RoutedEventArgs args = new RoutedEventArgs(MUpdateAxisEvent, m_chromatogram.MLines.MBase);
                RaiseEvent(args);
            }
            else//放大
            {
                m_clickPt = pt;

                if (!m_isDrag)
                {
                    m_isClicked = true;
                    Canvas.SetLeft(rectZoom, m_clickPt.X);
                    Canvas.SetTop(rectZoom, m_clickPt.Y);
                }
                else
                {
                    m_isDragIng = true;
                }
            }
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point pt = e.GetPosition(canvas);

            if (m_isPeakStartMove)//移动峰前虚线
            {
                if (pt.X >= m_chromatogram.MChartLeft && pt.X <= m_chromatogram.MChartRight)
                {
                    linePeakStart.X1 = pt.X;
                    linePeakStart.X2 = pt.X;
                    txtPeakStart.SetValue(Canvas.LeftProperty, pt.X);
                    txtPeakStart.Text = m_chromatogram.GetRulerOddO(pt.X);
                }
            }
            else if (m_isPeakEndMove)//移动峰前虚线
            {
                if (pt.X >= m_chromatogram.MChartLeft && pt.X <= m_chromatogram.MChartRight)
                {
                    linePeakEnd.X1 = pt.X;
                    linePeakEnd.X2 = pt.X;
                    txtPeakEnd.SetValue(Canvas.LeftProperty, pt.X);
                    txtPeakEnd.Text = m_chromatogram.GetRulerOddO(pt.X);
                }
            }
            else if(m_isRulerOddMove)//移动单标尺
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
                    if (menuRulerOddO.IsChecked)
                    {
                        txtRulerInfo.Text = m_chromatogram.GetRulerOddO(lineRulerOdd.X1);
                    }
                    else
                    {
                        txtRulerInfo.Text = m_chromatogram.GetRulerOddM(lineRulerOdd.X1);
                    }
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
            else if(m_isDragIng)
            {
                m_chromatogram.DragMove(m_clickPt.X, pt.X, m_clickPt.Y, pt.Y);
                UpdateDraw();
            }
        }

        /// <summary>
        /// 鼠标弹起
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Point pt = e.GetPosition(canvas);

            if (m_isPeakStartMove)//移动峰前虚线
            {
                m_isPeakStartMove = false;
            }
            else if (m_isPeakEndMove)//移动峰后虚线
            {
                m_isPeakEndMove = false;
            }
            else if (m_isRulerOddMove)//移动单标尺
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
            else if (m_isDragIng)
            {
                m_isDragIng = false;
                m_chromatogram.DragUp(m_clickPt.X, pt.X, m_clickPt.Y, pt.Y);
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
            menuDrag.IsEnabled = m_chromatogram.MEnabledZoom;
            
            if (null != sender)
            {
                //目标
                this.menu.PlacementTarget = this.btnMenu;
                //位置
                this.menu.Placement = System.Windows.Controls.Primitives.PlacementMode.Right;
            }
            else
            {
                //目标
                this.menu.PlacementTarget = this.btnMenu;
                //位置
                this.menu.Placement = System.Windows.Controls.Primitives.PlacementMode.Right;
            }

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

            menuDrag.IsChecked = false;
            m_isDrag = menuDrag.IsChecked;
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

            menuDrag.IsChecked = false;
            m_isDrag = menuDrag.IsChecked;
        }

        /// <summary>
        /// 拖拽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuDrag_Click(object sender, RoutedEventArgs e)
        {
            m_isDrag = menuDrag.IsChecked;
            m_chromatogram.AddScale();
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
        /// 多Y轴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuGrid_MultiY(object sender, RoutedEventArgs e)
        {
            m_chromatogram.MMultiY = menuMultiY.IsChecked;
            m_chromatogram.CalItemListShow();
            UpdateDraw();
        }

        /// <summary>
        /// 单标尺(单)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuRulerOddO_Click(object sender, RoutedEventArgs e)
        {
            if (menuRulerOddO.IsChecked)
            {
                menuRulerOddM.IsChecked = false;
                menuRulerEven.IsChecked = false;

                lineRulerOdd.Visibility = Visibility.Visible;
                lineRulerEven.Visibility = Visibility.Collapsed;
                txtRulerInfo.Visibility = Visibility.Visible;

                if (0 == lineRulerOdd.X1)
                {
                    lineRulerOdd.X1 = (m_chromatogram.MChartLeft + m_chromatogram.MChartRight) / 2;
                    lineRulerOdd.X2 = lineRulerOdd.X1;
                }
                txtRulerInfo.Text = m_chromatogram.GetRulerOddO(lineRulerOdd.X1);
            }
            else
            {
                lineRulerOdd.Visibility = Visibility.Collapsed;
                lineRulerEven.Visibility = Visibility.Collapsed;
                txtRulerInfo.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 单标尺(多)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuRulerOddM_Click(object sender, RoutedEventArgs e)
        {
            if (menuRulerOddM.IsChecked)
            {
                menuRulerOddO.IsChecked = false;
                menuRulerEven.IsChecked = false;

                lineRulerOdd.Visibility = Visibility.Visible;
                lineRulerEven.Visibility = Visibility.Collapsed;
                txtRulerInfo.Visibility = Visibility.Visible;

                if (0 == lineRulerOdd.X1)
                {
                    lineRulerOdd.X1 = (m_chromatogram.MChartLeft + m_chromatogram.MChartRight) / 2;
                    lineRulerOdd.X2 = lineRulerOdd.X1;
                }
                txtRulerInfo.Text = m_chromatogram.GetRulerOddM(lineRulerOdd.X1);
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
                menuRulerOddO.IsChecked = false;
                menuRulerOddM.IsChecked = false;

                lineRulerOdd.Visibility = Visibility.Visible;
                lineRulerEven.Visibility = Visibility.Visible;
                txtRulerInfo.Visibility = Visibility.Visible;
                if (0 == lineRulerOdd.X1)
                {
                    lineRulerOdd.X1 = (m_chromatogram.MChartLeft + m_chromatogram.MChartRight) / 2;
                    lineRulerOdd.X2 = lineRulerOdd.X1;
                }
                lineRulerEven.X1 = lineRulerOdd.X1 + 35;
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
        /// 标记显隐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuMarkerVisibility_Click(object sender, RoutedEventArgs e)
        {
            m_chromatogram.m_visibleMarker = menuMarkerVisibility.IsChecked;
            UpdateDraw();
        }

        /// <summary>
        /// 标记颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuMarkerColor_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog dlg = new System.Windows.Forms.ColorDialog();
            dlg.Color = m_chromatogram.MBackgroundInfo.MMarkerColor;
            if (System.Windows.Forms.DialogResult.OK == dlg.ShowDialog())
            {
                m_chromatogram.MBackgroundInfo.MMarkerColor = dlg.Color;
                MApp.DoEvents();

                if (m_chromatogram.UpdateBackgroundColor(EnumBackground.Marker, m_chromatogram.MBackgroundInfo.MMarkerColor))
                {
                    UpdateDraw();
                }
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
                AuditTrails.AuditTrailsStatic.Instance().InsertRowOperate(win.Title, win.MLogOper);
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
                markerList.Add(new StringBool(it.GetValByBase(m_chromatogram.MLines.MBase) + " " + m_chromatogram.MLines.MUnit + "    " + it.MType, true));
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
                        AuditTrails.AuditTrailsStatic.Instance().InsertRowOperate(win.Title, markerList[i].MName);
                    }
                }
            }
        }

        /// <summary>
        /// 收集显隐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuCollVisibility_Click(object sender, RoutedEventArgs e)
        {
            m_chromatogram.m_visibleColl = menuCollVisibility.IsChecked;
            UpdateDraw();
        }

        /// <summary>
        /// 收集颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuCollColorM_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog dlg = new System.Windows.Forms.ColorDialog();
            dlg.Color = m_chromatogram.MBackgroundInfo.MCollColorM;
            if (System.Windows.Forms.DialogResult.OK == dlg.ShowDialog())
            {
                m_chromatogram.MBackgroundInfo.MCollColorM = dlg.Color;
                MApp.DoEvents();

                if (m_chromatogram.UpdateBackgroundColor(EnumBackground.Coll_M, m_chromatogram.MBackgroundInfo.MCollColorM))
                {
                    UpdateDraw();
                }
            }
        }
        private void menuCollColorA_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog dlg = new System.Windows.Forms.ColorDialog();
            dlg.Color = m_chromatogram.MBackgroundInfo.MCollColorA;
            if (System.Windows.Forms.DialogResult.OK == dlg.ShowDialog())
            {
                m_chromatogram.MBackgroundInfo.MCollColorA = dlg.Color;
                MApp.DoEvents();

                if (m_chromatogram.UpdateBackgroundColor(EnumBackground.Coll_A, m_chromatogram.MBackgroundInfo.MCollColorA))
                {
                    UpdateDraw();
                }
            }
        }

        /// <summary>
        /// 切阀显隐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuValveVisibility_Click(object sender, RoutedEventArgs e)
        {
            m_chromatogram.m_visibleValve = menuValveVisibility.IsChecked;
            UpdateDraw();
        }

        /// <summary>
        /// 切阀颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuValveColor_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog dlg = new System.Windows.Forms.ColorDialog();
            dlg.Color = m_chromatogram.MBackgroundInfo.MValveColor;
            if (System.Windows.Forms.DialogResult.OK == dlg.ShowDialog())
            {
                m_chromatogram.MBackgroundInfo.MValveColor = dlg.Color;
                MApp.DoEvents();

                if (m_chromatogram.UpdateBackgroundColor(EnumBackground.Phase, m_chromatogram.MBackgroundInfo.MValveColor))
                {
                    UpdateDraw();
                }
            }
        }

        /// <summary>
        /// 方法阶段显隐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuPhaseVisibility_Click(object sender, RoutedEventArgs e)
        {
            m_chromatogram.m_visiblePhase = menuPhaseVisibility.IsChecked;
            UpdateDraw();
        }

        /// <summary>
        /// 方法阶段颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuPhaseColor_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog dlg = new System.Windows.Forms.ColorDialog();
            dlg.Color = m_chromatogram.MBackgroundInfo.MPhaseColor;
            if (System.Windows.Forms.DialogResult.OK == dlg.ShowDialog())
            {
                m_chromatogram.MBackgroundInfo.MPhaseColor = dlg.Color;
                MApp.DoEvents();

                if(m_chromatogram.UpdateBackgroundColor(EnumBackground.Phase, m_chromatogram.MBackgroundInfo.MPhaseColor))
                {
                    UpdateDraw();
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
            foreach (var it in m_chromatogram.MLines.MItemList)
            {
                CurveStyle chromatogramY = new CurveStyle();
                chromatogramY.MName = it.MName;
                chromatogramY.MUnit = it.MUnit;
                chromatogramY.MBrush = new SolidColorBrush(Share.ValueTrans.DrawToMedia(it.MColor));
                chromatogramY.MShow = it.MShow;
                chromatogramY.MMin = it.MMin;
                chromatogramY.MMax = it.MMax;
                chromatogramY.MAxisScale = it.MAxisScale;//该行赋值必须放在最值的后面
                style.MList.Add(chromatogramY);
            }

            CurveSetStyleWin win = new CurveSetStyleWin();
            win.DataContext = new CurveSetStyleVM(style);
            if (true == win.ShowDialog())
            {
                List<AxisScale> listAxisScales = new List<AxisScale>();

                m_chromatogram.MLines.MAxisScale = style.MAxisScale;

                switch (m_chromatogram.MLines.MAxisScale)
                {
                    case EnumAxisScale.Fixed:
                        m_chromatogram.MLines.MMinFix = style.MMin;
                        m_chromatogram.MLines.MMaxFix = style.MMax;
                        break;
                }
                listAxisScales.Add(new AxisScale(style.MAxisScale, style.MMin, style.MMax));

                for (int i = 0; i < style.MList.Count; i++)
                {
                    m_chromatogram.MLines.MItemList[i].MName = style.MList[i].MName;
                    m_chromatogram.MLines.MItemList[i].MUnit = style.MList[i].MUnit;
                    m_chromatogram.MLines.MItemList[i].MColor = Share.ValueTrans.MediaToDraw(((SolidColorBrush)style.MList[i].MBrush).Color);
                    m_chromatogram.MLines.MItemList[i].MShow = style.MList[i].MShow;
                    m_chromatogram.MLines.MItemList[i].MAxisScale = style.MList[i].MAxisScale;
                    if (EnumAxisScale.Fixed == style.MList[i].MAxisScale)
                    {
                        m_chromatogram.MLines.MItemList[i].MMinFix = style.MList[i].MMin;
                        m_chromatogram.MLines.MItemList[i].MMaxFix = style.MList[i].MMax;
                    }

                    if (null != m_chromatogram.MBGLines && 0 != m_chromatogram.MBGLines.MItemList.Count)
                    {
                        m_chromatogram.MBGLines.MItemList[i].MColor = m_chromatogram.MLines.MItemList[i].MColor;
                        m_chromatogram.MBGLines.MItemList[i].MShow = m_chromatogram.MLines.MItemList[i].MShow;
                    }

                    listAxisScales.Add(new AxisScale(style.MList[i].MAxisScale, style.MList[i].MMin, style.MList[i].MMax));
                }

                List<NameUnitColorShow> list = new List<NameUnitColorShow>();
                foreach (var it in m_chromatogram.MLines.MItemList)
                {
                    list.Add(new NameUnitColorShow(it.MName, it.MUnit, it.MColor, it.MShow));
                }

                CalItemListShow(-1 == m_chromatogram.MLines.MSelectIndex || !m_chromatogram.MLines.MItemList[m_chromatogram.MLines.MSelectIndex].MShow);

                RoutedEventArgs args = new RoutedEventArgs(MUpdateCurveEvent, list);
                RaiseEvent(args);

                RoutedEventArgs args2 = new RoutedEventArgs(MUpdateAxisScaleEvent, listAxisScales);
                RaiseEvent(args2);

                AuditTrails.AuditTrailsStatic.Instance().InsertRowOperate(menuCurve.Header.ToString());
            }
        }

        /// <summary>
        /// 背景设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuBGAdd_Click(object sender, RoutedEventArgs e)
        {
            Share.MessageBoxWin.Show(Share.ReadXaml.GetResources("D_BGMsg"));
        }

        /// <summary>
        /// 背景取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuBGDel_Click(object sender, RoutedEventArgs e)
        {
            m_chromatogram.ClearBG();
            UpdateDraw();

            AuditTrails.AuditTrailsStatic.Instance().InsertRowOperate(menuBG.Header.ToString(), menuBGDel.Header.ToString());
        }

        /// <summary>
        /// 谱图快照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuOutput_Click(object sender, RoutedEventArgs e)
        {
            string strDir = @"D:\HBJPG\";
            if (!Directory.Exists(strDir))
            {
                Directory.CreateDirectory(strDir);
            }

            PresentationSource source = PresentationSource.FromVisual(dockPanel);
            double dpiX = 96.0 * source.CompositionTarget.TransformToDevice.M11;
            double dpiY = 96.0 * source.CompositionTarget.TransformToDevice.M22;
            string jpgName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
            FileStream ms = new FileStream(strDir + jpgName, FileMode.Create);
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)dockPanel.ActualWidth, (int)dockPanel.ActualHeight, dpiX, dpiY, PixelFormats.Default);
            bmp.Render(dockPanel);
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            encoder.Save(ms);
            ms.Close();

            Microsoft.Win32.SaveFileDialog ofd = new Microsoft.Win32.SaveFileDialog();
            ofd.InitialDirectory = strDir;
            ofd.DefaultExt = ".jpg";
            ofd.Filter = "jpg file|*.jpg";
            ofd.FileName = jpgName;
            if (ofd.ShowDialog() == true)
            {
                System.IO.File.Move(strDir + jpgName, ofd.FileName);
                AuditTrails.AuditTrailsStatic.Instance().InsertRowColumnList(menuOutput.Header.ToString(), ofd.FileName);
                MessageBoxWin.Show(ofd.FileName, menuOutput.Header.ToString());
            }
            else
            {
                System.IO.File.Delete(strDir + jpgName);
            }
        }
    }
}
