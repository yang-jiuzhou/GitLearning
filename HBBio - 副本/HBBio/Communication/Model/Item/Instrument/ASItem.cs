using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class ASItem : BaseInstrument
    {
        public ENUMASName m_name = ENUMASName.AS01;
        public double m_sizeGet;        //(读)

        public ASItem()
        {
            MConstNameList = Enum.GetNames(typeof(ENUMASName));
            MConstName = MConstNameList[0];
        }
    }
}