using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class ComUVHT2 : ComUV
    {
        private byte m_timeA = 1;           //A上升时间
        private byte m_timeB = 1;           //B上升时间
        private byte m_frequency = 10;      //采样率
        private byte m_broadband = 1;       //宽带
        private byte m_rangeL = 1;          //输出范围L
        private byte m_rangeH = 1;          //输出范围H
        private byte m_work = 0;            //工作模式，0双，1A，2B
        private DateTime m_lastTimeRead = DateTime.Now;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ComUVHT2(ComConf info) : base(info)
        {
            switch (MComConf.MCommunMode)
            {
                case EnumCommunMode.Com:
                    m_serialPort.BaudRate = 38400;
                    m_serialPort.ReadTimeout = DlyBase.c_sleep5;
                    break;
                case EnumCommunMode.TCP:
                    break;
            }
            
            m_ReadByte = new byte[1024];                //读数据
        }

        /// <summary>
        /// 线程主函数
        /// </summary>
        protected override void ThreadRun()
        {
            bool lamp = false;
            double time = 0;
            int refValA = 0;
            int sigValA = 0;
            int refValB = 0;
            int sigValB = 0;
            double[] abs = new double[4];
            int start = 0;

            long uvNum = 0;
            while (true)
            {
                switch (m_state)
                {
                    case UVState.Free:
                        Close();
                        Thread.Sleep(DlyBase.c_sleep10);
                        m_communState = ENUMCommunicationState.Free;
                        break;
                    case UVState.Version:
                        if (Connect())
                        {
                            string tempVersion = null;
                            ReadVersion(ref tempVersion);
                            m_scInfo.MVersion = tempVersion;
                            Close();
                        }
                        m_state = UVState.Free;
                        break;
                    case UVState.Read:
                        if (Connect() && ReadAu(ref start, ref m_item.m_waveGet[0], ref m_item.m_waveGet[1], ref abs[0], ref abs[1]))
                        {
                            m_communState = ENUMCommunicationState.Success;

                            m_item.UpdateAbs(abs);

                            if (0 == uvNum % 10)
                            {
                                sigValA = m_item.MSig;
                                refValA = m_item.MRef;
                                sigValB = m_item.MSig;
                                refValB = m_item.MRef;
                                lamp = m_item.MLamp;
                                time = m_item.MTime;
                                if (ReadSig(ref sigValA, ref refValA, ref sigValB, ref refValB, ref lamp, ref time))
                                {
                                    m_item.MSig = Math.Max(sigValA, sigValB);
                                    m_item.MRef = Math.Max(refValA, refValB);
                                    m_item.MLamp = lamp;
                                    m_item.MTime = time;
                                }
                            }

                            if (m_item.m_wave)
                            {
                                m_item.m_wave = false;
                                WriteWave(m_item.m_waveSet[0], m_item.m_waveSet[1]);
                            }
                            if (m_item.m_clear)
                            {
                                m_item.m_clear = false;
                                WriteZero();
                            }
                            if (m_item.m_lampOn)
                            {
                                m_item.m_lampOn = false;
                                WriteLampOn();
                            }
                            if (m_item.m_lampOff)
                            {
                                m_item.m_lampOff = false;
                                WriteLampOff();
                            }

                            uvNum++;

                            //Thread.Sleep(DlyBase.c_sleep5);
                            if ((DateTime.Now - m_lastTimeRead).TotalMilliseconds < 1000)
                            {
                                Thread.Sleep(1000 - (int)(DateTime.Now - m_lastTimeRead).TotalMilliseconds);
                            }
                            m_lastTimeRead = DateTime.Now;
                        }
                        else
                        {
                            Close();

                            for (int i = 0; i < c_timeout; i++)
                            {
                                if (UVState.Read != m_state)
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
                    case UVState.ReadFirst:
                        if (Connect() && ReadWave(ref m_item.m_waveGet[0], ref m_item.m_waveGet[1]))
                        {
                            m_communState = ENUMCommunicationState.Success;
                            sigValA = m_item.MSig;
                            refValA = m_item.MRef;
                            sigValB = m_item.MSig;
                            refValB = m_item.MRef;
                            lamp = m_item.MLamp;
                            time = m_item.MTime;
                            if (ReadSig(ref sigValA, ref refValA, ref sigValB, ref refValB, ref lamp, ref time))
                            {
                                m_item.MSig = Math.Max(sigValA, sigValB);
                                m_item.MRef = Math.Max(refValA, refValB);
                                m_item.MLamp = lamp;
                                m_item.MTime = time;
                            }
                            WriteStop();
                            WriteStart();
                            start = 0;
                        }
                        m_state = UVState.Read;
                        break;
                    case UVState.Abort:
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
            try
            {
                m_WriteByte[0] = 0x55;

                m_WriteByte[1] = 0x0B;//15 - 1 - 2 - 1 = 0B
                m_WriteByte[2] = 0x00;

                m_WriteByte[3] = 0x00;
                m_WriteByte[4] = 0x00;
                m_WriteByte[5] = 0x00;
                m_WriteByte[6] = 0x00;

                m_WriteByte[7] = 0x01;
                m_WriteByte[8] = 0x00;
                m_WriteByte[9] = 0x00;
                m_WriteByte[10] = 0x00;

                m_WriteByte[11] = 0x06;
                m_WriteByte[12] = 0x01;

                m_WriteByte[13] = CRC.GetXOR(m_WriteByte, 1, 12);//15 - 1 - 1 - 1 = 12

                m_WriteByte[14] = 0x0D;

                if (write(15) && read())
                {
                    if (0x55 == m_ReadByte[0] && 0x06 == m_ReadByte[11] && 0x81 == m_ReadByte[12])
                    {
                        version = "";
                        return true;
                    }
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
            return ReadVersion(ref serial);
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
                if (ReadVersion(ref model))
                {
                    model = ENUMDetectorID.UVHT2.ToString();
                    result = true;
                }
            }
            catch
            { }

            return result;
        }

        /// <summary>
        /// 读吸收值
        /// </summary>
        /// <returns></returns>
        public bool ReadAu(ref int time, ref int wave1, ref int wave2, ref double uv1, ref double uv2)
        {
            try
            {
                m_WriteByte[0] = 0x55;

                m_WriteByte[1] = 0x10;//20 - 1 - 2 - 1 = 10
                m_WriteByte[2] = 0x00;

                m_WriteByte[3] = 0x00;
                m_WriteByte[4] = 0x00;
                m_WriteByte[5] = 0x00;
                m_WriteByte[6] = 0x00;

                m_WriteByte[7] = 0x01;
                m_WriteByte[8] = 0x00;
                m_WriteByte[9] = 0x00;
                m_WriteByte[10] = 0x00;

                m_WriteByte[11] = 0x06;
                m_WriteByte[12] = 0x37;

                byte[] arrTime = BitConverter.GetBytes(time);
                m_WriteByte[13] = arrTime[0];
                m_WriteByte[14] = arrTime[1];
                m_WriteByte[15] = arrTime[2];
                m_WriteByte[16] = arrTime[3];
                m_WriteByte[17] = 0x00;

                m_WriteByte[18] = CRC.GetXOR(m_WriteByte, 1, 17);//20 - 1 - 1 - 1 = 17

                m_WriteByte[19] = 0x0D;

                if (write(20) && read(DlyBase.c_sleep3))
                {
                    if (0x55 == m_ReadByte[0] && 0x06 == m_ReadByte[11] && 0xB7 == m_ReadByte[12])
                    {
                        if (0 == m_ReadByte[24])
                        {
                            time = BitConverter.ToInt32(m_ReadByte, 20);
                        }
                        else
                        {
                            wave1 = BitConverter.ToInt16(m_ReadByte, 16);
                            wave2 = BitConverter.ToInt16(m_ReadByte, 18);
                            double sum1 = 0;
                            double sum2 = 0;
                            for (int i = 0; i < m_ReadByte[24]; i++)
                            {
                                sum1 += BitConverter.ToInt32(m_ReadByte, 25 + 8 * i) / 10000.0;
                                sum2 += BitConverter.ToInt32(m_ReadByte, 29 + 8 * i) / 10000.0;
                            }
                            uv1 = sum1 / m_ReadByte[24];
                            uv2 = sum2 / m_ReadByte[24];
                            time += (m_ReadByte[24] - 1);
                        }
                        return true;
                    }
                }
                else if (write(20) && read(DlyBase.c_sleep3))
                {
                    if (0x55 == m_ReadByte[0] && 0x06 == m_ReadByte[11] && 0xB7 == m_ReadByte[12])
                    {
                        if (0 == m_ReadByte[24])
                        {
                            time = BitConverter.ToInt32(m_ReadByte, 20);
                        }
                        else
                        {
                            wave1 = BitConverter.ToInt16(m_ReadByte, 16);
                            wave2 = BitConverter.ToInt16(m_ReadByte, 18);
                            double sum1 = 0;
                            double sum2 = 0;
                            for (int i = 0; i < m_ReadByte[24]; i++)
                            {
                                sum1 += BitConverter.ToInt32(m_ReadByte, 25 + 8 * i) / 10000.0;
                                sum2 += BitConverter.ToInt32(m_ReadByte, 29 + 8 * i) / 10000.0;
                            }
                            uv1 = sum1 / m_ReadByte[24];
                            uv2 = sum2 / m_ReadByte[24];
                            time += (m_ReadByte[24] - 1);
                        }
                        return true;
                    }
                }
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 读波长
        /// </summary>
        /// <returns></returns>
        public bool ReadWave(ref int wave1, ref int wave2)
        {
            try
            {
                m_WriteByte[0] = 0x55;

                m_WriteByte[1] = 0x0B;//15 - 1 - 2 - 1 = 0B
                m_WriteByte[2] = 0x00;

                m_WriteByte[3] = 0x00;
                m_WriteByte[4] = 0x00;
                m_WriteByte[5] = 0x00;
                m_WriteByte[6] = 0x00;

                m_WriteByte[7] = 0x01;
                m_WriteByte[8] = 0x00;
                m_WriteByte[9] = 0x00;
                m_WriteByte[10] = 0x00;

                m_WriteByte[11] = 0x06;
                m_WriteByte[12] = 0x31;

                m_WriteByte[13] = CRC.GetXOR(m_WriteByte, 1, 12);//15 - 1 - 1 - 1 = 12

                m_WriteByte[14] = 0x0D;

                if (write(15) && read())
                {
                    if (0x55 == m_ReadByte[0] && 0x06 == m_ReadByte[11] && 0xB1 == m_ReadByte[12])
                    {
                        wave1 = m_ReadByte[13] + 256 * m_ReadByte[14];
                        wave2 = m_ReadByte[15] + 256 * m_ReadByte[16];
                        m_timeA = m_ReadByte[17];
                        m_timeB = m_ReadByte[18];
                        m_frequency = m_ReadByte[19];
                        m_rangeL = m_ReadByte[20];
                        m_rangeH = m_ReadByte[21];
                        m_work = m_ReadByte[22];

                        return true;
                    }
                }
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 读取状态
        /// </summary>
        /// <returns></returns>
        public bool ReadSig(ref int sigValA, ref int refValA, ref int sigValB, ref int refValB, ref bool onoff, ref double time)
        {
            try
            {
                m_WriteByte[0] = 0x55;

                m_WriteByte[1] = 0x0B;//15 - 1 - 2 - 1 = 0B
                m_WriteByte[2] = 0x00;

                m_WriteByte[3] = 0x00;
                m_WriteByte[4] = 0x00;
                m_WriteByte[5] = 0x00;
                m_WriteByte[6] = 0x00;

                m_WriteByte[7] = 0x01;
                m_WriteByte[8] = 0x00;
                m_WriteByte[9] = 0x00;
                m_WriteByte[10] = 0x00;

                m_WriteByte[11] = 0x06;
                m_WriteByte[12] = 0x3B;

                m_WriteByte[13] = CRC.GetXOR(m_WriteByte, 1, 12);//15 - 1 - 1 - 1 = 12

                m_WriteByte[14] = 0x0D;

                if (write(15) && read())
                {
                    if (0x55 == m_ReadByte[0] && 0x06 == m_ReadByte[11] && 0xBB == m_ReadByte[12])
                    {
                        m_work = m_ReadByte[13];
                        sigValA = BitConverter.ToInt32(m_ReadByte, 14);
                        refValA = BitConverter.ToInt32(m_ReadByte, 18);
                        sigValB = BitConverter.ToInt32(m_ReadByte, 22);
                        refValB = BitConverter.ToInt32(m_ReadByte, 26);
                        onoff = BitConverter.ToBoolean(m_ReadByte, 30);
                        time = BitConverter.ToInt16(m_ReadByte, 31);

                        return true;
                    }
                }
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 设置波长
        /// </summary>
        /// <param name="Wave1"></param>
        /// <param name="Wave2"></param>
        /// <param name="Wave3"></param>
        /// <param name="Wave4"></param>
        /// <returns></returns>
        public bool WriteWave(int Wave1, int Wave2)
        {
            try
            {
                m_WriteByte[0] = 0x55;

                m_WriteByte[1] = 0x16;//26 - 1 - 2 - 1 = 16
                m_WriteByte[2] = 0x00;

                m_WriteByte[3] = 0x00;
                m_WriteByte[4] = 0x00;
                m_WriteByte[5] = 0x00;
                m_WriteByte[6] = 0x00;

                m_WriteByte[7] = 0x01;
                m_WriteByte[8] = 0x00;
                m_WriteByte[9] = 0x00;
                m_WriteByte[10] = 0x00;

                m_WriteByte[11] = 0x06;
                m_WriteByte[12] = 0x30;

                byte[] arrWave1 = BitConverter.GetBytes((short)Wave1);
                m_WriteByte[13] = arrWave1[0];
                m_WriteByte[14] = arrWave1[1];
                byte[] arrWave2 = BitConverter.GetBytes((short)Wave2);
                m_WriteByte[15] = arrWave2[0];
                m_WriteByte[16] = arrWave2[1];
                m_WriteByte[17] = m_timeA;
                m_WriteByte[18] = m_timeB;
                m_WriteByte[19] = m_frequency;
                m_WriteByte[20] = m_broadband;
                m_WriteByte[21] = m_rangeL;
                m_WriteByte[22] = m_rangeH;
                m_WriteByte[23] = m_work;

                m_WriteByte[24] = CRC.GetXOR(m_WriteByte, 1, 23);//26 - 1 - 1 - 1 = 23

                m_WriteByte[25] = 0x0D;

                if (write(26) && read())
                {
                    if (0x55 == m_ReadByte[0] && 0x06 == m_ReadByte[11] && 0x81 == m_ReadByte[12])
                    {
                        return true;
                    }
                }
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 开灯
        /// </summary>
        /// <returns></returns>
        public bool WriteLampOn()
        {
            try
            {
                m_WriteByte[0] = 0x55;

                m_WriteByte[1] = 0x0D;//17 - 1 - 2 - 1 = 0D
                m_WriteByte[2] = 0x00;

                m_WriteByte[3] = 0x00;
                m_WriteByte[4] = 0x00;
                m_WriteByte[5] = 0x00;
                m_WriteByte[6] = 0x00;

                m_WriteByte[7] = 0x01;
                m_WriteByte[8] = 0x00;
                m_WriteByte[9] = 0x00;
                m_WriteByte[10] = 0x00;

                m_WriteByte[11] = 0x06;
                m_WriteByte[12] = 0x39;

                m_WriteByte[13] = 0x01;
                m_WriteByte[14] = 0x01;

                m_WriteByte[15] = CRC.GetXOR(m_WriteByte, 1, 14);//17 - 1 - 1 - 1 = 14

                m_WriteByte[16] = 0x0D;

                if (write(17) && read())
                {
                    if (0x55 == m_ReadByte[0] && 0x06 == m_ReadByte[11] && 0x81 == m_ReadByte[12])
                    {
                        return true;
                    }
                }
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 关灯
        /// </summary>
        /// <returns></returns>
        public bool WriteLampOff()
        {
            try
            {
                m_WriteByte[0] = 0x55;

                m_WriteByte[1] = 0x0D;//17 - 1 - 2 - 1 = 0D
                m_WriteByte[2] = 0x00;

                m_WriteByte[3] = 0x00;
                m_WriteByte[4] = 0x00;
                m_WriteByte[5] = 0x00;
                m_WriteByte[6] = 0x00;

                m_WriteByte[7] = 0x01;
                m_WriteByte[8] = 0x00;
                m_WriteByte[9] = 0x00;
                m_WriteByte[10] = 0x00;

                m_WriteByte[11] = 0x06;
                m_WriteByte[12] = 0x39;

                m_WriteByte[13] = 0x02;
                m_WriteByte[14] = 0x02;

                m_WriteByte[15] = CRC.GetXOR(m_WriteByte, 1, 14);//17 - 1 - 1 - 1 = 14

                m_WriteByte[16] = 0x0D;

                if (write(17) && read())
                {
                    if (0x55 == m_ReadByte[0] && 0x06 == m_ReadByte[11] && 0x81 == m_ReadByte[12])
                    {
                        return true;
                    }
                }
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 清零
        /// </summary>
        /// <returns></returns>
        public bool WriteZero()
        {
            try
            {
                m_WriteByte[0] = 0x55;

                m_WriteByte[1] = 0x0B;//15 - 1 - 2 - 1 = 0B
                m_WriteByte[2] = 0x00;

                m_WriteByte[3] = 0x00;
                m_WriteByte[4] = 0x00;
                m_WriteByte[5] = 0x00;
                m_WriteByte[6] = 0x00;

                m_WriteByte[7] = 0x01;
                m_WriteByte[8] = 0x00;
                m_WriteByte[9] = 0x00;
                m_WriteByte[10] = 0x00;

                m_WriteByte[11] = 0x06;
                m_WriteByte[12] = 0x33;

                m_WriteByte[13] = CRC.GetXOR(m_WriteByte, 1, 12);//15 - 1 - 1 - 1 = 12

                m_WriteByte[14] = 0x0D;

                if (write(15) && read())
                {
                    if (0x55 == m_ReadByte[0] && 0x06 == m_ReadByte[11] && 0x81 == m_ReadByte[12])
                    {
                        return true;
                    }
                }
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 开始
        /// </summary>
        /// <returns></returns>
        public bool WriteStart()
        {
            try
            {
                m_WriteByte[0] = 0x55;

                m_WriteByte[1] = 0x0B;//15 - 1 - 2 - 1 = 0B
                m_WriteByte[2] = 0x00;

                m_WriteByte[3] = 0x00;
                m_WriteByte[4] = 0x00;
                m_WriteByte[5] = 0x00;
                m_WriteByte[6] = 0x00;

                m_WriteByte[7] = 0x01;
                m_WriteByte[8] = 0x00;
                m_WriteByte[9] = 0x00;
                m_WriteByte[10] = 0x00;

                m_WriteByte[11] = 0x06;
                m_WriteByte[12] = 0x34;

                m_WriteByte[13] = CRC.GetXOR(m_WriteByte, 1, 12);//15 - 1 - 1 - 1 = 12

                m_WriteByte[14] = 0x0D;

                if (write(15) && read())
                {
                    if (0x55 == m_ReadByte[0] && 0x06 == m_ReadByte[11] && 0x81 == m_ReadByte[12])
                    {
                        return true;
                    }
                }
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 结束
        /// </summary>
        /// <returns></returns>
        public bool WriteStop()
        {
            try
            {
                m_WriteByte[0] = 0x55;

                m_WriteByte[1] = 0x0B;//15 - 1 - 2 - 1 = 0B
                m_WriteByte[2] = 0x00;

                m_WriteByte[3] = 0x00;
                m_WriteByte[4] = 0x00;
                m_WriteByte[5] = 0x00;
                m_WriteByte[6] = 0x00;

                m_WriteByte[7] = 0x01;
                m_WriteByte[8] = 0x00;
                m_WriteByte[9] = 0x00;
                m_WriteByte[10] = 0x00;

                m_WriteByte[11] = 0x06;
                m_WriteByte[12] = 0x36;

                m_WriteByte[13] = CRC.GetXOR(m_WriteByte, 1, 12);//15 - 1 - 1 - 1 = 12

                m_WriteByte[14] = 0x0D;

                if (write(15) && read())
                {
                    if (0x55 == m_ReadByte[0] && 0x06 == m_ReadByte[11] && 0x81 == m_ReadByte[12])
                    {
                        return true;
                    }
                }
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// byte转int
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <param name="b3"></param>
        /// <returns></returns>
        private int MByteToInt(byte b1, byte b2, byte b3)
        {
            return (b1 - 48) * 100 + (b2 - 48) * 10 + (b3 - 48);
        }
    }
}
