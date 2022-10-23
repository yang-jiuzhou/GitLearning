using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Database
{
    /**
     * ClassName: DBAutoBackupTable
     * Description: 数据库自动备份表
     * Version: 1.0
     * Create:  2020/11/12
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    class DBAutoBackupTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DBAutoBackupTable()
        {
            m_tableName = "DBAutoBackupTable";
        }

        /// <summary>
        /// 插入默认值
        /// </summary>
        /// <returns></returns>
        protected override string AddDefaultValue()
        {
            return InsertRow(new DBAutoBackupInfo());
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns></returns>
        protected override string CreateTable()
        {
            return SqlCreateTable(
                @"[AutoEnabled] [bit],
                [AutoFrequency] [int],
                [MaxCount] [int],
                [AutoLocal] [bit],
                [AutoRemote] [bit],
                [PathLocal] [nvarchar](Max),
                [IP] [nvarchar](Max),
                [UserName] [nvarchar](Max),
                [Pwd] [nvarchar](Max),
                [PathRemote] [nvarchar](Max)"
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
                    listName.Add("AutoEnabled");
                    listName.Add("AutoFrequency");
                    listName.Add("MaxCount");
                    listName.Add("AutoLocal");
                    listName.Add("AutoRemote");
                    listName.Add("PathLocal");
                    listName.Add("IP");
                    listName.Add("UserName");
                    listName.Add("Pwd");
                    listName.Add("PathRemote");

                    error = CreateNewTable(listName, listType, false);
                }
            }

            return error;
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string InsertRow(DBAutoBackupInfo item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + item.MEnabled);
            sb.Append("','" + item.MFrequency);
            sb.Append("','" + item.MCount);
            sb.Append("','" + item.MLocal);
            sb.Append("','" + item.MRemote);
            sb.Append("','" + item.MPathLocal);
            sb.Append("','" + item.MIP);
            sb.Append("','" + item.MUserName);
            sb.Append("','" + item.MPwd);
            sb.Append("','" + item.MPathRemote + "'");

            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 修改行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string UpdateRow(DBAutoBackupInfo item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("AutoEnabled='" + item.MEnabled);
            sb.Append("',AutoFrequency='" + item.MFrequency);
            sb.Append("',MaxCount='" + item.MCount);
            sb.Append("',AutoLocal='" + item.MLocal);
            sb.Append("',AutoRemote='" + item.MRemote);
            sb.Append("',PathLocal='" + item.MPathLocal);
            sb.Append("',IP='" + item.MIP);
            sb.Append("',UserName='" + item.MUserName);
            sb.Append("',Pwd='" + item.MPwd);
            sb.Append("',PathRemote='" + item.MPathRemote + "'");

            return SqlUpdateRow(sb.ToString());
        }

        /// <summary>
        /// 获取行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string SelectRow(out DBAutoBackupInfo item)
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
                        item = new DBAutoBackupInfo();
                        item.MEnabled = reader.GetBoolean(index++);
                        item.MFrequency = reader.GetInt32(index++);
                        item.MCount = reader.GetInt32(index++);
                        item.MLocal = reader.GetBoolean(index++);
                        item.MRemote = reader.GetBoolean(index++);
                        item.MPathLocal = reader.GetString(index++);
                        item.MIP = reader.GetString(index++);
                        item.MUserName = reader.GetString(index++);
                        item.MPwd = reader.GetString(index++);
                        item.MPathRemote = reader.GetString(index++);
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
