using HBBio.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Chromatogram
{
    class AxisScaleTable : BaseTable
    {
        private int m_id = -1;


        /// <summary>
        /// 构造函数
        /// </summary>
        public AxisScaleTable(int id)
        {
            m_id = id;
            m_tableName = "AxisScale" + m_id;
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
                @"[ID] [int] PRIMARY KEY IDENTITY(1,1),
                [AxisScale] [bit] NOT NULL,
                [ValMin] [float] NOT NULL,
                [ValMax] [float] NOT NULL"
                );
        }

        /// <summary>
        /// 获取完整表
        /// </summary>
        /// <returns></returns>
        public string GetList(out List<AxisScale> list)
        {
            string error = null;
            list = new List<AxisScale>();

            SqlDataReader reader = null;
            CreateConnAndReader(@"SELECT * FROM " + m_tableName + " ORDER BY ID", out reader);
            if (null != reader)
            {
                while (reader.Read())
                {
                    int index = 1;
                    list.Add(new AxisScale(reader.GetBoolean(index++) ? EnumAxisScale.Fixed : EnumAxisScale.Auto, reader.GetDouble(index++), reader.GetDouble(index++)));
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
        public string UpdateList(List<AxisScale> list)
        {
            string result = TruncateTable();

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
        public string InsertRow(AxisScale item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + (EnumAxisScale.Fixed == item.MAxisScale));
            sb.Append("','" + item.MMin);
            sb.Append("','" + item.MMax + "'");

            return SqlInsertRow(sb.ToString());
        }
    }
}
