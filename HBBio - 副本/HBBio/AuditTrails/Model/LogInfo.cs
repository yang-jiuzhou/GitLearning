using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.AuditTrails
{
    /**
     * ClassName: LogInfo
     * Description: 日志行信息
     * Version: 1.0
     * Create:  2020/01/21
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    [Serializable]
    public class LogInfo
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string MType { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public string MDate { get; set; }
        /// <summary>
        /// 批处理(时间)
        /// </summary>
        public string MBatchT { get; set; }
        /// <summary>
        /// 批处理(体积)
        /// </summary>
        public string MBatchV { get; set; }
        /// <summary>
        /// 批处理(柱体积)
        /// </summary>
        public string MBatchCV { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string MUserName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string MDescription { get; set; }
        /// <summary>
        /// 旧值->新值
        /// </summary>
        public string MOperation { get; set; }
    }
}
