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
                [ConstName] [nvarchar](64) UNIQUE,
                [DlyName] [nvarchar](64) UNIQUE,
                [Brush] [varchar](32),
                [Unit] [nvarchar](32),
                [ColorNew] [varchar](32),
                [ShowNew] [bit],
                [ColorOld] [varchar](32),
                [ShowOld] [bit],
                [ContrastOld] [bit],
                [ValLL] [float],
                [ValL] [float],
                [ValH] [float],
                [ValHH] [float],
                [ValMin] [float],
                [ValMax] [float],
                [Smooth] [int],
                [IsLine] [bit],
                [IsAlarmWarning] [bit],
                [IsShow] [bit],
                [BaseInstrumentID] [int] FOREIGN KEY REFERENCES BaseInstrument" + m_id + @"(ID)"
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
                    listName.Add("ConstName");
                    listName.Add("DlyName");
                    listName.Add("Brush");
                    listName.Add("Unit");
                    listName.Add("ColorNew");
                    listName.Add("ShowNew");
                    listName.Add("ColorOld");
                    listName.Add("ShowOld");
                    listName.Add("ContrastOld");
                    listName.Add("ValLL");
                    listName.Add("ValL");
                    listName.Add("ValH");
                    listName.Add("ValHH");
                    listName.Add("ValMin");
                    listName.Add("ValMax");
                    listName.Add("Smooth");
                    listName.Add("IsLine");
                    listName.Add("IsAlarmWarning");
                    listName.Add("IsShow");
                    listName.Add("BaseInstrumentID");

                    error = CreateNewTable(listName, listType, true);
                }
            }

            return error;
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
