using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Manual
{
    /**
     * ClassName: ManualManager
     * Description: 手动临时状态信息存储
     * Version: 1.0
     * Create:  2020/08/31
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class ManualManager
    {
        /// <summary>
        /// 添加临时数据
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string AddManualTemp(ManualValue info)
        {
            ManualTempTable table = new ManualTempTable();
            return table.AddRowTemp(info);
        }
        public string AddManualColl(string info)
        {
            ManualTempTable table = new ManualTempTable();
            return table.AddRowColl(info);
        }

        /// <summary>
        /// 删除临时数据
        /// </summary>
        /// <returns></returns>
        public string DelManualTemp()
        {
            ManualTempTable table = new ManualTempTable();
            return table.DelRow();
        }

        /// <summary>
        /// 获取临时数据
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetManualTemp(out ManualValue item)
        {
            ManualTempTable table = new ManualTempTable();
            return table.SelectRowTemp(out item);
        }
        public string GetManualColl(out string info)
        {
            ManualTempTable table = new ManualTempTable();
            return table.SelectRowColl(out info);
        }
    }
}
