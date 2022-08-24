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
    class ComASABD06 : ComAS
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ComASABD06(ComConf info) : base(info)
        {

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
                    case ASState.Free:
                        Close();
                        Thread.Sleep(DlyBase.c_sleep10);
                        m_communState = ENUMCommunicationState.Free;
                        break;
                    case ASState.Version:
                        if (Connect())
                        {
                            string tempVersion = null;
                            ReadVersion(ref tempVersion);
                            m_scInfo.MVersion = tempVersion;
                            Close();
                        }
                        m_state = ASState.Free;
                        break;
                    case ASState.First:
                        if (Connect())
                        {
                            SetSendF(1);
                            Close();
                        }
                        m_state = ASState.Read;
                        break;
                    case ASState.Read:
                        if (Connect() && ReadSize(ref m_item.m_sizeGet))
                        {
                            m_communState = ENUMCommunicationState.Success;
                        }
                        else
                        {
                            Close();

                            for (int i = 0; i < c_timeout; i++)
                            {
                                if (ASState.Read != m_state)
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
                    case ASState.Abort:
                        Close();
                        m_communState = ENUMCommunicationState.Over;
                        return;
                }
            }
        }


        /// <summary>
        /// 读AS型号
        /// </summary>
        /// <returns></returns>
        public override bool ReadVersion(ref string version)
        {
            SetSendF(0);

            bool result = false;

            try
            {
	            m_WriteByte[0] = 0x02;
	            m_WriteByte[1] = 0x36;
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
                m_WriteByte[15] = 0x03;//ETX

                if (!write(16) || !read())
                {
                    return false;
                }

                if (m_WriteByte[0] == m_ReadByte[0] && m_WriteByte[1] == m_ReadByte[1] && m_WriteByte[2] == m_ReadByte[2])
                {
                    version = Encoding.Default.GetString(m_ReadByte, 6, m_ReadLen);
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
            return true;
        }

        /// <summary>
        /// 读气泡大小
        /// </summary>
        /// <returns></returns>
        public bool ReadSize(ref double size)
        {
            if (!read(DlyBase.c_sleep5))
            {
                return false;
            }

            double asSize = 0;
            for (int index = 0; index < m_ReadLen - 15;)
            {
                if (0x02 == m_ReadByte[index++] && 0x36 == m_ReadByte[index++] && 0x30 == m_ReadByte[index++])
                {
                    double temp = 0;
                    index += 5;
                    if (m_ReadByte[index++] > 0x30)
                    {
                        temp += (m_ReadByte[index - 1] - 0x30) * 10;
                    }
                    if (m_ReadByte[index++] > 0x30)
                    {
                        temp += (m_ReadByte[index - 1] - 0x30) * 1;
                    }
                    if (m_ReadByte[index++] > 0x30)
                    {
                        temp += (m_ReadByte[index - 1] - 0x30) * 0.1;
                    }
                    if (m_ReadByte[index++] > 0x30)
                    {
                        temp += (m_ReadByte[index - 1] - 0x30) * 0.01;
                    }

                    if (temp > asSize)
                    {
                        asSize = temp;
                    }
                }
            }

            if (asSize < 4)
            {
                asSize = 4;
            }
            else if (asSize > 20)
            {
                asSize = 20;  
            }

            size = (asSize - 4) / 16 * 100;

            return true;
        }

        /// <summary>
        /// 设定发送频率
        /// </summary>
        /// <returns></returns>
        private bool SetSendF(int flag)
        {
            m_WriteByte[0] = 0x02;
            m_WriteByte[1] = 0x36;
            m_WriteByte[2] = 0x30;
            m_WriteByte[3] = 0x30;//AI，默认，“0”
            m_WriteByte[4] = 0x30;//PFC，读产品ID，01
            m_WriteByte[5] = 0x34;
            m_WriteByte[6] = 0x20;//VALUE
            m_WriteByte[7] = 0x20;
            m_WriteByte[8] = 0x20;
            m_WriteByte[9] = (99 < flag) ? ((byte)(0x30 + (flag / 100))) : (byte)0x20;
            m_WriteByte[10] = (9 < flag) ? ((byte)(0x30 + (flag / 10))) : (byte)0x20;
            m_WriteByte[11] = (0 < flag) ? ((byte)(0x30 + (flag % 10))) : (byte)0x20;
            byte[] mCRC = CRC.Cal12(m_WriteByte);//CRC校验
            m_WriteByte[12] = mCRC[0];
            m_WriteByte[13] = mCRC[1];
            m_WriteByte[14] = mCRC[2];
            m_WriteByte[15] = 0x03;//ETX

            if (!write(16) || !read())
            {
                return false;
            }

            if (0 == flag)
            {
                int index = 0;
                while (read())
                {
                    if (index++ > 5)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
