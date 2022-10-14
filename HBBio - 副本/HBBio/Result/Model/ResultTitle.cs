using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Result
{
    /**
    * ClassName: ResultTitle
    * Description: 运行结果标题类
    * Version: 1.0
    * Create:  2018/05/21
    * Author:  wangkai
    * Company: jshanbon
    **/
    public class ResultTitle
    {
        public int MID { get; set; }
        public string MName { get; set; }
        public int MProjectID { get; set; }
        public int MUserID { get; set; }
        public EnumResultIconType MType { get; set; }
        public DateTime MBeginTime { get; set; }
        public DateTime MEndTime { get; set; }

        //是否选中
        public bool MCheck { get; set; }
        //用户名
        public string MUserName { get; set; }
        //开始时间
        public string MBeginTimeStr
        {
            get
            {
                return MBeginTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
        //结束时间
        public string MEndTimeStr
        {
            get
            {
                return MEndTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        //文件夹图标
        private string m_iconManual = "/Bio-LabChrom;component/Image/manual.png";
        private string m_iconMethod = "/Bio-LabChrom;component/Image/method.png";
        private string m_iconMethodQueue = "/Bio-LabChrom;component/Image/methodQueue.png";
        public string MIcon
        {
            get
            {
                switch (MType)
                {
                    case EnumResultIconType.Method:
                        return m_iconMethod;
                    case EnumResultIconType.MethodQueue:
                        return m_iconMethodQueue;
                    default:
                        return m_iconManual;
                }
            }
        }       


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        public ResultTitle(int id, string name, int projectID, int userID, EnumResultIconType type, DateTime begin, DateTime end)
        {
            MID = id;
            MName = name;
            MProjectID = projectID;
            MUserID = userID;
            MType = type;
            MBeginTime = begin;
            MEndTime = end;
        }
    }


    /// <summary>
    /// 结果的类型枚举
    /// </summary>
    public enum EnumResultIconType
    {
        Manual,
        Method,
        MethodQueue
    }
}
