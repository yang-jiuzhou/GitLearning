using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.MethodEdit
{
    [Serializable]
    public class DlyPhase : BasePhase
    {
        public bool[] MArrIsRun { get; set; }           //仅在执行方法运行临时所用，其它地方不要使用
        public bool[] MArrIsIncubation { get; set; }    //仅在执行方法运行临时所用，其它地方不要使用


        /// <summary>
        /// 构造函数
        /// </summary>
        public DlyPhase() : base()
        {

        }

        /// <summary>
        /// 计算阶段长度
        /// </summary>
        public override string StatisticsAllStep(double columnVol)
        {
            string error = null;

            ClearList();

            double flowVol = 0;

            foreach (var it in MListGroup)
            {
                switch (it.MType)
                {
                    case EnumGroupType.FlowRate:
                        {
                            FlowRate tmp = (FlowRate)it;
                            if (0 == tmp.MFlowVolLen.MFlowVol)
                            {
                                error += MNamePhase + Share.ReadXaml.GetResources("ME_Msg_NullFlowRate");
                            }
                            flowVol = tmp.MFlowVolLen.MFlowVol;
                        }
                        break;
                    case EnumGroupType.ValveSelection:
                        {
                            ValveSelection tmp = (ValveSelection)it;
                            if (tmp.MVisibPer)
                            {
                                UpdatePer(tmp.MPerB, tmp.MPerC, tmp.MPerD);
                            }
                        }
                        break;
                    case EnumGroupType.Mixer:
                    case EnumGroupType.BPV:
                        break;
                    case EnumGroupType.SampleApplicationTech:
                        {
                            SampleApplicationTech tmp = (SampleApplicationTech)it;
                            switch (tmp.MEnumSAT)
                            {
                                case EnumSAT.ManualLoopFilling:
                                    AddStep(ReadXaml.GetResources("labMECPT_AS"), 0 == flowVol ? 0 : tmp.MEmptyLoopWith / flowVol, tmp.MEmptyLoopWith, tmp.MEmptyLoopWith / columnVol);
                                    break;
                                case EnumSAT.SamplePumpLoopFilling:
                                    AddStep(ReadXaml.GetResources("labMECPT_LL"), 0 == flowVol ? 0 : tmp.MFillLoopWith / flowVol, tmp.MFillLoopWith, tmp.MFillLoopWith / columnVol);
                                    AddStep(ReadXaml.GetResources("labMECPT_AS"), 0 == flowVol ? 0 : tmp.MEmptyLoopWith / flowVol, tmp.MEmptyLoopWith, tmp.MEmptyLoopWith / columnVol);
                                    break;
                                case EnumSAT.ISDOC:
                                    AddStep(ReadXaml.GetResources("labMECPT_DI"), tmp.MSampleTVCV);
                                    break;
                            }
                        }
                        break;
                    case EnumGroupType.TVCV:
                        {
                            BaseTVCV tmp = (BaseTVCV)it;
                            AddStep("labMECPT_ES", tmp);
                        }
                        break;
                    case EnumGroupType.FlowValveLength:
                        {
                            FlowValveLength tmp = (FlowValveLength)it;
                            for (int i = 0; i < tmp.MList.Count; i++)
                            {
                                AddStep(Share.ReadXaml.GetEnum(it.MType, "ME_EnumGroupType_") + tmp.MList.Count + "-" + (i + 1) + ""
                                    , tmp.MList[i].MBaseTVCV
                                    , tmp.MList[i].MPerBS, tmp.MList[i].MPerCS, tmp.MList[i].MPerDS
                                    , tmp.MList[i].MPerBE, tmp.MList[i].MPerCE, tmp.MList[i].MPerDE);
                            }
                        }
                        break;
                    case EnumGroupType.FlowRatePer:
                        {
                            FlowRatePer tmp = (FlowRatePer)it;
                            for (int i = 0; i < tmp.MList.Count; i++)
                            {
                                AddStep(ReadXaml.GetResources("labMEE_GS") + "_(" + (i + 1) + ")"
                                    , tmp.MList[i].MBaseTVCV
                                    , tmp.MList[i].MPerBS, tmp.MList[i].MPerCS, tmp.MList[i].MPerDS
                                    , tmp.MList[i].MPerBE, tmp.MList[i].MPerCE, tmp.MList[i].MPerDE);
                            }
                        }
                        break;
                    case EnumGroupType.PHCDUVUntil:
                        {
                            PHCDUVUntil tmp = (PHCDUVUntil)it;
                            switch (tmp.MUntilType)
                            {
                                case EnumPHCDUVUntil.Total:
                                    AddStep("labMECW_Wash", tmp.MTotalTVCV);
                                    break;
                                case EnumPHCDUVUntil.Met:
                                    AddStep("labMECW_CCW", tmp.MMaxTVCV);
                                    break;
                            }
                        }
                        break;
                    case EnumGroupType.CIP:
                        {
                            CIP tmp = (CIP)it;
                            if (0 == tmp.MFlowRate.MFlowVolLen.MFlowVol)
                            {
                                AddStep("", 0, 0, 0);
                                error = MNamePhase + Share.ReadXaml.GetResources("ME_Msg_NullFlowRate");
                            }
                            else
                            {
                                AddStep("", tmp.MVolumeTotal / tmp.MFlowRate.MFlowVolLen.MFlowVol, tmp.MVolumeTotal, tmp.MVolumeTotal / columnVol);
                            }
                        }
                        break;
                }
            }

            return error;
        }

        public override bool Compare(BasePhase baseItem)
        {
            DlyPhase item = (DlyPhase)baseItem;
            if (MListGroup.Count != item.MListGroup.Count)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < MListGroup.Count; i++)
                {
                    if (!MListGroup[i].Compare(item.MListGroup[i]))
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
