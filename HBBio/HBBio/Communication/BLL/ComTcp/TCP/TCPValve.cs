using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class TCPValve : BaseTCP
    {
        protected ValveItem m_item = null;
        protected VALVEState m_state = VALVEState.Free;                 //泵状态
        protected ENUMValveID m_id = ENUMValveID.VICI4;                 //设备识别码


        /// <summary>
        /// 构造函数
        /// </summary>
        public TCPValve(ComConf info) : base(info)
        {
            if (0 != m_scInfo.MList.Count)
            {
                m_item = (ValveItem)m_scInfo.MList[0];
                m_item.MAre = m_are;
            }

            MComConf = info;
        }

        /// <summary>
        /// 获取运行数据读值
        /// </summary>
        /// <returns></returns>
        public override List<object> GetRunDataValueList()
        {
            List<object> result = new List<object>();

            if (m_item.MVisible)
            {
                result.Add(m_item.MValveGetStr);
            }

            return result;
        }

        /// <summary>
        /// 获取运行数据读值
        /// </summary>
        /// <returns></returns>
        public override List<object> SetRunDataValueList()
        {
            List<object> result = new List<object>();

            if (m_item.MVisible)
            {
                result.Add(m_item.MValveSetStr);
            }

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
                    m_state = VALVEState.Free;
                    m_are.Set();
                    break;
                case ENUMThreadStatus.Version:
                    m_state = VALVEState.Version;
                    m_are.Set();
                    break;
                case ENUMThreadStatus.WriteOrRead:
                    m_state = VALVEState.ReadWriteFirst;
                    m_are.Set();
                    break;
                case ENUMThreadStatus.Abort:
                    m_state = VALVEState.Abort;
                    m_are.Set();
                    break;
            }
        }
    }
}
