using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.Communication
{
    public class ComConfStatic
    {
        private static readonly ComConfStatic s_instance = new ComConfStatic();

        private readonly Dictionary<ENUMSamplerName, SamplerItem> m_dictSampler = new Dictionary<ENUMSamplerName, SamplerItem>();
        private readonly Dictionary<ENUMASName, ASItem> m_dictAS = new Dictionary<ENUMASName, ASItem>();
        private readonly Dictionary<ENUMValveName, ValveItem> m_dictValve = new Dictionary<ENUMValveName, ValveItem>();     
        private readonly Dictionary<ENUMPumpName, PumpItem> m_dictPump = new Dictionary<ENUMPumpName, PumpItem>();
        private readonly Dictionary<ENUMPTName, PTItem> m_dictPT = new Dictionary<ENUMPTName, PTItem>();
        private readonly Dictionary<ENUMMixerName, MixerItem> m_dictMixer = new Dictionary<ENUMMixerName, MixerItem>();
        private readonly Dictionary<ENUMPHName, PHItem> m_dictPH = new Dictionary<ENUMPHName, PHItem>();
        private readonly Dictionary<ENUMCDName, CDItem> m_dictCD = new Dictionary<ENUMCDName, CDItem>();
        private readonly Dictionary<ENUMTTName, TTItem> m_dictTT = new Dictionary<ENUMTTName, TTItem>();
        private readonly Dictionary<ENUMUVName, UVItem> m_dictUV = new Dictionary<ENUMUVName, UVItem>();
        private readonly Dictionary<ENUMRIName, RIItem> m_dictRI = new Dictionary<ENUMRIName, RIItem>();
        private readonly Dictionary<ENUMCollectorName, CollectorItem> m_dictCollector = new Dictionary<ENUMCollectorName, CollectorItem>();
        
        public CommunicationSets m_cs = null;                                       //通讯配置信息
        public List<ComConf> m_cfList = null;                                       //具体仪器列表
        public List<BaseCommunication> m_comList = new List<BaseCommunication>();   //通信集合  
        public List<Signal> m_runDataList = new List<Signal>();                     //运行数据集合
        public List<StringWrapper> m_runDataShowList = new List<StringWrapper>();   //运行数据显示集合
        public List<StringWrapper> m_runDataSetList = new List<StringWrapper>();    //运行数据设置集合
        public List<Signal> m_signalList = new List<Signal>();                      //曲线信号集合
        private AlarmWarning m_alarmWarning = new AlarmWarning();                   //警报警告
        public List<int> m_listSmooth = new List<int>();

        public List<BaseInstrument> m_biList = new List<BaseInstrument>();
        public List<InstrumentPoint> m_ipList = new List<InstrumentPoint>();
        public InstrumentSize m_size = new InstrumentSize();
        public List<Point> m_lsitCircle = new List<Point>();

        public double m_totalFlow = 0;
        public double m_pumpSFlow = 0;
        private DateTime m_datetimeAllData = DateTime.Today;
        private Thread m_thread = null;


        /// <summary>
        /// 私有构造函数
        /// </summary>
        private ComConfStatic()
        {
            for (int i = 0; i < Enum.GetNames(typeof(ENUMSamplerName)).GetLength(0); i++)
            {
                m_dictSampler.Add((ENUMSamplerName)i, new SamplerItem());
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMASName)).GetLength(0); i++)
            {
                m_dictAS.Add((ENUMASName)i, new ASItem());
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMValveName)).GetLength(0); i++)
            {
                m_dictValve.Add((ENUMValveName)i, new ValveItem());
            }   
            for (int i = 0; i < Enum.GetNames(typeof(ENUMPumpName)).GetLength(0); i++)
            {
                m_dictPump.Add((ENUMPumpName)i, new PumpItem());
            } 
            for (int i = 0; i < Enum.GetNames(typeof(ENUMPTName)).GetLength(0); i++)
            {
                m_dictPT.Add((ENUMPTName)i, new PTItem());
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMMixerName)).GetLength(0); i++)
            {
                m_dictMixer.Add((ENUMMixerName)i, new MixerItem());
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMPHName)).GetLength(0); i++)
            {
                m_dictPH.Add((ENUMPHName)i, new PHItem());
            }  
            for (int i = 0; i < Enum.GetNames(typeof(ENUMCDName)).GetLength(0); i++)
            {
                m_dictCD.Add((ENUMCDName)i, new CDItem());
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMTTName)).GetLength(0); i++)
            {
                m_dictTT.Add((ENUMTTName)i, new TTItem());
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMUVName)).GetLength(0); i++)
            {
                m_dictUV.Add((ENUMUVName)i, new UVItem(2));
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMRIName)).GetLength(0); i++)
            {
                m_dictRI.Add((ENUMRIName)i, new RIItem());
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMCollectorName)).GetLength(0); i++)
            {
                m_dictCollector.Add((ENUMCollectorName)i, new CollectorItem());
            }
            
            Init();
        }

        /// <summary>
        /// 单例模式
        /// </summary>
        /// <returns></returns>
        public static ComConfStatic Instance()
        {
            return s_instance;
        }

        /// <summary>
        /// 寻找当前通讯设置
        /// </summary>
        public void Init()
        {
            CommunicationSetsManager csManager = new CommunicationSetsManager();

            List<CommunicationSets> csList = null; 
            if (null == csManager.GetList(out csList))
            {
                for (int i = 0; i < csList.Count; i++)
                {
                    if (csList[i].MIsEnabled)
                    {
                        InitCIS(csList[i]);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 初始化当前通讯设置
        /// </summary>
        /// <param name="cs"></param>
        private void InitCIS(CommunicationSets cs)
        {
            //if (null != m_cs && m_cs.MId == cs.MId)
            //{
            //    return;
            //}

            m_cs = null;
            m_cs = cs;
            m_comList.Clear();
            m_runDataList.Clear();
            m_runDataShowList.Clear();
            m_runDataSetList.Clear();
            m_signalList.Clear();
            m_biList.Clear();
            m_listSmooth.Clear();

            ItemVisibility.ResetAll(Visibility.Collapsed);

            CommunicationSetsManager csManager = new CommunicationSetsManager();
            if (null == csManager.GetItem(cs, out m_cfList, out m_ipList, out m_size, out m_lsitCircle))
            {
                foreach (var itCC in m_cfList)
                {
                    m_comList.Add(CreateItem(itCC));

                    foreach (var itBI in itCC.MList)
                    {
                        m_biList.Add(itBI);
                        m_runDataList.AddRange(itBI.m_list);
                        foreach (var itSN in itBI.m_list)
                        {
                            if (itSN.MIsLine)
                            {
                                m_signalList.Add(itSN);
                                m_listSmooth.Add(itSN.MSmooth);
                            }
                        }
                    }
                }

                for (int i = 0; i < m_runDataList.Count; i++)
                {
                    StringWrapper itemG = new StringWrapper();
                    StringWrapper itemS = new StringWrapper();
                    if (string.IsNullOrEmpty(m_runDataList[i].MUnit))
                    {
                        itemG.MName = m_runDataList[i].MDlyName;
                        itemS.MName = m_runDataList[i].MDlyName;
                    }
                    else
                    {
                        itemG.MName = m_runDataList[i].MDlyName + "(" + m_runDataList[i].MUnit + ")";
                        itemS.MName = m_runDataList[i].MDlyName + "(" + m_runDataList[i].MUnit + ")";
                    }
                    m_runDataShowList.Add(itemG);
                    m_runDataSetList.Add(itemS);
                }

                //初始化警报警告
                StaticAlarmWarning.Init(GetAlarmWarning());
                StaticSystemConfig.Init(GetSystemConfig());
                double maxFlowS = 0;
                double maxFlowA = 0;
                double maxFlowB = 0;
                double maxFlowC = 0;
                double maxFlowD = 0;
                foreach (var it in m_dictPump)
                {
                    switch (it.Key)
                    {
                        case ENUMPumpName.FITS:
                            maxFlowS = it.Value.m_maxFlowVol;
                            break;
                        case ENUMPumpName.FITA:
                            maxFlowA = it.Value.m_maxFlowVol;
                            break;
                        case ENUMPumpName.FITB:
                            maxFlowB = it.Value.m_maxFlowVol;
                            break;
                        case ENUMPumpName.FITC:
                            maxFlowC = it.Value.m_maxFlowVol;
                            break;
                        case ENUMPumpName.FITD:
                            maxFlowD = it.Value.m_maxFlowVol;
                            break;

                    }
                }
                StaticValue.Init(maxFlowA + maxFlowB + maxFlowC + maxFlowD, maxFlowS, maxFlowA, maxFlowB, maxFlowC, maxFlowD);
            }
        }

        /// <summary>
        /// 启动子线程
        /// </summary>
        public void ThreadAllStart()
        {
            foreach (BaseCommunication item in m_comList)
            {
                item.ThreadStart();
            }
        }

        /// <summary>
        /// 设置子线程状态
        /// </summary>
        /// <param name="status"></param>
        public void ThreadAllStatus(ENUMThreadStatus status)
        {
            switch (status)
            {
                case ENUMThreadStatus.Free:
                    foreach (BaseCommunication item in m_comList)
                    {
                        item.ThreadStatus(ENUMThreadStatus.Free);
                    }
                    foreach (BaseCommunication item in m_comList)
                    {
                        while (ENUMCommunicationState.Free != item.m_communState)
                        {
                            Thread.Sleep(DlyBase.c_sleep1);
                            DispatcherHelper.DoEvents();
                        }
                    }
                    break;
                case ENUMThreadStatus.Version:
                    foreach (BaseCommunication item in m_comList)
                    {
                        item.ThreadStatus(ENUMThreadStatus.Version);
                    }
                    break;
                case ENUMThreadStatus.WriteOrRead:
                    foreach (BaseCommunication item in m_comList)
                    {
                        item.ThreadStatus(ENUMThreadStatus.WriteOrRead);
                    }
                    break;
                case ENUMThreadStatus.Abort:
                    foreach (BaseCommunication item in m_comList)
                    {
                        item.ThreadStatus(ENUMThreadStatus.Abort);
                    }
                    foreach (BaseCommunication item in m_comList)
                    {
                        while (ENUMCommunicationState.Over != item.m_communState && ENUMCommunicationState.Free != item.m_communState)
                        {
                            Thread.Sleep(DlyBase.c_sleep1);
                            DispatcherHelper.DoEvents();
                        }
                    }
                    while (null != m_thread && m_thread.IsAlive)
                    {
                        Thread.Sleep(DlyBase.c_sleep1);
                        DispatcherHelper.DoEvents();
                    }
                    break;
            }
        }

        /// <summary>
        /// 更新实时数值
        /// </summary>
        public void UpdateAllData()
        {
            try
            {
                if ((DateTime.Now - m_datetimeAllData).TotalSeconds > 0.5)
                {
                    m_datetimeAllData = DateTime.Now;

                    m_totalFlow = 0;
                    m_pumpSFlow = 0;
                    double totalFlowSet = 0;
                    double deltaPT = 0;         //柱压差
                    foreach (var it in m_dictPump)
                    {
                        m_totalFlow += it.Value.m_flowGet;
                        totalFlowSet += it.Value.m_flowSet;
                    }

                    if (m_dictPT[ENUMPTName.PTColumnBack].MVisible)
                    {
                        deltaPT = m_dictPT[ENUMPTName.PTColumnBack].m_pressGet - m_dictPT[ENUMPTName.PTColumnFront].m_pressGet;
                    }

                    int index = 0;
                    int indexSignal = 0;
                    int indexMonitor = 0;
                    foreach (var itBC in m_comList)
                    {
                        foreach (var itObj in itBC.GetRunDataValueList())
                        {
                            if (itObj is double)
                            {
                                if (m_runDataList[index].MConstName.Contains("FIT"))//涉及到百分比登转换
                                {
                                    if (m_runDataList[index].MConstName.Equals("FIT_Total"))
                                    {
                                        m_runDataShowList[index].MValue = m_totalFlow.ToString();
                                    }
                                    else if (m_runDataList[index].MConstName.Equals("FITLinear_Total"))
                                    {
                                        m_runDataShowList[index].MValue = (m_totalFlow * StaticValue.SVolToLen).ToString("f2");
                                    }
                                    else if (m_runDataList[index].MConstName.Contains("Per"))
                                    {
                                        if (0 < m_totalFlow)
                                        {
                                            m_runDataShowList[index].MValue = (m_dictPump[(ENUMPumpName)Enum.Parse(typeof(ENUMPumpName), m_runDataList[index].MConstName.Replace("_Per", ""))].m_flowGet / m_totalFlow * 100).ToString("f2");
                                        }
                                        else
                                        {
                                            m_runDataShowList[index].MValue = itObj.ToString();
                                        }
                                    }
                                    else if (m_runDataList[index].MConstName.Contains("Linear"))
                                    {
                                        m_runDataShowList[index].MValue = (Convert.ToDouble(itObj) * StaticValue.SVolToLen).ToString("f2");
                                    }
                                    else
                                    {
                                        m_runDataShowList[index].MValue = itObj.ToString();
                                    }
                                }
                                else if (m_runDataList[index].MConstName.Contains("PT_Delta"))
                                {
                                    m_runDataShowList[index].MValue = deltaPT.ToString();
                                }
                                else
                                {
                                    m_runDataShowList[index].MValue = itObj.ToString();
                                }

                                m_signalList[indexSignal++].MRealValue = Convert.ToDouble(m_runDataShowList[index].MValue);

                                if (m_runDataList[index].MConstName.Contains("pH")
                                    || m_runDataList[index].MConstName.Contains("Cd")
                                    || (m_runDataList[index].MConstName.Contains("UV") && m_runDataList[index].MConstName.Contains("_") && !m_runDataList[index].MConstName.Contains("Wave")))
                                {
                                    EnumMonitorInfo.SetValue(indexMonitor++, m_signalList[indexSignal - 1].MRealValue);
                                }
                            }
                            else
                            {
                                m_runDataShowList[index].MValue = (string)itObj;
                            }

                            index++;
                        }
                    }


                    index = 0;
                    indexSignal = 0;
                    foreach (var itBC in m_comList)
                    {
                        foreach (var itObj in itBC.SetRunDataValueList())
                        {
                            if (itObj is double)
                            {
                                if (m_runDataList[index].MConstName.Contains("FIT"))//涉及到百分比登转换
                                {
                                    if (m_runDataList[index].MConstName.Equals("FIT_Total"))
                                    {
                                        m_runDataSetList[index].MValue = totalFlowSet.ToString();
                                    }
                                    else if (m_runDataList[index].MConstName.Equals("FITLinear_Total"))
                                    {
                                        m_runDataSetList[index].MValue = totalFlowSet.ToString();
                                    }
                                    else if (m_runDataList[index].MConstName.Contains("Per"))
                                    {
                                        if (0 < totalFlowSet)
                                        {
                                            m_runDataSetList[index].MValue = (m_dictPump[(ENUMPumpName)Enum.Parse(typeof(ENUMPumpName), m_runDataList[index].MConstName.Replace("_Per", ""))].m_flowSet / totalFlowSet * 100).ToString("f2");
                                        }
                                        else
                                        {
                                            m_runDataSetList[index].MValue = itObj.ToString();
                                        }
                                    }
                                    else
                                    {
                                        m_runDataSetList[index].MValue = itObj.ToString();
                                    }
                                }
                                else
                                {
                                    m_runDataSetList[index].MValue = itObj.ToString();
                                }
                            }
                            else if (itObj is int)
                            {
                                m_runDataSetList[index].MValue = itObj.ToString();
                            }
                            else
                            {
                                m_runDataSetList[index].MValue = (string)itObj;
                            }

                            index++;
                        }
                    }
                }
            }
            catch
            { }
        }

        public void UpdateRunDataList()
        {
            CommunicationSetsManager csManager = new CommunicationSetsManager();
            csManager.EditItem(m_cs, m_runDataList);
        }

        public bool GetCommunInfo(ObservableCollection<StringString> listAlarm)
        {
            bool result = false;
            foreach (BaseCommunication item in m_comList)
            {
                if (!item.MComConf.MAlarm)
                {
                    continue;
                }

                if (ENUMCommunicationState.Error == item.m_communState)
                {
                    bool isnew = true;
                    foreach (var it in listAlarm)
                    {
                        if (it.MName.Equals(item.MComConf.MName + item.MComConf.MList[0].MDlyName))
                        {
                            isnew = false;
                            break;
                        }
                    }
                    if (isnew)
                    {
                        result = true;
                        listAlarm.Add(new StringString() { MName = item.MComConf.MName + item.MComConf.MList[0].MDlyName, MValue = ReadXamlCommunication.S_ComError });
                    }
                }
            }

            return result;
        }

        public bool GetAlarmInfo(ObservableCollection<StringString> listAlarm)
        {
            bool result = false;
            int index = 0;
            for (int i = 0; i < m_signalList.Count; i++)
            {
                if (m_signalList[i].MIsAlarmWarning)
                {
                    if (0 != StaticAlarmWarning.SAlarmWarning.MList[index].MCheckHH && m_signalList[i].MRealValue > StaticAlarmWarning.SAlarmWarning.MList[index].MValHH)
                    {
                        bool isnew = true;
                        string name = StaticAlarmWarning.SAlarmWarning.MList[index].MName + " " + Share.ReadXaml.GetResources("labHH1");
                        string value = m_signalList[i].MRealValue + StaticAlarmWarning.SAlarmWarning.MList[index].MUnit
                            + "(" + StaticAlarmWarning.SAlarmWarning.MList[index].MValHH 
                            + StaticAlarmWarning.SAlarmWarning.MList[index].MUnit + ")";
                        foreach (var it in listAlarm)
                        {
                            if (it.MName.Equals(name))
                            {
                                isnew = false;
                                it.MValue = value;
                                break;
                            }
                        }
                        if (isnew)
                        {
                            result = true;
                            listAlarm.Add(new StringString() { MName = name, MValue = value });
                        }
                    }
                    else if (0 != StaticAlarmWarning.SAlarmWarning.MList[index].MCheckLL && m_signalList[i].MRealValue < StaticAlarmWarning.SAlarmWarning.MList[index].MValLL)
                    {
                        bool isnew = true;
                        string name = StaticAlarmWarning.SAlarmWarning.MList[index].MName + " " + Share.ReadXaml.GetResources("labLL1");
                        string value = m_signalList[i].MRealValue + StaticAlarmWarning.SAlarmWarning.MList[index].MUnit
                            + "(" + StaticAlarmWarning.SAlarmWarning.MList[index].MValLL
                            + StaticAlarmWarning.SAlarmWarning.MList[index].MUnit + ")";
                        foreach (var it in listAlarm)
                        {
                            if (it.MName.Equals(name))
                            {
                                isnew = false;
                                it.MValue = value;
                                break;
                            }
                        }
                        if (isnew)
                        {
                            result = true;
                            listAlarm.Add(new StringString() { MName = name, MValue = value });
                        }
                    }
                    index++;
                }
            }

            return result;
        }

        public bool GetWarningInfo(ObservableCollection<StringString> listWarning)
        {
            bool result = false;
            int index = 0;
            int count = listWarning.Count;
            for (int i = 0; i < m_signalList.Count; i++)
            {
                if (m_signalList[i].MIsAlarmWarning)
                {
                    if (0 != StaticAlarmWarning.SAlarmWarning.MList[index].MCheckH && m_signalList[i].MRealValue > StaticAlarmWarning.SAlarmWarning.MList[index].MValH)
                    {
                        bool isnew = true;
                        string name = StaticAlarmWarning.SAlarmWarning.MList[index].MName + " " + Share.ReadXaml.GetResources("labH1");
                        string value = m_signalList[i].MRealValue + StaticAlarmWarning.SAlarmWarning.MList[index].MUnit
                            + "(" + StaticAlarmWarning.SAlarmWarning.MList[index].MValH
                            + StaticAlarmWarning.SAlarmWarning.MList[index].MUnit + ")";
                        foreach (var it in listWarning)
                        {
                            if (it.MName.Equals(name))
                            {
                                isnew = false;
                                break;
                            }
                        }
                        if (isnew)
                        {
                            result = true;
                        }
                        listWarning.Add(new StringString() { MName = name, MValue = value });
                    }
                    else if (0 != StaticAlarmWarning.SAlarmWarning.MList[index].MCheckL && m_signalList[i].MRealValue < StaticAlarmWarning.SAlarmWarning.MList[index].MValL)
                    {
                        bool isnew = true;
                        string name = StaticAlarmWarning.SAlarmWarning.MList[index].MName + " " + Share.ReadXaml.GetResources("labL1");
                        string value = m_signalList[i].MRealValue + StaticAlarmWarning.SAlarmWarning.MList[index].MUnit
                            + "(" + StaticAlarmWarning.SAlarmWarning.MList[index].MValL
                            + StaticAlarmWarning.SAlarmWarning.MList[index].MUnit + ")";
                        foreach (var it in listWarning)
                        {
                            if (it.MName.Equals(name))
                            {
                                isnew = false;
                                break;
                            }
                        }
                        if (isnew)
                        {
                            result = true;
                        }
                        listWarning.Add(new StringString() { MName = name, MValue = value });
                    }
                    index++;
                }
            }

            while (count-- > 0)
            {
                listWarning.RemoveAt(0);
            }

            return result;
        }

        public void UpdateSignalList()
        {
            CommunicationSetsManager csManager = new CommunicationSetsManager();
            csManager.EditItem(m_cs, m_signalList);
        }

        public SystemConfig GetSystemConfig()
        {
            SystemConfig conf = new SystemConfig();

            CommunicationSetsManager csManager = new CommunicationSetsManager();
            string scInfo = null;
            if (null == csManager.GetSystemConfig(m_cs, out scInfo))
            {
                conf.SetDBInfo(scInfo);
            }

            return conf;
        }

        public void SetSystemConfig(SystemConfig item)
        {
            CommunicationSetsManager manager = new CommunicationSetsManager();
            manager.EditSystemConfig(m_cs, item.GetDBInfo());
        }

        public AlarmWarning GetAlarmWarning()
        {
            AlarmWarning alarmWarning = new AlarmWarning();
            foreach (var it in m_signalList)
            {
                if (it.MIsAlarmWarning)
                {
                    AlarmWarningItem item = new AlarmWarningItem();
                    item.MTypeName = it.MConstName;
                    item.MName = it.MDlyName;
                    item.MUnit = it.MUnit;
                    item.MValLL = it.MValLL;
                    item.MValL = it.MValL;
                    item.MValH = it.MValH;
                    item.MValHH = it.MValHH;
                    item.MCheckLL = EnumAlarmWarningMode.Enabled;//这四句必须显式赋值
                    item.MCheckL = EnumAlarmWarningMode.Enabled;
                    item.MCheckH = EnumAlarmWarningMode.Enabled;
                    item.MCheckHH = EnumAlarmWarningMode.Enabled;
                    item.MValMin = it.MValMin;
                    item.MValMax = it.MValMax;
                    alarmWarning.MList.Add(item);
                }
            }

            return alarmWarning;
        }

        public void SetAlarmWarning(AlarmWarning alarmWarning)
        {
            int index = 0;
            for (int i = 0; i < m_signalList.Count; i++)
            {
                if (m_signalList[i].MIsAlarmWarning)
                {
                    m_signalList[i].MValLL = alarmWarning.MList[index].MValLL;
                    m_signalList[i].MValL = alarmWarning.MList[index].MValL;
                    m_signalList[i].MValH = alarmWarning.MList[index].MValH;
                    m_signalList[i].MValHH = alarmWarning.MList[index].MValHH;
                    index++;
                }
            }

            UpdateSignalList();
        }

        public void SetSmooth(List<int> list)
        {
            int index = 0;
            for (int i = 0; i < m_signalList.Count; i++)
            {
                if (m_signalList[i].MIsLine)
                {
                    m_signalList[i].MSmooth = list[index];
                    index++;
                }
            }

            UpdateSignalList();
        }

        /// <summary>
        /// 创建仪器对象
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        private BaseCommunication CreateItem(ComConf cc)
        {
            CommunicationSetsManager manager = new CommunicationSetsManager();
            BaseCommunication item = manager.CreateItem(cc);

            switch (cc.MType)
            {
                case ENUMInstrumentType.Sampler:
                    {
                        ENUMSamplerName name = (ENUMSamplerName)Enum.Parse(typeof(ENUMSamplerName), cc.MList[0].MConstName);
                        ItemVisibility.s_listSampler[name] = Visibility.Visible;
                        m_dictSampler[name] = (SamplerItem)cc.MList[0];
                    }
                    break;
                case ENUMInstrumentType.Valve:
                    {
                        ENUMValveName name = (ENUMValveName)Enum.Parse(typeof(ENUMValveName), cc.MList[0].MConstName);
                        ItemVisibility.s_listValve[name] = Visibility.Visible;
                        m_dictValve[name] = (ValveItem)cc.MList[0];
                    }
                    break;
                case ENUMInstrumentType.Pump:
                    {
                        if (((ENUMPumpID)Enum.Parse(typeof(ENUMPumpID), cc.MModel)).ToString().Contains("OEM"))
                        {
                            ENUMPumpName name = (ENUMPumpName)Enum.Parse(typeof(ENUMPumpName), cc.MList[0].MConstName);
                            ItemVisibility.s_listPump[name] = Visibility.Visible;
                            m_dictPump[name] = (PumpItem)cc.MList[0];
                            if (cc.MList[1].MVisible)
                            {
                                name = (ENUMPumpName)Enum.Parse(typeof(ENUMPumpName), cc.MList[1].MConstName);
                                ItemVisibility.s_listPump[name] = Visibility.Visible;
                                m_dictPump[name] = (PumpItem)cc.MList[1];
                            }
                        }
                        else
                        {
                            ENUMPumpName name = (ENUMPumpName)Enum.Parse(typeof(ENUMPumpName), cc.MList[0].MConstName);
                            ItemVisibility.s_listPump[name] = Visibility.Visible;
                            m_dictPump[name] = (PumpItem)cc.MList[0];
                        }
                    }
                    break;
                case ENUMInstrumentType.Detector:
                    {
                        ENUMDetectorID id = (ENUMDetectorID)Enum.Parse(typeof(ENUMDetectorID), cc.MModel);
                        switch (id)
                        {
                            case ENUMDetectorID.ASABD05:
                                {
                                    ENUMASName name= (ENUMASName)Enum.Parse(typeof(ENUMASName), cc.MList[0].MConstName);
                                    ItemVisibility.s_listAS[name] = Visibility.Visible;
                                    m_dictAS[name] = (ASItem)cc.MList[0];
                                }
                                break;
                            case ENUMDetectorID.ASABD06:
                                {
                                    ENUMASName name = (ENUMASName)Enum.Parse(typeof(ENUMASName), cc.MList[0].MConstName);
                                    ItemVisibility.s_listAS[name] = Visibility.Visible;
                                    m_dictAS[name] = (ASItem)cc.MList[0];
                                }
                                break;
                            case ENUMDetectorID.pHHamilton:
                                {
                                    ENUMPHName name = (ENUMPHName)Enum.Parse(typeof(ENUMPHName), cc.MList[0].MConstName);
                                    ItemVisibility.s_listPH[name] = Visibility.Visible;
                                    m_dictPH[name] = (PHItem)cc.MList[0];
                                }
                                {
                                    ENUMTTName name = (ENUMTTName)Enum.Parse(typeof(ENUMTTName), cc.MList[1].MConstName);
                                    ItemVisibility.s_listTEMP[name] = Visibility.Visible;
                                    m_dictTT[name] = (TTItem)cc.MList[1];
                                }
                                break;
                            case ENUMDetectorID.CdHamilton:
                                {
                                    ENUMCDName name = (ENUMCDName)Enum.Parse(typeof(ENUMCDName), cc.MList[0].MConstName);
                                    ItemVisibility.s_listCD[name] = Visibility.Visible;
                                    m_dictCD[name] = (CDItem)cc.MList[0];
                                }
                                {
                                    ENUMTTName name = (ENUMTTName)Enum.Parse(typeof(ENUMTTName), cc.MList[1].MConstName);
                                    ItemVisibility.s_listTEMP[name] = Visibility.Visible;
                                    m_dictTT[name] = (TTItem)cc.MList[1];
                                }
                                break;
                            case ENUMDetectorID.pHCdOEM:
                                {  
                                    ENUMPHName name = (ENUMPHName)Enum.Parse(typeof(ENUMPHName), cc.MList[0].MConstName);
                                    ItemVisibility.s_listPH[name] = Visibility.Visible;
                                    m_dictPH[name] = (PHItem)cc.MList[0];  
                                }
                                {
                                    ENUMCDName name = (ENUMCDName)Enum.Parse(typeof(ENUMCDName), cc.MList[1].MConstName);
                                    ItemVisibility.s_listCD[name] = Visibility.Visible;
                                    m_dictCD[name] = (CDItem)cc.MList[1];
                                }
                                {
                                    ENUMTTName name = (ENUMTTName)Enum.Parse(typeof(ENUMTTName), cc.MList[2].MConstName);
                                    ItemVisibility.s_listTEMP[name] = Visibility.Visible;
                                    m_dictTT[name] = (TTItem)cc.MList[2];
                                }
                                break;
                            case ENUMDetectorID.pHCdHamilton:
                                if (cc.MList[0].MVisible)
                                {
                                    ENUMPHName name = (ENUMPHName)Enum.Parse(typeof(ENUMPHName), cc.MList[0].MConstName);
                                    ItemVisibility.s_listPH[name] = Visibility.Visible;
                                    m_dictPH[name] = (PHItem)cc.MList[0];
                                }
                                if (cc.MList[1].MVisible)
                                {
                                    ENUMPHName name = (ENUMPHName)Enum.Parse(typeof(ENUMPHName), cc.MList[1].MConstName);
                                    ItemVisibility.s_listPH[name] = Visibility.Visible;
                                    m_dictPH[name] = (PHItem)cc.MList[1];
                                }
                                if (cc.MList[2].MVisible)
                                {
                                    ENUMCDName name = (ENUMCDName)Enum.Parse(typeof(ENUMCDName), cc.MList[2].MConstName);
                                    ItemVisibility.s_listCD[name] = Visibility.Visible;
                                    m_dictCD[name] = (CDItem)cc.MList[2];
                                }
                                if (cc.MList[3].MVisible)
                                {
                                    ENUMCDName name = (ENUMCDName)Enum.Parse(typeof(ENUMCDName), cc.MList[3].MConstName);
                                    ItemVisibility.s_listCD[name] = Visibility.Visible;
                                    m_dictCD[name] = (CDItem)cc.MList[3];
                                }
                                if (cc.MList[4].MVisible)
                                {
                                    ENUMTTName name = (ENUMTTName)Enum.Parse(typeof(ENUMTTName), cc.MList[4].MConstName);
                                    ItemVisibility.s_listTEMP[name] = Visibility.Visible;
                                    m_dictTT[name] = (TTItem)cc.MList[4];
                                }
                                if (cc.MList[5].MVisible)
                                {
                                    ENUMTTName name = (ENUMTTName)Enum.Parse(typeof(ENUMTTName), cc.MList[5].MConstName);
                                    ItemVisibility.s_listTEMP[name] = Visibility.Visible;
                                    m_dictTT[name] = (TTItem)cc.MList[5];
                                }
                                if (cc.MList[6].MVisible)
                                {
                                    ENUMTTName name = (ENUMTTName)Enum.Parse(typeof(ENUMTTName), cc.MList[6].MConstName);
                                    ItemVisibility.s_listTEMP[name] = Visibility.Visible;
                                    m_dictTT[name] = (TTItem)cc.MList[6];
                                }
                                if (cc.MList[7].MVisible)
                                {
                                    ENUMTTName name = (ENUMTTName)Enum.Parse(typeof(ENUMTTName), cc.MList[7].MConstName);
                                    ItemVisibility.s_listTEMP[name] = Visibility.Visible;
                                    m_dictTT[name] = (TTItem)cc.MList[7];
                                }
                                break;
                            case ENUMDetectorID.UVQBH2:
                                {
                                    ENUMUVName name = (ENUMUVName)Enum.Parse(typeof(ENUMUVName), cc.MList[0].MConstName);
                                    ItemVisibility.s_listUV[name] = Visibility.Visible;
                                    StaticValue.s_minWaveLength = 190;
                                    StaticValue.s_maxWaveLength = 400;
                                    StaticValue.s_waveEnabledVisible2 = Visibility.Visible;
                                    StaticValue.s_waveVisible3 = Visibility.Collapsed;
                                    StaticValue.s_waveVisible4 = Visibility.Collapsed;
                                    m_dictUV[name] = (UVItem)cc.MList[0];
                                }
                                break;
                            case ENUMDetectorID.UVECOM4:
                                {
                                    ENUMUVName name = (ENUMUVName)Enum.Parse(typeof(ENUMUVName), cc.MList[0].MConstName);
                                    ItemVisibility.s_listUV[name] = Visibility.Visible;
                                    StaticValue.s_minWaveLength = 190;
                                    StaticValue.s_maxWaveLength = 700;
                                    StaticValue.s_waveEnabledVisible2 = Visibility.Collapsed;
                                    StaticValue.s_waveVisible3 = Visibility.Visible;
                                    StaticValue.s_waveVisible4 = Visibility.Visible;
                                    m_dictUV[name] = (UVItem)cc.MList[0];
                                }
                                break;
                            case ENUMDetectorID.RIShodex:
                                {
                                    ENUMRIName name = (ENUMRIName)Enum.Parse(typeof(ENUMRIName), cc.MList[0].MConstName);
                                    ItemVisibility.s_listRI[name] = Visibility.Visible;
                                    m_dictRI[name] = (RIItem)cc.MList[0];
                                }
                                {
                                    ENUMTTName name = (ENUMTTName)Enum.Parse(typeof(ENUMTTName), cc.MList[1].MConstName);
                                    ItemVisibility.s_listTEMP[name] = Visibility.Visible;
                                    m_dictTT[name] = (TTItem)cc.MList[1];
                                }
                                break;
                        }
                    }
                    break;
                case ENUMInstrumentType.Collector:
                    {
                        ENUMCollectorName name = (ENUMCollectorName)Enum.Parse(typeof(ENUMCollectorName), cc.MList[0].MConstName);
                        ItemVisibility.s_listCollector[name] = Visibility.Visible;
                        m_dictCollector[name] = (CollectorItem)cc.MList[0];
                    }
                    break;
                case ENUMInstrumentType.Other:
                    {
                        ENUMOtherID id = (ENUMOtherID)Enum.Parse(typeof(ENUMOtherID), cc.MModel);
                        switch (id)
                        {
                            case ENUMOtherID.Mixer:
                                {
                                    ENUMMixerName name = (ENUMMixerName)Enum.Parse(typeof(ENUMMixerName), cc.MList[0].MConstName);
                                    ItemVisibility.s_listMixer[name] = Visibility.Visible;
                                    m_dictMixer[name] = (MixerItem)cc.MList[0];
                                }
                                break;
                            case ENUMOtherID.ValveMixer:
                                if (cc.MList[0].MVisible)
                                {
                                    ENUMValveName name = (ENUMValveName)Enum.Parse(typeof(ENUMValveName), cc.MList[0].MConstName);
                                    ItemVisibility.s_listValve[name] = Visibility.Visible;
                                    m_dictValve[name] = (ValveItem)cc.MList[0];
                                }
                                if (cc.MList[1].MVisible)
                                {
                                    ENUMValveName name = (ENUMValveName)Enum.Parse(typeof(ENUMValveName), cc.MList[1].MConstName);
                                    ItemVisibility.s_listValve[name] = Visibility.Visible;
                                    m_dictValve[name] = (ValveItem)cc.MList[1];
                                }
                                if (cc.MList[2].MVisible)
                                {
                                    ENUMValveName name = (ENUMValveName)Enum.Parse(typeof(ENUMValveName), cc.MList[2].MConstName);
                                    ItemVisibility.s_listValve[name] = Visibility.Visible;
                                    m_dictValve[name] = (ValveItem)cc.MList[2];
                                }
                                if (cc.MList[3].MVisible)
                                {
                                    ENUMMixerName name = (ENUMMixerName)Enum.Parse(typeof(ENUMMixerName), cc.MList[3].MConstName);
                                    ItemVisibility.s_listMixer[name] = Visibility.Visible;
                                    m_dictMixer[name] = (MixerItem)cc.MList[3];
                                }
                                break;
                        }
                    }
                    break;
            }

            return item;
        }

        public void SetValve(ENUMValveName name, int index)
        {
            if (m_dictValve[name].MValveSet == index)
            {
                return;
            }

            switch (name)
            {
                case ENUMValveName.CPV_1:
                    m_dictValve[ENUMValveName.CPV_1].MValveSet = index;
                    m_dictValve[ENUMValveName.CPV_2].MValveSet = index;
                    break;
                case ENUMValveName.CPV_2:
                    break;
                default:
                    m_dictValve[name].MValveSet = index;
                    break;
            }
        }
        public int GetValveGet(ENUMValveName name)
        {
            return m_dictValve[name].MValveGet;
        }
        public int GetValveSet(ENUMValveName name)
        {
            return m_dictValve[name].MValveSet;
        }
        public void ResetValve()
        {
            foreach(var it in m_dictValve)
            {
                SetValve(it.Key, 0);
            }
        }

        public void SetPump(ENUMPumpName name, double flowVol)
        {
            m_dictPump[name].m_flowSet = flowVol;
        }
        public void SetPumpSystem(double flowVol, double perB = 0, double perC = 0, double perD = 0)
        {
            m_dictPump[ENUMPumpName.FITA].m_flowSet = flowVol * (100 - perB - perC - perD) / 100;
            m_dictPump[ENUMPumpName.FITB].m_flowSet = flowVol * perB / 100;
            m_dictPump[ENUMPumpName.FITC].m_flowSet = flowVol * perC / 100;
            m_dictPump[ENUMPumpName.FITD].m_flowSet = flowVol * perD / 100;
        }
        public void SetPumpSample(double flowVol)
        {
            m_dictPump[ENUMPumpName.FITS].m_flowSet = flowVol;
        }
        public void SetPumpPause(bool pause)
        {
            m_dictPump[ENUMPumpName.FITS].m_pause = pause;
            m_dictPump[ENUMPumpName.FITA].m_pause = pause;
            m_dictPump[ENUMPumpName.FITB].m_pause = pause;
            m_dictPump[ENUMPumpName.FITC].m_pause = pause;
            m_dictPump[ENUMPumpName.FITD].m_pause = pause;
        }
        public double GetPumpGet(ENUMPumpName name)
        {
            return m_dictPump[name].m_flowGet;
        }
        public double GetPumpSet(ENUMPumpName name)
        {
            return m_dictPump[name].m_flowSet;
        }

        public double GetPHGet(ENUMPHName name)
        {
            return m_dictPH[name].m_pHGet;
        }

        public double GetCDGet(ENUMCDName name)
        {
            return m_dictCD[name].m_CdGet;
        }

        public void SetUVWave(ENUMUVName name, UVValue uvValue)
        {
            SetUVWave(name, uvValue.MWave1, uvValue.MWave2, uvValue.MWave3, uvValue.MWave4, uvValue.MEnabledWave2);
        }

        public void SetUVWave(ENUMUVName name, int wave1, int wave2, int wave3, int wave4, bool enabledWave2)
        {
            bool change = false;

            if (m_dictUV[name].m_waveSet[0] != wave1)
            {
                m_dictUV[name].m_waveSet[0] = wave1;
                change = true;
            }
            if (enabledWave2)
            {
                if (m_dictUV[name].m_waveSet[1] != wave2)
                {
                    m_dictUV[name].m_waveSet[1] = wave2;
                    change = true;
                }
            }
            else
            {
                if (m_dictUV[name].m_waveSet[1] != 0)
                {
                    m_dictUV[name].m_waveSet[1] = 0;
                    change = true;
                }
            }
            if (m_dictUV[name].m_waveSet[2] != wave3)
            {
                m_dictUV[name].m_waveSet[2] = wave3;
                change = true;
            }
            if (m_dictUV[name].m_waveSet[3] != wave4)
            {
                m_dictUV[name].m_waveSet[3] = wave4;
                change = true;
            }

            if (change)
            {
                m_dictUV[name].m_wave = true;
            }
        }
        public void SetUVLamp(ENUMUVName name, bool onoff)
        {
            if (onoff)
            {
                m_dictUV[name].m_lampOn = true;
            }
            else
            {
                m_dictUV[name].m_lampOff = true;
            }
        }
        public void SetUVClear(ENUMUVName name)
        {
            m_dictUV[name].m_clear = true;
        }
        public double GetUVGet(ENUMUVName name, int indexWave)
        {
            return m_dictUV[name].m_absGet[indexWave];
        }

        public void SetRITemperature(ENUMRIName name, int temperature)
        {
            bool change = false;

            if (m_dictRI[name].m_tempSet != temperature)
            {
                m_dictRI[name].m_tempSet = temperature;
                change = true;
            }
            if (change)
            {
                m_dictRI[name].m_temperature = true;
            }
        }
        public void SetRIPurge(ENUMRIName name, bool onoff)
        {
            if (onoff)
            {
                m_dictRI[name].m_purgeOn = true;
            }
            else
            {
                m_dictRI[name].m_purgeOff = true;
            }
        }
        public void SetRIClear(ENUMRIName name)
        {
            m_dictRI[name].m_clear = true;
        }

        public bool GetASGet(ENUMASName name)
        {
            return m_dictAS[name].m_sizeGet > StaticSystemConfig.SSystemConfig.MListConfAS[(int)name].MSize;
        }

        public void SetMixer(ENUMMixerName name, bool onoff)
        {
            m_dictMixer[name].m_onoffSet = onoff;
        }
        public MixerItem GetItem(ENUMMixerName name)
        {
            return m_dictMixer[name];
        }

        public UVItem GetItem(ENUMUVName name)
        {
            return m_dictUV[name];
        }

        public RIItem GetItem(ENUMRIName name)
        {
            return m_dictRI[name];
        }

        public CollectorItem GetItem(ENUMCollectorName name)
        {
            return m_dictCollector[name];
        }
    }
}
