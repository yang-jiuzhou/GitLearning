using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class MonitroParaVM
    {
        #region 属性
        public MonitroPara MItem { get; set; }

        public EnumJudge MJudge
        {
            get
            {
                return MItem.MJudge;
            }
            set
            {
                MItem.MJudge = value;
            }
        }
        public string MName
        {
            get
            {
                return MItem.MName;
            }
            set
            {
                MItem.MName = value;
            }
        }
        public double MMoreThan
        {
            get
            {
                return MItem.MMoreThan;
            }
            set
            {
                MItem.MMoreThan = value;
            }
        }
        public double MLessThan
        {
            get
            {
                return MItem.MLessThan;
            }
            set
            {
                MItem.MLessThan = value;
            }
        }
        public double MStabilityLength
        {
            get
            {
                return MItem.MStabilityLength;
            }
            set
            {
                MItem.MStabilityLength = value;
            }
        }
        public EnumBase MStabilityUnit
        {
            get
            {
                return MItem.MStabilityUnit;
            }
            set
            {
                MItem.MStabilityUnit = value;
            }
        }

        public string MUnit
        {
            get
            {
                if (MName.Contains("pH"))
                {
                    return DlyBase.SC_PHUNIT + "[" + StaticValue.s_minPH + "-" + StaticValue.s_maxPH + "]";
                }
                else if (MName.Contains("Cd"))
                {
                    return DlyBase.SC_CDUNIT + "[" + StaticValue.s_minCD + "-" + StaticValue.s_maxCD + "]";
                }
                else
                {
                    return DlyBase.SC_UVABSUNIT + "[" + StaticValue.s_minUV + "-" + StaticValue.s_maxUV + "]";
                }
            }
        }
        public double MMin
        {
            get
            {
                if (MName.Contains("pH"))
                {
                    return StaticValue.s_minPH;
                }
                else if (MName.Contains("Cd"))
                {
                    return StaticValue.s_minCD;
                }
                else
                {
                    return StaticValue.s_minUV;
                }
            }
        }
        public double MMax
        {
            get
            {
                if (MName.Contains("pH"))
                {
                    return StaticValue.s_maxPH;
                }
                else if (MName.Contains("Cd"))
                {
                    return StaticValue.s_maxCD;
                }
                else
                {
                    return StaticValue.s_maxUV;
                }
            }
        }
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="item"></param>
        public MonitroParaVM(MonitroPara item)
        {
            MItem = item;
        }
    }
}
