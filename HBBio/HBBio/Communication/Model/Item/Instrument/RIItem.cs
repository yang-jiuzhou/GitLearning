using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class RIItem : BaseInstrument
    {
        public double m_value = 0;              //读值
        public int m_tempSet = 0;               //设置温度

        public bool m_clear = false;            //清零
        public bool m_temperature = false;      //设置温度
        public bool m_purgeOn = false;          //清洗开始
        public bool m_purgeOff = false;         //清洗结束

        
        /// <summary>
        /// 构造函数
        /// </summary>
        public RIItem()
        {
            MConstNameList = Enum.GetNames(typeof(ENUMRIName));
            MConstName = MConstNameList[0];
        }

        /// <summary>
        /// 返回列表
        /// </summary>
        /// <returns></returns>
        public override List<Signal> CreateSignalList()
        {
            List<Signal> result = new List<Signal>();

            result.Add(new Signal(MConstName, MDlyName, DlyBase.SC_RIUNIT, -512, 512, true, true));
            return result;
        }

        public void Clear()
        {
            m_clear = true;
        }

        public void Temperature(int temp)
        {
            m_tempSet = temp;
            m_temperature = true;
        }

        public void PurgeOn()
        {
            m_purgeOn = true;
        }

        public void PurgeOff()
        {
            m_purgeOff = true;
        }
    }
}
