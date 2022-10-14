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
    class ComUVQBH2 : ComUV
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ComUVQBH2(ComConf info) : base(info)
        {
            m_ReadByte = new byte[512];                //读数据
        }

        /// <summary>
        /// 线程主函数
        /// </summary>
        protected override void ThreadRun()
        {
            bool lamp = false;
            double time = 0;
            int refVal = 0;
            int sigVal = 0;
            double[] abs = new double[4];

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
                        if (Connect() && ReadAu(m_item.MLamp, ref abs[0], ref abs[1]))
                        {
                            m_communState = ENUMCommunicationState.Success;

                            m_item.UpdateAbs(abs);
                            bool sendF = true;
                            if (0 == uvNum % 10 || m_item.m_wave || m_item.m_clear || m_item.m_lampOn || m_item.m_lampOff)
                            {
                                SetSendF(false);
                                sendF = false;
                            }

                            if (0 == uvNum % 10)
                            {
                                lamp = m_item.MLamp;
                                if (ReadLamp(ref lamp))
                                {
                                    m_item.MLamp = lamp;
                                }

                                if (0 == uvNum % 60)
                                {
                                    time = m_item.MTime;
                                    if (ReadLightTime(ref time))
                                    {
                                        m_item.MTime = time;
                                    }

                                    refVal = m_item.MRef;
                                    if (ReadRef(ref refVal))
                                    {
                                        m_item.MRef = refVal;
                                    }

                                    sigVal = m_item.MSig;
                                    if (ReadSig(ref sigVal))
                                    {
                                        m_item.MSig = sigVal;
                                    }
                                }
                            }

                            if (m_item.m_wave)
                            {
                                m_item.m_wave = false;
                                WriteWave(m_item.m_waveSet[0], m_item.m_waveSet[1]);
                                ReadWave(ref m_item.m_waveGet[0], ref m_item.m_waveGet[1]);
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

                            if (!sendF)
                            {
                                SetSendF(true);
                            }

                            uvNum++;

                            Thread.Sleep(DlyBase.c_sleep5);
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
                        if (Connect() && SetSendF(false))
                        {
                            m_communState = ENUMCommunicationState.Success;

                            ReadWave(ref m_item.m_waveGet[0], ref m_item.m_waveGet[1]);
                            lamp = m_item.MLamp;
                            if (ReadLamp(ref lamp))
                            {
                                m_item.MLamp = lamp;
                            }
                            time = m_item.MTime;
                            if (ReadLightTime(ref time))
                            {
                                m_item.MTime = time;
                            }
                            refVal = m_item.MRef;
                            if (ReadRef(ref refVal))
                            {
                                m_item.MRef = refVal;
                            }
                            sigVal = m_item.MSig;
                            if (ReadSig(ref sigVal))
                            {
                                m_item.MSig = sigVal;
                            }
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
            SetSendF(false);

            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0x35;//ID，双波长紫外检测器，51
                m_WriteByte[2] = 0x31;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x30;//PFC，读产品版本，06
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

                if (m_WriteByte[1] == m_ReadByte[1] && m_WriteByte[2] == m_ReadByte[2])
                {
                    if (0x30 == m_ReadByte[4] && 0x36 == m_ReadByte[5])
                    {
                        //解析ID,接收读命令返回成功，正确返回20 20 20 20 35 31
                        version = Encoding.ASCII.GetString(m_ReadByte, 6, 6);
                    }
                    else if (0x39 == m_ReadByte[4] && 0x30 == m_ReadByte[5])
                    {
                        version = "";
                    }

                    return true;
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
            SetSendF(false);

            bool result = false;

            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0x35;//ID，双波长紫外检测器，51
                m_WriteByte[2] = 0x31;
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
                    if (0x30 == m_ReadByte[4] && 0x31 == m_ReadByte[5])
                    {
                        //解析ID,接收读命令返回成功，正确返回20 20 20 20 35 31
                        if (Encoding.ASCII.GetString(m_ReadByte, 6, 6).Contains("51"))
                        {
                            model = ENUMDetectorID.UVQBH2.ToString();
                        }
                    }

                    result = true;
                }
            }
            catch
            { }

            return result;
        }

        /// <summary>
        /// 读高位序列号
        /// </summary>
        /// <returns></returns>
        public bool ReadSerialH(ref string serial)
        {
            SetSendF(false);

            bool result = false;

            try
            {
                //发送读序列号命令
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0x35;//ID，双波长紫外检测器，51
                m_WriteByte[2] = 0x31;
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
            SetSendF(false);

            bool result = false;

            try
            {
                //发送读序列号命令
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0x35;//ID，双波长紫外检测器，51
                m_WriteByte[2] = 0x31;
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
        /// 读吸收值
        /// </summary>
        /// <returns></returns>
        public bool ReadAu(bool onoff, ref double uv1, ref double uv2)
        {
            try
            {
                if (!read())
                {
                    SetSendF(true);
                    if (onoff)
                    {
                        if (!read(DlyBase.c_sleep5))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        uv1 = 0;
                        uv2 = 0;
                        Thread.Sleep(DlyBase.c_sleep50);
                        return true;
                    }
                }

                bool readAbs2 = false;
                string[] list = Encoding.ASCII.GetString(m_ReadByte).Split('\n');
                for (int i = list.Length - 2; i > list.Length - 4 && i > -1; i--)
                {
                    if (6 <= list[i].Length && '9' == list[i][4] && '0' == list[i][5])
                    {
                        string mValueStr = list[i].Substring(6, 6);
                        Int64 mTemp = Convert.ToInt64(mValueStr, 16);
                        if (mTemp >= 0x800000)
                        {
                            mTemp = ~(0x1000000 - mTemp);
                        }
                        else if (mTemp == 0x7fffff)
                        {
                            mTemp = 0;
                        }
                        else
                        { }

                        double mValue = Convert.ToDouble(mTemp) / 1000.0;
                        switch (list[i][3])//获取通道号
                        {
                            case '0'://通道1
                                uv1 = onoff ? Math.Round(mValue, 2) : 0;
                                break;
                            case '1'://通道2
                                uv2 = onoff ? Math.Round(mValue, 2) : 0;
                                readAbs2 = true;
                                break;
                        }
                    }
                }

                if (!readAbs2 && 2 < list.Length)
                {
                    uv2 = 0;
                }
            }
            catch
            { }

            return true;
        }

        /// <summary>
        /// 读波长
        /// </summary>
        /// <returns></returns>
        public bool ReadWave(ref int wave1, ref int wave2)
        {
            try
            {
                SetSendF(false);

                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0x35;//ID，双波长紫外检测器，51
                m_WriteByte[2] = 0x31;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x30;//PFC，读波长，09
                m_WriteByte[5] = 0x39;
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

                if (0x30 == m_ReadByte[4] && 0x39 == m_ReadByte[5])
                {
                    wave2 = MByteToInt(m_ReadByte[6], m_ReadByte[7], m_ReadByte[8]);
                    wave1 = MByteToInt(m_ReadByte[9], m_ReadByte[10], m_ReadByte[11]);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 读亮灯时间
        /// </summary>
        /// <returns></returns>
        public bool ReadLightTime(ref double time)
        {
            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0x35;//ID，双波长紫外检测器，51
                m_WriteByte[2] = 0x31;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x37;//PFC，读灯，70
                m_WriteByte[5] = 0x30;
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

                if (0x37 == m_ReadByte[4] && 0x30 == m_ReadByte[5])
                {
                    time = Convert.ToInt32(Encoding.ASCII.GetString(m_ReadByte,6,6));
                }
            }
            catch
            { }

            return true;
        }

        /// <summary>
        /// 读灯状态
        /// </summary>
        /// <returns></returns>
        public bool ReadLamp(ref bool on)
        {
            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0x35;//ID，双波长紫外检测器，51
                m_WriteByte[2] = 0x31;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x30;//PFC，读灯，04
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

                if (0x30 == m_ReadByte[4] && 0x34 == m_ReadByte[5])
                {
                    on = 0x31 == m_ReadByte[7];
                }
            }
            catch
            { }

            return true;
        }

        /// <summary>
        /// 参比能量
        /// </summary>
        /// <returns></returns>
        public bool ReadRef(ref int refVal)
        {
            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0x35;//ID，双波长紫外检测器，51
                m_WriteByte[2] = 0x31;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x30;//PFC
                m_WriteByte[5] = 0x37;
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

                if (0x30 == m_ReadByte[4] && 0x37 == m_ReadByte[5])
                {
                    refVal = Convert.ToInt32(Encoding.ASCII.GetString(m_ReadByte, 6, 6));
                }
            }
            catch
            { }

            return true;
        }

        /// <summary>
        /// 样比能量
        /// </summary>
        /// <returns></returns>
        public bool ReadSig(ref int sigVal)
        {
            try
            {
                m_WriteByte = new byte[16];
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0x35;//ID，双波长紫外检测器，51
                m_WriteByte[2] = 0x31;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x30;//PFC
                m_WriteByte[5] = 0x38;
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

                if (0x30 == m_ReadByte[4] && 0x38 == m_ReadByte[5])
                {
                    sigVal = Convert.ToInt32(Encoding.ASCII.GetString(m_ReadByte, 6, 6));
                }
            }
            catch
            { }

            return true;
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
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0x35;//ID，双波长紫外检测器，51
                m_WriteByte[2] = 0x31;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x31;//PFC，设定波长，10
                m_WriteByte[5] = 0x30;
                m_WriteByte[6] = (byte)(48 + Wave2 / 100);//VALUE
                m_WriteByte[7] = (byte)(48 + (Wave2 % 100) / 10);
                m_WriteByte[8] = (byte)(48 + Wave2 % 10);
                m_WriteByte[9] = (byte)(48 + Wave1 / 100);
                m_WriteByte[10] = (byte)(48 + (Wave1 % 100) / 10);
                m_WriteByte[11] = (byte)(48 + Wave1 % 10);
                byte[] mCRC = CRC.Cal12(m_WriteByte);//CRC校验
                m_WriteByte[12] = mCRC[0];
                m_WriteByte[13] = mCRC[1];
                m_WriteByte[14] = mCRC[2];
                m_WriteByte[15] = 0x0A;//ETX

                if (!write(16) || !read())
                {
                    return false;
                }
            }
            catch
            { }

            return 0x23 == m_ReadByte[0];
        }

        /// <summary>
        /// 开灯
        /// </summary>
        /// <returns></returns>
        public bool WriteLampOn()
        {
            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0x35;//ID，双波长紫外检测器，51
                m_WriteByte[2] = 0x31;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x31;//PFC，开灯，14
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
            }
            catch
            { }

            return 0x23 == m_ReadByte[0];
        }

        /// <summary>
        /// 关灯
        /// </summary>
        /// <returns></returns>
        public bool WriteLampOff()
        {
            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0x35;//ID，双波长紫外检测器，51
                m_WriteByte[2] = 0x31;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x31;//PFC，开灯，15
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
            }
            catch
            { }

            return 0x23 == m_ReadByte[0];
        }

        /// <summary>
        /// 清零
        /// </summary>
        /// <returns></returns>
        public bool WriteZero()
        {
            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0x35;//ID，双波长紫外检测器，51
                m_WriteByte[2] = 0x31;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x31;//PFC，清零，17
                m_WriteByte[5] = 0x37;
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
            }
            catch
            { }

            return 0x23 == m_ReadByte[0];
        }

        /// <summary>
        /// 设定吸收值发送频率
        /// </summary>
        /// <param name="mSendF"></param>
        /// <returns></returns>
        public bool SetSendF(bool on)
        {
            try
            {
                m_WriteByte[0] = 0x21;//STX，！
                m_WriteByte[1] = 0x35;//ID，双波长紫外检测器，51
                m_WriteByte[2] = 0x31;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x31;//PFC，设定Au值发送频率，18
                m_WriteByte[5] = 0x38;
                m_WriteByte[6] = 0x30;//VALUE
                m_WriteByte[7] = 0x30;
                m_WriteByte[8] = 0x30;
                m_WriteByte[9] = 0x30;
                m_WriteByte[10] = 0x30;
                m_WriteByte[11] = (byte)(on ? 0x31 : 0x30);
                byte[] mCRC = CRC.Cal12(m_WriteByte);//CRC校验
                m_WriteByte[12] = mCRC[0];
                m_WriteByte[13] = mCRC[1];
                m_WriteByte[14] = mCRC[2];
                m_WriteByte[15] = 0x0A;//ETX

                if (!write(16))
                {
                    return false;
                }

                int index = 0;
                while (read() && index++ < 10)
                {
                    string str = System.Text.Encoding.ASCII.GetString(m_ReadByte);
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
