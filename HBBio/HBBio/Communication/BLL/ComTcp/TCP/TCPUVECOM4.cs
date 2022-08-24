using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class TCPUVECOM4 : TCPUV
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TCPUVECOM4(ComConf info) : base(info)
        {
        }

        /// <summary>
        /// 线程主函数
        /// </summary>
        protected override void ThreadRun()
        {
            bool lamp = false;
            double time = 0;
            double[] abs = new double[4];

            int uvNum = 0;
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
                        if (Connect() && ReadAu(ref abs[0], ref abs[1], ref abs[2], ref abs[3]))
                        {
                            m_communState = ENUMCommunicationState.Success;

                            m_item.UpdateAbs(abs);
                            if (0 == uvNum % 10)
                            {
                                lamp = m_item.MLamp;
                                if (ReadLamp(ref lamp))
                                {
                                    m_item.MLamp = lamp;
                                }

                                if (0 == uvNum % 60)
                                {
                                    uvNum = 1;
                                    time = m_item.MTime;
                                    if (ReadLightTime(ref time))
                                    {
                                        m_item.MTime = time;
                                    }
                                }
                            }

                            if (m_item.m_wave)
                            {
                                m_item.m_wave = false;
                                WriteWave(m_item.m_waveSet[0], m_item.m_waveSet[1], m_item.m_waveSet[2], m_item.m_waveSet[3]);
                                ReadWave(ref m_item.m_waveGet[0], ref m_item.m_waveGet[1], ref m_item.m_waveGet[2], ref m_item.m_waveGet[3]);
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
                        if (Connect())
                        {
                            ReadWave(ref m_item.m_waveGet[0], ref m_item.m_waveGet[1], ref m_item.m_waveGet[2], ref m_item.m_waveGet[3]);
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
                m_WriteByte[0] = 0x23;
                m_WriteByte[1] = 0x53;
                m_WriteByte[2] = 0x4E;
                m_WriteByte[3] = 0x72;
                m_WriteByte[4] = 0x0A;

                if (!write(5) || !read())
                {
                    return false;
                }

                version = Encoding.ASCII.GetString(m_ReadByte, 0, m_ReadLen).Replace("\n", "");
                if (version.Contains("SNr"))
                {
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
            return true;
        }

        /// <summary>
        /// 读Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override bool ReadModel(ref string model)
        {
            model = ENUMDetectorID.UVECOM4.ToString();
            return true;
        }

        /// <summary>
        /// 读吸收值
        /// </summary>
        /// <returns></returns>
        public bool ReadAu(ref double mAB1, ref double mAB2, ref double mAB3, ref double mAB4)
        {
            //#ABr<LF>
            //ABrA+001023:0B+003025:0C+045023:0D+004111:0<LF>
            try
            {
                m_WriteByte[0] = 0x23;
                m_WriteByte[1] = 0x41;
                m_WriteByte[2] = 0x42;
                m_WriteByte[3] = 0x72;
                m_WriteByte[4] = 0x0A;

                if (!write(5) || !read())
                {
                    return false;
                }

                //解析
                string str = Encoding.ASCII.GetString(m_ReadByte);
                if (str.Contains("ERR"))
                {
                    mAB1 = 0;
                    mAB2 = 0;
                    mAB3 = 0;
                    mAB4 = 0;
                }
                else
                {
                    //ABrA+001023:0B+003025:0C+045023:0D+004111:0<LF>
                    if (str.Substring(3, 1).Equals("A") && str.Substring(12, 1).Equals("0"))//A通道，无错误
                    {
                        string str1 = str.Substring(5, 4);
                        double mf1 = Convert.ToInt16(str1);//整数部分
                        string str2 = str.Substring(9, 2);
                        double mf2 = Convert.ToInt16(str2);//小数部分
                        double mf3 = mf1 + mf2 / 100F;
                        if (str.Substring(4, 1).Equals("+"))//符号位
                        {
                            mAB1 = mf3;
                        }
                        else if (str.Substring(4, 1).Equals("-"))
                        {
                            mAB1 = mf3 * (-1F);
                        }
                    }
                    //ABrA+001023:0B+003025:0C+045023:0D+004111:0<LF>
                    if (str.Substring(13, 1).Equals("B") && str.Substring(22, 1).Equals("0"))//B通道，无错误
                    {
                        string str1 = str.Substring(15, 4);
                        double mf1 = Convert.ToInt16(str1);//整数部分
                        string str2 = str.Substring(19, 2);
                        double mf2 = Convert.ToInt16(str2);//小数部分
                        double mf3 = mf1 + mf2 / 100F;
                        if (str.Substring(14, 1).Equals("+"))//符号位
                        {
                            mAB2 = mf3;
                        }
                        else if (str.Substring(14, 1).Equals("-"))
                        {
                            mAB2 = mf3 * (-1F);
                        }
                    }
                    //ABrA+001023:0B+003025:0C+045023:0D+004111:0<LF>
                    if (str.Substring(23, 1).Equals("C") && str.Substring(32, 1).Equals("0"))//C通道，无错误
                    {
                        string str1 = str.Substring(25, 4);
                        double mf1 = Convert.ToInt16(str1);//整数部分
                        string str2 = str.Substring(29, 2);
                        double mf2 = Convert.ToInt16(str2);//小数部分
                        double mf3 = mf1 + mf2 / 100F;
                        if (str.Substring(24, 1).Equals("+"))//符号位
                        {
                            mAB3 = mf3;
                        }
                        else if (str.Substring(24, 1).Equals("-"))
                        {
                            mAB3 = mf3 * (-1F);
                        }
                    }
                    //ABrA+001023:0B+003025:0C+045023:0D+004111:0<LF>
                    if (str.Substring(33, 1).Equals("D") && str.Substring(42, 1).Equals("0"))//D通道，无错误
                    {
                        string str1 = str.Substring(35, 4);
                        double mf1 = Convert.ToInt16(str1);//整数部分
                        string str2 = str.Substring(39, 2);
                        double mf2 = Convert.ToInt16(str2);//小数部分
                        double mf3 = mf1 + mf2 / 100F;
                        if (str.Substring(34, 1).Equals("+"))//符号位
                        {
                            mAB4 = mf3;
                        }
                        else if (str.Substring(34, 1).Equals("-"))
                        {
                            mAB4 = mf3 * (-1F);
                        }
                    }
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
        public bool ReadWave(ref int wave1, ref int wave2, ref int wave3, ref int wave4)
        {
            //#WLr<LF>
            //WLrA200B400C600D800<LF>            
            try
            {
                m_WriteByte[0] = 0x23;
                m_WriteByte[1] = 0x57;
                m_WriteByte[2] = 0x4C;
                m_WriteByte[3] = 0x72;
                m_WriteByte[4] = 0x0A;

                if (!write(5) || !read())
                {
                    return false;
                }

                string str = System.Text.Encoding.ASCII.GetString(m_ReadByte);

                if (str.Substring(3, 1).Equals("A"))//A通道
                {
                    string str1 = str.Substring(4, 3);
                    double mf1 = Convert.ToInt16(str1);
                    wave1 = (int)mf1;
                }
                if (str.Substring(7, 1).Equals("B"))//B通道
                {
                    string str1 = str.Substring(8, 3);
                    double mf1 = Convert.ToInt16(str1);
                    wave2 = (int)mf1;
                }
                if (str.Substring(11, 1).Equals("C"))//C通道
                {
                    string str1 = str.Substring(12, 3);
                    double mf1 = Convert.ToInt16(str1);
                    wave3 = (int)mf1;
                }
                if (str.Substring(15, 1).Equals("D"))//D通道
                {
                    string str1 = str.Substring(16, 3);
                    double mf1 = Convert.ToInt16(str1);
                    wave4 = (int)mf1;
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
                m_WriteByte[0] = 0x23;
                m_WriteByte[1] = 0x4C;
                m_WriteByte[2] = 0x4C;
                m_WriteByte[3] = 0x72;
                m_WriteByte[4] = 0x0A;

                if (!write(5) || !read())
                {
                    return false;
                }

                //解析
                //LLrLnnHnnnnnn<LF> 
                string str = System.Text.Encoding.ASCII.GetString(m_ReadByte);
                string str1 = str.Substring(7, 4);
                double mf1 = Convert.ToInt16(str1);//整数部分
                string str2 = str.Substring(11, 2);
                double mf2 = Convert.ToInt16(str2);//小数部分
                time = mf1 + mf2 / 100F;
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
                m_WriteByte[0] = 0x23;
                m_WriteByte[1] = 0x4C;
                m_WriteByte[2] = 0x50;
                m_WriteByte[3] = 0x72;
                m_WriteByte[4] = 0x0A;

                if (!write(5) || !read())
                {
                    return false;
                }

                //解析
                //LPrT<LF> 
                string str = System.Text.Encoding.ASCII.GetString(m_ReadByte);
                if (str.Substring(3, 1).Equals("T"))//灯开
                {
                    on = true;
                }
                else if (str.Substring(3, 1).Equals("F"))//灯关
                {
                    on = false;
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
        public bool WriteWave(int Wave1, int Wave2, int Wave3, int Wave4)
        {
            //#WLwA198B405C254D580<LF>
            //WLwA198B405C254D580<LF>
            try
            {
                //波长值->ASCII->十六进制
                byte[] b1 = System.Text.Encoding.ASCII.GetBytes(Wave1.ToString());
                byte[] b2 = System.Text.Encoding.ASCII.GetBytes(Wave2.ToString());
                byte[] b3 = System.Text.Encoding.ASCII.GetBytes(Wave3.ToString());
                byte[] b4 = System.Text.Encoding.ASCII.GetBytes(Wave4.ToString());
                //写波长命令
                m_WriteByte[0] = 0x23;//#
                m_WriteByte[1] = 0x57;
                m_WriteByte[2] = 0x4C;
                m_WriteByte[3] = 0x77;
                m_WriteByte[4] = 0x41;//A
                m_WriteByte[5] = b1[0];
                m_WriteByte[6] = b1[1];
                m_WriteByte[7] = b1[2];
                m_WriteByte[8] = 0x42;//B
                m_WriteByte[9] = b2[0];
                m_WriteByte[10] = b2[1];
                m_WriteByte[11] = b2[2];
                m_WriteByte[12] = 0x43;//C
                m_WriteByte[13] = b3[0];
                m_WriteByte[14] = b3[1];
                m_WriteByte[15] = b3[2];
                m_WriteByte[16] = 0x44;//D
                m_WriteByte[17] = b4[0];
                m_WriteByte[18] = b4[1];
                m_WriteByte[19] = b4[2];
                m_WriteByte[20] = 0x0A;

                if (!write(21))
                {
                    return false;
                }
            }
            catch
            { }

            return true;
        }

        /// <summary>
        /// 开灯
        /// </summary>
        /// <returns></returns>
        public bool WriteLampOn()
        {
            //#LPwT<LF>
            //LPwT<LF>
            try
            {
                //开灯命令
                m_WriteByte[0] = 0x23;//#
                m_WriteByte[1] = 0x4C;
                m_WriteByte[2] = 0x50;
                m_WriteByte[3] = 0x77;
                m_WriteByte[4] = 0x54;//T
                m_WriteByte[5] = 0x0A;

                if (!write(6))
                {
                    return false;
                }
            }
            catch
            { }

            return true;
        }

        /// <summary>
        /// 关灯
        /// </summary>
        /// <returns></returns>
        public bool WriteLampOff()
        {
            //#LPwF<LF>
            //LPwF<LF>
            try
            {
                //关灯命令
                m_WriteByte[0] = 0x23;//#
                m_WriteByte[1] = 0x4C;
                m_WriteByte[2] = 0x50;
                m_WriteByte[3] = 0x77;
                m_WriteByte[4] = 0x46;//F
                m_WriteByte[5] = 0x0A;

                if (!write(6))
                {
                    return false;
                }
            }
            catch
            { }

            return true;
        }

        /// <summary>
        /// 清零
        /// </summary>
        /// <returns></returns>
        public bool WriteZero()
        {
            //#ZRw<LF>
            //ZRwT<LF>
            try
            {
                //清零命令
                m_WriteByte[0] = 0x23;//#
                m_WriteByte[1] = 0x5A;
                m_WriteByte[2] = 0x52;
                m_WriteByte[3] = 0x77;
                m_WriteByte[4] = 0x0A;

                if (!write(5))
                {
                    return false;
                }
            }
            catch
            { }

            return true;
        }
    }
}
