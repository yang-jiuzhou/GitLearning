using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.TubeStand
{
    class TubeStandManager
    {
        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string InsertItem(TubeStandItem item)
        {
            TubeStandTable table = new TubeStandTable();
            return table.InsertRow(item);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string DelItem(TubeStandItem item)
        {
            return DelItem(item.MVolume, item.MCount);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string DelItem(double volume, int count)
        {
            TubeStandTable table = new TubeStandTable();
            return table.DeleteRow(volume, count);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetItem(double volume, int count, TubeStandItem item)
        {
            TubeStandTable table = new TubeStandTable();
            return table.GetRow(volume, count, ref item);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public string GetList(out List<TubeStandItem> list)
        {
            TubeStandTable table = new TubeStandTable();
            return table.GetList(out list);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string UpdateItem(TubeStandItem item)
        {
            TubeStandTable table = new TubeStandTable();
            return table.UpdateRow(item);
        }
    }
}
