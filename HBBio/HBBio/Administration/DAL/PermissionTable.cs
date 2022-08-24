using HBBio.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Administration
{
    /**
     * ClassName: PermissionTable
     * Description: 权限表
     * Version: 1.0
     * Create:  2020/09/07
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class PermissionTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PermissionTable()
        {
            m_tableName = "PermissionTable";
        }

        /// <summary>
        /// 插入默认值
        /// </summary>
        /// <returns></returns>
        protected override string AddDefaultValue()
        {
            InsertRow(PermissionInfo.GetDefault(EnumPermissionName.Admin));
            InsertRow(PermissionInfo.GetDefault(EnumPermissionName.IT));
            InsertRow(PermissionInfo.GetDefault(EnumPermissionName.OPER));
            InsertRow(PermissionInfo.GetDefault(EnumPermissionName.QA));
            return null;
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns></returns>
        protected override string CreateTable()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[ID] [int] PRIMARY KEY IDENTITY(1,1),");
            sb.Append("[DeleteID] [int] NOT NULL,");
            sb.Append("[Name] [nvarchar](64) NOT NULL,");
            sb.Append("[Note] [nvarchar](max) NOT NULL,");
            for (int i = 0; i < Enum.GetNames(typeof(EnumPermission)).GetLength(0); i++)
            {
                sb.Append("[" + ((EnumPermission)i).ToString() + "] [bit] NOT NULL,");
            }
            sb.Append("CONSTRAINT DeleteID_Name UNIQUE (DeleteID,Name)");

            return SqlCreateTable(sb.ToString());
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string InsertRow(PermissionInfo item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + item.MDeleteID);
            sb.Append("','" + item.MName);
            sb.Append("','" + item.MNote);
            foreach (var it in item.MList)
            {
                sb.Append("','" + it);
            }
            sb.Append("'");

            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string DeleteRow(int ID)
        {
            return SqlUpdateRow(@"DeleteID='" + ID + "' WHERE ID='" + ID + "'");
        }

        /// <summary>
        /// 修改行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string UpdateRow(PermissionInfo item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Note='" + item.MNote + "',");
            for (int i = 0; i < item.MList.Count; i++)
            {
                sb.Append(((EnumPermission)i).ToString() + "='" + item.MList[i] + "',");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(" WHERE ID LIKE '" + item.MID + "'");

            return SqlUpdateRow(sb.ToString());
        }

        /// <summary>
        /// 获取行信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string SelectRow(int ID, out PermissionInfo item)
        {
            string error = null;
            item = null;

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT * FROM " + m_tableName + @" WHERE ID='" + ID + "'", out reader);
                if (null == error)
                {
                    if (reader.Read())//匹配
                    {
                        int index = 0;
                        item = new PermissionInfo(reader.GetInt32(index++), reader.GetInt32(index++), reader.GetString(index++), reader.GetString(index++), false);
                        for (int i = 0; i < item.MList.Count; i++)
                        {
                            item.MList[i] = reader.GetBoolean(index++);
                        }
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

        /// <summary>
        /// 获取行信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string SelectRow(string name, out PermissionInfo item)
        {
            string error = null;
            item = null;

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT * FROM " + m_tableName + @" WHERE Name='" + name + "'", out reader);
                if (null == error)
                {
                    if (reader.Read())//匹配
                    {
                        int index = 0;
                        item = new PermissionInfo(reader.GetInt32(index++), reader.GetInt32(index++), reader.GetString(index++), reader.GetString(index++), false);
                        for (int i = 0; i < item.MList.Count; i++)
                        {
                            item.MList[i] = reader.GetBoolean(index++);
                        }
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

        /// <summary>
        /// 获取完整表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string SelectList(out List<PermissionInfo> list)
        {
            string error = null;
            list = new List<PermissionInfo>();

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT * FROM " + m_tableName + @" WHERE DeleteID='0' ORDER BY ID", out reader);

                if (null == error)
                {
                    while (reader.Read())
                    {
                        int index = 0;
                        PermissionInfo item = new PermissionInfo(reader.GetInt32(index++), reader.GetInt32(index++), reader.GetString(index++), reader.GetString(index++), false);
                        for (int i = 0; i < item.MList.Count; i++)
                        {
                            item.MList[i] = reader.GetBoolean(index++);
                        }

                        list.Add(item);
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

        /// <summary>
        /// 获取所有拥有审核权限的权限ID
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string SelectListSignerReviewer(out List<int> list)
        {
            string error = null;
            list = new List<int>();

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT ID FROM " + m_tableName + @" WHERE Administration_SignerReviewer_Edit='1' AND DeleteID='0' ORDER BY ID", out reader);

                if (null == error)
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetInt32(0));
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
