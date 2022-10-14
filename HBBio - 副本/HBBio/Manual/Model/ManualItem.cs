using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Manual
{
    public class ManualItem
    {
        public object MValue { get; set; }              //需要执行的值
        public string MNameCurr { get; set; }           //需要执行的信息名称
        public string MNameParent { get; set; }         //需要执行的信息的上级名称
        public string MTextCurr { get; set; }           //需要执行的信息文本
        public string MTextParent { get; set; }         //需要执行的信息的上级文本
        

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nameCurr"></param>
        /// <param name="nameParent"></param>
        /// <param name="textCurr"></param>
        /// <param name="textParent"></param>
        /// <param name="value"></param>
        public ManualItem(object value, string nameCurr, string headerCurr, string nameParent, string headerParent)
        {
            MValue = value;
            MNameCurr = nameCurr;
            MTextCurr = headerCurr;
            MNameParent = nameParent;
            MTextParent = headerParent;
        }
    }
}
