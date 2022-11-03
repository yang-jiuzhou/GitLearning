using HBBio.Database;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.WindowSize
{
    /**
     * ClassName: WindowSizeTable
     * Description: 窗体宽高表
     * Version: 1.0
     * Create:  2022/02/21
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class WindowSizeTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public WindowSizeTable()
        {
            m_tableName = "WindowSizeTable";
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
            return SqlCreateTable(@"[ID] [int] PRIMARY KEY IDENTITY(1,1),
	            [WindowName] [nvarchar](64) UNIQUE,
                [WindowX] [float],
                [WindowY] [float],
                [WindowHeight] [float],
                [WindowWidth] [float]");
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
                    listName.Add("ID");
                    listName.Add("WindowName");
                    listName.Add("WindowX");
                    listName.Add("WindowY");
                    listName.Add("WindowHeight");
                    listName.Add("WindowWidth");

                    error = CreateNewTable(listName, listType, true);
                }
            }

            return error;
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string InsertRow(string name, double x, double y, double height, double width)
        {
            return SqlInsertRow("'" + name + "','" + x + "','" + y + "','" + height + "','" + width + "'");
        }

        /// <summary>
        /// 获取行
        /// </summary>
        /// <returns></returns>
        public string SelectRow(string name, out double x, out double y, out double height, out double width)
        {
            string error = null;
            x = 0;
            y = 0;
            height = 0;
            width = 0;

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT * FROM " + m_tableName + @" WHERE WindowName='" + name + "'", out reader);

                if (null == error)
                {
                    if (reader.Read())
                    {
                        x = reader.GetDouble(2);
                        y = reader.GetDouble(3);
                        height = reader.GetDouble(4);
                        width = reader.GetDouble(5);
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
        /// 更新行
        /// </summary>
        /// <returns></returns>
        public string UpdateRow(string name, double x, double y, double height, double width)
        {
            return SqlUpdateRow("WindowX='" + x + "',WindowY='" + y + "',WindowHeight='" + height + "',WindowWidth='" + width + "' WHERE WindowName='" + name + "'");
        }
    }
}
