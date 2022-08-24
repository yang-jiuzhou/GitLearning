using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace HBBio.Evaluation
{
    public class EvaluationManager
    {
        /// <summary>
        /// 获取积分设置
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetIntegrationSet(out IntegrationSet item)
        {
            IntegrationSetTable table = new IntegrationSetTable();
            return table.SelectRow(out item);
        }

        /// <summary>
        /// 更新积分设置
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string SetIntegrationSet(IntegrationSet item)
        {
            IntegrationSetTable table = new IntegrationSetTable();
            return table.UpdateRow(item);
        }

        /// <summary>
        /// 获取导出选项设置
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetOutputSelectSet(out OutputSelectSet item)
        {
            OutputSelectSetTable table = new OutputSelectSetTable();
            return table.SelectRow(out item);
        }

        /// <summary>
        /// 更新导出选项设置
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string SetOutputSelectSet(OutputSelectSet item)
        {
            OutputSelectSetTable table = new OutputSelectSetTable();
            return table.UpdateRow(item);
        }
    }
}
