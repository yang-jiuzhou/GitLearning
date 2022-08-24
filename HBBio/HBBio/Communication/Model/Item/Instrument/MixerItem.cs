using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class MixerItem : BaseInstrument
    {
        public bool m_onoffSet;        //(写)
        public bool m_onoffGet;        //(读)


        public MixerItem()
        {
            MConstNameList = Enum.GetNames(typeof(ENUMMixerName));
            MConstName = MConstNameList[0];
        }
    }
}
