using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class ComUV : BaseCom
    {
        protected UVItem m_item = null;
        protected UVState m_state = UVState.Free;                       //状态
        protected ENUMDetectorID m_id = ENUMDetectorID.UVQBH2;           //设备识别码


        /// <summary>
        /// 构造函数
        /// </summary>
        public ComUV(ComConf info) : base(info)
        {
            if (0 != m_scInfo.MList.Count)
            {
                m_item = (UVItem)m_scInfo.MList[0];
            }
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

                m_id = (ENUMDetectorID)Enum.Parse(typeof(ENUMDetectorID), m_scInfo.MModel);
            }
        }


        /// <summary>
        /// 获取运行数据名称
        /// </summary>
        /// <returns></returns>
        public override List<object> GetRunDataValueList()
        {
            List<object> result = new List<object>();

            result.Add(m_item.MLamp ? Share.ReadXaml.S_On : Share.ReadXaml.S_Off);
            for (int i = 0; i < m_item.m_signalCount; i++)
            {
                result.Add(m_item.m_waveGet[i].ToString());
            }
            for (int i = 0; i < m_item.m_signalCount; i++)
            {
                result.Add(m_item.m_absGet[i]);
            }

            return result;
        }

        /// <summary>
        /// 获取运行数据名称
        /// </summary>
        /// <returns></returns>
        public override List<object> SetRunDataValueList()
        {
            List<object> result = new List<object>();

            result.Add(m_item.MLamp ? Share.ReadXaml.S_On : Share.ReadXaml.S_Off);
            for (int i = 0; i < m_item.m_signalCount; i++)
            {
                result.Add(m_item.m_waveSet[i].ToString());
            }
            for (int i = 0; i < m_item.m_signalCount; i++)
            {
                result.Add("N/A");
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
                    m_state = UVState.Free;
                    break;
                case ENUMThreadStatus.Version:
                    m_state = UVState.Version;
                    break;
                case ENUMThreadStatus.WriteOrRead:
                    m_state = UVState.ReadFirst;
                    break;
                case ENUMThreadStatus.Abort:
                    m_state = UVState.Abort;
                    break;
            }
        }
    }
}
