using HBBio.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: MethodTable
     * Description: 方法表
     * Version: 1.0
     * Create:  2020/11/12
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    class MethodTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MethodTable()
        {
            m_tableName = "MethodTable";
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
            string error = null;
            error += SqlCreateTable(
                @"[ID] [int] PRIMARY KEY IDENTITY(1, 1),
                [CommunicationSetsID] [int],
                [ProjectID] [int] FOREIGN KEY REFERENCES ProjectTreeTable(ID),
                [MethodName] [nvarchar](64) NOT NULL,
                [MethodType] [int] NOT NULL,
                [IDList] [varchar](MAX),
                [StreamInfo] [varbinary](MAX),
                CONSTRAINT CommunicationSetsID_ProjectID_MethodName_MethodType UNIQUE (CommunicationSetsID,ProjectID,MethodName,MethodType)"
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
        /// <param name="item"></param>
        /// <returns></returns>
        public string InsertRow(Method item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO " + m_tableName + "(CommunicationSetsID,ProjectID,MethodName,MethodType,IDList,StreamInfo) VALUES(");
            sb.Append("'" + item.MCommunicationSetsID);
            sb.Append("','" + item.MProjectID);
            sb.Append("','" + item.MName);
            sb.Append("','" + (int)item.MType);

            sb.Append("','" + DBNull.Value);
            sb.Append("',@StreamInfo)");

            return SqlBaseCDIU(sb.ToString(), "StreamInfo", Share.DeepCopy.GetMemoryStream(item));
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string InsertRow(MethodQueue item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + item.MCommunicationSetsID);
            sb.Append("','" + item.MProjectID);
            sb.Append("','" + item.MName);
            sb.Append("','" + (int)item.MType);

            sb.Append("','" + item.GetMethodInfo());

            sb.Append("'," + System.Data.SqlTypes.SqlBinary.Null);

            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteRow(int id)
        {
            return SqlDeleteRow("ID='" + id + "'");
        }

        /// <summary>
        /// 修改行
        /// </summary>
        /// <param name="type"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public string UpdateMethod(Method item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE ");
            sb.Append(m_tableName);
            sb.Append(" SET ");
            sb.Append("MethodName='" + item.MName);
            sb.Append("',IDList='" + DBNull.Value);
            sb.Append("',StreamInfo=@StreamInfo");
            sb.Append(" WHERE ID='" + item.MID + "'");

            return SqlBaseCDIU(sb.ToString(), "StreamInfo", Share.DeepCopy.GetMemoryStream(item));
        }

        /// <summary>
        /// 修改行
        /// </summary>
        /// <param name="type"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public string UpdateMethod(MethodQueue item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("MethodName='" + item.MName);
            sb.Append("',IDList='" + item.GetMethodInfo());
            sb.Append("' WHERE ID='" + item.MID + "'");

            return SqlUpdateRow(sb.ToString());
        }

        /// <summary>
        /// 重命名
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string UpdateMethodName(MethodType item)
        {
            return SqlUpdateRow(@"MethodName='" + item.MName + "' WHERE ID='" + item.MID + "'");
        }

        /// <summary>
        /// 获取行信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string SelectRow(int id, out Method item)
        {
            string error = null;
            item = null;

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT ID,StreamInfo FROM " + m_tableName + @" WHERE ID='" + id + "'", out reader);
                if (null == error)
                {
                    if (reader.Read())//匹配
                    {
                        item = Share.DeepCopy.SetMemoryStream<Method>(reader.GetSqlBytes(1).Stream);
                        item.MID = reader.GetInt32(0);
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
        /// 获取行信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string SelectRow(int id, out MethodQueue item)
        {
            string error = null;
            item = null;

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT * FROM " + m_tableName + @" WHERE ID='" + id + "'", out reader);
                if (null == error)
                {
                    if (reader.Read())//匹配
                    {
                        int index = 0;
                        item = new MethodQueue(reader.GetInt32(index++), reader.GetInt32(index++), reader.GetInt32(index++), reader.GetString(index++), (EnumMethodType)reader.GetInt32(index++));
                        item.SetMethodInfo(reader.GetString(index++));
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
        /// 获取行信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string SelectRowType(int id, out EnumMethodType type)
        {
            string error = null;
            type = EnumMethodType.Method;

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT MethodType FROM " + m_tableName + @" WHERE ID='" + id + "'", out reader);
                if (null == error)
                {
                    if (reader.Read())//匹配
                    {
                        type = (EnumMethodType)reader.GetInt32(0);
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
        public string SelectListName(int communicationSetsID, int projectID, string filter, out List<MethodType> list)
        {
            string error = null;
            list = new List<MethodType>();

            try
            {
                SqlDataReader reader = null;
                if (string.IsNullOrEmpty(filter))
                {
                    error = CreateConnAndReader(@"SELECT ID,MethodName,MethodType FROM " + m_tableName + @" WHERE CommunicationSetsID='" + communicationSetsID + "' AND ProjectID ='" + projectID + "' ORDER BY ID", out reader);
                }
                else
                {
                    error = CreateConnAndReader(@"SELECT ID,MethodName,MethodType FROM " + m_tableName + @" WHERE CommunicationSetsID='" + communicationSetsID + "' AND ProjectID ='" + projectID + "' AND MethodName LIKE '%" + filter + "%' ORDER BY ID", out reader);
                }

                if (null == error)
                {
                    while (reader.Read())
                    {
                        list.Add(new MethodType(reader.GetInt32(0), reader.GetString(1), (EnumMethodType)reader.GetInt32(2)));
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
        /// 所有方法生成xml文件
        /// </summary>
        /// <returns></returns>
        public string CreateXml()
        {
            string error = null;

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT ID,StreamInfo FROM " + m_tableName + @" ORDER BY ID", out reader);
                if (null == error)
                {
                    while (reader.Read())//匹配
                    {
                        try
                        {
                            Method item = Share.DeepCopy.SetMemoryStream<Method>(reader.GetSqlBytes(1).Stream);
                            item.MID = reader.GetInt32(0);

                            Share.XmlSerialize.Serialize(BaseDB.m_pathError + item.MID + "method.xml", item);
                        }
                        catch
                        { }
                        
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
        /// 从xml中修复
        /// </summary>
        /// <returns></returns>
        public string RepairXml()
        {
            string error = null;

            try
            {
                DirectoryInfo dir = new DirectoryInfo(BaseDB.m_pathError);
                FileInfo[] fil = dir.GetFiles();
                DirectoryInfo[] dii = dir.GetDirectories();
                foreach (FileInfo f in fil)
                {
                    if (f.FullName.Contains("method.xml"))
                    {
                        Method item = Share.XmlSerialize.DeSerialize<Method>(f.FullName);
                        UpdateMethod(item);
                    }
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
