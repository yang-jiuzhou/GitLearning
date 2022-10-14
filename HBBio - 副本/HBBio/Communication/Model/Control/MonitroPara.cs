using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    [Serializable]
    public class MonitroPara
    {
        public EnumJudge MJudge { get; set; }
        public string MName { get; set; }
        public double MMoreThan { get; set; }
        public double MLessThan { get; set; }
        public double MStabilityLength { get; set; }
        public EnumBase MStabilityUnit { get; set; }
    }
}
