using HBBio.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HBBio.Print
{
    /**
     * ClassName: PDFSetTable
     * Description: 日志导出PDF设置信息表
     * Version: 1.0
     * Create:  2020/09/07
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class PDFSetTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PDFSetTable()
        {
            m_tableName = "PDFSetTable";
        }

        /// <summary>
        /// 插入默认值
        /// </summary>
        /// <returns></returns>
        protected override string AddDefaultValue()
        {
            return InsertRow(new PDFSet());
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns></returns>
        protected override string CreateTable()
        {
            return SqlCreateTable(
                @"[Icon] [nvarchar](max) NOT NULL,
                    [Title] [nvarchar](max) NOT NULL,
                    [MarginLeft] [int] NOT NULL,
                    [MarginRight] [int] NOT NULL,
                    [MarginTop] [int] NOT NULL,
                    [MarginBottom] [int] NOT NULL,
                    [MarkerStyle] [int] NOT NULL,
                    [Signer] [bit] NOT NULL,
                    [Reviewer] [bit] NOT NULL,
                    [OutputTime] [bit] NOT NULL,
                    [FontSize] [int] NOT NULL,
                    [FontFamily] [int] NOT NULL,
                    [ColorBack] [varchar](32) NOT NULL,
                    [ColorFore] [varchar](32) NOT NULL"
                );
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string InsertRow(PDFSet item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + item.m_icon);
            sb.Append("','" + item.m_title);
            sb.Append("','" + item.m_marginLeft);
            sb.Append("','" + item.m_marginRight);
            sb.Append("','" + item.m_marginTop);
            sb.Append("','" + item.m_marginBottom);
            sb.Append("','" + item.m_markerStyle);
            sb.Append("','" + item.m_signer);
            sb.Append("','" + item.m_reviewer);
            sb.Append("','" + item.m_outputTime);
            sb.Append("','" + item.m_fontSize);
            sb.Append("','" + item.m_fontFamily);
            sb.Append("','" + item.m_colorBack.ToString());
            sb.Append("','" + item.m_colorFore.ToString() + "'");

            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 获取行信息
        /// </summary>
        /// <returns></returns>
        public string UpdateRow(PDFSet item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Icon='" + item.m_icon);
            sb.Append("',Title='" + item.m_title);
            sb.Append("',MarginLeft='" + item.m_marginLeft);
            sb.Append("',MarginRight='" + item.m_marginRight);
            sb.Append("',MarginTop='" + item.m_marginTop);
            sb.Append("',MarginBottom='" + item.m_marginBottom);
            sb.Append("',MarkerStyle='" + item.m_markerStyle);
            sb.Append("',Signer='" + item.m_signer);
            sb.Append("',Reviewer='" + item.m_reviewer);
            sb.Append("',OutputTime='" + item.m_outputTime);
            sb.Append("',FontSize='" + item.m_fontSize);
            sb.Append("',FontFamily='" + item.m_fontFamily);
            sb.Append("',ColorBack='" + item.m_colorBack.ToString());
            sb.Append("',ColorFore='" + item.m_colorFore.ToString() + "'");

            return SqlUpdateRow(sb.ToString());
        }

        /// <summary>
        /// 获取行信息
        /// </summary>
        /// <returns></returns>
        public string SelectRow(out PDFSet item)
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
                        item = new PDFSet();
                        int index = 0;
                        item.m_icon = reader.GetString(index++);
                        item.m_title = reader.GetString(index++);
                        item.m_marginLeft = reader.GetInt32(index++);
                        item.m_marginRight = reader.GetInt32(index++);
                        item.m_marginTop = reader.GetInt32(index++);
                        item.m_marginBottom = reader.GetInt32(index++);
                        item.m_markerStyle = reader.GetInt32(index++);
                        item.m_signer = reader.GetBoolean(index++);
                        item.m_reviewer = reader.GetBoolean(index++);
                        item.m_outputTime = reader.GetBoolean(index++);
                        item.m_fontSize = reader.GetInt32(index++);
                        item.m_fontFamily = reader.GetInt32(index++);
                        BrushConverter brushConverter = new BrushConverter();
                        item.m_colorBack = (Brush)brushConverter.ConvertFromString(reader.GetString(index++));
                        item.m_colorFore = (Brush)brushConverter.ConvertFromString(reader.GetString(index++));
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
