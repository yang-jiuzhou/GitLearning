using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class ComAS : BaseCom
    {
        protected ASItem m_item = new ASItem();                         //元素
        protected ASState m_state = ASState.Free;                       //状态
        protected ENUMDetectorID m_id = ENUMDetectorID.ASABD05;         //设备识别码        
        protected int m_rule = 50;                                      //比较基值


        /// <summary>
        /// 构造函数
        /// </summary>
        public ComAS(ComConf info) : base(info)
        {
            if (0 != m_scInfo.MList.Count)
            {
                m_item = (ASItem)m_scInfo.MList[0];
            }
        }

        /// <summary>
        /// 获取运行数据读值
        /// </summary>
        /// <returns></returns>
        public override List<object> GetRunDataValueList()
        {
            List<object> valList = new List<object>();

            if (m_item.MVisible)
            {
                valList.Add(m_item.m_sizeGet > StaticSystemConfig.SSystemConfig.MListConfAS[(int)m_item.m_name].MSize ? Share.ReadXaml.S_Yes : Share.ReadXaml.S_No);
            }

            return valList;
        }

        /// <summary>
        /// 获取运行数据写值
        /// </summary>
        /// <returns></returns>
        public override List<object> SetRunDataValueList()
        {
            List<object> valList = new List<object>();

            if (m_item.MVisible)
            {
                valList.Add("N/A");
            }

            return valList;
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
        /// 线程的状态设置
        /// </summary>
        public override void ThreadStatus(ENUMThreadStatus status)
        {
            switch (status)
            {
                case ENUMThreadStatus.Free:
                    m_state = ASState.Free;
                    break;
                case ENUMThreadStatus.Version:
                    m_state = ASState.Version;
                    break;
                case ENUMThreadStatus.WriteOrRead:
                    m_state = ASState.First;
                    break;
                case ENUMThreadStatus.Abort:
                    m_state = ASState.Abort;
                    break;
            }
        }
    }
}
