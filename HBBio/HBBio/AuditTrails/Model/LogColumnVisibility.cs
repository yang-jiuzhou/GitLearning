using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.AuditTrails
{
    /**
     * ClassName: LogColumnVisibility
     * Description: 日志行显隐信息
     * Version: 1.0
     * Create:  2020/01/21
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class LogColumnVisibility
    {
        /// <summary>
        /// 各列显隐
        /// </summary>
        public bool[] MArrVisib { get; set; }
        

        /// <summary>
        /// 构造函数
        /// </summary>
        public LogColumnVisibility()
        {
            MArrVisib = new bool[Enum.GetNames(typeof(EnumLog)).GetLength(0)];
            for (int i = 0; i < MArrVisib.Length; i++)
            {
                MArrVisib[i] = true;
            }
        }
    }
}
