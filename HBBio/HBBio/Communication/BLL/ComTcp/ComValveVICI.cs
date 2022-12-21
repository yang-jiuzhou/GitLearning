using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class ComValveVICI : ComValve
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ComValveVICI(ComConf info) : base(info)
        {

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

                if (null == m_item)
                {
                    return;
                }

                m_id = (ENUMValveID)Enum.Parse(typeof(ENUMValveID), m_scInfo.MModel);

                switch (m_id)
                {
                    case ENUMValveID.VICI4_413:
                    case ENUMValveID.VICI4_342:
                        EnumBPVInfo.Init(3);
                        m_item.m_enumNames = EnumBPVInfo.NameList;
                        break;
                    case ENUMValveID.VICI6_AB:
                        EnumIJVInfo.Init2();
                        m_item.m_enumNames = EnumIJVInfo.NameList;
                        break;
                    default:
                        if (m_id.ToString().Contains("VICI") && !m_id.ToString().Contains("VICI_"))
                        {
                            int tempCount = Convert.ToInt32(m_id.ToString().Replace("VICI", ""));
                            switch ((ENUMValveName)Enum.Parse(typeof(ENUMValveName), m_scInfo.MList[0].MConstName))
                            {
                                case ENUMValveName.InS: EnumInSInfo.Init(tempCount); m_item.m_enumNames = EnumInSInfo.NameList; break;
                                case ENUMValveName.InA: EnumInAInfo.Init(tempCount); m_item.m_enumNames = EnumInAInfo.NameList; break;
                                case ENUMValveName.InB: EnumInBInfo.Init(tempCount); m_item.m_enumNames = EnumInBInfo.NameList; break;
                                case ENUMValveName.InC: EnumInCInfo.Init(tempCount); m_item.m_enumNames = EnumInCInfo.NameList; break;
                                case ENUMValveName.InD: EnumInDInfo.Init(tempCount); m_item.m_enumNames = EnumInDInfo.NameList; break;
                                case ENUMValveName.IJV: EnumIJVInfo.Init2(); m_item.m_enumNames = EnumIJVInfo.NameList; break;
                                case ENUMValveName.BPV: EnumBPVInfo.Init(3); m_item.m_enumNames = EnumBPVInfo.NameList; break;
                                case ENUMValveName.CPV_1: EnumCPVInfo.Init(tempCount); m_item.m_enumNames = EnumCPVInfo.NameList; break;
                                case ENUMValveName.CPV_2: m_item.m_enumNames = EnumCPVInfo.NameList; break;
                                case ENUMValveName.Out: EnumOutInfo.Init(tempCount); m_item.m_enumNames = EnumOutInfo.NameList; break;
                            }
                        }              
                        break;
                }
            }
        }

        /// <summary>
        /// 线程主函数
        /// </summary>
        protected override void ThreadRun()
        {
            int temp = -1;
            while (true)
            {
                m_are.WaitOne(DlyBase.c_sleep100);

                switch (m_state)
                {
                    case VALVEState.Free:
                        Close();
                        Thread.Sleep(DlyBase.c_sleep10);
                        m_communState = ENUMCommunicationState.Free;
                        break;
                    case VALVEState.Version:
                        if (Connect())
                        {
                            string tempVersion = null;
                            ReadVersion(ref tempVersion);
                            m_scInfo.MVersion = tempVersion;
                            Close();
                        }
                        m_state = VALVEState.Free;
                        m_are.Set();
                        break;
                    case VALVEState.ReadWriteFirst:
                        if (Connect() && ReadValue(ref temp))
                        {
                            m_item.MValveGet = temp;
                        }
                        m_state = VALVEState.ReadWrite;
                        m_are.Set();
                        break;
                    case VALVEState.ReadWrite:
                        if (Connect() && WriteValue(m_item.MValveSet, ref temp))
                        {
                            m_communState = ENUMCommunicationState.Success;
                            m_item.MValveGet = temp;

                            if (m_item.MValveSet != m_item.MValveGet)
                            {
                                m_are.Set();
                            }
                        }
                        else
                        {
                            Close();

                            for (int i = 0; i < c_timeout; i++)
                            {
                                if (VALVEState.ReadWrite != m_state)
                                {
                                    break;
                                }
                                else
                                {
                                    m_communState = ENUMCommunicationState.Error;
                                    Thread.Sleep(DlyBase.c_sleep10);
                                }
                            }
                            m_are.Set();
                        }
                        break;
                    case VALVEState.Abort:
                        Close();
                        m_communState = ENUMCommunicationState.Over;
                        return;
                }
            }
        }


        /// <summary>
        /// 读型号
        /// </summary>
        /// <returns></returns>
        public override bool ReadVersion(ref string version)
        {
            bool result = false;

            try
            {
                m_WriteByte = Encoding.ASCII.GetBytes("VR\r\n");

                if (!write(m_WriteByte.Length) || !read())
                {
                    return false;
                }

                version = Encoding.Default.GetString(m_ReadByte, 0, m_ReadLen);
                if (version.Contains("C5x"))
                {
                    result = true;
                }
            }
            catch
            { }

            return result;
        }

        /// <summary>
        /// 读序列
        /// </summary>
        /// <returns></returns>
        public override bool ReadSerial(ref string serial)
        {
            serial = "";

            return true;
        }

        /// <summary>
        /// 读Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override bool ReadModel(ref string model)
        {
            ENUMValveID id = ENUMValveID.VICI4;
            if (ReadModel(ref id))
            {
                model = id.ToString();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 读设备识别码
        /// </summary>
        /// <returns></returns>
        public bool ReadModel(ref ENUMValveID id)
        {
            bool result = false;

            try
            {
                m_WriteByte = Encoding.ASCII.GetBytes("RC\r\n");

                if (!write(m_WriteByte.Length) || !read())
                {
                    return false;
                }

                string readStr = Encoding.Default.GetString(m_ReadByte);
                if (!string.IsNullOrEmpty(readStr))//接收读命令返回成功
                {
                    if (readStr.Contains("A/B"))//字母
                    {
                        id = ENUMValveID.VICI6_AB;
                        result = true;
                    }
                    else//数字
                    {
                        if (readStr.Contains("Two-stage"))
                        {
                            if (readStr.Contains("C52-3344IA"))
                            {
                                id = ENUMValveID.VICI4_413;
                            }
                            else
                            {
                                id = ENUMValveID.VICI8;
                            }
                            result = true;
                        }
                        else if (readStr.Contains("Single-stage"))
                        {
                            if (readStr.Contains("C62-6154IA"))
                            {
                                id = ENUMValveID.VICI4_342;
                            }
                            else
                            {
                                string str1 = "Single-stage, ";
                                string str2 = "-port";
                                int index1 = readStr.IndexOf(str1);
                                int index2 = readStr.IndexOf(str2);
                                int idNum = Convert.ToInt32(readStr.Substring(index1 + str1.Length, index2 - index1 - str1.Length));
                                switch (idNum)
                                {
                                    case 4: id = ENUMValveID.VICI4; break;
                                    case 6: id = ENUMValveID.VICI6; break;
                                    case 8: id = ENUMValveID.VICI8; break;
                                    case 10: id = ENUMValveID.VICI10; break;
                                }
                            }
                            result = true;
                        }
                    }              
                }
            }
            catch
            { }

            return result;
        }

        /// <summary>
        /// 读阀
        /// </summary>
        /// <returns></returns>
        private bool ReadValue(ref int valve)
        {
            bool result = false;                   //返回读取到的型号   

            try
            {
                m_WriteByte = Encoding.ASCII.GetBytes("CP\r\n");

                if (!write(m_WriteByte.Length) || !read())
                {
                    return false;
                }

                if (0 == m_ReadByte[0] && 0 == m_ReadByte[1])
                {
                    if (!write(m_WriteByte.Length) || !read())
                    {
                        return false;
                    }
                }

                if (0 != m_ReadByte[0])
                {
                    //解析
                    if (m_ReadByte[0] > 0x40)//字母
                    {
                        valve = m_ReadByte[0] - 0x40 - 1;
                    }
                    else if(m_ReadByte[0] > 0x30)//数字
                    {
                        switch (m_id)
                        {
                            case ENUMValveID.VICI4_413:
                                switch (m_ReadByte[0])
                                {
                                    case 0x34: valve = 0; break;
                                    case 0x31: valve = 1; break;
                                    case 0x33: valve = 2; break;
                                }
                                break;
                            case ENUMValveID.VICI4_342:
                                switch (m_ReadByte[0])
                                {
                                    case 0x33: valve = 0; break;
                                    case 0x34: valve = 1; break;
                                    case 0x32: valve = 2; break;
                                }
                                break;
                            default:
                                valve = m_ReadByte[0] - 0x30 - 1;
                                break;
                        }
                    }
                    else if (m_ReadByte[0] < 0x10)//数字
                    {
                        valve = m_ReadByte[0] - 1;
                    }

                    result = true;
                }
                else if (0 != m_ReadByte[1])
                {
                    if (m_ReadByte[1] < 0x10)//数字
                    {
                        valve = m_ReadByte[1] - 1;
                    }

                    result = true;
                }
            }
            catch
            { }

            return result;
        }

        /// <summary>
        /// 写阀
        /// </summary>
        /// <returns></returns>
        private bool WriteValue(int valveIn, ref int valveOut)
        {
            try
            {
                if (valveIn != valveOut)
                {
                    string sendInfo = null;
                    switch (m_id)
                    {
                        case ENUMValveID.VICI4_413:
                            switch (valveIn)
                            {
                                case 0: sendInfo = "GO4"; break;
                                case 1: sendInfo = "GO1"; break;
                                case 2: sendInfo = "GO3"; break;
                            }
                            break;
                        case ENUMValveID.VICI4_342:
                            switch (valveIn)
                            {
                                case 0: sendInfo = "GO3"; break;
                                case 1: sendInfo = "GO4"; break;
                                case 2: sendInfo = "GO2"; break;
                            }
                            break;
                        case ENUMValveID.VICI6_AB:
                            switch (valveIn)
                            {
                                case 0: sendInfo = "GOA"; break;
                                case 1: sendInfo = "GOB"; break;
                            }
                            break;
                        default:
                            sendInfo = "GO" + (valveIn + 1);
                            break;
                    }

                    m_WriteByte = Encoding.ASCII.GetBytes(sendInfo + "\r\n");
                    if (write(m_WriteByte.Length))
                    {
                        return ReadValue(ref valveOut);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return ReadValue(ref valveOut);
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
