using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    public class FlowValveLengthItemVM : DlyNotifyPropertyChanged
    {
        public FlowValveLengthItem MItem { get; set; }

        public string MNote
        {
            get
            {
                return MItem.MNote;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MItem.MNote = "";
                }
                else
                {
                    MItem.MNote = value;
                }
            }
        }
        public int MInA
        {
            get
            {
                return MItem.MInA;
            }
            set
            {
                MItem.MInA = value;
            }
        }
        public int MInB
        {
            get
            {
                return MItem.MInB;
            }
            set
            {
                MItem.MInB = value;
            }
        }
        public int MInC
        {
            get
            {
                return MItem.MInC;
            }
            set
            {
                MItem.MInC = value;
            }
        }
        public int MInD
        {
            get
            {
                return MItem.MInD;
            }
            set
            {
                MItem.MInD = value;
            }
        }
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
                MItem.MBaseTVCV.Update(value, MItem.MFlowVolLen.MFlowVol, MMethodBaseValue.MColumnVol);
                OnPropertyChanged("MBaseTVCV");
            }
        }
        public double MFlowVolLen
        {
            get
            {
                return MItem.MFlowVolLen.MFlowRate;
            }
            set
            {
                MItem.MFlowVolLen.Update(value, MMethodBaseValue.MColumnArea);
                OnPropertyChanged("MFlowVolLen");

                MBaseTVCV = MBaseTVCV;
            }
        }
        public int MBPV
        {
            get
            {
                return MItem.MBPV;
            }
            set
            {
                MItem.MBPV = value;
            }
        }
        public int MVOut
        {
            get
            {
                return MItem.MVOut;
            }
            set
            {
                MItem.MVOut = value;
            }
        }
        public double MIncubation
        {
            get
            {
                return MItem.MIncubation;
            }
            set
            {
                MItem.MIncubation = value;
            }
        }

        private MethodBaseValue MMethodBaseValue { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="enumBase"></param>
        /// <param name="enumFlowRate"></param>
        public FlowValveLengthItemVM(MethodBaseValue methodBaseValue)
        {
            MItem = new FlowValveLengthItem();
            MItem.MFlowVolLen.Init(methodBaseValue.MEnumFlowRateOld, methodBaseValue.MColumnArea);
            MItem.MBaseTVCV.Init(methodBaseValue.MEnumBaseOld, MItem.MFlowVolLen.MFlowVol, methodBaseValue.MColumnVol);
            MMethodBaseValue = methodBaseValue;
        }
    }
}
