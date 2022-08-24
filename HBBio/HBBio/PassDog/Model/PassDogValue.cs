using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.PassDog
{
    public class PassDogValue
    {
        public string MHB { get; set; }
        public string MName { get; set; }
        public string MMode { get; set; }
        public string MSN { get; set; }
        public string MInfo
        {
            get
            {
                return MHB + "-" + MName + "-" + MMode + "-" + MSN;
            }
        }


        public PassDogValue()
        {
            MHB = "HB";
            MName = "Bio-LabChrom";
            MMode = "XXX";
            MSN = "";
        }
    }
}
