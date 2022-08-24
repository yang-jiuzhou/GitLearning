using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Database
{
    /**
     * ClassName: BaseTable
     * Description: 数据表操作基类
     * Version: 1.0
     * Create:  2020/04/27
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    public abstract class BaseTable
    {
        protected string m_connStr;
        protected string m_tableName;           //表的名称
        private SqlConnection m_conn;
        private SqlDataReader m_reader;
        private SqlDataAdapter m_adapter;


        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseTable()
        {
            m_connStr = BaseDB.ConfDB;
        }

        /// <summary>
        /// 插入默认值
        /// </summary>
        /// <returns></returns>
        protected abstract string AddDefaultValue();

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns></returns>
        protected abstract string CreateTable();

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
        protected string CreateConnAndReader(string commandText, out SqlDataReader reader)
        {
            string result = null;

            try
            {
                //建立数据库连接并打开
                m_conn = new SqlConnection(m_connStr);
                m_conn.Open();

                //执行SQL语句进行查询
                SqlCommand cmd = new SqlCommand(commandText, m_conn);
                cmd.CommandType = CommandType.Text;

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
        /// 关闭资源
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="reader"></param>
        protected void CloseConnAndAdapter()
        {
            if (null != m_adapter)
            {
                m_adapter = null;
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
        protected string CreateConnAndAdapter(out DataTable logdt, string commandText)
        {
            string result = null;

            try
            {
                //建立数据库连接并打开
                m_conn = new SqlConnection(m_connStr);
                m_conn.Open();

                //执行SQL语句进行查询
                m_adapter = new SqlDataAdapter(commandText, m_conn);

                DataSet ds = new DataSet();
                m_adapter.Fill(ds);

                logdt = ds.Tables[0].DefaultView.Table;
            }
            catch (Exception msg)
            {
                result = msg.Message;
                logdt = null;
                CloseConnAndAdapter();
            }

            return result;
        }

        /// <summary>
        /// 表是否存在
        /// </summary>
        /// <returns></returns>
        protected string ExistTable(ref bool exist)
        {
            string error = null;

            SqlConnection conn = new SqlConnection(m_connStr);
            SqlDataReader reader = null;
            SqlCommand cmd = new SqlCommand();

            try
            {
                conn.Open();

                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from sys.tables where name ='" + m_tableName + "'";
                reader = cmd.ExecuteReader();
                exist = reader.HasRows;
            }
            catch (Exception msg)
            {
                error = msg.Message;
            }
            finally
            {
                if (null != reader)
                {
                    cmd.Cancel();
                    reader.Close();
                }
                if (null != conn)
                {
                    conn.Close();
                }
            }

            return error;
        }

        /// <summary>
        /// 获取最新一条ID
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetLastID(out int id)
        {
            string error = null;
            id = -1;

            try
            {
                SqlDataReader reader = null;
                CreateConnAndReader(@"SELECT ID FROM " + m_tableName + @" ORDER BY ID DESC", out reader);
                if (null != reader)
                {
                    if (reader.Read())//匹配
                    {
                        id = reader.GetInt32(0);
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
        /// 初始化表
        /// </summary>
        public string InitTable()
        {
            string error = null;

            bool exist = false;
            error += ExistTable(ref exist);
            if (string.IsNullOrEmpty(error) && !exist)
            {
                error += CreateTable();
                error += AddDefaultValue();
            }

            if (string.IsNullOrEmpty(error))
            {
                error = null;
            }

            return error;
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
        /// 更新整个表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public string SqlUpdateTable(List<string> commandTextList)
        {
            string result = null;

            SqlConnection conn = new SqlConnection(m_connStr);
            SqlCommand cmd = new SqlCommand();
            SqlTransaction transaction = null;

            try
            {
                conn.Open();

                transaction = conn.BeginTransaction();

                cmd.Connection = conn;
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"TRUNCATE TABLE " + m_tableName;
                cmd.ExecuteNonQuery();

                for (int i = 0; i < commandTextList.Count; i++)
                {
                    cmd.CommandText = @"INSERT INTO " + m_tableName + @" VALUES(" + commandTextList[i] + ")";
                    cmd.ExecuteNonQuery();
                }

                //完成提交
                transaction.Commit();
            }
            catch (Exception msg)
            {
                result = msg.Message;

                //数据回滚
                if (null != transaction)
                {
                    transaction.Rollback();
                }
            }
            finally
            {
                if (null != cmd)
                {
                    cmd.Cancel();
                }
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
        /// <returns></returns>
        private string SqlBaseCDIU(string commandText)
        {
            string result = null;

            SqlConnection conn = new SqlConnection(m_connStr);

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(commandText, conn);
                cmd.CommandType = CommandType.Text;
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

            SqlConnection conn = new SqlConnection(m_connStr);

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(commandText, conn);
                SqlParameter param = cmd.Parameters.Add(colName, SqlDbType.VarBinary);
                param.Value = null == arr ? System.Data.SqlTypes.SqlBinary.Null : arr;
                cmd.CommandType = CommandType.Text;
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
        /// 清空表所有行
        /// </summary>
        /// <returns></returns>
        protected string TruncateTable()
        {
            string result = null;

            SqlConnection conn = new SqlConnection(m_connStr);

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("truncate table " + m_tableName + "", conn);
                cmd.CommandType = CommandType.Text;
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
