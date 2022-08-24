using HBBio.Database;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    class MethodExImTable : BaseExImTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="path"></param>
        public MethodExImTable(string path, string tableName) : base(path, tableName)
        {

        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns></returns>
        public override string CreateTable()
        {
            string error = null;
            error += SqlCreateTable(
                @"[ID] [INTEGER] PRIMARY KEY AUTOINCREMENT,
                [CommunicationSetsID] [int] NOT NULL,
                [ProjectID] [int] NOT NULL,
                [MethodName] [nvarchar] NOT NULL,
                [MethodType] [int] NOT NULL,
                [IDList] [varchar],
                [StreamInfo] [image]"
                );

            if (string.IsNullOrEmpty(error))
            {
                error = null;
            }

            return error;
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string InsertRow(Method item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO " + m_tableName + "(CommunicationSetsID,ProjectID,MethodName,MethodType,IDList,StreamInfo) VALUES(");
            sb.Append("'" + item.MCommunicationSetsID);
            sb.Append("','" + item.MProjectID);
            sb.Append("','" + item.MName);
            sb.Append("','" + (int)item.MType);

            sb.Append("','" + DBNull.Value);
            sb.Append("',@StreamInfo)");

            return SqlBaseCDIU(sb.ToString(), "StreamInfo", Share.DeepCopy.GetMemoryStream(item));
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string InsertRow(MethodQueue item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + item.MCommunicationSetsID);
            sb.Append("','" + item.MProjectID);
            sb.Append("','" + item.MName);
            sb.Append("','" + (int)item.MType);

            sb.Append("','" + item.GetMethodInfo());

            sb.Append("'," + System.Data.SqlTypes.SqlBinary.Null);

            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string GetList(List<Method> list)
        {
            string error = null;
            list.Clear();

            try
            {
                SQLiteDataReader reader = null;
                error = CreateConnAndReader(@"SELECT StreamInfo FROM " + m_tableName, out reader);
                if (null == error)
                {
                    while (reader.Read())//匹配
                    {
                        Method item = Share.DeepCopy.SetMemoryStream<Method>(reader["StreamInfo"] as byte[]);
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
