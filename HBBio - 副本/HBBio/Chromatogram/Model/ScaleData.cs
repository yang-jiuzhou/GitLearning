using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Chromatogram
{
    /**
    * ClassName: ScaleData
    * Description: 局部放大的数组列表管理
    * Version: 1.0
    * Create:  2018/05/21
    * Author:  wangkai
    * Company: jshanbon
    **/
    public class ScaleData
    {
        private List<double> m_xMin = new List<double>();  //X下限值
        private List<double> m_xMax = new List<double>();  //X上限值
        private List<double> m_yMin = new List<double>();  //Y下限值
        private List<double> m_yMax = new List<double>();  //Y上限值

        /// <summary>
        /// 属性,当前限值列表
        /// </summary>
        public int MCount
        {
            get
            {
                return m_xMin.Count;
            }
        }

        /// <summary>
        /// 属性，X下限
        /// </summary>
        public double MXMin
        {
            get
            {
                return m_xMin.Last();
            }
        }

        /// <summary>
        /// 属性，X上限
        /// </summary>
        public double MXMax
        {
            get
            {
                return m_xMax.Last();
            }
        }

        /// <summary>
        /// 属性，Y下限
        /// </summary>
        public double MYMin
        {
            get
            {
                return m_yMin.Last();
            }
        }

        /// <summary>
        /// 属性，Y上限
        /// </summary>
        public double MYMax
        {
            get
            {
                return m_yMax.Last();
            }
        }


        /// <summary>
        /// 带参构造函数
        /// </summary>
        public ScaleData()
        {
            InitData();
        }

        /// <summary>
        /// 添加一组
        /// </summary>
        /// <param name="startPerX"></param>
        /// <param name="stopPerX"></param>
        /// <param name="startPerY"></param>
        /// <param name="stopPerY"></param>
        public void Add(double startPerX, double stopPerX, double startPerY, double stopPerY)
        {
            int lastCount = MCount - 1;//Count一直变化，所以取临时值
            m_xMin.Add(startPerX * (m_xMax[lastCount] - m_xMin[lastCount]) + m_xMin[lastCount]);
            m_xMax.Add(stopPerX * (m_xMax[lastCount] - m_xMin[lastCount]) + m_xMin[lastCount]);
            m_yMin.Add(startPerY * (m_yMax[lastCount] - m_yMin[lastCount]) + m_yMin[lastCount]);
            m_yMax.Add(stopPerY * (m_yMax[lastCount] - m_yMin[lastCount]) + m_yMin[lastCount]);

            Judge();
        }
        public void AddMinMax(double xMin, double xMax, double yMin, double yMax)
        {
            m_xMin.Add(xMin);
            m_xMax.Add(xMax);
            m_yMin.Add(yMin);
            m_yMax.Add(yMax);
        }

        /// <summary>
        /// 删除最后一组
        /// </summary>
        public void RemoveLast()
        {
            int lastCount = MCount - 1;//Count一直变化，所以取临时值

            m_xMin.RemoveAt(lastCount);
            m_xMax.RemoveAt(lastCount);
            m_yMin.RemoveAt(lastCount);
            m_yMax.RemoveAt(lastCount);
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            m_xMin.Clear();
            m_xMax.Clear();
            m_yMin.Clear();
            m_yMax.Clear();

            InitData();
        }

        /// <summary>
        /// 数据失真
        /// </summary>
        public void Judge()
        {
            if (MCount > 1)
            {
                //放大太多，数据失真
                if (double.Equals(m_xMin[m_xMin.Count - 2], m_xMin[m_xMin.Count - 1]) &&
                double.Equals(m_xMax[m_xMax.Count - 2], m_xMax[m_xMax.Count - 1]) &&
                double.Equals(m_yMin[m_yMin.Count - 2], m_yMin[m_yMin.Count - 1]) &&
                double.Equals(m_yMax[m_yMax.Count - 2], m_yMax[m_yMax.Count - 1]))
                {
                    RemoveLast();
                }
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitData()
        {
            m_xMin.Add(0);
            m_xMax.Add(1);
            m_yMin.Add(0);
            m_yMax.Add(1);
        }
    }
}
