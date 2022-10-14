using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    [Serializable]
    public class ConfAS : IDBSetGet
    {
        //气泡最小值
        public double MSize { get; set; }
        //延迟长度              
        public double MDelayLength { get; set; }
        //延迟单位   
        public EnumBase MDelayUnit { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public ConfAS()
        {
            MSize = 50;
            MDelayLength = 0;
            MDelayUnit = EnumBase.T;
        }

        /// <summary>
        /// 返回写入数据库的信息
        /// </summary>
        /// <param name="split"></param>
        /// <returns></returns>
        public string GetDBInfo(string split)
        {
            StringBuilderSplit sb = new StringBuilderSplit(split);
            sb.Append(MSize);
            sb.Append(MDelayLength);
            sb.Append(MDelayUnit);
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

                MSize = Convert.ToDouble(info[index++]);
                MDelayLength = Convert.ToDouble(info[index++]);
                MDelayUnit = (EnumBase)Enum.Parse(typeof(EnumBase), info[index++]);
            }
            catch
            { }
        }
    }
}
