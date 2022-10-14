using HBBio.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Evaluation
{
    /// <summary>
    /// 峰的积分内容枚举
    /// </summary>
    public enum EnumIntegration
    {
        Name,               //峰名称
        RetentionTime,      //保留时间
        StartPt,            //峰起点
        EndPt,              //峰终点
        TopVal,              //峰值
        StartVal,           //起始值
        EndVal,             //终止值
        Height,             //峰高
        Area,               //峰面积
        AreaPer,            //峰面积百分比
        HalfWidth,          //半峰宽
        Tpn,                //理论塔板数
        TailingFactor,      //拖尾因子(对称因子)
        SymmetryFactor,     //不对称因子
        Resolution          //分离度
    }


    /**
     * ClassName: PeakIntegration
     * Description: 峰的积分
     * Version: 1.0
     * Create:  2021/03/06
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class PeakIntegration : DlyNotifyPropertyChanged
    {
        private string m_name = "";                     //峰名称
        private double m_retentionTime = 0;             //保留时间
        private double m_topVal = 0;                    //保留时间对应峰值
        private double m_startValX = 0;                 //起始值
        private double m_startValY = 0;                 //起始值
        private double m_endValX = 0;                   //终止值
        private double m_endValY = 0;                   //终止值
        private double m_height = 0;                    //峰高
        private double m_area = 0;                      //峰面积
        private double m_areaPer = 0;                   //峰面积百分比
        private double m_halfWidth = 0;                 //半峰宽
        private double m_tpn = 0;                       //理论塔板数
        private double m_tailingFactor = 0;             //拖尾因子(对称因子)5%
        private double m_symmetryFactor = 0;            //不对称因子10%
        private double m_resolution = 0;                //分离度

        public string MName
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
                OnPropertyChanged("MName");
            }
        }                //峰名称
        public double MRetentionTime
        {
            get
            {
                return m_retentionTime;
            }
            set
            {
                m_retentionTime = value;
                OnPropertyChanged("MRetentionTime");
            }
        }       //保留时间
        public double MTopVal
        {
            get
            {
                return m_topVal;
            }
            set
            {
                m_topVal = value;
                OnPropertyChanged("MTopVal");
            }
        }
        public double MStartValX
        {
            get
            {
                return m_startValX;
            }
            set
            {
                m_startValX = value;
                OnPropertyChanged("MStartValX");
            }
        }           //起始值     
        public double MStartValY
        {
            get
            {
                return m_startValY;
            }
            set
            {
                m_startValY = value;
                OnPropertyChanged("MStartValY");
            }
        }           //起始值
        public double MEndValX
        {
            get
            {
                return m_endValX;
            }
            set
            {
                m_endValX = value;
                OnPropertyChanged("MEndValX");
            }
        }             //终止值
        public double MEndValY
        {
            get
            {
                return m_endValY;
            }
            set
            {
                m_endValY = value;
                OnPropertyChanged("MEndValY");
            }
        }             //终止值
        public double MHeight
        {
            get
            {
                return m_height;
            }
            set
            {
                m_height = value;
                OnPropertyChanged("MHeight");
            }
        }              //峰高
        public double MArea
        {
            get
            {
                return m_area;
            }
            set
            {
                m_area = value;
                OnPropertyChanged("MArea");
            }
        }                //峰面积
        public double MAreaPer
        {
            get
            {
                return m_areaPer;
            }
            set
            {
                m_areaPer = value;
                OnPropertyChanged("MAreaPer");
            }
        }             //峰面积百分比
        public double MHalfWidth
        {
            get
            {
                return m_halfWidth;
            }
            set
            {
                m_halfWidth = value;
                OnPropertyChanged("MHalfWidth");
            }
        }           //半峰宽
        public double MTpn
        {
            get
            {
                return m_tpn;
            }
            set
            {
                m_tpn = value;
                OnPropertyChanged("MTpn");
            }
        }                 //理论塔板数
        public double MTailingFactor
        {
            get
            {
                return m_tailingFactor;
            }
            set
            {
                m_tailingFactor = value;
                OnPropertyChanged("MTailingFactor");
            }
        }       //拖尾因子(对称因子)5%
        public double MSymmetryFactor
        {
            get
            {
                return m_symmetryFactor;
            }
            set
            {
                m_symmetryFactor = value;
                OnPropertyChanged("MSymmetryFactor");
            }
        }      //不对称因子10%
        public double MResolution
        {
            get
            {
                return m_resolution;
            }
            set
            {
                m_resolution = value;
                OnPropertyChanged("MResolution");
            }
        }          //分离度

        public int StartPoint;          //点索引
        public int EndPoint;            //点索引
        public int PeekPoint;           //点索引
        public double StartBaseY;       //峰起始基线值
        public double EndBaseY;         //峰结束基线值
        public PeakType MPeakType = PeakType.VerticalSeparation;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="peekPoint"></param>
        /// <param name="startBaseY"></param>
        /// <param name="endBaseY"></param>
        public PeakIntegration(int startPoint, int endPoint, int peekPoint, double startBaseY, double endBaseY, PeakType type)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            PeekPoint = peekPoint;
            StartBaseY = startBaseY;
            EndBaseY = endBaseY;
            MPeakType = type;
        }
    }

    public enum PeakType
    {
        [Description("垂直分离")]
        VerticalSeparation = 1,
        [Description("峰谷分离")]
        PeakValleySeparation = 2,
        [Description("其它")]
        Other = 3,
    }
}
