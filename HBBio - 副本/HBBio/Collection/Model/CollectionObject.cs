using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Collection
{
    /**
     * ClassName: CollectionObject
     * Description: 收集对象
     * Version: 1.0
     * Create:  2021/03/12
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    [Serializable]
    public class CollectionObject : DlyNotifyPropertyChanged
    {
        private double m_length = 0;
        private double m_TbB = 0;
        private double m_TbE = 0;

        public int MType { get; set; }
        public double MLength
        {
            get
            {
                return m_length;
            }
            set
            {
                m_length = value;
                if (MType < 3)
                {
                    MTdB = 0;
                    MTdE = value;
                }
            }
        }
        public EnumThresholdSlope MTS { get; set; }
        public double MTdB
        {
            get
            {
                return m_TbB;
            }
            set
            {
                m_TbB = value;
                if (MType < 3)
                {
                    if (m_TbB > MTdE)
                    {
                        m_TbB = 0;
                    }
                }
                OnPropertyChanged("MTdB");
            }
        }
        public double MTdE
        {
            get
            {
                return m_TbE;
            }
            set
            {
                m_TbE = value;
                if (MType < 3)
                {
                    if (m_TbE < MTdB || m_TbE > MLength)
                    {
                        m_TbE = MLength;
                    }
                }
                OnPropertyChanged("MTdE");
            }
        }
        public EnumGreaterLess MSJ { get; set; }
        public double MSlope { get; set; }
    }

    /// <summary>
    /// 阈值/斜率
    /// </summary>
    public enum EnumThresholdSlope
    {
        Threshold,
        Slope,
        ThresholdSlope,
        Greater,
        Less
    }

    /// <summary>
    /// 大于或小于
    /// </summary>
    public enum EnumGreaterLess
    {
        Greater,
        Less
    }
}
