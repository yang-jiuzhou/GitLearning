using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HBBio.Communication
{
    /// <summary>
    /// ProcessPictureUC.xaml 的交互逻辑
    /// </summary>
    public partial class ProcessPictureUC : UserControl
    {
        public static readonly DependencyProperty MNameProperty = DependencyProperty.Register("MName", typeof(string), typeof(ProcessPictureUC), new PropertyMetadata(""));
        public string MName
        {
            get
            {
                return (string)GetValue(MNameProperty);
            }
            set
            {
                SetValue(MNameProperty, value);
            }
        }

        public static readonly DependencyProperty MLeftProperty = DependencyProperty.Register("MLeft", typeof(int), typeof(ProcessPictureUC), new PropertyMetadata(0));
        public int MLeft
        {
            get
            {
                return (int)GetValue(MLeftProperty);
            }
            set
            {
                SetValue(MLeftProperty, value);
            }
        }

        public static readonly DependencyProperty MTopProperty = DependencyProperty.Register("MTop", typeof(int), typeof(ProcessPictureUC), new PropertyMetadata(0));
        public int MTop
        {
            get
            {
                return (int)GetValue(MTopProperty);
            }
            set
            {
                SetValue(MTopProperty, value);
            }
        }

        /// <summary>
        /// 自定义事件，点击控件时触发
        /// </summary>
        public static readonly RoutedEvent MClickEvent =
             EventManager.RegisterRoutedEvent("MClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ProcessPictureUC));
        public event RoutedEventHandler MClick
        {
            add { AddHandler(MClickEvent, value); }
            remove { RemoveHandler(MClickEvent, value); }
        }

        private List<Point> m_listCircle = new List<Point>();
        private List<Point> m_listColumn = new List<Point>();
        private List<BaseInstrument> m_baseinstrumentList = null;
        private List<InstrumentPoint> m_listIP = null;

        private Dictionary<Thumb, BaseInstrument> m_list = new Dictionary<Thumb, BaseInstrument>();
        private Dictionary<Thumb, int> m_list2 = new Dictionary<Thumb, int>();
        private Dictionary<Thumb, int> m_list3 = new Dictionary<Thumb, int>();
        private Dictionary<string, Shape> m_dictShape = new Dictionary<string, Shape>();

        private bool m_isHV = true;     //添加的线段是否是先水平后垂直
        private EnumLineType m_lineType = EnumLineType.All;
        private int m_click = -1;
        public bool MConnHVPt
        {
            get
            {
                return m_isHV;
            }
            set
            {
                m_click = 0;
                m_isHV = true;
            }
        }
        public bool MConnVHPt
        {
            set
            {
                m_click = 0;
                m_isHV = false;
            }
        }
        public bool MDisConnPt
        {
            set
            {
                m_click = 3;
            }
        }
        public EnumLineType MLineType
        {
            set
            {
                m_lineType = value;
            }
        }

        public bool MRunS { get; set; }
        public bool MRunA { get; set; }
        public bool MRunB { get; set; }
        public bool MRunC { get; set; }
        public bool MRunD { get; set; }
        public bool MBPV { get; set; }


        //当前选中的控件
        private Thumb m_currThumb = null;
        private Thumb MCurrThumb
        {
            get
            {
                return m_currThumb;
            }
            set
            {
                m_currThumb = value;

                if (null != m_currThumb)
                {
                    MName = m_currThumb.ToolTip.ToString();
                    MLeft = (int)Canvas.GetLeft(m_currThumb) + (int)m_currThumb.Width / 2;
                    MTop = (int)Canvas.GetTop(m_currThumb) + (int)m_currThumb.Height / 2;
                }
            }
        }

        /// <summary>
        /// 属性，调整位置，只写
        /// </summary>
        public Point MPointPosition
        {
            set
            {
                if (null != MCurrThumb)
                {
                    Canvas.SetLeft(MCurrThumb, value.X - MCurrThumb.Width / 2);
                    Canvas.SetTop(MCurrThumb, value.Y - MCurrThumb.Height / 2);
                    MCurrThumb = MCurrThumb;
                }
            }
        }

        private Point m_pt1 = new Point(0, 0);
        private Point m_pt2 = new Point(0, 0);


        /// <summary>
        /// 构造函数
        /// </summary>
        public ProcessPictureUC()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 实时流路图
        /// </summary>
        /// <param name="baseinstrumentList"></param>
        /// <param name="listIP"></param>
        /// <param name="size"></param>
        public void UpdateItems(List<BaseInstrument> baseinstrumentList, List<InstrumentPoint> listIP, InstrumentSize size, List<Point> listCircle, List<Point> listColumn)
        {
            this.canvas.Children.Clear();
            m_list.Clear();
            m_list2.Clear();
            m_list3.Clear();
            m_dictShape.Clear();

            m_listIP = listIP;
            m_listCircle = listCircle;
            m_listColumn = listColumn;

            this.Width = size.MWidth;
            this.Height = size.MHeight;

            foreach (var it in baseinstrumentList)
            {
                if (!it.MVisible)
                {
                    continue;
                }

                Thumb thumb = new Thumb();
                thumb.ToolTip = it.MConstName;

                switch (it.MType)
                {
                    case ENUMInstrumentType.Sampler:
                        {
                            thumb.Width = 50;
                            thumb.Height = 50;
                            thumb.Template = FindResource("ctRectangle") as ControlTemplate;
                        }
                        break;
                    case ENUMInstrumentType.Valve:
                        {
                            thumb.Width = 60;
                            thumb.Height = 60;
                            thumb.Template = FindResource("ctEllipse") as ControlTemplate;
                            thumb.Name = it.MConstName;
                            thumb.SetBinding(Thumb.TagProperty, new Binding("MValveGetStr"));
                            thumb.DataContext = (ValveItem)it;
                        }
                        break;
                    case ENUMInstrumentType.Pump:
                        if (it.MConstName.Contains("FIT"))
                        {
                            thumb.Width = 36;
                            thumb.Height = 50;
                            thumb.Template = FindResource("ctRectangle") as ControlTemplate;
                            thumb.Name = it.MConstName.Replace("FIT", "");
                        }
                        else
                        {
                            thumb.Width = 36;
                            thumb.Height = 36;
                            thumb.Template = FindResource("ctRectangle") as ControlTemplate;
                            thumb.Name = "PT";
                        }
                        break;
                    case ENUMInstrumentType.Detector:
                    case ENUMInstrumentType.Other:
                        {
                            thumb.Width = 50;
                            thumb.Height = 36;
                            thumb.Template = FindResource("ctRectangle") as ControlTemplate;
                            if (System.Text.RegularExpressions.Regex.IsMatch(it.MConstName, "[0-9]+$"))
                            {
                                thumb.Name = it.MConstName.Remove(it.MConstName.Length - 2, 2);
                            }
                            else
                            {
                                thumb.Name = it.MConstName;
                            }
                        }
                        break;
                    case ENUMInstrumentType.Collector:
                        {
                            thumb.Width = 50;
                            thumb.Height = 50;
                            thumb.Template = FindResource("ctRectangle") as ControlTemplate;
                            thumb.Name = "Coll";
                        }
                        break;
                }

                thumb.SetBinding(Thumb.BorderBrushProperty, new Binding() { Path = new System.Windows.PropertyPath("MBrush") });
                thumb.SetBinding(Thumb.ForegroundProperty, new Binding() { Path = new System.Windows.PropertyPath("MForeground") });
                thumb.DataContext = it;
                thumb.DragCompleted += new DragCompletedEventHandler(this.Thumb_Click);

                this.canvas.Children.Add(thumb);
                m_list.Add(thumb, it);

                Canvas.SetLeft(thumb, it.MPt.X);
                Canvas.SetTop(thumb, it.MPt.Y);
                Panel.SetZIndex(thumb, 1);
            }

            for (int i = 0; i < listCircle.Count; i++)
            {
                AddItemCircle(i, listCircle[i], false);
            }

            for (int i = 0; i < listColumn.Count; i++)
            {
                AddItemColumn(i, listColumn[i], false);
            }

            foreach (var it in listIP)
            {
                AddPath(it.MName, it.MPt1, it.MPt2, it.MIsHV, it.MType, false);
            }
        }

        /// <summary>
        /// 更新运行状态
        /// </summary>
        /// <param name="runS"></param>
        /// <param name="runA"></param>
        /// <param name="runB"></param>
        /// <param name="runC"></param>
        /// <param name="runD"></param>
        /// <param name="bpv"></param>
        public void UpdateLines(bool runS, bool runA, bool runB, bool runC, bool runD, bool bpv)
        {
            if (null != m_listIP && (MRunS != runS || MRunA != runA || MRunB != runB || MRunC != runC || MRunD != runD || MBPV != bpv))
            {
                MRunS = runS;
                MRunA = runA;
                MRunB = runB;
                MRunC = runC;
                MRunD = runD;
                MBPV = bpv;
                foreach (var it in m_listIP)
                {
                    AddPath(it.MName, it.MPt1, it.MPt2, it.MIsHV, it.MType, runS || runA || runB || runC || runD);
                }
            }
        }

        /// <summary>
        /// 编辑流路图
        /// </summary>
        /// <param name="baseinstrumentList"></param>
        /// <param name="listIP"></param>
        public void UpdateItems(List<BaseInstrument> baseinstrumentList, List<InstrumentPoint> listIP, List<Point> listCircle, List<Point> listColumn)
        {
            m_baseinstrumentList = baseinstrumentList;
            m_listIP = listIP;
            m_listCircle = listCircle;
            m_listColumn = listColumn;

            foreach (var it in m_baseinstrumentList)
            {
                if (!it.MVisible)
                {
                    continue;
                }

                Thumb thumb = new Thumb();
                thumb.ToolTip = it.MConstName;

                switch (it.MType)
                {
                    case ENUMInstrumentType.Sampler:
                        {
                            thumb.Width = 50;
                            thumb.Height = 50;
                            thumb.Template = FindResource("ctRectangle") as ControlTemplate;
                        }
                        break;
                    case ENUMInstrumentType.Valve:
                        {
                            thumb.Width = 60;
                            thumb.Height = 60;
                            thumb.Template = FindResource("ctEllipse") as ControlTemplate;
                            thumb.Name = it.MConstName;
                        }
                        break;
                    case ENUMInstrumentType.Pump:
                        if (it.MConstName.Contains("FIT"))
                        {
                            thumb.Width = 36;
                            thumb.Height = 50;
                            thumb.Template = FindResource("ctRectangle") as ControlTemplate;
                            thumb.Name = it.MConstName.Replace("FIT", "");
                        }
                        else
                        {
                            thumb.Width = 36;
                            thumb.Height = 36;
                            thumb.Template = FindResource("ctEllipse") as ControlTemplate;
                            thumb.Name = "PT";
                        }
                        break;
                    case ENUMInstrumentType.Detector:
                    case ENUMInstrumentType.Other:
                        {
                            thumb.Width = 50;
                            thumb.Height = 36;
                            thumb.Template = FindResource("ctRectangle") as ControlTemplate;
                            if (System.Text.RegularExpressions.Regex.IsMatch(it.MConstName, "[0-9]+$"))
                            {
                                thumb.Name = it.MConstName.Remove(it.MConstName.Length - 2, 2);
                            }
                            else
                            {
                                thumb.Name = it.MConstName;
                            }
                        }
                        break;
                    case ENUMInstrumentType.Collector:
                        {
                            thumb.Width = 50;
                            thumb.Height = 50;
                            thumb.Template = FindResource("ctRectangle") as ControlTemplate;
                            thumb.Name = "Coll";
                        }
                        break;
                }

                thumb.DragDelta += new DragDeltaEventHandler(this.Thumb_DragDelta);
                thumb.DragStarted += new DragStartedEventHandler(this.Thumb_DragStarted);
                thumb.DragCompleted += new DragCompletedEventHandler(this.Thumb_DragCompleted);

                this.canvas.Children.Add(thumb);
                m_list.Add(thumb, it);

                Canvas.SetLeft(thumb, it.MPt.X);
                Canvas.SetTop(thumb, it.MPt.Y);
                Panel.SetZIndex(thumb, 1);
            }

            for (int i = 0; i < listCircle.Count; i++)
            {
                AddItemCircle(i, listCircle[i], true);
            }

            for (int i = 0; i < listColumn.Count; i++)
            {
                AddItemColumn(i, listColumn[i], true);
            }

            foreach (var it in m_listIP)
            {
                AddPath(it.MName, it.MPt1, it.MPt2, it.MIsHV, it.MType, false);
            }
        }

        /// <summary>
        /// 编辑流路图更新自定义圆圈
        /// </summary>
        /// <param name="count"></param>
        public void UpdateItemCircle(int count)
        {
            if (m_listCircle.Count < count)
            {
                while (m_listCircle.Count < count)
                {
                    AddItemCircle();
                }
            }
            else if (m_listCircle.Count > count)
            {
                while (m_listCircle.Count > count)
                {
                    RemoveItemCircle();
                }
            }
        }

        /// <summary>
        /// 编辑流路图增加自定义圆圈
        /// </summary>
        private void AddItemCircle()
        {
            Point pt = new Point(0, 0);
            AddItemCircle(m_listCircle.Count, pt, true);
            m_listCircle.Add(pt);
        }

        /// <summary>
        /// 编辑流路图删除自定义圆圈
        /// </summary>
        private void RemoveItemCircle()
        {
            int last = m_listCircle.Count - 1;

            m_listCircle.RemoveAt(last);

            foreach (var it in m_list2)
            {
                if (it.Value == last)
                {
                    this.canvas.Children.Remove(it.Key);
                    m_list2.Remove(it.Key);
                    break;
                }
            }
        }

        /// <summary>
        /// 编辑流路图增加自定义圆圈
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pt"></param>
        protected void AddItemCircle(int index, Point pt, bool drag)
        {
            Thumb thumb = new Thumb();
            thumb.ToolTip = index.ToString();
            thumb.Width = 25;
            thumb.Height = 25;
            thumb.Template = FindResource("ctCircle") as ControlTemplate;
            if (drag)
            {
                thumb.DragDelta += new DragDeltaEventHandler(this.Thumb_DragDelta);
                thumb.DragStarted += new DragStartedEventHandler(this.Thumb_DragStarted);
                thumb.DragCompleted += new DragCompletedEventHandler(this.Thumb_DragCompleted);
            }

            this.canvas.Children.Add(thumb);
            m_list2.Add(thumb, index);

            Canvas.SetLeft(thumb, pt.X);
            Canvas.SetTop(thumb, pt.Y);
            Panel.SetZIndex(thumb, 1);
        }

        /// <summary>
         /// 编辑流路图更新自定义圆圈
         /// </summary>
         /// <param name="count"></param>
        public void UpdateItemColumn(int count)
        {
            if (m_listColumn.Count < count)
            {
                while (m_listColumn.Count < count)
                {
                    AddItemColumn();
                }
            }
            else if (m_listColumn.Count > count)
            {
                while (m_listColumn.Count > count)
                {
                    RemoveItemColumn();
                }
            }
        }

        /// <summary>
        /// 编辑流路图增加自定义圆圈
        /// </summary>
        private void AddItemColumn()
        {
            Point pt = new Point(0, 0);
            AddItemColumn(m_listColumn.Count, pt, true);
            m_listColumn.Add(pt);
        }

        /// <summary>
        /// 编辑流路图删除自定义圆圈
        /// </summary>
        private void RemoveItemColumn()
        {
            int last = m_listColumn.Count - 1;

            m_listColumn.RemoveAt(last);

            foreach (var it in m_list3)
            {
                if (it.Value == last)
                {
                    this.canvas.Children.Remove(it.Key);
                    m_list3.Remove(it.Key);
                    break;
                }
            }
        }

        /// <summary>
        /// 编辑流路图增加自定义圆圈
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pt"></param>
        protected void AddItemColumn(int index, Point pt, bool drag)
        {
            Thumb thumb = new Thumb();
            thumb.ToolTip = index.ToString();
            thumb.Width = 35;
            thumb.Height = 50;
            thumb.Template = FindResource("ctColumn") as ControlTemplate;
            if (drag)
            {
                thumb.DragDelta += new DragDeltaEventHandler(this.Thumb_DragDelta);
                thumb.DragStarted += new DragStartedEventHandler(this.Thumb_DragStarted);
                thumb.DragCompleted += new DragCompletedEventHandler(this.Thumb_DragCompleted);
            }

            this.canvas.Children.Add(thumb);
            m_list3.Add(thumb, index);

            Canvas.SetLeft(thumb, pt.X);
            Canvas.SetTop(thumb, pt.Y);
            Panel.SetZIndex(thumb, 1);
        }

        /// <summary>
        /// 清除所有路径
        /// </summary>
        public void ClearShape()
        {
            foreach (var it in m_dictShape)
            {
                this.canvas.Children.Remove(it.Value);
            }

            m_dictShape.Clear();
            m_listIP.Clear();
        }

        /// <summary>
        /// 更新所有元素的位置
        /// </summary>
        public void UpdateAllPt()
        {
            foreach (var it in canvas.Children)
            {
                if (it is Thumb)
                {
                    Thumb myThumb = (Thumb)it;
                    if (m_list.ContainsKey(myThumb))
                    {
                        m_list[myThumb].MPt = new Point(Canvas.GetLeft(myThumb), Canvas.GetTop(myThumb));
                    }
                    else
                    {
                        if (m_list2.ContainsKey(myThumb))
                        {
                            m_listCircle[m_list2[myThumb]] = new Point(Canvas.GetLeft(myThumb), Canvas.GetTop(myThumb));
                        }
                        else
                        {
                            m_listColumn[m_list3[myThumb]] = new Point(Canvas.GetLeft(myThumb), Canvas.GetTop(myThumb));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 添加路径
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        private void AddPath(string key, Point pt1, Point pt2, bool isHV, EnumLineType type, bool running)
        {
            switch (type)
            {
                case EnumLineType.All:
                    if (pt2.X == pt1.X)
                    {
                        DrawLineItem(key + "1", pt1, pt2, running ? "#00FF00" : "#ECECEC", 5);
                        DrawLineItem(key + "2", new Point(pt1.X - 3, pt1.Y), new Point(pt2.X - 3, pt2.Y), "#BFBFBF", 3);
                        DrawLineItem(key + "3", new Point(pt1.X + 3, pt1.Y), new Point(pt2.X + 3, pt2.Y), "#BFBFBF", 3);
                    }
                    else if (pt2.Y == pt1.Y)
                    {
                        DrawLineItem(key + "1", pt1, pt2, running ? "#00FF00" : "#ECECEC", 5);
                        DrawLineItem(key + "2", new Point(pt1.X, pt1.Y - 3), new Point(pt2.X, pt2.Y - 3), "#BFBFBF", 3);
                        DrawLineItem(key + "3", new Point(pt1.X, pt1.Y + 3), new Point(pt2.X, pt2.Y + 3), "#BFBFBF", 3);
                    }
                    else
                    {
                        DrawLineAndArcItem(key + "1", pt1, pt2, new SolidColorBrush((Color)ColorConverter.ConvertFromString(running ? "#00FF00" : "#ECECEC")), 9, 5, isHV);
                        if (isHV)
                        {
                            if (pt1.Y < pt2.Y)
                            {
                                DrawLineAndArcItem(key + "2", new Point(pt1.X + 3, pt1.Y + 3), new Point(pt2.X - 3, pt2.Y - 3), new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BFBFBF")), 6.5, 3, isHV);
                                DrawLineAndArcItem(key + "3", new Point(pt1.X - 3, pt1.Y - 3), new Point(pt2.X + 3, pt2.Y + 3), new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BFBFBF")), 10.5, 3, isHV);
                            }
                            else
                            {
                                DrawLineAndArcItem(key + "2", new Point(pt1.X + 3, pt1.Y + 3), new Point(pt2.X + 3, pt2.Y + 3), new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BFBFBF")), 10.5, 3, isHV);
                                DrawLineAndArcItem(key + "3", new Point(pt1.X - 3, pt1.Y - 3), new Point(pt2.X - 3, pt2.Y - 3), new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BFBFBF")), 6.5, 3, isHV);
                            }
                        }
                        else
                        {
                            if (pt1.Y < pt2.Y)
                            {
                                DrawLineAndArcItem(key + "2", new Point(pt1.X + 3, pt1.Y + 3), new Point(pt2.X - 3, pt2.Y - 3), new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BFBFBF")), 6.5, 3, isHV);
                                DrawLineAndArcItem(key + "3", new Point(pt1.X - 3, pt1.Y - 3), new Point(pt2.X + 3, pt2.Y + 3), new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BFBFBF")), 10.5, 3, isHV);
                            }
                            else
                            {
                                DrawLineAndArcItem(key + "2", new Point(pt1.X + 3, pt1.Y + 3), new Point(pt2.X + 3, pt2.Y + 3), new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BFBFBF")), 6.5, 3, isHV);
                                DrawLineAndArcItem(key + "3", new Point(pt1.X - 3, pt1.Y - 3), new Point(pt2.X - 3, pt2.Y - 3), new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BFBFBF")), 10.5, 3, isHV);
                            }
                        }
                    }
                    break;
                default:
                    switch (type)
                    {
                        case EnumLineType.ABCD: running = MRunA | MRunB | MRunC | MRunD; break;
                        case EnumLineType.S: running = MRunS; break;
                        case EnumLineType.A: running = MRunA; break;
                        case EnumLineType.B: running = MRunB; break;
                        case EnumLineType.C: running = MRunC; break;
                        case EnumLineType.D: running = MRunD; break;
                        case EnumLineType.BPV: running = (MRunA | MRunB | MRunC | MRunD) & MBPV; break;
                    }
                    if (pt2.X == pt1.X)
                    {
                        DrawLineItem(key + "1", pt1, pt2, running ? "#00FF00" : "#ECECEC", 5);
                        DrawLineItem(key + "2", new Point(pt1.X - 3, pt1.Y), new Point(pt2.X - 3, pt2.Y), "#BFBFBF", 3);
                        DrawLineItem(key + "3", new Point(pt1.X + 3, pt1.Y), new Point(pt2.X + 3, pt2.Y), "#BFBFBF", 3);
                    }
                    else if (pt2.Y == pt1.Y)
                    {
                        DrawLineItem(key + "1", pt1, pt2, running ? "#00FF00" : "#ECECEC", 5);
                        DrawLineItem(key + "2", new Point(pt1.X, pt1.Y - 3), new Point(pt2.X, pt2.Y - 3), "#BFBFBF", 3);
                        DrawLineItem(key + "3", new Point(pt1.X, pt1.Y + 3), new Point(pt2.X, pt2.Y + 3), "#BFBFBF", 3);
                    }
                    else
                    {
                        DrawLineAndArcItem(key + "1", pt1, pt2, new SolidColorBrush((Color)ColorConverter.ConvertFromString(running ? "#00FF00" : "#ECECEC")), 9, 5, isHV);
                        if (isHV)
                        {
                            if (pt1.Y < pt2.Y)
                            {
                                DrawLineAndArcItem(key + "2", new Point(pt1.X + 3, pt1.Y + 3), new Point(pt2.X - 3, pt2.Y - 3), new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BFBFBF")), 6.5, 3, isHV);
                                DrawLineAndArcItem(key + "3", new Point(pt1.X - 3, pt1.Y - 3), new Point(pt2.X + 3, pt2.Y + 3), new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BFBFBF")), 10.5, 3, isHV);
                            }
                            else
                            {
                                DrawLineAndArcItem(key + "2", new Point(pt1.X + 3, pt1.Y + 3), new Point(pt2.X + 3, pt2.Y + 3), new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BFBFBF")), 10.5, 3, isHV);
                                DrawLineAndArcItem(key + "3", new Point(pt1.X - 3, pt1.Y - 3), new Point(pt2.X - 3, pt2.Y - 3), new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BFBFBF")), 6.5, 3, isHV);
                            }
                        }
                        else
                        {
                            if (pt1.Y < pt2.Y)
                            {
                                DrawLineAndArcItem(key + "2", new Point(pt1.X + 3, pt1.Y + 3), new Point(pt2.X - 3, pt2.Y - 3), new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BFBFBF")), 6.5, 3, isHV);
                                DrawLineAndArcItem(key + "3", new Point(pt1.X - 3, pt1.Y - 3), new Point(pt2.X + 3, pt2.Y + 3), new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BFBFBF")), 10.5, 3, isHV);
                            }
                            else
                            {
                                DrawLineAndArcItem(key + "2", new Point(pt1.X + 3, pt1.Y + 3), new Point(pt2.X + 3, pt2.Y + 3), new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BFBFBF")), 6.5, 3, isHV);
                                DrawLineAndArcItem(key + "3", new Point(pt1.X - 3, pt1.Y - 3), new Point(pt2.X - 3, pt2.Y - 3), new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BFBFBF")), 10.5, 3, isHV);
                            }
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// 删除路径
        /// </summary>
        /// <param name="key"></param>
        private void DelPath(string key)
        {
            if (m_dictShape.ContainsKey(key + "1"))
            {
                this.canvas.Children.Remove(m_dictShape[key + "1"]);
                m_dictShape.Remove(key + "1");

                this.canvas.Children.Remove(m_dictShape[key + "2"]);
                m_dictShape.Remove(key + "2");

                this.canvas.Children.Remove(m_dictShape[key + "3"]);
                m_dictShape.Remove(key + "3");
            }
        }

        /// <summary>
        /// 画两元素之间的连接（直线）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <param name="brush"></param>
        /// <param name="thick"></param>
        private void DrawLineItem(string key, Point pt1, Point pt2, string brush, double thick)
        {
            Line line = new Line();
            line.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(brush));
            line.StrokeThickness = thick;
            line.X1 = pt1.X;
            line.Y1 = pt1.Y;
            line.X2 = pt2.X;
            line.Y2 = pt2.Y;

            if (m_dictShape.ContainsKey(key))
            {
                this.canvas.Children.Remove(m_dictShape[key]);
                m_dictShape[key] = line;
            }
            else
            {
                m_dictShape.Add(key, line);
            }

            this.canvas.Children.Add(line);

            Panel.SetZIndex(line, 0);
        }

        /// <summary>
        /// 画两元素之间的连接（直线，弧线，直线）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <param name="brush"></param>
        /// <param name="arc"></param>
        /// <param name="thick"></param>
        private void DrawLineAndArcItem(string key, Point pt1, Point pt2, Brush brush, double arc, double thick, bool isHV)
        {
            Path path = new Path();
            path.Stroke = brush;
            path.StrokeThickness = thick;
            PathGeometry geo = new PathGeometry();
            PathFigure fig = new PathFigure();
            fig.StartPoint = pt1;
            if (isHV)
            {
                if (pt1.Y < pt2.Y)
                {
                    fig.Segments.Add(new LineSegment(new Point(pt2.X - arc, pt1.Y), true));
                    fig.Segments.Add(new ArcSegment(new Point(pt2.X, pt1.Y + arc), new Size(arc, arc), 0, false, SweepDirection.Clockwise, true));
                }
                else
                {
                    fig.Segments.Add(new LineSegment(new Point(pt2.X - arc, pt1.Y), true));
                    fig.Segments.Add(new ArcSegment(new Point(pt2.X, pt1.Y - arc), new Size(arc, arc), 0, false, SweepDirection.Counterclockwise, true));
                }
            }
            else
            {
                if (pt1.Y < pt2.Y)
                {
                    fig.Segments.Add(new LineSegment(new Point(pt1.X, pt2.Y - arc), true));
                    fig.Segments.Add(new ArcSegment(new Point(pt1.X + arc, pt2.Y), new Size(arc, arc), 0, false, SweepDirection.Counterclockwise, true));
                }
                else
                {
                    fig.Segments.Add(new LineSegment(new Point(pt1.X, pt2.Y + arc), true));
                    fig.Segments.Add(new ArcSegment(new Point(pt1.X + arc, pt2.Y), new Size(arc, arc), 0, false, SweepDirection.Clockwise, true));
                }
            }
            fig.Segments.Add(new LineSegment(pt2, true));
            geo.Figures.Add(fig);
            path.Data = geo;

            if (m_dictShape.ContainsKey(key))
            {
                this.canvas.Children.Remove(m_dictShape[key]);
                m_dictShape[key] = path;
            }
            else
            {
                m_dictShape.Add(key, path);
            }

            this.canvas.Children.Add(path);

            Panel.SetZIndex(path, 0);
        }

        /// <summary>
        /// 拖拽前的位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Thumb myThumb = (Thumb)sender;
            double nLeft = Canvas.GetLeft(myThumb) + e.HorizontalChange;
            double nTop = Canvas.GetTop(myThumb) + e.VerticalChange;

            //防止Thumb控件被拖出容器。
            if (nTop <= 0)
            {
                nTop = 0;
            }

            if (nTop >= (canvas.Height - myThumb.Height))
            {
                nTop = canvas.Height - myThumb.Height;
            }

            if (nLeft <= 0)
            {
                nLeft = 0;
            }

            if (nLeft >= (canvas.Width - myThumb.Width))
            {
                nLeft = canvas.Width - myThumb.Width;
            }

            Canvas.SetTop(myThumb, nTop);
            Canvas.SetLeft(myThumb, nLeft);

            MLeft = (int)Canvas.GetLeft(myThumb) + (int)myThumb.Width / 2;
            MTop = (int)Canvas.GetTop(myThumb) + (int)myThumb.Height / 2;
        }

        /// <summary>
        /// 拖拽开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            Thumb myThumb = (Thumb)sender;

            if (0 == m_click)//连接点1
            {
                m_pt1 = new Point(Canvas.GetLeft(myThumb) + myThumb.Width / 2, Canvas.GetTop(myThumb) + myThumb.Height / 2);
            }
            else if (1 == m_click)//连接点2
            {
                m_pt2 = new Point(Canvas.GetLeft(myThumb) + myThumb.Width / 2, Canvas.GetTop(myThumb) + myThumb.Height / 2);
            }
            else if (3 == m_click)//断开点1
            {
                m_pt1 = new Point(Canvas.GetLeft(myThumb) + myThumb.Width / 2, Canvas.GetTop(myThumb) + myThumb.Height / 2);
            }
            else if (2 == m_click)//断开点2
            {
                m_pt2 = new Point(Canvas.GetLeft(myThumb) + myThumb.Width / 2, Canvas.GetTop(myThumb) + myThumb.Height / 2);
            }
        }

        /// <summary>
        /// 拖拽结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Thumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            Thumb myThumb = (Thumb)sender;

            if (0 == m_click)
            {
                //判断是否发生位移
                if (!m_pt1.Equals(new Point(Canvas.GetLeft(myThumb) + myThumb.Width / 2, Canvas.GetTop(myThumb) + myThumb.Height / 2)))
                {
                    m_click = -1;
                    return;
                }
                m_click = 1;
            }
            else if (1 == m_click)
            {
                //判断是否发生位移
                if (!m_pt2.Equals(new Point(Canvas.GetLeft(myThumb) + myThumb.Width / 2, Canvas.GetTop(myThumb) + myThumb.Height / 2)))
                {
                    m_click = -1;
                    return;
                }
                m_click = -1;

                if ((m_pt1.X == m_pt2.X)
                    || (m_pt2.Y == m_pt1.Y)
                    || ((m_pt2.X - m_pt1.X) > ((Thumb)sender).Width / 2 && Math.Abs(m_pt2.Y - m_pt1.Y) > ((Thumb)sender).Height / 2))
                {
                    AddPath(MCurrThumb.ToolTip.ToString() + myThumb.ToolTip.ToString(), m_pt1, m_pt2, m_isHV, m_lineType, false);
                    m_listIP.Add(new InstrumentPoint(MCurrThumb.ToolTip.ToString() + myThumb.ToolTip.ToString(), m_pt1, m_pt2, m_isHV, m_lineType));
                }
            }
            else if (3 == m_click)
            {
                //判断是否发生位移
                if (!m_pt1.Equals(new Point(Canvas.GetLeft(myThumb) + myThumb.Width / 2, Canvas.GetTop(myThumb) + myThumb.Height / 2)))
                {
                    m_click = -1;
                    return;
                }
                m_click = 2;
            }
            else if (2 == m_click)
            {
                //判断是否发生位移
                if (!m_pt2.Equals(new Point(Canvas.GetLeft(myThumb) + myThumb.Width / 2, Canvas.GetTop(myThumb) + myThumb.Height / 2)))
                {
                    m_click = -1;
                    return;
                }
                m_click = -1;

                if ((m_pt1.X == m_pt2.X)
                    || (m_pt2.Y == m_pt1.Y)
                    || ((m_pt2.X - m_pt1.X) > ((Thumb)sender).Width / 2 && Math.Abs(m_pt2.Y - m_pt1.Y) > ((Thumb)sender).Height / 2))
                {
                    DelPath(MCurrThumb.ToolTip.ToString() + myThumb.ToolTip.ToString());
                    for (int i = 0; i < m_listIP.Count; i++)
                    {
                        if (m_listIP[i].MName.Equals(MCurrThumb.ToolTip.ToString() + myThumb.ToolTip.ToString()))
                        {
                            m_listIP.RemoveAt(i);
                            return;
                        }
                    }
                }
            }

            MCurrThumb = myThumb;
        }

        /// <summary>
        /// 元素点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Thumb_Click(object sender, DragCompletedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(MClickEvent, (Thumb)sender);
            RaiseEvent(args);
        }

        /// <summary>
        /// 获取焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
        }
    }
}