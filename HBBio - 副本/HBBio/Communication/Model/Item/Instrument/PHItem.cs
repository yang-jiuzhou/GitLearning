using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class PHItem : BaseInstrument
    {
        public double m_pHGet;        //(读)
        public double m_timeGet;


        public PHItem()
        {
            MConstNameList = Enum.GetNames(typeof(ENUMPHName));
            MConstName = MConstNameList[0];
        }

        /// <summary>
        /// 返回列表
        /// </summary>
        /// <returns></returns>
        public override List<Signal> CreateSignalList()
        {
            List<Signal> result = new List<Signal>();

            result.Add(new Signal(MConstName, MDlyName, DlyBase.SC_PHUNIT, 0, StaticValue.s_maxPH, true, true));

            return result;
        }
    }
}