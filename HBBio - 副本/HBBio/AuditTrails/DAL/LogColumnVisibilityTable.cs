using HBBio.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.AuditTrails
{
    /**
     * ClassName: LogColumnVisibilityTable
     * Description: 日志列显隐表
     * Version: 1.0
     * Create:  2020/09/07
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class LogColumnVisibilityTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public LogColumnVisibilityTable()
        {
            m_tableName = "LogColumnVisibilityTable";
        }

        /// <summary>
        /// 插入默认值
        /// </summary>
        /// <returns></returns>
        protected override string AddDefaultValue()
        {
            return InsertRow(new LogColumnVisibility());
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns></returns>
        protected override string CreateTable()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Enum.GetNames(typeof(EnumLog)).GetLength(0); i++)
            {
                sb.Append("[" + ((EnumLog)i).ToString() + "] [bit] NOT NULL,");
            }
            sb.Remove(sb.Length - 1, 1);

            return SqlCreateTable(sb.ToString());
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string InsertRow(LogColumnVisibility item)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < item.MArrVisib.Length; i++)
            {
                sb.Append("'" + item.MArrVisib[i] + "',");
            }
            sb.Remove(sb.Length - 1, 1);

            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 获取行信息
        /// </summary>
        /// <returns></returns>
        public string UpdateRow(LogColumnVisibility item)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < item.MArrVisib.Length; i++)
            {
                sb.Append(((EnumLog)i).ToString() + "='" + item.MArrVisib[i] + "',");
            }
            sb.Remove(sb.Length - 1, 1);

            return SqlUpdateRow(sb.ToString());
        }

        /// <summary>
        /// 获取行信息
        /// </summary>
        /// <returns></returns>
        public string SelectRow(out LogColumnVisibility item)
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
                        item = new LogColumnVisibility();
                        for (int i = 0; i < item.MArrVisib.Length; i++)
                        {
                            item.MArrVisib[i] = reader.GetBoolean(i);
                        }
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
