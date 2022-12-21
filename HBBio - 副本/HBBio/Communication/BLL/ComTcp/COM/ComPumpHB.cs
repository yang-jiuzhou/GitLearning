using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class ComPumpHB : ComPump
    {
        private PumpItem m_pumpItem1 = new PumpItem();
        private PumpItem m_pumpItem2 = new PumpItem();
        private PTItem m_ptItem1 = new PTItem();
        private PTItem m_ptItem2 = new PTItem();
        private bool m_runA = false;                    //是否正在运行
        private bool m_runB = false;                    //是否正在运行


        public ComPumpHB(ComConf info) : base(info)
        {
            if (0 != m_scInfo.MList.Count)
            {
                m_pumpItem1 = (PumpItem)m_scInfo.MList[0];
                m_pumpItem2 = (PumpItem)m_scInfo.MList[1];
                m_ptItem1 = (PTItem)m_scInfo.MList[2];
                m_ptItem2 = (PTItem)m_scInfo.MList[3];

                m_pumpItem1.m_maxFlowVol = m_maxFlowVol;
                m_pumpItem2.m_maxFlowVol = m_maxFlowVol;
            }
        }


        /// <summary>
        /// 获取运行数据读值
        /// </summary>
        /// <returns></returns>
        public override List<object> GetRunDataValueList()
        {
            List<object> valList = new List<object>();

            if (m_pumpItem1.MVisible)
            {
                //泵A关联总流速
                if (m_pumpItem1.MConstName.Equals(ENUMPumpName.FITA.ToString()))
                {
                    valList.Add(0.0);
                    valList.Add(0.0);
                }

                //泵S没有百分比
                if (!m_pumpItem1.MConstName.Equals(ENUMPumpName.FITS.ToString()))
                {
                    valList.Add(0.0);
                }

                valList.Add(m_pumpItem1.m_flowGet);
                valList.Add(m_pumpItem1.m_flowGet);
            }
            if (m_pumpItem2.MVisible)
            {
                //泵A关联总流速
                if (m_pumpItem2.MConstName.Equals(ENUMPumpName.FITA.ToString()))
                {
                    valList.Add(0.0);
                    valList.Add(0.0);
                }

                //泵S没有百分比
                if (!m_pumpItem2.MConstName.Equals(ENUMPumpName.FITS.ToString()))
                {
                    valList.Add(0.0);
                }

                valList.Add(m_pumpItem2.m_flowGet);
                valList.Add(m_pumpItem2.m_flowGet);
            }
            if (m_ptItem1.MVisible)
            {
                valList.Add(m_ptItem1.m_pressGet);
                if (m_ptItem1.MConstName.Equals(ENUMPTName.PTColumnBack.ToString()))
                {
                    valList.Add(0.0);
                }
            }
            if (m_ptItem2.MVisible)
            {
                valList.Add(m_ptItem2.m_pressGet);
                if (m_ptItem2.MConstName.Equals(ENUMPTName.PTColumnBack.ToString()))
                {
                    valList.Add(0.0);
                }
            }

            return valList;
        }

        /// <summary>
        /// 获取运行数据写值
        /// </summary>
        /// <returns></returns>
        public override List<object> SetRunDataValueList()
        {
            List<object> valList = new List<object>();

            if (m_pumpItem1.MVisible)
            {
                //泵A关联总流速
                if (m_pumpItem1.MConstName.Equals(ENUMPumpName.FITA.ToString()))
                {
                    valList.Add(0.0);
                    valList.Add(0.0);
                }

                //泵S没有百分比
                if (!m_pumpItem1.MConstName.Equals(ENUMPumpName.FITS.ToString()))
                {
                    valList.Add(0.0);
                }

                valList.Add(m_pumpItem1.m_flowSet);
                valList.Add(0.0);
            }
            if (m_pumpItem2.MVisible)
            {
                //泵A关联总流速
                if (m_pumpItem2.MConstName.Equals(ENUMPumpName.FITA.ToString()))
                {
                    valList.Add(0.0);
                    valList.Add(0.0);
                }

                //泵S没有百分比
                if (!m_pumpItem2.MConstName.Equals(ENUMPumpName.FITS.ToString()))
                {
                    valList.Add(0.0);
                }

                valList.Add(m_pumpItem2.m_flowSet);
                valList.Add(0.0);
            }
            if (m_ptItem1.MVisible)
            {
                valList.Add("N/A");
                if (m_ptItem1.MConstName.Equals(ENUMPTName.PTColumnBack.ToString()))
                {
                    valList.Add("N/A");
                }
            }
            if (m_ptItem2.MVisible)
            {
                valList.Add("N/A");
                if (m_ptItem2.MConstName.Equals(ENUMPTName.PTColumnBack.ToString()))
                {
                    valList.Add("N/A");
                }
            }

            return valList;
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
                    case PUMPState.Free:
                        Close();
                        Thread.Sleep(DlyBase.c_sleep10);
                        m_communState = ENUMCommunicationState.Free;
                        break;
                    case PUMPState.Version:
                        if (Connect())
                        {
                            string tempVersion = null;
                            ReadVersion(ref tempVersion);
                            m_scInfo.MVersion = tempVersion;
                            Close();
                        }
                        m_state = PUMPState.Free;
                        break;
                    case PUMPState.Start:
                        m_state = PUMPState.ReadWrite;
                        break;
                    case PUMPState.ReadWrite:
                        if (Connect() && (!m_ptItem1.MVisible || ReadPressA(ref m_ptItem1.m_pressGet)) && (!m_ptItem2.MVisible || ReadPressB(ref m_ptItem2.m_pressGet)))
                        {
                            m_communState = ENUMCommunicationState.Success;

                            if (m_pumpItem1.MVisible)
                            {
                                if (0 < m_pumpItem1.m_flowSet && !m_runA)
                                {
                                    if (WritePumpOnA())
                                    {
                                        m_runA = true;
                                    }
                                }
                                else if (DlyBase.DOUBLE > m_pumpItem1.m_flowSet && m_runA)
                                {
                                    if (WritePumpOffA())
                                    {
                                        m_runA = false;
                                    }
                                }
                            }

                            if (m_pumpItem2.MVisible)
                            {
                                if (0 < m_pumpItem2.m_flowSet && !m_runB)
                                {
                                    if (WritePumpOnB())
                                    {
                                        m_runB = true;
                                    }
                                }
                                else if (DlyBase.DOUBLE > m_pumpItem2.m_flowSet && m_runB)
                                {
                                    if (WritePumpOffB())
                                    {
                                        m_runB = false;
                                    }
                                }
                            }

                            WriteFlowA(m_pumpItem1.m_pause ? 0 : m_pumpItem1.m_flowSet, m_pumpItem1.m_flowGet);
                            m_pumpItem1.m_flowGet = m_pumpItem1.m_pause ? 0 : m_pumpItem1.m_flowSet;

                            WriteFlowB(m_pumpItem2.m_pause ? 0 : m_pumpItem2.m_flowSet, m_pumpItem2.m_flowGet);
                            m_pumpItem2.m_flowGet = m_pumpItem2.m_pause ? 0 : m_pumpItem2.m_flowSet;
                        }
                        else
                        {
                            Close();

                            for (int i = 0; i < c_timeout; i++)
                            {
                                if (PUMPState.ReadWrite != m_state)
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
                    case PUMPState.Abort:
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
                m_WriteByte[0] = 0x01;
                m_WriteByte[1] = 0x03;
                m_WriteByte[2] = 0x03;
                m_WriteByte[3] = 0xE8;
                m_WriteByte[4] = 0x00;
                m_WriteByte[5] = 0x02;
                byte[] crc = CRC.CRCLen(m_WriteByte, 6);
                m_WriteByte[6] = crc[0];//CRC校验
                m_WriteByte[7] = crc[1];

                if (!write(8) || !read())
                {
                    return false;
                }

                if (0x01 == m_ReadByte[0] && 0x03 == m_ReadByte[1])
                {
                    version = Encoding.Default.GetString(m_ReadByte, 3, 4);
                    return true;
                }

                return true;
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
            return true;
        }

        /// <summary>
        /// 读Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override bool ReadModel(ref string model)
        {
            model = ENUMPumpID.HB0030.ToString();

            try
            {
                m_WriteByte[0] = 0x01;
                m_WriteByte[1] = 0x03;
                m_WriteByte[2] = 0x03;
                m_WriteByte[3] = 0xE8;
                m_WriteByte[4] = 0x00;
                m_WriteByte[5] = 0x02;
                byte[] crc = CRC.CRCLen(m_WriteByte, 6);
                m_WriteByte[6] = crc[0];//CRC校验
                m_WriteByte[7] = crc[1];

                if (!write(8) || !read())
                {
                    return false;
                }

                if (0x01 == m_ReadByte[0] && 0x03 == m_ReadByte[1])
                {
                    int flow = ToUInt16(m_ReadByte, 3);
                    switch (flow)
                    {
                        case 30:
                            model = ENUMPumpID.HB0030.ToString();
                            break;
                    }
                    return true;
                }
            }
            catch
            { }

            return false;
        }


        /// <summary>
        /// 泵开
        /// </summary>
        /// <returns></returns>
        private bool WritePumpOnA()
        {
            bool result = false;

            try
            {
                m_WriteByte[0] = 0x01;
                m_WriteByte[1] = 0x05;

                m_WriteByte[2] = 0x00;
                m_WriteByte[3] = 0x00;

                m_WriteByte[4] = 0xFF;
                m_WriteByte[5] = 0x00;
                byte[] crc = CRC.CRCLen(m_WriteByte, 6);
                m_WriteByte[6] = crc[0];//CRC校验
                m_WriteByte[7] = crc[1];

                if (!write(8, DlyBase.c_sleep2) || !read())
                {
                    return false;
                }
            }
            catch
            { }

            return result;
        }

        /// <summary>
        /// 泵关
        /// </summary>
        /// <returns></returns>
        private bool WritePumpOffA()
        {
            bool result = false;

            try
            {
                m_WriteByte[0] = 0x01;
                m_WriteByte[1] = 0x05;

                m_WriteByte[2] = 0x00;
                m_WriteByte[3] = 0x00;

                m_WriteByte[4] = 0x00;
                m_WriteByte[5] = 0x00;
                byte[] crc = CRC.CRCLen(m_WriteByte, 6);
                m_WriteByte[6] = crc[0];//CRC校验
                m_WriteByte[7] = crc[1];

                if (!write(8, DlyBase.c_sleep2) || !read())
                {
                    return false;
                }
            }
            catch
            { }

            return result;
        }

        /// <summary>
        /// 泵开
        /// </summary>
        /// <returns></returns>
        private bool WritePumpOnB()
        {
            bool result = false;

            try
            {
                m_WriteByte[0] = 0x01;
                m_WriteByte[1] = 0x05;

                m_WriteByte[2] = 0x00;
                m_WriteByte[3] = 0x04;

                m_WriteByte[4] = 0xFF;
                m_WriteByte[5] = 0x00;
                byte[] crc = CRC.CRCLen(m_WriteByte, 6);
                m_WriteByte[6] = crc[0];//CRC校验
                m_WriteByte[7] = crc[1];

                if (!write(8, DlyBase.c_sleep2) || !read())
                {
                    return false;
                }
            }
            catch
            { }

            return result;
        }

        /// <summary>
        /// 泵关
        /// </summary>
        /// <returns></returns>
        private bool WritePumpOffB()
        {
            bool result = false;

            try
            {
                m_WriteByte[0] = 0x01;
                m_WriteByte[1] = 0x05;

                m_WriteByte[2] = 0x00;
                m_WriteByte[3] = 0x04;

                m_WriteByte[4] = 0x00;
                m_WriteByte[5] = 0x00;
                byte[] crc = CRC.CRCLen(m_WriteByte, 6);
                m_WriteByte[6] = crc[0];//CRC校验
                m_WriteByte[7] = crc[1];

                if (!write(8, DlyBase.c_sleep2) || !read())
                {
                    return false;
                }
            }
            catch
            { }

            return result;
        }

        /// <summary>
        /// 【写】泵A流速设置
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        public bool WriteFlowA(double flow, double flowOld)
        {
            try
            {
                if (flow == flowOld)
                {
                    return true;
                }

                //串口通信                  
                m_WriteByte[0] = 0x01;//从机地址
                m_WriteByte[1] = 0x10;//功能码

                m_WriteByte[2] = 0x00;//起始地址
                m_WriteByte[3] = 0xC8;

                m_WriteByte[4] = 0x00;//数量和字节数
                m_WriteByte[5] = 0x02;
                m_WriteByte[6] = 0x04;

                //内容
                try//判断输入的字符串是否是数字
                {
                    int intTen = (int)(flow * 1000); //向数组bytes里添加
                    BitConverter.GetBytes(intTen);
                    int i = 10;
                    foreach (byte b in BitConverter.GetBytes(intTen))
                    {
                        m_WriteByte[i] = b;
                        i--;
                    }
                }
                catch
                { }

                //crc
                byte[] crc = CRC.CRCLen(m_WriteByte, 11);
                m_WriteByte[11] = crc[0];//CRC校验
                m_WriteByte[12] = crc[1];

                if (!write(13) || !read())
                {
                    return false;
                }

                return true;
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 【写】泵A流速设置
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        public bool WriteFlowB(double flow, double flowOld)
        {
            try
            {
                if (flow == flowOld)
                {
                    return true;
                }

                //串口通信                  
                m_WriteByte[0] = 0x01;//从机地址
                m_WriteByte[1] = 0x10;//功能码

                m_WriteByte[2] = 0x00;//起始地址
                m_WriteByte[3] = 0xCA;

                m_WriteByte[4] = 0x00;//数量和字节数
                m_WriteByte[5] = 0x02;
                m_WriteByte[6] = 0x04;

                //内容
                try//判断输入的字符串是否是数字
                {
                    int intTen = (int)(flow * 1000); //向数组bytes里添加
                    BitConverter.GetBytes(intTen);
                    int i = 10;
                    foreach (byte b in BitConverter.GetBytes(intTen))
                    {
                        m_WriteByte[i] = b;
                        i--;
                    }
                }
                catch
                { }

                //crc
                byte[] crc = CRC.CRCLen(m_WriteByte, 11);
                m_WriteByte[11] = crc[0];//CRC校验
                m_WriteByte[12] = crc[1];

                if (!write(13) || !read())
                {
                    return false;
                }

                return true;
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 【读】泵A运行标志位
        /// </summary>
        /// <param name="runState"></param>
        /// <returns></returns>
        public bool ReadStatusA(ref bool runState)
        {
            try
            {
                m_WriteByte[0] = 0x01;
                m_WriteByte[1] = 0x01;
                m_WriteByte[2] = 0x00;
                m_WriteByte[3] = 0x00;
                m_WriteByte[4] = 0x00;
                m_WriteByte[5] = 0x01;
                m_WriteByte[6] = 0xFD;
                m_WriteByte[7] = 0xCA;

                if (!write(8) || !read())
                {
                    return false;
                }

                if (m_ReadByte[3] == 0)
                {
                    runState = false;
                    return true;
                }
                else if (m_ReadByte[3] == 1)
                {
                    runState = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 【读】泵A运行标志位
        /// </summary>
        /// <param name="runState"></param>
        /// <returns></returns>
        public bool ReadStatusB(ref bool runState)
        {
            try
            {
                m_WriteByte[0] = 0x01;
                m_WriteByte[1] = 0x01;
                m_WriteByte[2] = 0x00;
                m_WriteByte[3] = 0x04;
                m_WriteByte[4] = 0x00;
                m_WriteByte[5] = 0x01;
                byte[] crc = CRC.CRCLen(m_WriteByte, 6);
                m_WriteByte[6] = crc[0];//CRC校验
                m_WriteByte[7] = crc[1];

                if (!write(8) || !read())
                {
                    return false;
                }

                if (m_ReadByte[3] == 0)
                {
                    runState = false;
                    return true;
                }
                else if (m_ReadByte[3] == 1)
                {
                    runState = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 【读】泵A压力
        /// </summary>
        /// <param name="pressureA"></param>
        /// <returns></returns>
        public bool ReadPressA(ref double pressure)
        {
            try
            {
                m_WriteByte[0] = 0x01;
                m_WriteByte[1] = 0x04;
                m_WriteByte[2] = 0x00;
                m_WriteByte[3] = 0x06;
                m_WriteByte[4] = 0x00;
                m_WriteByte[5] = 0x02;
                byte[] crc = CRC.CRCLen(m_WriteByte, 6);
                m_WriteByte[6] = crc[0];//CRC校验
                m_WriteByte[7] = crc[1];

                if (!write(8) || !read())
                {
                    return false;
                }

                pressure = ToSingle(m_ReadByte, 3);
                return true;
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 【读】泵A压力
        /// </summary>
        /// <param name="pressureA"></param>
        /// <returns></returns>
        public bool ReadPressB(ref double pressure)
        {
            try
            {
                m_WriteByte[0] = 0x01;
                m_WriteByte[1] = 0x04;
                m_WriteByte[2] = 0x00;
                m_WriteByte[3] = 0x08;
                m_WriteByte[4] = 0x00;
                m_WriteByte[5] = 0x02;
                byte[] crc = CRC.CRCLen(m_WriteByte, 6);
                m_WriteByte[6] = crc[0];//CRC校验
                m_WriteByte[7] = crc[1];

                if (!write(8) || !read())
                {
                    return false;
                }

                pressure = ToSingle(m_ReadByte, 3);
                return true;
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 字节数组转浮点数
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <param name="b3"></param>
        /// <param name="b4"></param>
        /// <returns></returns>
        private double ToSingle(byte[] value, int startIndex)
        {
            byte[] intBuffer = new byte[4];
            //将byte数组的前后字节的高低位换过来
            intBuffer[0] = value[startIndex + 3];
            intBuffer[1] = value[startIndex + 2];
            intBuffer[2] = value[startIndex + 1];
            intBuffer[3] = value[startIndex];

            return Math.Round(BitConverter.ToSingle(intBuffer, 0), 2);
        }
        private int ToUInt16(byte[] value, int startIndex)
        {
            byte[] intBuffer = new byte[2];
            //将byte数组的前后字节的高低位换过来
            intBuffer[0] = value[startIndex + 1];
            intBuffer[1] = value[startIndex];

            return BitConverter.ToUInt16(intBuffer, 0);
        }
    }
}
