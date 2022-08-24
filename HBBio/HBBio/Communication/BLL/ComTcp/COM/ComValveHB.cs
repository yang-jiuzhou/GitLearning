using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class ComValveHB : ComValve
    {
        private int m_index = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ComValveHB(ComConf info) : base(info)
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
                    case ENUMValveID.HB2:
                        int tempCount = Convert.ToInt32(m_id.ToString().Replace("HB", ""));
                        switch ((ENUMValveName)Enum.Parse(typeof(ENUMValveName), m_scInfo.MList[0].MConstName))
                        {
                            case ENUMValveName.InS: EnumInSInfo.Init(tempCount); m_item.m_enumNames = EnumInSInfo.NameList; break;
                            case ENUMValveName.InA: EnumInAInfo.Init(tempCount); m_item.m_enumNames = EnumInAInfo.NameList; break;
                            case ENUMValveName.InB: EnumInBInfo.Init(tempCount); m_item.m_enumNames = EnumInBInfo.NameList; break;
                            case ENUMValveName.InC: EnumInCInfo.Init(tempCount); m_item.m_enumNames = EnumInCInfo.NameList; break;
                            case ENUMValveName.InD: EnumInDInfo.Init(tempCount); m_item.m_enumNames = EnumInDInfo.NameList; break;
                            case ENUMValveName.CPV_1:
                            case ENUMValveName.CPV_2: EnumCPVInfo.Init(tempCount); m_item.m_enumNames = EnumCPVInfo.NameList; break;
                            case ENUMValveName.Out: EnumOutInfo.Init(tempCount); m_item.m_enumNames = EnumOutInfo.NameList; break;
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
                        if (Connect())
                        {
                            FindValue(m_item.MValveSet, ref temp);
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
            version = "null";
            return true;
        }

        /// <summary>
        /// 读序列
        /// </summary>
        /// <returns></returns>
        public override bool ReadSerial(ref string serial)
        {
            serial = "null";
            return true;
        }

        /// <summary>
        /// 读Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override bool ReadModel(ref string model)
        {
            bool result = false;

            try
            {
                m_WriteByte[0] = 0x02;
                m_WriteByte[1] = 0x36;
                m_WriteByte[2] = 0x31;
                m_WriteByte[3] = 0x30;
                m_WriteByte[4] = 0x30;
                m_WriteByte[5] = 0x31;
                m_WriteByte[6] = 0x30;
                m_WriteByte[7] = 0x30;
                m_WriteByte[8] = 0x30;
                m_WriteByte[9] = 0x30;
                m_WriteByte[10] = 0x30;
                m_WriteByte[11] = 0x31;
                m_WriteByte[12] = 0x30;
                m_WriteByte[13] = 0x32;
                m_WriteByte[14] = 0x37;
                m_WriteByte[15] = 0x03;

                if (!write(16) || !read())
                {
                    return false;
                }

                if (0x02 == m_ReadByte[0] && 0x36 == m_ReadByte[1] && 0x31 == m_ReadByte[2])
                {
                    //model = Encoding.Default.GetString(m_ReadByte, 6, m_ReadLen);
                    model = ENUMValveID.HB2.ToString();
                    result = true;
                }
            }
            catch
            { }

            return result;
        }

        private void FindValue(int valveIn, ref int valveOut)
        {
            if (SetDigit(1, valveIn, ref valveOut))
            {
                m_index = 0;
                return;
            }
            else if (SetDigit(3, valveIn, ref valveOut))
            {
                m_index = 2;
                return;
            }
            else if (SetDigit(5, valveIn, ref valveOut))
            {
                m_index = 4;
                return;
            }
            else if (SetDigit(7, valveIn, ref valveOut))
            {
                m_index = 6;
                return;
            }
        }

        private bool WriteValue(int valveIn, ref int valveOut)
        {
            return SetDigit(m_index + (0 == valveIn ? 1 : 0), valveIn, ref valveOut);
        }

        /// <summary>
        /// 设定数字量输出状态
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool SetDigit(int index, int valveIn, ref int valveOut)
        {
            m_WriteByte[0] = 0x02;
            m_WriteByte[1] = 0x36;
            m_WriteByte[2] = 0x31;
            m_WriteByte[3] = 0x30;//AI，
            m_WriteByte[4] = 0x30;//PFC
            m_WriteByte[5] = 0x32;
            m_WriteByte[6] = 0x30;//VALUE
            m_WriteByte[7] = 0x30;
            m_WriteByte[8] = 0x30;
            m_WriteByte[9] = 0x30;
            m_WriteByte[10] = 0x30;
            m_WriteByte[11] = (byte)(0x30 + index);
            byte[] mCRC = CRC.Cal12(m_WriteByte);//CRC校验
            m_WriteByte[12] = mCRC[0];
            m_WriteByte[13] = mCRC[1];
            m_WriteByte[14] = mCRC[2];
            m_WriteByte[15] = 0x03;//ETX

            if (!write(16) || !read())
            {
                return false;
            }

            valveOut = valveIn;
            return true;
        }
    }
}
