using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Database
{
    /**
     * ClassName: DBBackupRestoreTable
     * Description: 数据库备份还原路径表
     * Version: 1.0
     * Create:  2020/11/12
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    class DBBackupRestoreTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DBBackupRestoreTable()
        {
            m_tableName = "DBBackupRestoreTable";
        }

        /// <summary>
        /// 插入默认值
        /// </summary>
        /// <returns></returns>
        protected override string AddDefaultValue()
        {
            return InsertRow(new DBBackupRestoreInfo());
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns></returns>
        protected override string CreateTable()
        {
            return SqlCreateTable(
                @"[BackupPathLocal] [nvarchar](Max),
                [BackupIP] [nvarchar](Max),
                [BackupUserName] [nvarchar](Max),
                [BackupPwd] [nvarchar](Max),
                [BackupPathRemote] [nvarchar](Max),
                [RestorePathLocal] [nvarchar](Max),
                [RestoreIP] [nvarchar](Max),
                [RestoreUserName] [nvarchar](Max),
                [RestorePwd] [nvarchar](Max),
                [RestorePathRemote] [nvarchar](Max)"
                );
        }

        /// <summary>
        /// 检查表
        /// </summary>
        /// <returns></returns>
        public override string CheckTable()
        {
            bool exist = false;
            string error = ExistTable(ref exist);
            if (null == error)
            {
                if (exist)
                {
                    List<string> listName = new List<string>();
                    List<string> listType = new List<string>();
                    listName.Add("BackupPathLocal");
                    listName.Add("BackupIP");
                    listName.Add("BackupUserName");
                    listName.Add("BackupPwd");
                    listName.Add("BackupPathRemote");
                    listName.Add("RestorePathLocal");
                    listName.Add("RestoreIP");
                    listName.Add("RestoreUserName");
                    listName.Add("RestorePwd");
                    listName.Add("RestorePathRemote");

                    error = CreateNewTable(listName, listType, false);
                }
                else
                {
                    error += CreateTable();
                    error += AddDefaultValue();
                }
            }

            return error;
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string InsertRow(DBBackupRestoreInfo item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + item.MBackupPathLocal);
            sb.Append("','" + item.MBackupIP);
            sb.Append("','" + item.MBackupUserName);
            sb.Append("','" + item.MBackupPwd);
            sb.Append("','" + item.MBackupPathRemote);
            sb.Append("','" + item.MRestorePathLocal);
            sb.Append("','" + item.MRestoreIP);
            sb.Append("','" + item.MRestoreUserName);
            sb.Append("','" + item.MRestorePwd);
            sb.Append("','" + item.MRestorePathRemote + "'");

            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 修改行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string UpdateRow(DBBackupRestoreInfo item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("BackupPathLocal='" + item.MBackupPathLocal);
            sb.Append("',BackupIP='" + item.MBackupIP);
            sb.Append("',BackupUserName='" + item.MBackupUserName);
            sb.Append("',BackupPwd='" + item.MBackupPwd);
            sb.Append("',BackupPathRemote='" + item.MBackupPathRemote);
            sb.Append("',RestorePathLocal='" + item.MRestorePathLocal);
            sb.Append("',RestoreIP='" + item.MRestoreIP);
            sb.Append("',RestoreUserName='" + item.MRestoreUserName);
            sb.Append("',RestorePwd='" + item.MRestorePwd);
            sb.Append("',RestorePathRemote='" + item.MRestorePathRemote + "'");

            return SqlUpdateRow(sb.ToString());
        }

        /// <summary>
        /// 获取行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string SelectRow(out DBBackupRestoreInfo item)
        {
            string error = null;
            item = null;

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT * FROM " + m_tableName, out reader);
                if (null == error)
                {
                    if (reader.Read())//匹配
                    {
                        int index = 0;
                        item = new DBBackupRestoreInfo();
                        item.MBackupPathLocal = reader.GetString(index++);
                        item.MBackupIP = reader.GetString(index++);
                        item.MBackupUserName = reader.GetString(index++);
                        item.MBackupPwd = reader.GetString(index++);
                        item.MBackupPathRemote = reader.GetString(index++);
                        item.MRestorePathLocal = reader.GetString(index++);
                        item.MRestoreIP = reader.GetString(index++);
                        item.MRestoreUserName = reader.GetString(index++);
                        item.MRestorePwd = reader.GetString(index++);
                        item.MRestorePathRemote = reader.GetString(index++);
                    }
                    else
                    {
                        error = Share.ReadXaml.S_ErrorNoData;
                    }
                    CloseConnAndReader();
                }
            }
            catch (Exception msg)
            {
                error = msg.Message;
            }

            return error;
        }
    }
}
