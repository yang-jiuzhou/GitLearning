using HBBio.Communication;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Manual
{
    [Serializable]
    public class ASListValue
    {
        public List<ASManualPara> MList { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public ASListValue()
        {
            MList = new List<ASManualPara>();
            foreach (ENUMASName it in Enum.GetValues(typeof(ENUMASName)))
            {
                MList.Add(new ASManualPara() { MName = it });
            }
        }

        /// <summary>
        /// 清除临时变量
        /// </summary>
        public void Clear()
        {
            foreach (var it in MList)
            {
                it.Clear();
            }
        }
    }
}