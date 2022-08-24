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
        /// 构造函数
        /// </summary>
        public ConfOther()
        {
            MResetValve = false;
            MCloseUV = false;
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
                MResetValve = Convert.ToBoolean(info[index++]);
                MCloseUV = Convert.ToBoolean(info[index++]);
            }
            catch { }
        }
    }
}
