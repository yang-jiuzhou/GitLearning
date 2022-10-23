using HBBio.Database;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.SystemControl
{
    /**
     * ClassName: ConfCheckableTable
     * Description: 主界面设置表
     * Version: 1.0
     * Create:  2021/08/05
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class ConfCheckableTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ConfCheckableTable()
        {
            m_tableName = "ConfCheckableTable";
        }

        /// <summary>
        /// 插入默认值
        /// </summary>
        /// <returns></returns>
        protected override string AddDefaultValue()
        {
            return InsertRow(new ConfCheckable());
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns></returns>
        protected override string CreateTable()
        {
            return SqlCreateTable(@"[LanguageSelect] [int],
                                    [RememberSize] [bit]");
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
                    listName.Add("LanguageSelect");
                    listName.Add("RememberSize");

                    error = CreateNewTable(listName, listType, false);
                }
            }

            return error;
        }

        /// <summary>
        /// 获取行信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetRow(ConfCheckable item)
        {
            string error = null;

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT * FROM " + m_tableName + @" WHERE 1=1", out reader);
                if (null == error)
                {
                    if (reader.Read())//匹配
                    {
                        int index = 0;
                        item.MEnumLanguage = (EnumLanguage)reader.GetInt32(index++);
                        item.MRememberSize = reader.GetBoolean(index++);
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
        /// 插入行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string InsertRow(ConfCheckable item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + (int)item.MEnumLanguage);
            sb.Append("','" + item.MRememberSize + "'");
            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 修改行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string UpdateRow(ConfCheckable item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("LanguageSelect='" + (int)item.MEnumLanguage);
            sb.Append("',RememberSize='" + item.MRememberSize + "'");
            return SqlUpdateRow(sb.ToString());
        }
    }
}
