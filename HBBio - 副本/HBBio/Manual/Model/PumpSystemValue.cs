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
        private double m_b = 0;
        private double m_c = 0;
        private double m_d = 0;
        #endregion

        #region 属性 控制
        public double MFlowVol
        {
            get
            {
                return m_flowVol;
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
