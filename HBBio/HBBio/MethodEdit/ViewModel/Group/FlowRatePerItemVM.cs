using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    public class FlowRatePerItemVM : DlyNotifyPropertyChanged
    {
        public FlowRatePerItem MItem { get; set; }

        public double MPerBS
        {
            get
            {
                return MItem.MPerBS;
            }
            set
            {
                MItem.MPerBS = value;
            }
        }
        public double MPerBE
        {
            get
            {
                return MItem.MPerBE;
            }
            set
            {
                MItem.MPerBE = value;
            }
        }
        public double MPerCS
        {
            get
            {
                return MItem.MPerCS;
            }
            set
            {
                MItem.MPerCS = value;
            }
        }
        public double MPerCE
        {
            get
            {
                return MItem.MPerCE;
            }
            set
            {
                MItem.MPerCE = value;
            }
        }
        public double MPerDS
        {
            get
            {
                return MItem.MPerDS;
            }
            set
            {
                MItem.MPerDS = value;
            }
        }
        public double MPerDE
        {
            get
            {
                return MItem.MPerDE;
            }
            set
            {
                MItem.MPerDE = value;
            }
        }
        public bool MFillSystem
        {
            get
            {
                return MItem.MFillSystem;
            }
            set
            {
                MItem.MFillSystem = value;
            }
        }
        public double MBaseTVCV
        {
            get
            {
                return MItem.MBaseTVCV.MTVCV;
            }
            set
            {
                MItem.MBaseTVCV.Update(value, MFlowVol, MMethodBaseValue.MColumnVol);
                OnPropertyChanged("MBaseTVCV");
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
                MBaseTVCV = MBaseTVCV;
            }
        }
        private double m_flowVol = 1;

        private MethodBaseValue MMethodBaseValue { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="enumBase"></param>
        /// <param name="enumFlowRate"></param>
        public FlowRatePerItemVM(MethodBaseValue methodBaseValue)
        {
            MItem = new FlowRatePerItem();
            MItem.MBaseTVCV.Init(methodBaseValue.MEnumBaseOld, MFlowVol, methodBaseValue.MColumnVol);
            MMethodBaseValue = methodBaseValue;
        }
    }
}
