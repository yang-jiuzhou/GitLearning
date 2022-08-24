using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    class MiscellaneousVM : BasePhaseVM
    {
        public Miscellaneous MItem 
        { 
            get
            {
                return (Miscellaneous)m_item;
            }
            set
            {
                m_item = value;

                MBaseStr = MMethodBaseValue.MBaseStr;
                MBaseUnitStr = MMethodBaseValue.MBaseUnitStr;
            }
        }

        public bool MEnableSetMark 
        { 
            get
            {
                return MItem.MEnableSetMark;
            }
            set
            {
                MItem.MEnableSetMark = value;
            }
        }
        public string MSetMark
        {
            get
            {
                return MItem.MSetMark;
            }
            set
            {
                MItem.MSetMark = value;
            }
        }

        public bool MEnableMethodDelay
        {
            get
            {
                return MItem.MEnableMethodDelay;
            }
            set
            {
                MItem.MEnableMethodDelay = value;
            }
        }
        public double MMethodDelay
        {
            get
            {
                return MItem.MMethodDelay.MTVCV;
            }
            set
            {
                MItem.MMethodDelay.Update(value, MFlowVol, MMethodBaseValue.MColumnVol);
                OnPropertyChanged("MMethodDelay");
            }
        }

        public bool MEnableMessage
        {
            get
            {
                return MItem.MEnableMessage;
            }
            set
            {
                MItem.MEnableMessage = value;
            }
        }
        public string MMessage
        {
            get
            {
                return MItem.MMessage;
            }
            set
            {
                MItem.MMessage = value;
            }
        }
        public bool MEnablePauseAfterMessage
        {
            get
            {
                return MItem.MEnablePauseAfterMessage;
            }
            set
            {
                MItem.MEnablePauseAfterMessage = value;
            }
        }

        public bool MEnablePauseTimer
        {
            get
            {
                return MItem.MEnablePauseTimer;
            }
            set
            {
                MItem.MEnablePauseTimer = value;
            }
        }
        public double MPauseTimer
        {
            get
            {
                return MItem.MPauseTimer;
            }
            set
            {
                MItem.MPauseTimer = value;
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

        public double MFlowVol
        {
            get
            {
                return m_flowVol;
            }
            set
            {
                m_flowVol = value;
                MMethodDelay = MMethodDelay;
            }
        }
        private double m_flowVol = 1;

        private string m_baseStr = "";
        private string m_baseUnitStr = "";

        public MiscellaneousVM(MethodBaseValue methodBaseValue) : base(methodBaseValue)
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
            MFlowVol = methodBaseValue.MFlowVol;

            if (methodBaseValue.MChangeBaseUnit)
            {
                MBaseStr = methodBaseValue.MBaseStr;
                MBaseUnitStr = methodBaseValue.MBaseUnitStr;
                MItem.MMethodDelay.MEnumBase = methodBaseValue.MEnumBaseNew;
                MMethodDelay = MMethodDelay;
            }
        }
    }
}
