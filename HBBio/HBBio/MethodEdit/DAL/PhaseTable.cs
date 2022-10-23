using HBBio.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: PhaseTable
     * Description: 用户自定义阶段表
     * Version: 1.0
     * Create:  2020/11/12
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    class PhaseTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PhaseTable()
        {
            m_tableName = "PhaseTable";
        }

        /// <summary>
        /// 插入默认值
        /// </summary>
        /// <returns></returns>
        protected override string AddDefaultValue()
        {
            return null;
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns></returns>
        protected override string CreateTable()
        {
            string error = null;
            error += SqlCreateTable(
                @"[ID] [int] PRIMARY KEY IDENTITY(1, 1),
                [Name] [nvarchar](128) NOT NULL UNIQUE CHECK(LEN(Name)>0),
                [Info] [nvarchar](MAX)"
                );

            if (string.IsNullOrEmpty(error))
            {
                error = null;
            }

            return error;
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
                    listName.Add("ID");
                    listName.Add("Name");
                    listName.Add("Info");

                    error = CreateNewTable(listName, listType, true);
                }
            }

            return error;
        }

        /// <summary>
        /// 删除行信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string DeleteRow(string name)
        {
            return SqlDeleteRow("Name LIKE '" + name + "'");
        }

        /// <summary>
        /// 获取行信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetRow(string name, ref string info)
        {
            string error = null;

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT Info FROM " + m_tableName + @" WHERE Name LIKE '" + name + "'", out reader);
                if (null == error)
                {
                    if (reader.Read())//匹配
                    {
                        info = reader.GetString(0);
                    }
                    else
                    {
                        error = Share.ReadXaml.S_ErrorNoData;
                    }
                    CloseConnAndReader();
                }
            }
            catch(Exception msg)
            {
                error = msg.Message;
            }

            return error;
        }

        /// <summary>
        /// 获取完整表
        /// </summary>
        /// <returns></returns>
        public string GetNameList(out List<string> list, string filter = null)
        {
            string error = null;
            list = new List<string>();

            SqlDataReader reader = null;
            if (string.IsNullOrEmpty(filter))
            {
                error = CreateConnAndReader(@"SELECT Name FROM " + m_tableName + @" ORDER BY ID", out reader);
            }
            else
            {
                error = CreateConnAndReader(@"SELECT Name FROM " + m_tableName + @" WHERE Name LIKE '%" + filter +"%' ORDER BY ID", out reader);
            }
            
            if (null == error)
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString(0));
                }
                CloseConnAndReader();
            }

            return error;
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string InsertRow(string name, string info)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + name);
            sb.Append("','" + info);
            sb.Append( "'");

            return SqlInsertRow(sb.ToString());
        }
    }
}
