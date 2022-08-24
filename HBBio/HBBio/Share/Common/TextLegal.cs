using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HBBio.Share
{
    /**
     * ClassName: TextLegal
     * Description: 名称密码等合法性
     * Version: 2.0
     * Create:  2019/12/13
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class TextLegal
    {
        private const int C_MaxLength = 64;


        /// <summary>
        /// 验证名称是否合法
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool NameLegal(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length > C_MaxLength)
            {
                return false;
            }

            Regex rg = new Regex(@"^[0-9a-zA-Z\u4e00-\u9fa5]+$");
            if (rg.IsMatch(name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证文件名是否合法
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool FileNameLegal(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length > C_MaxLength)
            {
                return false;
            }

            Regex rg = new Regex(@"^[0-9a-zA-Z\u4e00-\u9fa5\s@$%&+_-]+$");
            if (rg.IsMatch(name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 验证密码是否合法
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool PwdLegal(string pwd, string name)
        {
            if (string.IsNullOrEmpty(pwd) || pwd.Length < 6 || pwd.Length > C_MaxLength || name.Contains(pwd))
            {
                return false;
            }

            int count = 0;
            Regex rg = new Regex(@"^[0-9a-zA-Z~!@#$%^&*,./_]+$");
            if (rg.IsMatch(pwd))
            {
                Regex rg1 = new Regex(@"[0-9]");
                if (rg1.IsMatch(pwd))
                {
                    count++;
                }
                Regex rg2 = new Regex(@"[a-zA-Z]");
                if (rg2.IsMatch(pwd))
                {
                    count++;
                }
                Regex rg3 = new Regex(@"[~!@#$%^&*,./_-]");
                if (rg3.IsMatch(pwd))
                {
                    count++;
                }
                if (count > 1)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool DoubleLegal(string val)
        {
            if (Regex.IsMatch(val, @"^-?\d+\.\d+$") || Regex.IsMatch(val, @"^-?\d+$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
