using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Chromatogram
{
    /**
     * ClassName: ContrastManager
     * Description: 色谱图对比管理类
     * Version: 1.0
     * Create:  2018/05/16
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class ContrastManager
    {
        private double m_dpiX = 96;             //DPI参数，从系统获取
        private double m_dpiY = 96;             //DPI参数，从系统获取
        private int c_chartLeft = 70;           //图表区域起点左（横向边距）
        private int c_chartTop = 40;            //图表区域起点上（纵向边距）
        private int m_boardWidth = 100;         //画板宽度
        private int m_boardHeight = 100;        //画板高度
        private int m_chartLeft = 70;           //图表区域起点左（横向边距）
        public int MChartLeft
        {
            get
            {
                return m_chartLeft;
            }
        }
        private int m_chartTop = 40;            //图表区域起点上（纵向边距）
        private int m_chartWidth;               //图表区域宽度
        private int m_chartHeight;              //图表区域高度
        private int m_chartRight;               //图表区域终点右
		public int MChartRight
        {
            get
            {
                return m_chartRight;
            }
        }
        private int m_chartBottom;              //图表区域终点底
        private Font m_fontText = null;         //默认字体
        private Pen m_penGrid = null;           //默认网格画笔

        //信号集合列表
        public List<CurveSet> MListLines { get; }
        //当前选中信号集合
        public CurveSet MLines
        {
            get
            {
                return MListLines[MSelectIndex];
            }
        }
        //当前选中信号的序号
        public int MSelectIndex { get; set; }
        //可见信号的总数
        public int MItemShowCount { get; set; }
        //是否显示多Y轴
        public bool MMultiY { get; set; }
        //是否显示网格
        public bool MShowGrid { get; set; }
        //局部放大
        private ScaleData MScale { get; }
        //是否处于放大状态
        public bool MEnabledZoom
        {
            get
            {
                return 1 < MScale.MCount ? true : false;
            }
        }
        public List<int> m_minPartXIndex = new List<int>();         //局部图起始点的序号
        public List<int> m_maxPartXIndex = new List<int>();         //局部图终结点的序号
        public double m_minPartX = 0;                               //局部图最小值
        public double m_maxPartX = 1;                               //局部图最大值 
        //标记列表
        public List<MarkerInfo> MMarkerList { get; }
        public string MMarkerInfo
        {
            get
            {
                StringBuilderSplit sb = new StringBuilderSplit("^");
                foreach (var it in MMarkerList)
                {
                    sb.Append(it.MType + ";" + it.MT + ";" + it.MV + ";" + it.MCV);
                }
                return sb.ToString();
            }
            set
            {
                MMarkerList.Clear();
                string[] arrName = value.Split('^');
                foreach (var it in arrName)
                {
                    string[] arrVal = it.Split(';');
                    MMarkerList.Add(new MarkerInfo(arrVal[0], Convert.ToDouble(arrVal[1]), Convert.ToDouble(arrVal[2]), Convert.ToDouble(arrVal[3])));
                }
            }
        }

        /// <summary>
        /// 谱图对比选择项
        /// </summary>
        public List<bool> m_listContrast = new List<bool>();
        /// <summary>
        /// 谱图对比选择项已选的数量
        /// </summary>
        public int MCountContrast
        {
            get
            {
                int count = 0;
                foreach (var it in m_listContrast)
                {
                    if (it)
                    {
                        count++;
                    }
                }
                return count;
            }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        public ContrastManager()
        {
            m_fontText = new Font("微软雅黑", 12);
            m_penGrid = new Pen(Color.Gray);
            m_penGrid.DashPattern = new float[] { 1, 3 };

            MListLines = new List<CurveSet>();
            MScale = new ScaleData();
            MMarkerList = new List<MarkerInfo>();
        }

        public void CalMaxMin()
        {
            double min = MListLines[0].MMin;
            double max = MListLines[0].MMax;
            foreach (var it in MListLines)
            {
                if (it.MMin < min)
                {
                    min = it.MMin;
                }
                else if (it.MMax > max)
                {
                    max = it.MMax;
                }
            }
            foreach (var it in MListLines)
            {
                it.MMaxAuto = max;
                it.MMaxFix = max;
            }

            if(m_minPartXIndex.Count!= MListLines.Count)
            {
                for(int i=0;i<MListLines.Count;i++)
                {
                    m_minPartXIndex.Add(0);
                    m_maxPartXIndex.Add(0);
                }
            }
            else
            {
                for (int i = 0; i < MListLines.Count; i++)
                {
                    m_minPartXIndex[i] = 0;
                    m_maxPartXIndex[i] = 0;
                }
            }
        }

        /// <summary>
        /// 计算当前显示的曲线数量
        /// </summary>
        public void CalItemListShow()
        {
            MItemShowCount = MListLines.Count * MCountContrast;
            UpdateWidthHeight();
        }

        /// <summary>
        /// 计算DPI（最开始赋值）
        /// </summary>
        /// <param name="visual"></param>
        public void InitdpiXY(System.Windows.Media.Visual visual)
        {
            System.Windows.PresentationSource source = System.Windows.PresentationSource.FromVisual(visual);
            m_dpiX = 96.0 * source.CompositionTarget.TransformToDevice.M11;
            m_dpiY = 96.0 * source.CompositionTarget.TransformToDevice.M22;
        }

        /// <summary>
        /// 更新尺寸参数
        /// </summary>
        public void UpdateCanvas(System.Windows.Controls.Image image, int width, int height, int left = 70, int top = 40)
        {
            if (0 == width || 0 == height)
            {
                return;
            }

            c_chartLeft = left;
            c_chartTop = top;

            m_boardWidth = width;
            m_boardHeight = height;
            UpdateWidthHeight();
        }

        /// <summary>
        /// 添加局部图的比例数组
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void AddScale(Rectangle rect)
        {
            rect = Rectangle.Intersect(new Rectangle(m_chartLeft, m_chartTop, m_chartWidth, m_chartHeight), rect);

            //重叠拉框范围须大于5*5
            if (5 > rect.Width || 5 > rect.Height)
            {
                return;
            }

            MScale.Add((double)(rect.X - m_chartLeft) / m_chartWidth,
                (double)(rect.Right - m_chartLeft) / m_chartWidth,
                1.0 - (double)(rect.Bottom - m_chartTop) / m_chartHeight,
                1.0 - (double)(rect.Y - m_chartTop) / m_chartHeight);
        }

        /// <summary>
        /// 删除局部图的最近比例数组
        /// </summary>
        public void RemoveScale()
        {
            MScale.RemoveLast();
        }

        /// <summary>
        /// 清空局部图
        /// </summary>
        public void ClearScale()
        {
            MScale.Clear();
        }

        /// <summary>
        /// 新增标记
        /// </summary>
        /// <param name="item"></param>
        public void AddMarker(MarkerInfo item)
        {
            if (0 == MLines.MXList.Count)
            {
                //无数据
                return;
            }

            if (-1 == item.MValX)
            {
                //-1认为是实时值，即最后一个值
                item.MT = MLines.GetXList(EnumBase.T).Last();
                item.MV = MLines.GetXList(EnumBase.V).Last();
                item.MCV = MLines.GetXList(EnumBase.CV).Last();
            }
            else
            {
                //先假设是最后一个值
                int index = MLines.MXList.Count - 1;
                for (int i = 0; i < MLines.MXList.Count; i++)
                {
                    if (item.MValX <= MLines.MXList[i])
                    {
                        //找到最接近的值
                        index = i;
                        break;
                    }
                }
                switch (MLines.MBase)
                {
                    case EnumBase.T:
                        item.MT = item.MValX;
                        item.MV = MLines.GetXList(EnumBase.V)[index];
                        item.MCV = MLines.GetXList(EnumBase.CV)[index];
                        break;
                    case EnumBase.V:
                        item.MT = MLines.GetXList(EnumBase.T)[index];
                        item.MV = item.MValX;
                        item.MCV = MLines.GetXList(EnumBase.CV)[index];
                        break;
                    case EnumBase.CV:
                        item.MT = MLines.GetXList(EnumBase.T)[index];
                        item.MV = MLines.GetXList(EnumBase.V)[index];
                        item.MCV = item.MValX;
                        break;
                }
            }

            MMarkerList.Add(item);
        }

        /// <summary>
        /// 删除标记
        /// </summary>
        /// <param name="index"></param>
        public void DelMarker(int index)
        {
            MMarkerList.RemoveAt(index);
        }

        /// <summary>
        /// 返回单标尺信息
        /// </summary>
        /// <param name="coordX"></param>
        /// <returns></returns>
        public string GetRulerOdd(double coordX)
        {
            if (-1 == MLines.MSelectIndex)
            {
                return "";
            }

            double valueX = GetValueX(coordX);

            double valueY = GetValueY(valueX);

            return "X: " + valueX + "\n" + "Y: " + valueY.ToString("f2");
        }

        /// <summary>
        /// 返回双标尺信息
        /// </summary>
        /// <param name="coordX"></param>
        /// <returns></returns>
        public string GetRulerEven(double coordX1, double coordX2)
        {
            if (-1 == MLines.MSelectIndex)
            {
                return "";
            }

            if (coordX1 > coordX2)
            {
                double temp = coordX1;
                coordX1 = coordX2;
                coordX2 = temp;
            }

            double valueX1 = GetValueX(coordX1);
            double valueY1 = GetValueY(valueX1);
            double valueX2 = GetValueX(coordX2);
            double valueY2 = GetValueY(valueX2);

            int indexB = MLines.GetIndex(valueX1);
            int indexE = MLines.GetIndex(valueX2);

            if (-1 == indexB || -1 == indexE)
            {
                return "";
            }

            double maxVal = valueY1;
            double minVal = valueY1;
            double totalVal = 0;
            for (int i = indexB; i <= indexE; i++)
            {
                double tempVal = MLines.MSelectItem.GetData(i);
                
                if (MLines.MSelectItem.GetData(i) > maxVal)
                {
                    maxVal = tempVal;
                }
                else if (MLines.MSelectItem.GetData(i) < minVal)
                {
                    minVal = tempVal;
                }

                totalVal += tempVal;
            }

            StringBuilderSplit sb = new StringBuilderSplit("\n");
            sb.Append("X1:" + valueX1);
            sb.Append("Y1:" + valueY1.ToString("f2"));
            sb.Append("X2:" + valueX2);
            sb.Append("Y2:" + valueY2.ToString("f2"));
            sb.Append("Y_Max:" + maxVal.ToString("f2"));
            sb.Append("Y_Min:" + minVal.ToString("f2"));
            sb.Append("Y_Aver:" + (totalVal / (indexE - indexB + 1)).ToString("f2"));
            sb.Append("△X:" + (valueX2 - valueX1).ToString("f2"));
            return sb.ToString();
        }

        /// <summary>
        /// 判断是否切换X轴
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public bool IsSwitchAxisX(System.Windows.Point pt)
        {
            if (pt.X > m_chartRight && pt.Y > m_chartBottom)//切换X轴
            {
                switch (MLines.MBase)
                {
                    case EnumBase.T:
                        foreach(var it in MListLines)
                        {
                            it.MBase = EnumBase.V;
                        }
                        break;
                    case EnumBase.V:
                        foreach (var it in MListLines)
                        {
                            it.MBase = EnumBase.CV;
                        }
                        break;
                    case EnumBase.CV:
                        foreach (var it in MListLines)
                        {
                            it.MBase = EnumBase.T;
                        }
                        break;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是否移动Y轴
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public bool IsMoveY(System.Windows.Point pt)
        {
            if (pt.X > 0 && pt.X < m_chartLeft && pt.Y> m_chartTop && pt.Y < m_chartBottom)//切换X轴
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void MoveBeginY(double start, double end)
        {
            if (-1 != MLines.MSelectIndex)
            {
                MLines.MSelectItem.MMove = (start - end) / m_chartHeight * (MLines.MSelectItem.MMaxPart - MLines.MSelectItem.MMinPart);
            }
        }
        public void MoveEndY(double start, double end)
        {
            if (-1 != MLines.MSelectIndex)
            {
                MLines.MSelectItem.MMove = (start - end) / m_chartHeight * (MLines.MSelectItem.MMaxPart - MLines.MSelectItem.MMinPart);

                MLines.MSelectItem.MMaxFix = Math.Round(MLines.MSelectItem.MMax - MLines.MSelectItem.MMove, 2);
                MLines.MSelectItem.MMinFix = Math.Round(MLines.MSelectItem.MMin - MLines.MSelectItem.MMove, 2);
                MLines.MSelectItem.MAxisScale = EnumAxisScale.Fixed;
                MLines.MSelectItem.MMove = 0;
            }
        }


        /// <summary>
        /// 画图
        /// </summary>
        public System.Windows.Media.Imaging.WriteableBitmap DrawBitmap()
        {
            System.Windows.Media.Imaging.WriteableBitmap bitmap = new System.Windows.Media.Imaging.WriteableBitmap(m_boardWidth, m_boardHeight, m_dpiX, m_dpiY, System.Windows.Media.PixelFormats.Bgr24, null);

            bitmap.Lock();

            System.Drawing.Bitmap backBitmap = new System.Drawing.Bitmap(m_boardWidth, m_boardHeight, bitmap.BackBufferStride, System.Drawing.Imaging.PixelFormat.Format24bppRgb, bitmap.BackBuffer);
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(backBitmap);
            graphics.Clear(System.Drawing.Color.White);//整张画布置为白色                                    
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            CalSacleXY(MScale);
            DrawX(graphics);
            if (null != MLines.MSelectItem)
            {
                if (MMultiY)//多Y轴
                {
                    int index = 1;
                    foreach (var line in MLines.MItemList)
                    {
                        if (line.MShow)
                        {
                            DrawY(graphics, line, m_chartLeft * (index++) / MItemShowCount, line.Equals(MLines.MSelectItem));
                        }
                    }
                }
                else//单Y轴
                {
                    DrawY(graphics, MLines.MSelectItem, m_chartLeft, true);
                }
            }

            List<Point[]> listPoint = CalLinesPoint(m_chartWidth, m_chartHeight, m_chartLeft, m_chartBottom);
            DrawLines(graphics, listPoint);

            DrawMarker(graphics);

            graphics.Flush();
            graphics.Dispose();
            graphics = null;

            backBitmap.Dispose();
            backBitmap = null;

            bitmap.AddDirtyRect(new System.Windows.Int32Rect(0, 0, m_boardWidth, m_boardHeight));
            bitmap.Unlock();

            return bitmap;
        }

        /***内部处理****/

        /// <summary>
        /// 更新尺寸参数
        /// </summary>
        protected void UpdateWidthHeight()
        {
            if (MMultiY)
            {
                m_chartLeft = c_chartLeft * MItemShowCount;
            }
            else
            {
                m_chartLeft = c_chartLeft;
            }
            m_chartWidth = m_boardWidth - m_chartLeft - c_chartLeft;
            m_chartRight = m_boardWidth - c_chartLeft;
            m_chartHeight = m_boardHeight - 2 * m_chartTop;
            m_chartBottom = m_boardHeight - m_chartTop;
        }

        /// <summary>
        /// 根据坐标X返回x
        /// </summary>
        /// <param name="coordX"></param>
        protected double GetValueX(double coordX)
        {
            return Math.Round((coordX - m_chartLeft) / m_chartWidth * (m_maxPartX - m_minPartX) + m_minPartX, 2);
        }

        /// <summary>
        /// 根据x返回y
        /// </summary>
        /// <param name="coordX"></param>
        protected double GetValueY(double valueX)
        {
            int index = MLines.GetIndex(valueX);
            if (-1 != index)
            {
                return MLines.MSelectItem.GetData(index);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 绘图，X轴
        /// </summary>
        /// <param name="mgph"></param>
        protected void DrawX(Graphics mgph)
        {
            //绘制X轴
            mgph.DrawLine(Pens.Black, m_chartLeft, m_chartBottom, m_chartRight, m_chartBottom);//X轴
            mgph.DrawPolygon(Pens.Black, new PointF[3] { new PointF(m_chartRight, m_chartBottom + 5), new PointF(m_chartRight, m_chartBottom - 5), new PointF(m_chartRight + 10, m_chartBottom) });//X轴三角形
            mgph.DrawString(MLines.MUnit, m_fontText, Brushes.Black, new PointF(m_chartRight + 10, m_chartBottom - 5));//标签

            //绘制X刻度
            double tempMax = 0;
            double tempMin = 0;
            double radix = 0;
            double mean = 0;
            double drift = 0;
            int count = 0;
            CalXMaxMin(m_chartWidth, out tempMax, out tempMin, out radix, out mean, out drift, out count);
            for (int i = 0; i <= count; i++)
            {
                float x = (float)(m_chartLeft + drift + mean * i);
                mgph.DrawLine(Pens.Black, x, m_chartBottom, x, m_chartBottom + 10);
                if (MShowGrid)
                {
                    mgph.DrawLine(m_penGrid, x, m_chartTop, x, m_chartBottom);
                }
                double val = tempMin + radix * i;
                mgph.DrawString(val.ToString("f2"), m_fontText, Brushes.Black, new PointF(x - 10, m_chartBottom + 10));
            }
            if (mean >= 50)//需要画二级刻度
            {
                double littleMean = mean / 5;
                for (int i = -1; i <= count; i++)
                {
                    for (int j = 1; j < 5; j++)
                    {
                        float x = (float)(m_chartLeft + drift + mean * i + littleMean * j);
                        if (x >= m_chartLeft && x <= m_chartRight)
                        {
                            mgph.DrawLine(Pens.Black, x, m_chartBottom, x, m_chartBottom + 5);
                            if (MShowGrid)
                            {
                                mgph.DrawLine(m_penGrid, x, m_chartTop, x, m_chartBottom);
                            }
                            if (radix >= 0.1 && littleMean >= 50)
                            {
                                double val = tempMin + radix * i + radix / 5 * j;
                                mgph.DrawString(val.ToString("f2"), m_fontText, Brushes.Black, new PointF(x - 10, m_chartBottom + 10));
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 绘图，Y轴
        /// </summary>
        /// <param name="mgph"></param>
        /// <param name="line"></param>
        /// <param name="drawX"></param>
        protected void DrawY(Graphics mgph, Curve line, int drawX, bool selected)
        {
            if (null == line)
            {
                return;
            }

            //文字右对齐
            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);

            //绘制Y轴
            mgph.DrawLine(new Pen(line.MColor), drawX, m_chartTop, drawX, m_chartBottom);
            mgph.DrawPolygon(new Pen(line.MColor), new PointF[3] { new PointF(drawX - 5, m_chartTop), new PointF(drawX + 5, m_chartTop), new PointF(drawX, m_chartTop - 10) });//Y轴三角形
            mgph.DrawString(line.MUnit, m_fontText, new SolidBrush(line.MColor), new PointF(drawX - 20, m_chartTop - 30));//标签

            //绘制刻度
            double tempMax = 0;
            double tempMin = 0;
            double radix = 0;
            double mean = 0;
            double drift = 0;
            int count = 0;
            CalYMaxMin(m_chartHeight, line.MMaxPart, line.MMinPart, out tempMax, out tempMin, out radix, out mean, out drift, out count);
            string unit = CalUnit(radix);
            for (int i = 0; i <= count; i++)
            {
                float y = (float)(m_chartBottom - drift - mean * i);
                mgph.DrawLine(new Pen(line.MColor), drawX - 10, y, drawX, y);
                if (MShowGrid && selected)
                {
                    mgph.DrawLine(m_penGrid, m_chartLeft, y, m_chartRight, y);
                }
                double val = tempMin + radix * i;
                mgph.DrawString(val < 0 ? (Math.Abs(val).ToString(unit) + "-") : val.ToString(unit), m_fontText, new SolidBrush(line.MColor), new PointF(drawX - 10, y - 5), format);
            }
            if (mean > 50)//需要画二级刻度
            {
                unit = CalUnit(radix / 10);
                double littleMean = mean / 5;
                for (int i = -1; i <= count; i++)
                {
                    for (int j = 1; j < 5; j++)
                    {
                        float y = (float)(m_chartBottom - drift - mean * i - littleMean * j);
                        if (y >= m_chartTop && y <= m_chartBottom)
                        {
                            mgph.DrawLine(new Pen(line.MColor), drawX - 5, y, drawX, y);
                            if (MShowGrid && selected)
                            {
                                mgph.DrawLine(m_penGrid, m_chartLeft, y, m_chartRight, y);
                            }
                            if (radix >= 0.001 && littleMean >= 30)
                            {
                                double val = tempMin + radix * i + radix / 5 * j;
                                mgph.DrawString(val < 0 ? (Math.Abs(val).ToString(unit) + "-") : val.ToString(unit), m_fontText, new SolidBrush(line.MColor), new PointF(drawX - 10, y - 5), format);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 绘图，曲线集合
        /// </summary>
        /// <param name="mgph"></param>
        /// <param name="listPoint"></param>
        protected void DrawLines(Graphics mgph, List<Point[]> listPoint)
        {
            Region tmp = mgph.Clip;
            mgph.Clip = new Region(new Rectangle(m_chartLeft, m_chartTop, m_chartWidth, m_chartHeight));
            try
            {
                int select = -1;
                int ij = 0;
                for (int i = 0; i < m_listContrast.Count; i++)
                {
                    if (m_listContrast[i])
                    {
                        for (int j = 0; j < MListLines.Count; j++)
                        {
                            if (MLines.MSelectIndex != i || MSelectIndex != j)
                            {
                                if (null != listPoint[j])
                                {
                                    mgph.DrawLines(new Pen(MListLines[j].MItemList[i].MColor), listPoint[ij]);
                                }
                            }
                            else
                            {
                                select = ij;
                            }
                            ij++;
                        }
                    }
                }
                

                //选中加粗,最后画
                if (-1 != select && null != listPoint[select])
                {
                    mgph.DrawLines(new Pen(MLines.MSelectItem.MColor, 3), listPoint[select]);
                }

                for (int i = 0; i < listPoint.Count; i++)
                {
                    listPoint[i] = null;
                }
                listPoint.Clear();
                listPoint = null;
            }
            catch { }

            mgph.Clip = tmp;
        }

        /// <summary>
        /// 绘图，画标记
        /// </summary>
        /// <param name="mgph"></param>
        protected void DrawMarker(Graphics mgph)
        {
            foreach (var it in MMarkerList)
            {
                int drawX = (int)(m_chartWidth / (m_maxPartX - m_minPartX) * (it.GetValByBase(MLines.MBase) - m_minPartX)) + m_chartLeft;
                mgph.DrawLine(Pens.Red, drawX, m_chartTop, drawX, m_chartBottom);
                mgph.DrawString(it.GetValByBase(MLines.MBase).ToString() + " " + it.MType, new System.Drawing.Font("微软雅黑", 9), System.Drawing.Brushes.Red, drawX - 15, m_chartTop - 15);
            }
        }

        /// <summary>
        /// 计算坐标的精度
        /// </summary>
        /// <param name="dis"></param>
        /// <returns></returns>
        protected string CalUnit(double dis)
        {
            if (Math.Abs(dis) < 0.0001)
            {
                return "f5";
            }
            else if (Math.Abs(dis) < 0.001)
            {
                return "f4";
            }
            else if (Math.Abs(dis) < 0.01)
            {
                return "f3";
            }
            else if (Math.Abs(dis) < 0.1)
            {
                return "f2";
            }
            else if (Math.Abs(dis) < 1)
            {
                return "f1";
            }
            else
            {
                return "f0";
            }
        }

        /// <summary>
        /// 1.33->1.4 1.3->1.3 -1.33->-1.3 -1.3->-1.3
        /// </summary>
        /// <param name="radix"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        protected double CalGreater(double radix, double val)
        {
            double temp = val / radix;
            if (Math.Abs(temp - (int)temp) >= DlyBase.DOUBLE)
            {
                if (val >= 0)
                {
                    return ((int)temp + 1) * radix;
                }
                else
                {
                    return (int)temp * radix;
                }
            }
            else
            {
                return val;
            }
        }

        /// <summary>
        /// 1.33->1.3 1.3->1.3
        /// </summary>
        /// <param name="radix"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        protected double CalLess(double radix, double val)
        {
            double temp = val / radix;
            if (Math.Abs(temp - (int)temp) >= 0)
            {
                if (val >= 0)
                {
                    return (int)temp * radix;
                }
                else
                {
                    return ((int)temp - 1) * radix;
                }
            }
            else
            {
                return val;
            }
        }

        /// <summary>
        /// 计算X轴
        /// </summary>
        /// <param name="len"></param>
        /// <param name="maxTemp"></param>
        /// <param name="minTemp"></param>
        /// <param name="radix"></param>
        /// <param name="mean"></param>
        /// <param name="drift"></param>
        /// <param name="count"></param>
        protected void CalXMaxMin(double len, out double maxTemp, out double minTemp, out double radix, out double mean, out double drift, out int count)
        {
            double dis = m_maxPartX - m_minPartX;

            if (dis <= 0.1)
            {
                radix = 0.01;
            }
            else if (dis <= 1)
            {
                radix = 0.1;
            }
            else
            {
                radix = Math.Pow(10, ((int)dis).ToString().Length - 1);

                if (Math.Abs(dis - radix) < DlyBase.DOUBLE)
                {
                    radix /= 10;
                }
            }

            minTemp = CalGreater(radix, m_minPartX);
            maxTemp = CalLess(radix, m_maxPartX);
            if (Math.Abs(maxTemp - minTemp) < 0.01)
            {
                maxTemp = minTemp + radix;
            }

            mean = len / (dis / radix);
            drift = (int)((minTemp - m_minPartX) / radix * mean);
            count = Convert.ToInt32((maxTemp - minTemp) / radix);
        }

        /// <summary>
        /// 计算Y轴
        /// </summary>
        /// <param name="len"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <param name="tempMax"></param>
        /// <param name="tempMin"></param>
        /// <param name="radix"></param>
        /// <param name="mean"></param>
        /// <param name="drift"></param>
        /// <param name="count"></param>
        protected void CalYMaxMin(double len, double max, double min, out double tempMax, out double tempMin, out double radix, out double mean, out double drift, out int count)
        {
            double dis = max - min;

            if (dis <= 0.001)
            {
                radix = 0.0001;
            }
            else if (dis <= 0.01)
            {
                radix = 0.001;
            }
            else if (dis <= 0.1)
            {
                radix = 0.01;
            }
            else if (dis <= 1)
            {
                radix = 0.1;
            }
            else
            {
                radix = Math.Pow(10, ((int)dis).ToString().Length - 1);

                if (Math.Abs(dis - radix) < DlyBase.DOUBLE)
                {
                    radix /= 10;
                }
            }

            tempMin = CalGreater(radix, min);
            tempMax = CalLess(radix, max);
            if (Math.Abs(tempMax - tempMin) < 0.0001)
            {
                tempMax = tempMin + radix;
            }

            mean = len / (dis / radix);
            drift = (int)((tempMin - min) / radix * mean);
            count = Convert.ToInt32((tempMax - tempMin) / radix);
        }

        /// <summary>
        /// 计算X放后的值
        /// </summary>
        /// <param name="scale"></param>
        protected void CalScaleX(ScaleData scale)
        {
            m_minPartX = scale.MXMin * (MLines.MMax - MLines.MMin) + MLines.MMin;
            m_maxPartX = scale.MXMax * (MLines.MMax - MLines.MMin) + MLines.MMin;

            //X轴最小刻度0.01
            if (Math.Abs(m_maxPartX - m_minPartX) < 0.02)
            {
                m_minPartX = CalLess(0.01, m_minPartX);
                m_maxPartX = CalGreater(0.01, m_maxPartX);
            }

            for (int j = 0; j < MListLines.Count; j++)
            {
                if (MListLines[j].MXList.Count > 0)
                {
                    m_minPartXIndex[j] = MListLines[j].MXList.Count - 1;
                    m_maxPartXIndex[j] = MListLines[j].MXList.Count - 1;
                }
                else
                {
                    m_minPartXIndex[j] = 0;
                    m_maxPartXIndex[j] = -1;
                }

                for (int i = 0; i < MListLines[j].MXList.Count; i++)
                {
                    if (MListLines[j].MXList[i] >= m_minPartX)
                    {
                        m_minPartXIndex[j] = i;   //局部图起点序号
                        break;
                    }
                }
                for (int i = m_minPartXIndex[j]; i < MListLines[j].MXList.Count; i++)
                {
                    if (MListLines[j].MXList[i] >= m_maxPartX)
                    {
                        m_maxPartXIndex[j] = i;   //局部图终点序号
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 计算Y缩放后的值
        /// </summary>
        /// <param name="line"></param>
        /// <param name="scale"></param>
        protected void CalScaleY(Curve line, ScaleData scale)
        {
            line.MMinPart = scale.MYMin * (line.MMax - line.MMin) + line.MMin - line.MMove;
            line.MMaxPart = scale.MYMax * (line.MMax - line.MMin) + line.MMin - line.MMove;

            //Y轴最小刻度0.0001
            if (Math.Abs(line.MMaxPart - line.MMinPart) < 0.0002)
            {
                line.MMinPart = CalLess(0.01, line.MMinPart);
                line.MMaxPart = CalGreater(0.01, line.MMaxPart);
            }
        }

        /// <summary>
        /// 计算XY放后的值
        /// </summary>
        /// <param name="scale"></param>
        protected void CalSacleXY(ScaleData scale)
        {
            CalScaleX(scale);
            for (int i = 0; i < m_listContrast.Count; i++)
            {
                if (m_listContrast[i])
                {
                    foreach (var it in MListLines)
                    {
                        CalScaleY(it.MItemList[i], scale);
                    }
                }
            }
        }

        /// <summary>
        /// 数据点转化图标点
        /// </summary>
        /// <param name="chartWidth"></param>
        /// <param name="chartHeight"></param>
        /// <param name="chartLeft"></param>
        /// <param name="chartBottom"></param>
        /// <returns></returns>
        protected List<Point[]> CalLinesPoint(int chartWidth, int chartHeight, int chartLeft, int chartBottom)
        {
            List<Point[]> pointList = new List<Point[]>();

            double xaver = chartWidth / (m_maxPartX - m_minPartX);

            for (int i = 0; i < m_listContrast.Count; i++)
            {
                if (m_listContrast[i])
                {
                    for (int j = 0; j < MListLines.Count; j++)
                    {
                        Curve line = MListLines[j].MItemList[i];
                        double yaver = chartHeight / (line.MMaxPart - line.MMinPart);

                        List<Point> list = new List<Point>();

                        int currX = -999999;
                        int currY = -999999;
                        for (int col = m_minPartXIndex[j]; col <= m_maxPartXIndex[j]; col++)
                        {
                            int tempX = (int)(xaver * (MListLines[j].MXList[col] - m_minPartX)) + chartLeft;
                            int tempY = chartBottom - (int)(yaver * (line.GetData(col) - line.MMinPart));
                            if (tempX != currX || tempY == currY)
                            {
                                list.Add(new Point(tempX, tempY));
                                currX = tempX;
                                currY = tempY;
                            }
                        }

                        if (1 < list.Count)
                        {
                            pointList.Add(list.ToArray());
                        }
                        else
                        {
                            pointList.Add(null);
                        }
                    }

                }
            }

            return pointList;
        }
    }
}
