using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.Communication
{
    class CommunicationSetsManager
    {
        /// <summary>
        /// 新建通讯配置
        /// </summary>
        /// <param name="cs"></param>
        /// <param name="cfList"></param>
        /// <param name="biList"></param>
        /// <param name="snList"></param>
        /// <returns></returns>
        public string AddItem(CommunicationSets cs, List<ComConf> cfList)
        {
            string error = null;

            CommunicationSetsTable csDB = new CommunicationSetsTable();
            if (null == (error = csDB.InsertRow(cs)))
            {
                int lastId = -1;
                if (null == (error = csDB.GetLastID(out lastId)))
                {
                    ComConfTable ccDB = new ComConfTable(lastId);
                    ccDB.InitTable();
                    BaseInstrumentTable biDB = new BaseInstrumentTable(lastId);
                    biDB.InitTable();
                    CirclePointTable cpDB = new CirclePointTable(lastId);
                    cpDB.InitTable();
                    ColumnPointTable columnPointTable = new ColumnPointTable(lastId);
                    columnPointTable.InitTable();
                    InstrumentPointTable ipDB = new InstrumentPointTable(lastId);
                    ipDB.InitTable();
                    InstrumentSizeTable isDB = new InstrumentSizeTable(lastId);
                    isDB.InitTable();
                    SignalTable snDB = new SignalTable(lastId);
                    snDB.InitTable();
                    SystemConfigTable scDB = new SystemConfigTable(lastId);
                    scDB.InitTable();

                    if (null == (error = ccDB.InitDataList(cfList)))
                    {
                        double totalFlow = 0;
                        Signal tmp = null;
                        SystemConfig config = new SystemConfig();
                        foreach (var itCF in cfList)
                        {
                            for (int i = 0; i < itCF.MList.Count; i++)//已经被隐藏的需要在这里删除
                            {
                                if (!itCF.MList[i].MVisible)
                                {
                                    itCF.MList.RemoveAt(i);
                                    --i;
                                }
                            }

                            foreach (var itBI in itCF.MList)
                            {
                                itBI.MComConfId = itCF.MId;

                                if (itBI.MConstName.Contains("AS"))
                                {
                                    config.AddAS();
                                }
                                else if (itBI.MConstName.Contains("pH") || itBI.MConstName.Contains("Cd"))
                                {
                                    config.AddPHCDUV(itBI.MConstName);
                                }
                                else if (itBI.MConstName.Contains("UV"))
                                {
                                    for (int i = 0; i < ((UVItem)itBI).m_signalCount; i++)
                                    {
                                        config.AddPHCDUV(itBI.MConstName + "_" + (i + 1));
                                    }
                                }
                                else if (itBI.MConstName.Contains("Coll"))
                                {
                                    config.MConfCollector.MCountL = ((CollectorItem)itBI).m_countL;
                                    config.MConfCollector.MCountR = ((CollectorItem)itBI).m_countR;
                                }
                            }
                            
                            if (null == (error = biDB.InitDataList(itCF.MList)))
                            {
                                foreach (var itBI in itCF.MList)
                                {
                                    List<Signal> snList = itBI.CreateSignalList();
                                    foreach (var itSN in snList)
                                    {
                                        itSN.MBaseInstrumentId = itBI.MId;

                                        if(itSN.MConstName.Equals("FIT_Total"))
                                        {
                                            tmp = itSN;
                                        }

                                        if (itSN.MConstName.Equals(ENUMPumpName.FITA.ToString())
                                            || itSN.MConstName.Equals(ENUMPumpName.FITB.ToString())
                                            || itSN.MConstName.Equals(ENUMPumpName.FITC.ToString())
                                            || itSN.MConstName.Equals(ENUMPumpName.FITD.ToString()))
                                        {
                                            totalFlow += itSN.MValMax;
                                        }
                                    }
                                    error = snDB.InitDataList(snList);
                                } 
                            }
                        }
                        scDB.UpdateRow(config.GetDBInfo());
                        if (null != tmp)
                        {
                            tmp.MValMax = totalFlow;
                            snDB.UpdateRowTotalFIT(tmp);
                        }
                    }
                }
            }

            return error;
        }

        /// <summary>
        /// 编辑通讯配置
        /// </summary>
        /// <param name="cs"></param>
        /// <param name="cfList"></param>
        /// <param name="biList"></param>
        /// <param name="snList"></param>
        /// <returns></returns>
        public string EditItem(CommunicationSets cs, List<ComConf> cfList, List<BaseInstrument> biList, List<InstrumentPoint> ipList, InstrumentSize size, List<Point> listCircle, List<Point> listColumn)
        {
            string result = null;

            CommunicationSetsTable csDB = new CommunicationSetsTable();
            result += csDB.UpdateRow(cs);

            ComConfTable ccDB = new ComConfTable(cs.MId);
            result += ccDB.UpdateDataList(cfList);

            BaseInstrumentTable biDB = new BaseInstrumentTable(cs.MId);
            result += biDB.UpdateDataList(biList);

            InstrumentPointTable ipDB = new InstrumentPointTable(cs.MId);
            result += ipDB.UpdateDataList(ipList);

            InstrumentSizeTable isDB = new InstrumentSizeTable(cs.MId);
            result += isDB.UpdateRow(size);

            CirclePointTable cpDB = new CirclePointTable(cs.MId);
            result += cpDB.UpdateList(listCircle);

            ColumnPointTable columnPointTable = new ColumnPointTable(cs.MId);
            result += columnPointTable.UpdateList(listColumn);

            TimeSetTable rtDB = new TimeSetTable();
            foreach (var it in biList)
            {
                result += rtDB.UpdateRowSetTime(it.MTimeSetId, it.MSetTime);
                result += rtDB.UpdateRowCalibration(it.MTimeSetId, it.MCalibration);
            }

            if (string.IsNullOrEmpty(result))
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// 编辑通讯配置
        /// </summary>
        /// <param name="cs"></param>
        /// <param name="cfList"></param>
        /// <returns></returns>
        public string EditItem(CommunicationSets cs, List<ComConf> cfList)
        {
            ComConfTable ccDB = new ComConfTable(cs.MId);
            return ccDB.UpdateDataListAlarm(cfList);
        }

        /// <summary>
        /// 编辑通讯配置
        /// </summary>
        /// <param name="cs"></param>
        /// <param name="cfList"></param>
        /// <param name="biList"></param>
        /// <param name="snList"></param>
        /// <returns></returns>
        public string EditItem(CommunicationSets cs, List<Signal> snList)
        {
            string result = null;

            SignalTable snDB = new SignalTable(cs.MId);
            result += snDB.UpdateDataList(snList);

            if (string.IsNullOrEmpty(result))
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// 启用或停用通讯配置
        /// </summary>
        /// <param name="cs"></param>
        /// <returns></returns>
        public bool EnabledItem(CommunicationSets cs)
        {
            CommunicationSetsTable csDB = new CommunicationSetsTable();
            return null == csDB.UpdateRow(cs);
        }

        /// <summary>
        /// 删除通讯配置
        /// </summary>
        /// <param name="cs"></param>
        /// <returns></returns>
        public bool DeleteItem(CommunicationSets cs)
        {
            CommunicationSetsTable csDB = new CommunicationSetsTable();
            return null == csDB.DeleteRow(cs.MId);
        }

        /// <summary>
        /// 获取通讯配置的行具体信息
        /// </summary>
        /// <param name="cs"></param>
        /// <param name="cfList"></param>
        /// <param name="biList"></param>
        /// <param name="snList"></param>
        /// <returns></returns>
        public string GetItem(CommunicationSets cs, out List<ComConf> cfList, out List<InstrumentPoint> ipList, out InstrumentSize size, out List<Point> listCircle, out List<Point> listColumn)
        {
            string error = null;
            TimeSetTable tsDB = new TimeSetTable();
            ComConfTable ccDB = new ComConfTable(cs.MId);

            BaseInstrumentTable biDB = new BaseInstrumentTable(cs.MId);
            SignalTable snDB = new SignalTable(cs.MId);
            InstrumentPointTable ipDB = new InstrumentPointTable(cs.MId);
            InstrumentSizeTable isDB = new InstrumentSizeTable(cs.MId);
            CirclePointTable cpDB = new CirclePointTable(cs.MId);
            ColumnPointTable columnPointTable = new ColumnPointTable(cs.MId);

            if (null == (error = ccDB.GetDataList(out cfList)))
            {
                foreach(var itCC in cfList)
                {
                    itCC.MCommunMode = (EnumCommunMode)Enum.Parse(typeof(EnumCommunMode), cs.MCommunMode);
                    if (null == (error = biDB.GetDataListByComConfID(itCC.MId, ref itCC.MList)))
                    {
                        foreach (var itBI in itCC.MList)
                        {
                            itBI.MType = itCC.MType;
                            itBI.MModel = itCC.MModel;
                            itBI.MPortName = itCC.MPortName;
                            itBI.MAddress = itCC.MAddress;
                            itBI.MPort = itCC.MPort;

                            double setTime = 0;
                            double runTime = 0;
                            DateTime datetime = DateTime.Now;
                            tsDB.GetRow(itBI.MTimeSetId, ref setTime, ref runTime, ref datetime);
                            itBI.MSetTime = setTime;
                            itBI.MRunTime = runTime;
                            itBI.MCalibration = datetime;
                            error = snDB.GetDataListByBaseInstrumentID(itBI.MId, out itBI.m_list);
                        }
                    }
                }  
            }

            ipDB.GetDataList(out ipList);
            isDB.SelectRow(out size);
            cpDB.GetList(out listCircle);
            columnPointTable.GetList(out listColumn);

            return error;
        }

        /// <summary>
        /// 获取通讯配置列表
        /// </summary>
        /// <param name="csList"></param>
        /// <returns></returns>
        public string GetList(out List<CommunicationSets> csList)
        {
            CommunicationSetsTable csDB = new CommunicationSetsTable();
            return csDB.GetDataList(out csList);
        }

        /// <summary>
        /// 获取可用的新名称
        /// </summary>
        /// <returns></returns>
        public string GetNewName()
        {
            CommunicationSetsTable csDB = new CommunicationSetsTable();
            List<string> nameList = null;
            if (null == csDB.GetNameList(out nameList))
            {
                int temp = 1;
                for (int i = 0; i < nameList.Count; i++)
                {
                    if (nameList[i].Contains("System"))
                    {
                        int curr = Convert.ToInt32(nameList[i].Remove(0, 6));
                        if (curr >= temp)
                        {
                            temp = curr + 1;
                        }
                    }
                }

                return "System" + temp;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 检查通讯状态
        /// </summary>
        /// <param name="comConf"></param>
        public bool CheckConn(ComConf comConf)
        {
            bool result = false;
            string version = comConf.MVersion;
            string serial = comConf.MSerial;

            BaseCommunication item = CreateItem(comConf);
            if (item.Connect())
            {
                if (item.ReadVersion(ref version) && item.ReadSerial(ref serial))
                {
                    result = true;
                    comConf.MResult = Share.ReadXaml.S_SuccessTxt;
                }
                else
                {
                    comConf.MResult = Share.ReadXaml.S_FailureTxt;
                }
                item.Close();
            }
            else
            {
                comConf.MResult = Share.ReadXaml.S_FailureTxt;
            }
            comConf.MVersion = version;
            comConf.MSerial = serial;

            return result;
        }

        /// <summary>
        /// 检查通讯状态
        /// </summary>
        /// <param name="comConf"></param>
        public bool MatchConn(ComConf comConf)
        {
            bool result = false;
            string version = comConf.MVersion;
            string serial = comConf.MSerial;
            string model = "";

            BaseCommunication item = CreateItem(comConf);
            if (item.Connect())
            {
                if (item.ReadModel(ref model) && model.Equals(comConf.MModel) && item.ReadVersion(ref version) && item.ReadSerial(ref serial))
                {
                    result = true;
                    comConf.MResult = Share.ReadXaml.S_SuccessTxt;
                }
                else
                {
                    comConf.MResult = Share.ReadXaml.S_FailureTxt;
                }
                item.Close();
            }
            else
            {
                comConf.MResult = Share.ReadXaml.S_FailureTxt;
            }
            comConf.MVersion = version;
            comConf.MSerial = serial;

            return result;
        }

        /// <summary>
        /// 检查通讯状态
        /// </summary>
        /// <param name="comConf"></param>
        public bool FindConn(ComConf comConf)
        {
            bool result = false;

            BaseCommunication item = CreateItem(comConf);
            if (null != item && item.Connect())
            {
                string version = null;
                string serial = null;
                string model = comConf.MModel;
                if ((item.ReadModel(ref model)|| item.ReadModel(ref model)) && item.ReadVersion(ref version) && item.ReadSerial(ref serial))
                {
                    comConf.MModel = model;
                    comConf.MVersion = version;
                    comConf.MSerial = serial;
                    result = true;
                    comConf.MResult = Share.ReadXaml.S_SuccessTxt;
                }
                else
                {
                    comConf.MResult = Share.ReadXaml.S_FailureTxt;
                }
                item.Close();
            }
            else
            {
                comConf.MResult = Share.ReadXaml.S_FailureTxt;
            }
            
            return result;
        }

        /// <summary>
        /// 更新列表
        /// </summary>
        public void CreateInstrumentList(ComConf cc)
        {
            TimeSetTable rtDB = new TimeSetTable();
            int timeSetId = 0;
            double setTime = 0;
            double runTime = 0;
            DateTime datetime = DateTime.Now;
            for (int i = 0; i < cc.MList.Count; i++)
            {
                cc.MList[i].MType = cc.MType;
                cc.MList[i].MModel = cc.MModel;
                cc.MList[i].MPortName = cc.MPortName;
                cc.MList[i].MAddress = cc.MAddress;
                cc.MList[i].MPort = cc.MPort;
                cc.MList[i].MIndex = i;
                rtDB.GetRow(cc.MVersion, cc.MSerial, i, ref timeSetId, ref setTime, ref runTime, ref datetime);
                cc.MList[i].MTimeSetId = timeSetId;
                cc.MList[i].MSetTime = setTime;
                cc.MList[i].MRunTime = runTime;
                cc.MList[i].MCalibration = datetime;
            }
        }

        /// <summary>
        /// 创建仪器对象
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        public BaseCommunication CreateItem(ComConf cc)
        {
            switch (cc.MCommunMode)
            {
                case EnumCommunMode.TCP:
                    return CreateItemTCP(cc);
                default:
                    return CreateItemCom(cc);
            }
        }

        /// <summary>
        /// 创建仪器对象
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        private BaseCom CreateItemCom(ComConf cc)
        {
            BaseCom item = null;

            switch (cc.MType)
            {
                case ENUMInstrumentType.Sampler:
                    {
                        
                    }
                    break;
                case ENUMInstrumentType.Valve:
                    {
                        ENUMValveID id = (ENUMValveID)Enum.Parse(typeof(ENUMValveID), cc.MModel);
                        if (id.ToString().Contains("VICI"))
                        {
                            item = new ComValveVICI(cc);
                        }
                        else if (id.ToString().Contains("HB_Coll"))
                        {
                            item = new ComValveHBColl(cc);
                        }
                        else if (id.ToString().Contains("QBH_Coll"))
                        {
                            item = new ComValveQBHColl(cc);
                        }
                        else if (id.ToString().Contains("HB_T"))
                        {
                            item = new ComValveHB2(cc);
                        }
                        else if (id.ToString().Contains("HB_GS4"))
                        {
                            item = new ComValveHBGS4(cc);
                        }
                        else
                        {
                            item = new ComValveHB(cc);
                        }
                    }
                    break;
                case ENUMInstrumentType.Pump:
                    {
                        if (((ENUMPumpID)Enum.Parse(typeof(ENUMPumpID), cc.MModel)).ToString().Contains("OEM"))
                        {
                            item = new ComPumpOEM(cc);
                        }
                        else
                        {
                            item = new ComPumpQBH(cc);
                        }
                    }
                    break;
                case ENUMInstrumentType.Detector:
                    {
                        ENUMDetectorID id = (ENUMDetectorID)Enum.Parse(typeof(ENUMDetectorID), cc.MModel);
                        switch (id)
                        {
                            case ENUMDetectorID.ASABD05:
                                item = new ComASABD05(cc);
                                break;
                            case ENUMDetectorID.ASABD06:
                                item = new ComASABD06(cc);
                                break;
                            case ENUMDetectorID.pHHamilton:
                                item = new ComPHHamilton(cc);
                                break;
                            case ENUMDetectorID.CdHamilton:
                                item = new ComCDHamilton(cc);
                                break;
                            case ENUMDetectorID.pHCdOEM:
                                item = new ComPHCDOEM(cc);
                                break;
                            case ENUMDetectorID.pHCdHamilton:
                                item = new ComPHCDHamilton(cc);
                                break;
                            case ENUMDetectorID.UVQBH2:
                                item = new ComUVQBH2(cc);
                                break;
                            case ENUMDetectorID.UVECOM4:
                                item = new ComUVECOM4(cc);
                                break;
                            case ENUMDetectorID.RIShodex:
                                item = new ComRI(cc);
                                break;
                        }
                    }
                    break;
                case ENUMInstrumentType.Collector:
                    {
                        ENUMCollectorID id = (ENUMCollectorID)Enum.Parse(typeof(ENUMCollectorID), cc.MModel);
                        switch(id)
                        {
                            case ENUMCollectorID.QBH_DLY:
                                item = new ComCollectorQBH(cc);
                                break;
                            case ENUMCollectorID.HB_DLY_W:
                                item = new ComCollectorHB(cc);
                                ((ComCollectorHB)item).MHBMode = ENUMCollectorID.HB_DLY_W;
                                break;
                            case ENUMCollectorID.HB_DLY_B:
                                item = new ComCollectorHB(cc);
                                ((ComCollectorHB)item).MHBMode = ENUMCollectorID.HB_DLY_B;
                                break;
                        }
                    }
                    break;
                case ENUMInstrumentType.Other:
                    {
                        ENUMOtherID id = (ENUMOtherID)Enum.Parse(typeof(ENUMOtherID), cc.MModel);
                        switch (id)
                        {
                            case ENUMOtherID.Mixer:
                                item = new ComMixer(cc);
                                break;
                            case ENUMOtherID.ValveMixer:
                                item = new ComValveMixerHB(cc);
                                break;
                        }
                    }
                    break;
            }

            return item;
        }

        /// <summary>
        /// 创建仪器对象
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        private BaseTCP CreateItemTCP(ComConf cc)
        {
            BaseTCP item = null;

            switch (cc.MType)
            {
                case ENUMInstrumentType.Sampler:
                    {

                    }
                    break;
                case ENUMInstrumentType.Valve:
                    {
                        ENUMValveID id = (ENUMValveID)Enum.Parse(typeof(ENUMValveID), cc.MModel);
                        if (id.ToString().Contains("VICI_T"))
                        {
                            item = new TCPValveVICI2(cc);
                        }
                        else if(id.ToString().Contains("VICI"))
                        {
                            item = new TCPValveVICI(cc);
                        }
                        else if (id.ToString().Contains("HB_GS4"))
                        {
                            item = new TCPValveHBGS4(cc);
                        }
                        else
                        {
                            //item = new TCPValveHB(cc);
                        }
                    }
                    break;
                case ENUMInstrumentType.Pump:
                    {
                        if (((ENUMPumpID)Enum.Parse(typeof(ENUMPumpID), cc.MModel)).ToString().Contains("OEM"))
                        {
                            item = new TCPPumpOEM(cc);
                        }
                        else
                        {
                            item = new TCPPumpQBH(cc);
                        }
                    }
                    break;
                case ENUMInstrumentType.Detector:
                    {
                        ENUMDetectorID id = (ENUMDetectorID)Enum.Parse(typeof(ENUMDetectorID), cc.MModel);
                        switch (id)
                        {
                            case ENUMDetectorID.ASABD05:
                                item = new TCPASABD05(cc);
                                break;
                            case ENUMDetectorID.ASABD06:
                                item = new TCPASABD06(cc);
                                break;
                            case ENUMDetectorID.pHHamilton:
                                item = new TCPPHHamilton(cc);
                                break;
                            case ENUMDetectorID.CdHamilton:
                                item = new TCPCDHamilton(cc);
                                break;
                            case ENUMDetectorID.pHCdOEM:
                                item = new TCPPHCDOEM(cc);
                                break;
                            case ENUMDetectorID.pHCdHamilton:
                                item = new TCPPHCDHamilton(cc);
                                break;
                            case ENUMDetectorID.UVQBH2:
                                item = new TCPUVQBH2(cc);
                                break;
                            case ENUMDetectorID.UVECOM4:
                                item = new TCPUVECOM4(cc);
                                break;
                        }
                    }
                    break;
                case ENUMInstrumentType.Collector:
                    {
                        ENUMCollectorID id = (ENUMCollectorID)Enum.Parse(typeof(ENUMCollectorID), cc.MModel);
                        switch(id)
                        {
                            case ENUMCollectorID.QBH_DLY:
                                item = new TCPCollectorQBH(cc);
                                break;
                            case ENUMCollectorID.HB_DLY_W:
                                item = new TCPCollectorHB(cc);
                                ((TCPCollectorHB)item).MHBMode = ENUMCollectorID.HB_DLY_W;
                                break;
                            case ENUMCollectorID.HB_DLY_B:
                                item = new TCPCollectorHB(cc);
                                ((TCPCollectorHB)item).MHBMode = ENUMCollectorID.HB_DLY_B;
                                break;
                        }
                    }
                    break;
                case ENUMInstrumentType.Other:
                    {
                        ENUMOtherID id = (ENUMOtherID)Enum.Parse(typeof(ENUMOtherID), cc.MModel);
                        switch (id)
                        {
                            case ENUMOtherID.Mixer:
                                item = new TCPMixer(cc);
                                break;
                            case ENUMOtherID.ValveMixer:
                                item = new TCPValveMixerHB(cc);
                                break;
                        }
                    }
                    break;
            }

            return item;
        }

        /// <summary>
        /// 获取仪表配置
        /// </summary>
        /// <param name="cs"></param>
        /// <param name="cfList"></param>
        /// <param name="biList"></param>
        /// <param name="snList"></param>
        /// <returns></returns>
        public string GetSystemConfig(CommunicationSets cs, out string scInfo)
        {
            SystemConfigTable scDB = new SystemConfigTable(cs.MId);
            return scDB.SelectRow(out scInfo);
        }

        /// <summary>
        /// 编辑仪表配置
        /// </summary>
        /// <param name="cs"></param>
        /// <param name="cfList"></param>
        /// <param name="biList"></param>
        /// <param name="snList"></param>
        /// <returns></returns>
        public string EditSystemConfig(CommunicationSets cs, string scInfo)
        {
            SystemConfigTable scDB = new SystemConfigTable(cs.MId);
            return scDB.UpdateRow(scInfo);
        }
    }
}
