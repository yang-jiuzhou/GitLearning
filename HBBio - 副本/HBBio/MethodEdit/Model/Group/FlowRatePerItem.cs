using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: FlowRatePerItem
     * Description: 流速百分比梯度列表单元行
     * Version: 1.0
     * Create:  2020/05/27
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    [Serializable]
    public class FlowRatePerItem
    {
        public double MPerBS { get; set; }
        public double MPerBE { get; set; }
        public double MPerCS { get; set; }
        public double MPerCE { get; set; }
        public double MPerDS { get; set; }
        public double MPerDE { get; set; }
        public int MFillSystem { get; set; }
        public BaseTVCV MBaseTVCV { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public FlowRatePerItem()
        {
            MPerBS = 0;
            MPerBE = 0;
            MPerCS = 0;
            MPerCE = 0;
            MPerDS = 0;
            MPerDE = 0;
            MFillSystem = 0;
            MBaseTVCV = new BaseTVCV();
        }
    }
}
