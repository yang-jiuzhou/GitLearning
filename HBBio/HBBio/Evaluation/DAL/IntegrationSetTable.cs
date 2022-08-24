using HBBio.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Evaluation
{
    /**
     * ClassName: IntegrationSetTable
     * Description: 积分设置表
     * Version: 1.0
     * Create:  2020/09/07
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class IntegrationSetTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IntegrationSetTable()
        {
            m_tableName = "IntegrationSetTable";
        }

        /// <summary>
        /// 插入默认值
        /// </summary>
        /// <returns></returns>
        protected override string AddDefaultValue()
        {
            return InsertRow(new IntegrationSet());
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns></returns>
        protected override string CreateTable()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Enum.GetNames(typeof(EnumIntegration)).GetLength(0); i++)
            {
                sb.Append("[" + ((EnumIntegration)i).ToString() + "] [bit] NOT NULL,");
            }
            sb.Append("[IsMin] [bit] NOT NULL,");
            sb.Append("[MinHeight] [float] NOT NULL,");
            sb.Append("[MinArea] [float] NOT NULL,");
            sb.Append("[MinWidth] [float] NOT NULL,");
            sb.Append("[IsCount] [bit] NOT NULL,");
            sb.Append("[PeakCount] [int] NOT NULL");

            return SqlCreateTable(sb.ToString());
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string InsertRow(IntegrationSet item)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < item.m_arrShow.Length; i++)
            {
                sb.Append("'" + item.m_arrShow[i] + "',");
            }
            sb.Append("'" + item.MIsMin + "',");
            sb.Append("'" + item.MMinHeight + "',");
            sb.Append("'" + item.MMinArea + "',");
            sb.Append("'" + item.MMinWidth + "',");
            sb.Append("'" + item.MIsCount + "',");
            sb.Append("'" + item.MPeakCount + "'");

            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 获取行信息
        /// </summary>
        /// <returns></returns>
        public string UpdateRow(IntegrationSet item)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < item.m_arrShow.Length; i++)
            {
                sb.Append(((EnumIntegration)i).ToString() + "='" + item.m_arrShow[i] + "',");
            }
            sb.Append("IsMin='" + item.MIsMin + "',");
            sb.Append("MinHeight='" + item.MMinHeight + "',");
            sb.Append("MinArea='" + item.MMinArea + "',");
            sb.Append("MinWidth='" + item.MMinWidth + "',");
            sb.Append("IsCount='" + item.MIsCount + "',");
            sb.Append("PeakCount='" + item.MPeakCount + "'");

            return SqlUpdateRow(sb.ToString());
        }

        /// <summary>
        /// 获取行信息
        /// </summary>
        /// <returns></returns>
        public string SelectRow(out IntegrationSet item)
        {
            string error = null;
            item = null;

            try
            {
                SqlDataReader reader = null;
                CreateConnAndReader(@"SELECT * FROM " + m_tableName, out reader);
                if (null != reader)
                {
                    if (reader.Read())//匹配
                    {
                        item = new IntegrationSet();
                        int index = 0;
                        for (int i = 0; i < item.m_arrShow.Length; i++)
                        {
                            item.m_arrShow[i] = reader.GetBoolean(index++);
                        }
                        item.MIsMin = reader.GetBoolean(index++);
                        item.MMinHeight = reader.GetDouble(index++);
                        item.MMinArea = reader.GetDouble(index++);
                        item.MMinWidth = reader.GetDouble(index++);
                        item.MIsCount = reader.GetBoolean(index++);
                        item.MPeakCount = reader.GetInt32(index++);
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
    }
}
