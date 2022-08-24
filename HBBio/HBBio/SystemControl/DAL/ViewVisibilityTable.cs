using HBBio.Database;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.SystemControl
{
    /**
     * ClassName: ViewVisibilityTable
     * Description: 主界面各部件显隐表
     * Version: 1.0
     * Create:  2021/08/05
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class ViewVisibilityTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ViewVisibilityTable()
        {
            m_tableName = "ViewVisibilityTable";
        }

        /// <summary>
        /// 插入默认值
        /// </summary>
        /// <returns></returns>
        protected override string AddDefaultValue()
        {
            return InsertRow(new ViewVisibility());
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns></returns>
        protected override string CreateTable()
        {
            return SqlCreateTable(@"[ToolBar] [bit] NOT NULL,
                                    [Communication] [bit] NOT NULL,
                                    [InstrumentParameters] [bit] NOT NULL,
                                    [Administration] [bit] NOT NULL,
                                    [ColumnHandling] [bit] NOT NULL,
                                    [TubeStand] [bit] NOT NULL,
                                    [Manual] [bit] NOT NULL,
                                    [Project] [bit] NOT NULL,
                                    [AuditTrails] [bit] NOT NULL,
                                    [SystemMonitor] [bit] NOT NULL,
                                    [DB] [bit] NOT NULL,
                                    [RunData] [bit] NOT NULL,
                                    [Chromatogram] [bit] NOT NULL,
                                    [ProcessPicture] [bit] NOT NULL,
                                    [Monitor] [bit] NOT NULL,
                                    [StatusBar] [bit] NOT NULL");
        }

        /// <summary>
        /// 获取行信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetRow(ViewVisibility item)
        {
            string error = null;

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT * FROM " + m_tableName + @" WHERE 1=1", out reader);
                if (null == error)
                {
                    if (reader.Read())//匹配
                    {
                        int index = 0;
                        item.MToolBar = reader.GetBoolean(index++);
                        item.MCommunication = reader.GetBoolean(index++);
                        item.MInstrumentParameters = reader.GetBoolean(index++);
                        item.MAdministration = reader.GetBoolean(index++);
                        item.MColumnHandling = reader.GetBoolean(index++);
                        item.MTubeStand = reader.GetBoolean(index++);
                        item.MManual = reader.GetBoolean(index++);
                        item.MProject = reader.GetBoolean(index++);
                        item.MAuditTrails = reader.GetBoolean(index++);
                        item.MSystemMonitor = reader.GetBoolean(index++);
                        item.MDB = reader.GetBoolean(index++);
                        item.MRunData = reader.GetBoolean(index++);
                        item.MChromatogram = reader.GetBoolean(index++);
                        item.MProcessPicture = reader.GetBoolean(index++);
                        item.MMonitor = reader.GetBoolean(index++);
                        item.MStatusBar = reader.GetBoolean(index++);
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
        /// 插入行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string InsertRow(ViewVisibility item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + item.MToolBar);
            sb.Append("','" + item.MCommunication);
            sb.Append("','" + item.MInstrumentParameters);
            sb.Append("','" + item.MAdministration);
            sb.Append("','" + item.MColumnHandling);
            sb.Append("','" + item.MTubeStand);
            sb.Append("','" + item.MManual);
            sb.Append("','" + item.MProject);
            sb.Append("','" + item.MAuditTrails);
            sb.Append("','" + item.MSystemMonitor);
            sb.Append("','" + item.MDB);
            sb.Append("','" + item.MRunData);
            sb.Append("','" + item.MChromatogram);
            sb.Append("','" + item.MProcessPicture);
            sb.Append("','" + item.MMonitor);
            sb.Append("','" + item.MStatusBar + "'");
            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 修改行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string UpdateRow(ViewVisibility item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("ToolBar='" + item.MToolBar);
            sb.Append("',Communication='" + item.MCommunication);
            sb.Append("',InstrumentParameters='" + item.MInstrumentParameters);
            sb.Append("',Administration='" + item.MAdministration);
            sb.Append("',ColumnHandling='" + item.MColumnHandling);
            sb.Append("',TubeStand='" + item.MTubeStand);
            sb.Append("',Manual='" + item.MManual);
            sb.Append("',Project='" + item.MProject);
            sb.Append("',AuditTrails='" + item.MAuditTrails);
            sb.Append("',SystemMonitor='" + item.MSystemMonitor);
            sb.Append("',DB='" + item.MDB);
            sb.Append("',RunData='" + item.MRunData);
            sb.Append("',Chromatogram='" + item.MChromatogram);
            sb.Append("',ProcessPicture='" + item.MProcessPicture);
            sb.Append("',Monitor='" + item.MMonitor);
            sb.Append("',StatusBar='" + item.MStatusBar + "'");
            return SqlUpdateRow(sb.ToString());
        }
    }
}
