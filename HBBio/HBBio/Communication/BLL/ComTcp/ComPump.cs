using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class ComPump : BaseCom
    {
        protected double m_maxFlowVol = 0;                                  //最大流速
        protected PUMPState m_state = PUMPState.Free;                       //状态
        protected ENUMPumpID m_id = ENUMPumpID.OEM0100;                     //设备识别码


        /// <summary>
        /// 构造函数
        /// </summary>
        public ComPump(ComConf info) : base(info)
        {

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

                m_id = (ENUMPumpID)Enum.Parse(typeof(ENUMPumpID), m_scInfo.MModel);

                switch (m_id)
                {
                    case ENUMPumpID.NP7001: m_maxFlowVol = 10; break;
                    case ENUMPumpID.NP7005: m_maxFlowVol = 50; break;
                    case ENUMPumpID.NP7010: m_maxFlowVol = 100; break;
                    case ENUMPumpID.NP7030: m_maxFlowVol = 300; break;
                    case ENUMPumpID.NP7060: m_maxFlowVol = 600; break;
                    case ENUMPumpID.P1001L: m_maxFlowVol = 1000; break;
                    case ENUMPumpID.P1003L: m_maxFlowVol = 3000; break;
                    case ENUMPumpID.OEM0025: m_maxFlowVol = 30; break;
                    case ENUMPumpID.OEM0030: m_maxFlowVol = 30; break;
                    case ENUMPumpID.OEM0100: m_maxFlowVol = 100; break;
                    case ENUMPumpID.OEM0300: m_maxFlowVol = 300; break;
                    case ENUMPumpID.HB0030: m_maxFlowVol = 30; break;
                }
            }
        }


        /// <summary>
        /// 线程的状态设置
        /// </summary>
        public override void ThreadStatus(ENUMThreadStatus status)
        {
            switch (status)
            {
                case ENUMThreadStatus.Free:
                    m_state = PUMPState.Free;
                    break;
                case ENUMThreadStatus.Version:
                    m_state = PUMPState.Version;
                    break;
                case ENUMThreadStatus.WriteOrRead:
                    m_state = PUMPState.Start;
                    break;
                case ENUMThreadStatus.Abort:
                    m_state = PUMPState.Abort;
                    break;
            }
        }
    }
}
