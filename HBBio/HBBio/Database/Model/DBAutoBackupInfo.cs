using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Database
{
    /**
     * ClassName: DBAutoBackupInfo
     * Description: 数据库自动备份信息
     * Version: 1.0
     * Create:  2022/03/17
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    public class DBAutoBackupInfo
    {
        public bool MEnabled { get; set; }
        public int MFrequency { get; set; }
        public int MCount { get; set; }
        public bool MLocal { get; set; }
        public bool MRemote { get; set; }

        public string MPathLocal { get; set; }

        public string MIP { get; set; }
        public string MUserName { get; set; }
        public string MPwd { get; set; }
        public string MPathRemote { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public DBAutoBackupInfo()
        {
            MFrequency = 7;
            MCount = 3;
        }
    }
}
