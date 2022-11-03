using HBBio.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Manual
{
    class ManualTempTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ManualTempTable()
        {
            m_tableName = "ManualTempTable";
        }

        /// <summary>
        /// 插入默认值
        /// </summary>
        /// <returns></returns>
        protected override string AddDefaultValue()
        {
            return InsertRow(null);
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns></returns>
        protected override string CreateTable()
        {
            string error = null;
            error += SqlCreateTable(
                @"[StreamInfo] [varbinary](MAX),
	            [CollInfo] [nvarchar](max)"
                );

            if (string.IsNullOrEmpty(error))
            {
                error = null;
            }

            return error;
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
        /// <returns></returns>
        public string InsertRow(ManualValue item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO " + m_tableName + "(StreamInfo,CollInfo) VALUES(");
            sb.Append("@StreamInfo");
            sb.Append(",'" + DBNull.Value + "')");

            if (null != item)
            {
                return SqlBaseCDIU(sb.ToString(), "StreamInfo", Share.DeepCopy.GetMemoryStream(item));
            }
            else
            {
                return SqlBaseCDIU(sb.ToString(), "StreamInfo", null);
            }
        }

        /// <summary>
        /// 修改行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string AddRowTemp(ManualValue item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE ");
            sb.Append(m_tableName);
            sb.Append(" SET ");
            sb.Append("StreamInfo=@StreamInfo");
       
            if (null != item)
            {
                return SqlBaseCDIU(sb.ToString(), "StreamInfo", Share.DeepCopy.GetMemoryStream(item));
            }
            else
            {
                return SqlBaseCDIU(sb.ToString(), "StreamInfo", null);
            }
        }
        public string AddRowColl(string coll)
        {
            return SqlUpdateRow("CollInfo='" + coll + "'");
        }

        /// <summary>
        /// 删除行
        /// </summary>
        /// <returns></returns>
        public string DelRow()
        {
            return SqlUpdateRow("StreamInfo=" + System.Data.SqlTypes.SqlBinary.Null + ",CollInfo='" + DBNull.Value + "'");
        }

        /// <summary>
        /// 获取行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string SelectRowTemp(out ManualValue item)
        {
            string error = null;
            item = null;

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT StreamInfo FROM " + m_tableName, out reader);
                if (null == error)
                {
                    if (reader.Read())//匹配
                    {
                        if (!reader.GetSqlBytes(0).IsNull)
                        {
                            item = Share.DeepCopy.SetMemoryStream<ManualValue>(reader.GetSqlBytes(0).Stream);
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
        public string SelectRowColl(out string str)
        {
            string error = null;
            str = null;

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT CollInfo FROM " + m_tableName, out reader);
                if (null == error)
                {
                    if (reader.Read())//匹配
                    {
                        str = reader.GetString(0);
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
