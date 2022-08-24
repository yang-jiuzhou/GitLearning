using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Collection
{
    [Serializable]
    public class CollectionValveDelay
    {
        public int m_outIndex = -1;         	//出口阀
        public double m_vol = 0;                //体积
        public int m_mode = -1;                 //收集类型
        

        /// <summary>
        /// 构造函数
        /// </summary>
        public CollectionValveDelay()
        {
            m_outIndex = 0;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="outIndex"></param>
        /// <param name="vol"></param>
        /// <param name="arrVol"></param>
        /// <param name="mode1"></param>
        /// <param name="mode2"></param>
        /// <param name="mode3"></param>
        /// <param name="mode4"></param>
        public CollectionValveDelay(int outIndex, double vol, List<Communication.ConfpHCdUV> arrVol, int mode1, int mode2, int mode3 = 0, int mode4 = 0)
        {
            m_outIndex = outIndex;
            m_vol = vol;

            double delayVol = 0;
            if (mode1 >= 3 && delayVol < arrVol[mode1 - 3].MVol)
            {
                delayVol = arrVol[mode1 - 3].MVol;
                m_mode = mode1;
            }
            if (mode2 >= 3 && delayVol < arrVol[mode2 - 3].MVol)
            {
                delayVol = arrVol[mode2 - 3].MVol;
                m_mode = mode2;
            }
            if (mode3 >= 3 && delayVol < arrVol[mode3 - 3].MVol)
            {
                delayVol = arrVol[mode3 - 3].MVol;
                m_mode = mode3;
            }
            if (mode4 >= 3 && delayVol < arrVol[mode4 - 3].MVol)
            {
                delayVol = arrVol[mode4 - 3].MVol;
                m_mode = mode4;
            }      
        }
    }
}
