using HBBio.Communication;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: Method
     * Description: 方法
     * Version: 1.0
     * Create:  2020/11/12
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    [Serializable]
    public class Method
    {
        public int MID { get; set; }
        public int MCommunicationSetsID { get; set; }
        public int MProjectID { get; set; }
        public string MName { get; set; }
        public EnumMethodType MType { get; set; }
        public MethodSettings MMethodSetting { get; set; }
        public List<BasePhase> MPhaseList { get; set; }


        /// <summary>
        /// 无参构造函数
        /// </summary>
        public Method()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="communicationSetsID"></param>
        /// <param name="projectID"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public Method(int id, int communicationSetsID, int projectID, string name, EnumMethodType type = EnumMethodType.Method)
        {
            MID = id;
            MCommunicationSetsID = communicationSetsID;
            MProjectID = projectID;
            MName = name;
            MType = type;
            MMethodSetting = new MethodSettings();
            MPhaseList = new List<BasePhase>();
        }
    }
}
