using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class ComValveIMI : ComValve
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ComValveIMI(ComConf info) : base(info)
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
                    case ENUMValveID.IMI_IJV:
                        EnumIJVInfo.Init4();
                        m_item.m_enumNames = EnumIJVInfo.NameList;
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
            m_WriteByte[0] = 0x02;
            m_WriteByte[1] = 0x30;
            m_WriteByte[2] = 0x32;
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

            if (0x30 == m_ReadByte[0] || 0x32 == m_ReadByte[0])
            {
                version = Encoding.Default.GetString(m_ReadByte);
            }

            return true;
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
            ENUMValveID id = ENUMValveID.IMI_IJV;
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
                m_WriteByte[0] = 0x02;
                m_WriteByte[1] = 0x30;
                m_WriteByte[2] = 0x32;
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

                switch (m_ReadByte[0])
                {
                    case 0x35:
                        valve = 0;
                        return true;
                    case 0x34:
                        valve = 1;
                        return true;
                    case 0x36:
                        valve = 2;
                        return true;
                    case 0x33:
                        valve = 3;
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

                m_WriteByte[0] = 0x02;
                m_WriteByte[1] = 0x30;
                m_WriteByte[2] = 0x32;
                m_WriteByte[3] = 0x30;
                m_WriteByte[4] = 0x30;
                m_WriteByte[5] = 0x32;
                m_WriteByte[6] = 0x30;
                m_WriteByte[7] = 0x30;
                m_WriteByte[8] = 0x30;
                m_WriteByte[9] = 0x30;
                m_WriteByte[10] = 0x30;
                switch (valveIn)
                {
                    case 0:
                        m_WriteByte[11] = 0x35;
                        break;
                    case 1:
                        m_WriteByte[11] = 0x34;
                        break;
                    case 2:
                        m_WriteByte[11] = 0x36;
                        break;
                    default:
                        m_WriteByte[11] = 0x33;
                        break;
                }
                byte[] mCRC = CRC.Cal12(m_WriteByte);//CRC校验
                m_WriteByte[12] = mCRC[0];
                m_WriteByte[13] = mCRC[1];
                m_WriteByte[14] = mCRC[2];
                m_WriteByte[15] = 0x03;//ETX

                if (!write(16) || !read())
                {
                    return false;
                }
                System.Threading.Thread.Sleep(DlyBase.c_sleep10);
                if (0x06 == m_ReadByte[0])
                {
                    ReadValue(ref valveOut);

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
    }
}
