using HBBio.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    /**
     * ClassName: TacticsTable
     * Description: 安全策略表
     * Version: 1.0
     * Create:  2020/09/07
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class InstrumentSizeTable : BaseTable
    {
        private int m_id = -1;


        /// <summary>
        /// 构造函数
        /// </summary>
        public InstrumentSizeTable(int id)
        {
            m_id = id;
            m_tableName = "InstrumentSize" + m_id;
            m_connStr = BaseDB.ComDB;
        }

        /// <summary>
        /// 插入默认值
        /// </summary>
        /// <returns></returns>
        protected override string AddDefaultValue()
        {
            return InsertRow(new InstrumentSize());
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns></returns>
        protected override string CreateTable()
        {
            return SqlCreateTable(
                @"[Width] [int] NOT NULL,
                [Height] [int] NOT NULL"
                );
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <returns></returns>
        public string InsertRow(InstrumentSize item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + item.MWidth);
            sb.Append("','" + item.MHeight + "'");

            return SqlInsertRow(sb.ToString());
        }
		
		/// <summary>
        /// 修改行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string UpdateRow(InstrumentSize item)
        {
            return SqlUpdateRow("Width='" + item.MWidth + "',Height='" + item.MHeight + "'");
        }

        /// <summary>
        /// 获取行信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string SelectRow(out InstrumentSize item)
        {
            string error = null;
            item = new InstrumentSize();

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT * FROM " + m_tableName, out reader);
                if (null == error)
                {
                    if (reader.Read())//匹配
                    {
                        int index = 0;
                        item.MWidth = reader.GetInt32(index++);
                        item.MHeight = reader.GetInt32(index++);
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
