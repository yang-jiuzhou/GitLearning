using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.Manual
{
    class ReadXamlManual
    {
        /// <summary>
        /// 阀切换
        /// </summary>
        public const string C_ValveSwitch = "M_ValveSwitch";


        /// <summary>
        /// 字符串-阀切换
        /// </summary>
        public static string S_ValveSwitch
        {
            get
            {
                return (string)Application.Current.Resources[C_ValveSwitch];
            }
        }
    }
}
