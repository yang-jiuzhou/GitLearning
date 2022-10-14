using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class ConfpHCdUVVM
    {
        #region 属性
        public ConfpHCdUV MItem { get; set; }

        public string MName
        {
            get
            {
                return MItem.MName;
            }
            set
            {
                MItem.MName = value;
            }
        }
        public double MVol
        {
            get
            {
                return MItem.MVol;
            }
            set
            {
                MItem.MVol = value;
            }
        }
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="item"></param>
        public ConfpHCdUVVM(ConfpHCdUV item)
        {
            MItem = item;
        }
    }
}
