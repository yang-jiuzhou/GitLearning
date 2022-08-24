using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.Communication
{
    class ReadXamlCommunication
    {
        public const string C_DelComError = "Com_DelComError";          //该配置存在数据,不可删除!
        public const string C_ComError = "Com_ComError";                //通讯故障

        /// <summary>
        /// 字符串-该配置存在数据,不可删除!
        /// </summary>
        public static string S_DelComError
        {
            get
            {
                return (string)Application.Current.Resources[C_DelComError];
            }
        }

        /// <summary>
        /// 字符串-通讯故障
        /// </summary>
        public static string S_ComError
        {
            get
            {
                return (string)Application.Current.Resources[C_ComError];
            }
        }
    }
}
