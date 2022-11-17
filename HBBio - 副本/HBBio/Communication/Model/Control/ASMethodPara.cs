using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    [Serializable]
    public class ASMethodPara
    {
        public string MHeader
        {
            get
            {
                return MName.ToString();
            }
            set
            {

            }
        }
        public ENUMASName MName { get; set; }
        public EnumMonitorActionMethod MAction { get; set; }
        public double MLength { get; set; }
        public EnumBase MUnit { get; set; }

        public bool m_update = false;
        public bool m_signal = false;
        private int m_last = -1;
        private bool m_change = false;
        private ASTimeJudge m_judge = null;


        /// <summary>
        /// 构造函数
        /// </summary>
        public ASMethodPara()
        {
            MName = ENUMASName.AS01;
            MAction = EnumMonitorActionMethod.Ignore;
            MLength = 0;
            MUnit = EnumBase.T;
        }

        public bool Compare(ASMethodPara item)
        {
            if (null == item)
            {
                return false;
            }

            if (MName != item.MName
                || MAction != item.MAction
                || MLength != item.MLength
                || MUnit != item.MUnit)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 深度拷贝
        /// </summary>
        /// <param name="item"></param>
        public void DeepCopy(ASMethodPara item)
        {
            MName = item.MName;
            MAction = item.MAction;
            MLength = item.MLength;
            MUnit = item.MUnit;
        }

        /// <summary>
        /// 更新默认值
        /// </summary>
        /// <param name="length"></param>
        /// <param name="unit"></param>
        public void UpdateStart(double length, EnumBase unit)
        {
            if (!m_signal)
            {
                MAction = EnumMonitorActionMethod.Ignore;
                MLength = length;
                MUnit = unit;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            m_signal = true;
            m_last = -1;
        }

        /// <summary>
        /// 当前有气泡的反馈
        /// </summary>
        /// <param name="t"></param>
        /// <param name="v"></param>
        /// <param name="cv"></param>
        /// <param name="last">0表示已经持续的气泡，1表示开始有气泡但不需要切阀，2表示开始有气泡且需要切阀</param>
        /// <returns></returns>
        public int JudgeASYes(double t, double v, double cv, int last)
        {
            if (0 == last)
            {
                if (m_change)
                {
                    return 0;
                }
                else
                {
                    m_judge = new ASTimeJudge();
                    m_change = true;
                    return 1;
                }
            }
            else
            {
                m_judge = new ASTimeJudge();
                m_change = true;
                m_last = last;
                return 2;
            }
        }

        public int JudgeASNo(double t, double v, double cv, ref int last)
        {
            if (null == m_judge)
            {
                return 0;
            }

            if (m_change)
            {
                m_change = false;
                m_judge.Start(MLength, MUnit, t, v, cv);
                return 1;
            }
            else
            {
                if (m_judge.MStart)
                {
                    if (m_judge.Finish(MLength, MUnit, t, v, cv))
                    {
                        m_judge = null;
                        if (0 == last)
                        {
                            last = m_last;
                            return 2;
                        }
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// 清除临时变量
        /// </summary>
        public void Clear()
        {
            m_signal = false;
            m_last = -1;
        }
    }
}
