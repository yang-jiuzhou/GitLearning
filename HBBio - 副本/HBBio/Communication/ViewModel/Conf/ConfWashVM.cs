using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class ConfWashVM
    {
        #region 属性
        public ConfWash MItem { get; set; }

        public double MWashTime
        {
            get
            {
                return MItem.MWashTime;
            }
            set
            {
                MItem.MWashTime = value;
            }
        }
        public double MWashFlowPer
        {
            get
            {
                return MItem.MWashFlowPer;
            }
            set
            {
                MItem.MWashFlowPer = value;
            }
        }
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="item"></param>
        public ConfWashVM(ConfWash item)
        {
            MItem = item;
        }
    }
}
