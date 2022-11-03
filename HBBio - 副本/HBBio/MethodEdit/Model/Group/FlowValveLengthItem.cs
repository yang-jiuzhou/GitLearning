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
        public int MFillSystem { get; set; }
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
            MFillSystem = 0;
            MBaseTVCV = new BaseTVCV();
            MFlowVolLen = new FlowVolLen();
            MBPV = 0;
            MVOut = 0;
            MIncubation = 0;
        }

        public bool Compare(FlowValveLengthItem item)
        {
            if (null == item)
            {
                return false;
            }

            if (!MNote.Equals(item.MNote)
                || MInA != item.MInA
                || MInB != item.MInB
                || MInC != item.MInC
                || MInD != item.MInD
                || MPerBS != item.MPerBS
                || MPerBE != item.MPerBE
                || MPerCS != item.MPerCS
                || MPerCE != item.MPerCE
                || MPerDS != item.MPerDS
                || MPerDE != item.MPerDE
                || MFillSystem != item.MFillSystem
                || !MBaseTVCV.Compare(item.MBaseTVCV)
                || !MFlowVolLen.Compare(item.MFlowVolLen)
                || MBPV != item.MBPV
                || MVOut != item.MVOut
                || MIncubation != item.MIncubation)
            {
                return false;
            }

            return true;
        }
    }
}
