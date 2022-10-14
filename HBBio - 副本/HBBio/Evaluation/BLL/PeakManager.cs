using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Evaluation
{
    public class PeakManager
    {
        private int MultipleValue = 9;      //两侧比较点的数量
        private double PerCent = 0.9;//0.95          //最少满足的数量


        /// <summary>
        /// 自动优化参数寻峰
        /// </summary>
        /// <param name="peekType"></param>
        /// <returns></returns>
        public void AutoParaOptimizing(double[] _X, double[] _Y, List<PeakIntegration> peeks, PeakType peakType)
        {
            if (null == _X)
            {
                return;
            }

            MultipleValue = 3;

            FindPeek(_X, _Y, peeks, peakType);
            //取前两分钟信号计算离散率 
            int count = 60 * 2;
            if (count > _X.Length)
                return;
            //计算前两分钟信号的方差
            List<double> lisSum = new List<double>();
            for (int i = 0; i < count; i++)
            {
                lisSum.Add(_Y[i]);
            }
            double avg = lisSum.Average();
            lisSum.Clear();
            for (int i = 0; i < count; i++)
            {
                double r = Math.Pow((_Y[i] - avg), 2);
                lisSum.Add(r);
            }
            double dx = lisSum.Average();

            //计算峰谷差距量
            lisSum.Clear();
            for (int i = 0; i < peeks.Count; i++)
            {
                lisSum.Add(_Y[peeks[i].PeekPoint]);
            }
            //防止意外
            if (lisSum.Count == 0)
                return;
            double peek_Max = lisSum.Max();
            lisSum.Clear();
            for (int i = 0; i < peeks.Count; i++)
            {
                lisSum.Add(_Y[peeks[i].StartPoint]);
                lisSum.Add(_Y[peeks[i].EndPoint]);
            }
            double valley_min = lisSum.Min();
            double dis_peek_valley = Math.Abs(peek_Max - valley_min);

            if (dx < 100)
                MultipleValue = 8;
            else if (dx < 1000)
                MultipleValue = 5;
            else if (dx < 10000)
                MultipleValue = 3;
            else if (dx < 100000)
                MultipleValue = 2;
            else
                MultipleValue = 1;

            if (dis_peek_valley < 800)
                MultipleValue = 4;
            //else if (dis_peek_valley < 800)
            //    MultipleValue = 3;
            //MessageBox.Show(dis_peek_valley.ToString() + "  " + dx.ToString() + "  " + MultipleValue.ToString());
        }

        public void FindPeek(double[] x, double[] y, int indexS, int indexE, List<PeakIntegration> list, PeakType peakType)
        {
            list.Clear();

            if(null==x)
            {
                return;
            }

            //峰顶在数据中的序号
            List<int> LisPeekTopIndex = new List<int>();
            //寻峰顶判断左右是否是上升和下降
            indexS = Math.Max(indexS, MultipleValue);
            indexE = Math.Min(indexE, y.Length - MultipleValue);
            for (int i = indexS; i < indexE; i++)
            {
                if (isPeekorValley(y, i, 0))
                {
                    LisPeekTopIndex.Add(i);
                }     
            }
            //清理峰顶群算法，单位时间内留下最大的顶
            //for (int i = 0; i < LisPeekTopIndex.Count; i++)
            //{
            //    if (i + 1 < LisPeekTopIndex.Count && LisPeekTopIndex[i + 1] - LisPeekTopIndex[i] < MultipleValue && y[LisPeekTopIndex[i]] <= y[LisPeekTopIndex[i + 1]])
            //        LisPeekTopIndex.RemoveAt(i);
            //}
            for (int i = LisPeekTopIndex.Count - 1; i > 1; i--)
            {
                if (LisPeekTopIndex[i] - LisPeekTopIndex[i-1] < MultipleValue && y[LisPeekTopIndex[i]] <= y[LisPeekTopIndex[i - 1]])
                    LisPeekTopIndex.RemoveAt(i);
            }

            //通过峰顶扩展峰，找寻到基线的起始结束点
            Dictionary<int, int> DicPeekBody = new Dictionary<int, int>();
            //峰列表
            foreach (var item in LisPeekTopIndex)
            {
                int StartIndex = item;
                int EndIndex = item;
                //没到基线却是谷底；不是谷底，却到基线
                while (StartIndex > 0 && !isPeekorValley(y, StartIndex, 1) && y[StartIndex - 1] <= y[StartIndex])
                    StartIndex--;
                while (EndIndex < x.Length - 1 && !isPeekorValley(y, EndIndex, 1) && y[EndIndex + 1] <= y[EndIndex])
                    EndIndex++;

                if (!DicPeekBody.ContainsKey(StartIndex))
                    DicPeekBody.Add(StartIndex, EndIndex);

                bool isContainPeek = false;
                //每个可能的峰顶都探测一遍起始终点，如果起点相同，则确定列表中有该峰，峰信息以新的为准进行覆盖
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].StartPoint == StartIndex)
                    {
                        list[i].PeekPoint = item;
                        list[i].EndPoint = EndIndex;
                        isContainPeek = true;
                    }
                }
                //如果不存在，则在确定列表中添加该峰，并且要满足起始和终点与峰顶下标不同
                if (!isContainPeek && StartIndex != item & EndIndex != item)
                {
                    //if (peekType == PeekType.VerticalSeparation)
                    //LisPeeks.Add(new Peek(StartIndex, EndIndex, item, BaseLine, BaseLine));
                    //if (peekType == PeekType.PeakValleySeparation)
                    list.Add(new PeakIntegration(StartIndex, EndIndex, item, y[StartIndex], y[EndIndex], peakType));
                }
            }
            if (peakType == PeakType.VerticalSeparation)
                SeekBaseLine(x, y, list, 0, list.Count - 1);

            //过滤最小峰高和峰面积响应
            //if (String.IsNullOrEmpty(txt_MinSet.Text)/* && (string)btn_MinSet.Tag == "True"*/)
            //{
            //    List<Peek> temp = new List<Peek>();
            //    foreach (var item in LisPeeks)
            //    {
            //        if (Math.Round(CalcIntegration(x, y, item)) > Convert.ToInt32(txt_MinSet.Text))
            //            temp.Add(item);
            //    }
            //    LisPeeks = temp;
        }

        public void FindPeek(double[] x, double[] y, List<PeakIntegration> list, PeakType peakType)
        {
            list.Clear();

            if (null == x)
            {
                return;
            }

            //峰顶在数据中的序号
            List<int> LisPeekTopIndex = new List<int>();
            //寻峰顶判断左右是否是上升和下降
            for (int i = MultipleValue; i < y.Length - MultipleValue; i++)
            {
                if (isPeekorValley(y, i, 0))
                {
                    LisPeekTopIndex.Add(i);
                }
            }
            //清理峰顶群算法，单位时间内留下最大的顶
            //for (int i = 0; i < LisPeekTopIndex.Count; i++)
            //{
            //    if (i + 1 < LisPeekTopIndex.Count && LisPeekTopIndex[i + 1] - LisPeekTopIndex[i] < MultipleValue && y[LisPeekTopIndex[i]] <= y[LisPeekTopIndex[i + 1]])
            //        LisPeekTopIndex.RemoveAt(i);
            //}
            for (int i = LisPeekTopIndex.Count - 1; i > 1; i--)
            {
                if (LisPeekTopIndex[i] - LisPeekTopIndex[i - 1] < MultipleValue && y[LisPeekTopIndex[i]] <= y[LisPeekTopIndex[i - 1]])
                    LisPeekTopIndex.RemoveAt(i);
            }

            //通过峰顶扩展峰，找寻到基线的起始结束点
            Dictionary<int, int> DicPeekBody = new Dictionary<int, int>();
            //峰列表
            foreach (var item in LisPeekTopIndex)
            {
                int StartIndex = item;
                int EndIndex = item;
                //没到基线却是谷底；不是谷底，却到基线
                while (StartIndex > 0 && !isPeekorValley(y, StartIndex, 1) && y[StartIndex - 1] <= y[StartIndex])
                    StartIndex--;
                while (EndIndex < x.Length - 1 && !isPeekorValley(y, EndIndex, 1) && y[EndIndex + 1] <= y[EndIndex])
                    EndIndex++;

                if (!DicPeekBody.ContainsKey(StartIndex))
                    DicPeekBody.Add(StartIndex, EndIndex);

                bool isContainPeek = false;
                //每个可能的峰顶都探测一遍起始终点，如果起点相同，则确定列表中有该峰，峰信息以新的为准进行覆盖
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].StartPoint == StartIndex)
                    {
                        list[i].PeekPoint = item;
                        list[i].EndPoint = EndIndex;
                        isContainPeek = true;
                    }
                }
                //如果不存在，则在确定列表中添加该峰，并且要满足起始和终点与峰顶下标不同
                if (!isContainPeek && StartIndex != item & EndIndex != item)
                {
                    //if (peekType == PeekType.VerticalSeparation)
                    //LisPeeks.Add(new Peek(StartIndex, EndIndex, item, BaseLine, BaseLine));
                    //if (peekType == PeekType.PeakValleySeparation)
                    list.Add(new PeakIntegration(StartIndex, EndIndex, item, y[StartIndex], y[EndIndex], peakType));
                }
            }
            if (peakType == PeakType.VerticalSeparation)
                SeekBaseLine(x, y, list, 0, list.Count - 1);

            //过滤最小峰高和峰面积响应
            //if (String.IsNullOrEmpty(txt_MinSet.Text)/* && (string)btn_MinSet.Tag == "True"*/)
            //{
            //    List<Peek> temp = new List<Peek>();
            //    foreach (var item in LisPeeks)
            //    {
            //        if (Math.Round(CalcIntegration(x, y, item)) > Convert.ToInt32(txt_MinSet.Text))
            //            temp.Add(item);
            //    }
            //    LisPeeks = temp;
        }

        private double FindBaseLine(double[] y)
        {
            double baseLine = 0;

            Hashtable ht = new Hashtable();
            foreach (var it in y)
            {
                int index = (int)Math.Floor(it);
                if (ht.ContainsKey(index))
                {
                    ht[index] = (int)ht[index] + 1;
                }
                else
                {
                    ht.Add(index, 0);
                }
            }

            int countBaseLine = 0;
            foreach (DictionaryEntry de in ht)
            {
                if ((int)de.Value > countBaseLine)
                {
                    countBaseLine = (int)de.Value;
                    baseLine = (int)de.Key;
                }
            }

            return baseLine;
        }

        /// <summary>
        /// 寻找峰顶
        /// </summary>
        /// <param name="y"></param>
        /// <param name="_index"></param>
        /// <param name="_flag"></param>
        /// <returns></returns>
        private bool isPeekorValley(double[] y, int _index, int _flag)
        {
            switch (_flag)
            {
                case 0:
                    {
                        //计算左侧上升沿
                        int count1 = 0;
                        for (int j = _index - MultipleValue; j < _index; j++)
                            if (y[j] <= y[j + 1])
                                count1++;
                        //计算右侧下降沿
                        int count2 = 0;
                        for (int j = _index; j < _index + MultipleValue; j++)
                            if (y[j] >= y[j + 1])
                                count2++;
                        //判断是否是峰顶   
                        if (count1 > PerCent * MultipleValue && count2 > PerCent * MultipleValue && count1 == count2)
                            return true;
                        else
                            return false;
                    }
                case 1:
                    {
                        //计算左侧下降沿
                        int count1 = 0;
                        for (int j = _index - MultipleValue; j < _index && j > -1 && j + 1 < y.Length; j++)
                            if (y[j] >= y[j + 1])
                                count1++;
                        //计算右侧上升沿
                        int count2 = 0;
                        for (int j = _index; j < _index + MultipleValue && j > -1 && j + 1 < y.Length; j++)
                            if (y[j] <= y[j + 1])
                                count2++;
                        //判断是否是谷底
                        if (count1 > PerCent * MultipleValue && count2 > PerCent * MultipleValue && count1 == count2)
                            return true;
                        else
                            return false;
                    }
                default:
                    return false;
            }
        }

        /// <summary>
        /// 寻找垂直分离基线
        /// </summary>
        /// <param name="LisPeeks"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void SeekBaseLine(double[] x, double[] y, List<PeakIntegration> LisPeeks, int i, int j)
        {
            bool flag = true;
            if (LisPeeks.Count == 0)
                return;
            double k = (y[LisPeeks[j].EndPoint] - y[LisPeeks[i].StartPoint]) / (x[LisPeeks[j].EndPoint] - x[LisPeeks[i].StartPoint]);
            double b = y[LisPeeks[j].EndPoint] - k * x[LisPeeks[j].EndPoint];

            int n = i;
            for (int m = i + 1; m <= j; m++)
            {
                if (y[LisPeeks[m].StartPoint] < k * x[LisPeeks[m].StartPoint] + b)
                {
                    flag = false;
                    n = m;
                    SeekBaseLine(x,y, LisPeeks, m, j);
                    break;
                }
            }
            if (flag)
            {
                double K = (y[LisPeeks[j].EndPoint] - y[LisPeeks[i].StartPoint]) / (x[LisPeeks[j].EndPoint] - x[LisPeeks[i].StartPoint]);
                double B = y[LisPeeks[i].StartPoint] - K * x[LisPeeks[i].StartPoint];

                //LisBaseLinePointF.Add(new PointFPair(new PointF((float)x[LisPeeks[i].StartPoint], (float)(x[LisPeeks[i].StartPoint] * K + B)), new PointF((float)x[LisPeeks[j].EndPoint], (float)(x[LisPeeks[j].EndPoint] * K + B))));

                for (int t = i; t <= j; t++)
                {
                    LisPeeks[t].StartBaseY = x[LisPeeks[t].StartPoint] * K + B;
                    LisPeeks[t].EndBaseY = x[LisPeeks[t].EndPoint] * K + B;
                }
            }
            else
            {
                //LisBaseLinePointF.Add(new PointF(LisPeeks[i].StartPoint, LisPeeks[n - 1].EndPoint));
                double K = (y[LisPeeks[n - 1].EndPoint] - y[LisPeeks[i].StartPoint]) / (x[LisPeeks[n - 1].EndPoint] - x[LisPeeks[i].StartPoint]);
                double B = y[LisPeeks[i].StartPoint] - K * x[LisPeeks[i].StartPoint];

                //LisBaseLinePointF.Add(new PointFPair(new PointF((float)x[LisPeeks[i].StartPoint], (float)(x[LisPeeks[i].StartPoint] * K + B)), new PointF((float)x[LisPeeks[n - 1].EndPoint], (float)(x[LisPeeks[n - 1].EndPoint] * K + B))));

                for (int t = i; t <= n - 1; t++)
                {
                    LisPeeks[t].StartBaseY = x[LisPeeks[t].StartPoint] * K + B;
                    LisPeeks[t].EndBaseY = x[LisPeeks[t].EndPoint] * K + B;
                }
            }
        }
    }
}
