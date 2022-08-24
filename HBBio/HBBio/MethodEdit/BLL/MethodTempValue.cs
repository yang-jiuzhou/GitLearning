using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    public class MethodTempValue
    {
        public bool MChange { get; set; }

        public double MFlowSystem { get; set; }
        public double MPerB { get; set; }
        public double MPerC { get; set; }
        public double MPerD { get; set; }
        public double MFlowSample { get; set; }

        public double MFlow
        {
            get
            {
                if (MFlowSample > 0)
                {
                    return MFlowSample;
                }
                else
                {
                    return MFlowSystem;
                }
            }
        }


        public void Clear()
        {
            MChange = false;

            MFlowSystem = 0;
            MPerB = 0;
            MPerC = 0;
            MPerD = 0;
            MFlowSample = 0;
        }
    }
}
