using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class SamplerItem : BaseInstrument
    {
        public int m_valveSet = 0;
        public int m_valveGet = 1;
        public AutoResetEvent MAre { get; set; }

        public SamplerItem()
        {
            MConstNameList = Enum.GetNames(typeof(ENUMSamplerName));
            MConstName = MConstNameList[0];
        }
    }
}
