using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: SampleApplicationTech
     * Description: 上样技术
     * Version: 1.0
     * Create:  2020/05/27
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    [Serializable]
    public class SampleApplicationTech : BaseGroup
    {
        public EnumSAT MEnumSAT { get; set; }
        public int MInS { get; set; }
        public BaseTVCV MSampleTVCV { get; set; }
        public double MFillLoopWith { get; set; }
        public double MEmptyLoopWith { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public SampleApplicationTech()
        {
            MType = EnumGroupType.SampleApplicationTech;

            MEnumSAT = EnumSAT.ManualLoopFilling;
            MInS = 0;
            MSampleTVCV = new BaseTVCV();
            MFillLoopWith = 0;
            MEmptyLoopWith = 0;
        }
    }
}
