using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Database
{
    /**
     * ClassName: BaseExImTable
     * Description: 数据表操作基类
     * Version: 1.0
     * Create:  2021/05/12
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    public abstract class BaseExImTable
    {
        protected string m_dataSource;
        protected string m_tableName;           //表的名称
        private SQLiteConnection m_conn;
        private SQLiteDataReader m_reader;


        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseExImTable(string path, string tableName)
        {
            m_dataSource = "Data Source=" + path + ";";
            m_tableName = tableName;
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns></returns>
        public abstract string CreateTable();

        /// <summary>
        /// 关闭资源
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="reader"></param>
        protected void CloseConnAndReader()
        {
            if (null != m_reader)
            {
                m_reader.Close();
            }
            if (null != m_conn)
            {
                m_conn.Close();
            }
        }

        /// <summary>
        /// 创建资源
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        protected string CreateConnAndReader(string commandText, out SQLiteDataReader reader)
        {
            string result = null;

            try
            {
                //建立数据库连接并打开
                m_conn = new SQLiteConnection(m_dataSource);
                m_conn.Open();

                //执行SQL语句进行查询
                SQLiteCommand cmd = new SQLiteCommand(commandText, m_conn);

                //读取查询到的数据
                m_reader = cmd.ExecuteReader();
            }
            catch(Exception msg)
            {
                result = msg.Message;
                CloseConnAndReader();
            }

            reader = m_reader;
            return result;
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        protected string SqlCreateTable(string commandText)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("CREATE TABLE ");
            sb.Append(m_tableName);
            sb.Append(" (");
            sb.Append(commandText);
            sb.Append(");");

            return SqlBaseCDIU(sb.ToString());
        }

        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        protected string SqlDeleteRow(string commandText)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM ");
            sb.Append(m_tableName);
            sb.Append(" WHERE ");
            sb.Append(commandText);
            sb.Append(";");

            return SqlBaseCDIU(sb.ToString());
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        protected string SqlInsertRow(string commandText)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO ");
            sb.Append(m_tableName);
            sb.Append(" VALUES(");
            sb.Append(commandText);
            sb.Append(");");

            return SqlBaseCDIU(sb.ToString());
        }

        /// <summary>
        /// 更新行
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        protected string SqlUpdateRow(string commandText)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE ");
            sb.Append(m_tableName);
            sb.Append(" SET ");
            sb.Append(commandText);
            sb.Append(";");

            return SqlBaseCDIU(sb.ToString());
        }

        /// <summary>
        /// 设置多个字段组合为唯一字段
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        protected string SqlUniqueTable(string name, string commandText)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("ALTER TABLE ");
            sb.Append(m_tableName);
            sb.Append(" ");
            sb.Append(name);
            sb.Append(" UNIQUE(");
            sb.Append(commandText);
            sb.Append(");");

            return SqlBaseCDIU(sb.ToString());
        }

        /// <summary>
        /// 创建表、删除行、插入行、更新行的通用
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        private string SqlBaseCDIU(string commandText)
        {
            string result = null;

            SQLiteConnection conn = new SQLiteConnection(m_dataSource);

            try
            {
                conn.Open();

                SQLiteCommand cmd = new SQLiteCommand(commandText, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception msg)
            {
                result = msg.Message;
            }
            finally
            {
                if (null != conn)
                {
                    conn.Close();
                }
            }

            return result;
        }

        /// <summary>
        /// 创建表、删除行、插入行、更新行的通用
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="colName"></param>
        /// <param name="arr"></param>
        /// <returns></returns>
        protected string SqlBaseCDIU(string commandText, string colName, byte[] arr)
        {
            string result = null;

            SQLiteConnection conn = new SQLiteConnection(m_dataSource);

            try
            {
                conn.Open();

                SQLiteCommand cmd = new SQLiteCommand(commandText, conn);
                SQLiteParameter param = cmd.Parameters.Add(colName, System.Data.DbType.Binary);
                param.Value = arr;
                cmd.ExecuteNonQuery();
            }
            catch (Exception msg)
            {
                result = msg.Message;
            }
            finally
            {
                if (null != conn)
                {
                    conn.Close();
                }
            }

            return result;
        }
    }
}
