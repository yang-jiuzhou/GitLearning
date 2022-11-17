using HBBio.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: MixtureGridItem
     * Description: 混合列表单元行
     * Version: 1.0
     * Create:  2020/11/27
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    [Serializable]
    public class MixtureGridItem
    {
        public string MNote { get; set; }
        public int MFillSystem { get; set; }
        public BaseTVCV MBaseTVCV { get; set; }
        public FlowVolLen MFlowVolLenSample { get; set; }
        public FlowVolLen MFlowVolLenSystem { get; set; }
        public double MPerBS { get; set; }
        public double MPerBE { get; set; }
        public double MPerCS { get; set; }
        public double MPerCE { get; set; }
        public double MPerDS { get; set; }
        public double MPerDE { get; set; }
        public int MInS { get; set; }
        public int MInA { get; set; }
        public int MInB { get; set; }
        public int MInC { get; set; }
        public int MInD { get; set; }
        public int MIJV { get; set; }
        public int MBPV { get; set; }
        public int MCPV { get; set; }
        public int MVOut { get; set; }
        public bool MMixer { get; set; }
        public bool MUVClear { get; set; }
        public List<ASMethodPara> MASParaList { get; set; }
        public double MIncubation { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public MixtureGridItem()
        {
            MNote = "";
            MFillSystem = 0;
            MBaseTVCV = new BaseTVCV();
            MFlowVolLenSample = new FlowVolLen();
            MFlowVolLenSystem = new FlowVolLen();
            MPerBS = 0;
            MPerBE = 0;
            MPerCS = 0;
            MPerCE = 0;
            MPerDS = 0;
            MPerDE = 0;
            MInS = 0;
            MInA = 0;
            MInB = 0;
            MInC = 0;
            MInD = 0;
            MIJV = 0;
            MBPV = 0;
            MCPV = 0;
            MVOut = 0;
            MMixer = false;
            MUVClear = false;
            MASParaList = new List<ASMethodPara>();
            foreach (ENUMASName it in Enum.GetValues(typeof(ENUMASName)))
            {
                MASParaList.Add(new ASMethodPara() { MName = it });
            }
            MIncubation = 0;
        }

        public bool Compare(MixtureGridItem item)
        {
            if (null == item)
            {
                return false;
            }

            if (!MNote.Equals(item.MNote)
                || MFillSystem != item.MFillSystem
                || !MBaseTVCV.Compare(item.MBaseTVCV)
                || !MFlowVolLenSample.Compare(item.MFlowVolLenSample)
                || !MFlowVolLenSystem.Compare(item.MFlowVolLenSystem)
                || MPerBS != item.MPerBS
                || MPerBE != item.MPerBE
                || MPerCS != item.MPerCS
                || MPerCE != item.MPerCE
                || MPerDS != item.MPerDS
                || MPerDE != item.MPerDE
                || MInS != item.MInS
                || MInA != item.MInA
                || MInB != item.MInB
                || MInC != item.MInC
                || MInD != item.MInD
                || MIJV != item.MIJV
                || MBPV != item.MBPV
                || MCPV != item.MCPV
                || MVOut != item.MVOut
                || MMixer != item.MMixer
                || MUVClear != item.MUVClear
                || MIncubation != item.MIncubation)
            {
                return false;
            }

            for (int i = 0; i < MASParaList.Count; i++)
            {
                if (!MASParaList[i].Compare(item.MASParaList[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
