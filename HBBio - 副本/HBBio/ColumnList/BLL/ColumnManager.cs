using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.ColumnList
{
    /**
     * ClassName: ColumnManager
     * Description: 色谱柱管理类
     * Version: 1.0
     * Create:  2018/05/16
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class ColumnManager
    {
        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string InsertColumn(ColumnItem item)
        {
            ColumnListTable table = new ColumnListTable();
            return table.InsertRow(item);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string DelColumn(string name)
        {
            ColumnListTable table = new ColumnListTable();
            return table.DeleteRow(name);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetColumn(string name, ColumnItem item)
        {
            ColumnListTable table = new ColumnListTable();
            return table.GetRow(name, item);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetColumn(int id, ColumnItem item)
        {
            ColumnListTable table = new ColumnListTable();
            return table.GetRow(id, item);
        }

        /// <summary>
        /// 获取名称列表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public string GetNameList(out List<string> list, string filter = null)
        {
            ColumnListTable table = new ColumnListTable();
            return table.GetNameList(out list, filter);
        }

        /// <summary>
        /// 获取ID,名称列表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public string GetNameList(out List<ColumnItem> list, string filter = null)
        {
            ColumnListTable table = new ColumnListTable();
            return table.GetNameList(out list, filter);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string UpdateColumn(ColumnItem item)
        {
            ColumnListTable table = new ColumnListTable();
            return table.UpdateRow(item);
        }
    }
}
