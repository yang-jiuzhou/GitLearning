using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.MethodEdit
{
    public abstract class BaseGroupVM : DlyNotifyPropertyChanged
    {
        public MethodBaseValue MMethodBaseValue { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public EnumGroupType MType { get; set; }
        /// <summary>
        /// 显隐
        /// </summary>
        public Visibility MVisible { get; set; }


        public BaseGroupVM(MethodBaseValue methodBaseValue)
        {
            MVisible = Visibility.Collapsed;

            MMethodBaseValue = methodBaseValue;          
        }

        /// <summary>
        /// 改变基本单位
        /// </summary>
        /// <param name="methodBaseValue"></param>
        public abstract void ChangeEnumBase(MethodBaseValue methodBaseValue);
    }
}
