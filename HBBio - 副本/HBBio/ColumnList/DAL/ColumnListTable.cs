using HBBio.Database;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.ColumnList
{
    /**
     * ClassName: ColumnListTable
     * Description: 色谱柱表
     * Version: 1.0
     * Create:  2018/05/16
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class ColumnListTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ColumnListTable()
        {
            m_tableName = "ColumnListTable";
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
            StringBuilder sb = new StringBuilder();
            sb.Append(@"[ID] [int] PRIMARY KEY IDENTITY(1,1),
                [Name] [nvarchar](128) NOT NULL UNIQUE CHECK(LEN(Name)>0),
                [Note] [nvarchar](MAX) NOT NULL,
                [User] [nvarchar](64) NOT NULL,");
            for (int i = 0; i < EnumRunParametersInfo.Count; i++)
            {
                sb.Append("[");
                sb.Append(((EnumRunParameters)i).ToString());
                if (Share.ReadXaml.GetEnum((EnumRunParameters)i, "C_").Contains("*"))
                {
                    sb.Append("] [varchar](64) NOT NULL CHECK(LEN(" + ((EnumRunParameters)i).ToString() + ")>0),");
                }
                else
                {
                    sb.Append("] [varchar](64) NOT NULL,");
                }
            }
            for (int i = 0; i < EnumDetailsInfo.Count; i++)
            {
                sb.Append("[");
                sb.Append(((EnumDetails)i).ToString());
                if (Share.ReadXaml.GetEnum((EnumDetails)i, "C_").Contains("*"))
                {
                    sb.Append("] [varchar](64) NOT NULL CHECK(LEN(" + ((EnumDetails)i).ToString() + ")>0),");
                }
                else
                {
                    sb.Append("] [varchar](64) NOT NULL,");
                }
            }
            sb.Remove(sb.Length - 1, 1);

            return SqlCreateTable(sb.ToString());
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
        /// 删除行信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string DeleteRow(string name)
        {
            return SqlDeleteRow("Name LIKE '" + name + "'");
        }

        /// <summary>
        /// 获取行信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetRow(string name, ColumnItem item)
        {
            string error = null;

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT * FROM " + m_tableName + @" WHERE Name LIKE '" + name + "'", out reader);
                if (null == error)
                {
                    if (reader.Read())//匹配
                    {
                        int index = 0;
                        index++;//第一项是ID

                        List<string> strList = new List<string>();
                        for (int i = 0; i < 3 + EnumRunParametersInfo.Count + EnumDetailsInfo.Count; i++)
                        {
                            strList.Add(reader.GetString(index++));
                        }
                        item.InItList(strList);
                    }
                    else
                    {
                        error = Share.ReadXaml.S_ErrorNoData;
                    }
                    CloseConnAndReader();
                }
            }
            catch(Exception msg)
            {
                error = msg.Message;
            }

            return error;
        }

        /// <summary>
        /// 获取行信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetRow(int id, ColumnItem item)
        {
            string error = null;

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT * FROM " + m_tableName + @" WHERE ID LIKE '" + id + "'", out reader);
                if (null == error)
                {
                    if (reader.Read())//匹配
                    {
                        int index = 0;
                        index++;//第一项是ID

                        List<string> strList = new List<string>();
                        for (int i = 0; i < 3 + EnumRunParametersInfo.Count + EnumDetailsInfo.Count; i++)
                        {
                            strList.Add(reader.GetString(index++));
                        }
                        item.InItList(strList);
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
        public string GetNameList(out List<string> list, string filter = null)
        {
            string error = null;
            list = new List<string>();

            SqlDataReader reader = null;
            if (string.IsNullOrEmpty(filter))
            {
                error = CreateConnAndReader(@"SELECT Name FROM " + m_tableName + @" ORDER BY ID", out reader);
            }
            else
            {
                error = CreateConnAndReader(@"SELECT Name FROM " + m_tableName + @" WHERE Name LIKE '%" + filter +"%' ORDER BY ID", out reader);
            }
            
            if (null == error)
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString(0));
                }
                CloseConnAndReader();
            }

            return error;
        }

        /// <summary>
        /// 获取完整表
        /// </summary>
        /// <returns></returns>
        public string GetNameList(out List<ColumnItem> list, string filter = null)
        {
            string error = null;
            list = new List<ColumnItem>();

            SqlDataReader reader = null;
            if (string.IsNullOrEmpty(filter))
            {
                error = CreateConnAndReader(@"SELECT ID,Name FROM " + m_tableName + @" ORDER BY ID", out reader);
            }
            else
            {
                error = CreateConnAndReader(@"SELECT ID,Name FROM " + m_tableName + @" WHERE Name LIKE '%" + filter + "%' ORDER BY ID", out reader);
            }

            if (null == error)
            {
                list.Add(new ColumnItem(-1, "Any"));
                while (reader.Read())
                {
                    list.Add(new ColumnItem(reader.GetInt32(0), reader.GetString(1)));
                }
                CloseConnAndReader();
            }

            return error;
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string InsertRow(ColumnItem item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + item.MName);
            sb.Append("','" + item.MNote);
            sb.Append("','" + item.MUser);
            for (int i = 0; i < EnumRunParametersInfo.Count; i++)
            {
                sb.Append("','" + item.MRP.MList[i].MStr);
            }
            for (int i = 0; i < EnumDetailsInfo.Count; i++)
            {
                sb.Append("','" + item.MDT.MList[i].MStr);
            }
            sb.Append( "'");

            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 修改行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string UpdateRow(ColumnItem item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Note='" + item.MNote + "',");
            for (int i = 0; i < EnumRunParametersInfo.Count; i++)
            {
                sb.Append(((EnumRunParameters)i).ToString() + "='" + item.MRP.MList[i].MStr + "',");
            }
            for (int i = 0; i < EnumDetailsInfo.Count; i++)
            {
                sb.Append(((EnumDetails)i).ToString() + "='" + item.MDT.MList[i].MStr + "',");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(" WHERE Name LIKE '" + item.MName + "'");

            return SqlUpdateRow(sb.ToString());
        }
    }
}
