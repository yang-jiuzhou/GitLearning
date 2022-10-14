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
     * ClassName: OutputSelectSetTable
     * Description: 导出选项设置表
     * Version: 1.0
     * Create:  2020/09/07
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class OutputSelectSetTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public OutputSelectSetTable()
        {
            m_tableName = "OutputSelectSetTable";
        }

        /// <summary>
        /// 插入默认值
        /// </summary>
        /// <returns></returns>
        protected override string AddDefaultValue()
        {
            return InsertRow(new OutputSelectSet());
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns></returns>
        protected override string CreateTable()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[Note] [nvarchar](max),");
            sb.Append("[ShowUser] [bit] NOT NULL,");
            sb.Append("[ShowChromatogramName] [bit] NOT NULL,");
            sb.Append("[ShowChromatogram] [bit] NOT NULL,");
            sb.Append("[ShowIntegration] [bit] NOT NULL,");
            sb.Append("[ShowMethod] [bit] NOT NULL,");
            sb.Append("[ShowLog] [bit] NOT NULL");

            return SqlCreateTable(sb.ToString());
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string InsertRow(OutputSelectSet item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + item.m_note + "',");
            sb.Append("'" + item.m_showUser + "',");
            sb.Append("'" + item.m_showChromatogramName + "',");
            sb.Append("'" + item.m_showChromatogram + "',");
            sb.Append("'" + item.m_showIntegration + "',");
            sb.Append("'" + item.m_showMethod + "',");
            sb.Append("'" + item.m_showLog + "'");

            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 获取行信息
        /// </summary>
        /// <returns></returns>
        public string UpdateRow(OutputSelectSet item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Note='" + item.m_note + "',");
            sb.Append("ShowUser='" + item.m_showUser + "',");
            sb.Append("ShowChromatogramName='" + item.m_showChromatogramName + "',");
            sb.Append("ShowChromatogram='" + item.m_showChromatogram + "',");
            sb.Append("ShowIntegration='" + item.m_showIntegration + "',");
            sb.Append("ShowMethod='" + item.m_showMethod + "',");
            sb.Append("ShowLog='" + item.m_showLog + "'");

            return SqlUpdateRow(sb.ToString());
        }

        /// <summary>
        /// 获取行信息
        /// </summary>
        /// <returns></returns>
        public string SelectRow(out OutputSelectSet item)
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
                        item = new OutputSelectSet();
                        int index = 0;
                        item.m_note = reader.GetString(index++);                            //附加信息
                        item.m_showUser = reader.GetBoolean(index++);                      //是否显示用户
                        item.m_showChromatogramName = reader.GetBoolean(index++);          //是否显示谱图标识
                        item.m_showChromatogram = reader.GetBoolean(index++);              //是否显示谱图
                        item.m_showIntegration = reader.GetBoolean(index++);               //是否显示积分
                        item.m_showMethod = reader.GetBoolean(index++);                    //是否显示方法
                        item.m_showLog = reader.GetBoolean(index++);                       //是否显示日志 
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
