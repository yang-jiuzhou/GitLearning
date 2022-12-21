using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    /// <summary>
    /// 沃森马洛
    /// </summary>
    class ComPumpWatsonMarlow : ComPump
    {
        private PumpItem m_pumpItem = new PumpItem();

        private double m_slope = 1;     //ml/rev/泵扬程

        public ComPumpWatsonMarlow(ComConf info) : base(info)
        {

        }

        /// <summary>
        /// 读版本
        /// </summary>
        /// <returns></returns>
        public override bool ReadVersion(ref string version)
        {
            m_WriteByte = Encoding.ASCII.GetBytes("<1,RS,??>");

            if (!write(m_WriteByte.Length) || !read())
            {
                return false;
            }

            string valStr = System.Text.Encoding.Default.GetString(m_ReadByte);
            if(valStr.First().Equals('<')&&valStr.Last().Equals('>'))
            {
                //<1,530Du,15.12,520R2,9.60,73.3,CW,1,1461,0,54>
                //<地址（在泵上进行设置），泵类型， 转速体积比，泵编号，管道尺寸，当前转速，运行方向（CW顺时针，CCW逆时针），未知，未知，运行停止（0停止）>
                string[] arr = valStr.Split(',');
                m_slope = Convert.ToDouble(arr[2]);
                double ridus= Convert.ToDouble(arr[4]);//管道尺寸
                double rpm = Convert.ToDouble(arr[5]);//转速

            }
            if (valStr.Contains("PumpVer"))
            {
                version = valStr.Substring(valStr.IndexOf("PumpVer") + 7, 2);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 写流速
        /// </summary>
        /// <param name="val"></param>
        private bool WriteFlow(double val)
        {
            try
            {
                m_WriteByte = Encoding.ASCII.GetBytes("<1,SP," + (int)(val / m_slope) + ",??>");

                write(m_WriteByte.Length);

                Thread.Sleep(DlyBase.c_sleep2);

                return true;
            }
            catch { }

            return false;
        }
    }
}
