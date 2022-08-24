using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    public class MethodBaseValue
    {
        public bool MChangeFlowRateUnit { get; set; }
        public bool MChangeBaseUnit { get; set; }
        public bool MChangeFlowRate { get; set; }
        public bool MChangeValve { get; set; }


        public EnumBase MEnumBaseOld { get; set; }
        public EnumBase MEnumBaseNew { get; set; }
        public EnumFlowRate MEnumFlowRateOld { get; set; }
        public EnumFlowRate MEnumFlowRateNew { get; set; }
        public double MFlowRate { get; set; }
        public double MMaxFlowRate { get; set; }
        public double MFlowVol 
        { 
            get
            {
                if (EnumFlowRate.MLMIN == MEnumFlowRateNew)
                {
                    return MFlowRate;
                }
                else
                {
                    return Math.Round(MFlowRate * MColumnArea / 60, 2);
                }
            }
        }
        public double MColumnVol { get; set; }
        public double MColumnArea { get; set; }
        public string MBaseStr { get; set; }
        public string MBaseUnitStr { get; set; }
        public string MFlowRateUnitStr { get; set; }

        /// <summary>
        /// 入口阀A
        /// </summary>
        public int MInA { get; set; }
        /// <summary>
        /// 入口阀B
        /// </summary>
        public int MInB { get; set; }
        /// <summary>
        /// 入口阀C
        /// </summary>
        public int MInC { get; set; }
        /// <summary>
        /// 入口阀D
        /// </summary>
        public int MInD { get; set; }
        /// <summary>
        /// 旁通阀
        /// </summary>
        public int MBPV { get; set; }
    }
}
