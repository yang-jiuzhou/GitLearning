using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class ComValveHB2 : ComValve
    {
        public ComValveHB2(ComConf info) : base(info)
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
                    case ENUMValveID.HB_T2:
                    case ENUMValveID.HB_T4:
                        if (m_id.ToString().Contains("HB_T"))
                        {
                            int tempCount = Convert.ToInt32(m_id.ToString().Replace("HB_T", ""));
                            switch ((ENUMValveName)Enum.Parse(typeof(ENUMValveName), m_scInfo.MList[0].MConstName))
                            {
                                case ENUMValveName.InS: EnumInSInfo.Init(tempCount); m_item.m_enumNames = EnumInSInfo.NameList; break;
                                case ENUMValveName.InA: EnumInAInfo.Init(tempCount); m_item.m_enumNames = EnumInAInfo.NameList; break;
                                case ENUMValveName.InB: EnumInBInfo.Init(tempCount); m_item.m_enumNames = EnumInBInfo.NameList; break;
                                case ENUMValveName.InC: EnumInCInfo.Init(tempCount); m_item.m_enumNames = EnumInCInfo.NameList; break;
                                case ENUMValveName.InD: EnumInDInfo.Init(tempCount); m_item.m_enumNames = EnumInDInfo.NameList; break;
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
            return true;
        }

        /// <summary>
        /// 读序列
        /// </summary>
        /// <returns></returns>
        public override bool ReadSerial(ref string serial)
        {
            string serialH = "";
            string serialL = "";
            if (ReadSerialH(ref serialH) && ReadSerialL(ref serialL))
            {
                serial = serialH + serialL;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 读Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override bool ReadModel(ref string model)
        {
            ENUMValveID id = ENUMValveID.HB_T2;
            if (ReadID(ref id))
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
        /// 读高位序列号
        /// </summary>
        /// <returns></returns>
        public bool ReadSerialH(ref string serial)
        {
            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0x37;
                m_WriteByte[2] = 0x30;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x30;//PFC
                m_WriteByte[5] = 0x32;
                m_WriteByte[6] = 0x20;//VALUE
                m_WriteByte[7] = 0x20;
                m_WriteByte[8] = 0x20;
                m_WriteByte[9] = 0x20;
                m_WriteByte[10] = 0x20;
                m_WriteByte[11] = 0x20;
                byte[] mCRC = CRC.Cal12(m_WriteByte);//CRC校验
                m_WriteByte[12] = mCRC[0];
                m_WriteByte[13] = mCRC[1];
                m_WriteByte[14] = mCRC[2];
                m_WriteByte[15] = 0x0A;//ETX

                if (!write(16) || !read())
                {
                    return false;
                }

                if (m_WriteByte[1] == m_ReadByte[1] && m_WriteByte[2] == m_ReadByte[2] && m_WriteByte[4] == m_ReadByte[4] && m_WriteByte[5] == m_ReadByte[5])
                {
                    serial = Encoding.ASCII.GetString(m_ReadByte, 6, 6);
                    return true;
                }
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 读低位序列号
        /// </summary>
        /// <returns></returns>
        public bool ReadSerialL(ref string serial)
        {
            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0x37;
                m_WriteByte[2] = 0x30;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x30;//PFC
                m_WriteByte[5] = 0x33;
                m_WriteByte[6] = 0x20;//VALUE
                m_WriteByte[7] = 0x20;
                m_WriteByte[8] = 0x20;
                m_WriteByte[9] = 0x20;
                m_WriteByte[10] = 0x20;
                m_WriteByte[11] = 0x20;
                byte[] mCRC = CRC.Cal12(m_WriteByte);//CRC校验
                m_WriteByte[12] = mCRC[0];
                m_WriteByte[13] = mCRC[1];
                m_WriteByte[14] = mCRC[2];
                m_WriteByte[15] = 0x0A;//ETX

                if (!write(16) || !read())
                {
                    return false;
                }

                if (m_WriteByte[1] == m_ReadByte[1] && m_WriteByte[2] == m_ReadByte[2] && m_WriteByte[4] == m_ReadByte[4] && m_WriteByte[5] == m_ReadByte[5])
                {
                    serial = Encoding.ASCII.GetString(m_ReadByte, 6, 6);
                    return true;
                }
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 读设备识别码
        /// </summary>
        /// <returns></returns>
        public bool ReadID(ref ENUMValveID id)
        {
            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0x30;
                m_WriteByte[2] = 0x30;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x30;//PFC，读产品ID，01
                m_WriteByte[5] = 0x31;
                m_WriteByte[6] = 0x20;//VALUE
                m_WriteByte[7] = 0x20;
                m_WriteByte[8] = 0x20;
                m_WriteByte[9] = 0x20;
                m_WriteByte[10] = 0x20;
                m_WriteByte[11] = 0x20;
                byte[] mCRC = CRC.Cal12(m_WriteByte);//CRC校验
                m_WriteByte[12] = mCRC[0];
                m_WriteByte[13] = mCRC[1];
                m_WriteByte[14] = mCRC[2];
				m_WriteByte[15] = 0x0A;//ETX

                if (!write(16) || !read())
                {
                    return false;
                }

                if (0x37 == m_ReadByte[10] && 0x30 == m_ReadByte[11])
                {
                    id = ENUMValveID.HB_T2;

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
        private bool ReadValue(ref int valve)
        {
            return ReadDO(ref valve);
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
                    WriteDO(valveIn);
                }

                return ReadValue(ref valveOut);
            }
            catch
            {
                return false;
            }
        }

        private bool ReadDO(ref int valve)
        {
            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0x37;
                m_WriteByte[2] = 0x30;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x31;//PFC
                m_WriteByte[5] = 0x31;
                m_WriteByte[6] = 0x20;//VALUE
                m_WriteByte[7] = 0x20;
                m_WriteByte[8] = 0x20;
                m_WriteByte[9] = 0x20;
                m_WriteByte[10] = 0x20;
                m_WriteByte[11] = 0x20;
                byte[] mCRC = CRC.Cal12(m_WriteByte);//CRC校验
                m_WriteByte[12] = mCRC[0];
                m_WriteByte[13] = mCRC[1];
                m_WriteByte[14] = mCRC[2];
                m_WriteByte[15] = 0x0A;//ETX

                if (!write(16) || !read())
                {
                    return false;
                }

                if (0x31 == m_ReadByte[11])
                {
                    valve = 0;
                }
                else if (0x31 == m_ReadByte[10])
                {
                    valve = 1;
                }

                return true;
            }
            catch
            { }

            return false;
        }

        private bool WriteDO(int valve)
        {
            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0x37;
                m_WriteByte[2] = 0x30;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x31;//PFC
                m_WriteByte[5] = 0x30;
                m_WriteByte[6] = 5 == valve ? (byte)0x31 : (byte)0x30;//VALUE
                m_WriteByte[7] = 4 == valve ? (byte)0x31 : (byte)0x30;
                m_WriteByte[8] = 3 == valve ? (byte)0x31 : (byte)0x30;
                m_WriteByte[9] = 2 == valve ? (byte)0x31 : (byte)0x30;
                m_WriteByte[10] = 1 == valve ? (byte)0x31 : (byte)0x30;
                m_WriteByte[11] = 0 == valve ? (byte)0x31 : (byte)0x30;
                byte[] mCRC = CRC.Cal12(m_WriteByte);//CRC校验
                m_WriteByte[12] = mCRC[0];
                m_WriteByte[13] = mCRC[1];
                m_WriteByte[14] = mCRC[2];
                m_WriteByte[15] = 0x0A;//ETX

                if (!write(16) || !read())
                {
                    return false;
                }

                return 0x23 == m_ReadByte[0];
            }
            catch
            { }

            return false;
        }
    }
}
