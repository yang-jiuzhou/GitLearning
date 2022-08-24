using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Evaluation
{
    /**
     * ClassName: IntegrationSet
     * Description: 积分条件
     * Version: 1.0
     * Create:  2021/03/06
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    [Serializable]
    public class IntegrationSet
    {
        public readonly bool[] m_arrShow = null;    //信息显隐
        public bool MIsMin { get; set; }            //启用最值判断
        public double MMinHeight { get; set; }      //最小峰高
        public double MMinArea { get; set; }        //最小峰面积
        public double MMinWidth { get; set; }       //最小峰宽
        public bool MIsCount { get; set; }          //启用数量判断
        public int MPeakCount { get; set; }         //峰数量

        public double MOriginal { get; set; }       //原点(非存储)
        public double MCH { get; set; }             //柱高(非存储)


        /// <summary>
        /// 构造函数
        /// </summary>
        public IntegrationSet()
        {
            m_arrShow = new bool[Enum.GetNames(typeof(EnumIntegration)).GetLength(0)];
            for (int i = 0; i < m_arrShow.Length; i++)
            {
                m_arrShow[i] = true;
            }

            MIsMin = true;
            MMinHeight = 10;
            MMinArea = 10;
            MMinWidth = 0.5;
            MIsCount = false;
            MPeakCount = 1;

            MOriginal = 0;
            MCH = 1;
        }
    }
}
