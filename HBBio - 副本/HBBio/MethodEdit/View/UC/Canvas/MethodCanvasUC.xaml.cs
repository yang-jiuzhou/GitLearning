using HBBio.Share;
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

namespace HBBio.MethodEdit
{
    /// <summary>
    /// MethodCanvasUC.xaml 的交互逻辑
    /// </summary>
    public partial class MethodCanvasUC : UserControl
    {
        private int m_boardWidth;               //画板宽度
        private int m_boardHeight;              //画板高度
        private int m_chartLeft;                //图表区域起点左（横向边距）
        private int m_chartTop;                 //图表区域起点上（纵向边距）
        private int m_chartWidth;               //图表区域宽度
        private int m_chartHeight;              //图表区域高度
        private int m_chartRight;               //图表区域终点右
        private int m_chartBottom;              //图表区域终点底

        private double m_maxXT = 1;             
        private double m_maxXV = 1;
        private double m_maxXCV = 1;
        private double m_maxX = 1;              //x轴最大值
        private double m_minX = 0;              //x轴最小值
        private double m_maxY = 1;              //y轴最大值
        private double m_minY = 0;              //y轴最小值
        private double m_disXT = 0;
        private double m_disXV = 0;
        private double m_disXCV = 0;
        private double m_disX = 0;              //水平刻度间距像素
        private double m_disY = 0;              //垂直刻度间距像素

        private int m_count = 0;                                                //列表集合数
        private List<List<double>> m_TList = new List<List<double>>();          //时间列表
        private List<List<double>> m_VList = new List<List<double>>();          //体积列表
        private List<List<double>> m_CVList = new List<List<double>>();         //柱体积列表
        private List<List<double>> m_PerAList = new List<List<double>>();       //A百分比列表
        private List<List<double>> m_PerBList = new List<List<double>>();       //B百分比列表
        private List<List<double>> m_PerCList = new List<List<double>>();       //C百分比列表
        private List<List<double>> m_PerDList = new List<List<double>>();       //D百分比列表
        private List<string> m_namePhase = new List<string>();                  //阶段描述列表
        private List<List<string>> m_nameStep = new List<List<string>>();       //阶段内部描述列表

        private List<System.Drawing.Point[]> m_DataSourseA = new List<System.Drawing.Point[]>();       //线A集合
        private List<System.Drawing.Point[]> m_DataSourseB = new List<System.Drawing.Point[]>();       //线B集合
        private List<System.Drawing.Point[]> m_DataSourseC = new List<System.Drawing.Point[]>();       //线C集合
        private List<System.Drawing.Point[]> m_DataSourseD = new List<System.Drawing.Point[]>();       //线D集合

        private bool m_showA = true;            //显隐线A
        private bool m_showB = true;            //显隐线B
        private bool m_showC = true;            //显隐线C
        private bool m_showD = true;            //显隐线D
        private bool m_showGrid = false;        //显隐网格
        private bool m_showMark = false;        //显隐标记

        private EnumBase m_base = EnumBase.T;   //X轴类型
        private int m_markerX = -1;             //标记的X值

        public int MCurrClickIndex = -1;        //当前选中阶段

        bool MClicked = false;


        WriteableBitmap m_bitmap = null;


        public MethodCanvasUC()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 清空源数据
        /// </summary>
        public void ClearRawData()
        {
            m_TList.Clear();
            m_VList.Clear();
            m_CVList.Clear();

            m_PerAList.Clear();
            m_PerBList.Clear();
            m_PerCList.Clear();
            m_PerDList.Clear();

            m_namePhase.Clear();
            m_nameStep.Clear();

            m_count = 0;

            m_markerX = -1;
        }

        /// <summary>
        /// 设置显隐
        /// </summary>
        /// <param name="itemA"></param>
        /// <param name="itemB"></param>
        /// <param name="itemC"></param>
        /// <param name="itemD"></param>
        public void SetVisible(Visibility itemA, Visibility itemB, Visibility itemC, Visibility itemD)
        {
            cboxA.Visibility = itemA;
            cboxB.Visibility = itemB;
            cboxC.Visibility = itemC;
            cboxD.Visibility = itemD;
        }

