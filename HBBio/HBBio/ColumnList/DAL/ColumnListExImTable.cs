using HBBio.Database;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.ColumnList
{
    class ColumnListExImTable : BaseExImTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="path"></param>
        public ColumnListExImTable(string path, string tableName) : base(path, tableName)
        {

        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns></returns>
        public override string CreateTable()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"[ID] [INTEGER] PRIMARY KEY AUTOINCREMENT,
                [Name] [nvarchar] NOT NULL UNIQUE,
                [Note] [nvarchar] NOT NULL,
                [User] [nvarchar] NOT NULL,");
            for (int i = 0; i < EnumRunParametersInfo.Count; i++)
            {
                sb.Append("[");
                sb.Append(((EnumRunParameters)i).ToString());
                sb.Append("] [varchar] NOT NULL,");
            }
            for (int i = 0; i < EnumDetailsInfo.Count; i++)
            {
                sb.Append("[");
                sb.Append(((EnumDetails)i).ToString());
                sb.Append("] [varchar] NOT NULL,");
            }
            sb.Remove(sb.Length - 1, 1);

            return SqlCreateTable(sb.ToString());
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string InsertRow(ColumnItem item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("NULL,");
            sb.Append("'" + item.MName);
            sb.Append("','" + item.MNote);
            sb.Append("','" + item.MUser);
            for (int i = 0; i < EnumRunParametersInfo.Count; i++)
            {
                sb.Append("','" + item.MRP.MList[i].MStr);
            }
            for (int i = 0; i < EnumDetailsInfo.Count; i++)
            {
                sb.Append("','" + item.MDT.MList[i].MStr);
            }
            sb.Append("'");

            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string GetList(List<ColumnItem> list)
        {
            string error = null;
            list.Clear();

            try
            {
                SQLiteDataReader reader = null;
                error = CreateConnAndReader(@"SELECT * FROM " + m_tableName, out reader);
                if (null == error)
                {
                    while (reader.Read())//匹配
                    {
                        int index = 0;
                        index++;//第一项是ID

                        List<string> strList = new List<string>();
                        for (int i = 0; i < 3 + EnumRunParametersInfo.Count + EnumDetailsInfo.Count; i++)
                        {
                            strList.Add(reader.GetString(index++));
                        }
                        ColumnItem item = new ColumnItem();
                        item.InItList(strList);
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
    }
}
