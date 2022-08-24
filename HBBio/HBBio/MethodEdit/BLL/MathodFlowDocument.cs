using HBBio.Communication;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace HBBio.MethodEdit
{
    class MathodFlowDocument
    {
        /// <summary>
        /// 生成流文档文件
        /// </summary>
        /// <returns></returns>
        public FlowDocument GetFlowDocument(Method method)
        {
            FlowDocument doc = new FlowDocument();
            AddMethodSetting(doc, method);
            foreach (var it in method.MPhaseList)
            {
                AddBlock(doc, it.MNamePhase, 0);
                switch (it.MType)
                {
                    case EnumPhaseType.Miscellaneous:
                        AddPhaseMiscellaneous(doc, (Miscellaneous)it, method.MMethodSetting.MBaseUnitStr);
                        break;
                    default:
                        foreach (var itt in it.MListGroup)
                        {
                            switch (itt.MType)
                            {
                                case EnumGroupType.TVCV:
                                case EnumGroupType.PHCDUVUntil:
                                    break;
                                default:
                                    AddBlock(doc, ReadXaml.GetEnum(itt.MType, "ME_EnumGroupType_"), 1);
                                    break;
                            }

                            switch (itt.MType)
                            {
                                case EnumGroupType.FlowRate:
                                    AddGroupFlowRate(doc, itt, method.MMethodSetting.MFlowRateUnitStr);
                                    break;
                                case EnumGroupType.ValveSelection:
                                    AddGroupValveSelection(doc, itt);
                                    break;
                                case EnumGroupType.Mixer:
                                    AddGroupMixer(doc, itt);
                                    break;
                                case EnumGroupType.BPV:
                                    AddGroupBPV(doc, itt);
                                    break;
                                case EnumGroupType.UVReset:
                                    AddGroupUVReset(doc, itt);
                                    break;
                                case EnumGroupType.SampleApplicationTech:
                                    AddGroupSampleApplicationTech(doc, itt, method.MMethodSetting.MBaseStr, method.MMethodSetting.MBaseUnitStr);
                                    break;
                                case EnumGroupType.TVCV:
                                    AddGroupTVCV(doc, itt, method.MMethodSetting.MBaseStr, method.MMethodSetting.MBaseUnitStr);
                                    break;
                                case EnumGroupType.FlowValveLength:
                                    AddGroupFlowValveLength(doc, itt, method.MMethodSetting.MBaseStr, method.MMethodSetting.MBaseUnitStr, method.MMethodSetting.MFlowRateUnitStr);
                                    break;
                                case EnumGroupType.FlowRatePer:
                                    AddGroupFlowRatePer(doc, itt, method.MMethodSetting.MBaseStr, method.MMethodSetting.MBaseUnitStr);
                                    break;
                                case EnumGroupType.PHCDUVUntil:
                                    AddGroupPHCDUVUntil(doc, itt, method.MMethodSetting.MBaseUnitStr);
                                    break;
                                case EnumGroupType.CollValveCollector:
                                    AddGroupCollValveCollector(doc, itt);
                                    break;
                                case EnumGroupType.CIP:
                                    AddGroupCIP(doc, itt, method.MMethodSetting.MFlowRateUnitStr);
                                    break;
                            }
                        }
                        break;
                }
            }
            doc.DataContext = method;
            return doc;
        }

        /// <summary>
        /// 添加方法设置数据
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="itemSets"></param>
        private void AddMethodSetting(FlowDocument doc, Method method)
        {
            MethodSettings methodSettings = method.MMethodSetting;

            AddBlock(doc, ReadXamlMethod.S_MethodSettings, 0);

            //基本信息
            AddBlock(doc, ReadXaml.GetResources("ME_BasicInformation"), 1);
            AddBlock(doc, ReadXaml.GetResources("A_UserName1") + methodSettings.MUserName, 2);
            AddBlock(doc, ReadXaml.GetResources("ME_CreateTime1") + methodSettings.MCreateTime, 2);
            AddBlock(doc, ReadXaml.GetResources("ME_ModifyTime1") + methodSettings.MModifyTime, 2);

            //循环设置
            AddBlock(doc, ReadXaml.GetResources("ME_LoopSettings"), 1);
            AddBlock(doc, ReadXaml.GetResources("ME_LoopSet1") + methodSettings.MLoop, 2);

            //色谱柱选择
            AddBlock(doc, ReadXaml.GetResources("ME_ColumnSelection"), 1);
            if (-1 != methodSettings.MColumnId)
            {
                ColumnList.ColumnManager columnManager = new ColumnList.ColumnManager();
                List<ColumnList.ColumnItem> listColName = null;
                columnManager.GetNameList(out listColName);
                foreach (var it in listColName)
                {
                    if (it.MID == methodSettings.MColumnId)
                    {
                        AddBlock(doc, ReadXaml.GetResources("ME_ColumnName1") + it.MName, 2);
                        break;
                    }
                }
            }
            AddBlock(doc, ReadXaml.GetResources("ME_ColumnVolume1") + methodSettings.MColumnVol + DlyBase.SC_VUNITML, 2);
            if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.CPV_1])
            {
                AddBlock(doc, ReadXaml.GetResources("labCPV1") + EnumCPVInfo.NameList[methodSettings.MCPV], 2);
            }

            //单位选择
            AddBlock(doc, ReadXaml.GetResources("ME_UnitSelection"), 1);
            AddBlock(doc, ReadXaml.GetResources("ME_MethodBaseUnit1") + ReadXaml.GetEnumList<EnumBase>()[(int)methodSettings.MBaseUnit], 2);
            AddBlock(doc, ReadXaml.GetResources("ME_FlowRateUnit1") + EnumFlowRateUnitInfo.NameList[(int)methodSettings.MFlowRateUnit], 2);

            //流速
            AddBlock(doc, ReadXaml.GetResources("labFlowRate"), 1);
            AddBlock(doc, methodSettings.MFlowRate.ToString() + methodSettings.MFlowRateUnitStr, 2);

            //阀路选择
            if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.InA]
                || Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.InB]
                || Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.InC]
                || Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.InD]
                || Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.BPV])
            {
                AddBlock(doc, ReadXaml.GetResources("ME_ValveSelection"), 1);
                if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.InA])
                {
                    AddBlock(doc, ReadXaml.GetResources("labInA1") + EnumInAInfo.NameList[methodSettings.MInA], 2);
                }
                if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.InB])
                {
                    AddBlock(doc, ReadXaml.GetResources("labInB1") + EnumInBInfo.NameList[methodSettings.MInB], 2);
                }
                if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.InC])
                {
                    AddBlock(doc, ReadXaml.GetResources("labInC1") + EnumInCInfo.NameList[methodSettings.MInC], 2);
                }
                if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.InD])
                {
                    AddBlock(doc, ReadXaml.GetResources("labInD1") + EnumInDInfo.NameList[methodSettings.MInD], 2);
                }
                if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.BPV])
                {
                    AddBlock(doc, ReadXaml.GetResources("labBPV1") + EnumBPVInfo.NameList[methodSettings.MBPV], 2);
                }
            }

            //紫外检测器设置
            if (Visibility.Visible == ItemVisibility.s_listUV[ENUMUVName.UV01])
            {
                AddBlock(doc, ReadXamlMethod.S_UVSettings, 1);
                AddBlock(doc, ReadXaml.GetResources("labUVLamp1")
                    + (methodSettings.MUVValue.MOnoff ? ReadXaml.GetResources("labUVOn") : ReadXaml.GetResources("labUVOff"))
                    + (methodSettings.MUVValue.MClear ? ReadXaml.GetResources("labUVReset") : ""), 2);
                AddBlock(doc, ReadXaml.S_UVWaveLength1
                    + "[" + StaticValue.s_minWaveLength + "-" + StaticValue.s_maxWaveLength + DlyBase.SC_UVWAVEUNIT + "]"
                    + " UV1:" + methodSettings.MUVValue.MWave1
                    + " UV2:" + methodSettings.MUVValue.MWave2
                    + (Visibility.Visible == StaticValue.s_waveVisible3 ? (" UV3:" + methodSettings.MUVValue.MWave3) : "")
                    + (Visibility.Visible == StaticValue.s_waveVisible4 ? (" UV4:" + methodSettings.MUVValue.MWave4) : ""), 2);
            }

            //气泡传感器设置
            if (Visibility.Visible == ItemVisibility.s_listAS[ENUMASName.AS01])
            {
                AddBlock(doc, ReadXaml.GetResources("ME_AirSensorAlarm"), 1);
                for (int i = 0; i < methodSettings.MASParaList.Count; i++)
                {
                    if (Visibility.Visible == ItemVisibility.s_listAS[(ENUMASName)i])
                    {
                        AddBlock(doc, "AS0" + (i + 1) + ":" + ReadXaml.GetEnum(methodSettings.MASParaList[i].MAction, "EnumMonitorAction_"), 2);
                    }
                }
            }

            //ME_AlarmWarning
            AddBlock(doc, ReadXaml.GetResources("ME_AlarmWarning"), 1);
            foreach(var it in methodSettings.MAlarmWarning.MList)
            {
                AddBlock(doc, it.MName + ":"
                    + it.MValLL + (EnumAlarmWarningMode.Disabled != it.MCheckLL ? ReadXaml.S_Enabled : ReadXaml.S_Disabled)
                    + it.MValL + (EnumAlarmWarningMode.Disabled != it.MCheckL ? ReadXaml.S_Enabled : ReadXaml.S_Disabled)
                    + it.MValH + (EnumAlarmWarningMode.Disabled != it.MCheckH ? ReadXaml.S_Enabled : ReadXaml.S_Disabled)
                    + it.MValHH + (EnumAlarmWarningMode.Disabled != it.MCheckHH ? ReadXaml.S_Enabled : ReadXaml.S_Disabled), 2);
            }

            //其它-结果名称
            AddBlock(doc, ReadXaml.GetResources("labOther"), 1);
            StringBuilder sb = new StringBuilder();
            sb.Append(ReadXaml.GetResources("ME_ResultName1"));
            sb.Append(ReadXaml.GetEnum(methodSettings.MResultName.MType, "ME_EnumResultType_"));
            switch (methodSettings.MResultName.MType)
            {
                case EnumResultType.NoName:
                    break;
                case EnumResultType.DlyName:
                    sb.Append(methodSettings.MResultName.MDlyName);
                    break;
                case EnumResultType.MethodName:
                    break;
                case EnumResultType.DateTime:
                    break;
            }
            if (methodSettings.MResultName.MUnique)
            {
                sb.Append("(" + ReadXaml.GetResources("ME_AddUniqueIdentifier") + ")");
            }
            AddBlock(doc, sb.ToString(), 2);

            //其它-启动协议
            if (!string.IsNullOrEmpty(methodSettings.MDefineQuestions.MQuestion))
            {
                AddBlock(doc, ReadXaml.GetResources("ME_Question1") + methodSettings.MDefineQuestions.MQuestion, 2);

                sb.Clear();
                sb.Append(ReadXaml.GetResources("ME_AnswerType1"));
                sb.Append(ReadXaml.GetEnum(methodSettings.MDefineQuestions.MType, "ME_EnumAnswerType_"));
                switch (methodSettings.MDefineQuestions.MType)
                {
                    case EnumAnswerType.TextualInput:
                        sb.Append("{" + ReadXaml.GetResources("ME_DefaultAnswer1") + methodSettings.MDefineQuestions.MDefaultAnswer + "}");
                        break;
                    case EnumAnswerType.NoAnswer:
                        break;
                    case EnumAnswerType.NumericValue:
                        sb.Append("{" + methodSettings.MDefineQuestions.MMin + " -> " + methodSettings.MDefineQuestions.MMax + "}");
                        break;
                    case EnumAnswerType.MultipleChoice:
                        sb.Append("{");
                        foreach (var it in methodSettings.MDefineQuestions.MChoiceList)
                        {
                            sb.Append(it + ";");
                        }
                        sb.Append("}");
                        break;
                }
                AddBlock(doc, sb.ToString(), 2);
            }

            //其它-方法记录
            if (!string.IsNullOrEmpty(methodSettings.MMethodNotes))
            {
                AddBlock(doc, ReadXaml.GetResources("ME_Notes1") + methodSettings.MMethodNotes, 2);
            }
        }

        /// <summary>
        /// 添加其它数据
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="itemSets"></param>
        /// <param name="item"></param>
        /// <param name="baseStr"></param>
        /// <param name="baseUnitStr"></param>
        /// <param name="flowrateUnitStr"></param>
        private void AddPhaseMiscellaneous(FlowDocument doc, Miscellaneous item, string baseUnitStr)
        {
            if (item.MEnableSetMark)
            {
                AddBlock(doc, ReadXaml.GetResources("ME_SetMark") + item.MSetMark, 1);
            }

            if (item.MEnableMethodDelay)
            {
                AddBlock(doc, ReadXaml.GetResources("ME_MethodDelay") + item.MMethodDelay.MTVCV + baseUnitStr, 1);
            }

            if (item.MEnableMessage)
            {
                if (item.MEnablePauseAfterMessage)
                {
                    AddBlock(doc, ReadXaml.GetResources("ME_Message") + item.MMessage + ReadXaml.GetResources("ME_PauseAfterMessage"), 1);
                }
                else
                {
                    AddBlock(doc, ReadXaml.GetResources("ME_Message") + item.MMessage, 1);
                }
            }

            if (item.MEnablePauseTimer)
            {
                AddBlock(doc, ReadXaml.GetResources("ME_PauseTimer") + item.MPauseTimer, 1);
            }
        }

        private void AddGroupFlowValveLength(FlowDocument doc, BaseGroup baseGroup, string baseStr, string baseUnitStr, string flowrateUnitStr)
        {
            FlowValveLength item = (FlowValveLength)baseGroup;

            List list = new List();
            foreach (var it in item.MList)
            {
                StringBuilderSplit sb = new StringBuilderSplit(";");
                sb.Append(ReadXaml.GetResources("labNote") + ":" + it.MNote);
                if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.InA])
                {
                    sb.Append(ReadXaml.GetResources("labInA") + ":" + EnumInAInfo.NameList[it.MInA]);
                }
                if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.InB])
                {
                    sb.Append(ReadXaml.GetResources("labInB") + ":" + EnumInBInfo.NameList[it.MInB]);
                }
                if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.InC])
                {
                    sb.Append(ReadXaml.GetResources("labInC") + ":" + EnumInCInfo.NameList[it.MInC]);
                }
                if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.InD])
                {
                    sb.Append(ReadXaml.GetResources("labInD") + ":" + EnumInDInfo.NameList[it.MInD]);
                }
                if (Visibility.Visible == ItemVisibility.s_listPump[ENUMPumpName.FITB])
                {
                    sb.Append(ReadXaml.GetResources("LabBS") + ":" + it.MPerBS);
                    sb.Append(ReadXaml.GetResources("LabBE") + ":" + it.MPerBE);
                }
                if (Visibility.Visible == ItemVisibility.s_listPump[ENUMPumpName.FITC])
                {
                    sb.Append(ReadXaml.GetResources("LabCS") + ":" + it.MPerCS);
                    sb.Append(ReadXaml.GetResources("LabCE") + ":" + it.MPerCE);
                }
                if (Visibility.Visible == ItemVisibility.s_listPump[ENUMPumpName.FITD])
                {
                    sb.Append(ReadXaml.GetResources("LabDS") + ":" + it.MPerDS);
                    sb.Append(ReadXaml.GetResources("LabDE") + ":" + it.MPerDE);
                }
                sb.Append(ReadXaml.GetResources("labFillSystem") + ":" + (it.MFillSystem ? ReadXaml.S_Yes : ReadXaml.S_No));
                sb.Append(baseStr + "(" + baseUnitStr + ")" + ":" + it.MBaseTVCV.MTVCV);
                sb.Append(ReadXaml.GetResources("labFlowRate") + "(" + flowrateUnitStr + ")" + ":" + it.MFlowVolLen.MFlowRate);
                if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.BPV])
                {
                    sb.Append(ReadXaml.GetResources("labBPV") + ":" + EnumBPVInfo.NameList[it.MBPV]);
                }
                if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.Out])
                {
                    sb.Append(ReadXaml.GetResources("labOut") + ":" + EnumOutInfo.NameList[it.MVOut]);
                }
                sb.Append(ReadXaml.GetResources("ME_IncubationTime") + ":" + it.MIncubation);
                list.ListItems.Add(AddListItem(sb.ToString(), 2));
            }

            doc.Blocks.Add(list);
        }

        private void AddGroupFlowRate(FlowDocument doc, BaseGroup baseGroup, string flowrateUnitStr)
        {
            FlowRate item = (FlowRate)baseGroup;

            if (item.MEnableSameMS)
            {
                AddBlock(doc, ReadXaml.GetResources("ME_SameMSFlowRate"), 2);
            }
            AddBlock(doc, ReadXaml.GetResources("labFlowRate1") + item.MFlowVolLen.MFlowRate + flowrateUnitStr, 2);
        }

        private void AddGroupMixer(FlowDocument doc, BaseGroup baseGroup)
        {
            Mixer item = (Mixer)baseGroup;

            AddBlock(doc, ReadXaml.GetResources("labMixer1") + (item.MOnoff ? ReadXaml.S_On : ReadXaml.S_Off), 2);
        }

        private void AddGroupBPV(FlowDocument doc, BaseGroup baseGroup)
        {
            BPVValve item = (BPVValve)baseGroup;

            AddBlock(doc, ReadXaml.GetResources("labBPV1") + EnumBPVInfo.NameList[item.MBPV], 2);
        }

        private void AddGroupUVReset(FlowDocument doc, BaseGroup baseGroup)
        {
            UVReset item = (UVReset)baseGroup;

            if (item.MEnableResetUV)
            {
                AddBlock(doc, ReadXaml.GetResources("ME_ResetUVMonitor"), 2);
            }          
        }

        private void AddGroupSampleApplicationTech(FlowDocument doc, BaseGroup baseGroup, string baseStr, string baseUnitStr)
        {
            SampleApplicationTech item = (SampleApplicationTech)baseGroup;

            AddBlock(doc, ReadXaml.GetEnum(item.MEnumSAT, "ME_EnumSAT_"), 2);
            switch (item.MEnumSAT)
            {
                case EnumSAT.ManualLoopFilling:
                    AddBlock(doc, ReadXaml.GetResources("ME_EmptyLoopWith1") + item.MEmptyLoopWith + DlyBase.SC_VUNITML, 3);
                    break;
                case EnumSAT.SamplePumpLoopFilling:
                    AddBlock(doc, ReadXaml.GetResources("ME_NTSPWBWWSPTSA"), 3);
                    AddBlock(doc, ReadXaml.GetResources("labInS1") + EnumInSInfo.NameList[item.MInS], 3);
                    AddBlock(doc, ReadXaml.GetResources("ME_FillLoopWith1") + item.MFillLoopWith + DlyBase.SC_VUNITML, 3);
                    AddBlock(doc, ReadXaml.GetResources("ME_EmptyLoopWith1") + item.MEmptyLoopWith + DlyBase.SC_VUNITML, 3);
                    break;
                case EnumSAT.ISDOC:
                    AddBlock(doc, ReadXaml.GetResources("ME_NTSPWBWWSPTSA"), 3);
                    AddBlock(doc, ReadXaml.GetResources("labInS1") + EnumInSInfo.NameList[item.MInS], 3);
                    AddBlock(doc, ReadXaml.GetResources("ME_Sample1") + baseStr + " : " + item.MSampleTVCV + baseUnitStr, 3);
                    break;
            }
        }

        private void AddGroupTVCV(FlowDocument doc, BaseGroup baseGroup, string baseStr, string baseUnitStr)
        {
            BaseTVCV item = (BaseTVCV)baseGroup;

            AddBlock(doc, item.MHeaderText + baseStr, 1);

            AddBlock(doc, item.MTVCV + baseUnitStr, 2);
        }

        private void AddGroupFlowRatePer(FlowDocument doc, BaseGroup baseGroup, string baseStr, string baseUnitStr)
        {
            FlowRatePer item = (FlowRatePer)baseGroup;

            List list = new List();
            foreach (var it in item.MList)
            {
                StringBuilderSplit sb = new StringBuilderSplit(";");
                if (Visibility.Visible == ItemVisibility.s_listPump[ENUMPumpName.FITB])
                {
                    sb.Append(ReadXaml.GetResources("LabBS") + ":" + it.MPerBS);
                    sb.Append(ReadXaml.GetResources("LabBE") + ":" + it.MPerBE);
                }
                if (Visibility.Visible == ItemVisibility.s_listPump[ENUMPumpName.FITC])
                {
                    sb.Append(ReadXaml.GetResources("LabCS") + ":" + it.MPerCS);
                    sb.Append(ReadXaml.GetResources("LabCE") + ":" + it.MPerCE);
                }
                if (Visibility.Visible == ItemVisibility.s_listPump[ENUMPumpName.FITD])
                {
                    sb.Append(ReadXaml.GetResources("LabDS") + ":" + it.MPerDS);
                    sb.Append(ReadXaml.GetResources("LabDE") + ":" + it.MPerDE);
                }
                sb.Append(ReadXaml.GetResources("labFillSystem") + ":" + (it.MFillSystem ? ReadXaml.S_Yes : ReadXaml.S_No));
                sb.Append(baseStr + "(" + baseUnitStr + ")" + ":" + it.MBaseTVCV.MTVCV);
                list.ListItems.Add(AddListItem(sb.ToString(), 2));
            }

            doc.Blocks.Add(list);
        }

        private void AddGroupValveSelection(FlowDocument doc, BaseGroup baseGroup)
        {
            ValveSelection item = (ValveSelection)baseGroup;

            if (item.MEnableSameMS)
            {
                AddBlock(doc, ReadXaml.GetResources("ME_SameMSValve"), 2);
            }

            if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.InA])
            {
                AddBlock(doc, ReadXaml.GetResources("labInA1") + EnumInAInfo.NameList[item.MInA], 2);
            }
            if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.InB])
            {
                AddBlock(doc, ReadXaml.GetResources("labInB1") + EnumInAInfo.NameList[item.MInB], 2);
            }
            if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.InC])
            {
                AddBlock(doc, ReadXaml.GetResources("labInC1") + EnumInAInfo.NameList[item.MInC], 2);
            }
            if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.InD])
            {
                AddBlock(doc, ReadXaml.GetResources("labInD1") + EnumInAInfo.NameList[item.MInD], 2);
            }
            if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.BPV])
            {
                AddBlock(doc, ReadXaml.GetResources("labBPV1") + EnumInAInfo.NameList[item.MBPV], 2);
            }

            if (item.MVisibPer)
            {
                if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.InB])
                {
                    AddBlock(doc, item.MPerB + " %B", 2);
                }
                if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.InC])
                {
                    AddBlock(doc, item.MPerC + " %C", 2);
                }
                if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.InD])
                {
                    AddBlock(doc, item.MPerD + " %D", 2);
                }
            }

            if (item.MVisibWash)
            {
                if (item.MEnableWash)
                {
                    AddBlock(doc, ReadXaml.GetResources("ME_FTSWTSB"), 2);
                }
            } 
        }

        private void AddGroupPHCDUVUntil(FlowDocument doc, BaseGroup baseGroup, string baseUnitStr)
        {
            PHCDUVUntil item = (PHCDUVUntil)baseGroup;

            AddBlock(doc, ReadXaml.GetEnum(item.MType, "ME_EnumGroupType_") + item.MHeaderText, 1);

            switch (item.MUntilType)
            {
                case EnumPHCDUVUntil.Total:
                    AddBlock(doc, ReadXaml.GetResources("ME_TheTotalIs1") + item.MTotalTVCV.MTVCV + baseUnitStr, 2);
                    break;
                case EnumPHCDUVUntil.Met:
                    AddBlock(doc, ReadXaml.GetResources("ME_TFCIM1"), 2);
                    AddBlock(doc, ReadXaml.GetResources("ME_SignalSelection1") + EnumMonitorInfo.NameList[item.MMonitorIndex], 2);
                    AddBlock(doc, ReadXaml.GetResources("ME_JudgmentLogic1") + ReadXaml.GetEnumList<EnumJudge>()[(int)item.MJudgeIndex], 2);
                    if (EnumMonitorInfo.NameList[item.MMonitorIndex].Contains("pH"))
                    {
                        AddBlock(doc, ReadXaml.GetResources("ME_PHMoreThan1") + item.MMoreThan, 2);
                        AddBlock(doc, ReadXaml.GetResources("ME_PHLessThan1") + item.MLessThan, 2);
                    }
                    else if (EnumMonitorInfo.NameList[item.MMonitorIndex].Contains("Cd"))
                    {
                        AddBlock(doc, ReadXaml.GetResources("ME_CDMoreThan1") + item.MMoreThan, 2);
                        AddBlock(doc, ReadXaml.GetResources("ME_CDLessThan1") + item.MLessThan, 2);
                    }
                    else if (EnumMonitorInfo.NameList[item.MMonitorIndex].Contains("UV"))
                    {
                        AddBlock(doc, ReadXaml.GetResources("ME_UVMoreThan1") + item.MMoreThan, 2);
                        AddBlock(doc, ReadXaml.GetResources("ME_UVLessThan1") + item.MLessThan, 2);
                    }
                    else { }
                    AddBlock(doc, ReadXaml.GetResources("ME_StabilityTime1") + item.MStabilityTime, 2);
                    AddBlock(doc, ReadXaml.GetResources("ME_MaximumWash1") + item.MMaxTVCV.MTVCV + baseUnitStr, 2);
                    break;

            }
        }

        private void AddGroupCollValveCollector(FlowDocument doc, BaseGroup baseGroup)
        {
            CollValveCollector item = (CollValveCollector)baseGroup;

            AddBlock(doc, ReadXaml.GetEnum(item.MEnum, "Coll_EnumCollectionType_"), 2);
            switch (item.MEnum)
            {
                case Collection.EnumCollectionType.Waste:
                    break;
                case Collection.EnumCollectionType.Valve:
                    foreach(var it in item.MValve.MList)
                    {
                        AddBlock(doc, it.MShowInfo, 3);
                    }
                    break;
                case Collection.EnumCollectionType.Collector:
                    foreach (var it in item.MCollector.MList)
                    {
                        AddBlock(doc, it.MShowInfo, 3);
                    }
                    break;
            }
        }

        private void AddGroupCIP(FlowDocument doc, BaseGroup baseGroup, string flowrateUnitStr)
        {
            CIP item = (CIP)baseGroup;

            AddBlock(doc, ReadXaml.GetResources("labNote1") + item.MNote, 2);

            if (item.MPause)
            {
                AddBlock(doc, ReadXaml.GetResources("ME_PTMMAITTSS"), 2);
            }

            AddGroupFlowRate(doc, item.MFlowRate, flowrateUnitStr);

            AddBlock(doc, ReadXaml.GetResources("ME_VolumePerPosition1") + item.MVolumePerPosition + DlyBase.SC_VUNITML, 2);

            StringBuilderSplit sb = new StringBuilderSplit(" ");
            if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.InA])
            {
                sb.Clear();
                sb.Append(ReadXaml.GetResources("labInA1"));
                foreach (var it in item.MListInA)
                {
                    if (it.MIsSelected)
                    {
                        sb.Append(it.MValveName);
                    }
                }
                AddBlock(doc, sb.ToString(), 2);
            }

            if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.InB])
            {
                sb.Clear();
                sb.Append(ReadXaml.GetResources("labInB1"));
                foreach (var it in item.MListInB)
                {
                    if (it.MIsSelected)
                    {
                        sb.Append(it.MValveName);
                    }
                }
                AddBlock(doc, sb.ToString(), 2);
            }

            if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.InC])
            {
                sb.Clear();
                sb.Append(ReadXaml.GetResources("labInC1"));
                foreach (var it in item.MListInC)
                {
                    if (it.MIsSelected)
                    {
                        sb.Append(it.MValveName);
                    }
                }
                AddBlock(doc, sb.ToString(), 2);
            }

            if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.InD])
            {
                sb.Clear();
                sb.Append(ReadXaml.GetResources("labInD1"));
                foreach (var it in item.MListInD)
                {
                    if (it.MIsSelected)
                    {
                        sb.Append(it.MValveName);
                    }
                }
                AddBlock(doc, sb.ToString(), 2);
            }

            if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.InS])
            {
                sb.Clear();
                sb.Append(ReadXaml.GetResources("labInS1"));
                foreach (var it in item.MListInS)
                {
                    if (it.MIsSelected)
                    {
                        sb.Append(it.MValveName);
                    }
                }
                AddBlock(doc, sb.ToString(), 2);
            }

            if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.CPV_1])
            {
                sb.Clear();
                sb.Append(ReadXaml.GetResources("labCPV1"));
                foreach (var it in item.MListCPV)
                {
                    if (it.MIsSelected)
                    {
                        sb.Append(it.MValveName);
                    }
                }
                AddBlock(doc, sb.ToString(), 2);
            }

            if (Visibility.Visible == ItemVisibility.s_listValve[ENUMValveName.Out])
            {
                sb.Clear();
                sb.Append(ReadXaml.GetResources("labOut1"));
                foreach (var it in item.MListOut)
                {
                    if (it.MIsSelected)
                    {
                        sb.Append(it.MValveName);
                    }
                }
                AddBlock(doc, sb.ToString(), 2);
            }

            AddBlock(doc, ReadXaml.GetResources("ME_ESVUITP1") + item.MVolumeTotal + DlyBase.SC_VUNITML, 2);
        }

        private void AddBlock(FlowDocument doc, string text, int space)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < space; i++)
            {
                sb.Append("\t");
            }
            sb.Append(text);
            doc.Blocks.Add(new Paragraph(new Run(sb.ToString())));
        }

        private ListItem AddListItem(string text, int space)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < space; i++)
            {
                sb.Append("\t");
            }
            sb.Append(text);

            return new ListItem(new Paragraph(new Run(sb.ToString())));
        }
    }
}
