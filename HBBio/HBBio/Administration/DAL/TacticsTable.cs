using HBBio.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Administration
{
    /**
     * ClassName: TacticsTable
     * Description: 安全策略表
     * Version: 1.0
     * Create:  2020/09/07
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class TacticsTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TacticsTable()
        {
            m_tableName = "TacticsTable";
        }

        /// <summary>
        /// 插入默认值
        /// </summary>
        /// <returns></returns>
        protected override string AddDefaultValue()
        {
            return InsertRow(new TacticsInfo());
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns></returns>
        protected override string CreateTable()
        {
            StringBuilder sb = new StringBuilder();
            int columnCount= Enum.GetNames(typeof(EnumTactics)).GetLength(0);
            for (int i = 0; i < columnCount; i++)
            {
                sb.Append("[" + ((EnumTactics)i).ToString() + "] [int] NOT NULL,");
            }
            sb.Remove(sb.Length - 1, 1);

            return SqlCreateTable(sb.ToString());
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string InsertRow(TacticsInfo item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + item.NameReg + "',");
            sb.Append("'" + item.NameLock + "',");
            sb.Append("'" + item.PwdReg + "',");
            sb.Append("'" + item.PwdLength + "',");
            sb.Append("'" + item.PwdMaxTime + "',");    
            sb.Append("'" + item.ScreenLock + "'");

            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 修改行中的某一列
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string UpdateRow(EnumTactics index, int value)
        {
            return SqlUpdateRow(index.ToString() + "='" + value + "'");
        }

        /// <summary>
        /// 获取行信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string SelectRow(out TacticsInfo item)
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
                        item = new TacticsInfo();
                        item.NameReg = reader.GetInt32(index++);
                        item.NameLock = reader.GetInt32(index++);
                        item.PwdReg = reader.GetInt32(index++);
                        item.PwdLength = reader.GetInt32(index++);
                        item.PwdMaxTime = reader.GetInt32(index++);
                        item.ScreenLock = reader.GetInt32(index++);
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
