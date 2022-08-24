using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    [Serializable]
    public class ConfWash : IDBSetGet
    {
        //系统冲洗的时间
        public double MWashTime { get; set; }
        //系统冲洗的流速比        
        public double MWashFlowPer { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ConfWash()
        {
            MWashTime = 0.1;
            MWashFlowPer = 50.0;
        }

        /// <summary>
        /// 返回写入数据库的信息
        /// </summary>
        /// <param name="split"></param>
        /// <returns></returns>
        public string GetDBInfo(string split)
        {
            StringBuilderSplit sb = new StringBuilderSplit(split);
            sb.Append(MWashTime);
            sb.Append(MWashFlowPer);
            return sb.ToString();
        }

        /// <summary>
        /// 解析数据库信息
        /// </summary>
        /// <param name="split"></param>
        /// <param name="infoStr"></param>
        public void SetDBInfo(string split, string infoStr)
        {
            string[] info = System.Text.RegularExpressions.Regex.Split(infoStr, split);
            int index = 0;
            try
            {
                MWashTime = Convert.ToDouble(info[index++]);
                MWashFlowPer = Convert.ToDouble(info[index++]);
            }
            catch { }
        }
    }
}
