using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    [Serializable]
    public class ASTimeJudge
    {
        private double m_TVCV = 0;
        private EnumBase m_unit = EnumBase.T;
        private double m_startTVCV = 0;

        private bool m_start = false;
        public bool MStart 
        { 
            get
            {
                return m_start;
            }
        }


        public void Start(double length, EnumBase unit, double t, double v, double cv)
        {
            m_start = true;

            m_TVCV = length;
            m_unit = unit;

            switch (m_unit)
            {
                case EnumBase.T:
                    m_startTVCV = t;
                    break;
                case EnumBase.V:
                    m_startTVCV = v;
                    break;
                case EnumBase.CV:
                    m_startTVCV = cv;
                    break;
            }
        }

        public bool Finish(double length, EnumBase unit, double t, double v, double cv)
        {
            if (m_TVCV != length || m_unit != unit)
            {
                Start(length, unit, t, v, cv);
            }

            switch (m_unit)
            {
                case EnumBase.V:
                    if (v - m_startTVCV >= m_TVCV)
                    {
                        m_start = false;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case EnumBase.CV:
                    if (cv - m_startTVCV >= m_TVCV)
                    {
                        m_start = false;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    if (t - m_startTVCV >= m_TVCV)
                    {
                        m_start = false;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
        }
    }
}
