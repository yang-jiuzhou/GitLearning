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
    public class PumpSystemValue
    {
        #region 字段 控制
        public bool m_update = false;

        public bool m_signal = false;
        private double m_start = 0;
        private double m_flowVol = 0;
        private double m_hold = 0;
        private double m_a = 0;
        private double m_b = 0;
        private double m_c = 0;
        private double m_d = 0;
        private Dispensing[] m_dispensingArr = new Dispensing[4];
        #endregion

        #region 属性 控制
        public double MFlowVol
        {
            get
            {
                return m_flowVol;
            }
        }
        public double MA
        {
            get
            {
                return m_a;
            }
        }
        public double MB
        {
            get
            {
                return m_b;
            }
        }
        public double MC
        {
            get
            {
                return m_c;
            }
        }
        public double MD
        {
            get
            {
                return m_d;
            }
        }
        #endregion

        #region 属性 显示
        public double MLength { get; set; }
        public EnumBase MLengthUnit { get; set; }
        public double MFlow { get; set; }
        public EnumFlowRate MFlowUnit { get; set; }
        public double MBS { get; set; }
        public double MBE { get; set; }
        public double MCS { get; set; }
        public double MCE { get; set; }
        public double MDS { get; set; }
        public double MDE { get; set; }
        public bool MEnablePT { get; set; }
        public double MControlPT { get; set; }
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        public PumpSystemValue()
        {
            MLength = 1;
            MFlow = 1;
        }

        /// <summary>
        /// 初始化
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

            switch (MFlowUnit)
            {
                case EnumFlowRate.MLMIN:
                    m_flowVol = MFlow;
                    break;
                case EnumFlowRate.CMH:
                    m_flowVol = MFlow * StaticValue.SLenToVol;
                    break;
            }

            m_signal = true;
        }

        public void SetIncremental(double p, double i, double d)
        {
            for (int k = 0; k < m_dispensingArr.Length; k++)
            {
                m_dispensingArr[k] = new Dispensing(p, i, d);
            }
        }

        /// <summary>
        /// 更新流速
        /// </summary>
        /// <param name="t"></param>
        /// <param name="v"></param>
        /// <param name="cv"></param>
        /// <returns></returns>
        public EnumStatus UpdateFlow(double t, double v, double cv)
        {
            if (m_signal)
            {
                switch (MLengthUnit)
                {
                    case EnumBase.T: m_hold = Math.Round(t - m_start, 2); break;
                    case EnumBase.V: m_hold = Math.Round(v - m_start, 2); break;
                    case EnumBase.CV: m_hold = Math.Round(cv - m_start, 2); break;
                }

                if (m_hold < MLength)
                {
                    m_b = (MBE - MBS) * m_hold / MLength + MBS;
                    m_c = (MCE - MCS) * m_hold / MLength + MCS;
                    m_d = (MDE - MDS) * m_hold / MLength + MDS;

                    return EnumStatus.Ing;
                }
                else
                {
                    m_b = MBE;
                    m_c = MCE;
                    m_d = MDE;

                    m_signal = false;
                    return EnumStatus.Over;
                }
            }

            return EnumStatus.Null;
        }

        /// <summary>
        /// 更新流速
        /// </summary>
        /// <param name="t"></param>
        /// <param name="v"></param>
        /// <param name="cv"></param>
        /// <returns></returns>
        public EnumStatus UpdateFlow(double ptA, double ptB, double ptC, double ptD, double ptTotal, double flowA, double flowB, double flowC, double flowD)
        {
            if (m_signal)
            {
                if (0 == ptA)
                {
                    m_dispensingArr[0].PTControlFlow(MControlPT, ptTotal, ref flowA, m_flowVol * (100 - MBS - MCS - MDS) / 100);
                }
                else
                {
                    m_dispensingArr[0].PTControlFlow(MControlPT, ptA, ref flowA, m_flowVol * (100 - MBS - MCS - MDS) / 100);
                }
                if (0 == ptB)
                {
                    m_dispensingArr[1].PTControlFlow(MControlPT, ptTotal, ref flowB, m_flowVol * MBS / 100);
                }
                else
                {
                    m_dispensingArr[1].PTControlFlow(MControlPT, ptB, ref flowB, m_flowVol * MBS / 100);
                }
                if (0 == ptC)
                {
                    m_dispensingArr[2].PTControlFlow(MControlPT, ptTotal, ref flowC, m_flowVol * MCS / 100);
                }
                else
                {
                    m_dispensingArr[2].PTControlFlow(MControlPT, ptC, ref flowC, m_flowVol * MCS / 100);
                }
                if (0 == ptD)
                {
                    m_dispensingArr[3].PTControlFlow(MControlPT, ptTotal, ref flowD, m_flowVol * MDS / 100);
                }
                else
                {
                    m_dispensingArr[3].PTControlFlow(MControlPT, ptD, ref flowD, m_flowVol * MDS / 100);
                }

                m_a = flowA / m_flowVol * 100;
                m_b = flowB / m_flowVol * 100;
                m_c = flowC / m_flowVol * 100;
                m_d = flowD / m_flowVol * 100;

                return EnumStatus.Ing;
            }

            return EnumStatus.Null;
        }

        /// <summary>
        /// 清除临时变量
        /// </summary>
        public void Clear()
        {
            m_signal = false;

            MLength = 1;
            MLengthUnit = EnumBase.T;
            MFlow = 0;
            MFlowUnit = EnumFlowRate.MLMIN;
            MBS = 0;
            MBE = 0;
            MCS = 0;
            MCE = 0;
            MDS = 0;
            MDE = 0;
        }
    }
}
