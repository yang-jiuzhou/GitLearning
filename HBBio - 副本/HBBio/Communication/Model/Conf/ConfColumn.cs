using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    [Serializable]
    public class ConfColumn : IDBSetGet
    {
        //柱体积
        public double MColumnVol { get; set; }
        //柱直径
        public double MColumnDiameter { get; set; }
        //柱高   
        public double MColumnHeight { get; set; }
        /// <summary>
        /// 柱面积
        /// </summary>
        public double MColumnArea
        {
            get
            {
                return DlyBase.PI * MColumnDiameter * MColumnDiameter / 4;
            }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        public ConfColumn()
        {
            MColumnVol = DlyBase.PI;
            MColumnDiameter = 2;
            MColumnHeight = 1;
        }

        /// <summary>
        /// 返回写入数据库的信息
        /// </summary>
        /// <param name="split"></param>
        /// <returns></returns>
        public string GetDBInfo(string split)
        {
            StringBuilderSplit sb = new StringBuilderSplit(split);
            sb.Append(MColumnVol);
            sb.Append(MColumnDiameter);
            sb.Append(MColumnHeight);
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
                MColumnVol = Convert.ToDouble(info[index++]);
                MColumnDiameter = Convert.ToDouble(info[index++]);
                MColumnHeight = Convert.ToDouble(info[index++]);
            }
            catch { }
        }
    }
}