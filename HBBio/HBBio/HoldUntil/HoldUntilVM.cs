using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.HoldUntil
{
    public class HoldUntilVM
    {
        #region 属性
        public HoldUntil MItem { get; set; }

        public double MLength
        {
            get
            {
                return MItem.MLength;
            }
            set
            {
                MItem.MLength = value;
            }
        }
        public EnumBase MBase
        {
            get
            {
                return MItem.MBase;
            }
            set
            {
                MItem.MBase = value;
            }
        }
        public double MStart
        {
            get
            {
                return MItem.MStart;
            }
            set
            {
                MItem.MStart = value;
            }
        }
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="item"></param>
        public HoldUntilVM(HoldUntil item)
        {
            MItem = item;
        }
    }
}
