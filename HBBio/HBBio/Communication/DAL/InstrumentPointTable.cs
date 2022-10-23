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
    class InstrumentPointTable : BaseTable
    {
        private int m_id = -1;


        /// <summary>
        /// 构造函数
        /// </summary>
        public InstrumentPointTable(int id)
        {
            m_id = id;
            m_tableName = "InstrumentPoint" + m_id;
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
                [PtX1] [float] NOT NULL,
                [PtY1] [float] NOT NULL,
                [PtX2] [float] NOT NULL,
                [PtY2] [float] NOT NULL,
                [IsHV] [bit] NOT NULL,
                [LineType] [int] NOT NULL"
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
        public string GetDataList(out List<InstrumentPoint> list)
        {
            string error = null;

            list = new List<InstrumentPoint>();

            SqlDataReader reader = null;
            error = CreateConnAndReader(@"SELECT * FROM " + m_tableName, out reader);
            if (null != reader)
            {
                while (reader.Read())
                {
                    list.Add(new InstrumentPoint(reader.GetString(0), new Point(reader.GetDouble(1), reader.GetDouble(2)), new Point(reader.GetDouble(3), reader.GetDouble(4)), reader.GetBoolean(5), (EnumLineType)reader.GetInt32(6))); 
                }

                CloseConnAndReader();
            }

            return error;
        }

        /// <summary>
        /// 设置完整表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string InitDataList(List<InstrumentPoint> list)
        {
            string result = null;

            for (int i = 0; i < list.Count; i++)
            {
                result += InsertRow(list[i]);
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
        public string InsertRow(InstrumentPoint item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + item.MName);
            sb.Append("','" + item.MPt1.X);
            sb.Append("','" + item.MPt1.Y);
            sb.Append("','" + item.MPt2.X);
            sb.Append("','" + item.MPt2.Y);
            sb.Append("','" + item.MIsHV);
            sb.Append("','" + (int)item.MType + "'");

            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 修改完整表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string UpdateDataList(List<InstrumentPoint> list)
        {
            string result = null;
            List<string> sqlList = new List<string>();
            foreach (var item in list)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("'" + item.MName);
                sb.Append("','" + item.MPt1.X);
                sb.Append("','" + item.MPt1.Y);
                sb.Append("','" + item.MPt2.X);
                sb.Append("','" + item.MPt2.Y);
                sb.Append("','" + item.MIsHV);
                sb.Append("','" + (int)item.MType + "'");
                sqlList.Add(sb.ToString());
            }

            result = SqlUpdateTable(sqlList);
            if (string.IsNullOrEmpty(result))
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// 修改行
        /// </summary>
        /// <param name="type"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public string UpdateRow(InstrumentPoint item)
        {
            return SqlUpdateRow(@"PtX1='" + item.MPt1.X + "',PtY1='" + item.MPt1.Y + "',PtX2='" + item.MPt2.X + "',PtY2='" + item.MPt2.Y + "',IsHV='" + item.MIsHV + "',LineType='" + (int)item.MType + "' WHERE Name LIKE '" + item.MName + "'");
        }
    }
}
