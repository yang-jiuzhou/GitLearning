using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HBBio.Share
{
    public class XmlSerialize
    {
        private static string s_strFile = Environment.CurrentDirectory + "method.xml";

        /*
         * xml序列化的方式只能保存public的字段和可读写的属性，对于private的字段或属性不能序列化
         */
        /// <summary>
        /// 采用xml方式进行序列化
        /// </summary>
        /// <param name="book"></param>
        public static void Serialize<T>(string strFile, T book)
        {
            using (FileStream fs = new FileStream(strFile, FileMode.CreateNew))
            {
                try
                {
                    XmlSerializer xmlSerialize = new XmlSerializer(typeof(T));
                    xmlSerialize.Serialize(fs, book);
                }
                catch (Exception ex)
                {
                    MessageBoxWin.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// 使用xml进行反序列化
        /// </summary>
        /// <returns></returns>
        public static T DeSerialize<T>(string strFile)
        {
            T val = default(T);
            using (FileStream fs = new FileStream(strFile, FileMode.Open))
            {
                try
                {
                    XmlSerializer xmlSerialize = new XmlSerializer(typeof(T));
                    val = (T)xmlSerialize.Deserialize(fs);
                }
                catch (Exception ex)
                {
                    MessageBoxWin.Show(ex.Message);
                }
            }
            return val;
        }
    }
}
