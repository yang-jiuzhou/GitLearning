using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Chromatogram
{
    public class AxisScale
    {
        /// <summary>
        /// 自动手动标尺
        /// </summary>
        public EnumAxisScale MAxisScale { get; set; }

        /// <summary>
        /// 标尺下限
        /// </summary>
        public double MMin { get; set; }

        /// <summary>
        /// 标尺上限
        /// </summary>
        public double MMax { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="axisScale"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public AxisScale(EnumAxisScale axisScale, double min, double max)
        {
            MAxisScale = axisScale;
            MMin = min;
            MMax = max;
        }
    }
}
