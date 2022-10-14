using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    class MethodExImManager
    {
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="path"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public string Export(string path, List<Method> list)
        {
            string error = null;
            System.Data.SQLite.SQLiteConnection.CreateFile(path);
            MethodExImTable table = new MethodExImTable(path, "MethodTable");
            table.CreateTable();
            foreach (var it in list)
            {
                error += table.InsertRow(it);
            }
            if (string.IsNullOrEmpty(error))
            {
                error = null;
            }
            return error;
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="path"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public string Import(string path, List<Method> list)
        {
            MethodExImTable table = new MethodExImTable(path, "MethodTable");
            return table.GetList(list);
        }
    }
}
