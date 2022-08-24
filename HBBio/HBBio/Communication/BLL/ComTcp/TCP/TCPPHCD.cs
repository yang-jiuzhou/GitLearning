using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class TCPPHCD : BaseTCP
    {
        protected PHCDState m_state = PHCDState.Free;                 //泵状态
        protected ENUMDetectorID m_id = ENUMDetectorID.pHCdOEM;       //设备识别码   


        /// <summary>
        /// 构造函数
        /// </summary>
        public TCPPHCD(ComConf info) : base(info)
        {
            
        }

        /// <summary>
        /// 线程的状态设置
        /// </summary>
        public override void ThreadStatus(ENUMThreadStatus status)
        {
            switch (status)
            {
                case ENUMThreadStatus.Free:
                    m_state = PHCDState.Free;
                    break;
                case ENUMThreadStatus.Version:
                    m_state = PHCDState.Version;
                    break;
                case ENUMThreadStatus.WriteOrRead:
                    m_state = PHCDState.Read;
                    break;
                case ENUMThreadStatus.Abort:
                    m_state = PHCDState.Abort;
                    break;
            }
        }
    }
}
