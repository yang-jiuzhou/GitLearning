using HBBio.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Chromatogram
{
    class BackgroundTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BackgroundTable()
        {
            m_tableName = "BackgroundTable";
        }

        /// <summary>
        /// 插入默认值
        /// </summary>
        /// <returns></returns>
        protected override string AddDefaultValue()
        {
            return InsertRow(new BackgroundInfo());
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns></returns>
        protected override string CreateTable()
        {
            StringBuilder sb = new StringBuilder();
            int columnCount = Enum.GetNames(typeof(EnumBackground)).GetLength(0);
            for (int i = 0; i < columnCount; i++)
            {
                sb.Append("[" + ((EnumBackground)i).ToString() + "_C] [varchar](32) NOT NULL,");
            }
            for (int i = 0; i < columnCount; i++)
            {
                sb.Append("[" + ((EnumBackground)i).ToString() + "_V] [bit] NOT NULL,");
            }
            for (int i = 0; i < columnCount; i++)
            {
                sb.Append("[" + ((EnumBackground)i).ToString() + "_D] [bit] NOT NULL,");
            }
            sb.Remove(sb.Length - 1, 1);

            return SqlCreateTable(sb.ToString());
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string InsertRow(BackgroundInfo item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + Share.ValueTrans.DrawToMedia(item.MMarkerColor).ToString() + "',");
            sb.Append("'" + Share.ValueTrans.DrawToMedia(item.MCollColorM).ToString() + "',");
            sb.Append("'" + Share.ValueTrans.DrawToMedia(item.MCollColorA).ToString() + "',");
            sb.Append("'" + Share.ValueTrans.DrawToMedia(item.MValveColor).ToString() + "',");
            sb.Append("'" + Share.ValueTrans.DrawToMedia(item.MPhaseColor).ToString() + "',");

            sb.Append("'" + item.MMarkerVisible + "',");
            sb.Append("'" + item.MCollMVisible + "',");
            sb.Append("'" + item.MCollAVisible + "',");
            sb.Append("'" + item.MValveVisible + "',");
            sb.Append("'" + item.MPhaseVisible + "',");

            sb.Append("'" + item.MMarkerDirection + "',");
            sb.Append("'" + item.MCollMDirection + "',");
            sb.Append("'" + item.MCollADirection + "',");
            sb.Append("'" + item.MValveDirection + "',");
            sb.Append("'" + item.MPhaseDirection + "'");

            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 修改行中的某一颜色列
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string UpdateRowColor(EnumBackground index, Color value)
        {
            return SqlUpdateRow(index.ToString() + "_C='" + Share.ValueTrans.DrawToMedia(value).ToString() + "'");
        }

        /// <summary>
        /// 修改行中的某一显隐列
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string UpdateRowVisible(EnumBackground index, bool value)
        {
            return SqlUpdateRow(index.ToString() + "_V='" + value + "'");
        }

        /// <summary>
        /// 修改行中的某一方向列
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string UpdateRowDirection(EnumBackground index, bool value)
        {
            return SqlUpdateRow(index.ToString() + "_D='" + value + "'");
        }

        /// <summary>
        /// 获取行信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string SelectRow(out BackgroundInfo item)
        {
            string error = null;
            item = null;

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT * FROM " + m_tableName, out reader);
                if (null == error)
                {
                    if (reader.Read())//匹配
                    {
                        int index = 0;
                        item = new BackgroundInfo();
                        item.MMarkerColor = Share.ValueTrans.MediaToDraw((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(reader.GetString(index++)));
                        item.MCollColorM = Share.ValueTrans.MediaToDraw((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(reader.GetString(index++)));
                        item.MCollColorA = Share.ValueTrans.MediaToDraw((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(reader.GetString(index++)));
                        item.MValveColor = Share.ValueTrans.MediaToDraw((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(reader.GetString(index++)));
                        item.MPhaseColor = Share.ValueTrans.MediaToDraw((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(reader.GetString(index++)));

                        item.MMarkerVisible = reader.GetBoolean(index++);
                        item.MCollMVisible = reader.GetBoolean(index++);
                        item.MCollAVisible = reader.GetBoolean(index++);
                        item.MValveVisible = reader.GetBoolean(index++);
                        item.MPhaseVisible = reader.GetBoolean(index++);

                        item.MMarkerDirection = reader.GetBoolean(index++);
                        item.MCollMDirection = reader.GetBoolean(index++);
                        item.MCollADirection = reader.GetBoolean(index++);
                        item.MValveDirection = reader.GetBoolean(index++);
                        item.MPhaseDirection = reader.GetBoolean(index++);
                    }
                    else
                    {
                        error = Share.ReadXaml.S_ErrorNoData;
                    } 
                }
            }
            catch (Exception msg)
            {
                error = msg.Message;
            }
            finally
            {
                CloseConnAndReader();
            }

            if (!string.IsNullOrEmpty(error))
            {
                DropTable();
                InitTable();
            }

            return error;
        }
    }
}
