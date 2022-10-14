using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.Collection
{
    public class ReadXamlCollection
    {
        public const string C_CollPositionDefault = "Coll_Position_Default";           //默认
        public const string C_CollColl = "Coll_Coll";           //收集
        public const string C_CollWaste = "Coll_Waste";         //排废
        public const string C_CollDelay = "Coll_Delay";         //延迟切换
        public const string C_CollMarkM = "Coll_Mark_M";        //手动收集标签
        public const string C_CollMarkA = "Coll_Mark_A";        //自动收集标签
        public const string C_CollOver = "Coll_Over";           //收集已满


        /// <summary>
        /// 字符串-默认
        /// </summary>
        public static string S_Default
        {
            get
            {
                return (string)Application.Current.Resources[C_CollPositionDefault];
            }
        }

        /// <summary>
        /// 字符串-收集
        /// </summary>
        public static string S_CollColl
        {
            get
            {
                return (string)Application.Current.Resources[C_CollColl];
            }
        }

        /// <summary>
        /// 字符串-排废
        /// </summary>
        public static string S_CollWaste
        {
            get
            {
                return (string)Application.Current.Resources[C_CollWaste];
            }
        }

        /// <summary>
        /// 字符串-延迟切换
        /// </summary>
        public static string S_CollDelay
        {
            get
            {
                return (string)Application.Current.Resources[C_CollDelay];
            }
        }

        /// <summary>
        /// 字符串-手动收集标签
        /// </summary>
        public static string S_CollMarkM
        {
            get
            {
                return (string)Application.Current.Resources[C_CollMarkM];
            }
        }

        /// <summary>
        /// 字符串-自动收集标签
        /// </summary>
        public static string S_CollMarkA
        {
            get
            {
                return (string)Application.Current.Resources[C_CollMarkA];
            }
        }

        /// <summary>
        /// 字符串-收集已满
        /// </summary>
        public static string S_CollOver
        {
            get
            {
                return (string)Application.Current.Resources[C_CollOver];
            }
        }
    }
}
