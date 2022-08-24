using HBBio.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: CIPItem
     * Description: 冲洗单元
     * Version: 1.0
     * Create:  2020/05/27
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    [Serializable]
    public class CIPItem
    {
        public bool MIsSelected { get; set; }
        public ENUMValveName MType { get; set; }
        public string MValveName { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public CIPItem()
        {
            MIsSelected = false;
            MType = ENUMValveName.InA;
            MValveName = EnumInAInfo.NameList[0];
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="select"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        public CIPItem(bool select, ENUMValveName type, string name)
        {
            MIsSelected = select;
            MType = type;
            MValveName = name;
        }
    }
}
