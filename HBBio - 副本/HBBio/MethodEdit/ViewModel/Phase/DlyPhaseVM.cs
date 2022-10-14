using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.MethodEdit
{
    class DlyPhaseVM : BasePhaseVM
    {
        public DlyPhase MItem
        {
            get
            {
                return (DlyPhase)m_item;
            }
            set
            {
                m_item = value;

                GroupFactory groupFactory = new GroupFactory();
                foreach (var it in value.MListGroup)
                {
                    MListGroup.Add(groupFactory.GetGroupVM(it, MMethodBaseValue));
                    if (EnumGroupType.FlowRate == it.MType)
                    {
                        ((FlowRateVM)MListGroup.Last()).MFlowVolChangedHandler += FlowVolChanged;
                    }
                }
            }
        }


        public DlyPhaseVM(MethodBaseValue methodBaseValue) : base(methodBaseValue)
        {
            
        }

        /// <summary>
        /// 改变基本单位
        /// </summary>
        /// <param name="methodBaseValue"></param>
        public override void ChangeEnumBase(MethodBaseValue methodBaseValue)
        {
            foreach (var it in MListGroup)
            {
                it.ChangeEnumBase(methodBaseValue);
            }
        }

        /// <summary>
        /// 流速变化
        /// </summary>
        /// <param name="sender"></param>
        private void FlowVolChanged(object sender)
        {
            foreach (var it in MListGroup)
            {
                switch (it.MType)
                {
                    case EnumGroupType.SampleApplicationTech:
                        ((SampleApplicationTechVM)it).MFlowVol = (double)sender;
                        break;
                    case EnumGroupType.TVCV:
                        ((BaseTVCVVM)it).MFlowVol = (double)sender;
                        break;
                    case EnumGroupType.FlowRatePer:
                        ((FlowRatePerVM)it).MFlowVol = (double)sender;
                        break;
                    case EnumGroupType.PHCDUVUntil:
                        ((PHCDUVUntilVM)it).MFlowVol = (double)sender;
                        break;
                }
            }
        }
    }
}
