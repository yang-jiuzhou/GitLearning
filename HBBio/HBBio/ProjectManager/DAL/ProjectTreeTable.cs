using HBBio.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.ProjectManager
{
    /**
     * ClassName: TreeNode
     * Description: 项目树表
     * Version: 1.0
     * Create:  2020/11/12
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    class ProjectTreeTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ProjectTreeTable()
        {
            m_tableName = "ProjectTreeTable";
        }

        /// <summary>
        /// 插入默认值
        /// </summary>
        /// <returns></returns>
        protected override string AddDefaultValue()
        {
            return InsertRow(new TreeNode(0, 0, "General",EnumType.Other));
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns></returns>
        protected override string CreateTable()
        {
            return SqlCreateTable(
                @"[ID] [int] PRIMARY KEY IDENTITY(1,1),
                [ParentID] [int] NOT NULL,
                [UserID] [int] NOT NULL,
                [Name] [nvarchar](64) NOT NULL,
                [CreateTime] [varchar](32) NOT NULL,
                [CountMethod] [int] NOT NULL,
                [CountResult] [int] NOT NULL"
                );
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <returns></returns>
        public string InsertRow(TreeNode item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + item.MParentId);
            sb.Append("','" + item.MUserID);
            sb.Append("','" + item.MName);
            sb.Append("','" + item.MCreateTime);
            sb.Append("','" + item.MCountMethod);
            sb.Append("','" + item.MCountResult + "'");

            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string DeleteRow(int ID)
        {
            return SqlDeleteRow("ID='" + ID + "'");
        }

        /// <summary>
        /// 修改行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string UpdateRow(TreeNode item)
        {
            return SqlUpdateRow("Name='" + item.MName + "',CountMethod='" + item.MCountMethod + "',CountResult='" + item.MCountResult + "' WHERE ID='" + item.MId + "'");
        }

        /// <summary>
        /// 修改行
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string UpdateRowCountResult(int ID)
        {
            return SqlUpdateRow("CountResult=CountResult+1 WHERE ID='" + ID + "'");
        }

        /// <summary>
        /// 获取父亲ID
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public string SelectRowParentID(int ID,out int parentID)
        {
            string error = null;
            parentID = -1;

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader("SELECT ParentID FROM " + m_tableName + " WHERE ID='" + ID + "'", out reader);

                if (null == error)
                {
                    if (reader.Read())
                    {
                        parentID = reader.GetInt32(0);
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
        /// <returns></returns>
        public string SelectListAll(out List<TreeNode> list)
        {
            string error = null;
            list = new List<TreeNode>();

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader("SELECT * FROM " + m_tableName + " ORDER BY ID", out reader);

                if (null == error)
                {
                    while (reader.Read())
                    {
                        list.Add(new TreeNode(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6), EnumType.Other));
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
        /// 获取筛选表
        /// </summary>
        /// <returns></returns>
        public string SelectListFilter(string filter, out List<TreeNode> list)
        {
            string error = null;
            list = new List<TreeNode>();

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT * FROM " + m_tableName + @" WHERE Name LIKE '%" + filter + "%' ORDER BY ID", out reader);

                if (null == error)
                {
                    while (reader.Read())
                    {
                        list.Add(new TreeNode(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6), EnumType.Other));
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
