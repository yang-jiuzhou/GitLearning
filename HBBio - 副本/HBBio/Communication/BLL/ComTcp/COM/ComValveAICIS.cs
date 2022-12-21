using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class ComValveAICIS : ComValve
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ComValveAICIS(ComConf info) : base(info)
        {
            m_serialPort.BaudRate = 19200;
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
                    case ENUMValveID.AICIS_231:
                        EnumBPVInfo.Init(3);
                        m_item.m_enumNames = EnumBPVInfo.NameList;
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
                            if (-1 != temp)
                            {
                                m_item.MValveGet = temp;
                            }
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
            m_WriteByte[0] = 0x01;//地址
            m_WriteByte[1] = 0x03;//命令
            m_WriteByte[2] = 0x03;//功能码
            byte[] crc = CRC.CRCLen(m_WriteByte, 3);
            m_WriteByte[3] = crc[0];//CRC校验
            m_WriteByte[4] = crc[1];

            if (!write(5) || !read())
            {
                return false;
            }

            if (6 == m_ReadLen && 0x01 == m_ReadByte[0] && 0x03 == m_ReadByte[1] && 0x03 == m_ReadByte[2])
            {
                version = Encoding.Default.GetString(m_ReadByte, 3, m_ReadLen);
                return true;
            }

            return false;
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
            ENUMValveID id = ENUMValveID.AICIS_231;
            string version = null;
            if (ReadVersion(ref version))
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
        /// 读阀
        /// </summary>
        /// <returns></returns>
        private bool ReadValue(ref int valve)
        {
            try
            {
                m_WriteByte[0] = 0x01;//地址
                m_WriteByte[1] = 0x03;//命令
                m_WriteByte[2] = 0x00;//功能码
                byte[] crc = CRC.CRCLen(m_WriteByte, 3);
                m_WriteByte[3] = crc[0];//CRC校验
                m_WriteByte[4] = crc[1];

                if (!write(5) || !read())
                {
                    return false;
                }

                if (0x01 == m_ReadByte[0] && 0x03 == m_ReadByte[1] && 0x00 == m_ReadByte[2])
                {
                    if (0x40 == m_ReadByte[3])
                    {
                        switch (m_ReadByte[4])
                        {
                            case 0x05:
                                valve = 0;
                                return true;
                            case 0x07:
                                valve = 1;
                                return true;
                            case 0x04:
                                valve = 2;
                                return true;
                            case 0xFF:
                                valve = -1;
                                return true;
                        }
                    }

                    ReadValue(ref valve);

                    return true;
                }

                return false;
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
        private bool WriteValue(int valveIn, ref int valveOut)
        {
            try
            {
                if (valveIn == valveOut)
                {
                    if (!ReadValue(ref valveOut))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

                m_WriteByte[0] = 0x01;//地址
                m_WriteByte[1] = 0x06;//命令
                m_WriteByte[2] = 0x00;//功能码
                switch (valveIn)
                {
                    case 0:
                        m_WriteByte[3] = 0x05;
                        break;
                    case 1:
                        m_WriteByte[3] = 0x07;
                        break;
                    case 2:
                        m_WriteByte[3] = 0x04;
                        break;
                }
                byte[] crc = CRC.CRCLen(m_WriteByte, 4);
                m_WriteByte[4] = crc[0];//CRC校验
                m_WriteByte[5] = crc[1];

                if (!write(6) || !read())
                {
                    return false;
                }

                return ReadValue(ref valveOut);
            }
            catch
            {
                return false;
            }
        }
    }
}
