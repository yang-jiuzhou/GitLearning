using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: ValveSelection
     * Description: 入口阀选择
     * Version: 1.0
     * Create:  2020/05/27
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    [Serializable]
    public class ValveSelection : BaseGroup
    {
        /// <summary>
        /// 同方法设置
        /// </summary>
        public bool MEnableSameMS { get; set; }
        public int MInA { get; set; }
        public int MInB { get; set; }
        public int MInC { get; set; }
        public int MInD { get; set; }
        public int MBPV { get; set; }
        /// <summary>
        /// 是否启用百分比
        /// </summary>
        public bool MVisibPer { get; set; }
        public double MPerB { get; set; }
        public double MPerC { get; set; }
        public double MPerD { get; set; }
        /// <summary>
        /// 是否启用清洗
        /// </summary>
        public bool MVisibWash { get; set; }
        /// <summary>
        /// 系统清洗
        /// </summary>
        public bool MEnableWash { get; set; }


        public ValveSelection()
        {
            MType = EnumGroupType.ValveSelection;

            MEnableSameMS = false;
            MInA = 0;
            MInB = 0;
            MInC = 0;
            MInD = 0;
            MVisibPer = true;
            MPerB = 0;
            MPerC = 0;
            MPerD = 0;
            MVisibWash = true;
            MEnableWash = false;
        }
    }
}
