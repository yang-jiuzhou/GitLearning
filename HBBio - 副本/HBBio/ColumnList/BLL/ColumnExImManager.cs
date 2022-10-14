using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.ColumnList
{
    class ColumnExImManager
    {
        public string Export(string path, List<ColumnItem> list)
        {
            string error = null;
            System.Data.SQLite.SQLiteConnection.CreateFile(path);
            ColumnListExImTable table = new ColumnListExImTable(path, "ColumnTable");
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

        public string Import(string path, List<ColumnItem> list)
        {
            ColumnListExImTable table = new ColumnListExImTable(path, "ColumnTable");
            return table.GetList(list);
        }
    }
}
