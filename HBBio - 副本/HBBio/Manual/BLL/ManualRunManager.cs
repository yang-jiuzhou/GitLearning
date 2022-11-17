using HBBio.Collection;
using HBBio.Communication;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Manual
{
    /**
     * ClassName: ManualRunManager
     * Description: 手动状态处理
     * Version: 1.0
     * Create:  2020/08/31
     * Author:  wangkai
     * Company: jshanbon
     **/
    public class ManualRunManager
    {
        private static ManualRunManager s_instance = null;

        private ComConfStatic m_comconfStatic = null;               //传入参数，用于给通讯仪器赋值

        public ManualState m_state = ManualState.Free;        	    //手动状态

        public ManualValue m_manualValue = null;

        public string MRunT
        {
            get
            {
                if (MT > MTheoryT)
                {
                    return MT.ToString() + "(" + MTheoryT.ToString() + ")";
                }
                else
                {
                    return MT.ToString();
                }
            }
        }

        private double m_T = 0;                                     //设置谱图累计运行时间(外部赋值)
        public double MT
        {
            get
            {
                return m_T;
            }
            set
            {
                m_T = value;

                if (!m_isHold)
                {
                    m_theoryTVCV[0] = Math.Round(m_T - m_holdTTotal, 2);
                }
            }
        }
        private double m_V = 0;                                     //设置谱图累计运行体积(外部赋值)
        public double MV
        {
            get
            {
                return m_V;
            }
            set
            {
                m_V = value;

                if (!m_isHold)
                {
                    m_theoryTVCV[1] = Math.Round(m_V - m_holdVTotal, 2);
                }
            }
        }
        private double m_CV = 0;                                    //设置谱图累计运行柱体积(外部赋值)
        public double MCV
        {
            get
            {
                return m_CV;
            }
            set
            {
                m_CV = value;

                if (!m_isHold)
                {
                    m_theoryTVCV[2] = Math.Round(m_CV - m_holdCVTotal, 2);
                }
            }
        }

        private double[] m_theoryTVCV = new double[3];              //当前执行阶段的理论运行累计（不包含挂起）
        public double MTheoryT
        {
            get
            {
                return m_theoryTVCV[0];
            }
        }
        public double MTheoryV
        {
            get
            {
                return m_theoryTVCV[1];
            }
        }
        public double MTheoryCV
        {
            get
            {
                return m_theoryTVCV[2];
            }
        }

        private double m_holdTStart = 0;                            //当前执行阶段的一次挂起时间点
        private double m_holdTTotal = 0;                            //当前执行阶段的一次挂起时间累计
        private double m_holdVStart = 0;                            //当前执行阶段的一次挂起时间点
        private double m_holdVTotal = 0;                            //当前执行阶段的一次挂起时间累计
        private double m_holdCVStart = 0;                           //当前执行阶段的一次挂起时间点
        private double m_holdCVTotal = 0;                           //当前执行阶段的一次挂起时间累计

        private bool m_isHold = false;                              //当前执行阶段是否挂起
        public bool MIsHold
        {
            get
            {
                return m_isHold;
            }
            set
            {
                if (m_isHold == value)
                {
                    return;
                }

                m_isHold = value;

                if (m_isHold)
                {
                    m_holdTStart = m_T;
                    m_holdVStart = m_V;
                    m_holdCVStart = m_CV;
                    WriteDataToDB();
                }
                else
                {
                    m_holdTTotal += (m_T - m_holdTStart);
                    m_holdVTotal += (m_V - m_holdVStart);
                    m_holdCVTotal += (m_CV - m_holdCVStart);
                    WriteDataToDB();
                }
            }
        }

        private bool m_isBreak = false;
        public bool MIsBreak
        {
            get
            {
                return m_isBreak;
            }
        }

        //创建一个自定义委托，用于自定义的信号
        public delegate void MHandlerDdelegateMarker(object type, object val);
        //声明一个新建标记事件
        public MHandlerDdelegateMarker MMarkerHandler;

        //创建一个自定义委托，用于审计跟踪
        public delegate void MAuditTrailsDdelegate(object desc, object oper);
        //声明一个审计跟踪事件
        public MAuditTrailsDdelegate MAuditTrailsHandler;


        /// <summary>
        /// 私有构造函数
        /// </summary>
        private ManualRunManager(ComConfStatic comconfStatic)
        {
            m_comconfStatic = comconfStatic;

            m_manualValue = new ManualValue();

            ReadDataFromDB();
        }

        /// <summary>
        /// 单例模式
        /// </summary>
        /// <returns></returns>
        public static ManualRunManager Instance(ComConfStatic comconfStatic)
        {
            if (null == s_instance)
            {
                s_instance = new ManualRunManager(comconfStatic);
            }

            return s_instance;
        }

        /*-------------运行状态机 开始-------------------*/

        /// <summary>
        /// 空闲->运行
        /// </summary>
        public void FreeToRun()
        {
            if (ManualState.FreeToRun == m_state)
            {
                m_state = ManualState.Run;
            }
        }

        /// <summary>
        /// 运行
        /// </summary>
        public void Run(double time, double vol, double cv, bool run)
        {
            lock (m_manualValue)
            {
                if (run)
                {
                    MT = time;
                    MV = vol;
                    MCV = cv;
                }

                bool ischange = false;

                //泵-系统
                if (m_manualValue.m_pumpSystemValue.m_update)
                {
                    m_manualValue.m_pumpSystemValue.m_update = false;
                    ischange = true;

                    m_manualValue.m_pumpSystemValue.Init(MTheoryT, MTheoryV, MTheoryCV);
                    if (m_manualValue.m_pumpSystemValue.MEnablePT)
                    {
                        m_manualValue.m_pumpSystemValue.SetIncremental(StaticSystemConfig.SSystemConfig.MConfOther.MPIDP, StaticSystemConfig.SSystemConfig.MConfOther.MPIDI, StaticSystemConfig.SSystemConfig.MConfOther.MPIDD);
                    }
                }
                if (run)
                {
                    if (m_manualValue.m_pumpSystemValue.MEnablePT)
                    {
                        switch (m_manualValue.m_pumpSystemValue.UpdateFlow(
                            m_comconfStatic.GetPTGet(ENUMPTName.PTA)
                            , m_comconfStatic.GetPTGet(ENUMPTName.PTB)
                            , m_comconfStatic.GetPTGet(ENUMPTName.PTC)
                            , m_comconfStatic.GetPTGet(ENUMPTName.PTD)
                            , m_comconfStatic.GetPTGet(ENUMPTName.PTTotal)
                            , m_comconfStatic.GetPumpSet(ENUMPumpName.FITA)
                            , m_comconfStatic.GetPumpSet(ENUMPumpName.FITB)
                            , m_comconfStatic.GetPumpSet(ENUMPumpName.FITC)
                            , m_comconfStatic.GetPumpSet(ENUMPumpName.FITD)))
                        {
                            case EnumStatus.Ing:
                                m_comconfStatic.SetPumpSystem(m_manualValue.m_pumpSystemValue.MFlowVol, m_manualValue.m_pumpSystemValue.MA, m_manualValue.m_pumpSystemValue.MB, m_manualValue.m_pumpSystemValue.MC, m_manualValue.m_pumpSystemValue.MD);
                                break;
                        }
                    }
                    else
                    {
                        switch (m_manualValue.m_pumpSystemValue.UpdateFlow(MTheoryT, MTheoryV, MTheoryCV))
                        {
                            case EnumStatus.Ing:
                                m_comconfStatic.SetPumpSystem(m_manualValue.m_pumpSystemValue.MFlowVol, m_manualValue.m_pumpSystemValue.MB, m_manualValue.m_pumpSystemValue.MC, m_manualValue.m_pumpSystemValue.MD);
                                break;
                            case EnumStatus.Over:
                                m_comconfStatic.SetPumpSystem(m_manualValue.m_pumpSystemValue.MFlowVol, m_manualValue.m_pumpSystemValue.MB, m_manualValue.m_pumpSystemValue.MC, m_manualValue.m_pumpSystemValue.MD);
                                ischange = true;
                                break;
                        }
                    } 
                }

                //泵-上样
                if (m_manualValue.m_pumpSampleValue.m_update)
                {
                    m_manualValue.m_pumpSampleValue.m_update = false;
                    ischange = true;

                    m_manualValue.m_pumpSampleValue.Init(MTheoryT, MTheoryV, MTheoryCV);
                    if (m_manualValue.m_pumpSampleValue.MEnablePT)
                    {
                        m_manualValue.m_pumpSampleValue.SetIncremental(StaticSystemConfig.SSystemConfig.MConfOther.MPIDP, StaticSystemConfig.SSystemConfig.MConfOther.MPIDI, StaticSystemConfig.SSystemConfig.MConfOther.MPIDD);
                    }
                }
                if (run)
                {
                    if (m_manualValue.m_pumpSampleValue.MEnablePT)
                    {
                        switch (m_manualValue.m_pumpSampleValue.UpdateFlow(m_comconfStatic.GetPTGet(ENUMPTName.PTS), m_comconfStatic.GetPumpSet(ENUMPumpName.FITS)))
                        {
                            case EnumStatus.Ing:
                                m_comconfStatic.SetPumpSample(m_manualValue.m_pumpSampleValue.MFlowRun);
                                break;
                        }
                    }
                    else
                    {
                        switch (m_manualValue.m_pumpSampleValue.UpdateFlow(MTheoryT, MTheoryV, MTheoryCV))
                        {
                            case EnumStatus.Ing:
                                m_comconfStatic.SetPumpSample(m_manualValue.m_pumpSampleValue.MFlowRun);
                                break;
                            case EnumStatus.Over:
                                m_comconfStatic.SetPumpSample(0);
                                ischange = true;
                                break;
                        }
                    }   
                }

                //阀-普通阀
                if (m_manualValue.m_valveValue.m_update)
                {
                    m_manualValue.m_valveValue.m_update = false;
                    ischange = true;

                    for (int i = 0; i < m_manualValue.m_valveValue.MListValave.Count; i++)
                    {
                        switch ((ENUMValveName)i)
                        {
                            case ENUMValveName.Out:
                                ValveSwitchOut(m_manualValue.m_valveValue.MListValave[i].MIndex);
                                break;
                            default:
                                ValveSwitchOther((ENUMValveName)i, m_manualValue.m_valveValue.MListValave[i].MIndex);
                                break;
                        }
                    }
                }

                //阀-组分收集阀
                {
                    int outValve = m_comconfStatic.GetValveSet(ENUMValveName.Out);
                    if (m_manualValue.m_collValveValue.m_update)
                    {
                        m_manualValue.m_collValveValue.m_update = false;
                        ischange = true;
                        m_manualValue.m_collValveValue.Init(ref outValve, m_theoryTVCV);
                    }
                    if (run && m_manualValue.m_collValveValue.JudgeCondition(ref outValve, m_theoryTVCV, EnumMonitorInfo.ValueList, EnumMonitorInfo.ValueMinList, EnumMonitorInfo.ValueMaxList, EnumMonitorInfo.SlopeList, EnumMonitorInfo.SlopeLastList))
                    {
                        ischange = true;
                    }
                    string desc = null;
                    string oper = null;
                    while (m_manualValue.m_collValveValue.GetLogDescOper(ref desc, ref oper))
                    {
                        MAuditTrailsHandler?.Invoke(desc, oper);
                    }
                    m_comconfStatic.SetValve(ENUMValveName.Out, outValve);
                }

                //阀-组分收集器
                {
                    CollectorItem item = m_comconfStatic.GetItem(ENUMCollectorName.Collector01);
                    if (null != item)
                    {
                        lock (item.m_locker)
                        {
                            CollTextIndex index = new CollTextIndex(item.m_txtSet, item.m_indexSet, item.m_ingSet);
                            if (m_manualValue.m_collCollectorValue.m_update)
                            {
                                m_manualValue.m_collCollectorValue.m_update = false;
                                ischange = true;
                                m_manualValue.m_collCollectorValue.Init(ref index, m_theoryTVCV);
                            }
                            if (run && m_manualValue.m_collCollectorValue.JudgeCondition(ref index, m_theoryTVCV, EnumMonitorInfo.ValueList, EnumMonitorInfo.ValueMinList, EnumMonitorInfo.ValueMaxList, EnumMonitorInfo.SlopeList, EnumMonitorInfo.SlopeLastList))
                            {
                                ischange = true;
                            }
                            string desc = null;
                            string oper = null;
                            while (m_manualValue.m_collCollectorValue.GetLogDescOper(ref desc, ref oper))
                            {
                                MAuditTrailsHandler?.Invoke(desc, oper);
                            }

                            item.MIndexSet = index.MStr;
                            item.MStatusSet = index.MStatus;
                        }
                    }
                }

                //监控-AS
                int indexBPV = m_comconfStatic.GetValveSet(ENUMValveName.BPV);
                foreach (var it in m_manualValue.m_ASValue.MList)
                {
                    if (it.m_update)
                    {
                        it.m_update = false;
                        ischange = true;

                        it.Init();
                    }

                    if (run && it.m_signal)
                    {
                        if (m_comconfStatic.GetASGet(it.MName))
                        {
                            switch (it.MAction)
                            {
                                case EnumMonitorActionManual.Bypass:
                                    switch (it.JudgeASYes(MT, MV, MCV, indexBPV))
                                    {
                                        case 1:
                                            {
                                                MAuditTrailsHandler?.Invoke(it.MHeader + Share.ReadXaml.S_InfoASYes, Share.ReadXaml.GetEnum(it.MAction, "EnumMonitorAction_"));
                                            }
                                            break;
                                        case 2:
                                            {
                                                MAuditTrailsHandler?.Invoke(it.MHeader + Share.ReadXaml.S_InfoASYes, Share.ReadXaml.GetEnum(it.MAction, "EnumMonitorAction_") + " " + EnumBPVInfo.NameList[indexBPV] + " -> " + EnumBPVInfo.NameList[0]);
                                                indexBPV = 0;
                                                m_comconfStatic.SetValve(ENUMValveName.BPV, 0);
                                            }
                                            break;
                                    }
                                    break;
                                case EnumMonitorActionManual.Pause:
                                    m_state = ManualState.RunToPause;
                                    MAuditTrailsHandler?.Invoke(it.MHeader + Share.ReadXaml.S_InfoASYes, Share.ReadXaml.GetEnum(it.MAction, "EnumMonitorAction_"));
                                    ischange = true;
                                    break;
                                case EnumMonitorActionManual.Stop:
                                    m_state = ManualState.Stop;
                                    MAuditTrailsHandler?.Invoke(it.MHeader + Share.ReadXaml.S_InfoASYes, Share.ReadXaml.GetEnum(it.MAction, "EnumMonitorAction_"));
                                    it.m_signal = false;
                                    break;
                            }
                        }
                        else
                        {
                            switch (it.MAction)
                            {
                                case EnumMonitorActionManual.Bypass:
                                    switch (it.JudgeASNo(MT, MV, MCV, ref indexBPV))
                                    {
                                        case 1:
                                            MAuditTrailsHandler?.Invoke(it.MHeader + Share.ReadXaml.S_InfoASNo, Share.ReadXaml.GetEnum(it.MAction, "EnumMonitorAction_"));
                                            break;
                                        case 2:
                                            m_comconfStatic.SetValve(ENUMValveName.BPV, indexBPV);
                                            MAuditTrailsHandler?.Invoke(it.MHeader + Share.ReadXaml.S_InfoASNo, Share.ReadXaml.GetEnum(it.MAction, "EnumMonitorAction_") + " " + EnumBPVInfo.NameList[0] + " -> " + EnumBPVInfo.NameList[indexBPV]);
                                            break;
                                    }
                                    break;
                            }
                        }
                    }
                }

                //监控-pHCdUV
                if (m_manualValue.m_MonitorValue.m_update)
                {
                    m_manualValue.m_MonitorValue.m_update = false;
                    ischange = true;

                    m_manualValue.m_MonitorValue.Init(EnumMonitorInfo.NameList);
                }
                switch (m_manualValue.m_MonitorValue.Finish(EnumMonitorInfo.ValueList, MTheoryT, MTheoryV, MTheoryCV))
                {
                    case EnumStatus.Over:
                        ischange = true;
                        switch (m_manualValue.m_MonitorValue.MAction)
                        {
                            case EnumMonitorActionManual.Bypass:
                                m_comconfStatic.SetValve(ENUMValveName.BPV, 0);
                                MAuditTrailsHandler?.Invoke(Share.ReadXaml.GetEnum(m_manualValue.m_MonitorValue.MAction, "EnumMonitorAction_"), m_manualValue.m_MonitorValue.MValue.MName);
                                break;
                            case EnumMonitorActionManual.Pause:
                                m_state = ManualState.RunToPause;
                                MAuditTrailsHandler?.Invoke(Share.ReadXaml.GetEnum(m_manualValue.m_MonitorValue.MAction, "EnumMonitorAction_"), m_manualValue.m_MonitorValue.MValue.MName);
                                break;
                            case EnumMonitorActionManual.Stop:
                                m_state = ManualState.Stop;
                                MAuditTrailsHandler?.Invoke(Share.ReadXaml.GetEnum(m_manualValue.m_MonitorValue.MAction, "EnumMonitorAction_"), m_manualValue.m_MonitorValue.MValue.MName);
                                break;
                        }                    
                        break;              
                }

                //警报警告
                if (m_manualValue.m_alarmWarningValue.m_update)
                {
                    m_manualValue.m_alarmWarningValue.m_update = false;
                    ischange = true;

                    StaticAlarmWarning.SAlarmWarning = m_manualValue.m_alarmWarningValue.m_alarmWarning;
                }

                //其它-标记
                if (m_manualValue.m_markerValue.m_update)
                {
                    m_manualValue.m_markerValue.m_update = false;
                    MMarkerHandler?.Invoke(m_manualValue.m_markerValue.MType, m_manualValue.m_markerValue.MIsReal ? -1 : m_manualValue.m_markerValue.MVal);
                }

                //其它-暂停
                if (m_manualValue.m_pauseValue.m_update)
                {
                    m_manualValue.m_pauseValue.m_update = false;
                    ischange = true;

                    m_manualValue.m_pauseValue.Init(MTheoryT, MTheoryV, MTheoryCV);
                }
                switch (m_manualValue.m_pauseValue.Finish(MTheoryT, MTheoryV, MTheoryCV))
                {
                    case EnumStatus.Over:
                        ischange = true;
                        m_state = ManualState.RunToPause;
                        MAuditTrailsHandler?.Invoke(Share.ReadXaml.GetResources("M_Pause"),"N/A");
                        break;
                }

                //其它-停止
                if (m_manualValue.m_stopValue.m_update)
                {
                    m_manualValue.m_stopValue.m_update = false;
                    ischange = true;

                    m_manualValue.m_stopValue.Init(MTheoryT, MTheoryV, MTheoryCV);
                }
                switch (m_manualValue.m_stopValue.Finish(MTheoryT, MTheoryV, MTheoryCV))
                {
                    case EnumStatus.Over:
                        ischange = true;
                        m_state = ManualState.Stop;
                        MAuditTrailsHandler?.Invoke(Share.ReadXaml.GetResources("M_Stop"), "N/A");
                        break;
                }

                //其它-紫外检测器
                if (m_manualValue.m_uvValue.m_update)
                {
                    m_manualValue.m_uvValue.m_update = false;
                    ischange = true;

                    if (m_manualValue.m_uvValue.MOnoff)
                    {
                        m_comconfStatic.SetUVLamp(ENUMUVName.UV01, true);
                    }
                    else
                    {
                        m_comconfStatic.SetUVLamp(ENUMUVName.UV01, false);
                    }

                    if (m_manualValue.m_uvValue.MClear)
                    {
                        m_comconfStatic.SetUVClear(ENUMUVName.UV01);
                    }

                    m_comconfStatic.SetUVWave(ENUMUVName.UV01, m_manualValue.m_uvValue);
                }

                //其它-示差检测器
                if (m_manualValue.m_riValue.m_update)
                {
                    m_manualValue.m_riValue.m_update = false;
                    ischange = true;

                    if (m_manualValue.m_riValue.MOnoff)
                    {
                        m_comconfStatic.SetRIPurge(ENUMRIName.RI01, true);
                    }
                    else
                    {
                        m_comconfStatic.SetRIPurge(ENUMRIName.RI01, false);
                    }

                    if (m_manualValue.m_riValue.MClear)
                    {
                        m_comconfStatic.SetRIClear(ENUMRIName.RI01);
                    }

                    m_comconfStatic.SetRITemperature(ENUMRIName.RI01, m_manualValue.m_riValue.MTemperature);
                }

                //其它-动态混合器
                if (m_manualValue.m_mixerValue.m_update)
                {
                    m_manualValue.m_mixerValue.m_update = false;
                    ischange = true;

                    m_comconfStatic.SetMixer(ENUMMixerName.Mixer01, m_manualValue.m_mixerValue.MOnoff);
                }

                if (ischange)
                {
                    WriteDataToDB();
                }
            }
        }

        /// <summary>
        /// 运行->暂停
        /// </summary>
        public void RunToPause()
        {
            m_state = ManualState.Pause;

            m_comconfStatic.SetPumpPause(true);
        }

        /// <summary>
        /// 暂停->运行
        /// </summary>
        public void PauseToRun()
        {
            m_state = ManualState.Run;

            m_comconfStatic.SetPumpPause(false);
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            m_state = ManualState.Free;

            m_isHold = false;

            m_manualValue.Clear();

            ClearDataToDB();

            m_comconfStatic.SetPumpPause(false);
            m_comconfStatic.SetPumpSystem(0);
            m_comconfStatic.SetPumpSample(0);

            m_comconfStatic.SetValve(ENUMValveName.Out, 0);
            
            CollectorItem item = m_comconfStatic.GetItem(ENUMCollectorName.Collector01);
            if (null != item)
            {
                item.MStatusSet = false;
            }
        }

        /// <summary>
        /// 中断->暂停
        /// </summary>
        public void BreakToPause(double time, double vol, double cv)
        {
            m_state = ManualState.Pause;

            m_comconfStatic.SetPumpPause(true);
            Run(time, vol, cv, true);
        }

        /*-------------运行状态机 结束-------------------*/

        /// <summary>
        /// 写文件
        /// </summary>
        public void WriteDataToDB()
        {
            ManualManager manager = new ManualManager();
            manager.AddManualTemp(m_manualValue);
        }
        public void WriteDataToDBColl()
        {
            ManualManager manager = new ManualManager();
            manager.AddManualColl("");
        }
        private void ClearDataToDB()
        {
            ManualManager manager = new ManualManager();
            manager.DelManualTemp();
        }

        /// <summary>
        /// 读文件
        /// </summary>
        /// <param name="manualInfo"></param>
        private bool ReadDataFromDB()
        {
            ManualManager manager = new ManualManager();
            ManualValue item = null;
            if (null == manager.GetManualTemp(out item))
            {
                if (null != item)
                {
                    m_manualValue = item;
                    m_manualValue.m_valveValue.m_update = true;
                    m_manualValue.m_pumpSystemValue.m_signal = true;
                    if (m_manualValue.m_pumpSystemValue.MEnablePT)
                    {
                        m_manualValue.m_pumpSystemValue.SetIncremental(StaticSystemConfig.SSystemConfig.MConfOther.MPIDP, StaticSystemConfig.SSystemConfig.MConfOther.MPIDI, StaticSystemConfig.SSystemConfig.MConfOther.MPIDD);
                    }
                    m_manualValue.m_pumpSampleValue.m_signal = true;
                    StaticAlarmWarning.SAlarmWarning = m_manualValue.m_alarmWarningValue.m_alarmWarning;
                    m_isBreak = true;
                }
            }
            return true;
        }

        /// <summary>
        /// 切换阀(其它)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="index"></param>
        public void ValveSwitchOther(ENUMValveName name, int index)
        {
            int indexOld = m_comconfStatic.GetValveSet(name);
            if (indexOld != index && indexOld < EnumValveInfo.Count(name) && index < EnumValveInfo.Count(name))
            {
                m_comconfStatic.SetValve(name, index);
                MAuditTrailsHandler?.Invoke(ReadXamlManual.C_ValveSwitch, EnumValveInfo.NameList(name)[indexOld] + "->" + EnumValveInfo.NameList(name)[index]);
            }
        }
        /// <summary>
        /// 切换阀(出口阀)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="index"></param>
        public void ValveSwitchOut(int index)
        {
            int indexOld = m_comconfStatic.GetValveSet(ENUMValveName.Out);
            if (indexOld != index && index < EnumOutInfo.Count)
            {
                m_comconfStatic.SetValve(ENUMValveName.Out, index);
                MAuditTrailsHandler?.Invoke(ReadXamlCollection.C_CollMarkM, EnumOutInfo.NameList[index]);
            }
        }
    }


    /// <summary>
    /// 手动运行的状态
    /// </summary>
    public enum ManualState
    {
        Free,           //空闲
        Run,            //运行
        Pause,          //暂停
        Stop,           //停止 
        FreeToRun,      //空闲->运行
        RunToPause,     //运行->暂停
        PauseToRun,     //暂停->运行
        BreakToPause    //中断->暂停
    }
}
