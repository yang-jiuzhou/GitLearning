using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class ValveItem : BaseInstrument
    {
        private int m_valveSet = 0;
        private int m_valveGet = 1;
        public string[] m_enumNames = new string[] { "null" };       //阀枚举列表

        public int MValveSet
        {
            get
            {
                return m_valveSet;
            }
            set
            {
                if (value >= m_enumNames.Length)
                {
                    return;
                }

                m_valveSet = value;

                if (null != MAre)
                {
                    MAre.Set();
                }
            }
        }
        public int MValveGet
        {
            get
            {
                return m_valveGet;
            }
            set
            {
                if (m_valveGet == value)
                {
                    return;
                }

                if (value >= m_enumNames.Length)
                {
                    return;
                }

                m_valveGet = value;
                MValveGetStr = null;
            }
        }
        public string MValveSetStr
        {
            get
            {
                if (-1 != m_valveSet && m_valveSet < m_enumNames.Length)
                {
                    return m_enumNames[m_valveSet];
                }
                else
                {
                    return "";
                }
            }
        }
        public string MValveGetStr
        {
            get
            {
                if (-1 != m_valveGet && m_valveGet < m_enumNames.Length)
                {
                    return m_enumNames[m_valveGet];
                }
                else
                {
                    return "";
                }
            }
            set
            {
                OnPropertyChanged("MValveGetStr");
            }
        }
        public AutoResetEvent MAre { get; set; }

        public ValveItem()
        {
            MConstNameList = Enum.GetNames(typeof(ENUMValveName));
            MConstName = MConstNameList[0];
        }
    }
}
