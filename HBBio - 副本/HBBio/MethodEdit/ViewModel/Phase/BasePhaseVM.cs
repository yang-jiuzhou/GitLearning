using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.MethodEdit
{
    public abstract class BasePhaseVM : DlyNotifyPropertyChanged
    {
        public BasePhase MItemBase { get { return m_item; } }
        protected BasePhase m_item = null;

        /// <summary>
        /// 共享参数
        /// </summary>
        public MethodBaseValue MMethodBaseValue { get; set; }

        public List<BaseGroupVM> MListGroup { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public BasePhaseVM(MethodBaseValue methodBaseValue)
        {
            MMethodBaseValue = methodBaseValue;

            MListGroup = new List<BaseGroupVM>();
        }

        /// <summary>
        /// 改变基本单位
        /// </summary>
        /// <param name="methodBaseValue"></param>
        public abstract void ChangeEnumBase(MethodBaseValue methodBaseValue);
    }
}
