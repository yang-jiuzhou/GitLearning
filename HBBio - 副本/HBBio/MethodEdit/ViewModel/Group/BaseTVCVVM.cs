using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.MethodEdit
{
    public class BaseTVCVVM : BaseGroupVM
    {
        public BaseTVCV MItem
        {
            get
            {
                return m_item;
            }
            set
            {
                m_item = value;
                m_item.Init(MMethodBaseValue.MEnumBaseOld, m_flowVol, MMethodBaseValue.MColumnVol);
                MType = value.MType;

                MBaseStr = MMethodBaseValue.MBaseStr;
                MBaseUnitStr = MMethodBaseValue.MBaseUnitStr;
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
        public double MTVCV
        {
            get
            {
                return MItem.MTVCV;
            }
            set
            {
                MItem.Update(value, m_flowVol, MMethodBaseValue.MColumnVol);
                OnPropertyChanged("MBaseTVCV");
            }
        }

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
        private string m_baseStr = "";
        private string m_baseUnitStr = "";

        public double MFlowVol
        {
            get
            {
                return m_flowVol;
            }
            set
            {
                m_flowVol = value;
                MTVCV = MTVCV;
            }
        }
        private double m_flowVol = 1;

        public Visibility MTVCVVisible { get;set;}

        private BaseTVCV m_item = null;


        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseTVCVVM(MethodBaseValue methodBaseValue, double flowVol) : base(methodBaseValue)
        {
            MTVCVVisible = Visibility.Collapsed;

            m_flowVol = flowVol;
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
 
                MItem.MEnumBase = methodBaseValue.MEnumBaseNew;
                MTVCV = MTVCV;
            }
        }
    }
}
