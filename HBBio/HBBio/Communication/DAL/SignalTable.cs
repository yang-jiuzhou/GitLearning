using HBBio.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HBBio.Communication
{
    class SignalTable : BaseTable
    {
        private int m_id = -1;


        /// <summary>
        /// 构造函数
        /// </summary>
        public SignalTable(int id)
        {
            m_id = id;
            m_tableName = "Signal" + m_id;
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
                [ConstName] [nvarchar](64) NOT NULL UNIQUE,
                [DlyName] [nvarchar](64) NOT NULL UNIQUE,
                [Brush] [varchar](32) NOT NULL,
                [Unit] [nvarchar](32) NOT NULL,
                [ColorNew] [varchar](32) NOT NULL,
                [ShowNew] [bit] NOT NULL,
                [ColorOld] [varchar](32) NOT NULL,
                [ShowOld] [bit] NOT NULL,
                [ContrastOld] [bit] NOT NULL,
                [ValLL] [float] NOT NULL,
                [ValL] [float] NOT NULL,
                [ValH] [float] NOT NULL,
                [ValHH] [float] NOT NULL,
                [ValMin] [float] NOT NULL,
                [ValMax] [float] NOT NULL,
                [Smooth] [int] NOT NULL,
                [IsLine] [bit] NOT NULL,
                [IsAlarmWarning] [bit] NOT NULL,
                [IsShow] [bit] NOT NULL,
                [BaseInstrumentID] [int] FOREIGN KEY REFERENCES BaseInstrument" + m_id + @"(ID)"
                );
        }

        /// <summary>
        /// 获取完整表
        /// </summary>
        /// <returns></returns>
        public string GetDataListByBaseInstrumentID(int id, out List<Signal> list)
        {
            string error = null;
            list = new List<Signal>();

            SqlDataReader reader = null;
            CreateConnAndReader(@"SELECT * FROM " + m_tableName + @" WHERE BaseInstrumentID ='" + id + "' ORDER BY ID", out reader);
            if (null != reader)
            {
                while (reader.Read())
                {
                    int index = 0;
                    Signal item = new Signal(reader.GetInt32(index++), reader.GetString(index++), reader.GetString(index++), new SolidColorBrush((Color)ColorConverter.ConvertFromString(reader.GetString(index++))), reader.GetString(index++)
                        , (Color)ColorConverter.ConvertFromString(reader.GetString(index++)), reader.GetBoolean(index++)
                        , (Color)ColorConverter.ConvertFromString(reader.GetString(index++)), reader.GetBoolean(index++), reader.GetBoolean(index++)
                        , reader.GetDouble(index++), reader.GetDouble(index++), reader.GetDouble(index++), reader.GetDouble(index++), reader.GetDouble(index++), reader.GetDouble(index++)
                        , reader.GetInt32(index++)
                        , reader.GetBoolean(index++), reader.GetBoolean(index++), reader.GetBoolean(index++)
                        , reader.GetInt32(index++));

                    list.Add(item);
                }
                CloseConnAndReader();
            }

            return error;
        }

        /// <summary>
        /// 设置完整表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string InitDataList(List<Signal> list)
        {
            string result = null;

            for (int i = 0; i < list.Count; i++)
            {
                result += InsertRow(list[i]);
            }

            if (string.IsNullOrEmpty(result))
            {
                result = null;
            }

            return result; ;
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <returns></returns>
        public string InsertRow(Signal item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + item.MConstName);
            sb.Append("','" + item.MDlyName);
            sb.Append("','" + item.MBrush.ToString());
            sb.Append("','" + item.MUnit);
            sb.Append("','" + item.MColorNew.ToString());
            sb.Append("','" + item.MShowNew);
            sb.Append("','" + item.MColorOld.ToString());
            sb.Append("','" + item.MShowOld);
            sb.Append("','" + item.MContrastOld);
            sb.Append("','" + item.MValLL);
            sb.Append("','" + item.MValL);
            sb.Append("','" + item.MValH);
            sb.Append("','" + item.MValHH);
            sb.Append("','" + item.MValMin);
            sb.Append("','" + item.MValMax);
            sb.Append("','" + item.MSmooth);
            sb.Append("','" + item.MIsLine);
            sb.Append("','" + item.MIsAlarmWarning);
            sb.Append("','" + item.MIsShow);
            sb.Append("','" + item.MBaseInstrumentId + "'");

            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 修改完整表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string UpdateDataList(List<Signal> list)
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
        private string UpdateRow(Signal item)
        {
            return SqlUpdateRow(@"ConstName='" + item.MConstName
                 + "',DlyName='" + item.MDlyName
                 + "',Brush='" + item.MBrush.ToString()
                 + "',Unit='" + item.MUnit
                + "',ColorNew='" + item.MColorNew.ToString()
                + "',ShowNew='" + item.MShowNew
                + "',ColorOld='" + item.MColorOld.ToString()
                + "',ShowOld='" + item.MShowOld
                + "',ContrastOld='" + item.MContrastOld
                + "',ValLL='" + item.MValLL
                + "',ValL='" + item.MValL
                + "',ValH='" + item.MValH
                + "',ValHH='" + item.MValHH
                + "',ValMin='" + item.MValMin
                + "',ValMax='" + item.MValMax
                + "',Smooth='" + item.MSmooth
                + "',IsLine='" + item.MIsLine
                + "',IsAlarmWarning='" + item.MIsAlarmWarning
                + "',IsShow='" + item.MIsShow
                + "' WHERE ID LIKE '" + item.MId + "'");
        }

        /// <summary>
        /// 修改行
        /// </summary>
        /// <param name="type"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public string UpdateRowTotalFIT(Signal item)
        {
            return SqlUpdateRow(@"ValMax='" + item.MValMax + "' WHERE ConstName LIKE '" + item.MConstName + "'");
        }
    }
}
