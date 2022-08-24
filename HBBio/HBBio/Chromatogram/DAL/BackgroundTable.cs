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
                sb.Append("[" + ((EnumBackground)i).ToString() + "] [varchar](32) NOT NULL,");
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
            sb.Append("'" + Share.ValueTrans.DrawToMedia(item.MPhaseColor).ToString() + "'");

            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 修改行中的某一列
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string UpdateRow(EnumBackground index, Color value)
        {
            return SqlUpdateRow(index.ToString() + "='" + Share.ValueTrans.DrawToMedia(value).ToString() + "'");
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
