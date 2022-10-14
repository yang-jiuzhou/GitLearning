using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.SystemControl
{
    public enum EnumLanguage
    {
        Chinese,
        English
    }

    public class ConfCheckable
    {
        public EnumLanguage MEnumLanguage { get; set; }
        public bool MRememberSize { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public ConfCheckable()
        {

        }
    }
}
