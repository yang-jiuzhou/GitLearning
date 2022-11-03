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
    class ComConfTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ComConfTable(int id)
        {
            m_tableName = "ComConf" + id.ToString();
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
                [Type] [int],
                [Model] [nvarchar](64),
                [PortName] [nvarchar](64),
                [Address] [nvarchar](64),
                [Port] [nvarchar](64),
                [Version] [nvarchar](64),
                [Serial] [nvarchar](64),
                [Alarm] [bit]"
                );
        }

        /// <summary>
        /// 检查表
        /// </summary>
        /// <returns></returns>
        public override string CheckTable()
        {
            bool exist = false;
            string error = ExistTable(ref exist);
            if (null == error)
            {
                if (exist)
                {
                    List<string> listName = new List<string>();
                    List<string> listType = new List<string>();
                    listName.Add("ID");
                    listName.Add("Type");
                    listName.Add("Model");
                    listName.Add("PortName");
                    listName.Add("Address");
                    listName.Add("Port");
                    listName.Add("Version");
                    listName.Add("Serial");
                    listName.Add("Alarm");

                    error = CreateNewTable(listName, listType, true);
                }
            }

            return error;
        }

        /// <summary>
        /// 获取行信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="serial"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public string GetRow(int id, ref ENUMInstrumentType type, ref string model, ref string portName, ref string address, ref string port)
        {
            string error = null;

            try
            {
                SqlDataReader reader = null;
                CreateConnAndReader(@"SELECT Type,Model,PortName FROM " + m_tableName + @" WHERE ID LIKE '" + id + "'", out reader);
                if (null != reader)
                {
                    if (reader.Read())//匹配
                    {
                        type = (ENUMInstrumentType)reader.GetInt32(0);
                        model = reader.GetString(1);
                        portName = reader.GetString(2);
                        address = reader.GetString(3);
                        port = reader.GetString(4);
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
        /// 获取ID
        /// </summary>
        /// <param name="name"></param>
        /// <param name="model"></param>
        /// <param name="portName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetID(ENUMInstrumentType type, string model, string portName, ref int id)
        {
            string error = null;

            try
            {
                SqlDataReader reader = null;
                CreateConnAndReader(@"SELECT ID FROM " + m_tableName + @" WHERE Type LIKE '" + (int)type + "' AND Model LIKE '" + model + "' AND PortName LIKE '" + portName + "'", out reader);
                if (null != reader)
                {
                    if (reader.Read())//匹配
                    {
                        id = reader.GetInt32(0);
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
        public string GetDataList(out List<ComConf> list)
        {
            string error = null;
            list = new List<ComConf>();
            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT * FROM " + m_tableName + @" ORDER BY ID", out reader);
                if (null != reader)
                {
                    while (reader.Read())
                    {
                        list.Add(new ComConf(reader.GetInt32(0), (ENUMInstrumentType)reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetBoolean(8)));
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
        /// 设置完整表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string InitDataList(List<ComConf> list)
        {
            string result = null;

            int id = -1;
            for (int i = 0; i < list.Count; i++)
            {
                result += InsertRow(list[i]);
                result += GetLastID(out id);
                list[i].MId = id;
            }

            if (string.IsNullOrEmpty(result))
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <returns></returns>
        public string InsertRow(ComConf item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + (int)item.MType);
            sb.Append("','" + item.MModel);
            sb.Append("','" + item.MPortName);
            sb.Append("','" + item.MAddress);
            sb.Append("','" + item.MPort);
            sb.Append("','" + item.MVersion);
            sb.Append("','" + item.MSerial);
            sb.Append("','" + item.MAlarm + "'");

            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 修改完整表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string UpdateDataList(List<ComConf> list)
        {
            string result = null;

            for (int i = 0; i < list.Count; i++)
            {
                result += UpdateRow(list[i]);
            }

            if (string.IsNullOrEmpty(result))
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// 修改完整表的警报
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string UpdateDataListAlarm(List<ComConf> list)
        {
            string result = null;

            for (int i = 0; i < list.Count; i++)
            {
                result += UpdateRowAlarm(list[i]);
            }

            if (string.IsNullOrEmpty(result))
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// 修改行
        /// </summary>
        /// <param name="type"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public string UpdateRow(ComConf item)
        {
            return SqlUpdateRow(@"PortName='" + item.MPortName + "',Address='" + item.MAddress + "',Port='" + item.MPort + "' WHERE ID LIKE '" + item.MId + "'");
        }

        /// <summary>
        /// 修改行
        /// </summary>
        /// <param name="type"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public string UpdateRowAlarm(ComConf item)
        {
            return SqlUpdateRow(@"Alarm='" + item.MAlarm + "' WHERE ID LIKE '" + item.MId + "'");
        }
    }
}
