using HBBio.Share;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class ComValveVICI2 : ComValve
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ComValveVICI2(ComConf info) : base(info)
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

                int tempCount = Convert.ToInt32(m_id.ToString().Replace("VICI_T", ""));
                switch ((ENUMValveName)Enum.Parse(typeof(ENUMValveName), m_scInfo.MList[0].MConstName))
                {
                    case ENUMValveName.InS: EnumInSInfo.Init(tempCount); m_item.m_enumNames = EnumInSInfo.NameList; break;
                    case ENUMValveName.InA: EnumInAInfo.Init(tempCount); m_item.m_enumNames = EnumInAInfo.NameList; break;
                    case ENUMValveName.InB: EnumInBInfo.Init(tempCount); m_item.m_enumNames = EnumInBInfo.NameList; break;
                    case ENUMValveName.InC: EnumInCInfo.Init(tempCount); m_item.m_enumNames = EnumInCInfo.NameList; break;
                    case ENUMValveName.InD: EnumInDInfo.Init(tempCount); m_item.m_enumNames = EnumInDInfo.NameList; break;
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
        /// 重写
        /// </summary>
        /// <returns></returns>
        public override bool Connect()
        {
            try
            {
                if (!m_serialPort.IsOpen && SerialPort.GetPortNames().Contains(m_serialPort.PortName))
                {
                    m_serialPort.Open();
                    m_serialPort.DiscardOutBuffer();
                    m_serialPort.DiscardInBuffer();

                    int temp = 0;
                    if (ReadModes(ref temp) && 3 != temp)
                    {
                        WriteValueModes(3);
                    }
                }

				return m_serialPort.IsOpen;
            }
            catch
            { }

            return false;
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
                if (version.Contains("UA_MAIN_EQ"))
                {
                    version = "UA_MAIN_EQ";
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
                    if (readStr.Contains("E2 RC Invalid"))//字母
                    {
                        id = ENUMValveID.VICI_T6;
                        result = true;
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
            try
            {
                m_WriteByte = Encoding.ASCII.GetBytes("CP\r\n");

                if (!write(m_WriteByte.Length) || !read())
                {
                    return false;
                }

                if (m_WriteByte[0] == m_ReadByte[0] && m_WriteByte[1] == m_ReadByte[1])
                {
                    valve = m_ReadByte[3] - 0x30 - 1;
                    return true;
                }
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 读阀
        /// </summary>
        /// <returns></returns>
        private bool ReadModes(ref int modes)
        {
            m_WriteByte = Encoding.ASCII.GetBytes("AM\r");

            if (!write(m_WriteByte.Length) || !read())
            {
                return false;
            }

            modes = m_ReadByte[2] - 0x30;

            return true;
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
                    m_WriteByte = Encoding.ASCII.GetBytes("GO" + (valveIn + 1) + "\r\n");
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

        /// <summary>
        /// 写阀
        /// </summary>
        /// <returns></returns>
        private bool WriteValueModes(int modes)
        {
            m_WriteByte = Encoding.ASCII.GetBytes("AM" + modes.ToString() + "\r");

            if (!write(m_WriteByte.Length))
            {
                return false;
            }

            return true;
        }
    }
}
