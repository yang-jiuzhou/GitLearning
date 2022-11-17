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

        private static class ConfCheckableInner
        {
            public static ConfCheckable _stance = new ConfCheckable();
        }

        /// <summary>
        /// 单例引用
        /// </summary>
        /// <returns></returns>
        public static ConfCheckable GetInstance()
        {
            return ConfCheckableInner._stance;
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        private ConfCheckable()
        {

        }
    }
}
