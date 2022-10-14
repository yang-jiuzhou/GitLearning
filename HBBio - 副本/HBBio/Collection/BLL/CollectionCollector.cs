using HBBio.Communication;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Collection
{
    /**
	 * ClassName: CollectionCollector
	 * Description: 组分收集
	 * Version: 1.0
	 * Create:  2021/03/17
	 * Author:  yangjiuzhou
	 * Company: jshanbon
	 **/
    [Serializable]
    public class CollectionCollector
    {
        #region 字段 控制
        public bool m_update = false;

        private bool m_signal = false;                                      //是否启用     
        private int m_condIndex = -1;                                       //收集序号
        private bool m_condIndexNew = true;
        private CollTextIndex m_currIndex = new CollTextIndex(EnumCollIndexText.L, 1, false);
        private double m_condIndexVal1 = 0;                                 //条件1的起始点
        private double m_condIndexVal2 = 0;                                 //条件2的起始点
        private double m_condVol = 0;
        private EnumNOIngFinish m_condIndexStatus1 = EnumNOIngFinish.NULL;  //条件1状态
        private EnumNOIngFinish m_condIndexStatus2 = EnumNOIngFinish.NULL;  //条件2状态
        private double m_loopIndexVal1 = 0;                                 //当前循环的起始点1
        private double m_loopIndexVal2 = 0;                                 //当前循环的起始点2
        private double m_loopVol = 0;
        private EnumNOIngFinish m_loopIndexStatus1 = EnumNOIngFinish.NULL;  //循环1状态
        private EnumNOIngFinish m_loopIndexStatus2 = EnumNOIngFinish.NULL;  //循环2状态

        private Queue<CollectionCollectorDelay> m_delayQue = new Queue<CollectionCollectorDelay>();        //延迟信息列表
        private Queue<string> m_logDescQue = new Queue<string>();
        private Queue<string> m_logOperQue = new Queue<string>();
        private string m_logDesc = "";
        private string m_logOper = "";
        #endregion

        #region 字段 属性
        private List<CollectionItem> m_list = new List<CollectionItem>();   //集合
        #endregion

        #region 属性 控制
        /// <summary>
        /// 是否正在收集
        /// </summary>
        public bool MSignal
        {
            get
            {
                return m_signal;
            }
        }
        /// <summary>
        /// 收集集合的当前执行序号
        /// </summary>
        public int MIndex
        {
            get
            {
                if (null == m_list || 0 == m_list.Count)
                {
                    return 0;
                }
                else
                {
                    return m_condIndex + 1;
                }
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 收集集合
        /// </summary>
        public List<CollectionItem> MList
        {
            get
            {
                return m_list;
            }
            set
            {
                m_list.Clear();

                if (null != value)
                {
                    value.ForEach(i => m_list.Add(i));
                }
            }
        }
        #endregion


        /// <summary>
        /// 初始化条件
        /// </summary>
        /// <param name="index"></param>
        /// <param name="listTVCV"></param>
        public void Init(ref CollTextIndex index, double[] listTVCV)
        {
            m_condIndex = -1;
            m_condIndexNew = true;

            m_logDesc = "";
            m_logOper = "";

            if (0 == m_list.Count)
            {
                JudgeFinish();
                index.MStatus = false;
            }
            else
            {
                m_currIndex = new CollTextIndex(index.MText, index.MIndex);
                JudgeCondNext(listTVCV, true);
            }
        }

        /// <summary>
        /// 判断条件状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="listTVCV"></param>
        /// <param name="listVal"></param>
        /// <param name="listSlope"></param>
        /// <returns></returns>
        public bool JudgeCondition(ref CollTextIndex index, double[] listTVCV, List<double> listVal, List<double> listMinVal, List<double> listMaxVal, List<double> listSlope, List<double> listLastSlope)
        {
            if (m_signal)
            {
                //判断条件1
                switch (CollectionJudge.JudgeObjectMulti(ref m_condIndexStatus1, ref m_condIndexStatus2, listTVCV, listVal, listMinVal, listMaxVal, listSlope, listLastSlope, m_list[m_condIndex].MCond, m_condIndexVal1, ref m_condIndexVal2))
                {
                    case EnumNOIngFinish.NoFirst:
                        //收集未满足，切到排废
                        QueAddCond(new CollTextIndex(false), listTVCV[1], m_list[m_condIndex]);
                        break;
                    case EnumNOIngFinish.IngFirst:
                        //收集开始，切到收集口
                        m_condVol = listTVCV[1];
                        switch (m_list[m_condIndex].MPositionType)
                        {
                            case EnumPositionType.Fixed:
                                //没有内部循环                          
                                switch (m_list[m_condIndex].MPositionStart)
                                {
                                    case EnumPositionStart.Default:
                                        {
                                            //计算下一个收集口
                                            CollTextIndex currIndex = new CollTextIndex(index.MText, index.MIndex);
                                            CalNext(ref currIndex);
                                            QueAddCond(currIndex, listTVCV[1], m_list[m_condIndex]);
                                        }
                                        break;
                                    case EnumPositionStart.Left:
                                        QueAddCond(new CollTextIndex(EnumCollIndexText.L, m_list[m_condIndex].MStartIndex), listTVCV[1], m_list[m_condIndex]);
                                        break;
                                    case EnumPositionStart.Right:
                                        QueAddCond(new CollTextIndex(EnumCollIndexText.R, m_list[m_condIndex].MStartIndex), listTVCV[1], m_list[m_condIndex]);
                                        break;
                                }
                                break;
                            case EnumPositionType.Loop:
                                //有内部循环
                                {
                                    //计算下一个收集口
                                    CollTextIndex currIndex = new CollTextIndex(index.MText, index.MIndex);
                                    CalNext(ref currIndex);
                                    InitLoop(currIndex, listTVCV, m_condIndexNew);
                                }
                                break;
                        }
                        break;
                    case EnumNOIngFinish.Ing:
                        //收集中，进入循环以及判断是否漫出
                        switch (m_list[m_condIndex].MPositionType)
                        {
                            case EnumPositionType.Fixed:
                                //没有内部循环
                                switch (index.MText)
                                {
                                    case EnumCollIndexText.L:
                                        if (listTVCV[1] - m_condVol >= EnumCollectorInfo.s_btjL)
                                        {
                                            m_logDescQue.Enqueue(ReadXamlCollection.C_CollOver);
                                            m_logOperQue.Enqueue(ReadXamlCollection.S_CollOver);
                                            JudgeCondNext(listTVCV, false);
                                        }
                                        break;
                                    case EnumCollIndexText.R:
                                        if (listTVCV[1] - m_condVol >= EnumCollectorInfo.s_btjR)
                                        {
                                            m_logDescQue.Enqueue(ReadXamlCollection.C_CollOver);
                                            m_logOperQue.Enqueue(ReadXamlCollection.S_CollOver);
                                            JudgeCondNext(listTVCV, false);
                                        }
                                        break;
                                }
                                break;
                            case EnumPositionType.Loop:
                                //有内部循环
                                {
                                    //计算理论当前收集口
                                    CollTextIndex currIndex = new CollTextIndex(index.MText, index.MIndex);
                                    CalCurr(ref currIndex);
                                    JudgeLoop(currIndex, listTVCV, listVal, listMinVal, listMaxVal, listSlope, listLastSlope);
                                }
                                break;
                        }
                        break;
                    case EnumNOIngFinish.PauseFirst:
                        //收集暂停
                        QueAddCond(new CollTextIndex(false), listTVCV[1], m_list[m_condIndex]);
                        m_condIndexNew = false;
                        break;
                    case EnumNOIngFinish.OverFirst:
                        //收集不满足,切到排序
                        QueAddCond(new CollTextIndex(false), listTVCV[1], m_list[m_condIndex]);
                        break;
                    case EnumNOIngFinish.Finish:
                        //收集结束
                        JudgeCondNext(listTVCV, true);
                        break;
                }
            }

            if (m_signal)
            {
                //判断条件1
                switch (CollectionJudge.JudgeObjectMulti(ref m_condIndexStatus1, ref m_condIndexStatus2, listTVCV, listVal, listMinVal, listMaxVal, listSlope, listLastSlope, m_list[m_condIndex].MCond, m_condIndexVal1, ref m_condIndexVal2))
                {
                    case EnumNOIngFinish.NoFirst:
                        //收集未满足，切到排废
                        QueAddCond(new CollTextIndex(false), listTVCV[1], m_list[m_condIndex]);
                        break;
                    case EnumNOIngFinish.IngFirst:
                        //收集开始，切到收集口
                        m_condVol = listTVCV[1];
                        switch (m_list[m_condIndex].MPositionType)
                        {
                            case EnumPositionType.Fixed:
                                //没有内部循环                          
                                switch (m_list[m_condIndex].MPositionStart)
                                {
                                    case EnumPositionStart.Default:
                                        {
                                            //计算下一个收集口
                                            CollTextIndex currIndex = new CollTextIndex(index.MText, index.MIndex);
                                            CalNext(ref currIndex);
                                            QueAddCond(currIndex, listTVCV[1], m_list[m_condIndex]);
                                        }
                                        break;
                                    case EnumPositionStart.Left:
                                        QueAddCond(new CollTextIndex(EnumCollIndexText.L, m_list[m_condIndex].MStartIndex), listTVCV[1], m_list[m_condIndex]);
                                        break;
                                    case EnumPositionStart.Right:
                                        QueAddCond(new CollTextIndex(EnumCollIndexText.R, m_list[m_condIndex].MStartIndex), listTVCV[1], m_list[m_condIndex]);
                                        break;
                                }
                                break;
                            case EnumPositionType.Loop:
                                //有内部循环
                                {
                                    //计算下一个收集口
                                    CollTextIndex currIndex = new CollTextIndex(index.MText, index.MIndex);
                                    CalNext(ref currIndex);
                                    InitLoop(currIndex, listTVCV, m_condIndexNew);
                                }
                                break;
                        }
                        break;
                    case EnumNOIngFinish.Ing:
                        //收集中，进入循环以及判断是否漫出
                        switch (m_list[m_condIndex].MPositionType)
                        {
                            case EnumPositionType.Fixed:
                                //没有内部循环
                                switch (index.MText)
                                {
                                    case EnumCollIndexText.L:
                                        if (listTVCV[1] - m_condVol >= EnumCollectorInfo.s_btjL)
                                        {
                                            m_logDescQue.Enqueue(ReadXamlCollection.C_CollOver);
                                            m_logOperQue.Enqueue(ReadXamlCollection.S_CollOver);
                                            JudgeCondNext(listTVCV, false);
                                        }
                                        break;
                                    case EnumCollIndexText.R:
                                        if (listTVCV[1] - m_condVol >= EnumCollectorInfo.s_btjR)
                                        {
                                            m_logDescQue.Enqueue(ReadXamlCollection.C_CollOver);
                                            m_logOperQue.Enqueue(ReadXamlCollection.S_CollOver);
                                            JudgeCondNext(listTVCV, false);
                                        }
                                        break;
                                }
                                break;
                            case EnumPositionType.Loop:
                                //有内部循环
                                {
                                    //计算理论当前收集口
                                    CollTextIndex currIndex = new CollTextIndex(index.MText, index.MIndex);
                                    CalCurr(ref currIndex);
                                    JudgeLoop(currIndex, listTVCV, listVal, listMinVal, listMaxVal, listSlope, listLastSlope);
                                }
                                break;
                        }
                        break;
                    case EnumNOIngFinish.PauseFirst:
                        //收集暂停
                        QueAddCond(new CollTextIndex(false), listTVCV[1], m_list[m_condIndex]);
                        m_condIndexNew = false;
                        break;
                    case EnumNOIngFinish.OverFirst:
                        //收集不满足,切到排序
                        QueAddCond(new CollTextIndex(false), listTVCV[1], m_list[m_condIndex]);
                        break;
                    case EnumNOIngFinish.Finish:
                        //收集结束
                        JudgeCondNext(listTVCV, true);
                        break;
                }
            }

            QueJudge(ref index, listTVCV[1]);

            return m_signal;
        }

        /// <summary>
        /// 获取日志信息
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="oper"></param>
        /// <returns></returns>
        public bool GetLogDescOper(ref string desc, ref string oper)
        {
            if (0 < m_logDescQue.Count)
            {
                desc = m_logDescQue.Dequeue();
                oper = m_logOperQue.Dequeue();

                if (desc.Equals(m_logDesc) && oper.Equals(m_logOper))
                {
                    return false;
                }
                else
                {
                    m_logDesc = desc;
                    m_logOper = oper;
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 停止收集
        /// </summary>
        public void JudgeFinish()
        {
            if (m_signal)
            {
                m_delayQue.Clear();
                m_logDescQue.Clear();
                m_logOperQue.Clear();

                m_signal = false;
            }
        }

        /// <summary>
        /// 初始化循环
        /// </summary>
        /// <param name="index"></param>
        /// <param name="listTVCV"></param>
        /// <param name="first"></param>
        private void InitLoop(CollTextIndex index, double[] listTVCV, bool first)
        {
            JudgeLoopNext(index, listTVCV, first);
        }

        /// <summary>
        /// 判断内部循环状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="listTVCV"></param>
        /// <param name="listVal"></param>
        /// <param name="listSlope"></param>
        private void JudgeLoop(CollTextIndex index, double[] listTVCV, List<double> listVal, List<double> listMinVal, List<double> listMaxVal, List<double> listSlope, List<double> listLastSlope)
        {
            switch (CollectionJudge.JudgeObjectMulti(ref m_loopIndexStatus1, ref m_loopIndexStatus2, listTVCV, listVal, listMinVal, listMaxVal, listSlope, listLastSlope, m_list[m_condIndex].MLoop, m_loopIndexVal1, ref m_loopIndexVal2))
            {
                case EnumNOIngFinish.NoFirst:
                    QueAddLoop(new CollTextIndex(false), listTVCV[1], m_list[m_condIndex]);
                    break;
                case EnumNOIngFinish.IngFirst:
                    QueAddLoop(m_currIndex, listTVCV[1], m_list[m_condIndex]);
                    m_loopVol = listTVCV[1];
                    break;
                case EnumNOIngFinish.Ing:
                    switch (index.MText)
                    {
                        case EnumCollIndexText.L:
                            if (listTVCV[1] - m_loopVol >= EnumCollectorInfo.s_btjL)
                            {
                                m_logDescQue.Enqueue(ReadXamlCollection.C_CollOver);
                                m_logOperQue.Enqueue(ReadXamlCollection.S_CollOver);
                                JudgeLoopNext(index, listTVCV, false);
                            }
                            break;
                        case EnumCollIndexText.R:
                            if (listTVCV[1] - m_loopVol >= EnumCollectorInfo.s_btjR)
                            {
                                m_logDescQue.Enqueue(ReadXamlCollection.C_CollOver);
                                m_logOperQue.Enqueue(ReadXamlCollection.S_CollOver);
                                JudgeLoopNext(index, listTVCV, false);
                            }
                            break;
                    }
                    break;
                case EnumNOIngFinish.PauseFirst:
                    QueAddLoop(new CollTextIndex(false), listTVCV[1], m_list[m_condIndex]);
                    break;
                case EnumNOIngFinish.OverFirst:
                    QueAddLoop(new CollTextIndex(false), listTVCV[1], m_list[m_condIndex]);
                    break;
                case EnumNOIngFinish.Finish:
                    JudgeLoopNext(index, listTVCV, false);
                    break;
            }
        }

        /// <summary>
        /// 切换到下一行条件
        /// </summary>
        /// <param name="listTVCV"></param>
        /// <param name="change"></param>
        private void JudgeCondNext(double[] listTVCV, bool change)
        {
            if (change)
            {
                m_condIndexStatus1 = EnumNOIngFinish.NULL;
                m_condIndexStatus2 = EnumNOIngFinish.NULL;
            }

            if (m_condIndex < m_list.Count - 1)//未执行完列表
            {
                m_condIndex++;
                m_condIndexNew = true;

                switch (m_list[m_condIndex].MCond.MObj1.MType)
                {
                    case 0:
                    case 1:
                    case 2:
                        m_condIndexVal1 = listTVCV[m_list[m_condIndex].MCond.MObj1.MType];
                        break;
                }

                switch (m_list[m_condIndex].MCond.MObj2.MType)
                {
                    case 0:
                    case 1:
                    case 2:
                        m_condIndexVal2 = listTVCV[m_list[m_condIndex].MCond.MObj2.MType];
                        break;
                }

                m_signal = true;//保持执行
            }
            else//执行完列表
            {
                QueAddLoop(new CollTextIndex(false), listTVCV[1], m_list[m_condIndex]);

                m_signal = false;//不再执行
            }
        }

        /// <summary>
        /// 切换到下一行循环
        /// </summary>
        /// <param name="index"></param>
        /// <param name="listTVCV"></param>
        /// <param name="first"></param>
        private void JudgeLoopNext(CollTextIndex index, double[] listTVCV, bool first)
        {
            m_loopIndexStatus1 = EnumNOIngFinish.NULL;
            m_loopIndexStatus2 = EnumNOIngFinish.NULL;

            switch (m_list[m_condIndex].MLoop.MObj1.MType)
            {
                case 0:
                case 1:
                case 2:
                    m_loopIndexVal1 = listTVCV[m_list[m_condIndex].MLoop.MObj1.MType];
                    break;
            }

            switch (m_list[m_condIndex].MLoop.MObj2.MType)
            {
                case 0:
                case 1:
                case 2:
                    m_loopIndexVal2 = listTVCV[m_list[m_condIndex].MLoop.MObj2.MType];
                    break;
            }

            if (first)
            {
                switch (m_list[m_condIndex].MPositionStart)
                {
                    case EnumPositionStart.Default:
                        m_currIndex = new CollTextIndex(index.MText, index.MIndex);
                        break;
                    case EnumPositionStart.Left:
                        m_currIndex = new CollTextIndex(EnumCollIndexText.L, m_list[m_condIndex].MStartIndex);
                        break;
                    case EnumPositionStart.Right:
                        m_currIndex = new CollTextIndex(EnumCollIndexText.R, m_list[m_condIndex].MStartIndex);
                        break;
                }
            }
            else
            {
                m_currIndex.MIndex++;
                switch (m_currIndex.MText)
                {
                    case EnumCollIndexText.L:
                        if (m_currIndex.MIndex > EnumCollectorInfo.CountL)//超出范围
                        {
                            m_currIndex.MIndex = 1;
                            m_currIndex.MText = EnumCollIndexText.R;
                        }
                        break;
                    case EnumCollIndexText.R:
                        if (m_currIndex.MIndex > EnumCollectorInfo.CountR)//超出范围
                        {
                            m_currIndex.MIndex = 1;
                            m_currIndex.MText = EnumCollIndexText.L;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 增加收集操作队列
        /// </summary>
        /// <param name="index"></param>
        /// <param name="vol"></param>
        /// <param name="item"></param>
        private void QueAddCond(CollTextIndex index, double vol, CollectionItem item)
        {
            QueueAdd(new CollectionCollectorDelay(index, vol, Communication.StaticSystemConfig.SSystemConfig.MListConfpHCdUV, item.MCond.MObj1.MType, item.MCond.MObj2.MType));
        }

        /// <summary>
        /// 增加循环操作队列
        /// </summary>
        /// <param name="index"></param>
        /// <param name="vol"></param>
        /// <param name="item"></param>
        private void QueAddLoop(CollTextIndex index, double vol, CollectionItem item)
        {
            QueueAdd(new CollectionCollectorDelay(index, vol, Communication.StaticSystemConfig.SSystemConfig.MListConfpHCdUV, item.MCond.MObj1.MType, item.MCond.MObj2.MType, item.MLoop.MObj1.MType, item.MLoop.MObj2.MType));
        }

        /// <summary>
        /// 增加操作队列
        /// </summary>
        /// <param name="item"></param>
        private void QueueAdd(CollectionCollectorDelay item)
        {
            if (-1 == item.m_mode)
            {
                //清空延时收集数据
                m_delayQue.Clear();
                m_delayQue.Enqueue(item);
            }
            else
            {
                m_delayQue.Enqueue(item);
            }

            m_logDescQue.Enqueue(ReadXamlCollection.C_CollMarkA);
            m_logOperQue.Enqueue(item.m_index.MStr);
        }

        /// <summary>
        /// 计算理论当前收集口
        /// </summary>
        /// <param name="index"></param>
        private void CalCurr(ref CollTextIndex index)
        {
            if (0 < m_delayQue.Count)
            {
                index = m_delayQue.Last().m_index;
            }
        }

        /// <summary>
        /// 计算理论下一个收集口
        /// </summary>
        /// <param name="index"></param>
        private void CalNext(ref CollTextIndex index)
        {
            CalCurr(ref index);

            index.MIndex += 1;

            switch (index.MText)
            {
                case EnumCollIndexText.L:
                    if (index.MIndex > EnumCollectorInfo.CountL)
                    {
                        index.MIndex = 1;
                        index.MText = EnumCollIndexText.R;
                    }
                    break;
                case EnumCollIndexText.R:
                    if (index.MIndex > EnumCollectorInfo.CountR)
                    {
                        index.MIndex = 1;
                        index.MText = EnumCollIndexText.L;
                    }
                    break;
            }
        }

        /// <summary>
        /// 执行操作队列
        /// </summary>
        /// <param name="index"></param>
        /// <param name="vol"></param>
        private void QueJudge(ref CollTextIndex index, double vol)
        {
            if (0 < m_delayQue.Count)
            {
                CollectionCollectorDelay temp = m_delayQue.Peek();
                if (-1 != temp.m_mode && Math.Round(vol - temp.m_vol, 2) < Communication.StaticSystemConfig.SSystemConfig.MListConfpHCdUV[temp.m_mode - 3].MVol)
                {
                    return;
                }

                if (-1 != temp.m_mode && !index.Equals(temp.m_index))
                {
                    m_logDescQue.Enqueue(ReadXamlCollection.C_CollDelay);
                    m_logOperQue.Enqueue(index.MStr + " ->" + temp.m_index.MStr);
                }

                if (temp.m_index.MStatus)
                {
                    index = temp.m_index;
                }
                else
                {
                    index.MStatus = false;
                }

                m_delayQue.Dequeue();
            }
        }
    }
}