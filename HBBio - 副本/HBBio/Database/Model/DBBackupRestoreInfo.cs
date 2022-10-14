using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Database
{
    /**
     * ClassName: DBBackupRestoreInfo
     * Description: 数据库备份还原信息
     * Version: 1.0
     * Create:  2022/03/17
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    class DBBackupRestoreInfo
    {
        public string MBackupPathLocal { get; set; }
        public string MBackupIP { get; set; }
        public string MBackupUserName { get; set; }
        public string MBackupPwd { get; set; }
        public string MBackupPathRemote { get; set; }

        public string MRestorePathLocal { get; set; }
        public string MRestoreFileLocal { get; set; }       //无须存储
        public string MRestoreIP { get; set; }
        public string MRestoreUserName { get; set; }
        public string MRestorePwd { get; set; }
        public string MRestorePathRemote { get; set; }
        public string MRestoreFileRemote { get; set; }      //无须存储
    }
}
