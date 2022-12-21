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
    public class ComPHCDOEM : ComPHCD
    {
        private PHItem m_pHItem = new PHItem();                     //pH元素
        public PHItem MpHItem
        {
            get
            {
                return m_pHItem;
            }
        }
        private CDItem m_CdItem = new CDItem();                     //Cd元素
        public CDItem MCdItem
        {
            get
            {
                return m_CdItem;
            }
        }
        private TTItem m_ttItem = new TTItem();                     //温度元素
        public TTItem MTTItem
        {
            get
            {
                return m_ttItem;
            }
        }

        private double m_valPH = 0;
        public double MPHVal
        {
            set
            {
                m_valPH = value;
                m_state = PHCDState.WritePH;
            }
        }

        private double m_valCD = 0;
        public double MCDVal
        {
            set
            {
                m_valCD = value;
                m_state = PHCDState.WriteCD;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ComPHCDOEM(ComConf info) : base(info)
        {
            switch (MComConf.MCommunMode)
            {
                case EnumCommunMode.Com:
                    m_serialPort.BaudRate = 115200;
                    break;
                case EnumCommunMode.TCP:
                    break;
            }

            if (0 != m_scInfo.MList.Count)
            {
                m_pHItem = (PHItem)m_scInfo.MList[0];
                m_CdItem = (CDItem)m_scInfo.MList[1];
                m_ttItem = (TTItem)m_scInfo.MList[2];
            }
        }

        /// <summary>
        /// 获取运行数据读值
        /// </summary>
        /// <returns></returns>
        public override List<object> GetRunDataValueList()
        {
            List<object> result = new List<object>();

            if (m_pHItem.MVisible)
            {
                result.Add(m_pHItem.m_pHGet);
            }
            if (m_CdItem.MVisible)
            {
                result.Add(m_CdItem.m_CdGet);
            }
            if (m_ttItem.MVisible)
            {
                result.Add(m_ttItem.m_tempGet);
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

            if (m_pHItem.MVisible)
            {
                result.Add("N/A");
            }
            if (m_CdItem.MVisible)
            {
                result.Add("N/A");
            }
            if (m_ttItem.MVisible)
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
            double cdTemp = 0;
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
                        if (Connect() && ReadValue(ref m_pHItem.m_pHGet, ref m_ttItem.m_tempGet, ref cdVal, ref cdTemp))
                        {
                            m_communState = ENUMCommunicationState.Success;
                            m_CdItem.UpdateValue(cdVal);
                            Thread.Sleep(DlyBase.c_sleep5);
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
                    case PHCDState.WritePH:
                        if (Connect())
                        {
                            WritePHValue(m_valPH);
                        }
                        m_state = PHCDState.Read;
                        break;
                    case PHCDState.WriteCD:
                        if (Connect())
                        {
                            WriteCDValue(m_valCD);
                        }
                        m_state = PHCDState.Read;
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
            bool result = false;

            try
            {
                m_WriteByte = Encoding.ASCII.GetBytes("^PTCVerRD$");

                if (!write(m_WriteByte.Length) || !read())
                {
                    return false;
                }

                string valStr = Encoding.Default.GetString(m_ReadByte);
                if (valStr.Contains("PTCVer"))
                {
                    valStr = valStr.Remove(0, valStr.IndexOf("PTCVer"));
                    valStr = valStr.Substring(0, valStr.IndexOf('$'));
                    version = valStr.Replace("PTCVer", "");
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
                m_WriteByte = Encoding.ASCII.GetBytes("^PTCSNRD$");

                if (!write(m_WriteByte.Length) || !read())
                {
                    return false;
                }

                string valStr = Encoding.Default.GetString(m_ReadByte);
                if (valStr.Contains("PTCSN"))
                {
                    valStr = valStr.Remove(0, valStr.IndexOf("PTCSN"));
                    valStr = valStr.Substring(0, valStr.IndexOf('$'));
                    serial = valStr.Replace("PTCSN", "");
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
            ENUMDetectorID id = ENUMDetectorID.pHCdOEM;
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
        public bool ReadModel(ref ENUMDetectorID model)
        {
            bool result = false;

            try
            {
                m_WriteByte = Encoding.ASCII.GetBytes("^PTCModelRD$");

                if (!write(m_WriteByte.Length) || !read())
                {
                    return false;
                }

                string valStr = Encoding.Default.GetString(m_ReadByte);
                if (valStr.Contains("PTCModel"))
                {
                    valStr = valStr.Remove(0, valStr.IndexOf("PTCModel"));
                    valStr = valStr.Substring(0, valStr.IndexOf('$'));
                    model = ENUMDetectorID.pHCdOEM;
                    result = true;
                }
            }
            catch
            { }

            return result;
        }

        /// <summary>
        /// 读CD值
        /// </summary>
        /// <param name="ph"></param>
        /// <param name="temp"></param>
        /// <param name="cd1">电导值</param>
        /// <param name="cd2">校准前电导值</param>
        public bool ReadValue(ref double ph, ref double temp, ref double cd1, ref double cd2)
        {
            m_WriteByte = Encoding.ASCII.GetBytes("^PTCValueRQ$");

            if (!write(m_WriteByte.Length) || !read())
            {
                return false;
            }

            try
            {
                string valStr = Encoding.Default.GetString(m_ReadByte, 0, m_ReadLen);
                if (!string.IsNullOrEmpty(valStr) && valStr.First() == '^' && valStr.Last() == '$')
                {
                    valStr = valStr.Replace("^PTCValueSD", "").Replace("$", "");
                    string[] val = valStr.Split(new Char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    if (4 == val.Length)
                    {
                        //读pH
                        double tmp = Math.Round(double.Parse(val[0]), 2);
                        if (StaticValue.s_minPH <= tmp && tmp <= StaticValue.s_maxPH)
                        {
                            ph = tmp;
                        }
                        //读温度
                        tmp = Math.Round(double.Parse(val[1]), 2);
                        if (0 <= tmp && tmp <= 100)
                        {
                            temp = tmp;
                        }
                        //读电导
                        tmp = Math.Round(double.Parse(val[2]) / 1000, 4);
                        if (StaticValue.s_minCD <= tmp && tmp <= StaticValue.s_maxCD)
                        {
                            cd1 = tmp;
                        }
                        //读校准前电导
                        cd2 = Math.Round(double.Parse(val[3]) / 1000, 4);
                    }
                }
            }
            catch
            { }

            return true;
        }

        /// <summary>
        /// 校准PH
        /// </summary>
        /// <param name="val"></param>
        public void WritePHValue(double val)
        {
            m_WriteByte = Encoding.ASCII.GetBytes("^PHCalibST" + val.ToString("f2") + "$");
            write(m_WriteByte.Length);
        }

        /// <summary>
        /// 校准CD
        /// </summary>
        /// <param name="val"></param>
        public void WriteCDValue(double val)
        {
            m_WriteByte = Encoding.ASCII.GetBytes("^CondCalibST" + (val * 1000).ToString("f1") + "$");
            write(m_WriteByte.Length);
        }
    }
}
