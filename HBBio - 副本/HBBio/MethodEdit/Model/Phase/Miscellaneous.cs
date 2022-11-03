using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: Miscellaneous
     * Description: 其它
     * Version: 1.0
     * Create:  2020/05/27
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    [Serializable]
    public class Miscellaneous : BasePhase
    {
        public bool MEnableSetMark { get; set; }
        public string MSetMark { get; set; }

        public bool MEnableMethodDelay { get; set; }
        public BaseTVCV MMethodDelay { get; set; }

        public bool MEnableMessage { get; set; }
        public string MMessage { get; set; }
        public bool MEnablePauseAfterMessage { get; set; }

        public bool MEnablePauseTimer { get; set; }
        public double MPauseTimer { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type"></param>
        public Miscellaneous()
        {
            MSetMark = "";
            MMethodDelay = new BaseTVCV();
            MMessage = "";
            MPauseTimer = 0;
        }

        /// <summary>
        /// 计算阶段长度
        /// </summary>
        public override string StatisticsAllStep(double columnVol)
        {
            ClearList();

            if (MEnableMethodDelay)
            {
                AddStep(ReadXaml.GetResources("labMEMethodDelay"), MMethodDelay);
            }
            else
            {
                BaseTVCV tvcv = new BaseTVCV();
                tvcv.MTVCV = 0;
                AddStep(ReadXaml.GetResources("labMEMethodDelay"), tvcv);
            }

            return null;
        }

        public override bool Compare(BasePhase baseItem)
        {
            Miscellaneous item = (Miscellaneous)baseItem;

            if (MEnableSetMark != item.MEnableSetMark
                || !MSetMark.Equals(item.MSetMark)
                || MEnableMethodDelay != item.MEnableMethodDelay
                || !MMethodDelay.Compare(item.MMethodDelay)
                || MEnableMessage != item.MEnableMessage
                || !MMessage.Equals(item.MMessage)
                || MEnablePauseAfterMessage != item.MEnablePauseAfterMessage
                || MEnablePauseTimer != item.MEnablePauseTimer
                || MPauseTimer != item.MPauseTimer)
            {
                return false;
            }

            return true;
        }
    }
}
