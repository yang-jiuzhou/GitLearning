using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class TCPMixer : BaseTCP
    {
        private MixerItem m_item = new MixerItem();                     //元素
        private MIXERState m_state = MIXERState.Free;                   //状态


        /// <summary>
        /// 构造函数
        /// </summary>
        public TCPMixer(ComConf info) : base(info)
        {
            if (0 != m_scInfo.MList.Count)
            {
                m_item = (MixerItem)m_scInfo.MList[0];
            }
        }

        /// <summary>
        /// 获取运行数据读值
        /// </summary>
        /// <returns></returns>
        public override List<object> GetRunDataValueList()
        {
            List<object> result = new List<object>();

            result.Add(m_item.m_onoffGet ? Share.ReadXaml.S_On : Share.ReadXaml.S_Off);

            return result;
        }

        /// <summary>
        /// 获取运行数据写值
        /// </summary>
        /// <returns></returns>
        public override List<object> SetRunDataValueList()
        {
            List<object> result = new List<object>();

            result.Add(!m_item.m_pause && m_item.m_onoffSet ? Share.ReadXaml.S_On : Share.ReadXaml.S_Off);

            return result;
        }

        /// <summary>
        /// 线程主函数
        /// </summary>
        protected override void ThreadRun()
        {
            while (true)
            {
                switch (m_state)
                {
                    case MIXERState.Free:
                        Close();
                        Thread.Sleep(DlyBase.c_sleep10);
                        m_communState = ENUMCommunicationState.Free;
                        break;
                    case MIXERState.Version:
                        if (Connect())
                        {
                            string tempVersion = null;
                            ReadVersion(ref tempVersion);
                            m_scInfo.MVersion = tempVersion;
                            Close();
                        }
                        m_state = MIXERState.Free;
                        break;
                    case MIXERState.ReadWrite:
                        if (Connect() && OpenOrClose(!m_item.m_pause && m_item.m_onoffSet, ref m_item.m_onoffGet))
                        {
                            m_communState = ENUMCommunicationState.Success;
                            Thread.Sleep(DlyBase.c_sleep5);
                        }
                        else
                        {
                            Close();

                            for (int i = 0; i < c_timeout; i++)
                            {
                                if (MIXERState.ReadWrite != m_state)
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
                    case MIXERState.Abort:
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
                    m_state = MIXERState.Free;
                    break;
                case ENUMThreadStatus.Version:
                    m_state = MIXERState.Version;
                    break;
                case ENUMThreadStatus.WriteOrRead:
                    m_state = MIXERState.ReadWrite;
                    break;
                case ENUMThreadStatus.Abort:
                    m_state = MIXERState.Abort;
                    break;
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
                    model = ENUMOtherID.Mixer.ToString();
                    result = true;
                }
            }
            catch
            { }

            return result;
        }

        /// <summary>
        /// 开关,0关,1开
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public bool OpenOrClose(bool setFlag, ref bool readFlag)
        {
            try
            {
                //开PC3：02 36 31 30 30 32 30 30 30 30 30 30 30 32 37 03					
                //关PC3：02 36 31 30 30 32 30 30 30 30 30 31 30 32 38 03				
                //正确返回：23Hex  错误返回：24Hex
                m_WriteByte[0] = 0x02;
                m_WriteByte[1] = 0x36;
                m_WriteByte[2] = 0x31;
                m_WriteByte[3] = 0x30;
                m_WriteByte[4] = 0x30;
                m_WriteByte[5] = 0x32;
                m_WriteByte[6] = 0x30;
                m_WriteByte[7] = 0x30;
                m_WriteByte[8] = 0x30;
                m_WriteByte[9] = 0x30;
                m_WriteByte[10] = 0x30;
                m_WriteByte[12] = 0x30;
                m_WriteByte[13] = 0x32;
                m_WriteByte[15] = 0x03;
                if (setFlag)
                {
                    m_WriteByte[11] = 0x30;
                    m_WriteByte[14] = 0x37;
                }
                else
                {
                    m_WriteByte[11] = 0x31;
                    m_WriteByte[14] = 0x38;
                }

                if (!write(16) || !read())
                {
                    return false;
                }

                if (0x23 == m_ReadByte[0])
                {
                    readFlag = setFlag;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
