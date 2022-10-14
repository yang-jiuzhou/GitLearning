using HBBio.Database;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Administration
{
    /**
     * ClassName: UserTable
     * Description: 用户表
     * Version: 1.0
     * Create:  2020/09/07
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class UserTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UserTable()
        {
            m_tableName = "UserTable";
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
            return SqlCreateTable(
                @"[ID] [int] PRIMARY KEY IDENTITY(1, 1),
                [DeleteID] [int] NOT NULL,
                [UserName] [nvarchar](64) NOT NULL,
                [PermissionNameID] [int] FOREIGN KEY REFERENCES PermissionTable(ID),
                [Note] [nvarchar](max) NOT NULL,
                [Pwd] [varchar](max) NOT NULL,
                [PwdSign] [varchar](max) NOT NULL,
                [PwdTime] [datetime] NOT NULL,
                [PwdDay] [int] NOT NULL,
                [Enabled] [bit] NOT NULL,
                [ErrorNum] [int] NOT NULL,
                [ProjectID] [int] NOT NULL,
                CONSTRAINT DeleteID_UserName UNIQUE (DeleteID,UserName)"
                );
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string InsertRow(UserInfo item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + item.MDeleteID);
            sb.Append("','" + item.MUserName);
            sb.Append("','" + item.MPermissionNameID);
            sb.Append("','" + item.MNote);
            sb.Append("','" + Descsp.Encrypt(item.MPwd));
            sb.Append("','" + Descsp.Encrypt(item.MPwdSign));
            sb.Append("','" + item.MPwdTime);
            sb.Append("','" + item.MPwdDay);
            sb.Append("','" + item.MEnabled);
            sb.Append("','" + item.MErrorNum);
            sb.Append("','" + item.MProjectID + "'");

            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string DeleteRow(int ID)
        {
            return SqlUpdateRow(@"DeleteID='" + ID + "' WHERE ID='" + ID + "'");
        }

        /// <summary>
        /// 修改行(记录)
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public string UpdateRowNote(int ID, string note)
        {
            return SqlUpdateRow(@"Note='" + note + "' WHERE ID='" + ID + "'");
        }

        /// <summary>
        /// 修改行(权限)
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        public string UpdateRowPermission(int ID, int permissionNameID)
        {
            return SqlUpdateRow(@"PermissionNameID='" + permissionNameID + "' WHERE ID='" + ID + "'");
        }

        /// <summary>
        /// 修改行(登录密码)
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <param name="pwdOld"></param>
        /// <returns></returns>
        public string UpdateRowPwd(int ID, string pwd)
        {
            return SqlUpdateRow(@"Pwd='" + Descsp.Encrypt(pwd) + "',PwdTime='" + DateTime.Now.ToString() + "' WHERE ID='" + ID + "'");
        }

        /// <summary>
        /// 修改行(登录密码重置)
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public string UpdateRowPwdReset(int ID, string pwd)
        {
            return SqlUpdateRow(@"Pwd='" + Descsp.Encrypt(pwd) + "',PwdTime='1970/01/02 00:00:00' WHERE ID='" + ID + "'");
        }

        /// <summary>
        /// 修改行(签名密码)
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwdSign"></param>
        /// <returns></returns>
        public string UpdateRowPwdSign(int ID, string pwdSign)
        {
            return SqlUpdateRow(@"PwdSign='" + Descsp.Encrypt(pwdSign) + "' WHERE ID='" + ID + "'");
        }

        /// <summary>
        /// 修改行(登录密码重置)
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public string UpdateRowPwdSignReset(int ID, string pwdSign)
        {
            return SqlUpdateRow(@"PwdSign='" + Descsp.Encrypt(pwdSign) + "' WHERE ID='" + ID + "'");
        }

        /// <summary>
        /// 修改行(状态)
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public string UpdateRowEnabled(int ID, bool enabled)
        {
            return SqlUpdateRow(@"Enabled='" + enabled + "' WHERE ID='" + ID + "'");
        }

        /// <summary>
        /// 修改行(错误次数)
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="errorNum"></param>
        /// <returns></returns>
        public string UpdateRowErrorNum(int ID, int errorNum)
        {
            return SqlUpdateRow(@"ErrorNum='" + errorNum + "' WHERE ID='" + ID + "'");
        }

        /// <summary>
        /// 修改项目树ID
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="errorNum"></param>
        /// <returns></returns>
        public string UpdateRowProjectID(int ID, int projectID)
        {
            return SqlUpdateRow(@"ProjectID='" + projectID + "' WHERE ID='" + ID + "'");
        }

        /// <summary>
        /// 获取行信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string SelectRow(int ID, out UserInfo item)
        {
            string error = null;
            item = null;

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT * FROM " + m_tableName + @" WHERE ID='" + ID + "'", out reader);
                if (null == error)
                {
                    if (reader.Read())//匹配
                    {
                        int index = 0;
                        item = new UserInfo(reader.GetInt32(index++),
                            reader.GetInt32(index++),
                            reader.GetString(index++),
                            reader.GetInt32(index++),
                            reader.GetString(index++),
                            Descsp.Decrypt(reader.GetString(index++)),
                            Descsp.Decrypt(reader.GetString(index++)),
                            reader.GetDateTime(index++),
                            reader.GetInt32(index++),
                            reader.GetBoolean(index++),
                            reader.GetInt32(index++),
                            reader.GetInt32(index++));
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
        /// 获取行信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string SelectRowID(string userName, out int ID)
        {
            string error = null;
            ID = -1;

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT ID FROM " + m_tableName + @" WHERE UserName='" + userName + "' AND DeleteID='0'", out reader);
                if (null == error)
                {
                    if (reader.Read())//匹配
                    {
                        ID = reader.GetInt32(0);
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
        /// 获取拥有指定权限的所有用户列表
        /// </summary>
        /// <param name="PermissionNameID"></param>
        /// <param name="listUserName"></param>
        /// <returns></returns>
        public string SelectRowPermissionNameID(int permissionNameID, out List<string> listUserName)
        {
            string error = null;
            listUserName = new List<string>();

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT UserName FROM " + m_tableName + @" WHERE PermissionNameID='" + permissionNameID + "' AND DeleteID='0' ORDER BY ID", out reader);

                if (null == error)
                {
                    while (reader.Read())
                    {
                        listUserName.Add(reader.GetString(0));
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
        public string SelectList(out List<UserInfo> list)
        {
            string error = null;
            list = new List<UserInfo>();

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT * FROM " + m_tableName + @" WHERE DeleteID='0' ORDER BY ID", out reader);

                if (null == error)
                {
                    while (reader.Read())
                    {
                        int index = 0;
                        list.Add(new UserInfo(reader.GetInt32(index++),
                            reader.GetInt32(index++),
                            reader.GetString(index++),
                            reader.GetInt32(index++),
                            reader.GetString(index++),
                            Descsp.Decrypt(reader.GetString(index++)),
                            Descsp.Decrypt(reader.GetString(index++)),
                            reader.GetDateTime(index++),
                            reader.GetInt32(index++),
                            reader.GetBoolean(index++),
                            reader.GetInt32(index++),
                            reader.GetInt32(index++)));
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
        public string SelectListSignerReviewer(List<int> listPermissionID, out List<UserInfo> list)
        {
            string error = null;
            list = new List<UserInfo>();

            try
            {
                SqlDataReader reader = null;
                StringBuilder sb = new StringBuilder();
                sb.Append("PermissionNameID='" + listPermissionID[0] + "'");
                for (int i = 1; i < listPermissionID.Count; i++)
                {
                    sb.Append(" OR PermissionNameID='" + listPermissionID[i] + "'");
                }
                error = CreateConnAndReader(@"SELECT * FROM " + m_tableName + @" WHERE (" + sb.ToString() + ") AND DeleteID='0' ORDER BY ID", out reader);

                if (null == error)
                {
                    while (reader.Read())
                    {
                        int index = 0;
                        list.Add(new UserInfo(reader.GetInt32(index++),
                            reader.GetInt32(index++),
                            reader.GetString(index++),
                            reader.GetInt32(index++),
                            reader.GetString(index++),
                            Descsp.Decrypt(reader.GetString(index++)),
                            Descsp.Decrypt(reader.GetString(index++)),
                            reader.GetDateTime(index++),
                            reader.GetInt32(index++),
                            reader.GetBoolean(index++),
                            reader.GetInt32(index++),
                            reader.GetInt32(index++)));
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
