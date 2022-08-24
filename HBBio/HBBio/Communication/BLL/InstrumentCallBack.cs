using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    delegate void CallBackDelegate(List<ComConf> comConfList);

    class InstrumentCallBack
    {
        public bool MRun { get; set; }
        private EnumCommunMode m_mode = EnumCommunMode.Com;
        private List<AddressPort> m_listAddressPort;
        private List<Thread> m_threadList = new List<Thread>();
        private List<ComConf> m_comConfList = new List<ComConf>();
        private CallBackDelegate m_callback;      //回调委托


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="callbackDelegate"></param>
        public InstrumentCallBack(EnumCommunMode mode, List<AddressPort> listAddressPort, CallBackDelegate callbackDelegate)
        {
            MRun = true;
            m_mode = mode;
            m_listAddressPort = listAddressPort;
            m_callback = callbackDelegate;
        }

        public void ThreadProc()
        {
            switch (m_mode)
            {
                case EnumCommunMode.Com:
                    {
                        string[] comList = SerialPort.GetPortNames();
                        for (int i = 0; i < comList.Length; i++)
                        {
                            ComConf cc = new ComConf();
                            cc.MCommunMode = EnumCommunMode.Com;
                            cc.MPortName = comList[i];

                            m_comConfList.Add(cc);
                        }
                    }
                    break;
                case EnumCommunMode.TCP:
                    for (int i = 0; i < m_listAddressPort.Count; i++)
                    {
                        ComConf cc = new ComConf();
                        cc.MCommunMode = EnumCommunMode.TCP;
                        cc.MAddress = m_listAddressPort[i].MAddress;
                        cc.MPort = m_listAddressPort[i].MPort;
                        m_comConfList.Add(cc);
                    }
                    break;
            }

            for (int i = 0; i < m_comConfList.Count; i++)
            {
                m_threadList.Add(new Thread(new ParameterizedThreadStart(CreateFindThread)));
                m_threadList[i].IsBackground = true;
                m_threadList[i].Start(i);
            }

            for (int i = 0; i < m_threadList.Count; i++)
            {
                if (m_threadList[i].IsAlive)
                {
                    i--;
                    continue;
                }
            }

            if (null != m_callback)
            {
                m_callback(m_comConfList);
            }
        }

        private void CreateFindThread(object obj)
        {
            int index = Convert.ToInt32(obj);

            CommunicationSetsManager csManager = new CommunicationSetsManager();

            int curr = 0;
            while (MRun)
            {
                switch ((ENUMInstrumentType)curr)
                {
                    case ENUMInstrumentType.Sampler:
                        break;
                    case ENUMInstrumentType.Valve:
                        m_comConfList[index].MType = (ENUMInstrumentType)curr;
                        {
                            m_comConfList[index].MModel = ENUMValveID.VICI4.ToString();
                            if (csManager.FindConn(m_comConfList[index]))
                            {
                                return;
                            }
                            m_comConfList[index].MList.Clear();
                        }
                        {
                            m_comConfList[index].MModel = ENUMValveID.VICI_T6.ToString();
                            if (csManager.FindConn(m_comConfList[index]))
                            {
                                return;
                            }
                            m_comConfList[index].MList.Clear();
                        }
                        {
                            m_comConfList[index].MModel = ENUMValveID.QBH_Coll6.ToString();
                            if (csManager.FindConn(m_comConfList[index]))
                            {
                                return;
                            }
                            m_comConfList[index].MList.Clear();
                        }
                        {
                            m_comConfList[index].MModel = ENUMValveID.HB_Coll6.ToString();
                            if (csManager.FindConn(m_comConfList[index]))
                            {
                                return;
                            }
                            m_comConfList[index].MList.Clear();
                        }
                        {
                            m_comConfList[index].MModel = ENUMValveID.HB_T2.ToString();
                            if (csManager.FindConn(m_comConfList[index]))
                            {
                                return;
                            }
                            m_comConfList[index].MList.Clear();
                        }
                        {
                            m_comConfList[index].MModel = ENUMValveID.HB2.ToString();
                            if (csManager.FindConn(m_comConfList[index]))
                            {
                                return;
                            }
                            m_comConfList[index].MList.Clear();
                        }
                        {
                            m_comConfList[index].MModel = ENUMValveID.HB_GS4.ToString();
                            if (csManager.FindConn(m_comConfList[index]))
                            {
                                return;
                            }
                            m_comConfList[index].MList.Clear();
                        }
                        break;
                    case ENUMInstrumentType.Pump:
                        m_comConfList[index].MType = (ENUMInstrumentType)curr;
                        {
                            m_comConfList[index].MModel = ENUMPumpID.NP7001.ToString();
                            if (csManager.FindConn(m_comConfList[index]))
                            {
                                return;
                            }
                            m_comConfList[index].MList.Clear();
                        }
                        {
                            m_comConfList[index].MModel = ENUMPumpID.OEM0025.ToString();
                            if (csManager.FindConn(m_comConfList[index]))
                            {
                                return;
                            }
                            m_comConfList[index].MList.Clear();
                        }
                        break;
                    case ENUMInstrumentType.Detector:
                        m_comConfList[index].MType = (ENUMInstrumentType)curr;
                        {
                            m_comConfList[index].MModel = ENUMDetectorID.ASABD05.ToString();
                            if (csManager.FindConn(m_comConfList[index]))
                            {
                                return;
                            }
                            m_comConfList[index].MList.Clear();
                        }
                        {
                            m_comConfList[index].MModel = ENUMDetectorID.ASABD06.ToString();
                            if (csManager.FindConn(m_comConfList[index]))
                            {
                                return;
                            }
                            m_comConfList[index].MList.Clear();
                        }
                        {
                            m_comConfList[index].MModel = ENUMDetectorID.pHHamilton.ToString();
                            if (csManager.FindConn(m_comConfList[index]))
                            {
                                return;
                            }
                            m_comConfList[index].MList.Clear();
                        }
                        {
                            m_comConfList[index].MModel = ENUMDetectorID.CdHamilton.ToString();
                            if (csManager.FindConn(m_comConfList[index]))
                            {
                                return;
                            }
                            m_comConfList[index].MList.Clear();
                        }
                        {
                            m_comConfList[index].MModel = ENUMDetectorID.pHCdOEM.ToString();
                            if (csManager.FindConn(m_comConfList[index]))
                            {
                                return;
                            }
                            m_comConfList[index].MList.Clear();
                        }
                        {
                            m_comConfList[index].MModel = ENUMDetectorID.pHCdHamilton.ToString();
                            if (csManager.FindConn(m_comConfList[index]))
                            {
                                return;
                            }
                            m_comConfList[index].MList.Clear();
                        }
                        {
                            m_comConfList[index].MModel = ENUMDetectorID.UVQBH2.ToString();
                            if (csManager.FindConn(m_comConfList[index]))
                            {
                                return;
                            }
                            m_comConfList[index].MList.Clear();
                        }
                        {
                            m_comConfList[index].MModel = ENUMDetectorID.UVECOM4.ToString();
                            if (csManager.FindConn(m_comConfList[index]))
                            {
                                return;
                            }
                            m_comConfList[index].MList.Clear();
                        }
                        break;
                    case ENUMInstrumentType.Collector:
                        m_comConfList[index].MType = (ENUMInstrumentType)curr;
                        {
                            m_comConfList[index].MModel = ENUMCollectorID.QBH_DLY.ToString();
                            if (csManager.FindConn(m_comConfList[index]))
                            {
                                return;
                            }
                            m_comConfList[index].MList.Clear();
                        }
                        {
                            m_comConfList[index].MModel = ENUMCollectorID.HB_DLY_W.ToString();
                            if (csManager.FindConn(m_comConfList[index]))
                            {
                                return;
                            }
                            m_comConfList[index].MList.Clear();
                        }
                        break;
                    case ENUMInstrumentType.Other:
                        m_comConfList[index].MType = (ENUMInstrumentType)curr;
                        {
                            m_comConfList[index].MModel = ENUMOtherID.Mixer.ToString();
                            if (csManager.FindConn(m_comConfList[index]))
                            {
                                return;
                            }
                            m_comConfList[index].MList.Clear();
                        }
                        {
                            m_comConfList[index].MModel = ENUMOtherID.ValveMixer.ToString();
                            if (csManager.FindConn(m_comConfList[index]))
                            {
                                return;
                            }
                            m_comConfList[index].MList.Clear();
                        }
                        return;
                }
                curr++;
            }
        }
    }
}
