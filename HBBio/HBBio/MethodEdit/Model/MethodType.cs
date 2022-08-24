using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: MethodType
     * Description: 方法的类型
     * Version: 1.0
     * Create:  2020/11/12
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    public class MethodType
    {
        /// <summary>
        /// 标识
        /// </summary>
        public int MID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string MName { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public EnumMethodType MType { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string MIcon
        {
            get
            {
                switch (MType)
                {
                    case EnumMethodType.MethodQueue:
                        return m_iconMethodQueue;
                    default:
                        return m_iconMethod;
                }
            }
        }

        /// <summary>
        /// 方法图标
        /// </summary>
        private const string m_iconMethod = "/Bio-LabChrom;component/Image/method.png";
        /// <summary>
        /// 序列图标
        /// </summary>
        private const string m_iconMethodQueue = "/Bio-LabChrom;component/Image/methodQueue.png";


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public MethodType(int id, string name, EnumMethodType type)
        {
            MID = id;
            MName = name;
            MType = type;
        }
    }
}
