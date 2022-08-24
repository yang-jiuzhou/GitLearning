using HBBio.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.AuditTrails
{
    /**
     * ClassName: LogUnitTable
     * Description: 日志单元信息表
     * Version: 1.0
     * Create:  2020/01/21
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class LogUnitTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public LogUnitTable(string tableName)
        {
            m_tableName = tableName;
            m_connStr = BaseDB.LogDB;
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
                [Type] [nvarchar](64) NOT NULL,
                [Date] [datetime] NOT NULL,
                [BatchT] [varchar](32),
                [BatchV] [varchar](32),
                [BatchCV] [varchar](32),
                [UserName] [nvarchar](64),
                [Description] [nvarchar](MAX),
                [Operation] [nvarchar](MAX)"
                );
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string InsertRow(LogInfo item)
        {
            string commandText = "'" + item.MType
                    + "','" + item.MDate
                    + "','" + item.MBatchT
                    + "','" + item.MBatchV
                    + "','" + item.MBatchCV
                    + "','" + item.MUserName
                    + "','" + item.MDescription
                    + "','" + item.MOperation + "'";

            return SqlInsertRow(commandText);
        }

        /// <summary>
        /// 根据条件读取记录
        /// </summary>
        /// <param name="table"></param>
        /// <param name="type"></param>
        /// <param name="userName"></param>
        /// <param name="txtName"></param>
        /// <returns></returns>
        public string SearchLog(out DataTable logdt, string type, string userName, string filter)
        {
            string error = null;

            try
            {
                string sqlCommandString = "SELECT * FROM " + m_tableName + " WHERE 1=1";
                string addStr = "";
                if (!type.Equals("All"))
                {
                    addStr += (" and Type ='" + type + "'");
                }
                if (!userName.Equals("All"))
                {
                    addStr += (" and UserName ='" + userName + "'");
                }
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    addStr += (" and (Description like '%" + filter + "%'");
                    addStr += (" or Operation like '%" + filter + "%')");
                }
                sqlCommandString += addStr;

                error = CreateConnAndAdapter(out logdt, sqlCommandString);
            }
            catch(Exception msg)
            {
                error = msg.Message;
                logdt = null;
            }
            finally
            {
                CloseConnAndAdapter();
            }

            return error;
        }

        /// <summary>
        /// 根据条件读取记录
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string SearchLog(out DataTable logdt, DateTime time1, DateTime time2, string type, string userName, string filter)
        {
            string error = null;

            try
            {
                string sqlCommandString = "SELECT * FROM " + m_tableName + " WHERE Date>='" + time1 + "' AND Date<='" + time2 + "'";
                string addStr = "";
                if (!type.Equals("All"))
                {
                    addStr += (" and Type ='" + type + "'");
                }
                if (!userName.Equals("All"))
                {
                    addStr += (" and UserName ='" + userName + "'");
                }
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    addStr += (" and (Description like '%" + filter + "%'");
                    addStr += (" or Operation like '%" + filter + "%')");
                }
                sqlCommandString += addStr;

                error = CreateConnAndAdapter(out logdt, sqlCommandString);
            }
            catch (Exception msg)
            {
                error = msg.Message;
                logdt = null;
            }
            finally
            {
                CloseConnAndAdapter();
            }

            return error;
        }
    }
}
