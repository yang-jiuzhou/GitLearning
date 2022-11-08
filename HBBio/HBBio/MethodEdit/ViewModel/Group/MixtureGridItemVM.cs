using HBBio.Communication;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    public class MixtureGridItemVM : DlyNotifyPropertyChanged
    {
        public MixtureGridItem MItem 
        {
            get
            {
                return m_item;
            }
            set
            {
                m_item = value;

                MASParaList.Clear();
                foreach (var it in value.MASParaList)
                {
                    ASMethodParaVM item = new ASMethodParaVM(it);
                    MASParaList.Add(item);
                }
            }
        }
        private MixtureGridItem m_item = new MixtureGridItem();

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
        public int MFillSystem
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
                MItem.MBaseTVCV.Update(value, MItem.MFlowVolLenSample.MFlowVol, MMethodBaseValue.MColumnVol);
                MItem.MBaseTVCV.Update(value, MItem.MFlowVolLenSystem.MFlowVol, MMethodBaseValue.MColumnVol);
                OnPropertyChanged("MBaseTVCV");
            }
        }
        public double MFlowVolLenSample
        {
            get
            {
                return MItem.MFlowVolLenSample.MFlowRate;
            }
            set
            {
                MItem.MFlowVolLenSample.Update(value, MMethodBaseValue.MColumnArea);
                OnPropertyChanged("MFlowVolLenSample");

                MBaseTVCV = MBaseTVCV;
            }
        }
        public double MFlowVolLenSystem
        {
            get
            {
                return MItem.MFlowVolLenSystem.MFlowRate;
            }
            set
            {
                MItem.MFlowVolLenSystem.Update(value, MMethodBaseValue.MColumnArea);
                OnPropertyChanged("MFlowVolLenSystem");

                MBaseTVCV = MBaseTVCV;
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
        public int MIJV
        {
            get
            {
                return MItem.MIJV;
            }
            set
            {
                MItem.MIJV = value;
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
        public int MCPV
        {
            get
            {
                return MItem.MCPV;
            }
            set
            {
                MItem.MCPV = value;
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
        public bool MMixer
        {
            get
            {
                return MItem.MMixer;
            }
            set
            {
                MItem.MMixer = value;
            }
        }
        public bool MUVClear
        {
            get
            {
                return MItem.MUVClear;
            }
            set
            {
                MItem.MUVClear = value;
            }
        }
        public List<ASMethodParaVM> MASParaList { get; set; }
        public EnumMonitorActionMethod MAction01
        {
            get
            {
                return MASParaList[0].MAction;
            }
            set
            {
                MASParaList[0].MAction = value;
            }
        }
        public EnumMonitorActionMethod MAction02
        {
            get
            {
                return MASParaList[1].MAction;
            }
            set
            {
                MASParaList[1].MAction = value;
            }
        }
        public EnumMonitorActionMethod MAction03
        {
            get
            {
                return MASParaList[2].MAction;
            }
            set
            {
                MASParaList[2].MAction = value;
            }
        }
        public EnumMonitorActionMethod MAction04
        {
            get
            {
                return MASParaList[3].MAction;
            }
            set
            {
                MASParaList[3].MAction = value;
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
        public MixtureGridItemVM(MethodBaseValue methodBaseValue)
        {
            MASParaList = new List<ASMethodParaVM>();

            MItem.MFlowVolLenSample.Init(methodBaseValue.MEnumFlowRateOld, methodBaseValue.MColumnArea);
            MItem.MBaseTVCV.Init(methodBaseValue.MEnumBaseOld, MItem.MFlowVolLenSample.MFlowVol, methodBaseValue.MColumnVol);
            MItem.MFlowVolLenSystem.Init(methodBaseValue.MEnumFlowRateOld, methodBaseValue.MColumnArea);
            MItem.MBaseTVCV.Init(methodBaseValue.MEnumBaseOld, MItem.MFlowVolLenSystem.MFlowVol, methodBaseValue.MColumnVol);
            MMethodBaseValue = methodBaseValue;
        }
    }
}
