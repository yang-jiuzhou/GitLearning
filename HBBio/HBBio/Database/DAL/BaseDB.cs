using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Database
{
    /**
     * ClassName: BaseDB
     * Description: 创建数据库
     * Version: 1.0
     * Create:  2020/04/27
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    class BaseDB
    {
        private const string C_ID = "222";
        private const string ConfDBName = "HB_LabChrom_ConfDB" + C_ID;
        private const string ComDBName = "HB_LabChrom_ComDB" + C_ID;
        private const string LogDBName = "HB_LabChrom_LogDB" + C_ID;
        private const string CurveDBName = "HB_LabChrom_CurveDB" + C_ID;
        public readonly static string ConfDB = @"server=" + Environment.MachineName + ";database=" + ConfDBName + ";uid=sa;pwd=123;Connection Timeout=5;";
        public readonly static string ComDB = @"server=" + Environment.MachineName + ";database=" + ComDBName + ";uid=sa;pwd=123;Connection Timeout=5;";
        public readonly static string LogDB = @"server=" + Environment.MachineName + ";database=" + LogDBName + ";uid=sa;pwd=123;Connection Timeout=5;";
        public readonly static string CurveDB = @"server=" + Environment.MachineName + ";database=" + CurveDBName + ";uid=sa;pwd=123;Connection Timeout=5;";
        public readonly static string DB2 = @"server=" + Environment.MachineName + ";database=master;uid=sa;pwd=123;Connection Timeout=5;";
        private const string m_path = @"D:\HBSQLDATA222\";             //数据存放路径
        public const string m_pathError = @"D:\HBErrorDATA222\";       //错误日志存放路径


        /// <summary>
        /// 数据库名称数组
        /// </summary>
        private string[] DBNameArr
        {
            get
            {
                return new string[4] { ConfDBName, ComDBName, LogDBName, CurveDBName };
            }
        }

        /// <summary>
        /// 数据库链接数组
        /// </summary>
        private string[] DBConnArr
        {
            get
            {
                return new string[4] { ConfDB, ComDB, LogDB, CurveDB };
            }
        }


        /// <summary>
        /// 检查数据库文件夹是否存在
        /// </summary>
        /// <returns></returns>
        public bool ExistFileFolder()
        {
            return Directory.Exists(m_path);
        }

        /// <summary>
        /// 检查数据库文件是否存在
        /// </summary>
        /// <returns></returns>
        public bool ExistDBFile()
        {
            foreach (string it in DBNameArr)
            {
                if (!File.Exists(m_path + "\\" + it + ".mdf"))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 创建数据库以及表
        /// </summary>
        public void CreateAll()
        {
            CreateDirectory(m_path);
            CreateDirectory(m_pathError);

            Cmdshell();

            foreach (string it in DBNameArr)
            {
                Create(it);
            }
        }

        /// <summary>
        /// 判断日志大小
        /// </summary>
        public bool JudgeSizeAll(double maxSize = 500)
        {
            SqlConnection conn = new SqlConnection(DB2);

            try
            {
                //建立数据库连接并打开
                conn.Open();

                //用户名密码匹配，读取查询到的数据
                string sqlCommandString = "dbcc sqlperf(logspace)";
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommandString, conn);

                //填入dataset
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                DataTable dt = ds.Tables[0].DefaultView.Table;
                if (null != dt)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToDouble(dt.Rows[i][1].ToString()) > maxSize)
                        {
                            ResetSize(dt.Rows[i][0].ToString());
                        }
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (null != conn)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 修复数据库
        /// </summary>
        public void RepairAll()
        {
            for (int i = 0; i < DBConnArr.Length; i++)
            {
                if (!IsEnabled(DBConnArr[i]))
                {
                    Repair(DBNameArr[i]);
                }
            }
        }

        /// <summary>
        /// 设置权限
        /// </summary>
        /// <returns></returns>
        private bool Cmdshell()
        {
            SqlConnection conn = new SqlConnection(DB2);

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = @"exec sp_configure 'show advanced options',1 
                    reconfigure
                    exec sp_configure 'xp_cmdshell',1 
                    reconfigure ";
                cmd.ExecuteNonQuery();

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (null != conn)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path"></param>
        private void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="dbPath"></param>
        /// <returns></returns>
        private bool Create(string dbName)
        {
            bool result = false;

            SqlConnection conn = new SqlConnection(DB2);
            try
            {
                conn.Open();

                string sql = "if not exists(select * from master.dbo.sysdatabases where name='" + dbName + "')"
                    + "create database " + dbName + " on primary"
                    + "(name=" + dbName + ",filename='" + m_path + dbName + ".mdf',size=5mb,filegrowth=1mb)"
                    + "log on(name=" + dbName + "_log,filename='" + m_path + dbName + "_log.ldf',size=5mb,filegrowth=10%) COLLATE Chinese_PRC_CS_AS"
                    + " alter database " + dbName + " set recovery simple";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.ExecuteNonQuery();
                result = true;
            }
            catch
            {}
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
        /// 数据库是否可访问
        /// </summary>
        /// <returns></returns>
        private bool IsEnabled(string dbPath)
        {
            SqlConnection conn = new SqlConnection(dbPath);

            try
            {
                conn.Open();

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (null != conn)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 修复数据库
        /// </summary>
        private bool Repair(string dbName)
        {
            SqlConnection conn = new SqlConnection(DB2);
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"alter database " + dbName + " set emergency with rollback immediate;" +
                        "alter database " + dbName + " set single_user with rollback immediate; " +
                        "dbcc CheckDB(" + dbName + ", REPAIR_ALLOW_DATA_LOSS); " +
                        "alter database " + dbName + " set multi_user with rollback immediate;";
                cmd.ExecuteNonQuery();

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (null != conn)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 分离附加日志
        /// </summary>
        /// <param name="dbName"></param>
        private bool ResetSize(string dbName)
        {
            SqlConnection conn = new SqlConnection(DB2);

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "SELECT spid FROM sysprocesses WHERE dbid=DB_ID('" + dbName + "')";
                SqlDataReader reader = cmd.ExecuteReader();
                List<int> tempIDlIST = new List<int>();
                while (reader.Read())
                {
                    tempIDlIST.Add(reader.GetInt16(0));
                }
                reader.Close();

                for (int i = 0; i < tempIDlIST.Count; i++)
                {
                    cmd.CommandText = @"KILL " + tempIDlIST[i];
                    cmd.ExecuteNonQuery();
                }

                cmd.CommandText = @"exec sp_detach_db @dbname='" + dbName + "';";
                cmd.ExecuteNonQuery();

                File.Delete(m_path + dbName + "_log.ldf");

                cmd.CommandText = @"exec sp_attach_single_file_db @dbname='" + dbName +
                    "',@physname='" + m_path + dbName + ".mdf'";
                cmd.ExecuteNonQuery();

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (null != conn)
                {
                    conn.Close();
                }
            }
        }


        /// <summary>
        /// 全部备份到本地
        /// </summary>
        /// <param name="notime"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool BackupLocal(string notime, string path)
        {
            bool result = false;

            SqlConnection conn = new SqlConnection(DB2);
            try
            {
                conn.Open();

                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandTimeout = 0;

                for (int i = 0; i < DBNameArr.Length; i++)
                {
                    comm.CommandText = @"BACKUP DATABASE " + DBNameArr[i] + " TO DISK='" + path + @"\" + notime + "_" + (i + 1).ToString() + ".bak'";
                    comm.ExecuteNonQuery();
                }

                result = true;
            }
            catch
            { }
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
        /// 全部备份到远程
        /// </summary>
        /// <param name="notime"></param>
        /// <param name="ip"></param>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool BackupServer(string notime, string ip, string name, string pwd, string path)
        {
            bool result = false;

            SqlConnection conn = new SqlConnection(DB2);
            try
            {
                conn.Open();

                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandTimeout = 0;

                for (int i = 0; i < DBNameArr.Length; i++)
                {
                    comm.CommandText = "exec master..xp_cmdshell 'net use Y: \\\\" + ip + "\\" + path.Replace(":", "") + " " + pwd + " /user:" + ip + "\\" + name + "';"
                        + "backup database " + DBNameArr[i] + " to disk = 'Y:" + notime + "_" + (i + 1).ToString() + ".bak';"
                        + "exec master..xp_cmdshell 'net use Y: /delete'";
                    comm.ExecuteNonQuery();
                }

                result = true;
            }
            catch
            { }
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
        /// 创建远程备份路径
        /// </summary>
        /// <param name="notime"></param>
        /// <param name="ip"></param>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool CreateBackupDirectory(string notime, string ip, string name, string pwd, string path)
        {
            bool result = false;

            SqlConnection conn = new SqlConnection(DB2);
            try
            {
                conn.Open();

                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandTimeout = 0;

                comm.CommandText = "exec master..xp_cmdshell 'net use Y: \\\\" + ip + "\\" + path.Replace(":", "") + " " + pwd + " /user:" + ip + "\\" + name + "';"
                    + "exec xp_cmdshell 'mkdir Y:" + notime + "Auto';"
                    + "exec master..xp_cmdshell 'net use Y: /delete'";
                comm.ExecuteNonQuery();

                result = true;
            }
            catch
            { }
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
        /// 删除远程备份路径
        /// </summary>
        /// <param name="notime"></param>
        /// <param name="ip"></param>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool RemoveBackupDirectory(string notime, string ip, string name, string pwd, string path)
        {
            bool result = false;

            SqlConnection conn = new SqlConnection(DB2);
            try
            {
                conn.Open();

                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandTimeout = 0;

                comm.CommandText = "exec master..xp_cmdshell 'net use Y: \\\\" + ip + "\\" + path.Replace(":", "") + " " + pwd + " /user:" + ip + "\\" + name + "';"
                    + "exec xp_cmdshell 'rd Y:" + notime + "Auto /S/Q';"
                    + "exec master..xp_cmdshell 'net use Y: /delete'";
                comm.ExecuteNonQuery();

                result = true;
            }
            catch
            { }
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
        /// 从本地还原全部
        /// </summary>
        public bool RestoreLocal(string path, string filename)
        {
            bool result = true;

            try
            {
                for (int i = 0; i < DBNameArr.Length; i++)
                {
                    result &= RestoreLocal(DBNameArr[i], path, filename + "_" + (i + 1).ToString() + ".bak");
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 从本地还原指定
        /// </summary>
        private bool RestoreLocal(string dbname, string path, string filename)
        {
            string sqlSelesct = "SELECT spid FROM sysprocesses ,sysdatabases WHERE sysprocesses.dbid=sysdatabases.dbid AND sysdatabases.Name='" + dbname + "'";
            string sqlRestore = String.Format("RESTORE DATABASE {0} FROM DISK = '{1}' with replace", dbname, path + "\\" + filename);

            if (RestoreSql(sqlSelesct, sqlRestore))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 从本地还原全部
        /// </summary>
        public bool RestoreServer(string ip, string name, string pwd, string path, string filename)
        {
            bool result = true;

            try
            {
                for (int i = 0; i < DBNameArr.Length; i++)
                {
                    result &= RestoreServer(DBNameArr[i], ip, name, pwd, path, filename + "_" + (i + 1).ToString() + ".bak");
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 从本地还原指定
        /// </summary>
        private bool RestoreServer(string dbname, string ip, string name, string pwd, string path, string filename)
        {
            string sqlSelesct = "SELECT spid FROM sysprocesses ,sysdatabases WHERE sysprocesses.dbid=sysdatabases.dbid AND sysdatabases.Name='" + dbname + "'";
            string sqlRestore = "exec master..xp_cmdshell 'net use Y: \\\\" + ip + "\\" + path.Replace(":", "") + " " + pwd + " /user:" + ip + "\\" + name + "';"
                + "restore database " + dbname + " from disk = 'Y:\\\\" + filename + "' with replace;"
                + "exec master..xp_cmdshell 'net use Y: /delete'";

            if (RestoreSql(sqlSelesct, sqlRestore))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 从还原指定
        /// </summary>
        private bool RestoreSql(string sqlSelesct, string sqlRestore)
        {
            SqlConnection conn = new SqlConnection(DB2);

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlSelesct, conn);

                ArrayList list = new ArrayList();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(dr.GetInt16(0));
                }
                dr.Close();
                conn.Close();

                for (int i = 0; i < list.Count; i++)
                {
                    conn.Open();
                    cmd = new SqlCommand(string.Format("KILL {0}", list[i].ToString()), conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                conn.Open();
                cmd = new SqlCommand(sqlRestore, conn);
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (null != conn)
                {
                    conn.Close();
                }
            }
        }
    }
}
