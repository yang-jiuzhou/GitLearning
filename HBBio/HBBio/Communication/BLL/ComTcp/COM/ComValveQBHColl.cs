using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class ComValveQBHColl : ComValve
    {
        private bool m_changeBeginEnd = false;


        /// <summary>
        /// 构造函数
        /// </summary>
        public ComValveQBHColl(ComConf info) : base(info)
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
                    case ENUMValveID.QBH_Coll6:
                    case ENUMValveID.QBH_Coll8:
                    case ENUMValveID.QBH_Coll12:
                        if (m_id.ToString().Contains("QBH_Coll"))
                        {
                            int tempCount = Convert.ToInt32(m_id.ToString().Replace("QBH_Coll", ""));
                            switch ((ENUMValveName)Enum.Parse(typeof(ENUMValveName), m_scInfo.MList[0].MConstName))
                            {
                                case ENUMValveName.Out: EnumOutInfo.Init(tempCount); m_item.m_enumNames = EnumOutInfo.NameList; m_changeBeginEnd = true; break;
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
        /// 写阀
        /// </summary>
        /// <returns></returns>
        private bool WriteValue(int valveIn, ref int valveOut)
        {
            try
            {
                if (valveIn != valveOut)
                {
                    if (m_changeBeginEnd)
                    {
                        if (0 == valveIn)
                        {
                            valveIn = m_item.m_enumNames.Length - 1;
                        }
                        else
                        {
                            valveIn -= 1;
                        }
                    }
                    WriteStop();
                    WriteIndex(valveIn + 1);
                    if (m_item.m_enumNames.Length - 1 != valveIn)
                    {
                        WriteStart();
                    }
                    return ReadValue(ref valveOut);
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
        /// 读阀
        /// </summary>
        /// <returns></returns>
        private bool ReadValue(ref int valve)
        {
            int index = 0;
            bool status = false;
            if (ReadStatus(ref index, ref status))
            {
                if (status)
                {
                    valve = index - 1;
                }
                else
                {
                    valve = m_item.m_enumNames.Length - 1;
                }

                if (m_changeBeginEnd)
                {
                    if (m_item.m_enumNames.Length - 1 == valve)
                    {
                        valve = 0;
                    }
                    else
                    {
                        valve += 1;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 读版本
        /// </summary>
        /// <returns></returns>
        public override bool ReadVersion(ref string version)
        {
            bool result = false;

            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0x39;
                m_WriteByte[2] = 0x37;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x30;//PFC，
                m_WriteByte[5] = 0x36;
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
				
                if (m_WriteByte[4] == m_ReadByte[4] && m_WriteByte[5] == m_ReadByte[5])
                {
                    version = Encoding.ASCII.GetString(m_ReadByte, 6, 6);
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
            ENUMValveID id = ENUMValveID.QBH_Coll6;
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
        private bool ReadSerialH(ref string serial)
        {
            bool result = false;

            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0x39;
                m_WriteByte[2] = 0x37;
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
				
                if (m_WriteByte[4] == m_ReadByte[4] && m_WriteByte[5] == m_ReadByte[5])
                {
                    serial = Encoding.ASCII.GetString(m_ReadByte, 6, 6);
                    result = true;
                }
            }
            catch
            { }

            return result;
        }

        /// <summary>
        /// 读低位序列号
        /// </summary>
        /// <returns></returns>
        private bool ReadSerialL(ref string serial)
        {
            bool result = false;

            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0x39;
                m_WriteByte[2] = 0x37;
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

                if (m_WriteByte[4] == m_ReadByte[4] && m_WriteByte[5] == m_ReadByte[5])
                {
                    serial = Encoding.ASCII.GetString(m_ReadByte, 6, 6);
                    result = true;
                }
            }
            catch
            { }

            return result;
        }

        /// <summary>
        /// 读设备识别码
        /// </summary>
        /// <returns></returns>
        private bool ReadID(ref ENUMValveID id)
        {
            bool result = false;

            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0x39;
                m_WriteByte[2] = 0x37;
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

                if (m_WriteByte[1] == m_ReadByte[1] && m_WriteByte[2] == m_ReadByte[2])
                {
                    int temp = (m_ReadByte[1] - 0x30) * 10 + (m_ReadByte[2] - 0x30);
                    if (97 == temp)
                    {
                        id = ENUMValveID.QBH_Coll6;
                        result = true;
                    }
                }
            }
            catch
            { }

            return result;
        }

        /// <summary>
        /// 读取收集器运行状态
        /// </summary>
        /// <param name="on"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool ReadStatus(ref int index, ref bool on)
        {
            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0X39;
                m_WriteByte[2] = 0X37;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x30;//PFC
                m_WriteByte[5] = 0x34;
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

                on = 0x30 == m_ReadByte[6] ? false : true;
                index = Convert.ToInt32(Encoding.ASCII.GetString(m_ReadByte, 7, 5));

                return true;
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 设定收集器收集瓶号
        /// </summary>
        /// <param name="val"></param>
        public bool WriteIndex(int index)
        {
            try
            {
                if (0 == index)
                {
                    return true;
                }

                //发送命令
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0X39;
                m_WriteByte[2] = 0X37;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x31;
                m_WriteByte[5] = 0x30;
                IntToByte(ref m_WriteByte, index);//VALUE
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

        /// <summary>
        /// 上一个收集瓶并开始收集
        /// </summary>
        /// <returns></returns>
        protected bool WriteFront()
        {
            try
            {
                //发送命令
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0X39;
                m_WriteByte[2] = 0X37;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x31;
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

                return 0x23 == m_ReadByte[0];
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 下一个收集瓶并开始收集
        /// </summary>
        /// <returns></returns>
        protected bool WriteBack()
        {
            try
            {
                //发送命令
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0X39;
                m_WriteByte[2] = 0X37;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x31;
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

                return 0x23 == m_ReadByte[0];
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 开始收集
        /// </summary>
        /// <returns></returns>
        protected bool WriteStart()
        {
            try
            {
                //发送命令
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0X39;
                m_WriteByte[2] = 0X37;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x31;
                m_WriteByte[5] = 0x35;
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

                return 0x23 == m_ReadByte[0];
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 停止收集
        /// </summary>
        /// <returns></returns>
        protected bool WriteStop()
        {
            try
            {
                //发送命令
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0X39;
                m_WriteByte[2] = 0X37;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x31;
                m_WriteByte[5] = 0x36;
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

                return 0x23 == m_ReadByte[0];
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 设定收集器收集模式
        /// </summary>
        public bool WriteStyle(int x, int y)
        {
            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0X39;
                m_WriteByte[2] = 0X37;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x31;
                m_WriteByte[5] = 0x33;
                m_WriteByte[6] = 0x30;//VALUE
                m_WriteByte[7] = 0x30;
                m_WriteByte[8] = 0x30;
                m_WriteByte[9] = (byte)(48 + x);
                m_WriteByte[10] = 0x30;
                m_WriteByte[11] = (byte)(48 + y);
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

        /// <summary>
        /// int数值转换为6字节
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private void IntToByte(ref byte[] byteArr, int mValueFrom)
        {
            byteArr[6] = Convert.ToByte(48 + mValueFrom / 100000);
            mValueFrom %= 100000;
            byteArr[7] = Convert.ToByte(48 + mValueFrom / 10000);
            mValueFrom %= 10000;
            byteArr[8] = Convert.ToByte(48 + mValueFrom / 1000);
            mValueFrom %= 1000;
            byteArr[9] = Convert.ToByte(48 + mValueFrom / 100);
            mValueFrom %= 100;
            byteArr[10] = Convert.ToByte(48 + mValueFrom / 10);
            mValueFrom %= 10;
            byteArr[11] = Convert.ToByte(48 + mValueFrom);
        }
    }
}
