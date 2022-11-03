using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    [Serializable]
    public class ConfCollector : IDBSetGet
    {
        //管路死体积
        public double MGLTJ { get; set; }
        //左托盘试管体积
        public double MVolL { get; set; }
        //右托盘试管体积
        public double MVolR { get; set; }
        //左托盘数量
        public int MCountL { get; set; }
        //右托盘数量
        public int MCountR { get; set; }
        //左托盘模式
        public int MModeL { get; set; }
        //右托盘模式
        public int MModeR { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ConfCollector()
        {
            MGLTJ = 0;
            MVolL = 15;
            MVolR = 15;
            MCountL = 60;
            MCountR = 60;
            MModeL = 0;
            MModeR = 0;
        }

        /// <summary>
        /// 返回写入数据库的信息
        /// </summary>
        /// <param name="split"></param>
        /// <returns></returns>
        public string GetDBInfo(string split)
        {
            StringBuilderSplit sb = new StringBuilderSplit(split);
            sb.Append(MGLTJ);
            sb.Append(MVolL);
            sb.Append(MVolR);
            sb.Append(MCountL);
            sb.Append(MCountR);
            sb.Append(MModeL);
            sb.Append(MModeR);
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
                MGLTJ = Convert.ToDouble(info[index++]);
                MVolL = Convert.ToInt32(info[index++]);
                MVolR = Convert.ToInt32(info[index++]);
                MCountL = Convert.ToInt32(info[index++]);
                MCountR = Convert.ToInt32(info[index++]);
                MModeL = Convert.ToInt32(info[index++]);
                MModeR = Convert.ToInt32(info[index++]);
            }
            catch { }
        }
    }
}
