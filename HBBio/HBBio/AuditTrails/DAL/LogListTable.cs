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
     * ClassName: LogListTable
     * Description: 日志表名汇总表
     * Version: 1.0
     * Create:  2020/09/07
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class LogListTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public LogListTable()
        {
            m_tableName = "LogListTable";
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
            return SqlCreateTable(@"[ID] [int] PRIMARY KEY IDENTITY(1,1),
	            [LogName] [varchar](17) NOT NULL UNIQUE");
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
        /// 插入行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string InsertRow(string logname)
        {
            return SqlInsertRow("'" + logname + "'");
        }

        /// <summary>
        /// 获取行信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string SelectRowLastLogName(out string logName)
        {
            string error = null;
            logName = null;

            try
            {
                SqlDataReader reader = null;
                CreateConnAndReader(@"SELECT LogName FROM " + m_tableName + @" ORDER BY ID DESC", out reader);
                if (null != reader)
                {
                    if (reader.Read())//匹配
                    {
                        logName = reader.GetString(0);
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

        /// <summary>
        /// 获取完整表
        /// </summary>
        /// <returns></returns>
        public string SelectListName(out List<string> list)
        {
            string error = null;
            list = new List<string>();

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT LogName FROM " + m_tableName + @" ORDER BY ID", out reader);

                if (null == error)
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetString(0));
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
