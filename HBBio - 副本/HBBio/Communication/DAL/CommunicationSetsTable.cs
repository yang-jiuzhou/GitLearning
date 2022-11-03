using HBBio.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class CommunicationSetsTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CommunicationSetsTable()
        {
            m_tableName = "CommunicationSetsTable";
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
            return SqlCreateTable(
                @"[ID] [int] PRIMARY KEY IDENTITY(1,1),
                [Name] [nvarchar](64) NOT NULL UNIQUE,
                [CommunMode] [varchar](64) NOT NULL,
                [Note] [nvarchar](max) NOT NULL,
                [Datetime] [datetime] NOT NULL,
                [IsEnabled] [bit] NOT NULL,
                [Visibled] [bit] NOT NULL"
                );
        }

        /// <summary>
        /// 检查表
        /// </summary>
        /// <returns></returns>
        public override string CheckTable()
        {
            return "";
        }

        /// <summary>
        /// 删除行
        /// </summary>
        /// <returns></returns>
        public string DeleteRow(int id)
        {
            return SqlDeleteRow("ID LIKE '" + id + "'");
        }

        /// <summary>
        /// 获取完整表
        /// </summary>
        /// <returns></returns>
        public string GetDataList(out List<CommunicationSets> list)
        {
            string error = null;
            list = new List<CommunicationSets>();

            SqlDataReader reader = null;
            error = CreateConnAndReader(@"SELECT * FROM " + m_tableName + @" WHERE Visibled=1 ORDER BY ID", out reader);
            if (null != reader)
            {
                while (reader.Read())
                {
                    list.Add(new CommunicationSets(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetDateTime(4), reader.GetBoolean(5)));
                }
                CloseConnAndReader();
            }

            return error;
        }

        /// <summary>
        /// 获取名称表
        /// </summary>
        /// <returns></returns>
        public string GetNameList(out List<string> list)
        {
            string error = null;
            list = new List<string>();

            SqlDataReader reader = null;
            error = CreateConnAndReader(@"SELECT Name FROM " + m_tableName + @" WHERE Visibled=1 ORDER BY ID", out reader);
            if (null != reader)
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
        /// <returns></returns>
        public string InsertRow(CommunicationSets item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + item.MName);
            sb.Append("','" + item.MCommunMode);
            sb.Append("','" + item.MNote);
            sb.Append("','" + item.MDatetime);
            sb.Append("','" + item.MIsEnabled + "','1'");

            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 修改行
        /// </summary>
        /// <param name="type"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public string UpdateRow(CommunicationSets item)
        {
            return SqlUpdateRow(@"Name='" + item.MName + "',Note='" + item.MNote + "',IsEnabled='" + item.MIsEnabled + "' WHERE ID LIKE '" + item.MId + "'");
        }
    }
}
