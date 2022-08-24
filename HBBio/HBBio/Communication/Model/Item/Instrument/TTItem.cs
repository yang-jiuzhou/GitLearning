using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class TTItem : BaseInstrument
    {
        public double m_tempGet;        //(读)


        public TTItem()
        {
            MConstNameList = Enum.GetNames(typeof(ENUMTTName));
            MConstName = MConstNameList[0];
        }

        /// <summary>
        /// 返回列表
        /// </summary>
        /// <returns></returns>
        public override List<Signal> CreateSignalList()
        {
            List<Signal> result = new List<Signal>();

            result.Add(new Signal(MConstName, MDlyName, DlyBase.SC_TEMPUNIT, 0, 100, true, true));

            return result;
        }
    }
}