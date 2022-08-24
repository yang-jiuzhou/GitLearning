using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class TCPValveHBGS4 : TCPValve
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TCPValveHBGS4(ComConf info) : base(info)
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
                    case ENUMValveID.HB_GS4:
                        {
                            int tempCount = Convert.ToInt32(m_id.ToString().Replace("HB_GS", "")) + 1;
                            switch ((ENUMValveName)Enum.Parse(typeof(ENUMValveName), m_scInfo.MList[0].MConstName))
                            {
                                case ENUMValveName.InS: EnumInSInfo.Init(tempCount, true); m_item.m_enumNames = EnumInSInfo.NameList; break;
                                case ENUMValveName.InA: EnumInAInfo.Init(tempCount, true); m_item.m_enumNames = EnumInAInfo.NameList; break;
                                case ENUMValveName.InB: EnumInBInfo.Init(tempCount, true); m_item.m_enumNames = EnumInBInfo.NameList; break;
                                case ENUMValveName.InC: EnumInCInfo.Init(tempCount, true); m_item.m_enumNames = EnumInCInfo.NameList; break;
                                case ENUMValveName.InD: EnumInDInfo.Init(tempCount, true); m_item.m_enumNames = EnumInDInfo.NameList; break;
                                case ENUMValveName.Out: EnumOutInfo.Init(tempCount, true); m_item.m_enumNames = EnumOutInfo.NameList; break;
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

                if (0x30 == m_ReadByte[0] && 0x52 == m_ReadByte[1] && 0x34 == m_ReadByte[2])
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
            model = ENUMValveID.HB_GS4.ToString();

            return true;
        }

        private bool ReadValue(ref int valve)
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
                    if (0x31 == m_ReadByte[0])
                    {
                        valve = 0;
                    }
                    else if (0x31 == m_ReadByte[1])
                    {
                        valve = 1;
                    }
                    else if (0x31 == m_ReadByte[2])
                    {
                        valve = 2;
                    }
                    else if (0x31 == m_ReadByte[3])
                    {
                        valve = 3;
                    }
                    else
                    {
                        valve = 4;
                    }

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private bool WriteValue(int valveIn, ref int valveOut)
        {
            try
            {
                if (valveIn != valveOut)
                {
                    m_WriteByte[0] = 0x02;
                    m_WriteByte[1] = 0x30;
                    m_WriteByte[2] = 0x31;
                    m_WriteByte[3] = 0x30;
                    m_WriteByte[4] = 0x30;
                    m_WriteByte[5] = 0x32;
                    m_WriteByte[6] = 0 == valveIn ? (byte)0x31 : (byte)0x30;
                    m_WriteByte[7] = 1 == valveIn ? (byte)0x31 : (byte)0x30;
                    m_WriteByte[8] = 2 == valveIn ? (byte)0x31 : (byte)0x30;
                    m_WriteByte[9] = 3 == valveIn ? (byte)0x31 : (byte)0x30;
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
