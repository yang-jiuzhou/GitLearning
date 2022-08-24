using System;
using System.Windows;

namespace HBBio.SystemControl
{
    class LanguageHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="languagefileName"></param>
        public static void LoadLanguageFile(string languagefileName)
        {
            Application.Current.Resources.MergedDictionaries[0] = new ResourceDictionary()
            {
                Source = new Uri(languagefileName, UriKind.RelativeOrAbsolute)
            };
        }
    }
}
