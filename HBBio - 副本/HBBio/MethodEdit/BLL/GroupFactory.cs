using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    class GroupFactory
    {
        public static double m_flowVol = 1;

        /// <summary>
        /// 创建元素组
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public BaseGroup GetGroup(EnumGroupType type)
        {
            BaseGroup item = null;
            switch (type)
            {
                case EnumGroupType.FlowRate: item = new FlowRate(); break;
                case EnumGroupType.ValveSelection: item = new ValveSelection(); break;
                case EnumGroupType.Mixer: item = new Mixer(); break;
                case EnumGroupType.BPV: item = new BPVValve(); break;
                case EnumGroupType.UVReset: item = new UVReset(); break;
                case EnumGroupType.SampleApplicationTech: item = new SampleApplicationTech(); break;
                case EnumGroupType.TVCV: item = new BaseTVCV(); break;
                case EnumGroupType.FlowValveLength: item = new FlowValveLength(); break;
                case EnumGroupType.FlowRatePer: item = new FlowRatePer(); break;
                case EnumGroupType.PHCDUVUntil: item = new PHCDUVUntil(); break;
                case EnumGroupType.CollValveCollector: item = new CollValveCollector(); break;
                case EnumGroupType.CIP: item = new CIP(); break;
            }

            return item;
        }

        /// <summary>
        /// 创建元素组
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public BaseGroupVM GetGroupVM(BaseGroup baseGroup, MethodBaseValue methodBaseValue)
        {
            BaseGroupVM item = null;
            switch (baseGroup.MType)
            {
                case EnumGroupType.FlowRate:
                    item = new FlowRateVM(methodBaseValue);
                    ((FlowRateVM)item).MItem = (FlowRate)baseGroup;
                    m_flowVol = ((FlowRateVM)item).MItem.MFlowVolLen.MFlowVol;
                    break;
                case EnumGroupType.ValveSelection:
                    item = new ValveSelectionVM(methodBaseValue);
                    ((ValveSelectionVM)item).MItem = (ValveSelection)baseGroup;
                    break;
                case EnumGroupType.Mixer: 
                    item = new MixerVM(methodBaseValue);
                    ((MixerVM)item).MItem = (Mixer)baseGroup;
                    break;
                case EnumGroupType.BPV:
                    item = new BPVValveVM(methodBaseValue);
                    ((BPVValveVM)item).MItem = (BPVValve)baseGroup;
                    break;
                case EnumGroupType.UVReset:
                    item = new UVResetVM(methodBaseValue);
                    ((UVResetVM)item).MItem = (UVReset)baseGroup;
                    break;
                case EnumGroupType.SampleApplicationTech:
                    item = new SampleApplicationTechVM(methodBaseValue);
                    ((SampleApplicationTechVM)item).MItem = (SampleApplicationTech)baseGroup;
                    break;
                case EnumGroupType.TVCV:
                    item = new BaseTVCVVM(methodBaseValue, m_flowVol);
                    ((BaseTVCVVM)item).MItem = (BaseTVCV)baseGroup;
                    break;
                case EnumGroupType.FlowValveLength:
                    item = new FlowValveLengthVM(methodBaseValue);
                    ((FlowValveLengthVM)item).MItem = (FlowValveLength)baseGroup;
                    break;
                case EnumGroupType.FlowRatePer:
                    item = new FlowRatePerVM(methodBaseValue);
                    ((FlowRatePerVM)item).MItem = (FlowRatePer)baseGroup;
                    break;
                case EnumGroupType.PHCDUVUntil:
                    item = new PHCDUVUntilVM(methodBaseValue);
                    ((PHCDUVUntilVM)item).MItem = (PHCDUVUntil)baseGroup;
                    break;
                case EnumGroupType.CollValveCollector:
                    item = new CollValveCollectorVM(methodBaseValue);
                    ((CollValveCollectorVM)item).MItem = (CollValveCollector)baseGroup;
                    break;
                case EnumGroupType.CIP:
                    item = new CIPVM(methodBaseValue);
                    ((CIPVM)item).MItem = (CIP)baseGroup;
                    break;
            }

            return item;
        }

        /// <summary>
        /// 创建元素组
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public BaseGroupVM GetGroupVM(EnumGroupType type, MethodBaseValue methodBaseValue)
        {
            BaseGroupVM item = null;
            switch (type)
            {
                case EnumGroupType.FlowRate:
                    item = new FlowRateVM(methodBaseValue);
                    ((FlowRateVM)item).MItem = (FlowRate)GetGroup(type);
                    m_flowVol = ((FlowRateVM)item).MItem.MFlowVolLen.MFlowVol;
                    break;
                case EnumGroupType.ValveSelection:
                    item = new ValveSelectionVM(methodBaseValue);
                    ((ValveSelectionVM)item).MItem = (ValveSelection)GetGroup(type);
                    break;
                case EnumGroupType.Mixer:
                    item = new MixerVM(methodBaseValue);
                    ((MixerVM)item).MItem = (Mixer)GetGroup(type);
                    break;
                case EnumGroupType.BPV:
                    item = new BPVValveVM(methodBaseValue);
                    ((BPVValveVM)item).MItem = (BPVValve)GetGroup(type);
                    break;
                case EnumGroupType.UVReset:
                    item = new UVResetVM(methodBaseValue);
                    ((UVResetVM)item).MItem = (UVReset)GetGroup(type);
                    break;
                case EnumGroupType.SampleApplicationTech:
                    item = new SampleApplicationTechVM(methodBaseValue);
                    ((SampleApplicationTechVM)item).MItem = (SampleApplicationTech)GetGroup(type);
                    break;
                case EnumGroupType.TVCV:
                    item = new BaseTVCVVM(methodBaseValue, m_flowVol);
                    ((BaseTVCVVM)item).MItem = (BaseTVCV)GetGroup(type);
                    break;
                case EnumGroupType.FlowValveLength:
                    item = new FlowValveLengthVM(methodBaseValue);
                    ((FlowValveLengthVM)item).MItem = (FlowValveLength)GetGroup(type);
                    break;
                case EnumGroupType.FlowRatePer:
                    item = new FlowRatePerVM(methodBaseValue);
                    ((FlowRatePerVM)item).MItem = (FlowRatePer)GetGroup(type);
                    break;
                case EnumGroupType.PHCDUVUntil:
                    item = new PHCDUVUntilVM(methodBaseValue);
                    ((PHCDUVUntilVM)item).MItem = (PHCDUVUntil)GetGroup(type);
                    break;
                case EnumGroupType.CollValveCollector:
                    item = new CollValveCollectorVM(methodBaseValue);
                    ((CollValveCollectorVM)item).MItem = (CollValveCollector)GetGroup(type);
                    break;
                case EnumGroupType.CIP:
                    item = new CIPVM(methodBaseValue);
                    ((CIPVM)item).MItem = (CIP)GetGroup(type);
                    break;
            }

            return item;
        }
    }
}