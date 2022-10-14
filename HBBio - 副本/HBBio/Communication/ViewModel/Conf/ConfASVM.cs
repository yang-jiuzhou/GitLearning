using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class ConfASVM
    {
        #region 属性
        public ConfAS MItem { get; set; }

        public double MSize
        {
            get
            {
                return MItem.MSize;
            }
            set
            {
                MItem.MSize = value;
            }
        }
        public double MDelayLength
        {
            get
            {
                return MItem.MDelayLength;
            }
            set
            {
                MItem.MDelayLength = value;
            }
        }
        public EnumBase MDelayUnit
        {
            get
            {
                return MItem.MDelayUnit;
            }
            set
            {
                MItem.MDelayUnit = value;
            }
        }
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="item"></param>
        public ConfASVM(ConfAS item)
        {
            MItem = item;
        }
    }
}
