using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: FlowValveLengthItem
     * Description: 柱在线清洗列表单元行
     * Version: 1.0
     * Create:  2020/11/27
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    [Serializable]
    public class FlowValveLengthItem
    {
        public string MNote { get; set; }
        public int MInA { get; set; }
        public int MInB { get; set; }
        public int MInC { get; set; }
        public int MInD { get; set; }
        public double MPerBS { get; set; }
        public double MPerBE { get; set; }
        public double MPerCS { get; set; }
        public double MPerCE { get; set; }
        public double MPerDS { get; set; }
        public double MPerDE { get; set; }
        public bool MFillSystem { get; set; }
        public BaseTVCV MBaseTVCV { get; set; }
        public FlowVolLen MFlowVolLen { get; set; }
        public int MBPV { get; set; }
        public int MVOut { get; set; }
        public double MIncubation { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public FlowValveLengthItem()
        {
            MNote = "";
            MInA = 0;
            MInB = 0;
            MInC = 0;
            MInD = 0;
            MPerBS = 0;
            MPerBE = 0;
            MPerCS = 0;
            MPerCE = 0;
            MPerDS = 0;
            MPerDE = 0;
            MFillSystem = false;
            MBaseTVCV = new BaseTVCV();
            MFlowVolLen = new FlowVolLen();
            MBPV = 0;
            MVOut = 0;
            MIncubation = 0;
        }
    }
}
