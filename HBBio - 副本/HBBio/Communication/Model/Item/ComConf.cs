using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    /**
     * ClassName: ComConf
     * Description: Com通讯的配置信息状态
     * Version: 1.0
     * Create:  2019/05/28
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    public class ComConf : DlyNotifyPropertyChanged
    {
        private int m_id = -1;                              //-1表示新建，非-1表示从数据库读取(数据库)                                 
        public int MId
        {
            get
            {
                return m_id;
            }
            set
            {
                m_id = value;
            }
        }

        private ENUMInstrumentType m_type = ENUMInstrumentType.Valve;       //类型(数据库)  
        public ENUMInstrumentType MType
        {
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
                List<string> list = Share.ReadXaml.GetEnumList<ENUMInstrumentType>("Com_");
                if (null != list && (int)MType < list.Count)
                {
                    MName = list[(int)MType];
                }
                else
                {
                    MName = MType.ToString();
                }
            }
        }                 

        public string MName { get; set; }                   //类型字符(仅作显示用途)

        private string m_model = "";                        //型号(数据库) 
        public string MModel
        {
            get
            {
                return m_model;
            }
            set
            {
                m_model = value;

                UpdateList();
            }
        }

        public EnumCommunMode MCommunMode { get; set; }

        private string m_portName = "null";                 //COM号(数据库)              
        public string MPortName
        {
            get
            {
                return m_portName;
            }
            set
            {
                m_portName = value;
                OnPropertyChanged("MPortName");
            }
        }

        private string m_address = "null";
        public string MAddress
        {
            get
            {
                return m_address;
            }
            set
            {
                m_address = value;
                OnPropertyChanged("MAddress");
            }
        }

        private string m_port = "null";
        public string MPort
        {
            get
            {
                return m_port;
            }
            set
            {
                m_port = value;
                OnPropertyChanged("MPort");
            }
        }
         
        private string m_version = "";                      //版本号(数据库) 
        public string MVersion
        {
            get
            {
                return m_version;
            }
            set
            {
                m_version = value;
                OnPropertyChanged("MVersion");
            }
        }                         

        private string m_serial = "";                       //序列号(数据库) 
        public string MSerial
        {
            get
            {
                return m_serial;
            }
            set
            {
                m_serial = value;
                OnPropertyChanged("MSerial");
            }
        }

        private bool m_alarm = false;                       //启用报警(数据库) 
        public bool MAlarm
        {
            get
            {
                return m_alarm;
            }
            set
            {
                m_alarm = value;
            }
        }

        private string m_result = "";                       //结果         
        public string MResult
        {
            get
            {
                return m_result;
            }
            set
            {
                m_result = value;
                OnPropertyChanged("MResult");
            }
        }

        public List<BaseInstrument> MList = new List<BaseInstrument>();       //列表


        /// <summary>
        /// 构造函数，新建使用
        /// </summary>
        /// <param name="portName"></param>
        public ComConf()
        {

        }

        /// <summary>
        /// 构造函数，数据库使用
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="model"></param>
        /// <param name="portName"></param>
        /// <param name="version"></param>
        /// <param name="serial"></param>
        public ComConf(int id, ENUMInstrumentType type, string model, string portName, string address, string port, string version, string serial, bool alarm)
        {
            m_id = id;
            MType = type;
            MModel = model;
            MPortName = portName;
            MAddress = address;
            MPort = port;
            MVersion = version;
            MSerial = serial;
            MAlarm = alarm;
        }

        /// <summary>
        /// 更新列表
        /// </summary>
        private void UpdateList()
        {
            MList.Clear();
            switch (MType)
            {
                case ENUMInstrumentType.Sampler:
                    {
                        MList.Add(new SamplerItem());
                    }
                    break;
                case ENUMInstrumentType.Valve:
                    {
                        MList.Add(new ValveItem());
                    }
                    break;
                case ENUMInstrumentType.Pump:
                    {
                        if (!string.IsNullOrEmpty(MModel))
                        {
                            if (((ENUMPumpID)Enum.Parse(typeof(ENUMPumpID), MModel)).ToString().Contains("OEM"))
                            {
                                MList.Add(new PumpItem());
                                MList.Add(new PumpItem());
                                MList.Add(new PTItem());
                                MList.Add(new PTItem());
                                MList.Add(new PTItem());
                            }
                            else
                            {
                                MList.Add(new PumpItem());
                                MList.Add(new PTItem());
                            }
                        }
                    }
                    break;
                case ENUMInstrumentType.Detector:
                    {
                        if (!string.IsNullOrEmpty(MModel))
                        {
                            ENUMDetectorID id = (ENUMDetectorID)Enum.Parse(typeof(ENUMDetectorID), MModel);
                            switch (id)
                            {
                                case ENUMDetectorID.ASABD05:
                                case ENUMDetectorID.ASABD06:
                                    MList.Add(new ASItem());
                                    break;
                                case ENUMDetectorID.pHHamilton:
                                    MList.Add(new PHItem());
                                    MList.Add(new TTItem());
                                    break;
                                case ENUMDetectorID.CdHamilton:
                                    MList.Add(new CDItem());
                                    MList.Add(new TTItem());
                                    break;
                                case ENUMDetectorID.pHCdOEM:
                                    MList.Add(new PHItem());
                                    MList.Add(new CDItem());
                                    MList.Add(new TTItem());
                                    break;
                                case ENUMDetectorID.pHCdHamilton:
                                    MList.Add(new PHItem());
                                    MList.Add(new PHItem());
                                    MList.Add(new CDItem());
                                    MList.Add(new CDItem());
                                    MList.Add(new TTItem());
                                    MList.Add(new TTItem());
                                    MList.Add(new TTItem());
                                    MList.Add(new TTItem());
                                    break;
                                case ENUMDetectorID.UVQBH2:
                                    MList.Add(new UVItem(2));
                                    break;
                                case ENUMDetectorID.UVECOM4:
                                    MList.Add(new UVItem(4));
                                    break;
                                case ENUMDetectorID.RIShodex:
                                    MList.Add(new RIItem());
                                    MList.Add(new TTItem());
                                    break;
                            }
                        }
                    }
                    break;
                case ENUMInstrumentType.Collector:
                    {
                        MList.Add(new CollectorItem());
                    }
                    break;
                case ENUMInstrumentType.Other:
                    {
                        if (!string.IsNullOrEmpty(MModel))
                        {
                            ENUMOtherID id = (ENUMOtherID)Enum.Parse(typeof(ENUMOtherID), MModel);
                            switch (id)
                            {
                                case ENUMOtherID.Mixer:
                                    MList.Add(new MixerItem());
                                    break;
                                case ENUMOtherID.ValveMixer:
                                    MList.Add(new ValveItem());
                                    MList.Add(new ValveItem());
                                    MList.Add(new ValveItem());
                                    MList.Add(new MixerItem());
                                    break;
                            }
                        }
                    }
                    break;
            }
        }
    }
}
