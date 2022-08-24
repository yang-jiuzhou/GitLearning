using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    [Serializable]
    public class RIValue
    {
        public bool m_update = false;

        public bool MOnoff { get; set; }
        public bool MClear { get; set; }
        public int MTemperature { get; set; }

        /// <summary>
        /// 返回副本
        /// </summary>
        /// <returns></returns>
        public RIValue Clone()
        {
            RIValue item = new RIValue();
            item.m_update = m_update;
            item.MOnoff = MOnoff;
            item.MClear = MClear;
            item.MTemperature = MTemperature;

            return item;
        }

        /// <summary>
        /// 设置实时信息
        /// </summary>
        /// <param name="item"></param>
        public void SetCurrInfo(RIItem item)
        {
            MOnoff = item.m_purgeOn;
            MTemperature = item.m_tempSet;
        }

        /// <summary>
        /// 清除临时变量
        /// </summary>
        public void Clear()
        {
            MClear = false;
        }
    }
}
