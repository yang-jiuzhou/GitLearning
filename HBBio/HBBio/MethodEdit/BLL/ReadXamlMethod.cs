using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.MethodEdit
{
    class ReadXamlMethod
    {
        public const string C_PhaseName = "ME_PhaseTitle";              //阶段名称
        public const string C_MethodSettings = "ME_MethodSettings";     //方法设置
        public const string C_UVSettings = "ME_UVSettings";             //紫外检测器设置
        public const string C_ReplaceMethod = "ME_Desc_ReplaceMethod";  //替换方法
        public const string C_Run = "ME_Desc_Run";                      //方法或队列正在运行中


        /// <summary>
        /// 字符串-阶段名称
        /// </summary>
        public static string S_PhaseName
        {
            get
            {
                return (string)Application.Current.Resources[C_PhaseName];
            }
        }

        /// <summary>
        /// 字符串-方法设置
        /// </summary>
        public static string S_MethodSettings
        {
            get
            {
                return (string)Application.Current.Resources[C_MethodSettings];
            }
        }

        /// <summary>
        /// 字符串-紫外检测器设置
        /// </summary>
        public static string S_UVSettings
        {
            get
            {
                return (string)Application.Current.Resources[C_UVSettings];
            }
        }

        /// <summary>
        /// 字符串-替换方法
        /// </summary>
        public static string S_ReplaceMethod
        {
            get
            {
                return (string)Application.Current.Resources[C_ReplaceMethod];
            }
        }

        /// <summary>
        /// 字符串-方法或队列正在运行中
        /// </summary>
        public static string S_Run
        {
            get
            {
                return (string)Application.Current.Resources[C_Run];
            }
        }
    }
}
