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
     * ClassName: SignerReviewerTable
     * Description: 签名审核表
     * Version: 1.0
     * Create:  2020/09/07
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class SignerReviewerTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SignerReviewerTable()
        {
            m_tableName = "SignerReviewerTable";
        }

        /// <summary>
        /// 插入默认值
        /// </summary>
        /// <returns></returns>
        protected override string AddDefaultValue()
        {
            return InsertRow(new SignerReviewerInfo());
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns></returns>
        protected override string CreateTable()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[ID] [int] NOT NULL,");
            for (int i = 0; i < Enum.GetNames(typeof(EnumSignerReviewer)).GetLength(0); i++)
            {
                sb.Append("[" + ((EnumSignerReviewer)i).ToString() + "] [bit] NOT NULL,");
            }
            sb.Remove(sb.Length - 1, 1);

            return SqlCreateTable(sb.ToString());
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string InsertRow(SignerReviewerInfo item)
        {
            string error = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("'1',");
            for (int i = 0; i < item.MListSigner.Count; i++)
            {
                sb.Append("'" + item.MListSigner[i] + "',");
            }
            sb.Remove(sb.Length - 1, 1);
            error += SqlInsertRow(sb.ToString());

            sb = new StringBuilder();
            sb.Append("'2',");
            for (int i = 0; i < item.MListReviewer.Count; i++)
            {
                sb.Append("'" + item.MListReviewer[i] + "',");
            }
            sb.Remove(sb.Length - 1, 1);
            error += SqlInsertRow(sb.ToString());

            if (string.IsNullOrEmpty(error))
            {
                error = null;
            }

            return error;
        }

        /// <summary>
        /// 修改行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string UpdateRow(SignerReviewerInfo item)
        {
            string error = null;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < item.MListSigner.Count; i++)
            {
                sb.Append(((EnumSignerReviewer)i).ToString() + "='" + item.MListSigner[i] + "',");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(" WHERE ID LIKE '1'");
            error += SqlUpdateRow(sb.ToString());

            sb = new StringBuilder();
            for (int i = 0; i < item.MListReviewer.Count; i++)
            {
                sb.Append(((EnumSignerReviewer)i).ToString() + "='" + item.MListReviewer[i] + "',");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(" WHERE ID LIKE '2'");
            error += SqlUpdateRow(sb.ToString());

            if (string.IsNullOrEmpty(error))
            {
                error = null;
            }

            return error;
        }

        /// <summary>
        /// 获取行信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string SelectRow(out SignerReviewerInfo item)
        {
            string error = null;
            item = null;

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT * FROM " + m_tableName, out reader);
                if (null == error)
                {
                    item = new SignerReviewerInfo();
                    if (reader.Read())//匹配
                    {
                        int index = 0;
                        index++;//ID
                        for (int i = 0; i < item.MListSigner.Count; i++)
                        {
                            item.MListSigner[i] = reader.GetBoolean(index++);
                        }
                    }
                    else
                    {
                        error = Share.ReadXaml.S_ErrorNoData;
                    }
                    if (reader.Read())//匹配
                    {
                        int index = 0;
                        index++;//ID
                        for (int i = 0; i < item.MListReviewer.Count; i++)
                        {
                            item.MListReviewer[i] = reader.GetBoolean(index++);
                        }
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
