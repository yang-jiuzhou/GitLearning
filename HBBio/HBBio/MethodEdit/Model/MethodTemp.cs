using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    public class MethodTemp
    {
        public int MID { get; set; }
        public string MName { get; set; }
        public int MType { get; set; }
        public int MIndexCurrMethod { get; set; }
        public int MIndexCurrPhase { get; set; }
        public double MPhaseStartT { get; set; }                        //当前执行阶段的开始时间点
        public double MPhaseStartV { get; set; }                        //当前执行阶段的开始体积点
        public double MPhaseStopT { get; set; }                         //当前执行阶段的结束时间点
        public double MPhaseStopV { get; set; }                         //当前执行阶段的结束体积点
        public double MPhaseRunTime { get; set; }
        public double MHoldStartT { get; set; }                         //当前执行阶段的一次挂起时间点
        public double MHoldStartV { get; set; }                         //当前执行阶段的一次挂起体积点
        public double MHoldTotalT { get; set; }                         //当前执行阶段的一次挂起时间累计
        public double MHoldTotalV { get; set; }                         //当前执行阶段的一次挂起体积累计
        public bool MIsHold { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public MethodTemp()
        {
            MID = -1;
        }
    }
}
