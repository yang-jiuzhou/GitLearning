using HBBio.Collection;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class ComCollector : BaseCom
    {
        protected CollectorItem m_item = null;
        protected CollectorState m_state = CollectorState.Free;                         //状态
        protected ENUMCollectorID m_id = ENUMCollectorID.QBH_DLY;                       //设备识别码
        protected int m_countL = 0;
        protected int m_countR = 0;


        /// <summary>
        /// 构造函数
        /// </summary>
        public ComCollector(ComConf info) : base(info)
        {
            if (0 != m_scInfo.MList.Count)
            {
                m_item = (CollectorItem)m_scInfo.MList[0];
            }

            MComConf = info;
        }


        /// <summary>
        /// 属性，重载配置参数
        /// </summary>
        public override ComConf MComConf
        {
            get
            {
                return m_scInfo;
            }
            set
            {
                m_scInfo = value;

                if (string.IsNullOrEmpty(m_scInfo.MModel))
                {
                    return;
                }

                if (null == m_item)
                {
                    return;
                }

                m_id = (ENUMCollectorID)Enum.Parse(typeof(ENUMCollectorID), m_scInfo.MModel);
            }
        }

        /// <summary>
        /// 获取运行数据名称
        /// </summary>
        /// <returns></returns>
        public override List<object> GetRunDataValueList()
        {
            List<object> result = new List<object>();

            result.Add(m_item.MShowGet);

            return result;
        }

        /// <summary>
        /// 获取运行数据名称
        /// </summary>
        /// <returns></returns>
        public override List<object> SetRunDataValueList()
        {
            List<object> result = new List<object>();

            result.Add(m_item.MShowSet);

            return result;
        }


        /// <summary>
        /// 线程的状态设置
        /// </summary>
        public override void ThreadStatus(ENUMThreadStatus status)
        {
            switch (status)
            {
                case ENUMThreadStatus.Free:
                    m_state = CollectorState.FreeFirst;
                    break;
                case ENUMThreadStatus.Version:
                    m_state = CollectorState.Version;
                    break;
                case ENUMThreadStatus.WriteOrRead:
                    m_state = CollectorState.ReadFirst;
                    break;
                case ENUMThreadStatus.Abort:
                    m_state = CollectorState.Abort;
                    break;
            }
        }
    }
}
