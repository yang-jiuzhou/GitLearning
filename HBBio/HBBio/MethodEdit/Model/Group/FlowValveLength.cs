using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: FlowValveLength
     * Description: 柱在线清洗
     * Version: 1.0
     * Create:  2020/05/27
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    [Serializable]
    public class FlowValveLength : BaseGroup
    {
        /// <summary>
        /// 行数据列表
        /// </summary>
        public List<FlowValveLengthItem> MList { get; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public FlowValveLength()
        {
            MType = EnumGroupType.FlowValveLength;

            MList = new List<FlowValveLengthItem>();
        }
    }
}
