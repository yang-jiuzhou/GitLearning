using HBBio.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.PassDog
{
    /**
     * ClassName: PassDogTable
     * Description: 加密狗表
     * Version: 1.0
     * Create:  2021/09/13
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class PassDogTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PassDogTable()
        {
            m_tableName = "PassDogTable";
        }

        /// <summary>
        /// 插入默认值
        /// </summary>
        /// <returns></returns>
        protected override string AddDefaultValue()
        {
            return InsertRow(new PassDogValue());
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns></returns>
        protected override string CreateTable()
        {
            return SqlCreateTable(
                @"[DogHB] [nvarchar](64) NOT NULL,
                    [DogName] [nvarchar](64) NOT NULL,
                    [DogMode] [nvarchar](64) NOT NULL,
                    [DogSN] [nvarchar](64) NOT NULL");
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
        /// 获取行信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetRow(ref PassDogValue dogSN)
        {
            string error = null;
            if (null == dogSN)
            {
                dogSN = new PassDogValue();
            }

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT * FROM " + m_tableName + @" WHERE 1=1", out reader);
                if (null == error)
                {
                    if (reader.Read())//匹配
                    {
                        dogSN.MHB = reader.GetString(0);
                        dogSN.MName = reader.GetString(1);
                        dogSN.MMode = reader.GetString(2);
                        dogSN.MSN = reader.GetString(3);
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
        public string InsertRow(PassDogValue dogSN)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + dogSN.MHB);
            sb.Append("','" + dogSN.MName);
            sb.Append("','" + dogSN.MMode);
            sb.Append("','" + dogSN.MSN + "'");
            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 修改行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string UpdateRow(PassDogValue dogSN)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DogHB='" + dogSN.MHB);
            sb.Append("',DogName='" + dogSN.MName);
            sb.Append("',DogMode='" + dogSN.MMode);
            sb.Append("',DogSN='" + dogSN.MSN + "'");
            return SqlUpdateRow(sb.ToString());
        }
    }
}