        /// <summary>
        /// 添加源数据
        /// </summary>
        /// <param name="stepT"></param>
        /// <param name="stepV"></param>
        /// <param name="stepCV"></param>
        /// <param name="perA"></param>
        /// <param name="perB"></param>
        /// <param name="perC"></param>
        /// <param name="perD"></param>
        public void AddRawData(List<double> stepT, List<double> stepV, List<double> stepCV, List<double> perA, List<double> perB, List<double> perC, List<double> perD
            , string namePhase, List<string> nameStep)
        {
            m_TList.Add(stepT);
            m_VList.Add(stepV);
            m_CVList.Add(stepCV);

            m_PerAList.Add(perA);
            m_PerBList.Add(perB);
            m_PerCList.Add(perC);
            m_PerDList.Add(perD);

            m_namePhase.Add(namePhase);
            m_nameStep.Add(nameStep);

            ++m_count;
        }

        /// <summary>
        /// 计算源数据
        /// </summary>
        public void CalRawData()
        {
            CalRawDataXMaxMin();
            CalRawDataYMaxMin();
            JudgeXTVCV();
            CalLineData();
            SetGridValue();
        }

        /// <summary>
        /// 计算X最值
        /// </summary>
        private void CalRawDataXMaxMin()
        {
            m_maxXT = 0;
            m_maxXV = 0;
            m_maxXCV = 0;
            m_minX = 0;

            foreach(var it in m_TList)
            {
                foreach(var it2 in it)
                {
                    m_maxXT += it2;
                }
            }
            if (0 == m_maxXT)
            {
                m_maxXT = 1;
            }
            ValueTrans.CalMaxMin(ref m_minX, ref m_maxXT, ref m_disXT);

            foreach (var it in m_VList)
            {
                foreach (var it2 in it)
                {
                    m_maxXV += it2;
                }
            }
            if (0 == m_maxXV)
            {
                m_maxXV = 1;
            }
            ValueTrans.CalMaxMin(ref m_minX, ref m_maxXV, ref m_disXV);

            foreach (var it in m_CVList)
            {
                foreach (var it2 in it)
                {
                    m_maxXCV += it2;
                }
            }
            if (0 == m_maxXCV)
            {
                m_maxXCV = 1;
            }
            ValueTrans.CalMaxMin(ref m_minX, ref m_maxXCV, ref m_disXCV);
        }

        /// <summary>
        /// 计算Y最值
        /// </summary>
        private void CalRawDataYMaxMin()
        {
            m_maxY = 1;
            m_minY = 0;

            foreach (var list in m_PerAList)
            {
                if (0 < list.Count)
                {
                    double max = list.Max();
                    double min = list.Min();
                    if (max > m_maxY)
                    {
                        m_maxY = max;
                    }
                    if (min < m_minY)
                    {
                        m_minY = min;
                    }
                }
            }

            foreach (var list in m_PerBList)
            {
                if (0 < list.Count)
                {
                    double max = list.Max();
                    double min = list.Min();
                    if (max > m_maxY)
                    {
                        m_maxY = max;
                    }
                    if (min < m_minY)
                    {
                        m_minY = min;
                    }
                }
            }

            foreach (var list in m_PerCList)
            {
                if (0 < list.Count)
                {
                    double max = list.Max();
                    double min = list.Min();
                    if (max > m_maxY)
                    {
                        m_maxY = max;
                    }
                    if (min < m_minY)
                    {
                        m_minY = min;
                    }
                }
            }

            foreach (var list in m_PerDList)
            {
                if (0 < list.Count)
                {
                    double max = list.Max();
                    double min = list.Min();
                    if (max > m_maxY)
                    {
                        m_maxY = max;
                    }
                    if (min < m_minY)
                    {
                        m_minY = min;
                    }
                }
            }

            ValueTrans.CalMaxMin(ref m_minY, ref m_maxY, ref m_disY);
        }

        /// <summary>
        /// 判断X轴单位
        /// </summary>
        private void JudgeXTVCV()
        {
            switch (m_base)
            {
                case EnumBase.T:
                    m_maxX = m_maxXT;
                    m_disX = m_disXT;
                    break;
                case EnumBase.V:
                    m_maxX = m_maxXV;
                    m_disX = m_disXV;
                    break;
                case EnumBase.CV:
                    m_maxX = m_maxXCV;
                    m_disX = m_disXCV;
                    break;
            }
        }

