using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Chromatogram
{
    /**
    * ClassName: MarkerInfo
    * Description: 标记信息
    * Version: 1.0
    * Create:  2019/01/30
    * Author:  yangjiuzhou
    * Company: jshanbon
    **/
    public class MarkerInfo
    {
        //说明
        public string MType { get; set; }
        //画的X值
        public double MValX { get; set; }
        //时间(需要计算)
        public double MT { get; set; }
        public double MV { get; set; }
        public double MCV { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="valX"></param>
        public MarkerInfo(string type, double valX = -1)
        {
            MType = type;
            MValX = valX;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="mT"></param>
        /// <param name="mV"></param>
        /// <param name="mCV"></param>
        public MarkerInfo(string type, double mT, double mV, double mCV)
        {
            MType = type;
            MT = mT;
            MV = mV;
            MCV = mCV;
        }

        public double GetValByBase(EnumBase enumBase)
        {
            switch (enumBase)
            {
                case EnumBase.V: return MV;
                case EnumBase.CV: return MCV;
                default: return MT;
            }
        }
    }
}
