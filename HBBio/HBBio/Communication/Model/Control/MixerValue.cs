using HBBio.Communication;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    [Serializable]
    public class MixerValue
    {
        public bool m_update = false;

        public bool MOnoff { get; set; }


        /// <summary>
        /// 深度拷贝
        /// </summary>
        /// <param name="item"></param>
        public void DeepCopy(MixerValue item)
        {
            m_update = item.m_update;
            MOnoff = item.MOnoff;
        }

        /// <summary>
        /// 设置实时信息
        /// </summary>
        /// <param name="item"></param>
        public void SetCurrInfo(MixerItem item)
        {
            MOnoff = item.m_onoffGet;
        }

        /// <summary>
        /// 清除临时变量
        /// </summary>
        public void Clear()
        {
            MOnoff = false;
        }
    }
}
