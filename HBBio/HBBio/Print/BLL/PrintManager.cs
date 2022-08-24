using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Print
{
    class PrintManager
    {
        /// <summary>
        /// 获取导出设置信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetPDFSet(out PDFSet item)
        {
            PDFSetTable table = new PDFSetTable();
            return table.SelectRow(out item);
        }

        /// <summary>
        /// 设置导出设置信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string SetPDFSet(PDFSet item)
        {
            PDFSetTable table = new PDFSetTable();
            return table.UpdateRow(item);
        }
    }
}
