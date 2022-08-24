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
     * ClassName: MethodSettingsVM
     * Description: 方法设置
     * Version: 1.0
     * Create:  2020/05/27
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    public class MethodSettingsVM : DlyNotifyPropertyChanged
    {
        public MethodSettings MItem 
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
                MResultName.MItem = value.MResultName;
                MDefineQuestions.MItem = value.MDefineQuestions;
            }
        }
        private MethodSettings m_item = new MethodSettings();

        /********基本信息********/

        /// <summary>
        /// 用户名
        /// </summary>
        public string MUserName 
        { 
            get
            {
                return MItem.MUserName;
            }
            set
            {
                MItem.MUserName = value;
            }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public string MCreateTime
        {
            get
            {
                return MItem.MCreateTime;
            }
            set
            {
                MItem.MCreateTime = value;
            }
        }
        /// <summary>
        /// 修改日期
        /// </summary>
        public string MModifyTime
        {
            get
            {
                return MItem.MModifyTime;
            }
            set
            {
                MItem.MModifyTime = value;
            }
        }

        /********循环次数********/
        public int MLoop 
        { 
            get
            {
                return MItem.MLoop;
            }
            set
            {
                MItem.MLoop = value;
            }
        }

        /********色谱柱参数********/

        /// <summary>
        /// 柱子ID 
        /// </summary>
        public int MColumnId
        {
            get
            {
                return MItem.MColumnId;
            }
            set
            {
                if (MItem.MColumnId == value)
                {
                    return;
                }

                MItem.MColumnId = value;

                ColumnManager manager = new ColumnManager();
                ColumnItem item = new ColumnItem();
                if (null == manager.GetColumn(MColumnId, item))
                {
                    switch (item.MRP.MList[(int)EnumRunParameters.RPCVU].MIndex)
                    {
                        case 0:
                            MColumnVol = item.MRP.MList[(int)EnumRunParameters.RPCV].MValue * 1000;
                            MColumnArea = ValueTrans.CalArea(item.MDT.MList[(int)EnumDetails.DHD].MValue);
                            MMaxFlowVol = item.MRP.MList[(int)EnumRunParameters.RPFIT].MValue * 1000 / 60;
                            MMaxFlowLen = item.MRP.MList[(int)EnumRunParameters.RPLFIT].MValue;
                            break;
                        case 1:
                            MColumnVol = item.MRP.MList[(int)EnumRunParameters.RPCV].MValue;
                            MColumnArea = ValueTrans.CalArea(item.MDT.MList[(int)EnumDetails.DHD].MValue);
                            MMaxFlowVol = item.MRP.MList[(int)EnumRunParameters.RPFIT].MValue;
                            MMaxFlowLen = item.MRP.MList[(int)EnumRunParameters.RPLFIT].MValue;
                            break;
                        case 2:
                            MColumnVol = item.MRP.MList[(int)EnumRunParameters.RPCV].MValue / 1000;
                            MColumnArea = ValueTrans.CalArea(item.MDT.MList[(int)EnumDetails.DHD].MValue);
                            MMaxFlowVol = item.MRP.MList[(int)EnumRunParameters.RPFIT].MValue / 1000;
                            MMaxFlowLen = item.MRP.MList[(int)EnumRunParameters.RPLFIT].MValue;
                            break;
                    }

                    switch (MFlowRateUnit)
                    {
                        case EnumFlowRate.MLMIN:
                            MMaxFlowRate = Math.Min(MMaxFlowVol, Communication.StaticValue.s_maxFlowVol);
                            MFlowRate = item.MRP.MList[(int)EnumRunParameters.RPDFIT].MValue;
                            MFlowRateUnitStr = DlyBase.SC_FITUNITML + "[" + 0 + "-" + MMaxFlowRate + "]";
                            break;
                        case EnumFlowRate.CMH:
                            MMaxFlowRate = Math.Min(MMaxFlowLen, Communication.StaticValue.s_maxFlowLen);
                            MFlowRate = item.MRP.MList[(int)EnumRunParameters.RPDLFIT].MValue;
                            MFlowRateUnitStr = DlyBase.SC_LINEARFITUNIT + "[" + 0 + "-" + MMaxFlowRate + "]";
                            break;
                    }

                    MAlarmWarning.SetPTColumn(item.MRP.MList[(int)EnumRunParameters.RPPPT].MValue);
                    MAlarmWarning.SetPTColumnDelta(item.MRP.MList[(int)EnumRunParameters.RPDPT].MValue);
                    MAlarmWarning.SetpH(item.MRP.MList[(int)EnumRunParameters.RPMINPHL].MValue
                        , item.MRP.MList[(int)EnumRunParameters.RPMINPHS].MValue
                        , item.MRP.MList[(int)EnumRunParameters.RPMAXPHS].MValue
                        , item.MRP.MList[(int)EnumRunParameters.RPMAXPHL].MValue);
                }
                else
                {
                    MColumnVol = 1;
                    MColumnArea = 1;
                    MMaxFlowVol = DlyBase.MAX;
                    MMaxFlowLen = DlyBase.MAX;

                    switch (MFlowRateUnit)
                    {
                        case EnumFlowRate.MLMIN:
                            MMaxFlowRate = Math.Min(MMaxFlowVol, Communication.StaticValue.s_maxFlowVol);
                            MFlowRateUnitStr = DlyBase.SC_FITUNITML + "[" + 0 + "-" + MMaxFlowRate + "]";
                            break;
                        case EnumFlowRate.CMH:
                            MMaxFlowRate = Math.Min(MMaxFlowLen, Communication.StaticValue.s_maxFlowLen);
                            MFlowRateUnitStr = DlyBase.SC_LINEARFITUNIT + "[" + 0 + "-" + MMaxFlowRate + "]";
                            break;
                    }

                    MAlarmWarning.ClearPT();
                    MAlarmWarning.ClearpH();
                    for (int i = 0; i < StaticAlarmWarning.SAlarmWarningOriginal.MList.Count; i++)
                    {
                        MAlarmWarning.MList[i].ResetValue(StaticAlarmWarning.SAlarmWarningOriginal.MList[i]);
                    }

                    //流速单位只能用ml/min
                    MFlowRateUnit = EnumFlowRate.MLMIN;
                }

                MFlowRateUnitHandler?.Invoke(MMethodBaseValue);
            }
        }
        /// <summary>
        /// 柱子体积
        /// </summary>
        public double MColumnVol
        {
            get
            {
                return MItem.MColumnVol;
            }
            set
            {
                if (MItem.MColumnVol == value)
                {
                    return;
                }

                MItem.MColumnVol = value;
                OnPropertyChanged("MColumnVol");

                MColumnVolHandler?.Invoke(MMethodBaseValue);
            }
        }
        /// <summary>
        /// 柱位阀
        /// </summary>
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
        /// <summary>
        /// 柱面积
        /// </summary>
        public double MColumnArea
        {
            get
            {
                return MItem.MColumnArea;
            }
            set
            {
                MItem.MColumnArea = value;
            }
        }
        /// <summary>
        /// 色谱柱最大体积流速
        /// </summary>
        public double MMaxFlowVol { get; set; }
        /// <summary>
        /// 色谱柱最大线性流速
        /// </summary>
        public double MMaxFlowLen { get; set; }
        /// <summary>
        /// 色谱柱最大流速
        /// </summary>
        public double MMaxFlowRate
        {
            get
            {
                return m_maxFlowRate;
            }
            set
            {
                if (m_maxFlowRate == value)
                {
                    return;
                }

                m_maxFlowRate = Math.Round(value, 2);
                OnPropertyChanged("MMaxFlowRate");
            }
        }
        private double m_maxFlowRate = DlyBase.MAX;

        /********单位选择********/

        /// <summary>
        /// 基本单位
        /// </summary>
        public EnumBase MBaseUnit
        {
            get
            {
                return MItem.MBaseUnit;
            }
            set
            {
                if (MItem.MBaseUnit == value)
                {
                    return;
                }

                EnumBase old = MItem.MBaseUnit;
                MItem.MBaseUnit = value;

                MBaseStr = ReadXaml.GetEnum(MBaseUnit);
                MBaseUnitStr = EnumBaseString.GetItemsSource()[(int)MBaseUnit].MName;

                MethodBaseValue tmp = MMethodBaseValue;
                tmp.MChangeBaseUnit = true;
                tmp.MEnumBaseOld = old;
                tmp.MEnumBaseNew = value;
                MBaseUnitHandler?.Invoke(tmp);
            }
        }
        /// <summary>
        /// 流速单位
        /// </summary>
        public EnumFlowRate MFlowRateUnit
        {
            get
            {
                return MItem.MFlowRateUnit;
            }
            set
            {
                if (MItem.MFlowRateUnit == value)
                {
                    return;
                }

                EnumFlowRate old = MItem.MFlowRateUnit;
                MItem.MFlowRateUnit = value;
                OnPropertyChanged("MFlowRateUnit");

                switch (value)
                {
                    case EnumFlowRate.MLMIN:
                        MFlowRate = Math.Round(MFlowRate * MColumnArea / 60, 2);
                        MMaxFlowRate = Math.Min(MMaxFlowVol, Communication.StaticValue.s_maxFlowVol);
                        MFlowRateUnitStr = DlyBase.SC_FITUNITML + "[" + 0 + "-" + MMaxFlowRate + "]";
                        break;
                    case EnumFlowRate.CMH:
                        MFlowRate = Math.Round(MFlowRate * 60 / MColumnArea, 2);
                        MMaxFlowRate = Math.Min(MMaxFlowLen, Communication.StaticValue.s_maxFlowLen);
                        MFlowRateUnitStr = DlyBase.SC_LINEARFITUNIT + "[" + 0 + "-" + MMaxFlowRate + "]";
                        break;
                }

                MethodBaseValue tmp = MMethodBaseValue;
                tmp.MChangeFlowRateUnit = true;
                tmp.MEnumFlowRateOld = old;
                tmp.MEnumFlowRateNew = value;
                MFlowRateUnitHandler?.Invoke(tmp);
            }
        }
        /// <summary>
        /// 基本单位对应的后缀字符串
        /// </summary>
        public string MBaseStr
        {
            get
            {
                return MItem.MBaseStr;
            }
            set
            {
                if (MItem.MBaseStr.Equals(value))
                {
                    return;
                }

                MItem.MBaseStr = value;
                OnPropertyChanged("MBaseStr");
            }
        }
        /// <summary>
        /// 基本单位对应的后缀字符串
        /// </summary>
        public string MBaseUnitStr
        {
            get
            {
                return MItem.MBaseUnitStr;
            }
            set
            {
                if (MItem.MBaseUnitStr.Equals(value))
                {
                    return;
                }

                MItem.MBaseUnitStr = value;
                OnPropertyChanged("MBaseUnitStr");
            }
        }

        /********流速设置********/

        /// <summary>
        /// 流速数值
        /// </summary>
        public double MFlowRate
        {
            get
            {
                return MItem.MFlowRate;
            }
            set
            {
                if (MItem.MFlowRate == value)
                {
                    return;
                }

                double old = MItem.MFlowRate;
                MItem.MFlowRate = value;
                OnPropertyChanged("MFlowRate");

                MethodBaseValue tmp = MMethodBaseValue;
                tmp.MChangeFlowRate = true;
                MFlowRateHandler?.Invoke(tmp);
            }
        }
        /// <summary>
        /// 流速单位对应的后缀字符串
        /// </summary>
        public string MFlowRateUnitStr
        {
            get
            {
                return MItem.MFlowRateUnitStr;
            }
            set
            {
                if (MItem.MFlowRateUnitStr.Equals(value))
                {
                    return;
                }

                MItem.MFlowRateUnitStr = value;
                OnPropertyChanged("MFlowRateUnitStr");
            }
        }

        /********入口阀选择********/

        /// <summary>
        /// 入口阀A
        /// </summary>
        public int MInA
        {
            get
            {
                return MItem.MInA;
            }
            set
            {
                MItem.MInA = value;

                MValveHandler?.Invoke(MMethodBaseValue);
            }
        }
        /// <summary>
        /// 入口阀B
        /// </summary>
        public int MInB
        {
            get
            {
                return MItem.MInB;
            }
            set
            {
                MItem.MInB = value;
                MValveHandler?.Invoke(MMethodBaseValue);
            }
        }
        /// <summary>
        /// 入口阀C
        /// </summary>
        public int MInC
        {
            get
            {
                return MItem.MInC;
            }
            set
            {
                MItem.MInC = value;
                MValveHandler?.Invoke(MMethodBaseValue);
            }
        }
        /// <summary>
        /// 入口阀D
        /// </summary>
        public int MInD
        {
            get
            {
                return MItem.MInD;
            }
            set
            {
                MItem.MInD = value;
                MValveHandler?.Invoke(MMethodBaseValue);
            }
        }
        /// <summary>
        /// 旁通阀
        /// </summary>
        public int MBPV
        {
            get
            {
                return MItem.MBPV;
            }
            set
            {
                MItem.MBPV = value;
                MValveHandler?.Invoke(MMethodBaseValue);
            }
        }

        /********紫外检测器********/

        /// <summary>
        /// 紫外检测器
        /// </summary>
        public UVValue MUVValue
        {
            get
            {
                return MItem.MUVValue;
            }
            set
            {
                MItem.MUVValue = value;
            }
        }

        /********气泡传感器********/

        /// <summary>
        /// 气泡传感器
        /// </summary>
        public List<ASMethodParaVM> MASParaList { get; set; }

        /********警报警告********/

        /// <summary>
        /// 警报警告
        /// </summary>
        public AlarmWarning MAlarmWarning
        {
            get
            {
                return MItem.MAlarmWarning;
            }
            set
            {
                MItem.MAlarmWarning = value;
            }
        }

        /********其它设置********/

        /// <summary>
        /// 结果名称
        /// </summary>
        public ResultNameVM MResultName { get; set; }
        /// <summary>
        /// 问题定义
        /// </summary>
        public DefineQuestionsVM MDefineQuestions { get; set; }
        /// <summary>
        /// 方法记录
        /// </summary>
        public string MMethodNotes
        {
            get
            {
                return MItem.MMethodNotes;
            }
            set
            {
                MItem.MMethodNotes = value;
            }
        }

        /********事件设置********/

        /// <summary>
        /// 基本参数集合
        /// </summary>
        public MethodBaseValue MMethodBaseValue
        {
            get
            {
                MethodBaseValue methodBaseValue = new MethodBaseValue();
                methodBaseValue.MEnumBaseOld = MBaseUnit;
                methodBaseValue.MEnumFlowRateOld = MFlowRateUnit;
                methodBaseValue.MFlowRate = MFlowRate;
                methodBaseValue.MMaxFlowRate = MMaxFlowRate;
                methodBaseValue.MColumnVol = MColumnVol;
                methodBaseValue.MColumnArea = MColumnArea;

                methodBaseValue.MBaseStr = MBaseStr;
                methodBaseValue.MBaseUnitStr = MBaseUnitStr;
                methodBaseValue.MFlowRateUnitStr = MFlowRateUnitStr;

                methodBaseValue.MInA = MInA;
                methodBaseValue.MInB = MInB;
                methodBaseValue.MInC = MInC;
                methodBaseValue.MInD = MInD;
                methodBaseValue.MBPV = MBPV;

                return methodBaseValue;
            }
        }

        //创建一个自定义委托，用于自定义的信号
        public delegate void MHandlerDdelegate(object sender);
        //声明一个修改柱体积事件
        public MHandlerDdelegate MColumnVolHandler;
        //声明一个修改基本单位事件
        public MHandlerDdelegate MBaseUnitHandler;
        //声明一个修改流速事件
        public MHandlerDdelegate MFlowRateHandler;
        //声明一个修改流速单位事件
        public MHandlerDdelegate MFlowRateUnitHandler;
        //声明一个切阀事件
        public MHandlerDdelegate MValveHandler;


        /// <summary>
        /// 构造函数
        /// </summary>
        public MethodSettingsVM()
        {
            MMaxFlowRate = StaticValue.s_maxFlowVol;

            MASParaList = new List<ASMethodParaVM>();
            MResultName = new ResultNameVM();
            MDefineQuestions = new DefineQuestionsVM();
        }
    }
}
