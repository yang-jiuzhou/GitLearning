using HBBio.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.TubeStand
{
    /**
     * ClassName: TubeStandTable
     * Description: 试管架表
     * Version: 1.0
     * Create:  2018/05/16
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class TubeStandTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TubeStandTable()
        {
            m_tableName = "TubeStandTable";
        }

        /// <summary>
        /// 插入默认值
        /// </summary>
        /// <returns></returns>
        protected override string AddDefaultValue()
        {
            InsertRow(new TubeStandItem(2, 12, 7));
            InsertRow(new TubeStandItem(15, 12, 5));
            InsertRow(new TubeStandItem(5, 12, 5));
            InsertRow(new TubeStandItem(7, 11, 4));
            InsertRow(new TubeStandItem(10, 10, 4));
            InsertRow(new TubeStandItem(15, 10, 4));
            InsertRow(new TubeStandItem(25, 8, 3));
            InsertRow(new TubeStandItem(50, 7, 3));
            InsertRow(new TubeStandItem(30, 6, 2));
            InsertRow(new TubeStandItem(50, 6, 2));
            return null;
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns></returns>
        protected override string CreateTable()
        {
            return SqlCreateTable(@"[ID] [int] PRIMARY KEY IDENTITY(1,1),
                [Name] [nvarchar](128) NOT NULL,
                [CollVolume] [float] NOT NULL,
                [Volume] [float] NOT NULL,
                [Count] [int] NOT NULL,
                [Diameter] [float] NOT NULL,
                [Height] [float] NOT NULL,
                [Row] [int] NOT NULL,
                [Col] [int] NOT NULL,
                CONSTRAINT Volume_Count UNIQUE (Volume,Count)");
        }

        /// <summary>
        /// 删除行信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string DeleteRow(double volume, int count)
        {
            return SqlDeleteRow("Volume LIKE '" + volume + "' AND Count LIKE '" + count + "'");
        }

        /// <summary>
        /// 获取行信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetRow(double volume, int count, ref TubeStandItem item)
        {
            string error = null;

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT * FROM " + m_tableName + @" WHERE Volume LIKE '" + volume + "' AND Count LIKE '" + count + "'", out reader);
                if (null == error)
                {
                    if (reader.Read())//匹配
                    {
                        if (null == item)
                        {
                            item = new TubeStandItem();
                        }
                        int index = 1;//第一项是ID
                        item.MName = reader.GetString(index++);
                        item.MCollVolume = reader.GetDouble(index++);
                        item.MVolume = reader.GetDouble(index++);
                        item.MCount = reader.GetInt32(index++);
                        item.MDiameter = reader.GetDouble(index++);
                        item.MHeight = reader.GetDouble(index++);
                        item.MRow = reader.GetInt32(index++);
                        item.MCol = reader.GetInt32(index++);
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
        public string GetList(out List<TubeStandItem> list)
        {
            string error = null;
            list = new List<TubeStandItem>();

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT * FROM " + m_tableName + @" ORDER BY ID", out reader);
                if (null == error)
                {
                    while (reader.Read())
                    {
                        TubeStandItem item = new TubeStandItem();
                        int index = 1;//第一项是ID
                        item.MName = reader.GetString(index++);
                        item.MCollVolume = reader.GetDouble(index++);
                        item.MVolume = reader.GetDouble(index++);
                        item.MCount = reader.GetInt32(index++);
                        item.MDiameter = reader.GetDouble(index++);
                        item.MHeight = reader.GetDouble(index++);
                        item.MRow = reader.GetInt32(index++);
                        item.MCol = reader.GetInt32(index++);
                        list.Add(item);
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
        public string InsertRow(TubeStandItem item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + item.MName);
            sb.Append("','" + item.MCollVolume);
            sb.Append("','" + item.MVolume);
            sb.Append("','" + item.MCount);
            sb.Append("','" + item.MDiameter);
            sb.Append("','" + item.MHeight);
            sb.Append("','" + item.MRow);
            sb.Append("','" + item.MCol + "'");

            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 修改行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string UpdateRow(TubeStandItem item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("CollVolume='" + item.MCollVolume + "'");
            sb.Append(" WHERE Volume LIKE '" + item.MVolume + "' AND Count LIKE '" + item.MCount + "'");

            return SqlUpdateRow(sb.ToString());
        }
    }
}
