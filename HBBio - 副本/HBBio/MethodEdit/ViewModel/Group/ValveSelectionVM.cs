using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    public class ValveSelectionVM : BaseGroupVM
    {
        public ValveSelection MItem
        {
            get
            {
                return m_item;
            }
            set
            {
                m_item = value;
                MType = value.MType;
            }
        }

        public bool MEnableSameMS
        { 
            get
            {
                return MItem.MEnableSameMS;
            }
            set
            {
                MItem.MEnableSameMS = value;
                if (value)
                {
                    MInA = MMethodBaseValue.MInA;
                    MInB = MMethodBaseValue.MInB;
                    MInC = MMethodBaseValue.MInC;
                    MInD = MMethodBaseValue.MInD;
                    MBPV = MMethodBaseValue.MBPV;
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
                OnPropertyChanged("MInA");
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
                OnPropertyChanged("MInB");
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
                OnPropertyChanged("MInC");
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
                OnPropertyChanged("MInD");
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
                OnPropertyChanged("MBPV");
            }
        }
        public bool MVisibPer 
        { 
            get
            {
                return MItem.MVisibPer;
            }
            set
            {
                MItem.MVisibPer = value;
            }
        }
        public double MPerB
        {
            get
            {
                return MItem.MPerB;
            }
            set
            {
                MItem.MPerB = value;
            }
        }
        public double MPerC
        {
            get
            {
                return MItem.MPerC;
            }
            set
            {
                MItem.MPerC = value;
            }
        }
        public double MPerD
        {
            get
            {
                return MItem.MPerD;
            }
            set
            {
                MItem.MPerD = value;
            }
        }
        public bool MVisibWash
        {
            get
            {
                return MItem.MVisibWash;
            }
            set
            {
                MItem.MVisibWash = value;
            }
        }
        public bool MEnableWash
        {
            get
            {
                return MItem.MEnableWash;
            }
            set
            {
                MItem.MEnableWash = value;
            }
        }


        private ValveSelection m_item = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="methodBaseValue"></param>
        public ValveSelectionVM(MethodBaseValue methodBaseValue) : base(methodBaseValue)
        {

        }

        /// <summary>
        /// 改变基本单位
        /// </summary>
        /// <param name="methodBaseValue"></param>
        public override void ChangeEnumBase(MethodBaseValue methodBaseValue)
        {
            MMethodBaseValue = methodBaseValue;

            if (MEnableSameMS)
            {
                MInA = methodBaseValue.MInA;
                MInB = methodBaseValue.MInB;
                MInC = methodBaseValue.MInC;
                MInD = methodBaseValue.MInD;
                MBPV = MMethodBaseValue.MBPV;
            }
        }
    }
}
