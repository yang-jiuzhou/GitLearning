using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: ResultName
     * Description: 结果名称
     * Version: 1.0
     * Create:  2020/11/12
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    [Serializable]
    public class ResultName
    {
        public EnumResultType MType { get; set; }
        public string MDlyName { get; set; }
        public bool MUnique { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public ResultName()
        {
            MType = EnumResultType.NoName;
            MDlyName = "";
            MUnique = false;
        }
    }
}
