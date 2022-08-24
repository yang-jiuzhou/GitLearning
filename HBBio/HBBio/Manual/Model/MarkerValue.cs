using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Manual
{
    [Serializable]
    public class MarkerValue
    {
        public bool m_update = false;

        public string MType { get; set; }
        public bool MIsReal { get; set; }
        public double MVal { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public MarkerValue()
        {
            MType = "";
            MIsReal = true;
            MVal = 0;
        }

        /// <summary>
        /// 返回副本
        /// </summary>
        /// <returns></returns>
        public MarkerValue Clone()
        {
            MarkerValue item = new MarkerValue();
            item.MType = MType;
            item.MIsReal = MIsReal;
            item.MVal = MVal;

            return item;
        }
    }
}
