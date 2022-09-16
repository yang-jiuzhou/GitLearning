using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.Administration
{
    public class ReadXamlAdministration
    {
        /// <summary>
        /// 默认权限
        /// </summary>
        public static string S_DefaultPermission { get { return Share.ReadXaml.GetResources("A_DefaultPermission"); } }

        /// <summary>
        /// 从xmal获取文本
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetTitle1(EnumTactics index)
        {
            return (string)Application.Current.Resources["A_T_" + index.ToString() + "Title1"];
        }
        public static string GetTitle2(EnumTactics index)
        {
            return (string)Application.Current.Resources["A_T_" + index.ToString() + "Title2"];
        }
        public static string GetUnit(EnumTactics index)
        {
            return (string)Application.Current.Resources["A_T_" + index.ToString() + "Unit"];
        }
        public static string GetInfo(EnumTactics index)
        {
            return ((string)Application.Current.Resources["A_T_" + index.ToString() + "Info"]).Replace("\\n", "\n");
        }
    }
}
