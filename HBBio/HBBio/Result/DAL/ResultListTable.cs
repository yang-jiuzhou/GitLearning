using HBBio.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Result
{
    class ResultListTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ResultListTable()
        {
            m_tableName = "ResultListTable";
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
                [Name] [nvarchar](MAX) NOT NULL,
                [CommunicationSetsID] [int] FOREIGN KEY REFERENCES CommunicationSetsTable(ID),
                [ProjectTreeID] [int] FOREIGN KEY REFERENCES ProjectTreeTable(ID),
                [UserID] [int] NOT NULL,
                [Type] [int] NOT NULL,
                [MethodStreamInfo] [varbinary](MAX),
                [BeginTime] [datetime],
                [EndTime] [datetime],
                [CurveName] [varchar](19) NOT NULL UNIQUE,
                [ColumnVol] [float] NOT NULL,
                [ColumnHeight] [float] NOT NULL,
                [AttachmentInfo] [nvarchar](MAX),
                [MarkerInfo] [nvarchar](MAX)
                ");
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string InsertRow(string name, int csID, int projectID, int userID, EnumResultIconType type, byte[] methodStreamInfo, DateTime beginTime, string curveName, double columnVol, double columnHeight, string attachmentInfo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO " + m_tableName + "(Name,CommunicationSetsID,ProjectTreeID,UserID,Type,MethodStreamInfo,BeginTime,EndTime,CurveName,ColumnVol,ColumnHeight,AttachmentInfo,MarkerInfo) VALUES(");
            sb.Append("'" + name);
            sb.Append("','" + csID);
            sb.Append("','" + projectID);
            sb.Append("','" + userID);
            sb.Append("','" + (int)type);
            sb.Append("',@MethodStreamInfo");
            sb.Append(",'" + beginTime);
            sb.Append("','" + beginTime);
            sb.Append("','" + curveName);
            sb.Append("','" + columnVol);
            sb.Append("','" + columnHeight);
            sb.Append("','" + attachmentInfo);
            sb.Append("','')");

            return SqlBaseCDIU(sb.ToString(), "MethodStreamInfo", methodStreamInfo);
        }

        /// <summary>
        /// 更新结束时间
        /// </summary>
        /// <returns></returns>
        public string UpdateRowEndTime()
        {
            int id = -1;
            string error = GetLastID(out id);
            if (null == error)
            {
                error = SqlUpdateRow("EndTime='" + DateTime.Now + "' WHERE ID='" + id + "'");
            }

            return error;
        }

        /// <summary>
        /// 更新标记信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string UpdateRowMarkerInfo(string markerInfo)
        {
            int id = -1;
            string error = GetLastID(out id);
            if (null == error)
            {
                error = SqlUpdateRow("MarkerInfo='" + markerInfo + "' WHERE ID='" + id + "'");
            }

            return error;
        }

        /// <summary>
        /// 重命名
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string UpdateName(ResultTitle item)
        {
            return SqlUpdateRow(@"Name='" + item.MName + "' WHERE ID='" + item.MID + "'");
        }

        /// <summary>
        /// 获取行信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string SelectRowLastLogName(out string name, out string curveName, out double columnVol)
        {
            string error = null;
            name = null;
            curveName = null;
            columnVol = 1;

            try
            {
                SqlDataReader reader = null;
                CreateConnAndReader(@"SELECT Name,CurveName,ColumnVol FROM " + m_tableName + @" ORDER BY ID DESC", out reader);
                if (null != reader)
                {
                    if (reader.Read())//匹配
                    {
                        name = reader.GetString(0);
                        curveName = reader.GetString(1);
                        columnVol = reader.GetDouble(2);
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
        public string SelectListName(int communicationSetsID, int projectID, string filter, out List<ResultTitle> list)
        {
            string error = null;
            list = new List<ResultTitle>();

            try
            {
                SqlDataReader reader = null;
                if (string.IsNullOrEmpty(filter))
                {
                    error = CreateConnAndReader(@"SELECT ID,Name,ProjectTreeID,UserID,Type,BeginTime,EndTime FROM " + m_tableName + @" WHERE CommunicationSetsID='" + communicationSetsID + "' AND ProjectTreeID='" + projectID + "' ORDER BY ID DESC", out reader);
                }
                else
                {
                    error = CreateConnAndReader(@"SELECT ID,Name,ProjectTreeID,UserID,Type,BeginTime,EndTime FROM " + m_tableName + @" WHERE CommunicationSetsID='" + communicationSetsID + "' AND ProjectTreeID='" + projectID + "' AND Name LIKE '%" + filter + "%' ORDER BY ID DESC", out reader);
                }

                if (null == error)
                {
                    while (reader.Read())
                    {
                        list.Add(new ResultTitle(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), (EnumResultIconType)reader.GetInt32(4), reader.GetDateTime(5), reader.GetDateTime(6)));
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
        /// 获取名称相似的谱图名列表
        /// </summary>
        /// <returns></returns>
        public string SelectListName(int communicationSetsID, int projectID, string name, out List<string> list)
        {
            string error = null;
            list = new List<string>();

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT Name FROM " + m_tableName + @" WHERE CommunicationSetsID='" + communicationSetsID + "' AND ProjectTreeID='" + projectID + "' AND Name like'" + name + "%' ORDER BY ID DESC", out reader);

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

        /// <summary>
        /// 获取其它信息
        /// </summary>
        /// <param name="markerInfo"></param>
        /// <returns></returns>
        public string SelectRowInfo(int id, out string name, out int projectID)
        {
            string error = null;
            name = null;
            projectID = -1;

            try
            {
                SqlDataReader reader = null;
                CreateConnAndReader(@"SELECT Name,ProjectTreeID FROM " + m_tableName + " WHERE ID='" + id + "'", out reader);
                if (null != reader)
                {
                    if (reader.Read())//匹配
                    {
                        name = reader.GetString(0);
                        projectID = reader.GetInt32(1);
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
        /// 获取其它信息
        /// </summary>
        /// <param name="markerInfo"></param>
        /// <returns></returns>
        public string SelectRowInfo(int id, out System.IO.Stream ms, out string curveName, out double columnVol, out double columnHeight, out string attachment, out string markerInfo, out DateTime time)
        {
            string error = null;
            ms = null;
            curveName = null;
            columnVol = 1;
            columnHeight = 1;
            attachment = "";
            markerInfo = null;
            time = DateTime.Now;

            try
            {
                SqlDataReader reader = null;
                CreateConnAndReader(@"SELECT MethodStreamInfo,CurveName,ColumnVol,ColumnHeight,AttachmentInfo,MarkerInfo,BeginTime FROM " + m_tableName + " WHERE ID='" + id + "'", out reader);
                if (null != reader)
                {
                    if (reader.Read())//匹配
                    {
                        if (!reader.GetSqlBytes(0).IsNull)
                        {
                            ms = reader.GetSqlBytes(0).Stream;
                        } 
                        curveName = reader.GetString(1);
                        columnVol = reader.GetDouble(2);
                        columnHeight = reader.GetDouble(3);
                        attachment = reader.GetString(4);
                        markerInfo = reader.GetString(5);
                        time = reader.GetDateTime(6);
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
