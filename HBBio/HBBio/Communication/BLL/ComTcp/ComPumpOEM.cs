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
    class ComPumpOEM : ComPump
    {
        private PumpItem m_pumpItem1 = new PumpItem();
        private PumpItem m_pumpItem2 = new PumpItem();
        private PTItem m_ptItem1 = new PTItem();
        private PTItem m_ptItem2 = new PTItem();
        private PTItem m_ptItem3 = new PTItem();


        /// <summary>
        /// 构造函数
        /// </summary>
        public ComPumpOEM(ComConf info) : base(info)
        {
            switch (MComConf.MCommunMode)
            {
                case EnumCommunMode.Com:
                    m_serialPort.BaudRate = 115200;
                    m_serialPort.Parity = Parity.Even;
                    break;
                case EnumCommunMode.TCP:
                    break;
            }

            if (0 != m_scInfo.MList.Count)
            {
                m_pumpItem1 = (PumpItem)m_scInfo.MList[0];
                m_pumpItem1.MAre = m_are;
                m_pumpItem2 = (PumpItem)m_scInfo.MList[1];
                m_pumpItem2.MAre = m_are;
                m_ptItem1 = (PTItem)m_scInfo.MList[2];
                m_ptItem2 = (PTItem)m_scInfo.MList[3];
                m_ptItem3 = (PTItem)m_scInfo.MList[4];

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
            if (m_ptItem3.MVisible)
            {
                valList.Add(m_ptItem3.m_pressGet);
                if (m_ptItem3.MConstName.Equals(ENUMPTName.PTColumnBack.ToString()))
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
            if (m_ptItem3.MVisible)
            {
                valList.Add("N/A");
                if (m_ptItem3.MConstName.Equals(ENUMPTName.PTColumnBack.ToString()))
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
            DateTime lastTimeRead = DateTime.Now;
            int timeDistance = 0;
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
                        if (Connect() && ReadVal(ref m_pumpItem1.m_flowGet, ref m_pumpItem2.m_flowGet, ref m_ptItem1.m_pressGet, ref m_ptItem2.m_pressGet, ref m_ptItem3.m_pressGet))
                        {
                            m_communState = ENUMCommunicationState.Success;

                            timeDistance = (int)(DateTime.Now - lastTimeRead).TotalMilliseconds;
                            if (timeDistance < DlyBase.c_sleep10)
                            {
                                m_are.WaitOne(DlyBase.c_sleep10 - timeDistance);
                            }
                            lastTimeRead = DateTime.Now;

                            if (m_pumpItem1.MVisible)
                            {
                                WriteFlow("A", m_pumpItem1.m_pause ? 0 : m_pumpItem1.m_flowSet, m_pumpItem1.m_flowGet);
                            }
                            if (m_pumpItem2.MVisible)
                            {
                                WriteFlow("B", m_pumpItem2.m_pause ? 0 : m_pumpItem2.m_flowSet, m_pumpItem2.m_flowGet);
                            }
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
                        WritePressMax("A", m_ptItem1.m_maxSet);
                        WritePressMax("B", m_ptItem2.m_maxSet);
                        WritePressMax("C", m_ptItem3.m_maxSet);
                        m_state = PUMPState.ReadWrite;
                        break;
                    case PUMPState.MinPress:
                        WritePressMin("A", m_ptItem1.m_minSet);
                        WritePressMin("B", m_ptItem2.m_minSet);
                        WritePressMin("C", m_ptItem3.m_minSet);
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
            m_WriteByte = Encoding.ASCII.GetBytes("^PumpVerRD$");

            if (!write(m_WriteByte.Length) || !read())
            {
                return false;
            }

            string valStr = System.Text.Encoding.Default.GetString(m_ReadByte);
            if (valStr.Contains("PumpVer"))
            {
                version = valStr.Substring(valStr.IndexOf("PumpVer") + 7, 2);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 读序列
        /// </summary>
        /// <returns></returns>
        public override bool ReadSerial(ref string serial)
        {
            m_WriteByte = Encoding.ASCII.GetBytes("^PumpSNRD$");

            if (!write(m_WriteByte.Length) || !read())
            {
                return false;
            }

            string valStr = System.Text.Encoding.Default.GetString(m_ReadByte);
            if (valStr.Contains("PumpSN"))
            {
                serial = valStr.Substring(valStr.IndexOf("PumpSN") + 6, 8);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 读Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override bool ReadModel(ref string model)
        {
            ENUMPumpID id = ENUMPumpID.OEM0100;
            if (ReadModel(ref id))
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
        /// 读型号
        /// </summary>
        /// <returns></returns>
        public bool ReadModel(ref ENUMPumpID model)
        {
            m_WriteByte = Encoding.ASCII.GetBytes("^PumpModelRD$");

            if (!write(m_WriteByte.Length) || !read(DlyBase.c_sleep5))
            {
                return false;
            }

            string valStr = System.Text.Encoding.Default.GetString(m_ReadByte);
            if (valStr.Contains("PumpModel"))
            {
                valStr = valStr.Remove(0, valStr.IndexOf("PumpModel"));
                valStr = valStr.Substring(0, valStr.IndexOf('$'));
                model = (ENUMPumpID)Enum.Parse(typeof(ENUMPumpID), "OEM" + valStr.Replace("PumpModel", ""));
                return true;
            }
         
            return false;
        }

        /// <summary>
        /// 读值
        /// </summary>
        /// <param name="flowA"></param>
        /// <param name="pressA"></param>
        public bool ReadVal(ref double flowA, ref double flowB, ref double pressA, ref double pressB, ref double pressC)
        {
            try
            {
                string str = null;
                while (string.IsNullOrEmpty(str) || !str.Contains("PumpERR"))
                {
                    if (!read())
                    {
                        return false;
                    }
                    str += Encoding.Default.GetString(m_ReadByte, 0, m_ReadLen);
                }

                string[] info = str.Split(new string[] { "^Pump" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < info.Length; i++)
                {
                    if (info[i].Contains("ARateSD") && info[i].First() == 'A' && info[i].Last() == '$')
                    {
                        flowA = Math.Round(Convert.ToDouble(info[i].Replace("$", "").Replace("ARateSD", "")) / 1000, 2);
                    }
                    else if (info[i].Contains("BRateSD") && info[i].First() == 'B' && info[i].Last() == '$')
                    {
                        flowB = Math.Round(Convert.ToDouble(info[i].Replace("$", "").Replace("BRateSD", "")) / 1000, 2);
                    }
                    else if (info[i].Contains("APressSD") && info[i].First() == 'A' && info[i].Last() == '$')
                    {
                        pressA = Math.Round(Convert.ToDouble(info[i].Replace("$", "").Replace("APressSD", "")) / 1000, 2);
                        if (pressA < 0)
                        {
                            pressA = 0;
                        }
                    }
                    else if (info[i].Contains("BPressSD") && info[i].First() == 'B' && info[i].Last() == '$')
                    {
                        pressB = Math.Round(Convert.ToDouble(info[i].Replace("BPressSD", "").Replace("$", "")) / 1000, 2);
                        if (pressB < 0)
                        {
                            pressB = 0;
                        }
                    }
                    else if (info[i].Contains("CPressSD") && info[i].First() == 'C' && info[i].Last() == '$')
                    {
                        pressC = Math.Round(Convert.ToDouble(info[i].Replace("CPressSD", "").Replace("$", "")) / 1000, 2);
                        if (pressC < 0)
                        {
                            pressC = 0;
                        }
                    }
                }
            }
            catch { }

            return true;
        }

        /// <summary>
        /// 写流速
        /// </summary>
        /// <param name="type"></param>
        /// <param name="val"></param>
        public void WriteFlow(string type, double valNew, double valOld)
        {
            try
            {
                if (Math.Abs(valNew - valOld) < DlyBase.DOUBLE)
                {
                    return;
                }

                if (valNew > 0 )
                {
                    m_WriteByte = Encoding.ASCII.GetBytes("^Pump" + type + "RateST" + ((int)(valNew * 1000) + 10000000).ToString().Substring(1, 7) + "$");  
                }
                else
                {
                    m_WriteByte = Encoding.ASCII.GetBytes("^Pump" + type + "RateMST0000000$");
                }

                write(m_WriteByte.Length);

                Thread.Sleep(DlyBase.c_sleep2);
            }
            catch { }
        }

        /// <summary>
        /// 写最大压力
        /// </summary>
        /// <param name="type"></param>
        /// <param name="val"></param>
        public bool WritePressMax(string type, double val)
        {
            m_WriteByte = Encoding.ASCII.GetBytes("^Pump" + type + "PressMaxST" + ((int)(val * 100) + 100000).ToString().Substring(1, 5) + "$");
            return write(m_WriteByte.Length);
        }

        /// <summary>
        /// 写最小压力
        /// </summary>
        /// <param name="type"></param>
        /// <param name="val"></param>
        public bool WritePressMin(string type, double val)
        {
            m_WriteByte = Encoding.ASCII.GetBytes("^Pump" + type + "PressMinST" + ((int)(val * 100) + 100000).ToString().Substring(1, 5) + "$");
            return write(m_WriteByte.Length);
        }
    }
}
