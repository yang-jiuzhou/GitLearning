using HBBio.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class TimeSetTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TimeSetTable()
        {
            m_tableName = "TimeSet";
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
                [Version] [nvarchar](64) NOT NULL,
                [Serial] [nvarchar](64) NOT NULL,
                [TimeIndex] [nvarchar](64) NOT NULL,
                [SetTime] [float] NOT NULL,
                [RunTime] [float] NOT NULL,
                [Calibration] [datetime] NOT NULL"
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
        public string GetDataTable(out DataTable logdt)
        {
            string error = null;

            try
            {
                error = CreateConnAndAdapter(out logdt, "SELECT * FROM " + m_tableName + " ORDER BY ID");
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

        /// <summary>
        /// 获取行
        /// </summary>
        /// <param name="id"></param>
        /// <param name="setTime"></param>
        /// <param name="runTime"></param>
        /// <param name="calibration"></param>
        /// <returns></returns>
        public string GetRow(int id, ref double setTime, ref double runTime, ref DateTime calibration)
        {
            string error = null;

            try
            {
                SqlDataReader reader = null;
                CreateConnAndReader(@"SELECT SetTime,RunTime,Calibration FROM " + m_tableName + @" WHERE ID LIKE '" + id + "'", out reader);
                if (null != reader)
                {
                    if (reader.Read())//匹配
                    {
                        setTime = reader.GetDouble(0);
                        runTime = reader.GetDouble(1);
                        calibration = reader.GetDateTime(2);
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
        /// 获取行
        /// </summary>
        /// <param name="version"></param>
        /// <param name="serial"></param>
        /// <param name="dly"></param>
        /// <param name="id"></param>
        /// <param name="setTime"></param>
        /// <param name="runTime"></param>
        /// <param name="calibration"></param>
        /// <returns></returns>
        public string GetRow(string version, string serial, int timeIndex, ref int id, ref double setTime, ref double runTime, ref DateTime calibration)
        {
            string error = null;

            try
            {
                SqlDataReader reader = null;
                CreateConnAndReader(@"SELECT ID,SetTime,RunTime,Calibration FROM " + m_tableName + @" WHERE Version LIKE '" + version + "' AND Serial LIKE '" + serial + "' AND TimeIndex LIKE '" + timeIndex + "'", out reader);
                if (null != reader)
                {
                    if (reader.Read())//匹配
                    {
                        id = reader.GetInt32(0);
                        setTime = reader.GetDouble(1);
                        runTime = reader.GetDouble(2);
                        calibration = reader.GetDateTime(3);

                        CloseConnAndReader();
                    }
                    else
                    {
                        CloseConnAndReader();

                        error = InsertRow(version, serial, timeIndex, 10000, 0, DateTime.Now.AddYears(1));
                        error += GetRow(version, serial, timeIndex, ref id, ref setTime, ref runTime, ref calibration);
                    }
                }
            }
            catch (Exception msg)
            {
                error += msg.Message;
            }

            if (string.IsNullOrEmpty(error))
            {
                error = null;
            }

            return error;
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <returns></returns>
        public string InsertRow(string version, string serial, int timeIndex, double setTime, double runTime, DateTime calibration)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + version);
            sb.Append("','" + serial);
            sb.Append("','" + timeIndex);
            sb.Append("','" + setTime);
            sb.Append("','" + runTime);
            sb.Append("','" + calibration + "'");

            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 修改行
        /// </summary>
        /// <param name="id"></param>
        /// <param name="runTime"></param>
        /// <returns></returns>
        public string UpdateRowRunTime(int id, double runTime)
        {
            return SqlUpdateRow(@"RunTime='" + runTime + "' WHERE ID LIKE '" + id + "'");
        }

        /// <summary>
        /// 修改行
        /// </summary>
        /// <param name="id"></param>
        /// <param name="setTime"></param>
        /// <returns></returns>
        public string UpdateRowSetTime(int id, double setTime)
        {
            return SqlUpdateRow(@"SetTime='" + setTime + "' WHERE ID LIKE '" + id + "'");
        }

        /// <summary>
        /// 修改行
        /// </summary>
        /// <param name="id"></param>
        /// <param name="calibration"></param>
        /// <returns></returns>
        public string UpdateRowCalibration(int id, DateTime calibration)
        {
            return SqlUpdateRow(@"Calibration='" + calibration + "' WHERE ID LIKE '" + id + "'");
        }
    }
}
