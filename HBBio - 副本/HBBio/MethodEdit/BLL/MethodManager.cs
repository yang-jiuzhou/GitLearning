using HBBio.ColumnList;
using HBBio.Communication;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: MethodManager
     * Description: 方法管理
     * Version: 1.0
     * Create:  2020/11/12
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    class MethodManager
    {
        /// <summary>
        /// 新建方法
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string AddMethod(Method item)
        {
            string error = CheckDate(item);
            if (null != error)
            {
                return error;
            }

            MethodTable table = new MethodTable();
            error = table.InsertRow(item);
            if (null == error)
            {
                int id = -1;
                error = table.GetLastID(out id);
                if (null == error)
                {
                    item.MID = id;
                }
            }

            return error;
        }

        /// <summary>
        /// 新建方法序列
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string AddMethodQueue(MethodQueue item)
        {
            MethodTable table = new MethodTable();
            string error = table.InsertRow(item);
            if (null == error)
            {
                int id = -1;
                error = table.GetLastID(out id);
                if (null == error)
                {
                    item.MID = id;
                }
            }

            return error;
        }

        /// <summary>
        /// 删除方法或者方法序列
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DelMethod(int id)
        {
            MethodTable table = new MethodTable();
            return table.DeleteRow(id);
        }

        /// <summary>
        /// 修改方法或者方法序列的名称
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string UpdateMethodName(MethodType item)
        {
            MethodTable table = new MethodTable();
            return table.UpdateMethodName(item);
        }

        /// <summary>
        /// 修改方法
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string UpdateMethod(Method item)
        {
            string error = CheckDate(item);
            if (null != error)
            {
                return error;
            }

            MethodTable table = new MethodTable();
            return table.UpdateMethod(item);
        }

        /// <summary>
        /// 修改方法序列
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string UpdateMethodQueue(MethodQueue item)
        {
            MethodTable table = new MethodTable();
            return table.UpdateMethod(item);
        }

        /// <summary>
        /// 获取指定ID的方法类型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetMethodType(int id, out EnumMethodType type)
        {
            MethodTable table = new MethodTable();
            return table.SelectRowType(id, out type);
        }

        /// <summary>
        /// 获取指定ID的方法
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetMethod(int id, out Method item)
        {
            MethodTable table = new MethodTable();
            string error = table.SelectRow(id, out item);
            if (null == error)
            {
                for (int i = 0; i < item.MPhaseList.Count; i++)
                {
                    item.MPhaseList[i].StatisticsAllStep(item.MMethodSetting.MColumnVol);
                }
            }

            return error;
        }

        /// <summary>
        /// 获取指定ID的方法序列
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetMethodQueue(int id, out MethodQueue item)
        {
            MethodTable table = new MethodTable();
            return table.SelectRow(id, out item);
        }

        /// <summary>
        /// 获取全部方法和方法序列的列表
        /// </summary>
        /// <param name="communicationSetsID"></param>
        /// <param name="projectID"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public string GetListName(int communicationSetsID, int projectID, string filter, out List<MethodType> list)
        {
            MethodTable table = new MethodTable();
            return table.SelectListName(communicationSetsID, projectID, filter, out list);
        }

        /// <summary>
        /// 获取全部方法的列表
        /// </summary>
        /// <param name="communicationSetsID"></param>
        /// <param name="projectID"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public string GetListNameMethod(int communicationSetsID, int projectID, out List<MethodType> list)
        {
            string error = GetListName(communicationSetsID, projectID, null, out list);
            if (null == error)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (EnumMethodType.MethodQueue == list[i].MType)
                    {
                        list.RemoveAt(i);
                    }
                }
            }

            if (0 == list.Count)
            {
                error = Share.ReadXaml.S_ErrorNoData;
            }

            return error;
        }

        /// <summary>
        /// 添加临时数据
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string AddMethodTemp(MethodTemp item)
        {
            MethodTempTable table = new MethodTempTable();
            return table.AddRow(item);
        }

        /// <summary>
        /// 删除临时数据
        /// </summary>
        /// <returns></returns>
        public string DelMethodTemp()
        {
            MethodTempTable table = new MethodTempTable();
            return table.DelRow();
        }

        /// <summary>
        /// 获取临时数据
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetMethodTemp(out MethodTemp item)
        {
            MethodTempTable table = new MethodTempTable();
            return table.SelectRow(out item);
        }

        public string AddPhase(string name, string info)
        {
            PhaseTable table = new PhaseTable();
            return table.InsertRow(name, info);
        }

        public string DelPhase(string name)
        {
            PhaseTable table = new PhaseTable();
            return table.DeleteRow(name);
        }

        /// <summary>
        /// 获取行信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetPhase(string name, ref string info)
        {
            PhaseTable table = new PhaseTable();
            return table.GetRow(name, ref info);
        }

        /// <summary>
        /// 获取完整表
        /// </summary>
        /// <returns></returns>
        public string GetPhaseNameList(out List<string> list, string filter = null)
        {
            PhaseTable table = new PhaseTable();
            return table.GetNameList(out list, filter);
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string CheckDate(Method item)
        {
            foreach (BasePhase basePhase in item.MPhaseList)
            {
                double flowVol = item.MMethodSetting.MFlowVol;
                foreach (BaseGroup baseGroup in basePhase.MListGroup)
                {
                    switch (baseGroup.MType)
                    {
                        case EnumGroupType.FlowRate:
                            {
                                flowVol = ((FlowRate)baseGroup).MFlowVolLen.MFlowVol;
                            }
                            break;
                        case EnumGroupType.ValveSelection:
                            {
                                ValveSelection valveSelection = (ValveSelection)baseGroup;
                                if (valveSelection.MVisibPer)
                                {
                                    string error = StaticValue.CheckData(EnumFlowRate.MLMIN, flowVol, valveSelection.MPerB, valveSelection.MPerC, valveSelection.MPerD);
                                    if (!string.IsNullOrEmpty(error))
                                    {
                                        StringBuilderSplit sb = new StringBuilderSplit();
                                        sb.Append(basePhase.MNamePhase);
                                        sb.Append(ReadXaml.GetEnum(baseGroup.MType, "ME_EnumGroupType_"));
                                        sb.Append(error);
                                        return sb.ToString();
                                    }
                                }
                            }
                            break;
                        case EnumGroupType.FlowValveLength:
                            {
                                FlowValveLength flowValveLength = (FlowValveLength)baseGroup;
                                for (int i = 0; i < flowValveLength.MList.Count; i++)
                                {
                                    string error = StaticValue.CheckData(EnumFlowRate.MLMIN, flowValveLength.MList[i].MFlowVolLen.MFlowVol
                                        , flowValveLength.MList[i].MPerBS, flowValveLength.MList[i].MPerBE
                                        , flowValveLength.MList[i].MPerCS, flowValveLength.MList[i].MPerCE
                                        , flowValveLength.MList[i].MPerDS, flowValveLength.MList[i].MPerDE);
                                    if (!string.IsNullOrEmpty(error))
                                    {
                                        StringBuilderSplit sb = new StringBuilderSplit();
                                        sb.Append(basePhase.MNamePhase);
                                        sb.Append(ReadXaml.GetEnum(baseGroup.MType, "ME_EnumGroupType_"));
                                        sb.Append(flowValveLength.MList.Count + " - " + (i + 1));
                                        sb.Append(error);
                                        return sb.ToString();
                                    }
                                }
                            }
                            break;
                        case EnumGroupType.FlowRatePer:
                            {
                                FlowRatePer flowRatePer = (FlowRatePer)baseGroup;
                                for (int i = 0; i < flowRatePer.MList.Count; i++)
                                {
                                    string error = StaticValue.CheckData(EnumFlowRate.MLMIN, flowVol
                                        , flowRatePer.MList[i].MPerBS, flowRatePer.MList[i].MPerBE
                                        , flowRatePer.MList[i].MPerCS, flowRatePer.MList[i].MPerCE
                                        , flowRatePer.MList[i].MPerDS, flowRatePer.MList[i].MPerDE);
                                    if (!string.IsNullOrEmpty(error))
                                    {
                                        StringBuilderSplit sb = new StringBuilderSplit();
                                        sb.Append(basePhase.MNamePhase);
                                        sb.Append(ReadXaml.GetEnum(baseGroup.MType, "ME_EnumGroupType_"));
                                        sb.Append(flowRatePer.MList.Count + " - " + (i + 1));
                                        sb.Append(error);
                                        return sb.ToString();
                                    }
                                }
                            }
                            break;
                        case EnumGroupType.MixtureGrid:
                            {
                                MixtureGrid flowValveLength = (MixtureGrid)baseGroup;
                                for (int i = 0; i < flowValveLength.MList.Count; i++)
                                {
                                    string error = StaticValue.CheckData(EnumFlowRate.MLMIN, flowValveLength.MList[i].MFlowVolLenSystem.MFlowVol
                                        , flowValveLength.MList[i].MPerBS, flowValveLength.MList[i].MPerBE
                                        , flowValveLength.MList[i].MPerCS, flowValveLength.MList[i].MPerCE
                                        , flowValveLength.MList[i].MPerDS, flowValveLength.MList[i].MPerDE);
                                    if (!string.IsNullOrEmpty(error))
                                    {
                                        StringBuilderSplit sb = new StringBuilderSplit();
                                        sb.Append(basePhase.MNamePhase);
                                        sb.Append(ReadXaml.GetEnum(baseGroup.MType, "ME_EnumGroupType_"));
                                        sb.Append(flowValveLength.MList.Count + " - " + (i + 1));
                                        sb.Append(error);
                                        return sb.ToString();
                                    }
                                }
                            }
                            break;
                    }
                }
            }

            return null;
        }
    }
}
