using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HBBio.Chromatogram
{
    /**
     * ClassName: ChromatogramManager
     * Description: 色谱图管理类
     * Version: 1.0
     * Create:  2018/05/16
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class ChromatogramManager
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

        //信号集合对象
        public CurveSet MLines { get; }
        //背景信号集合对象
        public CurveSet MBGLines { get; }
        private bool m_BGEnabled = false;
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
        public int m_minPartXIndex = 0;                             //局部图起始点的序号
        public int m_maxPartXIndex = -1;                            //局部图终结点的序号
        public List<int> m_Index = new List<int>();
        public int m_minPartBGXIndex = 0;                           //局部图起始点的序号
        public int m_maxPartBGXIndex = -1;                          //局部图终结点的序号
        public double m_minPartX = 0;                               //局部图最小值
        public double m_maxPartX = 1;                               //局部图最大值 
        public double m_disX = 0;
        public double m_disLastX = 0;
        public double m_disY = 0;
        public double m_disLastY = 0;
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
                string[] arrName = value.Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var it in arrName)
                {
                    string[] arrVal = it.Split(new char[] { ';' }, StringSplitOptions.None);
                    MMarkerList.Add(new MarkerInfo(arrVal[0], Convert.ToDouble(arrVal[1]), Convert.ToDouble(arrVal[2]), Convert.ToDouble(arrVal[3])));
                }
            }
        }
        //收集列表
        public List<MarkerInfo> MCollListM { get; }
        public List<MarkerInfo> MCollListA { get; }
        //阀列表
        public List<MarkerInfo> MValveList { get; }
        //阶段列表
        public List<MarkerInfo> MPhaseList { get; }
        //各种标记的颜色
        public BackgroundInfo MBackgroundInfo { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public ChromatogramManager()
        {
            m_fontText = new Font("微软雅黑", 12);
            m_penGrid = new Pen(Color.Gray);
            m_penGrid.DashPattern = new float[] { 1, 3 };

            MLines = new CurveSet();
            MBGLines = new CurveSet();
            MScale = new ScaleData();
            MMarkerList = new List<MarkerInfo>();
            MCollListM = new List<MarkerInfo>();
            MCollListA = new List<MarkerInfo>();
            MValveList = new List<MarkerInfo>();
            MPhaseList = new List<MarkerInfo>();

            MBackgroundInfo = new BackgroundInfo();
        }

        /// <summary>
        /// 数据清零
        /// </summary>
        public void Clear()
        {
            MLines.Clear();
            MScale.Clear();
            MMarkerList.Clear();
            MCollListM.Clear();
            MCollListA.Clear();
            MValveList.Clear();
            MPhaseList.Clear();

            if (m_BGEnabled)
            {
                SetLineAndBGLine(true);
            }
        }

        /// <summary>
        /// 背景数据清零
        /// </summary>
        public void ClearBG()
        {
            m_BGEnabled = false;
            MBGLines.Clear();
            SetLineAndBGLine(false);
        }

        /// <summary>
        /// 初始化曲线信号
        /// </summary>
        /// <param name="list"></param>
        public void InitLineList(List<Curve> list, List<Curve> listBG, AxisScale axisScale)
        {
            if (null != axisScale)
            {
                MLines.MAxisScale = axisScale.MAxisScale;
                switch (MLines.MAxisScale)
                {
                    case EnumAxisScale.Fixed:
                        MLines.MMinFix = axisScale.MMin;
                        MLines.MMaxFix = axisScale.MMax;
                        break;
                }
            }

            MLines.InitItemList(list);
            MBGLines.InitItemList(listBG);
        }

        /// <summary>
        /// 计算当前显示的曲线数量
        /// </summary>
        public void CalItemListShow()
        {
            MItemShowCount = 0;
            foreach (var it in MLines.MItemList)
            {
                if (it.MShow)
                {
                    MItemShowCount++;
                }
            }

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
        /// 新增数据
        /// </summary>
        /// <param name="info"></param>
        public void AddLineItemData(double t, double v, double cv, List<double> dataList)
        {
            MLines.AddLineItemData(t, v, cv, dataList);
        }

        /// <summary>
        /// 还原数据
        /// </summary>
        /// <param name="info"></param>
        public void RestoreLineItemData()
        {
            MLines.RestoreLineItemData();
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="info"></param>
        public void RestoreBGLineItemData()
        {
            m_BGEnabled = true;
            MBGLines.RestoreLineItemData();
        }
        public void ClearBGLineItemData()
        {
            MBGLines.ClearLineItemData();
        }
        public void SetLineAndBGLine(bool flag)
        {
            if (flag)
            {
                MLines.MAxisScale = EnumAxisScale.Fixed;
                MLines.MMaxFix = MBGLines.MMax;
                MLines.MMinFix = MBGLines.MMin;
                MLines.MMaxAuto = MBGLines.MMax;
                MLines.MMinAuto = MBGLines.MMin;

                int index = 0;
                foreach (var it in MLines.MItemList)
                {
                    it.MAxisScale = EnumAxisScale.Fixed;
                    it.MMaxFix = MBGLines.MItemList[index].MMax;
                    it.MMinFix = MBGLines.MItemList[index].MMin;
                    it.MMaxAuto = MBGLines.MItemList[index].MMax;
                    it.MMinAuto = MBGLines.MItemList[index].MMin;
                    index++;
                }
            }
            else
            {
                MLines.MAxisScale = EnumAxisScale.Auto;
                foreach (var it in MLines.MItemList)
                {
                    it.MAxisScale = EnumAxisScale.Auto;
                }
            }
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

            m_disX = 0;
            m_disLastX = 0;
            m_disY = 0;
            m_disLastY = 0;
        }

        /// <summary>
        /// 清空局部图
        /// </summary>
        public void ClearScale()
        {
            MScale.Clear();

            m_disX = 0;
            m_disLastX = 0;
            m_disY = 0;
            m_disLastY = 0;
        }

        public void AddScale()
        {
            MScale.AddMinMax((m_minPartX - MLines.MMin) / (MLines.MMax - MLines.MMin),
                                    (m_maxPartX - MLines.MMin) / (MLines.MMax - MLines.MMin),
                                    (MLines.MSelectItem.MMinPart - MLines.MSelectItem.MMin) / (MLines.MSelectItem.MMax - MLines.MSelectItem.MMin),
                                    (MLines.MSelectItem.MMaxPart - MLines.MSelectItem.MMin) / (MLines.MSelectItem.MMax - MLines.MSelectItem.MMin));

            m_disX = 0;
            m_disLastX = 0;
            m_disY = 0;
            m_disLastY = 0;
        }

        public void DragMove(double startX, double endX, double startY, double endY)
        {
            m_disX = m_disLastX + (startX - endX) / m_chartWidth * (m_maxPartX - m_minPartX);
            if (null != MLines.MSelectItem)
            {
                m_disY = m_disLastY + (endY - startY) / m_chartHeight * (MLines.MSelectItem.MMaxPart - MLines.MSelectItem.MMinPart);
            }
        }
        public void DragUp(double startX, double endX, double startY, double endY)
        {
            m_disX = m_disLastX + (startX - endX) / m_chartWidth * (m_maxPartX - m_minPartX);
            m_disLastX = m_disX;

            if (null != MLines.MSelectItem)
            {
                m_disY = m_disLastY + (endY - startY) / m_chartHeight * (MLines.MSelectItem.MMaxPart - MLines.MSelectItem.MMinPart);
            }
            m_disLastY = m_disY;
        }

        /// <summary>
        /// 新增标记
        /// </summary>
        /// <param name="item"></param>
        public void AddMarker(MarkerInfo item)
        {
            if (0 == MLines.MXList.Count)
            {
                if (-1 == item.MValX || 0 == item.MValX)
                {
                    item.MT = 0;
                    item.MV = 0;
                    item.MCV = 0;
                }
                else
                {
                    return;
                }
            }
            else
            {
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
        /// 新增收集
        /// </summary>
        /// <param name="item"></param>
        public void AddCollM(MarkerInfo item)
        {
            if (0 == MLines.MXList.Count)
            {
                if (-1 == item.MValX || 0 == item.MValX)
                {
                    item.MT = 0;
                    item.MV = 0;
                    item.MCV = 0;
                }
                else
                {
                    return;
                }
            }
            else
            {
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
            }

            MCollListM.Add(item);
        }
        public void AddCollA(MarkerInfo item)
        {
            if (0 == MLines.MXList.Count)
            {
                if (-1 == item.MValX || 0 == item.MValX)
                {
                    item.MT = 0;
                    item.MV = 0;
                    item.MCV = 0;
                }
                else
                {
                    return;
                }
            }
            else
            {
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
            }

            MCollListA.Add(item);
        }

        /// <summary>
        /// 新增切阀
        /// </summary>
        /// <param name="item"></param>
        public void AddValve(MarkerInfo item)
        {
            if (0 == MLines.MXList.Count)
            {
                if (-1 == item.MValX || 0 == item.MValX)
                {
                    item.MT = 0;
                    item.MV = 0;
                    item.MCV = 0;
                }
                else
                {
                    return;
                }
            }
            else
            {
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
            }

            MValveList.Add(item);
        }

        /// <summary>
        /// 新增方法行信息
        /// </summary>
        /// <param name="item"></param>
        public void AddPhase(MarkerInfo item)
        {
            if (0 == MLines.MXList.Count)
            {
                if (-1 == item.MValX || 0 == item.MValX)
                {
                    item.MT = 0;
                    item.MV = 0;
                    item.MCV = 0;
                }
                else
                {
                    return;
                }
            }
            else
            {
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
            }

            MPhaseList.Add(item);
        }

        public int GetIndex(double coordX)
        {
            return MLines.GetIndex(GetValueX(coordX));
        }

        /// <summary>
        /// 返回单标尺信息
        /// </summary>
        /// <param name="coordX"></param>
        /// <returns></returns>
        public string GetRulerOddO(double coordX)
        {
            if (-1 == MLines.MSelectIndex)
            {
                return "";
            }

            double valueX = GetValueX(coordX);
            double valueY = GetValueY(valueX);

            StringBuilderSplit sb = new StringBuilderSplit("\n");
            sb.Append("X1 : " + valueX + " " + MLines.MUnit);
            sb.Append("Y1 : " + valueY.ToString("f2") + " " + MLines.MSelectItem.MUnit);
            return sb.ToString();
        }

        /// <summary>
        /// 返回单标尺信息
        /// </summary>
        /// <param name="coordX"></param>
        /// <returns></returns>
        public string GetRulerOddM(double coordX)
        {
            if (-1 == MLines.MSelectIndex)
            {
                return "";
            }

            double valueX = GetValueX(coordX);
            double valueY = GetValueY(valueX);

            StringBuilderSplit sb = new StringBuilderSplit("\n");
            sb.Append("X1 : " + valueX + " " + MLines.MUnit);
            sb.Append("Y1 : " + valueY.ToString("f2") + " " + MLines.MSelectItem.MUnit);
            int index = MLines.GetIndex(valueX);
            if (-1 == index)
            {
                return "";
            }

            foreach (var it in MLines.MItemList)
            {
                sb.Append(it.MName + " : " + it.GetData(index).ToString("f2") + " " + it.MUnit);
            }
            return sb.ToString();
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
            sb.Append("X1 : " + valueX1);
            sb.Append("Y1 : " + valueY1.ToString("f2"));
            sb.Append("X2 : " + valueX2);
            sb.Append("Y2 : " + valueY2.ToString("f2"));
            sb.Append("Y_Max : " + maxVal.ToString("f2"));
            sb.Append("Y_Min : " + minVal.ToString("f2"));
            sb.Append("Y_Aver : " + (totalVal / (indexE - indexB + 1)).ToString("f2"));
            sb.Append("△X : " + (valueX2 - valueX1).ToString("f2"));
            if (null != MLines.MListPeak)
            {
                sb.Append("Drift: " + Evaluation.PeakMath.CalcDrift(MLines.MListPeak[MLines.MSelectIndex].m_x, MLines.MListPeak[MLines.MSelectIndex].m_y, valueX1, valueX2));
                sb.Append("ASTM : " + Evaluation.PeakMath.CalcAstmNoise(MLines.MListPeak[MLines.MSelectIndex].m_x, MLines.MListPeak[MLines.MSelectIndex].m_y, valueX1, valueX2));
                sb.Append("6_Sigma : " + Evaluation.PeakMath.CalcSixSigmaNoise(MLines.MListPeak[MLines.MSelectIndex].m_x, MLines.MListPeak[MLines.MSelectIndex].m_y, valueX1, valueX2));
            }
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
                    case EnumBase.T: MLines.MBase = EnumBase.V; break;
                    case EnumBase.V: MLines.MBase = EnumBase.CV; break;
                    case EnumBase.CV: MLines.MBase = EnumBase.T; break;
                }

                MBGLines.MBase = MLines.MBase;

                if (0 < MLines.MXList.Count)
                {
                    switch (MLines.MAxisScale)
                    {
                        case EnumAxisScale.Auto:
                            MLines.MMaxAuto = ValueTrans.GetTimes(MLines.MXList.Last());
                            break;
                    }
                }

                if (null != MLines.MListPeak)
                {
                    MLines.CalPeak(MLines.MListPeak, -1);
                }

                return true;
            }
            else
            {
                return false;
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
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            if (MBackgroundInfo.MPhaseVisible)
            {
                DrawPhase(graphics);
            }

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

            Region temp = graphics.Clip;
            graphics.Clip = new Region(new Rectangle(m_chartLeft, m_chartTop, m_chartWidth, m_chartHeight));

            List<PointF[]> listPoint = CalLinesPoint(m_chartWidth, m_chartHeight, m_chartLeft, m_chartBottom);
            DrawLines(graphics, listPoint);

            if (m_BGEnabled)
            {
                CalSacleBGXY(MScale);
                List<PointF[]> listBGPoint = CalBGLinesPoint(m_chartWidth, m_chartHeight, m_chartLeft, m_chartBottom);
                DrawLines(graphics, listBGPoint, true);
            }

            graphics.Clip = temp;

            if (MBackgroundInfo.MMarkerVisible)
            {
                DrawMarker(graphics);
            }

            if (MBackgroundInfo.MCollMVisible)
            {
                DrawCollM(graphics);
            }

            if (MBackgroundInfo.MCollAVisible)
            {
                DrawCollA(graphics);
            }

            if (MBackgroundInfo.MValveVisible)
            {
                DrawValve(graphics);
            }

            DrawPeak(graphics, listPoint);

            for (int i = 0; i < listPoint.Count; i++)
            {
                listPoint[i] = null;
            }
            listPoint.Clear();
            listPoint = null;

            graphics.Flush();
            graphics.Dispose();
            graphics = null;

            backBitmap.Dispose();
            backBitmap = null;

            bitmap.AddDirtyRect(new System.Windows.Int32Rect(0, 0, m_boardWidth, m_boardHeight));
            bitmap.Unlock();

            return bitmap;
        }

        /// <summary>
        /// 画图
        /// </summary>
        public System.Windows.Media.Imaging.WriteableBitmap DrawCurveNames()
        {
            int rowCount = MItemShowCount / (m_boardWidth / 100) + 1;
            int height = 30 * rowCount;
            System.Windows.Media.Imaging.WriteableBitmap bitmap = new System.Windows.Media.Imaging.WriteableBitmap(m_boardWidth, height, m_dpiX, m_dpiY, System.Windows.Media.PixelFormats.Bgr24, null);

            bitmap.Lock();

            System.Drawing.Bitmap backBitmap = new System.Drawing.Bitmap(m_boardWidth, height, bitmap.BackBufferStride, System.Drawing.Imaging.PixelFormat.Format24bppRgb, bitmap.BackBuffer);
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(backBitmap);
            graphics.Clear(System.Drawing.Color.White);//整张画布置为白色                                    
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            int index = 0;
            foreach (var line in MLines.MItemList)
            {
                if (line.MShow)
                {
                    graphics.DrawString(line.MName, m_fontText, new SolidBrush(line.MColor), new PointF((index % 5) * 100, 30 * (index / 5)));
                    index += ((4 == index % 5) ? 1 : (1 + line.MName.Length / 11));
                }
            }

            graphics.Flush();
            graphics.Dispose();
            graphics = null;

            backBitmap.Dispose();
            backBitmap = null;

            bitmap.AddDirtyRect(new System.Windows.Int32Rect(0, 0, m_boardWidth, height));
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

        protected float GetCoordY(double y)
        {
            return m_chartBottom - (int)(m_chartHeight / (MLines.MSelectItem.MMaxPart - MLines.MSelectItem.MMinPart) * (y - MLines.MSelectItem.MMinPart));
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
            return MLines.MSelectItem.GetData(MLines.GetIndex(valueX));
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
        protected void DrawLines(Graphics mgph, List<PointF[]> listPoint, bool dashPattern = false)
        {
            try
            {
                for (int i = 0; i < MLines.MItemList.Count; i++)
                {
                    using (Pen pen = new Pen(MLines.MItemList[i].MColor))
                    {
                        if (dashPattern)
                        {
                            pen.DashPattern = new float[] { 5, 5 };
                        }
                        if (MLines.MSelectIndex != i && null != listPoint[i])
                        {
                            mgph.DrawLines(pen, listPoint[i]);
                        }
                    }
                }

                //选中加粗,最后画
                if (null != MLines.MSelectItem)
                {
                    using (Pen pen = new Pen(MLines.MSelectItem.MColor, 3))
                    {
                        if (dashPattern)
                        {
                            pen.DashPattern = new float[] { 5, 5 };
                        }
                        if (-1 != MLines.MSelectIndex && null != listPoint[MLines.MSelectIndex])
                        {
                            mgph.DrawLines(pen, listPoint[MLines.MSelectIndex]);
                        }
                    }
                }
            }
            catch { }
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
                mgph.DrawLine(MBackgroundInfo.MMarkerPen, drawX, m_chartTop, drawX, m_chartBottom);
                mgph.DrawString(it.GetValByBase(MLines.MBase).ToString() + " " + it.MType, new System.Drawing.Font("微软雅黑", 9), MBackgroundInfo.MMarkerBrush, drawX - 15, m_chartTop - 15);
            }
        }

        /// <summary>
        /// 绘图，画收集
        /// </summary>
        /// <param name="mgph"></param>
        protected void DrawCollM(Graphics mgph)
        {
            foreach (var it in MCollListM)
            {
                int drawX = (int)(m_chartWidth / (m_maxPartX - m_minPartX) * (it.GetValByBase(MLines.MBase) - m_minPartX)) + m_chartLeft;
                mgph.DrawLine(MBackgroundInfo.MCollPenM, drawX, m_chartBottom - 30, drawX, m_chartBottom);
                if (MBackgroundInfo.MCollMDirection)
                {
                    mgph.DrawString(it.GetValByBase(MLines.MBase).ToString(), new System.Drawing.Font("微软雅黑", 9), MBackgroundInfo.MCollBrushM, drawX - 15, m_chartBottom - 60);
                    mgph.DrawString(it.MType, new System.Drawing.Font("微软雅黑", 9), MBackgroundInfo.MCollBrushM, drawX - 15, m_chartBottom - 45);
                }
                else
                {
                    mgph.DrawString(it.GetValByBase(MLines.MBase).ToString(), new System.Drawing.Font("微软雅黑", 9), MBackgroundInfo.MCollBrushM, drawX - 30, m_chartBottom - 45, new StringFormat(StringFormatFlags.DirectionVertical));
                    mgph.DrawString(it.MType, new System.Drawing.Font("微软雅黑", 9), MBackgroundInfo.MCollBrushM, drawX - 15, m_chartBottom - 45, new StringFormat(StringFormatFlags.DirectionVertical));
                }
            }
        }

        /// <summary>
        /// 绘图，画收集
        /// </summary>
        /// <param name="mgph"></param>
        protected void DrawCollA(Graphics mgph)
        {
            foreach (var it in MCollListA)
            {
                int drawX = (int)(m_chartWidth / (m_maxPartX - m_minPartX) * (it.GetValByBase(MLines.MBase) - m_minPartX)) + m_chartLeft;
                mgph.DrawLine(MBackgroundInfo.MCollPenA, drawX, m_chartBottom - 30, drawX, m_chartBottom);
                if (MBackgroundInfo.MCollADirection)
                {
                    mgph.DrawString(it.GetValByBase(MLines.MBase).ToString(), new System.Drawing.Font("微软雅黑", 9), MBackgroundInfo.MCollBrushA, drawX - 15, m_chartBottom - 60);
                    mgph.DrawString(it.MType, new System.Drawing.Font("微软雅黑", 9), MBackgroundInfo.MCollBrushA, drawX - 15, m_chartBottom - 45);
                }
                else
                {
                    mgph.DrawString(it.GetValByBase(MLines.MBase).ToString(), new System.Drawing.Font("微软雅黑", 9), MBackgroundInfo.MCollBrushA, drawX - 30, m_chartBottom - 45, new StringFormat(StringFormatFlags.DirectionVertical));
                    mgph.DrawString(it.MType, new System.Drawing.Font("微软雅黑", 9), MBackgroundInfo.MCollBrushA, drawX - 15, m_chartBottom - 45, new StringFormat(StringFormatFlags.DirectionVertical));
                }
            }
        }

        /// <summary>
        /// 绘图，画切阀
        /// </summary>
        /// <param name="mgph"></param>
        protected void DrawValve(Graphics mgph)
        {
            foreach (var it in MValveList)
            {
                int drawX = (int)(m_chartWidth / (m_maxPartX - m_minPartX) * (it.GetValByBase(MLines.MBase) - m_minPartX)) + m_chartLeft;
                mgph.DrawLine(MBackgroundInfo.MValvePen, drawX, m_chartBottom - 50, drawX, m_chartBottom);
                if (MBackgroundInfo.MValveDirection)
                {
                    mgph.DrawString(it.GetValByBase(MLines.MBase).ToString() + " " + it.MType, new System.Drawing.Font("微软雅黑", 9), MBackgroundInfo.MValveBrush, drawX - 15, m_chartBottom - 65);
                }
                else
                {
                    mgph.DrawString(it.GetValByBase(MLines.MBase).ToString() + " " + it.MType, new System.Drawing.Font("微软雅黑", 9), MBackgroundInfo.MValveBrush, drawX - 15, m_chartBottom - 65, new StringFormat(StringFormatFlags.DirectionVertical));
                }
            }
        }

        /// <summary>
        /// 绘图，画方法行
        /// </summary>
        /// <param name="mgph"></param>
        protected void DrawPhase(Graphics mgph)
        {
            if (0 < MPhaseList.Count)
            {
                int drawX1 = 0;
                int drawX2 = 0;
                for (int i = 0; i < MPhaseList.Count - 1; i++)
                {
                    drawX1 = (int)(m_chartWidth / (m_maxPartX - m_minPartX) * (MPhaseList[i].GetValByBase(MLines.MBase) - m_minPartX)) + m_chartLeft;
                    drawX2 = (int)(m_chartWidth / (m_maxPartX - m_minPartX) * (MPhaseList[i + 1].GetValByBase(MLines.MBase) - m_minPartX)) + m_chartLeft;
                    if (0 == i % 2)
                    {
                        mgph.FillRectangle(MBackgroundInfo.MPhaseBrush, drawX1, m_chartTop, drawX2 - drawX1, m_chartHeight);
                    }
                    if (MBackgroundInfo.MPhaseDirection)
                    {
                        mgph.DrawString(MPhaseList[i].GetValByBase(MLines.MBase).ToString() + " " + MPhaseList[i].MType, new System.Drawing.Font("微软雅黑", 9), MBackgroundInfo.MPhaseBrush, drawX1 + 15, m_chartTop - 15);
                    }
                    else
                    {
                        mgph.DrawString(MPhaseList[i].GetValByBase(MLines.MBase).ToString() + " " + MPhaseList[i].MType, new System.Drawing.Font("微软雅黑", 9), MBackgroundInfo.MPhaseBrush, drawX1 + 15, m_chartTop - 15, new StringFormat(StringFormatFlags.DirectionVertical));
                    }
                }
                drawX1 = (int)(m_chartWidth / (m_maxPartX - m_minPartX) * (MPhaseList.Last().GetValByBase(MLines.MBase) - m_minPartX)) + m_chartLeft;
                drawX2 = (int)(m_chartWidth / (m_maxPartX - m_minPartX) * (MLines.MXList.Last() - m_minPartX)) + m_chartLeft;
                if (0 == (MPhaseList.Count - 1) % 2)
                {
                    mgph.FillRectangle(MBackgroundInfo.MPhaseBrush, drawX1, m_chartTop, drawX2 - drawX1, m_chartHeight);
                }
                if (MBackgroundInfo.MPhaseDirection)
                {
                    mgph.DrawString(MPhaseList.Last().GetValByBase(MLines.MBase).ToString() + " " + MPhaseList.Last().MType, new System.Drawing.Font("微软雅黑", 9), MBackgroundInfo.MPhaseBrush, drawX1 + 15, m_chartTop - 15);
                }
                else
                {
                    mgph.DrawString(MPhaseList.Last().GetValByBase(MLines.MBase).ToString() + " " + MPhaseList.Last().MType, new System.Drawing.Font("微软雅黑", 9), MBackgroundInfo.MPhaseBrush, drawX1 + 15, m_chartTop - 15, new StringFormat(StringFormatFlags.DirectionVertical));
                }
            }
        }

        /// <summary>
        /// 绘图，画标记
        /// </summary>
        /// <param name="mgph"></param>
        protected void DrawPeak(Graphics mgph, List<PointF[]> listPoint)
        {
            if (-1 == MLines.MSelectIndex || 0 == listPoint.Count || null == listPoint[MLines.MSelectIndex])
            {
                return;
            }

            PointF[] arrPixel = listPoint[MLines.MSelectIndex];

            if (null != MLines.MSelectPeakValue && 0 < MLines.MSelectPeakValue.m_list.Count)
            {
                //画总基线
                Pen dashPen = new Pen(MLines.MSelectItem.MColor, 2);
                dashPen.DashPattern = new float[] { 3, 3 };
                foreach (var it in MLines.MSelectPeakValue.m_list)
                {
                    if (it.StartPoint >= m_minPartXIndex && it.StartPoint <= m_maxPartXIndex
                        && it.EndPoint >= m_minPartXIndex && it.EndPoint <= m_maxPartXIndex)
                    {
                        switch (it.MPeakType)
                        {
                            case Evaluation.PeakType.VerticalSeparation:
                                mgph.DrawLine(dashPen, new PointF(arrPixel[m_Index[it.StartPoint - m_minPartXIndex]].X, GetCoordY(it.StartBaseY)), new PointF(arrPixel[m_Index[it.EndPoint - m_minPartXIndex]].X, GetCoordY(it.EndBaseY)));
                                break;
                            default:
                                mgph.DrawLine(dashPen, arrPixel[m_Index[it.StartPoint - m_minPartXIndex]], arrPixel[m_Index[it.EndPoint - m_minPartXIndex]]);
                                break;
                        }

                        if (-1 != MLines.MSelectPeakIndex && MLines.MSelectPeakIndex < MLines.MSelectPeakValue.m_list.Count && it.Equals(MLines.MSelectPeakValue.m_list[MLines.MSelectPeakIndex]))
                        {
                            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                            PointF[] points = new PointF[it.EndPoint - it.StartPoint + 1];
                            for (int i = 0; i < points.Length; i++)
                            {
                                points[i] = arrPixel[m_Index[it.StartPoint + i - m_minPartXIndex]];
                            }
                            path.AddLines(points);
                            switch (it.MPeakType)
                            {
                                case Evaluation.PeakType.VerticalSeparation:
                                    path.AddLines(new PointF[] { new PointF(arrPixel[m_Index[it.EndPoint - m_minPartXIndex]].X, GetCoordY(it.EndBaseY)), new PointF(arrPixel[m_Index[it.StartPoint - m_minPartXIndex]].X, GetCoordY(it.StartBaseY))});
                                    break;
                            }
                            mgph.FillPath(new SolidBrush(MLines.MSelectItem.MColor), path);
                        }
                    }  
                }

                //画线
                Pen ellipsePen = new Pen(MLines.MSelectItem.MColor, 5);
                foreach (var it in MLines.MSelectPeakValue.m_list)
                {
                    //峰起始线
                    if (it.StartPoint >= m_minPartXIndex && it.StartPoint <= m_maxPartXIndex)
                    {
                        mgph.DrawEllipse(ellipsePen, arrPixel[m_Index[it.StartPoint - m_minPartXIndex]].X, arrPixel[m_Index[it.StartPoint - m_minPartXIndex]].Y - 2.5f, 5, 5);
                        mgph.DrawLine(dashPen, new PointF(arrPixel[m_Index[it.StartPoint - m_minPartXIndex]].X, GetCoordY(it.StartBaseY)), arrPixel[m_Index[it.StartPoint - m_minPartXIndex]]);
                    }

                    //峰结束线
                    if (it.EndPoint >= m_minPartXIndex && it.EndPoint <= m_maxPartXIndex)
                    {
                        mgph.DrawEllipse(ellipsePen, arrPixel[m_Index[it.EndPoint - m_minPartXIndex]].X, arrPixel[m_Index[it.EndPoint - m_minPartXIndex]].Y - 2.5f, 5, 5);
                        mgph.DrawLine(dashPen, new PointF(arrPixel[m_Index[it.EndPoint - m_minPartXIndex]].X, GetCoordY(it.EndBaseY)), arrPixel[m_Index[it.EndPoint - m_minPartXIndex]]);
                    }
                    
                    //峰顶
                    if (it.PeekPoint >= m_minPartXIndex && it.PeekPoint <= m_maxPartXIndex)
                    {
                        mgph.DrawPolygon(ellipsePen, 
                            new PointF[3] { new PointF(arrPixel[m_Index[it.PeekPoint - m_minPartXIndex]].X - 2.5f, arrPixel[m_Index[it.PeekPoint - m_minPartXIndex]].Y - 5)
                            , new PointF(arrPixel[m_Index[it.PeekPoint - m_minPartXIndex]].X + 2.5f, arrPixel[m_Index[it.PeekPoint - m_minPartXIndex]].Y - 5)
                            , new PointF(arrPixel[m_Index[it.PeekPoint - m_minPartXIndex]].X,arrPixel[m_Index[it.PeekPoint - m_minPartXIndex]].Y - 10) });
                        //mgph.DrawLine(dashPen, arrPixel[m_Index[it.PeekPoint - m_minPartXIndex]].X, arrPixel[m_Index[it.PeekPoint - m_minPartXIndex]].Y - 10, arrPixel[m_Index[it.PeekPoint - m_minPartXIndex]].X, arrPixel[m_Index[it.PeekPoint - m_minPartXIndex]].Y + 10);
                    }
                }
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

            if (0 != m_disX)
            {
                if (m_maxPartX + m_disX > MLines.MMax)
                {
                    m_maxPartX += (MLines.MMax - m_maxPartX);
                    m_minPartX += (MLines.MMax - m_maxPartX);
                }
                else if (m_minPartX + m_disX < MLines.MMin)
                {
                    m_maxPartX += (MLines.MMin - m_minPartX);
                    m_minPartX += (MLines.MMin - m_minPartX);
                }
                else
                {
                    m_maxPartX += m_disX;
                    m_minPartX += m_disX;
                }
            }

            //防止俩个值一样
            if (m_maxPartX == m_minPartX)
            {
                m_maxPartX = m_minPartX + 1;
            }

            if (MLines.MXList.Count > 0)
            {
                m_minPartXIndex = 0;
                m_maxPartXIndex = MLines.MXList.Count - 1;
            }
            else
            {
                m_minPartXIndex = 0;
                m_maxPartXIndex = -1;
            }

            if (1 < scale.MCount)
            {
                //X轴最小刻度0.01
                if (Math.Abs(m_maxPartX - m_minPartX) < 0.02)
                {
                    m_minPartX = CalLess(0.01, m_minPartX);
                    m_maxPartX = CalGreater(0.01, m_maxPartX);
                }

                for (int i = 0; i < MLines.MXList.Count; i++)
                {
                    if (MLines.MXList[i] >= m_minPartX)
                    {
                        m_minPartXIndex = i;   //局部图起点序号
                        if (m_minPartXIndex > 0)
                        {
                            m_minPartXIndex -= 1;
                        }
                        break;
                    }
                }
                for (int i = m_minPartXIndex; i < MLines.MXList.Count; i++)
                {
                    if (MLines.MXList[i] >= m_maxPartX)
                    {
                        m_maxPartXIndex = i;   //局部图终点序号
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 计算X放后的值
        /// </summary>
        /// <param name="scale"></param>
        protected void CalScaleBGX(ScaleData scale)
        {
            if (MBGLines.MXList.Count > 0)
            {
                m_minPartBGXIndex = 0;
                m_maxPartBGXIndex = MBGLines.MXList.Count - 1;
            }
            else
            {
                m_minPartBGXIndex = 0;
                m_maxPartBGXIndex = -1;
            }

            if (1 < scale.MCount)
            {
                for (int i = 0; i < MBGLines.MXList.Count; i++)
                {
                    if (MBGLines.MXList[i] >= m_minPartX)
                    {
                        m_minPartBGXIndex = i;   //局部图起点序号
                        break;
                    }
                }
                for (int i = m_minPartBGXIndex; i < MBGLines.MXList.Count; i++)
                {
                    if (MBGLines.MXList[i] >= m_maxPartX)
                    {
                        m_maxPartBGXIndex = i;   //局部图终点序号
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
            line.MMinPart = scale.MYMin * (line.MMax - line.MMin) + line.MMin;
            line.MMaxPart = scale.MYMax * (line.MMax - line.MMin) + line.MMin;

            if (0 != m_disY)
            {
                if (line.MMaxPart + m_disY > MLines.MMax)
                {
                    line.MMaxPart += (MLines.MMax - line.MMaxPart);
                    line.MMinPart += (MLines.MMax - line.MMaxPart);
                }
                else if (line.MMinPart + m_disY < MLines.MMin)
                {
                    line.MMaxPart += (MLines.MMin - line.MMinPart);
                    line.MMinPart += (MLines.MMin - line.MMinPart);
                }
                else
                {
                    line.MMaxPart += m_disY;
                    line.MMinPart += m_disY;
                }
            }

            //Y轴最小刻度0.0001
            if (Math.Abs(line.MMaxPart - line.MMinPart) < 0.0002)
            {
                line.MMinPart = CalLess(0.01, line.MMinPart);
                //line.MMaxPart = CalGreater(0.01, line.MMaxPart);
                line.MMaxPart = line.MMinPart + 0.01;
            }
        }

        /// <summary>
        /// 计算XY放后的值
        /// </summary>
        /// <param name="scale"></param>
        protected void CalSacleXY(ScaleData scale)
        {
            CalScaleX(scale);
            foreach (var line in MLines.MItemList)
            {
                if (line.MShow)
                {
                    CalScaleY(line, scale);
                }
            }
        }

        /// <summary>
        /// 计算XY放后的值
        /// </summary>
        /// <param name="scale"></param>
        protected void CalSacleBGXY(ScaleData scale)
        {
            CalScaleBGX(scale);
            foreach (var line in MBGLines.MItemList)
            {
                if (line.MShow)
                {
                    CalScaleY(line, scale);
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
        protected List<PointF[]> CalLinesPoint(int chartWidth, int chartHeight, int chartLeft, int chartBottom)
        {
            List<PointF[]> pointList = new List<PointF[]>();
            double xaver = chartWidth / (m_maxPartX - m_minPartX);

            int index = 0;
            foreach (var line in MLines.MItemList)
            {
                if (line.MShow)
                {
                    double yaver = chartHeight / (line.MMaxPart - line.MMinPart);

                    List<PointF> list = new List<PointF>();

                    int currX = -999999;
                    int currY = -999999;
                    if (line == MLines.MSelectItem)
                    {
                        m_Index.Clear();
                        
                        for (int col = m_minPartXIndex; col <= m_maxPartXIndex; col++)
                        {
                            int tempX = (int)(xaver * (MLines.MXList[col] - m_minPartX)) + chartLeft;
                            int tempY = chartBottom - (int)(yaver * (line.GetData(col) - line.MMinPart));
                            if (tempX != currX || tempY != currY)
                            {
                                list.Add(new PointF(tempX, tempY));
                                if (list.Count > 2)
                                {
                                    if (list[list.Count - 1].X == list[list.Count - 2].X && list[list.Count - 2].X == list[list.Count - 3].X)
                                    {
                                        if (list[list.Count - 1].Y >= list[list.Count - 2].Y && list[list.Count - 1].Y >= list[list.Count - 3].Y)
                                        {
                                            list.RemoveAt(list.Count - 1);
                                        }
                                        else if (list[list.Count - 2].Y >= list[list.Count - 1].Y && list[list.Count - 2].Y >= list[list.Count - 3].Y)
                                        {
                                            list.RemoveAt(list.Count - 2);
                                        }
                                        else if (list[list.Count - 3].Y > list[list.Count - 1].Y && list[list.Count - 3].Y > list[list.Count - 2].Y)
                                        {
                                            list.RemoveAt(list.Count - 3);
                                        }
                                    }
                                }
                                currX = tempX;
                                currY = tempY;
                            }

                            m_Index.Add(list.Count - 1);
                        }
                    }
                    else
                    {
                        for (int col = m_minPartXIndex; col <= m_maxPartXIndex; col++)
                        {
                            int tempX = (int)(xaver * (MLines.MXList[col] - m_minPartX)) + chartLeft;
                            int tempY = chartBottom - (int)(yaver * (line.GetData(col) - line.MMinPart));
                            if (tempX != currX || tempY != currY)
                            {
                                list.Add(new PointF(tempX, tempY));
                                if (list.Count > 2)
                                {
                                    if (list[list.Count - 1].X == list[list.Count - 2].X && list[list.Count - 2].X == list[list.Count - 3].X)
                                    {
                                        if (list[list.Count - 1].Y >= list[list.Count - 2].Y && list[list.Count - 1].Y >= list[list.Count - 3].Y)
                                        {
                                            list.RemoveAt(list.Count - 1);
                                        }
                                        else if (list[list.Count - 2].Y >= list[list.Count - 1].Y && list[list.Count - 2].Y >= list[list.Count - 3].Y)
                                        {
                                            list.RemoveAt(list.Count - 2);
                                        }
                                        else if (list[list.Count - 3].Y > list[list.Count - 1].Y && list[list.Count - 3].Y > list[list.Count - 2].Y)
                                        {
                                            list.RemoveAt(list.Count - 3);
                                        }
                                    }
                                }

                                currX = tempX;
                                currY = tempY;
                            }
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
                else
                {
                    pointList.Add(null);
                }

                index++;
            }

            return pointList;
        }

        /// <summary>
        /// 数据点转化图标点
        /// </summary>
        /// <param name="chartWidth"></param>
        /// <param name="chartHeight"></param>
        /// <param name="chartLeft"></param>
        /// <param name="chartBottom"></param>
        /// <returns></returns>
        protected List<PointF[]> CalBGLinesPoint(int chartWidth, int chartHeight, int chartLeft, int chartBottom)
        {
            List<PointF[]> pointList = new List<PointF[]>();

            double xaver = chartWidth / (m_maxPartX - m_minPartX);

            foreach (var line in MBGLines.MItemList)
            {
                if (line.MShow)
                {
                    double yaver = chartHeight / (line.MMaxPart - line.MMinPart);

                    List<PointF> list = new List<PointF>();

                    int currX = -999999;
                    int currY = -999999;
                    for (int col = m_minPartBGXIndex; col <= m_maxPartBGXIndex; col++)
                    {
                        int tempX = (int)(xaver * (MBGLines.MXList[col] - m_minPartX)) + chartLeft;
                        int tempY = chartBottom - (int)(yaver * (line.GetData(col) - line.MMinPart));
                        if (tempX != currX || tempY != currY)
                        {
                            list.Add(new PointF(tempX, tempY));
                            if (list.Count > 2)
                            {
                                if (list[list.Count - 1].X == list[list.Count - 2].X && list[list.Count - 2].X == list[list.Count - 3].X)
                                {
                                    if (list[list.Count - 1].Y >= list[list.Count - 2].Y && list[list.Count - 1].Y >= list[list.Count - 3].Y)
                                    {
                                        list.RemoveAt(list.Count - 1);
                                    }
                                    else if (list[list.Count - 2].Y >= list[list.Count - 1].Y && list[list.Count - 2].Y >= list[list.Count - 3].Y)
                                    {
                                        list.RemoveAt(list.Count - 2);
                                    }
                                    else if (list[list.Count - 3].Y > list[list.Count - 1].Y && list[list.Count - 3].Y > list[list.Count - 2].Y)
                                    {
                                        list.RemoveAt(list.Count - 3);
                                    }
                                }
                            }
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
                else
                {
                    pointList.Add(null);
                }
            }

            return pointList;
        }

        public BackgroundInfo GetBackgroundColor()
        {
            BackgroundTable table = new BackgroundTable();
            BackgroundInfo item = null;
            table.SelectRow(out item);
            return item;
        }

        public bool UpdateBackgroundColor(EnumBackground index, Color value)
        {
            BackgroundTable table = new BackgroundTable();
            return null == table.UpdateRowColor(index, value);
        }

        public bool UpdateBackgroundVisible(EnumBackground index, bool value)
        {
            BackgroundTable table = new BackgroundTable();
            return null == table.UpdateRowVisible(index, value);
        }

        public bool UpdateBackgroundDirection(EnumBackground index, bool value)
        {
            BackgroundTable table = new BackgroundTable();
            return null == table.UpdateRowDirection(index, value);
        }

        public string GetAxisScaleList(int id, out List<AxisScale> list)
        {
            AxisScaleTable axisScaleTable = new AxisScaleTable(id);
            return axisScaleTable.GetList(out list);
        }

        public string UpdateAxisScaleList(int id, List<AxisScale> list)
        {
            AxisScaleTable axisScaleTable = new AxisScaleTable(id);
            return axisScaleTable.UpdateList(list);
        }
    }
}