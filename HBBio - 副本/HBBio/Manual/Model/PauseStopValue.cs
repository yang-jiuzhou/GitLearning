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
    public class PauseStopValue
    {
        public bool m_update = false;

        private bool m_signal = false;
        private double m_start = 0;
        private double m_remainder = 0;

        public double MLength { get; set; }
        public EnumBase MLengthUnit { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public PauseStopValue()
        {
            MLength = 0;
            MLengthUnit = EnumBase.T;
        }

        /// <summary>
        /// 返回副本
        /// </summary>
        /// <returns></returns>
        public PauseStopValue Clone()
        {
            PauseStopValue item = new PauseStopValue();
            item.MLength = MLength;
            item.MLengthUnit = MLengthUnit;

            return item;
        }

        /// <summary>
        /// 清除临时变量
        /// </summary>
        public void Clear()
        {
            m_signal = false;
        }

        /// <summary>
        /// 开始判断
        /// </summary>
        /// <param name="t"></param>
        /// <param name="v"></param>
        /// <param name="cv"></param>
        public void Init(double t, double v, double cv)
        {
            switch (MLengthUnit)
            {
                case EnumBase.T: m_start = t; break;
                case EnumBase.V: m_start = v; break;
                case EnumBase.CV: m_start = cv; break;
            }

            m_signal = true;
        }

        /// <summary>
        /// 判断是否完成
        /// </summary>
        /// <returns></returns>
        public EnumStatus Finish(double t, double v, double cv)
        {
            if (m_signal)
            {
                switch (MLengthUnit)
                {
                    case EnumBase.T:
                        m_remainder = m_start + MLength - t;
                        if (t >= m_start + MLength)
                        {
                            m_signal = false;
                            return EnumStatus.Over;
                        }
                        else
                        {
                            break;
                        }
                    case EnumBase.V:
                        m_remainder = m_start + MLength - v;
                        if (v >= m_start + MLength)
                        {
                            m_signal = false;
                            return EnumStatus.Over;
                        }
                        else
                        {
                            break;
                        }
                    case EnumBase.CV:
                        m_remainder = m_start + MLength - cv;
                        if (cv >= m_start + MLength)
                        {
                            m_signal = false;
                            return EnumStatus.Over;
                        }
                        else
                        {
                            break;
                        }
                }

                if (m_remainder < 0)
                {
                    m_remainder = 0;
                }
            }

            return EnumStatus.Ing;
        }

        /// <summary>
        /// 返回实时倒计信息
        /// </summary>
        /// <returns></returns>
        public string GetInfo()
        {
            if (m_signal && m_remainder > 0)
            {
                return m_remainder.ToString("f2") + EnumBaseInfo.NameList[(int)MLengthUnit];
            }
            else
            {
                return null;
            }
        }
    }
}
