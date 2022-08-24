using HBBio.Communication;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.MethodEdit
{
    public class PHCDUVUntilVM : BaseGroupVM
    {
        public PHCDUVUntil MItem
        {
            get
            {
                return m_item;
            }
            set
            {
                m_item = value;
                MType = value.MType;

                MBaseStr = MMethodBaseValue.MBaseStr;
                MBaseUnitStr = MMethodBaseValue.MBaseUnitStr;

                MUntilType = m_item.MUntilType;
                MMonitorIndex = (int)m_item.MMonitorIndex;
                MJudgeIndex = (int)m_item.MJudgeIndex;
            }
        }

        public string MHeaderText
        {
            get
            {
                return MItem.MHeaderText;
            }
            set
            {
                MItem.MHeaderText = value;
            }
        }

        public EnumPHCDUVUntil MUntilType
        {
            get
            {
                return MItem.MUntilType;
            }
            set
            {
                MItem.MUntilType = value;
            }
        }
        public double MTotalTVCV
        {
            get
            {
                return MItem.MTotalTVCV.MTVCV;
            }
            set
            {
                MItem.MTotalTVCV.Update(value, MFlowVol, MMethodBaseValue.MColumnVol);
                OnPropertyChanged("MTotalTVCV");
            }
        }
        public EnumBase MEnumBase { get; set; }
        public string MBaseStr
        {
            get
            {
                return m_baseStr;
            }
            set
            {
                m_baseStr = value;
                OnPropertyChanged("MBaseStr");
            }
        }
        public string MBaseUnitStr
        {
            get
            {
                return m_baseUnitStr;
            }
            set
            {
                m_baseUnitStr = value;
                OnPropertyChanged("MBaseUnitStr");
            }
        }
        public int MMonitorIndex
        {
            get
            {
                return MItem.MMonitorIndex;
            }
            set
            {
                MItem.MMonitorIndex = value;
                if (EnumMonitorInfo.NameList[value].Contains("pH"))
                {
                    MVisibPH = Visibility.Visible;
                    MVisibCD = Visibility.Collapsed;
                    MVisibUV = Visibility.Collapsed;
                    MMoreLessThanMax = StaticValue.s_maxPH;
                    MMoreLessThanMin = StaticValue.s_minPH;
                    MMoreLessThanStr = "[" + StaticValue.s_minPH + " - " + StaticValue.s_maxPH + "]";
                }
                else if (EnumMonitorInfo.NameList[value].Contains("Cd"))
                {
                    MVisibPH = Visibility.Collapsed;
                    MVisibCD = Visibility.Visible;
                    MVisibUV = Visibility.Collapsed;
                    MMoreLessThanMax = StaticValue.s_maxCD;
                    MMoreLessThanMin = StaticValue.s_minCD;
                    MMoreLessThanStr = "[" + StaticValue.s_minCD + " - " + StaticValue.s_maxCD + "]";
                }
                else
                {
                    MVisibPH = Visibility.Collapsed;
                    MVisibCD = Visibility.Collapsed;
                    MVisibUV = Visibility.Visible;
                    MMoreLessThanMax = StaticValue.s_maxUV;
                    MMoreLessThanMin = StaticValue.s_minUV;
                    MMoreLessThanStr = "[" + StaticValue.s_minUV + " - " + StaticValue.s_maxUV + "]";
                }
            }
        }
        public int MJudgeIndex
        {
            get
            {
                return (int)MItem.MJudgeIndex;
            }
            set
            {
                MItem.MJudgeIndex = (EnumJudge)value;
                switch ((EnumJudge)value)
                {
                    case EnumJudge.Stable:
                        MVisibMoreThan = Visibility.Visible;
                        MVisibLessThan = Visibility.Visible;
                        break;
                    case EnumJudge.MoreThan:
                        MVisibMoreThan = Visibility.Visible;
                        MVisibLessThan = Visibility.Hidden;
                        break;
                    case EnumJudge.LessThan:
                        MVisibMoreThan = Visibility.Hidden;
                        MVisibLessThan = Visibility.Visible;
                        break;
                }
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
        public double MMoreLessThanMax
        {
            get
            {
                return m_moreLessThanMax;
            }
            set
            {
                m_moreLessThanMax = value;
                OnPropertyChanged("MMoreLessThanMax");
            }
        }
        public double MMoreLessThanMin
        {
            get
            {
                return m_moreLessThanMin;
            }
            set
            {
                m_moreLessThanMin = value;
                OnPropertyChanged("MMoreLessThanMin");
            }
        }
        public string MMoreLessThanStr
        {
            get
            {
                return m_moreLessThanStr;
            }
            set
            {
                m_moreLessThanStr = value;
                OnPropertyChanged("MMoreLessThanStr");
            }
        }
        public double MStabilityTime
        {
            get
            {
                return MItem.MStabilityTime;
            }
            set
            {
                MItem.MStabilityTime = value;
            }
        }
        public double MMaxTVCV
        {
            get
            {
                return MItem.MMaxTVCV.MTVCV;
            }
            set
            {
                MItem.MMaxTVCV.Update(value, MFlowVol, MMethodBaseValue.MColumnVol);
                OnPropertyChanged("MMaxTVCV");
            }
        }

        public Visibility MVisibPH
        {
            get
            {
                return m_visibPH;
            }
            set
            {
                m_visibPH = value;
                OnPropertyChanged("MVisibPH");
            }
        }
        public Visibility MVisibCD
        {
            get
            {
                return m_visibCD;
            }
            set
            {
                m_visibCD = value;
                OnPropertyChanged("MVisibCD");
            }
        }
        public Visibility MVisibUV
        {
            get
            {
                return m_visibUV;
            }
            set
            {
                m_visibUV = value;
                OnPropertyChanged("MVisibUV");
            }
        }
        public Visibility MVisibMoreThan
        {
            get
            {
                return m_visibMoreThan;
            }
            set
            {
                m_visibMoreThan = value;
                OnPropertyChanged("MVisibMoreThan");
            }
        }
        public Visibility MVisibLessThan
        {
            get
            {
                return m_visibLessThan;
            }
            set
            {
                m_visibLessThan = value;
                OnPropertyChanged("MVisibLessThan");
            }
        }

        public double MFlowVol
        {
            get
            {
                return m_flowVol;
            }
            set
            {
                m_flowVol = value;
                MTotalTVCV = MTotalTVCV;
                MMaxTVCV = MMaxTVCV;
            }
        }
        private double m_flowVol = 1;

        private PHCDUVUntil m_item = new PHCDUVUntil();

        private string m_baseStr = "";
        private string m_baseUnitStr = "";
        public double m_moreLessThanMax = 999999;
        public double m_moreLessThanMin = 0;
        private string m_moreLessThanStr = "";

        private Visibility m_visibPH = Visibility.Visible;
        private Visibility m_visibCD = Visibility.Visible;
        private Visibility m_visibUV = Visibility.Visible;
        private Visibility m_visibMoreThan = Visibility.Visible;
        private Visibility m_visibLessThan = Visibility.Visible;

        /// <summary>
        /// 构造函数
        /// </summary>
        public PHCDUVUntilVM(MethodBaseValue methodBaseValue) : base(methodBaseValue)
        {
            m_flowVol = methodBaseValue.MFlowVol;
        }

        /// <summary>
        /// 改变基本单位
        /// </summary>
        /// <param name="methodBaseValue"></param>
        public override void ChangeEnumBase(MethodBaseValue methodBaseValue)
        {
            MMethodBaseValue = methodBaseValue;

            if (methodBaseValue.MChangeBaseUnit)
            {
                MBaseStr = methodBaseValue.MBaseStr;
                MBaseUnitStr = methodBaseValue.MBaseUnitStr;

                MItem.MTotalTVCV.MEnumBase = methodBaseValue.MEnumBaseNew;
                MTotalTVCV = MTotalTVCV;

                MItem.MMaxTVCV.MEnumBase = methodBaseValue.MEnumBaseNew;
                MMaxTVCV = MMaxTVCV;
            }
        }
    }
}
