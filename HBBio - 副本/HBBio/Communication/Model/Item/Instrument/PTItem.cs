using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class PTItem : BaseInstrument
    {
        public double m_pressGet;       //实时压力(读)
        public double m_maxSet;         //最大压力(写)
        public double m_maxGet;         //最大压力(读)
        public double m_minSet;         //最小压力(写)
        public double m_minGet;         //最小压力(读)


        public PTItem()
        {
            MConstNameList = Enum.GetNames(typeof(ENUMPTName));
            MConstName = MConstNameList[0];
        }

        /// <summary>
        /// 返回列表
        /// </summary>
        /// <returns></returns>
        public override List<Signal> CreateSignalList()
        {
            List<Signal> result = new List<Signal>();

            result.Add(new Signal(MConstName, MDlyName, DlyBase.SC_PTUNITMPA, 0, StaticValue.s_maxPT, true, true));

            if (MConstName.Equals(ENUMPTName.PTColumnBack.ToString()))
            {
                result.Add(new Signal("PT_Delta", "PT_Delta", DlyBase.SC_PTUNITMPA, 0, StaticValue.s_maxPT, true, true));//柱压差
            }

            return result;
        }
    }
}
