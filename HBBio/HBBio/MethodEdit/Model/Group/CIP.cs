using HBBio.Communication;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: CIP
     * Description: 系统冲洗
     * Version: 1.0
     * Create:  2020/05/27
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    [Serializable]
    public class CIP : BaseGroup
    {
        /// <summary>
        /// 备注
        /// </summary>
        public string MNote { get; set; }
        /// <summary>
        /// 暂停
        /// </summary>
        public bool MPause { get; set; }
        /// <summary>
        /// 流速设置
        /// </summary>
        public FlowRate MFlowRate { get; set; }
        /// <summary>
        /// 单位体积
        /// </summary>
        public double MVolumePerPosition { get; set; }

        public List<CIPItem> MListInA { get; set; }
        public List<CIPItem> MListInB { get; set; }
        public List<CIPItem> MListInC { get; set; }
        public List<CIPItem> MListInD { get; set; }
        public List<CIPItem> MListInS { get; set; }
        public List<CIPItem> MListCPV { get; set; }
        public List<CIPItem> MListOut { get; set; }
        /// <summary>
        /// 总体积
        /// </summary>
        public double MVolumeTotal { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public CIP()
        {
            MType = EnumGroupType.CIP;

            MNote = "";
            MPause = false;
            MFlowRate = new FlowRate();
            MVolumePerPosition = 10;

            MListInA = new List<CIPItem>();
            for (int i = 0; i < EnumInAInfo.Count; i++)
            {
                CIPItem item = new CIPItem(false, ENUMValveName.InA, EnumInAInfo.NameList[i]);
                MListInA.Add(item);
            }
            if (0 != MListInA.Count)
            {
                MListInA[0].MIsSelected = true;
            }

            MListInB = new List<CIPItem>();
            for (int i = 0; i < EnumInBInfo.Count; i++)
            {
                CIPItem item = new CIPItem(false, ENUMValveName.InB, EnumInBInfo.NameList[i]);
                MListInB.Add(item);
            }

            MListInC = new List<CIPItem>();
            for (int i = 0; i < EnumInCInfo.Count; i++)
            {
                CIPItem item = new CIPItem(false, ENUMValveName.InC, EnumInCInfo.NameList[i]);
                MListInC.Add(item);
            }

            MListInD = new List<CIPItem>();
            for (int i = 0; i < EnumInDInfo.Count; i++)
            {
                CIPItem item = new CIPItem(false, ENUMValveName.InD, EnumInDInfo.NameList[i]);
                MListInD.Add(item);
            }

            MListInS = new List<CIPItem>();
            for (int i = 0; i < EnumInSInfo.Count; i++)
            {
                CIPItem item = new CIPItem(false, ENUMValveName.InS, EnumInSInfo.NameList[i]);
                MListInS.Add(item);
            }

            MListCPV = new List<CIPItem>();
            for (int i = 0; i < EnumCPVInfo.Count; i++)
            {
                CIPItem item = new CIPItem(false, ENUMValveName.CPV_1, EnumCPVInfo.NameList[i]);
                MListCPV.Add(item);
            }
            if (0 != MListCPV.Count)
            {
                MListCPV[0].MIsSelected = true;
            }

            MListOut = new List<CIPItem>();
            for (int i = 0; i < EnumOutInfo.Count; i++)
            {
                CIPItem item = new CIPItem(false, ENUMValveName.Out, EnumOutInfo.NameList[i]);
                MListOut.Add(item);
            }
            if (0 != MListOut.Count)
            {
                MListOut[0].MIsSelected = true;
            }

            MVolumeTotal = 10;
        }
    }
}
