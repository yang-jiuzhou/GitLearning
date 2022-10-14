using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    public class FlowRateVM : BaseGroupVM
    {
        public FlowRate MItem
        {
            get
            {
                return m_item;
            }
            set
            {
                m_item = value;
                MType = value.MType;

                MFlowRateUnitStr = MMethodBaseValue.MFlowRateUnitStr;
                MFlowMax = MMethodBaseValue.MMaxFlowRate;
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
                    MFlowVolLen = MMethodBaseValue.MFlowRate;
                }
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

                MFlowVolChangedHandler?.Invoke(MItem.MFlowVolLen.MFlowVol);
            }
        }
        public double MFlowMax
        {
            get
            {
                return m_flowMax;
            }
            set
            {
                m_flowMax = value;
                OnPropertyChanged("MFlowMax");
            }
        }
        public string MFlowRateUnitStr
        {
            get
            {
                return m_flowRateUnitStr;
            }
            set
            {
                m_flowRateUnitStr = value;
                OnPropertyChanged("MFlowRateUnitStr");
            }
        }

        private FlowRate m_item = null;
        private double m_flowMax = 0;
        private string m_flowRateUnitStr = "";

        public delegate void MHandlerDdelegate(object sender);
        //声明一个修改流速比事件
        public MHandlerDdelegate MFlowVolChangedHandler;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FlowRateVM(MethodBaseValue methodBaseValue) : base(methodBaseValue)
        {
            
        }

        /// <summary>
        /// 改变基本单位
        /// </summary>
        /// <param name="methodBaseValue"></param>
        public override void ChangeEnumBase(MethodBaseValue methodBaseValue)
        {
            MMethodBaseValue = methodBaseValue;

            if (methodBaseValue.MChangeFlowRateUnit)
            {
                MFlowRateUnitStr = methodBaseValue.MFlowRateUnitStr;
                MFlowMax = MMethodBaseValue.MMaxFlowRate;

                MFlowVolLen = MFlowVolLen;
            }

            if (methodBaseValue.MChangeFlowRate)
            {
                if (MEnableSameMS)
                {
                    MFlowVolLen = methodBaseValue.MFlowRate;
                }
            }
        }
    }
}
