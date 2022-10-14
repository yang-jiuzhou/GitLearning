using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.MethodEdit
{
    public class SampleApplicationTechVM : BaseGroupVM
    {
        public SampleApplicationTech MItem 
        { 
            get
            {
                return m_item;
            }
            set
            {
                m_item = value;
                MType = value.MType;

                MEnumSAT = m_item.MEnumSAT;

                MBaseStr = MMethodBaseValue.MBaseStr;
                MBaseUnitStr = MMethodBaseValue.MBaseUnitStr;
            }
        }
        public EnumSAT MEnumSAT
        {
            get
            {
                return MItem.MEnumSAT;
            }
            set
            {
                MItem.MEnumSAT = value;
                //switch (value)
                //{
                //    case EnumSAT.ManualLoopFilling:
                //        MVisibAutoSampleValve = Visibility.Visible;
                //        MVisibMENTSPWBWWSPTSA = Visibility.Hidden;
                //        MVisibInS = Visibility.Hidden;
                //        MVisibSampleTVCV = Visibility.Hidden;
                //        MVisibFillLoopWith = Visibility.Hidden;
                //        MVisibEmptyLoopWith = Visibility.Visible;
                //        break;
                //    case EnumSAT.SamplePumpLoopFilling:
                //        MVisibAutoSampleValve = Visibility.Visible;
                //        MVisibMENTSPWBWWSPTSA = Visibility.Visible;
                //        MVisibInS = Visibility.Visible;
                //        MVisibSampleTVCV = Visibility.Hidden;
                //        MVisibFillLoopWith = Visibility.Visible;
                //        MVisibEmptyLoopWith = Visibility.Visible;
                //        break;
                //    case EnumSAT.ISDOC:
                //        MVisibAutoSampleValve = Visibility.Hidden;
                //        MVisibMENTSPWBWWSPTSA = Visibility.Visible;
                //        MVisibInS = Visibility.Visible;
                //        MVisibSampleTVCV = Visibility.Visible;
                //        MVisibFillLoopWith = Visibility.Hidden;
                //        MVisibEmptyLoopWith = Visibility.Hidden;
                //        break;
                //}
                switch (value)
                {
                    case EnumSAT.ManualLoopFilling:
                        MVisibAutoSampleValve = Visibility.Visible;
                        MVisibMENTSPWBWWSPTSA = Visibility.Collapsed;
                        MVisibInS = Visibility.Collapsed;
                        MVisibSampleTVCV = Visibility.Collapsed;
                        MVisibFillLoopWith = Visibility.Collapsed;
                        MVisibEmptyLoopWith = Visibility.Visible;
                        break;
                    case EnumSAT.SamplePumpLoopFilling:
                        MVisibAutoSampleValve = Visibility.Visible;
                        MVisibMENTSPWBWWSPTSA = Visibility.Visible;
                        MVisibInS = Visibility.Visible;
                        MVisibSampleTVCV = Visibility.Collapsed;
                        MVisibFillLoopWith = Visibility.Visible;
                        MVisibEmptyLoopWith = Visibility.Visible;
                        break;
                    case EnumSAT.ISDOC:
                        MVisibAutoSampleValve = Visibility.Collapsed;
                        MVisibMENTSPWBWWSPTSA = Visibility.Visible;
                        MVisibInS = Visibility.Visible;
                        MVisibSampleTVCV = Visibility.Visible;
                        MVisibFillLoopWith = Visibility.Collapsed;
                        MVisibEmptyLoopWith = Visibility.Collapsed;
                        break;
                }
            }
        }
        public int MInS 
        { 
            get
            {
                return MItem.MInS;
            }
            set
            {
                MItem.MInS = value;
            }
        }
        public double MSampleTVCV
        {
            get
            {
                return MItem.MSampleTVCV.MTVCV;
            }
            set
            {
                MItem.MSampleTVCV.Update(value, MFlowVol, MMethodBaseValue.MColumnVol);
                OnPropertyChanged("MSampleTVCV");
            }
        }
        public double MFillLoopWith
        {
            get
            {
                return MItem.MFillLoopWith;
            }
            set
            {
                MItem.MFillLoopWith = value;
            }
        }
        public double MEmptyLoopWith
        {
            get
            {
                return MItem.MEmptyLoopWith;
            }
            set
            {
                MItem.MEmptyLoopWith = value;
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
        public Visibility MVisibAutoSampleValve
        {
            get
            {
                return m_visibAutoSampleValve;
            }
            set
            {
                m_visibAutoSampleValve = value;
                OnPropertyChanged("MVisibAutoSampleValve");
            }
        }
        public Visibility MVisibMENTSPWBWWSPTSA
        {
            get
            {
                return m_visibMENTSPWBWWSPTSA;
            }
            set
            {
                m_visibMENTSPWBWWSPTSA = value;
                OnPropertyChanged("MVisibMENTSPWBWWSPTSA");
            }
        }
        public Visibility MVisibInS
        {
            get
            {
                return m_visibInS;
            }
            set
            {
                m_visibInS = value;
                OnPropertyChanged("MVisibInS");
            }
        }
        public Visibility MVisibSampleTVCV
        {
            get
            {
                return m_visibSampleTVCV;
            }
            set
            {
                m_visibSampleTVCV = value;
                OnPropertyChanged("MVisibSampleTVCV");
            }
        }
        public Visibility MVisibFillLoopWith
        {
            get
            {
                return m_visibFillLoopWith;
            }
            set
            {
                m_visibFillLoopWith = value;
                OnPropertyChanged("MVisibFillLoopWith");
            }
        }
        public Visibility MVisibEmptyLoopWith
        {
            get
            {
                return m_visibEmptyLoopWith;
            }
            set
            {
                m_visibEmptyLoopWith = value;
                OnPropertyChanged("MVisibEmptyLoopWith");
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
                MSampleTVCV = MSampleTVCV;
            }
        }
        private double m_flowVol = 1;

        private SampleApplicationTech m_item = new SampleApplicationTech();
        private string m_baseStr = "";
        private string m_baseUnitStr = "";
        private Visibility m_visibAutoSampleValve = Visibility.Visible;
        private Visibility m_visibMENTSPWBWWSPTSA = Visibility.Visible;
        private Visibility m_visibInS = Visibility.Visible;
        private Visibility m_visibSampleTVCV = Visibility.Visible;
        private Visibility m_visibFillLoopWith = Visibility.Visible;
        private Visibility m_visibEmptyLoopWith = Visibility.Visible;
        

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="enumBase"></param>
        public SampleApplicationTechVM(MethodBaseValue methodBaseValue) : base(methodBaseValue)
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
                MItem.MSampleTVCV.MEnumBase = methodBaseValue.MEnumBaseNew;
                MSampleTVCV = MSampleTVCV;
            }
        }
    }
}
