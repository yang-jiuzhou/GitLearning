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
    class BaseInstrumentTable : BaseTable
    {
        private int m_id = -1;


        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseInstrumentTable(int id)
        {
            m_id = id;
            m_tableName = "BaseInstrument" + m_id;
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
                [Index] [int] NOT NULL,
                [ConstName] [nvarchar](64) NOT NULL UNIQUE,
                [DlyName] [nvarchar](64) NOT NULL UNIQUE,
                [EnableRunTime] [bit] NOT NULL,
                [EnableCalibration] [bit] NOT NULL,
                [PtX] [float] NOT NULL,
                [PtY] [float] NOT NULL,
                [ComConfID] [int] FOREIGN KEY REFERENCES ComConf" + m_id + @"(ID),
                [TimeSetID] [int] FOREIGN KEY REFERENCES TimeSet(ID)"
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
        public string GetDataListByComConfID(int id, ref List<BaseInstrument> list)
        {
            string error = null;

            SqlDataReader reader = null;
            error = CreateConnAndReader(@"SELECT * FROM " + m_tableName + @" WHERE ComConfID ='" + id + "' ORDER BY ID", out reader);
            if (null != reader)
            {
                int index = 0;
                while (reader.Read())
                {
                    index = 0;
                    list[reader.GetInt32(1)].GetDataFromDB(reader.GetInt32(index++), reader.GetInt32(index++), reader.GetString(index++), reader.GetString(index++)
                        , reader.GetBoolean(index++), reader.GetBoolean(index++), new System.Windows.Point(reader.GetDouble(index++), reader.GetDouble(index++))
                        , reader.GetInt32(index++), reader.GetInt32(index++));
                }

                foreach (var it in list)
                {
                    if(-1 == it.MId)
                    {
                        it.MVisible = false;
                    } 
                }

                CloseConnAndReader();
            }

            return error;
        }

        /// <summary>
        /// 获取行
        /// </summary>
        /// <param name="id"></param>
        /// <param name="runTime"></param>
        /// <param name="calibration"></param>
        /// <returns></returns>
        public string GetRow(int id, ref BaseInstrument item)
        {
            string error = null;

            try
            {
                SqlDataReader reader = null;
                CreateConnAndReader(@"SELECT * FROM " + m_tableName + @" WHERE ID LIKE '" + id + "'", out reader);
                if (null != reader)
                {
                    if (reader.Read())//匹配
                    {
                        item = new BaseInstrument();
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
        /// <param name="constName"></param>
        /// <param name="dlyName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetID(string constName, string dlyName, ref int id)
        {
            string error = null;

            try
            {
                SqlDataReader reader = null;
                CreateConnAndReader(@"SELECT ID FROM " + m_tableName + @" WHERE ConstName LIKE '" + constName + "' AND DlyName LIKE '" + dlyName + "'", out reader);
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
        /// 设置完整表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string InitDataList(List<BaseInstrument> list)
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
        public string InsertRow(BaseInstrument item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + item.MIndex);
            sb.Append("','" + item.MConstName);
            sb.Append("','" + item.MDlyName);
            sb.Append("','" + item.MEnableRunTime);
            sb.Append("','" + item.MEnableCalibration);
            sb.Append("','" + item.MPt.X);
            sb.Append("','" + item.MPt.Y);
            sb.Append("','" + item.MComConfId);
            sb.Append("','" + item.MTimeSetId + "'");

            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 修改完整表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string UpdateDataList(List<BaseInstrument> list)
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
        /// 修改行
        /// </summary>
        /// <param name="type"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public string UpdateRow(BaseInstrument item)
        {
            return SqlUpdateRow(@"DlyName='" + item.MDlyName + "',EnableRunTime='" + item.MEnableRunTime + "',EnableCalibration='" + item.MEnableCalibration + "',PtX='" + item.MPt.X + "',PtY='" + item.MPt.Y + "' WHERE ID LIKE '" + item.MId + "'");
        }
    }
}
