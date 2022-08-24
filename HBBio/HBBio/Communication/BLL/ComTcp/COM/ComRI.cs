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
    class ComRI : BaseCom
    {
        private RIItem m_itemRI = new RIItem();                  
        private TTItem m_itemTT = new TTItem();
        private RIState m_state = RIState.Free;                   //状态


        /// <summary>
        /// 构造函数
        /// </summary>
        public ComRI(ComConf info) : base(info)
        {
            if (0 != m_scInfo.MList.Count)
            {
                m_itemRI = (RIItem)m_scInfo.MList[0];
                m_itemTT = (TTItem)m_scInfo.MList[1];
            }
        }

        /// <summary>
        /// 获取运行数据读值
        /// </summary>
        /// <returns></returns>
        public override List<object> GetRunDataValueList()
        {
            List<object> result = new List<object>();

            result.Add(m_itemRI.m_value);
            result.Add(m_itemTT.m_tempGet);

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
            result.Add(m_itemRI.m_tempSet);

            return result;
        }

        /// <summary>
        /// 线程主函数
        /// </summary>
        protected override void ThreadRun()
        {
            int num = 0;
            while (true)
            {
                switch (m_state)
                {
                    case RIState.Free:
                        Close();
                        Thread.Sleep(DlyBase.c_sleep10);
                        m_communState = ENUMCommunicationState.Free;
                        break;
                    case RIState.Version:
                        if (Connect())
                        {
                            string tempVersion = null;
                            ReadVersion(ref tempVersion);
                            m_scInfo.MVersion = tempVersion;
                            Close();
                        }
                        m_state = RIState.Free;
                        break;
                    case RIState.ReadFirst:
                        if (Connect())
                        {
                            int tempGet = (int)m_itemTT.m_tempGet;
                            if (ReadCT(ref m_itemRI.m_tempSet, ref tempGet))
                            {
                                m_itemTT.m_tempGet = tempGet;
                            }

                            SetStartEnd(true);

                            Close();
                        }
                        m_state = RIState.ReadWrite;
                        break;
                    case RIState.ReadWrite:
                        if (Connect() && ReadValue(ref m_itemRI.m_value))
                        {
                            m_communState = ENUMCommunicationState.Success;

                            if (m_itemRI.m_clear)
                            {
                                m_itemRI.m_clear = false;
                                SetAutoZero();
                            }
                            if (m_itemRI.m_purgeOn)
                            {
                                m_itemRI.m_purgeOn = false;
                                SetPurge(true);
                            }
                            if (m_itemRI.m_purgeOff)
                            {
                                m_itemRI.m_purgeOff = false;
                                SetPurge(false);
                            }
                            if (m_itemRI.m_temperature)
                            {
                                m_itemRI.m_temperature = false;
                                SetTemp(m_itemRI.m_tempSet);
                            }

                            if (30 == num++)
                            {
                                int tempSet = 0;
                                int tempGet = (int)m_itemTT.m_tempGet;
                                if (ReadCT(ref tempSet, ref tempGet))
                                {
                                    m_itemTT.m_tempGet = tempGet;
                                }
                                num = 0;
                            }
                            Thread.Sleep(DlyBase.c_sleep5);
                        }
                        else
                        {
                            Close();

                            for (int i = 0; i < c_timeout; i++)
                            {
                                if (RIState.ReadWrite != m_state)
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
                    case RIState.Abort:
                        if (Connect())
                        {
                            SetStartEnd(false);

                            Close();
                        }
                        m_communState = ENUMCommunicationState.Over;
                        return;
                }
            }
        }

        /// <summary>
        /// 线程的状态设置
        /// </summary>
        public override void ThreadStatus(ENUMThreadStatus status)
        {
            switch (status)
            {
                case ENUMThreadStatus.Free:
                    m_state = RIState.Free;
                    break;
                case ENUMThreadStatus.Version:
                    m_state = RIState.Version;
                    break;
                case ENUMThreadStatus.WriteOrRead:
                    m_state = RIState.ReadFirst;
                    break;
                case ENUMThreadStatus.Abort:
                    m_state = RIState.Abort;
                    break;
            }
        }


        /// <summary>
        /// 读型号
        /// </summary>
        /// <returns></returns>
        public override bool ReadVersion(ref string version)
        {
            bool result = false;

            try
            {
                m_WriteByte = Encoding.ASCII.GetBytes("CN\n");

                if (!write(m_WriteByte.Length) || !read())
                {
                    return false;
                }

                string[] arrStr = Encoding.Default.GetString(m_ReadByte).Substring(0, Encoding.Default.GetString(m_ReadByte).IndexOf("\r\n")).Split(',');
                if (arrStr[0].Equals("CN"))
                {
                    version = arrStr[2];
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
            bool result = false;

            try
            {
                m_WriteByte = Encoding.ASCII.GetBytes("CN\n");

                if (!write(m_WriteByte.Length) || !read())
                {
                    return false;
                }

                string[] arrStr = Encoding.Default.GetString(m_ReadByte).Substring(0, Encoding.Default.GetString(m_ReadByte).IndexOf("\r\n")).Split(',');
                if (arrStr[0].Equals("CN"))
                {
                    serial = arrStr[1];
                    result = true;
                }
            }
            catch
            { }

            return result;
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
                m_WriteByte = Encoding.ASCII.GetBytes("CN\n");

                if (!write(m_WriteByte.Length) || !read())
                {
                    return false;
                }

                string[] arrStr = Encoding.Default.GetString(m_ReadByte).Substring(0, Encoding.Default.GetString(m_ReadByte).IndexOf("\r\n")).Split(',');
                if (arrStr[0].Equals("CN"))
                {
                    model = ENUMRIName.RI01.ToString();
                    result = true;
                }
            }
            catch
            { }

            return result;
        }

        /// <summary>
        /// 读取设定温度和实际温度
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ReadCT(ref int tempSet, ref int tempGet)
        {
            bool result = false;

            try
            {
                m_WriteByte = Encoding.ASCII.GetBytes("CT\n");

                if (!write(m_WriteByte.Length) || !read())
                {
                    return false;
                }

                string[] arrStr = Encoding.Default.GetString(m_ReadByte).Substring(0, Encoding.Default.GetString(m_ReadByte).IndexOf("\r\n")).Split(',');
                if (arrStr[0].Equals("CT"))
                {
                    tempSet = Convert.ToInt32(arrStr[1]);
                    tempGet = Convert.ToInt32(arrStr[2]);
                    result = true;
                }
            }
            catch
            { }

            return result;
        }

        /// <summary>
        /// 读取吸收值
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ReadValue(ref double value)
        {
            bool result = false;

            try
            {
                m_WriteByte = Encoding.ASCII.GetBytes("DF,01\n");

                if (!write(m_WriteByte.Length) || !read())
                {
                    return false;
                }

                string[] arrStr = Encoding.Default.GetString(m_ReadByte).Substring(0, Encoding.Default.GetString(m_ReadByte).IndexOf("\r\n")).Split(',');
                if (arrStr[0].Equals("DF"))
                {
                    if (3 == arrStr.Length)
                    {
                        value = Math.Round(Convert.ToDouble(arrStr[2]) / 500, 2);// *2/1000
                    }

                    result = true;
                }
            }
            catch
            { }

            return result;
        }

        /// <summary>
        /// 开始、结束发送数据
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public bool SetStartEnd(bool flag)
        {
            bool result = false;

            try
            {
                m_WriteByte = Encoding.ASCII.GetBytes("SC" + (flag ? ",1\n" : ",0\n"));

                if (!write(m_WriteByte.Length) || !read())
                {
                    return false;
                }

                if (0x06 == m_ReadByte[0] && 0x0D == m_ReadByte[1] && 0x0A == m_ReadByte[2] && 0x1A == m_ReadByte[3])
                {
                    result = true;
                }
            }
            catch
            { }

            return result;
        }

        /// <summary>
        /// 清洗
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public bool SetPurge(bool flag)
        {
            bool result = false;

            try
            {
                m_WriteByte = Encoding.ASCII.GetBytes("SV" + (flag ? ",1\n" : ",0\n"));

                if (!write(m_WriteByte.Length) || !read())
                {
                    return false;
                }

                if (0x06 == m_ReadByte[0] && 0x0D == m_ReadByte[1] && 0x0A == m_ReadByte[2] && 0x1A == m_ReadByte[3])
                {
                    result = true;
                }
            }
            catch
            { }

            return result;
        }

        /// <summary>
        /// 自动清零
        /// </summary>
        /// <returns></returns>
        public bool SetAutoZero()
        {
            bool result = false;

            try
            {
                m_WriteByte = Encoding.ASCII.GetBytes("AZ\n");

                if (!write(m_WriteByte.Length) || !read())
                {
                    return false;
                }

                string valStr = Encoding.Default.GetString(m_ReadByte);
                if (0x06 == m_ReadByte[0] && 0x0D == m_ReadByte[1] && 0x0A == m_ReadByte[2] && 0x1A == m_ReadByte[3]
                    || valStr.Contains("AZ"))
                {
                    result = true;
                }
            }
            catch
            { }

            return result;
        }

        /// <summary>
        /// 设置温度
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public bool SetTemp(int temp)
        {
            bool result = false;

            try
            {
                m_WriteByte = Encoding.ASCII.GetBytes("TS," + temp + "\n");

                if (!write(m_WriteByte.Length) || !read())
                {
                    return false;
                }

                if (0x06 == m_ReadByte[0] && 0x0D == m_ReadByte[1] && 0x0A == m_ReadByte[2] && 0x1A == m_ReadByte[3])
                {
                    result = true;
                }
            }
            catch
            { }

            return result;
        }
    }
}
