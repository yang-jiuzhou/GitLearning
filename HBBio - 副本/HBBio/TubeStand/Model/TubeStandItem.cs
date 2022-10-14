using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.TubeStand
{
    /**
     * ClassName: TubeStandItem
     * Description: 试管架信息
     * Version: 1.0
     * Create:  2022/01/27
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class TubeStandItem
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string MName { get; set; }
        /// <summary>
        /// 收集体积
        /// </summary>
        public double MCollVolume { get; set; }
        /// <summary>
        /// 试管体积
        /// </summary>
        public double MVolume { get; set; }
        /// <summary>
        /// 试管数量
        /// </summary>
        public int MCount { get; set; }
        /// <summary>
        /// 直径
        /// </summary>
        public double MDiameter { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public double MHeight { get; set; }
        /// <summary>
        /// 行
        /// </summary>
        public int MRow { get; set; }
        /// <summary>
        /// 列
        /// </summary>
        public int MCol { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public TubeStandItem()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="volume"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public TubeStandItem(double volume, int row, int col)
        {
            MName = volume.ToString() + " X " + (row * col);
            MCollVolume = volume;
            MVolume = volume;
            MCount = row * col;
            MDiameter = 1;
            MHeight = volume;
            MRow = row;
            MCol = col;
        }
    }
}