        /// <summary>
        /// 赋值源数据到线数据
        /// </summary>
        private void CalLineData()
        {
            m_DataSourseA.Clear();
            m_DataSourseB.Clear();
            m_DataSourseC.Clear();
            m_DataSourseD.Clear();

            double total = 0;
            for (int i = 0; i < m_count; i++)
            {
                System.Drawing.Point[] phaseA = new System.Drawing.Point[m_TList[i].Count * 2];
                System.Drawing.Point[] phaseB = new System.Drawing.Point[m_TList[i].Count * 2];
                System.Drawing.Point[] phaseC = new System.Drawing.Point[m_TList[i].Count * 2];
                System.Drawing.Point[] phaseD = new System.Drawing.Point[m_TList[i].Count * 2];

                for (int j = 0; j < m_TList[i].Count; j++)
                {
                    phaseA[j * 2] = GetDrawPoint(total, m_PerAList[i][j * 2]);
                    phaseB[j * 2] = GetDrawPoint(total, m_PerBList[i][j * 2]);
                    phaseC[j * 2] = GetDrawPoint(total, m_PerCList[i][j * 2]);
                    phaseD[j * 2] = GetDrawPoint(total, m_PerDList[i][j * 2]);
                    
                    switch (m_base)
                    {
                        case EnumBase.T: total += m_TList[i][j]; break;
                        case EnumBase.V: total += m_VList[i][j]; break;
                        case EnumBase.CV: total += m_CVList[i][j]; break;
                    }

                    phaseA[j * 2 + 1] = GetDrawPoint(total, m_PerAList[i][j * 2 + 1]);
                    phaseB[j * 2 + 1] = GetDrawPoint(total, m_PerBList[i][j * 2 + 1]);
                    phaseC[j * 2 + 1] = GetDrawPoint(total, m_PerCList[i][j * 2 + 1]);
                    phaseD[j * 2 + 1] = GetDrawPoint(total, m_PerDList[i][j * 2 + 1]);
                }

                m_DataSourseA.Add(phaseA);
                m_DataSourseB.Add(phaseB);
                m_DataSourseC.Add(phaseC);
                m_DataSourseD.Add(phaseD);
            }

            DrawBitmap();
        }

        /// <summary>
        /// 初始化尺寸参数
        /// </summary>
        private void InitCanvas()
        {
            m_boardWidth = (int)mainCanvas.ActualWidth;
            m_boardHeight = (int)mainCanvas.ActualHeight;
            m_chartLeft = 50;
            m_chartTop = 20;
            m_chartWidth = m_boardWidth - 2 * m_chartLeft;
            m_chartHeight = m_boardHeight - 2 * m_chartTop;
            m_chartRight = m_boardWidth - m_chartLeft;
            m_chartBottom = m_boardHeight - m_chartTop;

            PresentationSource source = PresentationSource.FromVisual(this);
            m_bitmap = new WriteableBitmap(m_boardWidth, m_boardHeight, 96.0 * source.CompositionTarget.TransformToDevice.M11, 96.0 * source.CompositionTarget.TransformToDevice.M22, PixelFormats.Bgr24, null);
            displayImage.Source = m_bitmap;

            CalLineData();
        }

        /// <summary>
        /// 画图
        /// </summary>
        public void DrawBitmap()
        {
            if (null == m_bitmap)
            {
                return;
            }

            m_bitmap.Lock();
            System.Drawing.Bitmap backBitmap = new System.Drawing.Bitmap(m_boardWidth, m_boardHeight, m_bitmap.BackBufferStride, System.Drawing.Imaging.PixelFormat.Format24bppRgb, m_bitmap.BackBuffer);

            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(backBitmap);
            graphics.Clear(System.Drawing.Color.White);//整张画布置为白色                                    
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            if (0 != m_count)
            {
                DrawXYAxis(graphics);

                DrawXAxisTicks(graphics);
                DrawYAxisTicks(graphics);

                DrawPhaseSplitPt(graphics, m_DataSourseA, System.Drawing.Brushes.Blue);

                if (Visibility.Visible == cboxA.Visibility && m_showA)
                {
                    DrawPolyline(graphics, m_DataSourseA, new System.Drawing.Pen(System.Drawing.Color.Red, 2));
                }
                if (Visibility.Visible == cboxB.Visibility && m_showB)
                {
                    DrawPolyline(graphics, m_DataSourseB, new System.Drawing.Pen(System.Drawing.Color.Green, 2));
                }
                if (Visibility.Visible == cboxC.Visibility && m_showC)
                {
                    DrawPolyline(graphics, m_DataSourseC, new System.Drawing.Pen(System.Drawing.Color.Blue, 2));
                }
                if (Visibility.Visible == cboxD.Visibility && m_showD)
                {
                    DrawPolyline(graphics, m_DataSourseD, new System.Drawing.Pen(System.Drawing.Color.Orange, 2));
                }

                DrawCurrPhaseBG(graphics);

                if (m_showMark)
                {
                    DrawMarker(graphics);
                }
            }

            graphics.Flush();
            graphics.Dispose();
            graphics = null;

            backBitmap.Dispose();
            backBitmap = null;

            m_bitmap.AddDirtyRect(new Int32Rect(0, 0, m_boardWidth, m_boardHeight));
            m_bitmap.Unlock();
        }

