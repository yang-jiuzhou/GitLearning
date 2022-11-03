using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace HBBio.MethodEdit
{
    [XmlInclude(typeof(BaseTVCV))]
    [XmlInclude(typeof(BPVValve))]
    [XmlInclude(typeof(CIP))]
    [XmlInclude(typeof(CollValveCollector))]
    [XmlInclude(typeof(FlowRate))]
    [XmlInclude(typeof(FlowRatePer))]
    [XmlInclude(typeof(FlowValveLength))]
    [XmlInclude(typeof(Mixer))]
    [XmlInclude(typeof(PHCDUVUntil))]
    [XmlInclude(typeof(SampleApplicationTech))]
    [XmlInclude(typeof(UVReset))]
    [XmlInclude(typeof(ValveSelection))]
    [Serializable]
    public abstract class BaseGroup
    {
        /// <summary>
        /// 类型
        /// </summary>
        public EnumGroupType MType { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int MIndex { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseGroup()
        {
            MType = EnumGroupType.FlowValveLength;
            MIndex = 0;
        }

        public abstract bool Compare(BaseGroup baseItem);
    }
}
