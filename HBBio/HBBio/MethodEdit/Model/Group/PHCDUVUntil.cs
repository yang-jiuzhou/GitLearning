using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: PHCDUVUntil
     * Description: PHCDUV满足条件
     * Version: 1.0
     * Create:  2020/05/27
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    [Serializable]
    public class PHCDUVUntil : BaseGroup
    {
        public string MHeaderText { get; set; }
        public EnumPHCDUVUntil MUntilType { get; set; }
        public BaseTVCV MTotalTVCV { get; set; }
        public int MMonitorIndex { get; set; }
        public EnumJudge MJudgeIndex { get; set; }
        public double MMoreThan { get; set; }
        public double MLessThan { get; set; }
        public double MStabilityTime { get; set; }
        public BaseTVCV MMaxTVCV { get; set; }

        private bool m_judgeFlag = false;       //非存储，开始计时的标志
        private double m_judgeStart = 0;        //非存储，开始计时


        /// <summary>
        /// 构造函数
        /// </summary>
        public PHCDUVUntil()
        {
            MType = EnumGroupType.PHCDUVUntil;

            MHeaderText = "";
            MTotalTVCV = new BaseTVCV();
            MMaxTVCV = new BaseTVCV();
        }

        /// <summary>
        /// 设置判断开始
        /// </summary>
        public void JudgeInit()
        {
            m_judgeFlag = false;
        }

        /// <summary>
        /// 实时判断是否结束
        /// </summary>
        /// <param name="val"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool JudgeFinish(double val, double time)
        {
            bool result = false;

            switch (MJudgeIndex)
            {
                case EnumJudge.Stable:
                    if (val >= MMoreThan && val <= MLessThan)
                    {
                        if (m_judgeFlag)
                        {
                            if ((time - m_judgeStart) >= MStabilityTime)
                            {
                                result = true;
                            }
                        }
                        else
                        {
                            m_judgeFlag = true;
                            m_judgeStart = time;
                        }
                    }
                    else
                    {
                        m_judgeFlag = false;
                    }
                    break;
                case EnumJudge.MoreThan:
                    if (val >= MMoreThan)
                    {
                        if (m_judgeFlag)
                        {
                            if ((time - m_judgeStart) >= MStabilityTime)
                            {
                                result = true;
                            }
                        }
                        else
                        {
                            m_judgeFlag = true;
                            m_judgeStart = time;
                        }
                    }
                    else
                    {
                        m_judgeFlag = false;
                    }
                    break;
                case EnumJudge.LessThan:
                    if (val <= MLessThan)
                    {
                        if (m_judgeFlag)
                        {
                            if ((time - m_judgeStart) >= MStabilityTime)
                            {
                                result = true;
                            }
                        }
                        else
                        {
                            m_judgeFlag = true;
                            m_judgeStart = time;
                        }
                    }
                    else
                    {
                        m_judgeFlag = false;
                    }
                    break;
            }

            return result;
        }
    }
}
