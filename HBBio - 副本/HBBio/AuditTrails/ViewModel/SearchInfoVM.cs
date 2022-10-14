using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.AuditTrails
{
    /**
     * ClassName: SearchInfoVM
     * Description: 日志查找选项
     * Version: 1.0
     * Create:  2020/01/21
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class SearchInfoVM
    {
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime MDateTimeStart
        {
            get
            {
                return m_dateTimeStart;
            }
            set
            {
                m_dateTimeStart = value;
            }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime MDateTimeStop
        {
            get
            {
                return m_dateTimeStop;
            }
            set
            {
                m_dateTimeStop = value;
            }
        }
        /// <summary>
        /// 类别
        /// </summary>
        public string MType
        {
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
            }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string MUserName
        {
            get
            {
                return m_userName;
            }
            set
            {
                m_userName = value;
            }
        }
        /// <summary>
        /// 过滤
        /// </summary>
        public string MFilter
        {
            get
            {
                return m_filter;
            }
            set
            {
                m_filter = value;
            }
        }

        private DateTime m_dateTimeStart = DateTime.Today;
        private DateTime m_dateTimeStop = DateTime.Now;
        private string m_type;
        private string m_userName;
        private string m_filter = "";


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="userName"></param>
        public SearchInfoVM(string type, string userName)
        {
            MDateTimeStop = DateTime.Today.AddDays(1);
            MType = type;
            MUserName = userName;
        }
    }
}
