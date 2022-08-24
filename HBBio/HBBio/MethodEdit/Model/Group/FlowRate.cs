using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: FlowRate
     * Description: 流速
     * Version: 1.0
     * Create:  2020/05/27
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    [Serializable]
    public class FlowRate : BaseGroup
    {
        public bool MEnableSameMS { get; set; }
        public FlowVolLen MFlowVolLen { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public FlowRate()
        {
            MType = EnumGroupType.FlowRate;

            MEnableSameMS = false;
            MFlowVolLen = new FlowVolLen();
        }
    }
}