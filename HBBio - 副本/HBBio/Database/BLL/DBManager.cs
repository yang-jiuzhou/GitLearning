using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Database
{
    class DBManager
    {
        public string GetDBBackupRestore(out DBBackupRestoreInfo item)
        {
            DBBackupRestoreTable table = new DBBackupRestoreTable();
            return table.SelectRow(out item);
        }

        public string SetDBBackupRestore(DBBackupRestoreInfo item)
        {
            DBBackupRestoreTable table = new DBBackupRestoreTable();
            return table.UpdateRow(item);
        }

        public string GetDBAutoBackup(out DBAutoBackupInfo item)
        {
            DBAutoBackupTable table = new DBAutoBackupTable();
            return table.SelectRow(out item);
        }

        public string SetDBAutoBackup(DBAutoBackupInfo item)
        {
            DBAutoBackupTable table = new DBAutoBackupTable();
            return table.UpdateRow(item);
        }
    }
}
