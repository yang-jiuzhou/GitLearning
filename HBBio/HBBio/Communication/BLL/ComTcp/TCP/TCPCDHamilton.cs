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
    class TCPCDHamilton : TCPPHCD
    {
        private CDItem m_CdItem = new CDItem();        //Cd元素
        private TTItem m_ttItem = new TTItem();      //温度元素


        /// <summary>
        /// 构造函数
        /// </summary>
        public TCPCDHamilton(ComConf info) : base(info)
        {
            if (0 != m_scInfo.MList.Count)
            {
                m_CdItem = (CDItem)m_scInfo.MList[0];
                m_ttItem = (TTItem)m_scInfo.MList[1];
            }
        }

        /// <summary>
        /// 获取运行数据读值
        /// </summary>
        /// <returns></returns>
        public override List<object> GetRunDataValueList()
        {
            List<object> result = new List<object>();
            result.Add(m_CdItem.m_CdGet);
            result.Add(m_ttItem.m_tempGet);

            return result;
        }

        /// <summary>
        /// 获取运行数据写值
        /// </summary>
        /// <returns></returns>
        public override List<object> SetRunDataValueList()
        {
            List<object> result = new List<object>();
            result.Add("N/A");
            result.Add("N/A");

            return result;
        }

        /// <summary>
        /// 线程主函数
        /// </summary>
        protected override void ThreadRun()
        {
            int phcdNum = 0;

            double cdVal = 0;

            while (true)
            {
                switch (m_state)
                {
                    case PHCDState.Free:
                        Close();
                        Thread.Sleep(DlyBase.c_sleep10);
                        m_communState = ENUMCommunicationState.Free;
                        break;
                    case PHCDState.Version:
                        if (Connect())
                        {
                            string tempVersion = null;
                            ReadVersion(ref tempVersion);
                            m_scInfo.MVersion = tempVersion;
                            Close();
                        }
                        m_state = PHCDState.Free;
                        break;
                    case PHCDState.Read:
                        if (Connect() && ReadCDValue(ref cdVal))
                        {
                            m_communState = ENUMCommunicationState.Success;

                            m_CdItem.UpdateValue(cdVal);

                            if (0 == phcdNum % 10)
                            {
                                ReadCDTempeture(ref m_ttItem.m_tempGet);
                                if (0 == phcdNum % 60)
                                {
                                    ReadCDTime(ref m_CdItem.m_timeGet);
                                }
                            }

                            Thread.Sleep(DlyBase.c_sleep5);

                            phcdNum++;
                            if (61 == phcdNum)
                            {
                                phcdNum = 1;
                            }
                        }
                        else
                        {
                            Close();

                            for (int i = 0; i < c_timeout; i++)
                            {
                                if (PHCDState.Read != m_state)
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
                    case PHCDState.Abort:
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
            return true;
        }

        /// <summary>
        /// 读模式
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override bool ReadModel(ref string model)
        {
            try
            {
                m_WriteByte[0] = 0x01;//设备地址
                m_WriteByte[1] = 0x03;//功能码，读保持寄存器
                m_WriteByte[2] = 0x08;//读起始地址
                m_WriteByte[3] = 0x29;
                m_WriteByte[4] = 0x00;//读寄存器个数
                m_WriteByte[5] = 0x0A;
                byte[] crc = CRC.CRCLen(m_WriteByte, 6);
                m_WriteByte[6] = crc[0];//CRC校验
                m_WriteByte[7] = crc[1];

                if (!write(8) || !read())
                {
                    return false;
                }

                if (0x01 == m_ReadByte[0] && 0x03 == m_ReadByte[1]
                    && (0x02 == m_ReadByte[3] || 0x04 == m_ReadByte[3]) && 0x00 == m_ReadByte[4]
                    && 0x00 == m_ReadByte[5] && 0x00 == m_ReadByte[6])
                {
                    model = ENUMDetectorID.CdHamilton.ToString();
                    return true;
                }
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 读柱前Cd值
        /// </summary>
        /// <returns></returns>
        public bool ReadCDValue(ref double val)
        {
            try
            {
                m_WriteByte[0] = 0x01;//设备地址
                m_WriteByte[1] = 0x03;//功能码，读保持寄存器
                m_WriteByte[2] = 0x08;//读起始地址
                m_WriteByte[3] = 0x29;
                m_WriteByte[4] = 0x00;//读寄存器个数
                m_WriteByte[5] = 0x0A;
                byte[] crc = CRC.CRCLen(m_WriteByte, 6);
                m_WriteByte[6] = crc[0];//CRC校验
                m_WriteByte[7] = crc[1];

                if (!write(8) || !read())
                {
                    return false;
                }

                if (0x01 == m_ReadByte[0] && 0x03 == m_ReadByte[1])
                {
                    switch (m_ReadByte[3])
                    {
                        case 0x02:
                            val = Math.Round(MByteToFloat(m_ReadByte[7], m_ReadByte[8], m_ReadByte[9], m_ReadByte[10]) / 1000, 3);
                            break;
                        case 0x04:
                            val = Math.Round(MByteToFloat(m_ReadByte[7], m_ReadByte[8], m_ReadByte[9], m_ReadByte[10]), 3);
                            break;
                    }
                    if (0 > val || 999 < val)
                    {
                        val = 0;
                    }
                    return true;
                }
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 读温度值
        /// </summary>
        /// <returns></returns>
        public bool ReadCDTempeture(ref double val)
        {
            try
            {
                m_WriteByte[0] = 0x01;//设备地址
                m_WriteByte[1] = 0x03;//功能码，读保持寄存器
                m_WriteByte[2] = 0x09;//读起始地址
                m_WriteByte[3] = 0x69;
                m_WriteByte[4] = 0x00;//读寄存器个数
                m_WriteByte[5] = 0x0A;
                byte[] crc = CRC.CRCLen(m_WriteByte, 6);
                m_WriteByte[6] = crc[0];//CRC校验
                m_WriteByte[7] = crc[1];

                if (!write(8) || !read())
                {
                    return false;
                }

                if (0x01 == m_ReadByte[0] && 0x03 == m_ReadByte[1])
                {
                    val = Math.Round(MByteToFloat(m_ReadByte[7], m_ReadByte[8], m_ReadByte[9], m_ReadByte[10]), 2);
                    if (0 > val || 100 < val)
                    {
                        val = 0;
                    }
                    return true;
                }
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 读柱前Cd时间
        /// </summary>
        /// <returns></returns>
        public bool ReadCDTime(ref double val)
        {
            try
            {
                m_WriteByte[0] = 0x01;//设备地址
                m_WriteByte[1] = 0x03;//功能码，读保持寄存器
                m_WriteByte[2] = 0x12;//读起始地址
                m_WriteByte[3] = 0x43;
                m_WriteByte[4] = 0x00;//读寄存器个数
                m_WriteByte[5] = 0x06;
                byte[] crc = CRC.CRCLen(m_WriteByte, 6);
                m_WriteByte[6] = crc[0];//CRC校验
                m_WriteByte[7] = crc[1];

                if (!write(8) || !read())
                {
                    return false;
                }

                if (0x01 == m_ReadByte[0] && 0x03 == m_ReadByte[1])
                {
                    val = Math.Round(MByteToFloat(m_ReadByte[3], m_ReadByte[4], m_ReadByte[5], m_ReadByte[6]), 2);
                    return true;
                }
            }
            catch
            { }

            return false;
        }

        private double MByteToFloat(byte b1, byte b2, byte b3, byte b4)
        {
            byte[] intBuffer = new byte[4];
            //将byte数组的前后两个字节的高低位换过来
            intBuffer[0] = b2;
            intBuffer[1] = b1;
            intBuffer[2] = b4;
            intBuffer[3] = b3;
            return BitConverter.ToSingle(intBuffer, 0);
        }
    }
}
