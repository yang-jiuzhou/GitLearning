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
    public class ComCollectorHB : ComCollector
    {
        public ENUMCollectorID MHBMode { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public ComCollectorHB(ComConf info) : base(info)
        {
            
        }


        /// <summary>
        /// 线程主函数
        /// </summary>
        protected override void ThreadRun()
        {
            int countL = 0;
            int countR = 0;
            int modeL = 0;
            int modeR = 0;
            double volL = 0;
            double volR = 0;

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
                            ReadStatus(ref m_item.m_txtGet, ref m_item.m_indexGet, ref m_item.m_ingGet, ref countL, ref countR, ref modeL, ref modeR, ref volL, ref volR);
                            WriteDirect(1);
                            EnumCollectorInfo.Init(countL, countR);
                            EnumCollectorInfo.SetBottleCollVol(volL, volR);
                            EnumCollectorInfo.ReSetBottleCollVol();
                            m_item.m_countL = countL;
                            m_item.m_countR = countR;

                            if (0 == m_item.m_indexGet)
                            {
                                m_item.m_indexSet = 1;
                            }
                            else
                            {
                                m_item.m_indexSet = m_item.m_indexGet;
                            }
                            m_item.m_txtSet = m_item.m_txtGet;
                            m_item.m_ingSet = m_item.m_ingGet;
                        }
                        m_state = CollectorState.ReadWrite;
                        break;
                    case CollectorState.ReadWrite:
                        if (Connect() && ReadStatus(ref m_item.m_txtGet, ref m_item.m_indexGet, ref m_item.m_ingGet, ref countL, ref countR, ref modeL, ref modeR, ref volL, ref volR))
                        {
                            m_communState = ENUMCommunicationState.Success;

                            if (EnumCollectorInfo.CountL != countL || EnumCollectorInfo.CountR != countR)
                            {
                                lock (m_item.m_locker)
                                {
                                    if (EnumCollectorInfo.CountL != countL)//左托盘变化
                                    {
                                        switch (m_item.m_txtSet)
                                        {
                                            case EnumCollIndexText.L:
                                                if (0 == EnumCollectorInfo.CountR)
                                                {
                                                    m_item.m_indexSet = 1;     
                                                }
                                                else
                                                {
                                                    m_item.m_indexSet = 1;
                                                    m_item.m_txtSet = EnumCollIndexText.R;
                                                }
                                                break;
                                            case EnumCollIndexText.R:
                                                if (0 == EnumCollectorInfo.CountR)
                                                {
                                                    m_item.m_txtSet = EnumCollIndexText.L;
                                                }
                                                break;
                                        }
                                    }
                                    else//右托盘变化
                                    {
                                        switch (m_item.m_txtSet)
                                        {
                                            case EnumCollIndexText.L:
                                                if (0 == EnumCollectorInfo.CountL)
                                                {
                                                    m_item.m_txtSet = EnumCollIndexText.R;
                                                }
                                                break;
                                            case EnumCollIndexText.R:
                                                if (0 == EnumCollectorInfo.CountL)
                                                {
                                                    m_item.m_indexSet = 1;
                                                }
                                                else
                                                {
                                                    m_item.m_indexSet = 1;
                                                    m_item.m_txtSet = EnumCollIndexText.L;
                                                }
                                                break;
                                        }
                                    }
                                }
                                WriteDirect(1);
                                EnumCollectorInfo.Init(countL, countR);
                                EnumCollectorInfo.SetBottleCollVol(volL, volR);
                                EnumCollectorInfo.ReSetBottleCollVol();
                                m_item.m_countL = countL;
                                m_item.m_countR = countR;
                            }
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
            version = "";

            return true;
        }

        /// <summary>
        /// 读序列
        /// </summary>
        /// <returns></returns>
        public override bool ReadSerial(ref string serial)
        {
            serial = "";

            return true;
        }

        /// <summary>
        /// 读Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override bool ReadModel(ref string model)
        {
            try
            {
                //发送命令
                m_WriteByte[0] = 0x02;//STX，！
                m_WriteByte[1] = 0X30;
                m_WriteByte[2] = 0X30;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x30;//PFC
                m_WriteByte[5] = 0x31;
                m_WriteByte[6] = 0x20;//VALUE
                m_WriteByte[7] = 0x20;
                m_WriteByte[8] = 0x20;
                m_WriteByte[9] = 0x20;
                m_WriteByte[10] = 0x20;
                m_WriteByte[11] = 0x20;
                m_WriteByte[12] = 0x00;//CRC
                m_WriteByte[13] = 0x00;
                m_WriteByte[14] = 0x00;
                byte[] mCRC = CRC.Cal12(m_WriteByte);//CRC校验
                m_WriteByte[12] = mCRC[0];
                m_WriteByte[13] = mCRC[1];
                m_WriteByte[14] = mCRC[2];
                m_WriteByte[15] = 0x03;//ETX

                if (!write(16) || !read())
                {
                    return false;
                }
				
				if (0x31 == m_ReadByte[1] && 0x30 == m_ReadByte[2])
                {
                    model = ENUMCollectorID.HB_DLY_W.ToString();
                    return true;
                }                  
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 读取收集器运行状态
        /// </summary>
        /// <param name="on"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool ReadStatus(ref EnumCollIndexText left, ref int index, ref bool on, ref int countL, ref int countR, ref int modeL, ref int modeR, ref double volL, ref double volR)
        {
            try
            {
                m_WriteByte[0] = 0x02;//STX，！
                m_WriteByte[1] = 0X31;
                m_WriteByte[2] = 0X30;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x30;//PFC
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

                left = 0x30 == m_ReadByte[6] ? EnumCollIndexText.L : EnumCollIndexText.R;
                index = (m_ReadByte[7] - 0x30) * 10 + (m_ReadByte[8] - 0x30);
                on = 0x30 == m_ReadByte[9] ? false : true;
                modeL = m_ReadByte[10] - 0x30;
                switch (MHBMode)
                {
                    case ENUMCollectorID.HB_DLY_W:
                        switch (modeL)
                        {
                            case 0: countL = 0; volL = 0; break;
                            case 1: countL = 40; volL = 15; break;
                            case 2: countL = 24; volL = 25; break;
                            case 3: countL = 12; volL = 30; break;
                        }
                        break;
                    case ENUMCollectorID.HB_DLY_B:
                        switch (modeL)
                        {
                            case 0: countL = 0; volL = 0; break;
                            case 1: countL = 84; volL = 2; break;
                            case 2: countL = 60; volL = 5; break;
                            case 3: countL = 44; volL = 7; break;
                            case 4: countL = 40; volL = 10; break;
                            case 5: countL = 40; volL = 15; break;
                            case 6: countL = 24; volL = 25; break;
                            case 7: countL = 12; volL = 50; break;
                        }
                        break;
                }
                m_countL = countL;
                modeR = m_ReadByte[11] - 0x30;
                switch (MHBMode)
                {
                    case ENUMCollectorID.HB_DLY_W:
                        switch (modeR)
                        {
                            case 0: countR = 0; volR = 0; break;
                            case 1: countR = 40; volR = 15; break;
                            case 2: countR = 24; volR = 25; break;
                            case 3: countR = 12; volR = 30; break;
                        }
                        break;
                    case ENUMCollectorID.HB_DLY_B:
                        switch (modeR)
                        {
                            case 0: countR = 0; volR = 0; break;
                            case 1: countR = 84; volR = 2; break;
                            case 2: countR = 60; volR = 5; break;
                            case 3: countR = 44; volR = 7; break;
                            case 4: countR = 40; volR = 10; break;
                            case 5: countR = 40; volR = 15; break;
                            case 6: countR = 24; volR = 25; break;
                            case 7: countR = 12; volR = 50; break;
                        }
                        break;

                }
                m_countR = countR;

                if (0 == countL && 0 == countR)
                {
                    index = 0;
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
                m_WriteByte[0] = 0x02;//STX，！
                m_WriteByte[1] = 0X31;
                m_WriteByte[2] = 0X30;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x31;//PFC
                m_WriteByte[5] = 0x30;
                m_WriteByte[6] = 0x20;
                m_WriteByte[7] = 0x20;
                m_WriteByte[8] = 0x20;
                switch (left)
                {
                    case EnumCollIndexText.L:
                        m_WriteByte[9] = 0x31;
                        break;
                    case EnumCollIndexText.R:
                        m_WriteByte[9] = 0x32;
                        break;
                }
                m_WriteByte[10] = (byte)(0x30 + (index / 10));
                m_WriteByte[11] = (byte)(0x30 + (index % 10));
                byte[] mCRC = CRC.Cal12(m_WriteByte);//CRC校验
                m_WriteByte[12] = mCRC[0];
                m_WriteByte[13] = mCRC[1];
                m_WriteByte[14] = mCRC[2];
                m_WriteByte[15] = 0x03;//ETX

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
                m_WriteByte[0] = 0x02;//STX，！
                m_WriteByte[1] = 0X31;
                m_WriteByte[2] = 0X30;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x31;//PFC
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
                m_WriteByte[15] = 0x03;//ETX

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
                m_WriteByte[0] = 0x02;//STX，！
                m_WriteByte[1] = 0X31;
                m_WriteByte[2] = 0X30;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x31;//PFC
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
                m_WriteByte[0] = 0x02;//STX，！
                m_WriteByte[1] = 0X31;
                m_WriteByte[2] = 0X30;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x31;//PFC
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
                m_WriteByte[15] = 0x03;//ETX

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
                m_WriteByte[0] = 0x02;//STX，！
                m_WriteByte[1] = 0X31;
                m_WriteByte[2] = 0X30;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x31;//PFC
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
                m_WriteByte[15] = 0x03;//ETX

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
        /// 设置方向
        /// </summary>
        /// <returns></returns>
        private bool WriteDirect(int mode)
        {
            try
            {
                //发送命令
                m_WriteByte[0] = 0x02;//STX，！
                m_WriteByte[1] = 0X31;
                m_WriteByte[2] = 0X30;
                m_WriteByte[3] = 0x30;//AI，默认，“0”
                m_WriteByte[4] = 0x30;//PFC
                m_WriteByte[5] = 0x32;
                m_WriteByte[6] = 0x20;//VALUE
                m_WriteByte[7] = 0x20;
                m_WriteByte[8] = 0x20;
                m_WriteByte[9] = 0x20;
                m_WriteByte[10] = (byte)(0x30 + mode);
                m_WriteByte[11] = 0x33;
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
    }
}