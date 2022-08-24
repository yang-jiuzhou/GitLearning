using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Result
{
    /**
    * ClassName: ResultRow
    * Description: 运行结果数据值类，单元数据库的一行
    * Version: 1.0
    * Create:  2018/05/21
    * Author:  wangkai
    * Company: jshanbon
    **/
    public class ResultRow
    {
        public double m_T = -0.01;
        public double m_V = 0.0;
        public double m_CV = 0.0;
        public List<double> m_valList = new List<double>();


        /// <summary>
        /// 重设数据
        /// </summary>
        /// <param name="length"></param>
        public void SetList(int length)
        {
            if (m_valList.Count != length)
            {
                m_valList.Clear();
                for (int i = 0; i < length; i++)
                {
                    m_valList.Add(0);
                }
            }
        }
    }
}
