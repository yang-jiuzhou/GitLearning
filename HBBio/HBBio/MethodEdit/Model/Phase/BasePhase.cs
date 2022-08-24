using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: BasePhase
     * Description: 方法阶段抽象类，所有阶段必须继承此基类
     * Version: 1.0
     * Create:  2020/07/31
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    [Serializable]
    public abstract class BasePhase
    {
        public List<BaseGroup> MListGroup { get; set; }

        public EnumPhaseType MType { get; set; }    //类型
        public string MNameType { get; set; }       //类型字符串
        public string MNamePhase { get; set; }      //阶段名称
        public List<string> MNameStep { get; set; } //阶段子步骤名称列表

        public List<double> MStepT { get; set; }    //时间列表
        public List<double> MStepV { get; set; }    //体积列表
        public List<double> MStepCV { get; set; }   //柱体积列表   
        public List<double> MPerA { get; set; }     //A百分比列表
        public List<double> MPerB { get; set; }     //B百分比列表
        public List<double> MPerC { get; set; }     //C百分比列表
        public List<double> MPerD { get; set; }     //D百分比列表

        private static double s_perB = 0;
        private static double s_perC = 0;
        private static double s_perD = 0;


        /// <summary>
        /// 构造函数
        /// </summary>
        public BasePhase()
        {
            MListGroup = new List<BaseGroup>();      

            MStepT = new List<double>();
            MStepV = new List<double>();
            MStepCV = new List<double>();

            MPerA = new List<double>();
            MPerB = new List<double>();
            MPerC = new List<double>();
            MPerD = new List<double>();

            MNameStep = new List<string>();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="type"></param>
        public void Init(EnumPhaseType type, string nameType = null)
        {
            MType = type;
            switch (type)
            {
                case EnumPhaseType.Personal:
                    MNameType = nameType;
                    MNamePhase = nameType;
                    break;
                default:
                    MNameType = Share.ReadXaml.GetEnum(type, "ME_EnumPhaseType_");
                    MNamePhase = Share.ReadXaml.GetEnum(type, "ME_EnumPhaseType_");
                    break;
            }

            GroupFactory groupFactory = new GroupFactory();
            switch (type)
            {
                case EnumPhaseType.ColumnCIP:
                    MListGroup.Add(groupFactory.GetGroup(EnumGroupType.FlowValveLength));
                    break;
                case EnumPhaseType.SampleApplication:
                    MListGroup.Add(groupFactory.GetGroup(EnumGroupType.FlowRate));
                    MListGroup.Add(groupFactory.GetGroup(EnumGroupType.BPV));
                    MListGroup.Add(groupFactory.GetGroup(EnumGroupType.SampleApplicationTech));
                    break;
                case EnumPhaseType.ColumnWash:
                    MListGroup.Add(groupFactory.GetGroup(EnumGroupType.FlowRate));
                    MListGroup.Add(groupFactory.GetGroup(EnumGroupType.ValveSelection));
                    MListGroup.Add(groupFactory.GetGroup(EnumGroupType.PHCDUVUntil));
                    MListGroup.Add(groupFactory.GetGroup(EnumGroupType.CollValveCollector));

                    ((PHCDUVUntil)MListGroup[2]).MHeaderText = "冲洗至";
                    break;
                case EnumPhaseType.Elution:
                    MListGroup.Add(groupFactory.GetGroup(EnumGroupType.FlowRate));
                    MListGroup.Add(groupFactory.GetGroup(EnumGroupType.ValveSelection));
                    MListGroup.Add(groupFactory.GetGroup(EnumGroupType.FlowRatePer));
                    MListGroup.Add(groupFactory.GetGroup(EnumGroupType.CollValveCollector));

                    ((ValveSelection)MListGroup[1]).MVisibPer = false;
                    ((ValveSelection)MListGroup[1]).MVisibWash = false;
                    break;
                case EnumPhaseType.Equilibration:
                    MListGroup.Add(groupFactory.GetGroup(EnumGroupType.UVReset));
                    MListGroup.Add(groupFactory.GetGroup(EnumGroupType.FlowRate));
                    MListGroup.Add(groupFactory.GetGroup(EnumGroupType.ValveSelection));
                    MListGroup.Add(groupFactory.GetGroup(EnumGroupType.PHCDUVUntil));

                    ((PHCDUVUntil)MListGroup[3]).MHeaderText = "平衡至";
                    break;
                case EnumPhaseType.Miscellaneous:
                    break;
                case EnumPhaseType.SystemCIP:
                    MListGroup.Add(groupFactory.GetGroup(EnumGroupType.CIP));
                    break;
                case EnumPhaseType.Personal:
                    {
                        MethodManager methodManager = new MethodManager();
                        string info = null;
                        if (null == methodManager.GetPhase(nameType, ref info))
                        {
                            string[] arr = info.Split(';');
                            for (int i = 0; i < arr.Length - 1; i++)
                            {
                                string[] arr2 = arr[i].Split('&');
                                EnumGroupType enumGroupType = (EnumGroupType)Enum.Parse(typeof(EnumGroupType), arr2[0]);
                                MListGroup.Add(groupFactory.GetGroup(enumGroupType));
                                switch (enumGroupType)
                                {
                                    case EnumGroupType.ValveSelection:
                                        ((ValveSelection)MListGroup[i]).MVisibPer = Convert.ToBoolean(arr2[1]);
                                        ((ValveSelection)MListGroup[i]).MVisibWash = Convert.ToBoolean(arr2[2]);
                                        break;
                                    case EnumGroupType.PHCDUVUntil:
                                        ((PHCDUVUntil)MListGroup[i]).MHeaderText = arr2[1];
                                        break;
                                }
                            }
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// 重新计数
        /// </summary>
        public void ClearList()
        {
            MStepT.Clear();
            MStepV.Clear();
            MStepCV.Clear();

            MPerA.Clear();
            MPerB.Clear();
            MPerC.Clear();
            MPerD.Clear();

            MNameStep.Clear();
        }

        public static void ClearStatic()
        {
            s_perB = 0;
            s_perC = 0;
            s_perD = 0;
        }

        protected void AddStep(string name
            , BaseTVCV item
            , double perBS = -1, double perCS = -1, double perDS = -1
            , double perBE = -1, double perCE = -1, double perDE = -1)
        {
            MNameStep.Add(name);

            MStepT.Add(item.MT);
            MStepV.Add(item.MV);
            MStepCV.Add(item.MCV);

            if (-1 == perBS)
            {
                perBS = s_perB;
                perCS = s_perC;
                perDS = s_perD;
                perBE = s_perB;
                perCE = s_perC;
                perDE = s_perD;
            }
            else
            {
                s_perB = perBE;
                s_perC = perCE;
                s_perD = perDE;
            }

            MPerA.Add(100 - perBS - perCS - perDS);
            MPerB.Add(perBS);
            MPerC.Add(perCS);
            MPerD.Add(perDS);

            MPerA.Add(100 - perBE - perCE - perDE);
            MPerB.Add(perBE);
            MPerC.Add(perCE);
            MPerD.Add(perDE);
        }

        protected void AddStep(string name
            , double t, double v, double cv
            , double perBS = -1, double perCS = -1, double perDS = -1
            , double perBE = -1, double perCE = -1, double perDE = -1)
        {
            MNameStep.Add(ReadXaml.GetResources(name));

            MStepT.Add(t);
            MStepV.Add(v);
            MStepCV.Add(cv);

            if (-1 == perBS)
            {
                perBS = s_perB;
                perCS = s_perC;
                perDS = s_perD;
                perBE = s_perB;
                perCE = s_perC;
                perDE = s_perD;
            }
            else
            {
                s_perB = perBE;
                s_perC = perCE;
                s_perD = perDE;
            }

            MPerA.Add(100 - perBS - perCS - perDS);
            MPerB.Add(perBS);
            MPerC.Add(perCS);
            MPerD.Add(perDS);

            MPerA.Add(100 - perBE - perCE - perDE);
            MPerB.Add(perBE);
            MPerC.Add(perCE);
            MPerD.Add(perDE);
        }

        protected void UpdatePer(double perB, double perC, double perD)
        {
            s_perB = perB;
            s_perC = perC;
            s_perD = perD;
        }

        /// <summary>
        /// 计算阶段长度
        /// </summary>
        public abstract string StatisticsAllStep(double columnVol);
    }
}
