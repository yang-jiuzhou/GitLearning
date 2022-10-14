using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Chromatogram
{
    /**
    * ClassName: CurveSetStyle
    * Description: 曲线集合属性设置类
    * Version: 1.0
    * Create:  2021/05/21
    * Author:  yangjiuzhou
    * Company: jshanbon
    **/
    public class CurveSetStyle : DlyNotifyPropertyChanged
    {
        #region 字段
        private EnumAxisScale m_axisScale = EnumAxisScale.Auto;
        #endregion

        #region 属性
        /// <summary>
        /// X轴坐标自动还是固定
        /// </summary>
        public EnumAxisScale MAxisScale
        {
            get
            {
                return m_axisScale;
            }
            set
            {
                m_axisScale = value;
                OnPropertyChanged("MAxisScale");
            }
        }
        /// <summary>
        /// X轴最小值
        /// </summary>
        public double MMin { get; set; }
        /// <summary>
        /// X轴最大值
        /// </summary>
        public double MMax { get; set; }
        /// <summary>
        /// Y值列表
        /// </summary>
        public List<CurveStyle> MList { get; set; }
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        public CurveSetStyle()
        {
            MList = new List<CurveStyle>();
        }
    }

    /**
    * ClassName: CurveStyle
    * Description: 曲线属性设置类
    * Version: 1.0
    * Create:  2021/05/21
    * Author:  yangjiuzhou
    * Company: jshanbon
    **/
    public class CurveStyle : DlyNotifyPropertyChanged
    {
        #region 字段
        private System.Windows.Media.Brush m_brush = System.Windows.Media.Brushes.Red;
        private bool m_show = true;
        private EnumAxisScale m_axisScale = EnumAxisScale.Auto;
        #endregion
        private double m_min = 0;
        #region 属性
        /// <summary>
        /// 名称
        /// </summary>
        public string MName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string MUnit { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        public System.Windows.Media.Brush MBrush
        {
            get
            {
                return m_brush;
            }
            set
            {
                m_brush = value;
                OnPropertyChanged("MBrush");
            }
        }
        /// <summary>
        /// 显隐
        /// </summary>
        public bool MShow
        {
            get
            {
                return m_show;
            }
            set
            {
                m_show = value;
                OnPropertyChanged("MShow");
            }
        }
        /// <summary>
        /// Y轴坐标自动还是固定
        /// </summary>
        public EnumAxisScale MAxisScale
        {
            get
            {
                return m_axisScale;
            }
            set
            {
                m_axisScale = value;
                OnPropertyChanged("MAxisScale");
            }
        }
        /// <summary>
        /// Y轴最小值
        /// </summary>
        public double MMin
        {
            get
            {
                return m_min;
            }
            set
            {
                m_min = value;
            }
        }
        /// <summary>
        /// Y轴最大值
        /// </summary>
        public double MMax { get; set; }
        #endregion       
    }
}