        /// <summary>
        /// 画X轴Y轴
        /// </summary>
        private void DrawXYAxis(System.Drawing.Graphics graphics)
        {
            //X轴线段
            graphics.DrawLine(System.Drawing.Pens.Black, m_chartLeft, m_chartBottom, m_chartRight, m_chartBottom);

            //X轴单位
            switch (m_base)
            {
                case EnumBase.T: graphics.DrawString(DlyBase.SC_TUNIT, new System.Drawing.Font("微软雅黑", 9), System.Drawing.Brushes.Blue, m_chartRight + 10, m_chartBottom); break;
                case EnumBase.V: graphics.DrawString(DlyBase.SC_VUNITML, new System.Drawing.Font("微软雅黑", 9), System.Drawing.Brushes.Blue, m_chartRight + 10, m_chartBottom); break;
                case EnumBase.CV: graphics.DrawString(DlyBase.SC_CVUNIT, new System.Drawing.Font("微软雅黑", 9), System.Drawing.Brushes.Blue, m_chartRight + 10, m_chartBottom); break;
            }

            //Y轴线段
            graphics.DrawLine(System.Drawing.Pens.Black, m_chartLeft, m_chartTop, m_chartLeft, m_chartBottom);

            //Y轴单位
            graphics.DrawString(DlyBase.SC_FERUNIT, new System.Drawing.Font("微软雅黑", 9), System.Drawing.Brushes.Blue, m_chartLeft - 15, m_chartTop - 15);
        }

        /// <summary>
        /// 画X轴刻度尺
        /// </summary>
        private void DrawXAxisTicks(System.Drawing.Graphics graphics)
        {
            for (double i = m_minX; i <= m_maxX; i += m_disX)
            {
                //画x轴刻度
                int x = (int)(m_chartLeft + m_chartWidth * (i - m_minX) / (m_maxX - m_minX));
                graphics.DrawLine(System.Drawing.Pens.Black, x, m_chartBottom, x, m_chartBottom + 5);

                //画x轴网格
                if (m_showGrid)
                {
                    System.Drawing.Pen dashline = new System.Drawing.Pen(System.Drawing.Color.Black);
                    dashline.DashPattern = new float[] { 2, 5 };
                    graphics.DrawLine(dashline, x, m_chartTop, x, m_chartBottom);
                }

                //画x轴字符
                graphics.DrawString(i.ToString(), new System.Drawing.Font("微软雅黑", 9), System.Drawing.Brushes.Blue, x - 10, m_chartBottom + 5);
            }
        }

        /// <summary>
        /// 画Y轴刻度尺
        /// </summary>
        private void DrawYAxisTicks(System.Drawing.Graphics graphics)
        {
            for (var i = m_minY; i <= m_maxY; i += m_disY)
            {
                //画y轴刻度
                int y = (int)(m_chartBottom - m_chartHeight * (i - m_minY) / (m_maxY - m_minY));
                graphics.DrawLine(System.Drawing.Pens.Black, m_chartLeft - 5, y, m_chartLeft, y);

                //画x轴网格
                if (m_showGrid)
                {
                    System.Drawing.Pen dashline = new System.Drawing.Pen(System.Drawing.Color.Black);
                    dashline.DashPattern = new float[] { 2, 5 };
                    graphics.DrawLine(dashline, m_chartLeft, y, m_chartRight, y);
                }

                //画y轴字符
                graphics.DrawString(i.ToString(), new System.Drawing.Font("微软雅黑", 9), System.Drawing.Brushes.Blue, m_chartLeft - 10, y - 5, new System.Drawing.StringFormat(System.Drawing.StringFormatFlags.DirectionRightToLeft));
            }
        }

