using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class ComValveHBColl : ComValve
    {
        private bool m_changeBeginEnd = false;


        /// <summary>
        /// 构造函数
        /// </summary>
        public ComValveHBColl(ComConf info) : base(info)
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
                    case ENUMValveID.HB_Coll6:
                    case ENUMValveID.HB_Coll8:
                    case ENUMValveID.HB_Coll10:
                    case ENUMValveID.HB_Coll12:
                        if (m_id.ToString().Contains("HB_Coll"))
                        {
                            int tempCount = Convert.ToInt32(m_id.ToString().Replace("HB_Coll", ""));
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
        /// 读型号
        /// </summary>
        /// <returns></returns>
        public override bool ReadVersion(ref string version)
        {
            version = "";

            string model = "";
            if (ReadModel(ref model))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 读序列
        /// </summary>
        /// <returns></returns>
        public override bool ReadSerial(ref string serial)
        {
            serial = "";

            string model = "";
            if (ReadModel(ref model))
            {
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
            try
            {
                //发送命令
                m_WriteByte[0] = 0x02;
                m_WriteByte[1] = 0x30;
                m_WriteByte[2] = 0x30;
                m_WriteByte[3] = 0x30;
                m_WriteByte[4] = 0x30;
                m_WriteByte[5] = 0x30;
                m_WriteByte[6] = 0x30;
                m_WriteByte[7] = 0x03;

                if (!write(8) || !read())
                {
                    return false;
                }

                if (0x02 == m_ReadByte[0] && 0x39 == m_ReadByte[1])
                {
                    model = "HB_Coll" + ((m_ReadByte[5] - 0x30) * 10 + (m_ReadByte[6] - 0x30));
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
            try
            {
                m_WriteByte[0] = 0x02;
                m_WriteByte[1] = 0x30;
                m_WriteByte[2] = 0x34;
                m_WriteByte[3] = 0x30;
                m_WriteByte[4] = 0x30;
                m_WriteByte[5] = 0x30;
                m_WriteByte[6] = 0x30;
                m_WriteByte[7] = 0x03;

                if (!write(8) || !read())
                {
                    return false;
                }

                if (0x56 == m_ReadByte[0] && 0x61 == m_ReadByte[1])
                {
                    valve = m_ReadByte[7] - 0x30 - 1;
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
            }
            catch
            { }

            return false;
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
                    m_WriteByte[0] = 0x02;
                    m_WriteByte[1] = 0x30;
                    m_WriteByte[2] = 0x33;
                    m_WriteByte[3] = 0x30;
                    m_WriteByte[4] = 0x30;
                    m_WriteByte[5] = 0x30;
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
                    m_WriteByte[6] = (byte)(0x30 + (valveIn + 1));
                    m_WriteByte[7] = 0x03;

                    if (!write(8) || !read())
                    {
                        return false;
                    }

                    if (0x41 == m_ReadByte[0] && 0x55 == m_ReadByte[1])
                    {
                        if (valveIn + 1 == m_ReadByte[7] - 0x30)
                        {
                            return ReadValue(ref valveOut);
                        }
                    }

                    return false;
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
