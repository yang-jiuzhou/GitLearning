using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.MethodEdit
{
    [Serializable]
    public abstract class BaseGroup
    {
        /// <summary>
        /// 类型
        /// </summary>
        public EnumGroupType MType { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int MIndex { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseGroup()
        {
            MType = EnumGroupType.FlowValveLength;
            MIndex = 0;
        }
    }
}
