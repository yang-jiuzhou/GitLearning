using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    [Serializable]
    public class ConfOther : IDBSetGet
    {
        /// <summary>
        /// 阀归位
        /// </summary>
        public bool MResetValve { get; set; }
        /// <summary>
        /// 紫外关灯
        /// </summary>
        public bool MCloseUV { get; set; }
        /// <summary>
        /// 双泵运行开启动态混合器
        /// </summary>
        public bool MOpenMixer { get; set; }

        /// <summary>
        /// PID参数的P
        /// </summary>
        public double MPIDP { get; set; }
        /// <summary>
        /// PID参数的P
        /// </summary>
        public double MPIDI { get; set; }
        /// <summary>
        /// PID参数的P
        /// </summary>
        public double MPIDD { get; set; }

        /// <summary>
        /// 清博华紫外检测器连着手动进样阀
        /// </summary>
        public bool MUVIJV { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public ConfOther()
        {
            MResetValve = false;
            MCloseUV = false;
            MOpenMixer = false;

            MPIDP = 12;
            MPIDI = 8;
            MPIDD = 2;

            MUVIJV = false;
        }

        /// <summary>
        /// 返回写入数据库的信息
        /// </summary>
        /// <param name="split"></param>
        /// <returns></returns>
        public string GetDBInfo(string split)
        {
            StringBuilderSplit sb = new StringBuilderSplit(split);
            sb.Append(MResetValve);
            sb.Append(MCloseUV);
            sb.Append(MOpenMixer);
            sb.Append(MPIDP);
            sb.Append(MPIDI);
            sb.Append(MPIDD);
            sb.Append(MUVIJV);
            return sb.ToString();
        }

        /// <summary>
        /// 解析数据库信息
        /// </summary>
        /// <param name="split"></param>
        /// <param name="infoStr"></param>
        public void SetDBInfo(string split, string infoStr)
        {
            try
            {
                string[] info = System.Text.RegularExpressions.Regex.Split(infoStr, split);
                int index = 0;
                MResetValve = Convert.ToBoolean(info[index++]);
                MCloseUV = Convert.ToBoolean(info[index++]);
                MOpenMixer = Convert.ToBoolean(info[index++]);
                MPIDP = Convert.ToDouble(info[index++]);
                MPIDI = Convert.ToDouble(info[index++]);
                MPIDD = Convert.ToDouble(info[index++]);
                MUVIJV = Convert.ToBoolean(info[index++]);
            }
            catch { }
        }
    }
}
