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
	 * ClassName: CollectionValue
	 * Description: 组分收集
	 * Version: 1.0
	 * Create:  2021/03/17
	 * Author:  yangjiuzhou
	 * Company: jshanbon
	 **/
    [Serializable]
    public class CollectionValve
    {
        #region 字段 控制
        public bool m_update = false;

        private bool m_signal = false;                                      //是否启用     
        private int m_condIndex = -1;                                       //收集序号
        private bool m_condIndexNew = true;
        private int m_currIndex = 0;
        private double m_condIndexVal1 = 0;                                 //条件1的起始点
        private double m_condIndexVal2 = 0;                                 //条件2的起始点
        private EnumNOIngFinish m_condIndexStatus1 = EnumNOIngFinish.NULL;  //条件1状态
        private EnumNOIngFinish m_condIndexStatus2 = EnumNOIngFinish.NULL;  //条件2状态
        private double m_loopIndexVal1 = 0;                                 //当前循环的起始点1
        private double m_loopIndexVal2 = 0;                                 //当前循环的起始点2
        private EnumNOIngFinish m_loopIndexStatus1 = EnumNOIngFinish.NULL;  //循环1状态
        private EnumNOIngFinish m_loopIndexStatus2 = EnumNOIngFinish.NULL;  //循环2状态

        private Queue<CollectionValveDelay> m_delayQue = new Queue<CollectionValveDelay>();        //延迟信息列表
        private Queue<string> m_logDescQue = new Queue<string>();
        private Queue<string> m_logOperQue = new Queue<string>();
        private string m_logDesc = "";
        private string m_logOper = "";

        private static int m_lastIndex = 0;                                 //上一次收集口
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
        public void Init(ref int index, double[] listTVCV)
        {
            m_condIndex = -1;
			m_condIndexNew = true;

            m_logDesc = "";
            m_logOper = "";

            if (0 == m_list.Count)
            {
                JudgeFinish();
                index = 0;
            }
            else
            {
                m_currIndex = m_lastIndex;
                JudgeCondNext(listTVCV);
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
        public bool JudgeCondition(ref int index, double[] listTVCV, List<double> listVal, List<double> listSlope)
        {
            if (m_signal)
            {
                //判断条件1
                switch (CollectionJudge.JudgeObjectMulti(ref m_condIndexStatus1, ref m_condIndexStatus2, listTVCV, listVal, listSlope, m_list[m_condIndex].MCond, m_condIndexVal1, ref m_condIndexVal2))
                {
                    case EnumNOIngFinish.NoFirst:
                        //收集未满足，切到排废
                        QueAddCond(0, listTVCV[1], m_list[m_condIndex]);
                        break;
                    case EnumNOIngFinish.IngFirst:
                        //收集开始，切到收集口
                        switch (m_list[m_condIndex].MPositionType)
                        {
                            case EnumPositionType.Fixed:
                                //没有内部循环                          
                                switch (m_list[m_condIndex].MPositionStart)
                                {
                                    case EnumPositionStart.Default:
                                        {
                                            //计算下一个收集口
                                            int currIndex = m_lastIndex;
                                            CalNext(ref currIndex);
                                        	QueAddCond(currIndex, listTVCV[1], m_list[m_condIndex]);
                                        }
                                        break;
                                    case EnumPositionStart.Out:
                                        QueAddCond(m_list[m_condIndex].MStartIndex, listTVCV[1], m_list[m_condIndex]);
                                        break;
                                }
                                break;
                            case EnumPositionType.Loop:
                                //有内部循环
                                {
                                    //计算下一个收集口
                                    int currIndex = m_lastIndex;
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
                                break;
                            case EnumPositionType.Loop:
                                //有内部循环
                                {
                                    //计算理论当前收集口
                                    int currIndex = m_lastIndex;
                                    CalCurr(ref currIndex);
                                	JudgeLoop(currIndex, listTVCV, listVal, listSlope);
                                }
                                break;
                        }
                        break;
                    case EnumNOIngFinish.PauseFirst:
                        //收集暂停
                        QueAddCond(0, listTVCV[1], m_list[m_condIndex]);
                        m_condIndexNew = false;
                        break;
                    case EnumNOIngFinish.OverFirst:
                        //收集不满足,切到排序
                        QueAddCond(0, listTVCV[1], m_list[m_condIndex]);
                        break;
                    case EnumNOIngFinish.Finish:
                        //收集结束
                        JudgeCondNext(listTVCV);
                        break;
                }
            }

            int tmp = QueJudge(ref index, listTVCV[1]);
            if (0 != tmp)
            {
                m_lastIndex = tmp;
            }

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
                QueueAdd(new CollectionValveDelay());

                m_signal = false;
            }
        }

        /// <summary>
        /// 初始化循环
        /// </summary>
        /// <param name="index"></param>
        /// <param name="listTVCV"></param>
        /// <param name="first"></param>
        private void InitLoop(int index, double[] listTVCV, bool first)
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
        private void JudgeLoop(int index, double[] listTVCV, List<double> listVal, List<double> listSlope)
        {
            switch (CollectionJudge.JudgeObjectMulti(ref m_loopIndexStatus1, ref m_loopIndexStatus2, listTVCV, listVal, listSlope, m_list[m_condIndex].MLoop, m_loopIndexVal1, ref m_loopIndexVal2))
            {
                case EnumNOIngFinish.NoFirst:
                    QueAddLoop(0, listTVCV[1], m_list[m_condIndex]);
                    break;
                case EnumNOIngFinish.IngFirst:
                    QueAddLoop(m_currIndex, listTVCV[1], m_list[m_condIndex]);
                    break;
                case EnumNOIngFinish.Ing:
                    break;
                case EnumNOIngFinish.PauseFirst:
                    QueAddLoop(0, listTVCV[1], m_list[m_condIndex]);
                    break;
                case EnumNOIngFinish.OverFirst:
                    QueAddLoop(0, listTVCV[1], m_list[m_condIndex]);
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
        private void JudgeCondNext(double[] listTVCV)
        {
            m_condIndexStatus1 = EnumNOIngFinish.NULL;
            m_condIndexStatus2 = EnumNOIngFinish.NULL;

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
                QueAddLoop(0, listTVCV[1], m_list[m_condIndex]);

                m_signal = false;//不再执行
            }
        }

        /// <summary>
        /// 切换到下一行循环
        /// </summary>
        /// <param name="index"></param>
        /// <param name="listTVCV"></param>
        /// <param name="first"></param>
        private void JudgeLoopNext(int index, double[] listTVCV, bool first)
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
                        m_currIndex = index;
                        break;
                    case EnumPositionStart.Out:
                        m_currIndex = m_list[m_condIndex].MStartIndex - 1;
                        break;
                }
            }
            else
            {
                m_currIndex++;
                if (m_currIndex >= EnumOutInfo.Count)//超出范围
                {
                    m_currIndex = 1;
                }
            }
        }

        /// <summary>
        /// 增加收集操作队列
        /// </summary>
        /// <param name="index"></param>
        /// <param name="vol"></param>
        /// <param name="item"></param>
        private void QueAddCond(int index, double vol, CollectionItem item)
        {
            QueueAdd(new CollectionValveDelay(index, vol, Communication.StaticSystemConfig.SSystemConfig.MListConfpHCdUV, item.MCond.MObj1.MType, item.MCond.MObj2.MType));
        }

        /// <summary>
        /// 增加循环操作队列
        /// </summary>
        /// <param name="index"></param>
        /// <param name="vol"></param>
        /// <param name="item"></param>
        private void QueAddLoop(int index, double vol, CollectionItem item)
        {
            QueueAdd(new CollectionValveDelay(index, vol, Communication.StaticSystemConfig.SSystemConfig.MListConfpHCdUV, item.MCond.MObj1.MType, item.MCond.MObj2.MType, item.MLoop.MObj1.MType, item.MLoop.MObj2.MType));
        }

        /// <summary>
        /// 增加操作队列
        /// </summary>
        /// <param name="item"></param>
        private void QueueAdd(CollectionValveDelay item)
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
            m_logOperQue.Enqueue(EnumOutInfo.NameList[item.m_outIndex]);
        }

        /// <summary>
        /// 计算理论当前收集口
        /// </summary>
        /// <param name="index"></param>
        private void CalCurr(ref int index)
        {
            if (0 < m_delayQue.Count)
            {
                index = m_delayQue.Last().m_outIndex;
            }
        }

        /// <summary>
        /// 计算理论下一个收集口
        /// </summary>
        /// <param name="index"></param>
        private void CalNext(ref int index)
        {
            CalCurr(ref index);

            if (++index >= EnumOutInfo.Count)
            {
                index = 1;//0是废液
            }
        }

        /// <summary>
        /// 执行操作队列
        /// </summary>
        /// <param name="index"></param>
        /// <param name="vol"></param>
        private int QueJudge(ref int index, double vol)
        {
            int result = 0;
            if (0 < m_delayQue.Count)
            {
                result = m_delayQue.Last().m_outIndex;

                CollectionValveDelay temp = m_delayQue.Peek();
                if (-1 != temp.m_mode && vol - temp.m_vol < Communication.StaticSystemConfig.SSystemConfig.MListConfpHCdUV[temp.m_mode - 3].MVol)
                {
                    return result;
                }

                if (-1 != temp.m_mode && index != temp.m_outIndex)
                {
                    m_logDescQue.Enqueue(ReadXamlCollection.C_CollDelay);
                    m_logOperQue.Enqueue(EnumOutInfo.NameList[index] + " ->" + EnumOutInfo.NameList[temp.m_outIndex]);
                }

                index = temp.m_outIndex;

                m_delayQue.Dequeue();
            }

            return result;
        }
    }
}
