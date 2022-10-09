using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Chromatogram
{
    /**
    * ClassName: Curve
    * Description: 曲线类，每条曲线包括的所有参数
    * Version: 1.0
    * Create:  2018/05/21
    * Author:  wangkai
    * Company: jshanbon
    **/
    public class Curve
    {
        //名称      
        public string MName { get; set; }                      

        //Y轴单位
        public string MUnit { get; set; }

        //颜色
        public Color MColor { get; set; }

        //显示
        public bool MShow { get; set; }

        //Y轴标尺手动自动
        public EnumAxisScale MAxisScale { get; set; }

        //Y轴自动标尺上限
        public double MMaxAuto { get; set; }

        //Y轴自动标尺下限
        public double MMinAuto { get; set; }

        //Y轴手动标尺上限
        public double MMaxFix { get; set; }

        //Y轴手动标尺下限
        public double MMinFix { get; set; }

        //最大值
        public double MMax
        {
            get
            {
                switch (MAxisScale)
                {
                    case EnumAxisScale.Fixed: return MMaxFix;
                    default: return MMaxAuto;
                }
            }
        }

        //最小值
        public double MMin
        {
            get
            {
                switch (MAxisScale)
                {
                    case EnumAxisScale.Fixed: return MMinFix;
                    default: return MMinAuto;
                }
            }
        }

        //局部图最大值
        private double m_maxPart = 1;
        public double MMaxPart
        {
            get
            {
                return m_maxPart;
            }
            set
            {
                m_maxPart = value;
            }
        }

        //局部图最小值
        private double m_minPart = 0;
        public double MMinPart
        {
            get
            {
                return m_minPart;
            }
            set
            {
                m_minPart = value;
            }
        }

        /// <summary>
        /// 位移
        /// </summary>
        public double MMove { get; set; }

        //实时运行数据列表
        private List<double> m_drawData = new List<double>();    
        public List<double> MDrawData
        {
            get
            {
                return m_drawData;
            }
        }


        /// <summary>
        /// 带参构造函数，设置曲线的名称和颜色
        /// </summary>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="unit"></param>
        /// <param name="show"></param>
        public Curve(string name, string unit, Color color, bool show)
        {
            MName = name;
            MUnit = unit;
            MColor = color;
            MShow = show;

            Clear();
        }

        /// <summary>
        /// 清除值
        /// </summary>
        /// <param name="flag"></param>
        public void Clear()
        {
            switch (MAxisScale)
            {
                case EnumAxisScale.Auto:
                    MMaxAuto = 1.0;
                    MMinAuto = 0.0;
                    MMaxFix = 1.0;
                    MMinFix = 0.0;
                    break;
            }

            m_drawData.Clear();
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="data"></param>
        public void AddData(double data)
        {
            m_drawData.Add(data);

            if (data > MMaxAuto)
            {
                MMaxAuto = data;
            }
            else if (data < MMinAuto)
            {
                MMinAuto = data;
            }
        }

        /// <summary>
        /// 还原数据后进行最值赋值
        /// </summary>
        public void RestoreData()
        {
            if (0 < m_drawData.Count)
            {
                MMaxAuto = m_drawData.Max();
                MMinAuto = m_drawData.Min();
            }

            if (MMinAuto == MMaxAuto)
            {
                MMaxAuto += 1;
            }
        }

        /// <summary>
        /// 获取指定序号的Y值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double GetData(int index)
        {
            if (0 <= index && index < m_drawData.Count)
            {
                return m_drawData[index];
            }
            else
            {
                return 0;
            }
        }
    }


    /// <summary>
    /// 坐标尺的最值枚举
    /// </summary>
    public enum EnumAxisScale
    {
        Auto,
        Fixed
    }
}
