using HBBio.Collection;
using HBBio.Communication;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Manual
{
    /**
     * ClassName: ManualValue
     * Description: 手动操作集合
     * Version: 1.0
     * Create:  2021/05/21
     * Author:  wangkai
     * Company: jshanbon
     **/
    [Serializable]
    public class ManualValue
    {
        public PumpSystemValue m_pumpSystemValue = new PumpSystemValue();
        public PumpSampleValue m_pumpSampleValue = new PumpSampleValue();
        public ValveListValue m_valveValue = new ValveListValue();
        public CollectionValve m_collValveValue = new CollectionValve();
        public CollectionCollector m_collCollectorValue = new CollectionCollector();
        public ASListValue m_ASValue = new ASListValue();
        public MonitorValue m_MonitorValue = new MonitorValue();
        public AlarmWarningValve m_alarmWarningValue = new AlarmWarningValve();
        public MarkerValue m_markerValue = new MarkerValue();
        public PauseStopValue m_pauseValue = new PauseStopValue();
        public PauseStopValue m_stopValue = new PauseStopValue();
        public UVValue m_uvValue = new UVValue();
        public RIValue m_riValue = new RIValue();
        public MixerValue m_mixerValue = new MixerValue();


        /// <summary>
        /// 更新开始值
        /// </summary>
        /// <param name="list"></param>
        public void UpdateStart(List<ConfAS> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                m_ASValue.MList[i].UpdateStart(list[i].MDelayLength, list[i].MDelayUnit);
            }
        }

        /// <summary>
        /// 运行结束后清空数据
        /// </summary>
        public void Clear()
        {
            m_pumpSystemValue.Clear();
            m_pumpSampleValue.Clear();
            m_valveValue.Clear();
            m_collValveValue.JudgeFinish();
            m_collCollectorValue.JudgeFinish();
            m_ASValue.Clear();
            m_MonitorValue.Clear();
            m_alarmWarningValue.Clear();
            m_pauseValue.Clear();
            m_stopValue.Clear();
            m_uvValue.Clear();
            m_riValue.Clear();
            m_mixerValue.Clear();
        }
    }
}