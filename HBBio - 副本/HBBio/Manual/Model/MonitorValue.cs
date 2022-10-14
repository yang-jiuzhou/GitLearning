using HBBio.Communication;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Manual
{
    [Serializable]
    public class MonitorValue
    {
        public bool m_update = false;

        private bool m_signal = false;
        private int m_index = 0;
        private bool m_judgeFlag = false;
        private double m_judgeStart = 0;

        public EnumMonitorActionManual MAction { get; set; }
        public MonitroPara MValue { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public MonitorValue()
        {
            MValue = new MonitroPara();
        }

        /// <summary>
        /// 返回副本
        /// </summary>
        /// <returns></returns>
        public MonitorValue Clone()
        {
            return Share.DeepCopy.DeepCopyByXml(this);
        }

        /// <summary>
        /// 清除临时变量
        /// </summary>
        public void Clear()
        {
            MAction = EnumMonitorActionManual.Ignore;
        }

        /// <summary>
        /// 开始判断
        /// </summary>
        public void Init(string[] nameList)
        {
            m_index = nameList.ToList().IndexOf(MValue.MName);
            m_judgeFlag = false;
            m_signal = true;
        }

        /// <summary>
        /// 判断是否完成
        /// </summary>
        /// <param name="listVal"></param>
        /// <param name="t"></param>
        /// <param name="v"></param>
        /// <param name="cv"></param>
        /// <returns></returns>
        public EnumStatus Finish(List<double> listVal, double t, double v, double cv)
        {
            if (m_signal)
            {
                switch (MValue.MStabilityUnit)
                {
                    case EnumBase.T:
                        return Finish(listVal[m_index], t);
                    case EnumBase.V:
                        return Finish(listVal[m_index], v);
                    case EnumBase.CV:
                        return Finish(listVal[m_index], cv);
                }
            }

            return EnumStatus.Null;
        }

        /// <summary>
        /// 实时判断是否结束
        /// </summary>
        /// <param name="val"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private EnumStatus Finish(double val, double tvcv)
        {
            switch (MValue.MJudge)
            {
                case EnumJudge.Stable:
                    if (val >= MValue.MMoreThan && val <= MValue.MLessThan)
                    {
                        if (m_judgeFlag)
                        {
                            if ((tvcv - m_judgeStart) >= MValue.MStabilityLength)
                            {
                                m_signal = false;
                                return EnumStatus.Over;
                            }
                        }
                        else
                        {
                            m_judgeFlag = true;
                            m_judgeStart = tvcv;
                        }
                    }
                    else
                    {
                        m_judgeFlag = false;
                    }
                    break;
                case EnumJudge.MoreThan:
                    if (val >= MValue.MMoreThan)
                    {
                        if (m_judgeFlag)
                        {
                            if ((tvcv - m_judgeStart) >= MValue.MStabilityLength)
                            {
                                m_signal = false;
                                return EnumStatus.Over;
                            }
                        }
                        else
                        {
                            m_judgeFlag = true;
                            m_judgeStart = tvcv;
                        }
                    }
                    else
                    {
                        m_judgeFlag = false;
                    }
                    break;
                case EnumJudge.LessThan:
                    if (val <= MValue.MLessThan)
                    {
                        if (m_judgeFlag)
                        {
                            if ((tvcv - m_judgeStart) >= MValue.MStabilityLength)
                            {
                                m_signal = false;
                                return EnumStatus.Over;
                            }
                        }
                        else
                        {
                            m_judgeFlag = true;
                            m_judgeStart = tvcv;
                        }
                    }
                    else
                    {
                        m_judgeFlag = false;
                    }
                    break;
            }

            return EnumStatus.Ing;
        }
    }
}
