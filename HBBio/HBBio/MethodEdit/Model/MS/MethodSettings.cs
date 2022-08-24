using HBBio.ColumnList;
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
     * ClassName: MethodSettings
     * Description: 方法设置
     * Version: 1.0
     * Create:  2020/05/27
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    [Serializable]
    public class MethodSettings
    {
        /********基本信息********/
        /// <summary>
        /// 用户名
        /// </summary>
        public string MUserName { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public string MCreateTime { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public string MModifyTime { get; set; }

        /********循环次数********/
        public int MLoop { get; set; }

        /********色谱柱参数********/
        /// <summary>
        /// 柱子ID
        /// </summary>
        public int MColumnId { get; set; }
        /// <summary>
        /// 柱子体积
        /// </summary>
        public double MColumnVol { get; set; }
        /// <summary>
        /// 柱子面积
        /// </summary>
        public double MColumnArea { get; set; }
        /// <summary>
        /// 柱位阀
        /// </summary>
        public int MCPV { get; set; }

        /********单位选择********/
        /// <summary>
        /// 基本单位
        /// </summary>
        public EnumBase MBaseUnit { get; set; }
        /// <summary>
        /// 流速单位
        /// </summary>
        public EnumFlowRate MFlowRateUnit { get; set; }
        /// <summary>
        /// 基本单位字符串
        /// </summary>
        public string MBaseStr { get; set; }
        /// <summary>
        /// 基本单位字符串
        /// </summary>
        public string MBaseUnitStr { get; set; }
        /// <summary>
        /// 流速单位字符串
        /// </summary>
        public string MFlowRateUnitStr { get; set; }

        /********流速设置********/
        /// <summary>
        /// 流速数值
        /// </summary>
        public double MFlowRate { get; set; }
        /// <summary>
        /// 体积流速
        /// </summary>
        public double MFlowVol { get; set; }


        /********入口阀选择********/
        /// <summary>
        /// 入口阀A
        /// </summary>
        public int MInA { get; set; }
        /// <summary>
        /// 入口阀B
        /// </summary>
        public int MInB { get; set; }
        /// <summary>
        /// 入口阀C
        /// </summary>
        public int MInC { get; set; }
        /// <summary>
        /// 入口阀D
        /// </summary>
        public int MInD { get; set; }
        /// <summary>
        /// 旁通阀
        /// </summary>
        public int MBPV { get; set; }


        /********紫外检测器********/
        public UVValue MUVValue { get; set; }


        /********气泡传感器********/
        public List<ASMethodPara> MASParaList { get; set; }


        /********警报警告********/
        public AlarmWarning MAlarmWarning { get; set; }


        /********其它设置********/
        /// <summary>
        /// 结果名称
        /// </summary>
        public ResultName MResultName { get; set; }
        /// <summary>
        /// 问题定义
        /// </summary>
        public DefineQuestions MDefineQuestions { get; set; }
        /// <summary>
        /// 方法记录
        /// </summary>
        public string MMethodNotes { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public MethodSettings()
        {
            MUserName = "";
            MCreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            MModifyTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            MLoop = 1;

            MColumnId = -1;
            MColumnVol = 1;
            MColumnArea = 1;
            MCPV = 0;

            MBaseUnit = EnumBase.T;
            MFlowRateUnit = EnumFlowRate.MLMIN;
            MBaseStr = ReadXaml.GetEnum(MBaseUnit);
            MBaseUnitStr = EnumBaseString.GetItemsSource()[(int)MBaseUnit].MName;
            switch (MFlowRateUnit)
            {
                case EnumFlowRate.MLMIN:
                    MFlowRateUnitStr = StaticValue.s_maxFlowVolUnit;
                    break;
                case EnumFlowRate.CMH:
                    MFlowRateUnitStr = StaticValue.s_maxFlowLenUnit;
                    break;
            }

            MFlowRate = 1;
            MFlowVol = 1;

            MInA = 0;
            MInB = 0;
            MInC = 0;
            MInD = 0;
            MBPV = 0;

            MUVValue = new UVValue();

            MASParaList = new List<ASMethodPara>();
            foreach (ENUMASName it in Enum.GetValues(typeof(ENUMASName)))
            {
                MASParaList.Add(new ASMethodPara() { MName = it });
            }

            MAlarmWarning = new AlarmWarning();

            MResultName = new ResultName();
            MDefineQuestions = new DefineQuestions();
            MMethodNotes = "";
        }
    }
}
