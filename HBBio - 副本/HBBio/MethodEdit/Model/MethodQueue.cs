using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: MethodQueue
     * Description: 方法队列
     * Version: 1.0
     * Create:  2020/11/12
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    [Serializable]
    public class MethodQueue
    {
        public int MID { get; set; }
        public int MCommunicationSetsID { get; set; }
        public int MProjectID { get; set; }
        public string MName { get; set; }
        public EnumMethodType MType { get; set; }
        public List<int> MMethodList { get; set; }
        public bool MOnly { get; set; }

        protected string m_split = "#~#";


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="communicationSetsID"></param>
        /// <param name="projectID"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public MethodQueue(int id, int communicationSetsID, int projectID, string name, EnumMethodType type = EnumMethodType.MethodQueue)
        {
            MID = id;
            MCommunicationSetsID = communicationSetsID;
            MProjectID = projectID;
            MName = name;
            MType = type;
            MMethodList = new List<int>();
        }

        public string GetMethodInfo()
        {
            StringBuilderSplit sb = new StringBuilderSplit(m_split);

            for (int i = 0; i < MMethodList.Count; i++)
            {
                sb.Append(MMethodList[i]);
            }

            return sb.ToString();
        }

        public void SetMethodInfo(string info)
        {
            string[] list = System.Text.RegularExpressions.Regex.Split(info, m_split);
            try
            {
                int index = 0;
                while (index < list.Length - 1)
                {
                    MMethodList.Add(Convert.ToInt32(list[index++]));
                }
            }
            catch { }
        }
    }
}
