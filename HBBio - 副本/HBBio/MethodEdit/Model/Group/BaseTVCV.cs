using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: BaseTVCV
     * Description: 
     * Version: 1.0
     * Create:  2020/05/27
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    [Serializable]
    public class BaseTVCV : BaseGroup
    {
        public string MHeaderText { get; set; }
        /// <summary>
        /// 基本单位
        /// </summary>
        public EnumBase MEnumBase { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public double MT { get; set; }
        /// <summary>
        /// 体积
        /// </summary>
        public double MV { get; set; }
        /// <summary>
        /// 柱体积 
        /// </summary>
        public double MCV { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public double MTVCV
        {
            get
            {
                switch (MEnumBase)
                {
                    case EnumBase.T: 
                        return MT;
                    case EnumBase.V: 
                        return MV;
                    default: 
                        return MCV;
                }
            }
            set
            {
                double newVal= Math.Round(value, 2);
                switch (MEnumBase)
                {
                    case EnumBase.T:
                        MT = newVal;
                        break;
                    case EnumBase.V:
                        MV = newVal;
                        break;
                    default:
                        MCV = newVal;
                        break;
                }
            }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseTVCV()
        {
            MType = EnumGroupType.TVCV;

            MHeaderText = "";
            MEnumBase = EnumBase.T;
            MT = 1;
            MV = 1;
            MCV = 1;
        }

        public void Init(EnumBase enumBase, double flowVol, double columnVol)
        {
            MEnumBase = enumBase;

            switch (enumBase)
            {
                case EnumBase.T:
                    MV = MT * flowVol;
                    MCV = MV / columnVol;
                    break;
                case EnumBase.V:
                    if (0 == flowVol)
                    {
                        MV = 0;
                        MCV = 0;
                    }
                    else
                    {
                        MT = MV / flowVol;
                        MCV = MV / columnVol;
                    }
                    break;
                case EnumBase.CV:
                    if (0 == flowVol)
                    {
                        MV = 0;
                        MCV = 0;
                    }
                    else
                    {
                        MT = MCV * columnVol / flowVol;
                        MV = MCV * columnVol;
                    }
                    break;
            }
        }

        public void Update(double newVal, double flowVol, double columnVol)
        {
            switch (MEnumBase)
            {
                case EnumBase.T:
                    MT = newVal;
                    MV = MT * flowVol;
                    MCV = MV / columnVol;
                    break;
                case EnumBase.V:
                    if (0 == flowVol)
                    {
                        MT = 0;
                        MV = 0;
                        MCV = 0;
                    }
                    else
                    {
                        MV = newVal;
                        MT = MV / flowVol;
                        MCV = MV / columnVol;
                    }
                    break;
                case EnumBase.CV:
                    if (0 == flowVol)
                    {
                        MT = 0;
                        MV = 0;
                        MCV = 0;
                    }
                    else
                    {
                        MCV = newVal;
                        MT = MCV * columnVol / flowVol;
                        MV = MCV * columnVol;
                    }
                    break;
            }
        }
    }
}
