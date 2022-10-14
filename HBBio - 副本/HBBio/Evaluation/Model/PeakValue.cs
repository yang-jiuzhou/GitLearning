using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Evaluation
{
    public class PeakValue
    {
        public double[] m_x = null;
        public double[] m_y = null;
        
        public List<PeakIntegration> m_listOriginal = new List<PeakIntegration>();
        public ObservableCollection<PeakIntegration> m_list = new ObservableCollection<PeakIntegration>();
        private double m_minHeight = 0;
        private double m_minArea = 0;
        private double m_minHalfWidth = 0;
        private double m_ch = 1;
        private double m_original = 0;


        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Init(List<double> x, List<double> y, double ch)
        {
            m_x = null;
            m_y = null;
            m_x = x.ToArray();
            m_y = y.ToArray();

            if (-1 != ch)
            {
                m_ch = ch;
            }
        }

        /// <summary>
        /// 计算积分
        /// </summary>
        /// <param name="minHeight"></param>
        /// <param name="minArea"></param>
        /// <param name="minHalfWidth"></param>
        public void CalIntegration(IntegrationSet integrationSet)
        {
            m_minHeight = integrationSet.MMinHeight;
            m_minArea = integrationSet.MMinArea;
            m_minHalfWidth = integrationSet.MMinArea;

            m_ch = integrationSet.MCH;
            m_original = integrationSet.MOriginal;

            List<double> listArea = new List<double>();
            double totalArea = 0;
            m_list.Clear();
            for (int i = 0; i < m_listOriginal.Count; i++)
            {
                PeakIntegration item = new PeakIntegration(m_listOriginal[i].StartPoint, m_listOriginal[i].EndPoint, m_listOriginal[i].PeekPoint, m_listOriginal[i].StartBaseY, m_listOriginal[i].EndBaseY, m_listOriginal[i].MPeakType);
                item.MRetentionTime = m_x[m_listOriginal[i].PeekPoint] - m_original;      //保留时间
                if (item.MRetentionTime < 0)
                {
                    continue;
                }
                item.MTopVal = m_y[m_listOriginal[i].PeekPoint];
                item.MStartValX = m_x[m_listOriginal[i].StartPoint];                       //起始值
                item.MStartValY = m_y[m_listOriginal[i].StartPoint];                       //起始值
                item.MEndValX = m_x[m_listOriginal[i].EndPoint];                           //终止值
                item.MEndValY = m_y[m_listOriginal[i].EndPoint];                           //终止值
                item.MHeight = PeakMath.CalcPeekHigh(m_x, m_y, m_listOriginal[i]);         //峰高
                item.MArea = PeakMath.CalcIntegration(m_x, m_y, m_listOriginal[i]);        //峰面积
                item.MHalfWidth = PeakMath.CalHalfWidth(m_x, m_y, m_listOriginal[i]);      //半峰宽
                if (integrationSet.MIsMin)
                {
                    if (item.MHeight < integrationSet.MMinHeight || item.MArea < integrationSet.MMinArea || item.MHalfWidth < integrationSet.MMinWidth)
                    {
                        continue;
                    }
                }
                else
                {
                    if (listArea.Count < integrationSet.MPeakCount)
                    {
                        listArea.Add(item.MArea);
                    }
                    else
                    {
                        double min = listArea[0];
                        int minK = 0;
                        for (int k = 1; k < listArea.Count; k++)
                        {
                            if (listArea[k] < min)
                            {
                                min = listArea[k];
                                minK = k;
                            }
                        }

                        if (min < item.MArea)
                        {
                            listArea.RemoveAt(minK);
                            listArea.Add(item.MArea);

                            totalArea -= m_list[minK].MArea;
                            m_list.RemoveAt(minK);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                item.MTpn = PeakMath.CalcTPN(m_x, m_y, m_listOriginal[i], m_ch);                 //理论塔板数
                item.MTailingFactor = PeakMath.CalcTF(m_x, m_y, m_listOriginal[i]);        //拖尾因子(对称因子)5%
                item.MSymmetryFactor = PeakMath.CalcAS(m_x, m_y, m_listOriginal[i]);       //不对称因子10%

                m_list.Add(item);

                totalArea += item.MArea;
            }

            for (int i = 0; i < m_list.Count; i++)
            {
                m_list[i].MName = (i + 1).ToString();
                m_list[i].MAreaPer = Math.Round(m_list[i].MArea / totalArea * 100, 2);     //峰面积百分比
                if (0 < i)
                {
                    m_list[i].MResolution = PeakMath.CalcResolution(m_x, m_y, m_list[i - 1], m_list[i]);
                }
            }
        }

        /// <summary>
        /// 计算积分
        /// </summary>
        private void CalIntegration()
        {
            double totalArea = 0;
            int index = 1;
            PeakIntegration last = null;
            foreach (var item in m_list)
            {
                item.MName = index.ToString();
                index++;
                item.MRetentionTime = m_x[item.PeekPoint] - m_original;         //保留时间
                item.MTopVal = m_y[item.PeekPoint];
                item.MStartValX = m_x[item.StartPoint];                       //起始值
                item.MStartValY = m_y[item.StartPoint];                       //起始值
                item.MEndValX = m_x[item.EndPoint];                           //终止值
                item.MEndValY = m_y[item.EndPoint];                           //终止值
                item.MHeight = PeakMath.CalcPeekHigh(m_x, m_y, item);         //峰高
                item.MArea = PeakMath.CalcIntegration(m_x, m_y, item);        //峰面积
                item.MHalfWidth = PeakMath.CalHalfWidth(m_x, m_y, item);        //半峰宽
                item.MTpn = PeakMath.CalcTPN(m_x, m_y, item, m_ch);             //理论塔板数
                item.MTailingFactor = PeakMath.CalcTF(m_x, m_y, item);        //拖尾因子(对称因子)5%
                item.MSymmetryFactor = PeakMath.CalcAS(m_x, m_y, item);       //不对称因子10%
                if (2 < index)
                {
                    item.MResolution = PeakMath.CalcResolution(m_x, m_y, last, item);    //分离度
                }
                last = item;

                totalArea += item.MArea;
            }

            foreach (var it in m_list)
            {
                it.MAreaPer = Math.Round(it.MArea / totalArea * 100, 2);     //峰面积百分比
            }
        }

        /// <summary>
        /// 修改峰宽
        /// </summary>
        /// <param name="indexS"></param>
        /// <param name="indexE"></param>
        /// <returns></returns>
        public bool ModifyPeakWidth(int indexS, int indexE)
        {
            if (null == m_x)
            {
                return false;
            }

            if (indexS > indexE)
            {
                Share.ValueTrans.Swap(ref indexS, ref indexE);
            }

            for (int i = 0; i < m_list.Count; i++)
            {
                if (indexS < m_list[i].PeekPoint && indexE > m_list[i].PeekPoint)
                {
                    if (0 == i)
                    {
                        if (-1 == indexS)
                        {
                            indexS = 0;
                        }
                    }
                    else if (m_list.Count - 1 == i)
                    {
                        if (-1 == indexE)
                        {
                            indexE = m_x.Length - 1;
                        }
                    }
                    else
                    {
                        if (indexS < m_list[i - 1].EndPoint || indexE > m_list[i + 1].StartPoint)
                        {
                            return false;
                        }
                    }

                    m_list[i].StartPoint = indexS;
                    m_list[i].EndPoint = indexE;
                    m_list[i].StartBaseY = m_y[indexS];
                    m_list[i].EndBaseY = m_y[indexE];
                    CalIntegration();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 修改峰的起点
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool ModifyPeakStart(int index)
        {
            if (null == m_x)
            {
                return false;
            }

            for (int i = 0; i < m_list.Count; i++)
            {
                if (index < m_list[i].PeekPoint)
                {
                    if (0 == i)
                    {
                        if (-1 == index)
                        {
                            index = 0;
                        }
                    }
                    else
                    {
                        if (index < m_list[i - 1].EndPoint)
                        {
                            return false;
                        }
                    }

                    m_list[i].StartPoint = index;
                    m_list[i].StartBaseY = m_y[index];
                    CalIntegration();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 修改峰的终点
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool ModifyPeakEnd(int index)
        {
            if (null == m_x)
            {
                return false;
            }

            for (int i = 0; i < m_list.Count; i++)
            {
                if (index > m_list[i].PeekPoint)
                {
                    if (m_list.Count - 1 == i)
                    {
                        if (-1 == index)
                        {
                            index = m_x.Length - 1;
                        }
                    }
                    else
                    {
                        if (index > m_list[i + 1].StartPoint)
                        {
                            return false;
                        }
                    }

                    m_list[i].EndPoint = index;
                    m_list[i].EndBaseY = m_y[index];
                    CalIntegration();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 增加正峰
        /// </summary>
        /// <param name="indexS"></param>
        /// <param name="indexE"></param>
        /// <returns></returns>
        public bool ModifyPeakPlus(int indexS, int indexE)
        {
            if (null == m_x)
            {
                return false;
            }

            if (indexS > indexE)
            {
                Share.ValueTrans.Swap(ref indexS, ref indexE);
            }

            for (int i = 0; i < m_list.Count; i++)
            {
                if (indexS < m_list[i].StartPoint && indexE > m_list[i].StartPoint
                    || indexS < m_list[i].EndPoint && indexE > m_list[i].EndPoint)
                {
                    return false;
                }
                else if(indexS >= m_list[i].StartPoint && indexE <= m_list[i].EndPoint)
                {
                    return false;
                }
            }

            int maxIndex = indexS;
            double maxVal = m_y[indexS];
            for (int i = indexS; i <= indexE; i++)
            {
                if (m_y[i] > maxVal)
                {
                    maxVal = m_y[i];
                    maxIndex = i;
                }
            }

            if (maxIndex == indexS || maxIndex == indexE)
            {
                return false;
            }

            PeakIntegration item = new PeakIntegration(indexS, indexE, maxIndex, m_y[indexS], m_y[indexE], PeakType.VerticalSeparation);

            if (0 == m_list.Count)
            {
                m_list.Add(item);
            }
            else
            {
                for (int i = 0; i < m_list.Count; i++)
                {
                    if (item.PeekPoint < m_list[i].PeekPoint)
                    {
                        m_list.Insert(i, item);
                        break;
                    }
                    else if (i == m_list.Count - 1)
                    {
                        m_list.Add(item);
                        break;
                    }
                }
            }
            CalIntegration();

            return false;
        }

        /// <summary>
        /// 删除峰
        /// </summary>
        /// <param name="indexS"></param>
        /// <param name="indexE"></param>
        /// <returns></returns>
        public bool ModifyPeakDel(int indexS, int indexE)
        {
            if (null == m_x)
            {
                return false;
            }

            if (indexS > indexE)
            {
                Share.ValueTrans.Swap(ref indexS, ref indexE);
            }

            bool flag = false;
            for (int i = 0; i < m_list.Count; i++)
            {
                if (indexS <= m_list[i].PeekPoint && indexE >= m_list[i].PeekPoint)
                {
                    m_list.RemoveAt(i);
                    i--;
                    flag = true;
                }
            }
            if (flag)
            {
                CalIntegration();
            }

            return flag;
        }

        /// <summary>
        /// 峰前切
        /// </summary>
        /// <param name="indexS"></param>
        /// <param name="indexE"></param>
        /// <returns></returns>
        public bool ModifyFrontCut(int indexS, int indexE)
        {
            if (null == m_x)
            {
                return false;
            }

            int i = 0;
            while (i < m_list.Count && m_list[i].StartPoint < indexS)
            {
                i++;
            }
            int j = i;
            while (j < m_list.Count && m_list[j].EndPoint < indexE)
            {
                j++;
            }
            j--;

            if (i > j)
            {
                return false;
            }

            FrontCut(i, j);

            CalIntegration();

            return true;
        }

        private void FrontCut(int i, int j)
        {
            bool flag = true;

            if (0 == m_list.Count)
            {
                return;
            }

            double k = (m_y[m_list[j].EndPoint] - m_y[m_list[i].StartPoint]) / (m_x[m_list[j].EndPoint] - m_x[m_list[i].StartPoint]);
            double b = m_y[m_list[j].EndPoint] - k * m_x[m_list[j].EndPoint];

            int n = i;
            for (int m = i + 1; m < j; m++)
            {
                if (m_y[m_list[m].StartPoint] < k * m_x[m_list[m].StartPoint] + b)
                {
                    flag = false;
                    n = m;
                    FrontCut(m, j);
                    break;
                }
            }
            if (flag)
            {
                double K = (m_y[m_list[j].EndPoint] - m_y[m_list[i].StartPoint]) / (m_x[m_list[j].EndPoint] - m_x[m_list[i].StartPoint]);
                double B = m_y[m_list[i].StartPoint] - K * m_x[m_list[i].StartPoint];

                for (int t = i; t <= j; t++)
                {
                    m_list[t].StartBaseY = m_x[m_list[t].StartPoint] * K + B;
                    m_list[t].EndBaseY = m_x[m_list[t].EndPoint] * K + B;
                }
            }
            else
            {
                double K = (m_y[m_list[n - 1].EndPoint] - m_y[m_list[i].StartPoint]) / (m_x[m_list[n - 1].EndPoint] - m_x[m_list[i].StartPoint]);
                double B = m_y[m_list[i].StartPoint] - K * m_x[m_list[i].StartPoint];

                for (int t = i; t <= n - 1; t++)
                {
                    m_list[t].StartBaseY = m_x[m_list[t].StartPoint] * K + B;
                    m_list[t].EndBaseY = m_x[m_list[t].EndPoint] * K + B;
                }
            }
        }

        /// <summary>
        /// 峰后切
        /// </summary>
        /// <param name="indexS"></param>
        /// <param name="indexE"></param>
        /// <returns></returns>
        public bool ModifyBackCut(int indexS, int indexE)
        {
            if (null == m_x)
            {
                return false;
            }

            int i = 0;
            while (i < m_list.Count && m_list[i].StartPoint < indexS)
            {
                i++;
            }
            int j = i + 1;
            while (j < m_list.Count && m_list[j].EndPoint < indexE)
            {
                j++;
            }
            j--;

            if (i > j)
            {
                return false;
            }

            BackCut(i, j);

            CalIntegration();

            return true;
        }

        private void BackCut(int i, int j)
        {
            bool flag = true;

            if (0 == m_list.Count)
            {
                return;
            }

            double k = (m_y[m_list[j].EndPoint] - m_y[m_list[i].StartPoint]) / (m_x[m_list[j].EndPoint] - m_x[m_list[i].StartPoint]);
            double b = m_y[m_list[j].EndPoint] - k * m_x[m_list[j].EndPoint];

            int n = i;
            for (int m = i; m < j; m++)
            {
                //如果点比切线低或者斜率为正
                if (m_y[m_list[m].EndPoint] < k * m_x[m_list[m].EndPoint] + b)
                {
                    flag = false;
                    n = m;
                    BackCut(m + 1, j);
                    break;
                }
            }

            if (flag)
            {
                double K = (m_y[m_list[j].EndPoint] - m_y[m_list[i].StartPoint]) / (m_x[m_list[j].EndPoint] - m_x[m_list[i].StartPoint]);
                double B = m_y[m_list[i].StartPoint] - K * m_x[m_list[i].StartPoint];

                for (int t = i; t <= j; t++)
                {
                    m_list[t].StartBaseY = m_x[m_list[t].StartPoint] * K + B;
                    m_list[t].EndBaseY = m_x[m_list[t].EndPoint] * K + B;
                }
            }
            else
            {
                double K = (m_y[m_list[n].EndPoint] - m_y[m_list[i].StartPoint]) / (m_x[m_list[n].EndPoint] - m_x[m_list[i].StartPoint]);
                double B = m_y[m_list[i].StartPoint] - K * m_x[m_list[i].StartPoint];

                for (int t = i; t <= n; t++)
                {
                    m_list[t].StartBaseY = m_x[m_list[t].StartPoint] * K + B;
                    m_list[t].EndBaseY = m_x[m_list[t].EndPoint] * K + B;
                }
            }
        }
    }
}