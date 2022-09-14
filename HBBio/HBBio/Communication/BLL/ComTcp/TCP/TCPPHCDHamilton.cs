using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class TCPPHCDHamilton : TCPPHCD
    {
        private PHItem m_pHItem1 = new PHItem();        //pH01元素
        private PHItem m_pHItem2 = new PHItem();        //pH02元素
        private CDItem m_CdItem1 = new CDItem();        //Cd01元素
        private CDItem m_CdItem2 = new CDItem();        //Cd02元素
        private TTItem m_ttpHItem1 = new TTItem();      //pH01温度元素
        private TTItem m_ttpHItem2 = new TTItem();      //pH02温度元素
        private TTItem m_ttCdItem1 = new TTItem();      //Cd01温度元素
        private TTItem m_ttCdItem2 = new TTItem();      //Cd02温度元素


        /// <summary>
        /// 构造函数
        /// </summary>
        public TCPPHCDHamilton(ComConf info) : base(info)
        {
            if (0 != m_scInfo.MList.Count)
            {
                m_pHItem1 = (PHItem)m_scInfo.MList[0];
                m_pHItem2 = (PHItem)m_scInfo.MList[1];
                m_CdItem1 = (CDItem)m_scInfo.MList[2];
                m_CdItem2 = (CDItem)m_scInfo.MList[3];
                m_ttpHItem1 = (TTItem)m_scInfo.MList[4];
                m_ttpHItem2 = (TTItem)m_scInfo.MList[5];
                m_ttCdItem1 = (TTItem)m_scInfo.MList[6];
                m_ttCdItem2 = (TTItem)m_scInfo.MList[7];
            }
        }

        /// <summary>
        /// 获取运行数据读值
        /// </summary>
        /// <returns></returns>
        public override List<object> GetRunDataValueList()
        {
            List<object> result = new List<object>();

            if (m_pHItem1.MVisible)
            {
                result.Add(m_pHItem1.m_pHGet);
            }
            if (m_pHItem2.MVisible)
            {
                result.Add(m_pHItem2.m_pHGet);
            }
            if (m_CdItem1.MVisible)
            {
                result.Add(m_CdItem1.m_CdGet);
            }
            if (m_CdItem2.MVisible)
            {
                result.Add(m_CdItem2.m_CdGet);
            }
            if (m_ttpHItem1.MVisible)
            {
                result.Add(m_ttpHItem1.m_tempGet);
            }
            if (m_ttpHItem2.MVisible)
            {
                result.Add(m_ttpHItem2.m_tempGet);
            }
            if (m_ttCdItem1.MVisible)
            {
                result.Add(m_ttCdItem1.m_tempGet);
            }
            if (m_ttCdItem2.MVisible)
            {
                result.Add(m_ttCdItem2.m_tempGet);
            }

            return result;
        }

        /// <summary>
        /// 获取运行数据写值
        /// </summary>
        /// <returns></returns>
        public override List<object> SetRunDataValueList()
        {
            List<object> result = new List<object>();

            if (m_pHItem1.MVisible)
            {
                result.Add("N/A");
            }
            if (m_pHItem2.MVisible)
            {
                result.Add("N/A");
            }
            if (m_CdItem1.MVisible)
            {
                result.Add("N/A");
            }
            if (m_CdItem2.MVisible)
            {
                result.Add("N/A");
            }
            if (m_ttpHItem1.MVisible)
            {
                result.Add("N/A");
            }
            if (m_ttpHItem2.MVisible)
            {
                result.Add("N/A");
            }
            if (m_ttCdItem1.MVisible)
            {
                result.Add("N/A");
            }
            if (m_ttCdItem2.MVisible)
            {
                result.Add("N/A");
            }

            return result;
        }

        /// <summary>
        /// 线程主函数
        /// </summary>
        protected override void ThreadRun()
        {
            int phcdNum = 0;
            bool result = true;

            double cdVal1 = 0;
            double cdVal2 = 0;

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
                        result = true;
                        if (Connect())
                        {
                            if (result && m_pHItem1.MVisible)
                            {
                                result = ReadPHValue1(ref m_pHItem1.m_pHGet);
                            }
                            if (result && m_pHItem2.MVisible)
                            {
                                result = ReadPHValue2(ref m_pHItem2.m_pHGet);
                            }
                            if (result && m_CdItem1.MVisible)
                            {
                                result = ReadCDValue1(ref cdVal1);
                                m_CdItem1.UpdateValue(cdVal1);
                            }
                            if (result && m_CdItem2.MVisible)
                            {
                                result = ReadCDValue2(ref cdVal2);
                                m_CdItem2.UpdateValue(cdVal2);
                            }
                            if (result)
                            {
                                m_communState = ENUMCommunicationState.Success;

                                if (0 == phcdNum % 10)
                                {
                                    if (m_pHItem1.MVisible)
                                    {
                                        ReadPHTempeture1(ref m_ttpHItem1.m_tempGet);
                                    }
                                    if (m_pHItem2.MVisible)
                                    {
                                        ReadPHTempeture2(ref m_ttpHItem2.m_tempGet);
                                    }
                                    if (m_CdItem1.MVisible)
                                    {
                                        ReadCDTempeture1(ref m_ttCdItem1.m_tempGet);
                                    }
                                    if (m_CdItem2.MVisible)
                                    {
                                        ReadCDTempeture2(ref m_ttCdItem2.m_tempGet);
                                    }
                                    if (0 == phcdNum % 60)
                                    {
                                        if (m_pHItem1.MVisible)
                                        {
                                            ReadPHTime1(ref m_pHItem1.m_timeGet);
                                        }
                                        if (m_pHItem2.MVisible)
                                        {
                                            ReadPHTime2(ref m_pHItem2.m_timeGet);
                                        }
                                        if (m_CdItem1.MVisible)
                                        {
                                            ReadCDTime1(ref m_CdItem1.m_timeGet);
                                        }
                                        if (m_CdItem2.MVisible)
                                        {
                                            ReadCDTime2(ref m_CdItem2.m_timeGet);
                                        }
                                    }
                                }

                                Thread.Sleep(DlyBase.c_sleep5);
                            }

                            phcdNum++;
                            if (61 == phcdNum)
                            {
                                phcdNum = 1;
                            }
                        }

                        if (!result)
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
                double val = 0;
                if (ReadPHValue2(ref val) || ReadCDValue1(ref val) || ReadCDValue2(ref val))
                {
                    model = ENUMDetectorID.pHCdHamilton.ToString();
                    return true;
                }
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 读柱前PH值
        /// </summary>
        /// <returns></returns>
        public bool ReadPHValue1(ref double val)
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
                    val = Math.Round(MByteToFloat(m_ReadByte[7], m_ReadByte[8], m_ReadByte[9], m_ReadByte[10]), 2);
                    if (0 > val || 14 < val)
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
        /// 读柱后PH值
        /// </summary>
        /// <returns></returns>
        public bool ReadPHValue2(ref double val)
        {
            try
            {
                m_WriteByte[0] = 0x03;//设备地址
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

                if (0x03 == m_ReadByte[0] && 0x03 == m_ReadByte[1])
                {
                    val = Math.Round(MByteToFloat(m_ReadByte[7], m_ReadByte[8], m_ReadByte[9], m_ReadByte[10]), 2);
                    if (0 > val || 14 < val)
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
        /// 读柱前电导值
        /// </summary>
        /// <returns></returns>
        public bool ReadCDValue1(ref double val)
        {
            try
            {
                m_WriteByte[0] = 0x02;//设备地址
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

                if (0x02 == m_ReadByte[0] && 0x03 == m_ReadByte[1])
                {
                    val = Math.Round(MByteToFloat(m_ReadByte[7], m_ReadByte[8], m_ReadByte[9], m_ReadByte[10]) / 1000, 3);
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
        /// 读柱后电导值
        /// </summary>
        /// <returns></returns>
        public bool ReadCDValue2(ref double val)
        {
            try
            {
                m_WriteByte[0] = 0x04;//设备地址
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

                if (0x04 == m_ReadByte[0] && 0x03 == m_ReadByte[1])
                {
                    val = Math.Round(MByteToFloat(m_ReadByte[7], m_ReadByte[8], m_ReadByte[9], m_ReadByte[10]) / 1000, 3);
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
        public bool ReadPHTempeture1(ref double val)
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
        /// 读温度值
        /// </summary>
        /// <returns></returns>
        public bool ReadPHTempeture2(ref double val)
        {
            try
            {
                m_WriteByte[0] = 0x03;//设备地址
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

                if (0x03 == m_ReadByte[0] && 0x03 == m_ReadByte[1])
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
        /// 读温度值
        /// </summary>
        /// <returns></returns>
        public bool ReadCDTempeture1(ref double val)
        {
            try
            {
                m_WriteByte[0] = 0x02;//设备地址
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

                if (0x02 == m_ReadByte[0] && 0x03 == m_ReadByte[1])
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
        /// 读温度值
        /// </summary>
        /// <returns></returns>
        public bool ReadCDTempeture2(ref double val)
        {
            try
            {
                m_WriteByte[0] = 0x04;//设备地址
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

                if (0x04 == m_ReadByte[0] && 0x03 == m_ReadByte[1])
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
        /// 读柱前pH时间
        /// </summary>
        /// <returns></returns>
        public bool ReadPHTime1(ref double val)
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

        /// <summary>
        /// 读柱后pH时间
        /// </summary>
        /// <returns></returns>
        public bool ReadPHTime2(ref double val)
        {
            try
            {
                m_WriteByte[0] = 0x03;//设备地址
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

                if (0x03 == m_ReadByte[0] && 0x03 == m_ReadByte[1])
                {
                    val = Math.Round(MByteToFloat(m_ReadByte[3], m_ReadByte[4], m_ReadByte[5], m_ReadByte[6]), 2);
                    return true;
                }
            }
            catch
            { }

            return false; ;
        }

        /// <summary>
        /// 读柱前Cd时间
        /// </summary>
        /// <returns></returns>
        public bool ReadCDTime1(ref double val)
        {
            try
            {
                m_WriteByte[0] = 0x02;//设备地址
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

                if (0x02 == m_ReadByte[0] && 0x03 == m_ReadByte[1])
                {
                    val = Math.Round(MByteToFloat(m_ReadByte[3], m_ReadByte[4], m_ReadByte[5], m_ReadByte[6]), 2);
                    return true;
                }
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 读柱后Cd时间
        /// </summary>
        /// <returns></returns>
        public bool ReadCDTime2(ref double val)
        {
            try
            {
                m_WriteByte[0] = 0x04;//设备地址
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

                if (0x04 == m_ReadByte[0] && 0x03 == m_ReadByte[1])
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
