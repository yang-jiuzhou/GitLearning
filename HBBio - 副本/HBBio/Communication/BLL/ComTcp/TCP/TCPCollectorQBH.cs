using HBBio.Collection;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class TCPCollectorQBH : TCPCollector
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TCPCollectorQBH(ComConf info) : base(info)
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
                    case CollectorState.Free:
                        Thread.Sleep(DlyBase.c_sleep10);
                        m_communState = ENUMCommunicationState.Free;
                        break;
                    case CollectorState.FreeFirst:
                        Close();
                        m_state = CollectorState.Free;
                        break;
                    case CollectorState.Version:
                        if (Connect())
                        {
                            string tempVersion = null;
                            ReadVersion(ref tempVersion);
                            m_scInfo.MVersion = tempVersion;
                            Close();
                        }
                        m_state = CollectorState.Free;
                        break;
                    case CollectorState.ReadFirst:
                        if (Connect())
                        {
                            m_countL = EnumCollectorInfo.CountL;
                            m_countR = EnumCollectorInfo.CountR;
                            ReadStatus(ref m_item.m_txtGet, ref m_item.m_indexGet, ref m_item.m_ingGet);

                            m_item.m_countL = m_countL;
                            m_item.m_countR = m_countR;

                            m_item.m_indexSet = m_item.m_indexGet;
                            m_item.m_txtSet = m_item.m_txtGet;
                            m_item.m_ingSet = m_item.m_ingGet;
                        }
                        Thread.Sleep(DlyBase.c_sleep50);
                        m_state = CollectorState.ReadWrite;
                        break;
                    case CollectorState.ReadWrite:
                        if (Connect() && ReadStatus(ref m_item.m_txtGet, ref m_item.m_indexGet, ref m_item.m_ingGet))
                        {
                            m_communState = ENUMCommunicationState.Success;

                            m_item.MStatusGet = m_item.m_ingGet;
                            m_item.MIndexGet = m_item.m_txtGet.ToString() + m_item.m_indexGet;
                            if (m_item.m_ingSet != m_item.m_ingGet)
                            {
                                if (m_item.m_ingSet)
                                {
                                    WriteStart();
                                }
                                else
                                {
                                    WriteStop();
                                } 
                            }
                            if (m_item.m_indexSet != m_item.m_indexGet || m_item.m_txtSet != m_item.m_txtGet)
                            {
                                WriteIndex(m_item.m_txtSet, m_item.m_indexSet);
                            }
                        }
                        else
                        {
                            Close();

                            for (int i = 0; i < c_timeout; i++)
                            {
                                if (CollectorState.ReadWrite != m_state)
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
                    case CollectorState.Abort:
                        Close();
                        m_communState = ENUMCommunicationState.Over;
                        return;
                }
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
                m_WriteByte[2] = 0x38;
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
            ENUMCollectorID id = ENUMCollectorID.QBH_DLY;
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
                m_WriteByte[2] = 0x38;
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
                m_WriteByte[2] = 0x38;
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
        private bool ReadID(ref ENUMCollectorID id)
        {
            bool result = false;

            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0x39;
                m_WriteByte[2] = 0x38;
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
                    if (98 == temp)
                    {
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
        public bool ReadStatus(ref EnumCollIndexText left, ref int index, ref bool on)
        {
            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0X39;
                m_WriteByte[2] = 0X38;
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
                if (index > m_countL)
                {
                    index -= m_countL;
                    left = EnumCollIndexText.R;
                }
                else
                {
                    left = EnumCollIndexText.L;
                }

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
        public bool WriteIndex(EnumCollIndexText left, int index)
        {
            try
            {
                //发送命令
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0X39;
                m_WriteByte[2] = 0X38;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x31;
                m_WriteByte[5] = 0x30;
                switch (left)
                {
                    case EnumCollIndexText.L:
                        IntToByte(ref m_WriteByte, index);//VALUE
                        break;
                    case EnumCollIndexText.R:
                        IntToByte(ref m_WriteByte, m_countL + index);//VALUE
                        break;
                }
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
                m_WriteByte[2] = 0X38;
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
                m_WriteByte[2] = 0X38;
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
                m_WriteByte[2] = 0X38;
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
                m_WriteByte[2] = 0X38;
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
                m_WriteByte[2] = 0X38;
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
