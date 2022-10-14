using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class CDItem : BaseInstrument
    {
        public double m_CdGet;        //(读)
        public double m_timeGet;
        private Queue<double> m_smooth = new Queue<double>();


        public CDItem()
        {
            MConstNameList = Enum.GetNames(typeof(ENUMCDName));
            MConstName = MConstNameList[0];
        }


        /// <summary>
        /// 返回列表
        /// </summary>
        /// <returns></returns>
        public override List<Signal> CreateSignalList()
        {
            List<Signal> result = new List<Signal>();

            result.Add(new Signal(MConstName, MDlyName, DlyBase.SC_CDUNIT, 0, StaticValue.s_maxCD, true, true));

            return result;
        }

        public void UpdateValue(double val)
        {
            m_smooth.Enqueue(val);
            if (m_smooth.Count > 5)
            {
                m_smooth.Dequeue();
            }
            m_CdGet = Math.Round(m_smooth.Average(), 3);
        }
    }
}