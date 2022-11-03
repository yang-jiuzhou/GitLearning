using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: FlowVolLen
     * Description: 
     * Version: 1.0
     * Create:  2021/02/03
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    [Serializable]
    public class FlowVolLen
    {
        /// <summary>
        /// 流速单位
        /// </summary>
        public EnumFlowRate MEnumFlowRate { get; set; }
        /// <summary>
        /// 体积流速
        /// </summary>
        public double MFlowVol { get; set; }
        /// <summary>
        /// 线性流速
        /// </summary>
        public double MFlowLen { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public double MFlowRate
        {
            get
            {
                switch (MEnumFlowRate)
                {
                    case EnumFlowRate.MLMIN:
                        return MFlowVol;
                    default:
                        return MFlowLen;
                }
            }
            set
            {
                double newVal = Math.Round(value, 2);
                switch (MEnumFlowRate)
                {
                    case EnumFlowRate.MLMIN:
                        MFlowVol = newVal;
                        break;
                    default:
                        MFlowLen = newVal;
                        break;
                }
            }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        public FlowVolLen()
        {
            MEnumFlowRate = EnumFlowRate.MLMIN;
            MFlowVol = 1;
            MFlowLen = 1;
        }

        public bool Compare(FlowVolLen item)
        {
            if (null == item)
            {
                return false;
            }

            if (MEnumFlowRate != item.MEnumFlowRate
                || MFlowVol != item.MFlowVol
                || MFlowLen != item.MFlowLen
                || MFlowRate != item.MFlowRate)
            {
                return false;
            }

            return true;
        }

        public void Init(EnumFlowRate enumFlowRate, double columnArea)
        {
            MEnumFlowRate = enumFlowRate;
            switch(enumFlowRate)
            {
                case EnumFlowRate.MLMIN:
                    MFlowLen = MFlowVol * 60 / columnArea;
                    break;
                case EnumFlowRate.CMH:
                    MFlowVol = MFlowLen * columnArea / 60;
                    break;
            }
        }

        public void Update(double newVal, double columnArea)
        {
            switch (MEnumFlowRate)
            {
                case EnumFlowRate.MLMIN:
                    MFlowVol = newVal;
                    MFlowLen = MFlowVol * 60 / columnArea;
                    break;
                case EnumFlowRate.CMH:
                    MFlowLen = newVal;
                    MFlowVol = MFlowLen * columnArea / 60;
                    break;
            }
        }
    }
}
