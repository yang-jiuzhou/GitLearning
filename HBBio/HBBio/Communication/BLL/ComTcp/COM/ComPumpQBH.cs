using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class ComPumpQBH : ComPump
    {
        private PumpItem m_pumpItem = new PumpItem();
        private PTItem m_ptItem = new PTItem();
        private int MID
        {
            get
            {
                switch (m_id)
                {
                    case ENUMPumpID.NP7005: return 11;
                    case ENUMPumpID.NP7010: return 20;
                    case ENUMPumpID.NP7030: return 21;
                    case ENUMPumpID.NP7060: return 22;
                    case ENUMPumpID.P1001L: return 23;
                    case ENUMPumpID.P1003L: return 24;
                    default: return 10;
                }
            }
        }
        /// <summary>
        /// 上次读压力的时间
        /// </summary>
        private DateTime m_lastTimeReadPress = DateTime.Now;


        /// <summary>
        /// 构造函数
        /// </summary>
        public ComPumpQBH(ComConf info) : base(info)
        {
            if (0 != m_scInfo.MList.Count)
            {
                m_pumpItem = (PumpItem)m_scInfo.MList[0];
                m_ptItem = (PTItem)m_scInfo.MList[1];

                m_pumpItem.m_maxFlowVol = m_maxFlowVol;
            }
        }


        /// <summary>
        /// 获取运行数据读值
        /// </summary>
        /// <returns></returns>
        public override List<object> GetRunDataValueList()
        {
            List<object> valList = new List<object>();

            if (m_pumpItem.MVisible)
            {
                //泵A关联总流速
                if (m_pumpItem.MConstName.Equals(ENUMPumpName.FITA.ToString()))
                {
                    valList.Add(0.0);
                    valList.Add(0.0);
                }

                //泵S没有百分比
                if (!m_pumpItem.MConstName.Equals(ENUMPumpName.FITS.ToString()))
                {
                    valList.Add(0.0);
                }

                valList.Add(m_pumpItem.m_flowGet);
                valList.Add(m_pumpItem.m_flowGet);
            }
            if (m_ptItem.MVisible)
            {
                valList.Add(m_ptItem.m_pressGet);
                if (m_ptItem.MConstName.Equals(ENUMPTName.PTColumnBack.ToString()))
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

            if (m_pumpItem.MVisible)
            {
                //泵A关联总流速
                if (m_pumpItem.MConstName.Equals(ENUMPumpName.FITA.ToString()))
                {
                    valList.Add(0.0);
                    valList.Add(0.0);
                }

                //泵S没有百分比
                if (!m_pumpItem.MConstName.Equals(ENUMPumpName.FITS.ToString()))
                {
                    valList.Add(0.0);
                }

                valList.Add(m_pumpItem.m_flowSet);
                valList.Add(0.0);
            }
            if (m_ptItem.MVisible)
            {
                valList.Add("N/A");
                if (m_ptItem.MConstName.Equals(ENUMPTName.PTColumnBack.ToString()))
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
                        if (Connect())
                        {
                            WriteSendF(10);
                        }
                        m_state = PUMPState.ReadWrite;
                        break;
                    case PUMPState.ReadWrite:
                        if (Connect() && ReadPress(ref m_ptItem.m_pressGet))
                        {
                            m_communState = ENUMCommunicationState.Success;

                            if (0 < m_pumpItem.m_flowSet && !m_run)
                            {
                                if (WritePumpOn())
                                {
                                    m_run = true;
                                }
                            }
                            else if (DlyBase.DOUBLE > m_pumpItem.m_flowSet && m_run)
                            {
                                if (WritePumpOff())
                                {
                                    m_run = false;
                                }
                            }

                            WriteFlow(m_pumpItem.m_pause ? 0 : m_pumpItem.m_flowSet);
                            m_pumpItem.m_flowGet = m_pumpItem.m_pause ? 0 : m_pumpItem.m_flowSet;
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
                    case PUMPState.MaxPress:
                        WritePressMax(m_ptItem.m_maxSet);
                        m_state = PUMPState.ReadWrite;
                        break;
                    case PUMPState.MinPress:
                        WritePressMin(m_ptItem.m_minSet);
                        m_state = PUMPState.ReadWrite;
                        break;
                    case PUMPState.Zero:
                        WritePumpZero();
                        m_state = PUMPState.ReadWrite;
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
            if (!WriteSendF(0))
            {
                return false;
            }

            bool result = false;

            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = (byte)(48 + MID / 10);
                m_WriteByte[2] = (byte)(48 + MID % 10);
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
				
                if (m_WriteByte[1] == m_ReadByte[1] && m_WriteByte[2] == m_ReadByte[2] && m_WriteByte[4] == m_ReadByte[4] && m_WriteByte[5] == m_ReadByte[5])
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
            ENUMPumpID id = ENUMPumpID.OEM0100;
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
            bool result = false;

            try
            {
                //发送读序列号命令
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0x30;
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
        public bool ReadSerialL(ref string serial)
        {
            bool result = false;

            try
            {
                //发送读序列号命令
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0x30;
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
        public bool ReadID(ref ENUMPumpID id)
        {
            if (!WriteSendF(0))
            {
                return false;
            }

            bool result = false;

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

                if (m_WriteByte[4] == m_ReadByte[4] && m_WriteByte[5] == m_ReadByte[5])
                {
                    int temp = (m_ReadByte[1] - 0x30) * 10 + (m_ReadByte[2] - 0x30);
                    if (10 <= temp && temp <= 33)
                    {
                        switch (temp)
                        {
                            case 10: id = ENUMPumpID.NP7001; break;
                            case 11: id = ENUMPumpID.NP7005; break;
                            case 20: id = ENUMPumpID.NP7010; break;
                            case 21: id = ENUMPumpID.NP7030; break;
                            case 22: id = ENUMPumpID.NP7060; break;
                            case 23: id = ENUMPumpID.P1001L; break;
                            case 24: id = ENUMPumpID.P1003L; break;
                        }
                        m_id = id;
                        result = true;
                    }
                }
            }
            catch
            { }

            return result;
        }

        /// <summary>
        /// 读压力
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private bool ReadPress(ref double val)
        {
            bool result = false;

            try
            {
                if ((DateTime.Now - m_lastTimeReadPress).TotalMilliseconds < 1000)
                {
                    Thread.Sleep(1000 - (int)(DateTime.Now - m_lastTimeReadPress).TotalMilliseconds);
                }

                if (!readLine(0))
                {
                    if (!WriteSendF(10))
                    {
                        m_lastTimeReadPress = DateTime.Now;
                        return false;
                    }
                    else
                    {
                        if (!readLine(0))
                        {
                            m_lastTimeReadPress = DateTime.Now;
                            return false;
                        }
                    }
                }
                m_lastTimeReadPress = DateTime.Now;

                for (int i = 0; i < m_ReadLen - 15; i++)
                {
                    if (0x21 == m_ReadByte[i] && 0x39 == m_ReadByte[i + 4] && 0x30 == m_ReadByte[i + 5] && 0x0A == m_ReadByte[i + 15])
                    {
                        val = (m_ReadByte[i + 8] - 48) * 10 + (m_ReadByte[i + 9] - 48)
                            + (m_ReadByte[i + 10] - 48) * 0.1 + (m_ReadByte[i + 11] - 48) * 0.01;

                        if (val > 42)
                        {
                            val = 0;
                        }

                        break;
                    }
                }
				result = true;
            }
            catch
            { }

            return result;
        }

        /// <summary>
        /// 读设备识别码
        /// </summary>
        /// <returns></returns>
        public bool ReadStatus(ref bool run)
        {
            bool result = false;

            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = (byte)(48 + MID / 10);
                m_WriteByte[2] = (byte)(48 + MID % 10);
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x30;//PFC，读产品ID，01
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

                if (m_WriteByte[4] == m_ReadByte[4] && m_WriteByte[5] == m_ReadByte[5])
                {
                    run = 0x31 == m_ReadByte[6];
                    result = true;
                }
            }
            catch
            { }

            return result;
        }

        /// <summary>
        /// 写流速
        /// </summary>
        /// <param name="val"></param>
        private bool WriteFlow(double val)
        {
            bool result = false;

            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = (byte)(48 + MID / 10);
                m_WriteByte[2] = (byte)(48 + MID % 10);
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x31;
                m_WriteByte[5] = 0x30;
                DoubleToByte(ref m_WriteByte, val);//VALUE
                m_WriteByte[12] = 0x00;//CRC
                m_WriteByte[13] = 0x00;
                m_WriteByte[14] = 0x00;
                m_WriteByte[15] = 0x0A;//ETX
                byte[] mCRC = CRC.Cal12(m_WriteByte);
                m_WriteByte[12] = mCRC[0];
                m_WriteByte[13] = mCRC[1];
                m_WriteByte[14] = mCRC[2];

                if (!write(16) || !read())
                {
                    return false;
                }

                if (0x23 == m_ReadByte[0])
                {
                    result = true;
                }
            }
            catch
            { }

            return result;
        }

        /// <summary>
        /// 写最大压力
        /// </summary>
        /// <param name="val"></param>
        private bool WritePressMax(double val)
        {
            bool result = false;

            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = (byte)(48 + MID / 10);
                m_WriteByte[2] = (byte)(48 + MID % 10);
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x31;
                m_WriteByte[5] = 0x33;
                DoubleToByte(ref m_WriteByte, val);//VALUE
                m_WriteByte[12] = 0x00;//CRC
                m_WriteByte[13] = 0x00;
                m_WriteByte[14] = 0x00;
                m_WriteByte[15] = 0x0A;//ETX
                byte[] mCRC = CRC.Cal12(m_WriteByte);
                m_WriteByte[12] = mCRC[0];
                m_WriteByte[13] = mCRC[1];
                m_WriteByte[14] = mCRC[2];

                if (!write(16) || !read())
                {
                    return false;
                }

                if (0x23 == m_ReadByte[0])
                {
                    result = true;
                }
            }
            catch
            { }

            return result;
        }

        /// <summary>
        /// 写最小压力
        /// </summary>
        /// <param name="val"></param>
        private bool WritePressMin(double val)
        {
            bool result = false;

            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = (byte)(48 + MID / 10);
                m_WriteByte[2] = (byte)(48 + MID % 10);
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x31;
                m_WriteByte[5] = 0x34;
                DoubleToByte(ref m_WriteByte, val);//VALUE
                m_WriteByte[12] = 0x00;//CRC
                m_WriteByte[13] = 0x00;
                m_WriteByte[14] = 0x00;
                m_WriteByte[15] = 0x0A;//ETX
                byte[] mCRC = CRC.Cal12(m_WriteByte);
                m_WriteByte[12] = mCRC[0];
                m_WriteByte[13] = mCRC[1];
                m_WriteByte[14] = mCRC[2];

                if (!write(16) || !read())
                {
                    return false;
                }

                if (0x23 == m_ReadByte[0])
                {
                    result = true;
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
        private bool WritePumpOn()
        {
            bool result = false;

            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = (byte)(48 + MID / 10);
                m_WriteByte[2] = (byte)(48 + MID % 10);
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x31;
                m_WriteByte[5] = 0x35;
                m_WriteByte[6] = 0x20;
                m_WriteByte[7] = 0x20;
                m_WriteByte[8] = 0x20;
                m_WriteByte[9] = 0x20;
                m_WriteByte[10] = 0x20;
                m_WriteByte[11] = 0x20;
                m_WriteByte[12] = 0x00;//CRC
                m_WriteByte[13] = 0x00;
                m_WriteByte[14] = 0x00;
                m_WriteByte[15] = 0x0A;//ETX
                byte[] mCRC = CRC.Cal12(m_WriteByte);
                m_WriteByte[12] = mCRC[0];
                m_WriteByte[13] = mCRC[1];
                m_WriteByte[14] = mCRC[2];

                if (!write(16) || !read())
                {
                    return false;
                }

                for (int i = 0; i < m_ReadLen; i += 16)
                {
                    if (0x23 == m_ReadByte[i])
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
        /// 泵关
        /// </summary>
        /// <returns></returns>
        private bool WritePumpOff()
        {
            bool result = false;

            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = (byte)(48 + MID / 10);
                m_WriteByte[2] = (byte)(48 + MID % 10);
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x31;
                m_WriteByte[5] = 0x36;
                m_WriteByte[6] = 0x20;//VALUE
                m_WriteByte[7] = 0x20;
                m_WriteByte[8] = 0x20;
                m_WriteByte[9] = 0x20;
                m_WriteByte[10] = 0x20;
                m_WriteByte[11] = 0x20;
                m_WriteByte[12] = 0x00;//CRC
                m_WriteByte[13] = 0x00;
                m_WriteByte[14] = 0x00;
                m_WriteByte[15] = 0x0A;//ETX
                byte[] mCRC = CRC.Cal12(m_WriteByte);
                m_WriteByte[12] = mCRC[0];
                m_WriteByte[13] = mCRC[1];
                m_WriteByte[14] = mCRC[2];

                if (!write(16) || !read())
                {
                    return false;
                }

                for (int i = 0; i < m_ReadLen; i += 16)
                {
                    if (0x23 == m_ReadByte[i])
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
        /// 泵清零
        /// </summary>
        /// <returns></returns>
        private bool WritePumpZero()
        {
            bool result = false;

            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = (byte)(48 + MID / 10);
                m_WriteByte[2] = (byte)(48 + MID % 10);
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x31;
                m_WriteByte[5] = 0x37;
                m_WriteByte[6] = 0x20;//VALUE
                m_WriteByte[7] = 0x20;
                m_WriteByte[8] = 0x20;
                m_WriteByte[9] = 0x20;
                m_WriteByte[10] = 0x20;
                m_WriteByte[11] = 0x20;
                m_WriteByte[12] = 0x00;//CRC
                m_WriteByte[13] = 0x00;
                m_WriteByte[14] = 0x00;
                m_WriteByte[15] = 0x0A;//ETX
                byte[] mCRC = CRC.Cal12(m_WriteByte);
                m_WriteByte[12] = mCRC[0];
                m_WriteByte[13] = mCRC[1];
                m_WriteByte[14] = mCRC[2];

                if (!write(16) || !read())
                {
                    return false;
                }

                if (0x23 == m_ReadByte[0])
                {
                    result = true;
                }
            }
            catch
            { }

            return result;
        }

        /// <summary>
        /// 写发送频率(0~100)
        /// </summary>
        /// <param name="mSendF"></param>
        /// <returns></returns>
        private bool WriteSendF(int val)
        {
            try
            {
                if (val < 0 || val > 100)
                {
                    val = 10;
                }

                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = (byte)(48 + MID / 10);
                m_WriteByte[2] = (byte)(48 + MID % 10);
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x31;
                m_WriteByte[5] = 0x38;
                m_WriteByte[6] = 0x20;
                m_WriteByte[7] = 0x20;
                m_WriteByte[8] = 0x20;
                m_WriteByte[9] = (byte)(48 + val / 100);
                m_WriteByte[10] = (byte)(48 + val / 10);
                m_WriteByte[11] = (byte)(48 + val % 10);
                byte[] mCRC = CRC.Cal12(m_WriteByte);
                m_WriteByte[12] = mCRC[0];
                m_WriteByte[13] = mCRC[1];
                m_WriteByte[14] = mCRC[2];
                m_WriteByte[15] = 0x0A;//ETX
                
                if (!write(16))
				{
					return false;
				}

				int index = 0;
                while (read() && index++ < 5)
                {
                    string str = System.Text.Encoding.ASCII.GetString(m_ReadByte, 0, m_ReadLen);
                    if (str.Contains('#') || str.Contains('$'))
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
        /// double数值转换为6字节
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private void DoubleToByte(ref byte[] byteArr, double value)
        {
            int mValueFrom = (int)(value * 100);

            if (ENUMPumpID.P1001L <= m_id) //1000ml泵 * 10，10ml泵 * 1000，其他泵 * 100
            {
                mValueFrom = (int)(value * 10);
            }
            else if (ENUMPumpID.NP7001 >= m_id)
            {
                mValueFrom = (int)(value * 1000);
            }

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
