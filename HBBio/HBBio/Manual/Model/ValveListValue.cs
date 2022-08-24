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
    public class ValveListValue
    {
        public bool m_update = false;

        public List<StringInt> MListValave { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public ValveListValue()
        {
            MListValave = new List<StringInt>();
            foreach (var it in ItemVisibility.s_listValve)
            {
                MListValave.Add(new StringInt("", 0));
            }
        }

        /// <summary>
        /// 清除临时变量
        /// </summary>
        public void Clear()
        {

        }
    }
}
