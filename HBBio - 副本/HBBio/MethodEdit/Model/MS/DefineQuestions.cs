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
     * ClassName: DefineQuestions
     * Description: 问题定义
     * Version: 1.0
     * Create:  2020/11/12
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    [Serializable]
    public class DefineQuestions
    {
        /// <summary>
        /// 问题描述
        /// </summary>
        public string MQuestion { get; set; }
        /// <summary>
        /// 答案类型
        /// </summary>
        public EnumAnswerType MType { get; set; }
        /// <summary>
        /// 默认答案
        /// </summary>
        public string MDefaultAnswer { get; set; }
        /// <summary>
        /// 范围最小值
        /// </summary>
        public double MMin { get; set; }
        /// <summary>
        /// 范围最大值
        /// </summary>
        public double MMax { get; set; }
        /// <summary>
        /// 选择列表
        /// </summary>
        public List<string> MChoiceList { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public DefineQuestions()
        {
            MQuestion = "";
            MType = EnumAnswerType.TextualInput;
            MDefaultAnswer = "";
            MMin = 0;
            MMax = 100;
            MChoiceList = new List<string>();
        }
    }
}
