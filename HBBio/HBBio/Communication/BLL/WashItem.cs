using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    /**
     * ClassName: WashItem
     * Description: 清洗单元
     * Version: 1.0
     * Create:  2021/04/21
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class WashItem
    {
        private EnumWashStatus m_state = EnumWashStatus.No;     //当前清洗的状态
        private DateTime m_start = DateTime.Now;                //开始清洗的时间点
        private double m_flow = 0;                              //保存清洗前的流速


        /// <summary>
        /// 开始清洗
        /// </summary>
        /// <param name="comConf"></param>
        /// <param name="index"></param>
        public void Start(ComConfStatic comConf, ENUMPumpName index)
        {
            if (EnumWashStatus.No == m_state)
            {
                m_start = DateTime.Now;
                m_flow = comConf.GetPumpSet(index);
                m_state = EnumWashStatus.Start;
            }
        }

        /// <summary>
        /// 返回当前状态
        /// </summary>
        /// <param name="comConf"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public EnumWashStatus Update(ComConfStatic comConf, ENUMPumpName index)
        {
            switch (m_state)
            {
                case EnumWashStatus.Start:
                    bool canIng = false;
                    if (4 == EnumIJVInfo.Count)
                    {
                        if (comConf.GetValveSet(ENUMValveName.IJV) == comConf.GetValveGet(ENUMValveName.IJV)
                            &&(System.Windows.Visibility.Visible != ItemVisibility.s_listValve[ENUMValveName.BPV] || comConf.GetValveSet(ENUMValveName.BPV) == comConf.GetValveGet(ENUMValveName.BPV)))
                        {
                            canIng = true;
                        }
                    }
                    else
                    {
                        if (System.Windows.Visibility.Visible != ItemVisibility.s_listValve[ENUMValveName.BPV] || comConf.GetValveSet(ENUMValveName.BPV) == comConf.GetValveGet(ENUMValveName.BPV))
                        {
                            canIng = true;
                        }
                    }

                    if (canIng)
                    {
                        //如果存在旁通阀，则切换到旁路开始设置流速
                        m_state = EnumWashStatus.Ing;
                        switch (index)
                        {
                            case ENUMPumpName.FITS:
                                comConf.SetPump(index, StaticValue.s_maxFlowSVol * StaticSystemConfig.SSystemConfig.MConfWash.MWashFlowPer / 100);
                                break;
                            case ENUMPumpName.FITA:
                                comConf.SetPump(index, StaticValue.s_maxFlowAVol * StaticSystemConfig.SSystemConfig.MConfWash.MWashFlowPer / 100);
                                break;
                            case ENUMPumpName.FITB:
                                comConf.SetPump(index, StaticValue.s_maxFlowBVol * StaticSystemConfig.SSystemConfig.MConfWash.MWashFlowPer / 100);
                                break;
                            case ENUMPumpName.FITC:
                                comConf.SetPump(index, StaticValue.s_maxFlowCVol * StaticSystemConfig.SSystemConfig.MConfWash.MWashFlowPer / 100);
                                break;
                            case ENUMPumpName.FITD:
                                comConf.SetPump(index, StaticValue.s_maxFlowDVol * StaticSystemConfig.SSystemConfig.MConfWash.MWashFlowPer / 100);
                                break;
                        }
                    }
                    break;
                case EnumWashStatus.Ing:
                    if (ValueTrans.TimeSpanToMin(DateTime.Now, m_start) >= StaticSystemConfig.SSystemConfig.MConfWash.MWashTime - 0.05)
                    {
                        //提前0.05分钟降速
                        comConf.SetPump(index, m_flow);
                        m_start = DateTime.Now;
                        m_state = EnumWashStatus.Stop;
                    }
                    break;
                case EnumWashStatus.Stop:
                    if (ValueTrans.TimeSpanToMin(DateTime.Now, m_start) >= 0.05)
                    {
                        m_state = EnumWashStatus.Over;
                    }
                    break;
            }

            return m_state;
        }

        public void Stop(ComConfStatic comConf, ENUMPumpName index)
        {
            comConf.SetPump(index, m_flow);
            m_start = DateTime.Now;
            m_state = EnumWashStatus.Stop;
        }

        public void Clear()
        {
            m_state = EnumWashStatus.No;
        }
    }

    /// <summary>
    /// 清洗的状态枚举
    /// </summary>
    public enum EnumWashStatus
    {
        No,
        Start,
        Ing,
        Stop,
        Over
    }
}