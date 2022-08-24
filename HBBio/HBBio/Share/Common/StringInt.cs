using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Share
{
    /**
     * ClassName: StringInt
     * Description: 适用于名称+选择框
     * Version: 1.0
     * Create:  2021/02/03
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    [Serializable]
    public class StringInt : DlyNotifyPropertyChanged
    {
        /// <summary>
        /// 名称
        /// </summary>
        private string m_name = "";
        public string MName
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }

        /// <summary>
        /// 选择
        /// </summary>
        private int m_index = 0;
        public int MIndex
        {
            get
            {
                return m_index;
            }
            set
            {
                m_index = value;
                OnPropertyChanged("MIndex");
            }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="index"></param>
        public StringInt(string name, int index)
        {
            MName = name;
            MIndex = index;
        }

        /// <summary>
        /// 返回副本
        /// </summary>
        /// <returns></returns>
        public StringInt Clone()
        {
            return new StringInt(MName, MIndex);
        }


        public static List<StringInt> GetItemsSource(string[] arr)
        {
            List<StringInt> list = new List<StringInt>();
            for (int i = 0; i < arr.Length; i++)
            {
                list.Add(new StringInt(arr[i], i));
            }

            return list;
        }

        public static List<StringInt> GetItemsSource(List<string> listStr)
        {
            List<StringInt> list = new List<StringInt>();
            for (int i = 0; i < listStr.Count; i++)
            {
                list.Add(new StringInt(listStr[i], i));
            }

            return list;
        }
    }
}
