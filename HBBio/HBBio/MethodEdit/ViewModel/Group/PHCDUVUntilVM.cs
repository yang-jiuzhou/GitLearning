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
                if (value < EnumMonitorInfo.NameList.Length)
                {
                    if (EnumMonitorInfo.NameList[value].Contains("pH"))
                    {
                        MVisibPHMore = Visibility.Visible == MVisibMoreThan ? Visibility.Visible : Visibility.Collapsed;
                        MVisibCDMore = Visibility.Collapsed;
                        MVisibUVMore = Visibility.Collapsed;
                        MVisibPHLess = Visibility.Visible == MVisibLessThan ? Visibility.Visible : Visibility.Collapsed;
                        MVisibCDLess = Visibility.Collapsed;
                        MVisibUVLess = Visibility.Collapsed;
                        MMoreLessThanMax = StaticValue.s_maxPH;
                        MMoreLessThanMin = StaticValue.s_minPH;
                        MMoreLessThanStr = "[" + StaticValue.s_minPH + " - " + StaticValue.s_maxPH + "]";
                    }
                    else if (EnumMonitorInfo.NameList[value].Contains("Cd"))
                    {
                        MVisibPHMore = Visibility.Collapsed;
                        MVisibCDMore = Visibility.Visible == MVisibMoreThan ? Visibility.Visible : Visibility.Collapsed;
                        MVisibUVMore = Visibility.Collapsed;
                        MVisibPHLess = Visibility.Collapsed;
                        MVisibCDLess = Visibility.Visible == MVisibLessThan ? Visibility.Visible : Visibility.Collapsed;
                        MVisibUVLess = Visibility.Collapsed;
                        MMoreLessThanMax = StaticValue.s_maxCD;
                        MMoreLessThanMin = StaticValue.s_minCD;
                        MMoreLessThanStr = "[" + StaticValue.s_minCD + " - " + StaticValue.s_maxCD + "]";
                    }
                    else
                    {
                        MVisibPHMore = Visibility.Collapsed;
                        MVisibCDMore = Visibility.Collapsed;
                        MVisibUVMore = Visibility.Visible == MVisibMoreThan ? Visibility.Visible : Visibility.Collapsed;
                        MVisibPHLess = Visibility.Collapsed;
                        MVisibCDLess = Visibility.Collapsed;
                        MVisibUVLess = Visibility.Visible == MVisibLessThan ? Visibility.Visible : Visibility.Collapsed;
                        MMoreLessThanMax = StaticValue.s_maxUV;
                        MMoreLessThanMin = StaticValue.s_minUV;
                        MMoreLessThanStr = "[" + StaticValue.s_minUV + " - " + StaticValue.s_maxUV + "]";
                    }
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
                MMonitorIndex = MMonitorIndex;
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

        public Visibility MVisibPHMore
        {
            get
            {
                return m_visibPHMore;
            }
            set
            {
                m_visibPHMore = value;
                OnPropertyChanged("MVisibPHMore");
            }
        }
        public Visibility MVisibCDMore
        {
            get
            {
                return m_visibCDMore;
            }
            set
            {
                m_visibCDMore = value;
                OnPropertyChanged("MVisibCDMore");
            }
        }
        public Visibility MVisibUVMore
        {
            get
            {
                return m_visibUVMore;
            }
            set
            {
                m_visibUVMore = value;
                OnPropertyChanged("MVisibUVMore");
            }
        }
        public Visibility MVisibPHLess
        {
            get
            {
                return m_visibPHLess;
            }
            set
            {
                m_visibPHLess = value;
                OnPropertyChanged("MVisibPHLess");
            }
        }
        public Visibility MVisibCDLess
        {
            get
            {
                return m_visibCDLess;
            }
            set
            {
                m_visibCDLess = value;
                OnPropertyChanged("MVisibCDLess");
            }
        }
        public Visibility MVisibUVLess
        {
            get
            {
                return m_visibUVLess;
            }
            set
            {
                m_visibUVLess = value;
                OnPropertyChanged("MVisibUVLess");
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

        private Visibility m_visibPHMore = Visibility.Visible;
        private Visibility m_visibCDMore = Visibility.Visible;
        private Visibility m_visibUVMore = Visibility.Visible;
        private Visibility m_visibPHLess = Visibility.Visible;
        private Visibility m_visibCDLess = Visibility.Visible;
        private Visibility m_visibUVLess = Visibility.Visible;
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
