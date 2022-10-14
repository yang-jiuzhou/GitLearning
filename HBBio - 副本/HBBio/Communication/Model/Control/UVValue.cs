using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    [Serializable]
    public class UVValue
    {
        public bool m_update = false;

        public bool MOnoff { get; set; }
        public bool MClear { get; set; }

        public int MWave1 { get; set; }
        public int MWave2 { get; set; }
        public int MWave3 { get; set; }
        public int MWave4 { get; set; }

        public bool MEnabledWave2 { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public UVValue()
        {
            MOnoff = true;
            MWave1 = 254;
            MWave2 = 254;
            MWave3 = 254;
            MWave4 = 254;
            MEnabledWave2 = true;
        }

        /// <summary>
        /// 深度拷贝
        /// </summary>
        /// <param name="item"></param>
        public void DeepCopy(UVValue item)
        {
            m_update = item.m_update;
            MOnoff = item.MOnoff;
            MClear = item.MClear;
            MWave1 = item.MWave1;
            MWave2 = item.MWave2;
            MWave3 = item.MWave3;
            MWave4 = item.MWave4;
            MEnabledWave2 = item.MEnabledWave2;
        }

        /// <summary>
        /// 设置实时信息
        /// </summary>
        /// <param name="item"></param>
        public void SetCurrInfo(UVItem item)
        {
            MOnoff = item.MLamp;
            MWave1 = item.m_waveGet[0];
            MWave2 = item.m_waveGet[1];
            MWave3 = item.m_waveGet[2];
            MWave4 = item.m_waveGet[3];

            MEnabledWave2 = 0 < MWave2;
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
