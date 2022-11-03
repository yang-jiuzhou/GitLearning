using HBBio.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.Communication
{
    class CirclePointTable : BaseTable
    {
        private int m_id = -1;


        /// <summary>
        /// 构造函数
        /// </summary>
        public CirclePointTable(int id)
        {
            m_id = id;
            m_tableName = "CirclePoint" + m_id;
            m_connStr = BaseDB.ComDB;
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
                @"[Name] [nvarchar](32) NOT NULL UNIQUE,
                [PtX] [float] NOT NULL,
                [PtY] [float] NOT NULL"
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
        /// 获取完整表
        /// </summary>
        /// <returns></returns>
        public string GetList(out List<Point> list)
        {
            string error = null;

            list = new List<Point>();

            SqlDataReader reader = null;
            error = CreateConnAndReader(@"SELECT * FROM " + m_tableName + " ORDER BY Name", out reader);
            if (null != reader)
            {
                while (reader.Read())
                {
                    list.Add(new Point(reader.GetDouble(1), reader.GetDouble(2))); 
                }

                CloseConnAndReader();
            }
            else
            {
                InitTable();
            }

            return error;
        }

        /// <summary>
        /// 设置完整表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string UpdateList(List<Point> list)
        {
            string result = TruncateTable();

            for (int i = 0; i < list.Count; i++)
            {
                result += InsertRow(i, list[i]);
            }

            if (string.IsNullOrEmpty(result))
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <returns></returns>
        public string InsertRow(int index, Point item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + index);
            sb.Append("','" + item.X);
            sb.Append("','" + item.Y + "'");

            return SqlInsertRow(sb.ToString());
        }
    }
}
