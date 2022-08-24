using HBBio.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Result
{
    class ResultUnitTable : BaseTable
    {
        private List<string> m_nameList = null;


        /// <summary>
        /// 构造函数
        /// </summary>
        public ResultUnitTable(string tableName, List<string> nameList)
        {
            m_tableName = tableName;
            m_connStr = BaseDB.CurveDB;

            m_nameList = nameList;
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
            StringBuilder sb = new StringBuilder();
            sb.Append("[ID] [int] PRIMARY KEY IDENTITY(1,1),");
            sb.Append("[T] [float] NOT NULL,");
            sb.Append("[V] [float] NOT NULL,");
            foreach (var it in m_nameList)
            {
                sb.Append("[" + it + "] [float] NOT NULL,");
            }
            sb.Remove(sb.Length - 1, 1);

            return SqlCreateTable(sb.ToString());
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string InsertRow(ResultRow item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + item.m_T);
            sb.Append("','" + item.m_V);
            foreach (var it in item.m_valList)
            {
                sb.Append("','" + it);
            }
            sb.Append("'");

            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 获取完整表
        /// </summary>
        /// <returns></returns>
        public string SelectList(double columnVol, List<int> smooth, List<List<double>> listList)
        {
            string error = null;

            try
            {
                int length = 0;

                SqlDataReader reader = null;
                error = CreateConnAndReader("SELECT COUNT(*) FROM syscolumns s WHERE s.id=OBJECT_ID('" + m_tableName + "')", out reader);
                if (null == error)
                {
                    if (reader.Read())
                    {
                        //去除ID,T,V
                        length = reader.GetInt32(0) - 3;
                    }
                    CloseConnAndReader();
                }

                error = CreateConnAndReader(@"SELECT * FROM " + m_tableName + @" ORDER BY ID", out reader);

                if (null == error)
                {
                    List<Queue<double>> listQueue = new List<Queue<double>>();
                    for (int i = 0; i < length; i++)
                    {
                        listQueue.Add(new Queue<double>());
                    }
                    while (reader.Read())
                    {
                        int index = 1;//排除ID列
                        listList[0].Add(reader.GetDouble(index++));//T
                        listList[1].Add(reader.GetDouble(index++));//V
                        listList[2].Add(listList[1].Last()/ columnVol);//CV
                        for (int i = 0; i < length; i++)
                        {
                            listQueue[i].Enqueue(reader.GetDouble(index++));
                            while (listQueue[i].Count > smooth[i])
                            {
                                listQueue[i].Dequeue();
                            }
                            listList[3 + i].Add(listQueue[i].Average());
                        }
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
