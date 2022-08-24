using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    [Serializable]
    public class ConfpHCdUV : IDBSetGet
    {
        public string MName { get; set; }
        public double MVol { get; set; }


        /// <summary>
        /// 返回写入数据库的信息
        /// </summary>
        /// <param name="split"></param>
        /// <returns></returns>
        public string GetDBInfo(string split)
        {
            StringBuilderSplit sb = new StringBuilderSplit(split);
            sb.Append(MName);
            sb.Append(MVol);

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

                MName = info[index++];
                MVol = Convert.ToDouble(info[index++]);
            }
            catch
            { }
        }
    }
}
