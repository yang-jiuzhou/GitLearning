using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.HoldUntil
{
    public class HoldUntil
    {
        #region 属性
        public double MLength { get; set; }
        public EnumBase MBase { get; set; }
        public double MStart { get; set; }
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        public HoldUntil()
        {
            MLength = 0;
            MBase = EnumBase.T;
            MStart = 0;
        }
    }
}
