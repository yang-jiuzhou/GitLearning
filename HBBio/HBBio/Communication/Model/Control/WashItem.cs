using HBBio.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class WashPara
    {
        public ENUMPumpName MPump { get; set; }
        public ENUMValveName MValve { get; set; }
        public int MValveIndex { get; set; }

        public WashPara(ENUMPumpName pump, ENUMValveName valve, int valveIndex)
        {
            MPump = pump;
            MValve = valve;
            MValveIndex = valveIndex;
        }
    }
}
