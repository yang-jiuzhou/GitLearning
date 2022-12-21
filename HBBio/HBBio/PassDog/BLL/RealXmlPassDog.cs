using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.PassDog
{
    class RealXmlPassDog
    {
        public const string C_LoginFail = "PassDog_LoginFail";          //登录失败
        public const string C_ReadFail = "PassDog_ReadFail";            //读信息失败
        public const string C_ExitFail = "PassDog_ExitFail";            //退出失败
        public const string C_MemoryFail = "PassDog_MemoryFail";        //读内存失败
        public const string C_SNFail = "PassDog_SNFail";                //序列号对比失败

        /// <summary>
        /// 登录失败
        /// </summary>
        public static string S_LoginFail
        {
            get
            {
                return (string)Application.Current.Resources[C_LoginFail];
            }
        }

        /// <summary>
        /// 读信息失败
        /// </summary>
        public static string S_ReadFail
        {
            get
            {
                return (string)Application.Current.Resources[C_ReadFail];
            }
        }

        /// <summary>
        /// 退出失败
        /// </summary>
        public static string S_ExitFail
        {
            get
            {
                return (string)Application.Current.Resources[C_ExitFail];
            }
        }

        /// <summary>
        /// 读内存失败
        /// </summary>
        public static string S_MemoryFail
        {
            get
            {
                return (string)Application.Current.Resources[C_MemoryFail];
            }
        }

        /// <summary>
        /// 序列号对比失败
        /// </summary>
        public static string S_SNFail
        {
            get
            {
                return (string)Application.Current.Resources[C_SNFail];
            }
        }
    }
}
