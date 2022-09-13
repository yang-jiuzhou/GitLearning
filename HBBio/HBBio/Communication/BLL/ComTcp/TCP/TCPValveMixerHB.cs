using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class TCPValveMixerHB : BaseTCP
    {
        private EnumValveMixerState m_state = EnumValveMixerState.Free;         //状态
        private ValveItem m_itemV1 = new ValveItem();       //元素1
        private ValveItem m_itemV2 = new ValveItem();       //元素2
        private ValveItem m_itemV3 = new ValveItem();       //元素3
        private MixerItem m_itemM = new MixerItem();        //


        /// <summary>
        /// 构造函数
        /// </summary>
        public TCPValveMixerHB(ComConf info) : base(info)
        {
            if (0 != m_scInfo.MList.Count)
            {
                m_itemV1 = (ValveItem)m_scInfo.MList[0];
                m_itemV2 = (ValveItem)m_scInfo.MList[1];
                m_itemV3 = (ValveItem)m_scInfo.MList[2];
                m_itemM = (MixerItem)m_scInfo.MList[3];
            }

            MComConf = info;
        }

        /// <summary>
        /// 获取运行数据读值
        /// </summary>
        /// <returns></returns>
        public override List<object> GetRunDataValueList()
        {
            List<object> result = new List<object>();

            if (m_itemV1.MVisible)
            {
                result.Add(m_itemV1.MValveGetStr);
            }
            if (m_itemV2.MVisible)
            {
                result.Add(m_itemV2.MValveGetStr);
            }
            if (m_itemV3.MVisible)
            {
                result.Add(m_itemV3.MValveGetStr);
            }
            if (m_itemM.MVisible)
            {
                result.Add(m_itemM.m_onoffGet ? Share.ReadXaml.S_On : Share.ReadXaml.S_Off);
            }

            return result;
        }

        /// <summary>
        /// 获取运行数据写值
        /// </summary>
        /// <returns></returns>
        public override List<object> SetRunDataValueList()
        {
            List<object> result = new List<object>();

            if (m_itemV1.MVisible)
            {
                result.Add(m_itemV1.MValveSetStr);
            }
            if (m_itemV2.MVisible)
            {
                result.Add(m_itemV2.MValveSetStr);
            }
            if (m_itemV3.MVisible)
            {
                result.Add(m_itemV3.MValveSetStr);
            }
            if (m_itemM.MVisible)
            {
                result.Add(m_itemM.m_onoffSet ? Share.ReadXaml.S_On : Share.ReadXaml.S_Off);
            }

            return result;
        }

        /// <summary>
        /// 属性，重载配置参数
        /// </summary>
        public override ComConf MComConf
        {
            get
            {
                return m_scInfo;
            }
            set
            {
                m_scInfo = value;

                if (string.IsNullOrEmpty(m_scInfo.MModel))
                {
                    return;
                }

                if (null != m_scInfo.MList[0] && m_scInfo.MList[0].MVisible)
                {
                    ValveItem valveItem = (ValveItem)m_scInfo.MList[0];
                    switch ((ENUMValveName)Enum.Parse(typeof(ENUMValveName), m_scInfo.MList[0].MConstName))
                    {
                        case ENUMValveName.InS: EnumInSInfo.Init(2); valveItem.m_enumNames = EnumInSInfo.NameList; break;
                        case ENUMValveName.InA: EnumInAInfo.Init(2); valveItem.m_enumNames = EnumInAInfo.NameList; break;
                        case ENUMValveName.InB: EnumInBInfo.Init(2); valveItem.m_enumNames = EnumInBInfo.NameList; break;
                        case ENUMValveName.InC: EnumInCInfo.Init(2); valveItem.m_enumNames = EnumInCInfo.NameList; break;
                        case ENUMValveName.InD: EnumInDInfo.Init(2); valveItem.m_enumNames = EnumInDInfo.NameList; break;
                        case ENUMValveName.CPV_1:
                        case ENUMValveName.CPV_2: EnumCPVInfo.Init(2); valveItem.m_enumNames = EnumCPVInfo.NameList; break;
                        case ENUMValveName.Out: EnumOutInfo.Init(2); valveItem.m_enumNames = EnumOutInfo.NameList; break;
                    }
                }

                if (null != m_scInfo.MList[1] && m_scInfo.MList[1].MVisible)
                {
                    ValveItem valveItem = (ValveItem)m_scInfo.MList[1];
                    switch ((ENUMValveName)Enum.Parse(typeof(ENUMValveName), m_scInfo.MList[1].MConstName))
                    {
                        case ENUMValveName.InS: EnumInSInfo.Init(2); valveItem.m_enumNames = EnumInSInfo.NameList; break;
                        case ENUMValveName.InA: EnumInAInfo.Init(2); valveItem.m_enumNames = EnumInAInfo.NameList; break;
                        case ENUMValveName.InB: EnumInBInfo.Init(2); valveItem.m_enumNames = EnumInBInfo.NameList; break;
                        case ENUMValveName.InC: EnumInCInfo.Init(2); valveItem.m_enumNames = EnumInCInfo.NameList; break;
                        case ENUMValveName.InD: EnumInDInfo.Init(2); valveItem.m_enumNames = EnumInDInfo.NameList; break;
                        case ENUMValveName.CPV_1:
                        case ENUMValveName.CPV_2: EnumCPVInfo.Init(2); valveItem.m_enumNames = EnumCPVInfo.NameList; break;
                        case ENUMValveName.Out: EnumOutInfo.Init(2); valveItem.m_enumNames = EnumOutInfo.NameList; break;
                    }
                }

                if (null != m_scInfo.MList[2] && m_scInfo.MList[2].MVisible)
                {
                    ValveItem valveItem = (ValveItem)m_scInfo.MList[2];
                    switch ((ENUMValveName)Enum.Parse(typeof(ENUMValveName), m_scInfo.MList[2].MConstName))
                    {
                        case ENUMValveName.InS: EnumInSInfo.Init(2); valveItem.m_enumNames = EnumInSInfo.NameList; break;
                        case ENUMValveName.InA: EnumInAInfo.Init(2); valveItem.m_enumNames = EnumInAInfo.NameList; break;
                        case ENUMValveName.InB: EnumInBInfo.Init(2); valveItem.m_enumNames = EnumInBInfo.NameList; break;
                        case ENUMValveName.InC: EnumInCInfo.Init(2); valveItem.m_enumNames = EnumInCInfo.NameList; break;
                        case ENUMValveName.InD: EnumInDInfo.Init(2); valveItem.m_enumNames = EnumInDInfo.NameList; break;
                        case ENUMValveName.CPV_1:
                        case ENUMValveName.CPV_2: EnumCPVInfo.Init(2); valveItem.m_enumNames = EnumCPVInfo.NameList; break;
                        case ENUMValveName.Out: EnumOutInfo.Init(2); valveItem.m_enumNames = EnumOutInfo.NameList; break;
                    }
                }
            }
        }

        /// <summary>
        /// 线程主函数
        /// </summary>
        protected override void ThreadRun()
        {
            int tempV1 = 1;
            int tempV2 = 1;
            int tempV3 = 1;

            while (true)
            {
                switch (m_state)
                {
                    case EnumValveMixerState.Free:
                        Close();
                        Thread.Sleep(DlyBase.c_sleep10);
                        m_communState = ENUMCommunicationState.Free;
                        break;
                    case EnumValveMixerState.Version:
                        if (Connect())
                        {
                            string tempVersion = null;
                            ReadVersion(ref tempVersion);
                            m_scInfo.MVersion = tempVersion;
                            Close();
                        }
                        m_state = EnumValveMixerState.Free;
                        break;
                    case EnumValveMixerState.First:
                        if (Connect())
                        {
                            ReadStatus(ref m_itemM.m_onoffGet, ref tempV1, ref tempV2, ref tempV3);
                            m_itemV1.MValveGet = tempV1;
                            m_itemV2.MValveGet = tempV2;
                            m_itemV3.MValveGet = tempV3;
                        }
                        m_state = EnumValveMixerState.ReadWrite;
                        break;
                    case EnumValveMixerState.ReadWrite:
                        if (Connect() && SetStatus(m_itemM.m_onoffSet, ref m_itemM.m_onoffGet, m_itemV1.MValveSet, ref tempV1, m_itemV2.MValveSet, ref tempV2, m_itemV3.MValveSet, ref tempV3))
                        {
                            m_communState = ENUMCommunicationState.Success;
                            m_itemV1.MValveGet = tempV1;
                            m_itemV2.MValveGet = tempV2;
                            m_itemV3.MValveGet = tempV3;

                            Thread.Sleep(DlyBase.c_sleep5);
                        }
                        else
                        {
                            Close();

                            for (int i = 0; i < c_timeout; i++)
                            {
                                if (EnumValveMixerState.ReadWrite != m_state)
                                {
                                    break;
                                }
                                else
                                {
                                    m_communState = ENUMCommunicationState.Error;
                                    Thread.Sleep(DlyBase.c_sleep10);
                                }
                            }
                        }
                        break;
                    case EnumValveMixerState.Abort:
                        Close();
                        m_communState = ENUMCommunicationState.Over;
                        return;
                }
            }
        }

        /// <summary>
        /// 线程的状态设置
        /// </summary>
        public override void ThreadStatus(ENUMThreadStatus status)
        {
            switch (status)
            {
                case ENUMThreadStatus.Free:
                    m_state = EnumValveMixerState.Free;
                    break;
                case ENUMThreadStatus.Version:
                    m_state = EnumValveMixerState.Version;
                    break;
                case ENUMThreadStatus.WriteOrRead:
                    m_state = EnumValveMixerState.First;
                    break;
                case ENUMThreadStatus.Abort:
                    m_state = EnumValveMixerState.Abort;
                    break;
            }
        }


        /// <summary>
        /// 读型号
        /// </summary>
        /// <returns></returns>
        public override bool ReadVersion(ref string version)
        {
            try
            {
                m_WriteByte[0] = 0x02;
                m_WriteByte[1] = 0x30;
                m_WriteByte[2] = 0x31;
                m_WriteByte[3] = 0x30;
                m_WriteByte[4] = 0x30;
                m_WriteByte[5] = 0x31;
                m_WriteByte[6] = 0x30;//VALUE
                m_WriteByte[7] = 0x30;
                m_WriteByte[8] = 0x30;
                m_WriteByte[9] = 0x30;
                m_WriteByte[10] = 0x30;
                m_WriteByte[11] = 0x30;
                byte[] mCRC = CRC.Cal12(m_WriteByte);//CRC校验
                m_WriteByte[12] = mCRC[0];
                m_WriteByte[13] = mCRC[1];
                m_WriteByte[14] = mCRC[2];
                m_WriteByte[15] = 0x03;//ETX

                if (!write(16) || !read())
                {
                    return false;
                }

                if (0x44 == m_ReadByte[0] && 0x52 == m_ReadByte[1] && (0x31 <= m_ReadByte[2] && m_ReadByte[2] <= 0x33))
                {
                    version = Encoding.Default.GetString(m_ReadByte, 0, m_ReadLen);
                    return true;
                }
                else if (0x44 == m_ReadByte[0] && 0x30 == m_ReadByte[1] && 0x30 <= m_ReadByte[2])
                {
                    version = Encoding.Default.GetString(m_ReadByte, 0, m_ReadLen);
                    return true;
                } 
            }
            catch
            { }
            
            return false;
        }

        /// <summary>
        /// 读Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override bool ReadModel(ref string model)
        {
            model = ENUMOtherID.ValveMixer.ToString();

            return true;
        }

        public bool SetStatus(bool setMixer, ref bool readMixer, int setV1, ref int readV1, int setV2, ref int readV2, int setV3, ref int readV3)
        {
            try
            {
                if (!ReadStatus(ref readMixer, ref readV1, ref readV2, ref readV3))
                {
                    return false;
                }

                if (setMixer == readMixer && setV1 == readV1 && setV2 == readV2 && setV3 == readV3)
                {
                    return true;
                }

                m_WriteByte[0] = 0x02;
                m_WriteByte[1] = 0x30;
                m_WriteByte[2] = 0x31;
                m_WriteByte[3] = 0x30;
                m_WriteByte[4] = 0x30;
                m_WriteByte[5] = 0x32;
                m_WriteByte[6] = setMixer ? (byte)0x31 : (byte)0x30;
                m_WriteByte[7] = 1 == setV1 ? (byte)0x31 : (byte)0x30;
                m_WriteByte[8] = 1 == setV2 ? (byte)0x31 : (byte)0x30;
                m_WriteByte[9] = 1 == setV3 ? (byte)0x31 : (byte)0x30;
                m_WriteByte[10] = 0x30;
                m_WriteByte[11] = 0x30;
                byte[] mCRC = CRC.Cal12(m_WriteByte);//CRC校验
                m_WriteByte[12] = mCRC[0];
                m_WriteByte[13] = mCRC[1];
                m_WriteByte[14] = mCRC[2];
                m_WriteByte[15] = 0x03;//ETX

                if (!write(16) || !read())
                {
                    return false;
                }

                if (0x23 == m_ReadByte[0])
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private bool ReadStatus(ref bool readMixer, ref int readV1, ref int readV2, ref int readV3)
        {
            try
            {
                m_WriteByte[0] = 0x02;
                m_WriteByte[1] = 0x30;
                m_WriteByte[2] = 0x31;
                m_WriteByte[3] = 0x30;
                m_WriteByte[4] = 0x30;
                m_WriteByte[5] = 0x33;
                m_WriteByte[6] = 0x30;//VALUE
                m_WriteByte[7] = 0x30;
                m_WriteByte[8] = 0x30;
                m_WriteByte[9] = 0x30;
                m_WriteByte[10] = 0x30;
                m_WriteByte[11] = 0x30;
                byte[] mCRC = CRC.Cal12(m_WriteByte);//CRC校验
                m_WriteByte[12] = mCRC[0];
                m_WriteByte[13] = mCRC[1];
                m_WriteByte[14] = mCRC[2];
                m_WriteByte[15] = 0x03;//ETX

                if (!write(16) || !read())
                {
                    return false;
                }

                if (0x24 == m_ReadByte[0])
                {
                    return false;
                }
                else
                {
                    readMixer = 0x31 == m_ReadByte[0] ? true : false;
                    readV1 = 0x31 == m_ReadByte[1] ? 1 : 0;
                    readV2 = 0x31 == m_ReadByte[2] ? 1 : 0;
                    readV3 = 0x31 == m_ReadByte[3] ? 1 : 0;

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