        /// <summary>
        /// 画阶段内不同步骤间隔点
        /// </summary>
        /// <param name="dataSourse"></param>
        /// <param name="brush"></param>
        private void DrawPhaseSplitPt(System.Drawing.Graphics graphics, List<System.Drawing.Point[]> dataSourse, System.Drawing.Brush brush)
        {
            foreach (var list in dataSourse)
            {
                int index = 0;
                foreach (var t in list)
                {
                    if (1 == index++ % 2)
                    {
                        continue;
                    }

                    graphics.FillEllipse(brush, t.X, m_chartBottom - 10, 10, 10);
                } 
            }
        }

        /// <summary>
        /// 画折线
        /// </summary>
        private void DrawPolyline(System.Drawing.Graphics graphics, List<System.Drawing.Point[]> dataSourse, System.Drawing.Pen pen)
        {
            foreach (var list in dataSourse)
            {
                if (null != list && 1 < list.Length)
                {
                    graphics.DrawLines(pen, list);
                }
            }

            for (int i = 0; i < dataSourse.Count - 1; i++)
            {
                graphics.DrawLine(pen, dataSourse[i].Last(), dataSourse[i + 1].First());
            }
        }

        /// <summary>
        /// 画当前选中阶段
        /// </summary>
        /// <param name="index"></param>
        private void DrawCurrPhaseBG(System.Drawing.Graphics graphics)
        {
            if (-1 != MCurrClickIndex && 0 < m_DataSourseA[MCurrClickIndex].Length)
            {
                int xS = m_DataSourseA[MCurrClickIndex].First().X;
                int xE = m_DataSourseA[MCurrClickIndex].Last().X;
                graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(125, 0,0,255)), xS, m_chartTop, xE - xS, m_chartHeight);
            }
        }

        /// <summary>
        /// 画垂直标记线
        /// </summary>
        private void DrawMarker(System.Drawing.Graphics graphics)
        {
            graphics.DrawLine(System.Drawing.Pens.Black, m_markerX, m_chartTop, m_markerX, m_chartBottom);
        }

        /// <summary>
        /// 获取点对应的画图值
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private System.Drawing.Point GetDrawPoint(double x, double y)
        {
            return new System.Drawing.Point(GetDrawX(x), GetDrawY(y));
        }

        /// <summary>
        /// 获取X对应的画图值
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private int GetDrawX(double val)
        {
            return (int)(m_chartLeft + (val - m_minX) * m_chartWidth / (m_maxX - m_minX));
        }

        /// <summary>
        /// 获取Y对应的画图值
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private int GetDrawY(double val)
        {
            return (int)(m_chartTop + (m_maxY - val) * m_chartHeight / (m_maxY - m_minY));
        }

        /// <summary>
        /// 获取X对应的实际值
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private double GetRealX(int val)
        {
            return (val - m_chartLeft) * (m_maxX - m_minX) / m_chartWidth + m_minX;
        }

        /// <summary>
        /// 获取Y对应的实际值
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private double GetRealY(int val)
        {
            return m_maxY - (val - m_chartTop) * (m_maxY - m_minY) / m_chartHeight;
        }

        /// <summary>
        /// 窗体尺寸变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            InitCanvas();
        }

        /// <summary>
        /// 鼠标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point pt = e.GetPosition(mainCanvas);
            if (pt.X < m_chartLeft || pt.Y < m_chartTop || pt.X > m_chartRight || pt.Y > m_chartBottom)
            {
                return;
            }

            if (Cursors.ScrollWE == this.Cursor)
            {
                MClicked = true;
            }
            else
            {
                m_markerX = (int)pt.X;
                SetGridValue();
  
                for (int i = 0; i < m_DataSourseA.Count; i++)
                {
                    if (m_markerX >= m_DataSourseA[i].First().X && m_markerX <= m_DataSourseA[i].Last().X)
                    {
                        MCurrClickIndex = i;
                        DrawBitmap();
                        return;
                    }
                }

                MCurrClickIndex = -1;
                DrawBitmap();
            }            
        }

        private void mainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Point pt = e.GetPosition(mainCanvas);
            if (pt.X < m_chartLeft || pt.Y < m_chartTop || pt.X > m_chartRight || pt.Y > m_chartBottom)
            {
                return;
            }

            if (MClicked)
            {
                m_markerX = (int)pt.X;
                SetGridValue();
                DrawBitmap();
            }
            else
            {
                if (m_showMark)
                {
                    if (Math.Abs(pt.X - m_markerX) < 20)
                    {
                        this.Cursor = Cursors.ScrollWE;
                    }
                    else
                    {
                        this.Cursor = null;
                    }
                } 
            }
        }

        private void mainCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MClicked = false;

            System.Windows.Point pt = e.GetPosition(mainCanvas);
            if (pt.X > m_chartRight && pt.Y > m_chartBottom)
            {
                switch (m_base)
                {
                    case EnumBase.T: m_base = EnumBase.V; break;
                    case EnumBase.V: m_base = EnumBase.CV; break;
                    case EnumBase.CV: m_base = EnumBase.T; break;
                }

                JudgeXTVCV();
                CalLineData();
            }
        }

        private void SetGridValue()
        {
            txtBase.Text = GetRealX(m_markerX).ToString("f2");

            int indexI = -1;
            int indexJ = -1;
            for (int i = 0; i < m_DataSourseA.Count; i++)
            {
                if (null == m_DataSourseA[i] || 2 > m_DataSourseA[i].Length)
                {
                    continue;
                }

                if (m_markerX >= m_DataSourseA[i].First().X && m_markerX <= m_DataSourseA[i].Last().X)
                {
                    indexI = i;
                    for (int j = 0; j < m_DataSourseA[i].Length; j += 2)
                    {
                        if (m_markerX >= m_DataSourseA[i][j].X && m_markerX <= m_DataSourseA[i][j + 1].X)
                        {
                            indexJ = j;
                            double specific = (double)(m_markerX - m_DataSourseA[i][j].X) / (m_DataSourseA[i][j + 1].X - m_DataSourseA[i][j].X);
                            txtA.Text = GetRealY((int)(specific * (m_DataSourseA[i][j + 1].Y - m_DataSourseA[i][j].Y) + m_DataSourseA[i][j].Y)).ToString("F2");
                            txtB.Text = GetRealY((int)(specific * (m_DataSourseB[i][j + 1].Y - m_DataSourseB[i][j].Y) + m_DataSourseB[i][j].Y)).ToString("F2");
                            txtC.Text = GetRealY((int)(specific * (m_DataSourseC[i][j + 1].Y - m_DataSourseC[i][j].Y) + m_DataSourseC[i][j].Y)).ToString("F2");
                            txtD.Text = GetRealY((int)(specific * (m_DataSourseD[i][j + 1].Y - m_DataSourseD[i][j].Y) + m_DataSourseD[i][j].Y)).ToString("F2");
                            txtName.Text = m_namePhase[i] + "(" + m_nameStep[i][j / 2] + ")";
                            break;
                        }
                    }
                }
            }

            if (-1 == indexI || -1 == indexJ)
            {
                txtBase.Text = "";
                txtA.Text = "";
                txtB.Text = "";
                txtC.Text = "";
                txtD.Text = "";
                if (-1 == MCurrClickIndex)
                {
                    txtName.Text = "";
                }
                else
                {
                    txtName.Text = m_namePhase[MCurrClickIndex];
                }
            }
        }

        private void cboxA_Checked(object sender, RoutedEventArgs e)
        {
            m_showA = true;
            DrawBitmap();
        }

        private void cboxA_Unchecked(object sender, RoutedEventArgs e)
        {
            m_showA = false;
            DrawBitmap();
        }

        private void cboxB_Checked(object sender, RoutedEventArgs e)
        {
            m_showB = true;
            DrawBitmap();
        }

        private void cboxB_Unchecked(object sender, RoutedEventArgs e)
        {
            m_showB = false;
            DrawBitmap();
        }

        private void cboxC_Checked(object sender, RoutedEventArgs e)
        {
            m_showC = true;
            DrawBitmap();
        }

        private void cboxC_Unchecked(object sender, RoutedEventArgs e)
        {
            m_showC = false;
            DrawBitmap();
        }

        private void cboxD_Checked(object sender, RoutedEventArgs e)
        {
            m_showD = true;
            DrawBitmap();
        }

        private void cboxD_Unchecked(object sender, RoutedEventArgs e)
        {
            m_showD = false;
            DrawBitmap();
        }

        private void chboxGrid_Checked(object sender, RoutedEventArgs e)
        {
            m_showGrid = true;
            DrawBitmap();
        }

        private void chboxGrid_Unchecked(object sender, RoutedEventArgs e)
        {
            m_showGrid = false;
            DrawBitmap();
        }

        private void chboxMarker_Checked(object sender, RoutedEventArgs e)
        {
            m_showMark = true;
            DrawBitmap();
        }

        private void chboxMarker_Unchecked(object sender, RoutedEventArgs e)
        {
            m_showMark = false;
            DrawBitmap();
        }
    }
}
