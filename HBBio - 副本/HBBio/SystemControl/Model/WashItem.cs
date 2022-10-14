using HBBio.Communication;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.SystemControl
{
    public class WashSystem
    {
        private List<WashItem> MList { get; set; }
        private List<EnumWashStatus> MListStatus { get; set; }
        private int MBPV { get; set; }
        private int MCPV { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public WashSystem()
        {
            MList = new List<WashItem>();
            MListStatus = new List<EnumWashStatus>();
            MBPV = -1;
            MCPV = -1;

            foreach (var it in Enum.GetNames(typeof(ENUMPumpName)))
            {
                MList.Add(new WashItem());
                MListStatus.Add(EnumWashStatus.No);
            }
        }

        /// <summary>
        /// 开始清洗
        /// </summary>
        /// <param name="comConf"></param>
        /// <param name="index"></param>
        public void Start(ComConfStatic comConf, ENUMPumpName index, int wash)
        {
            if (-1 == MBPV)
            {
                switch (wash)
                {
                    case 1:
                        //清洗系统
                        MBPV = comConf.GetValveSet(ENUMValveName.BPV);
                        comConf.SetValve(ENUMValveName.BPV, 0);
                        break;
                    case 2:
                        //清洗泵
                        MBPV = comConf.GetValveSet(ENUMValveName.BPV);
                        comConf.SetValve(ENUMValveName.BPV, 1);
                        MCPV = comConf.GetValveSet(ENUMValveName.CPV_1);
                        comConf.SetValve(ENUMValveName.CPV_1, 0);
                        break;
                } 
            }
            MList[(int)index].Start(comConf, index);
        }

        public void StartAll(ComConfStatic comConf, int wash)
        {
            if (-1 == MBPV)
            {
                switch (wash)
                {
                    case 1:
                        //清洗系统
                        MBPV = comConf.GetValveSet(ENUMValveName.BPV);
                        comConf.SetValve(ENUMValveName.BPV, 0);
                        break;
                    case 2:
                        //清洗泵
                        MBPV = comConf.GetValveSet(ENUMValveName.BPV);
                        comConf.SetValve(ENUMValveName.BPV, 1);
                        MCPV = comConf.GetValveSet(ENUMValveName.CPV_1);
                        comConf.SetValve(ENUMValveName.CPV_1, 0);
                        break;
                }
            }
            for (int i = 0; i < MList.Count; i++)
            {
                MList[i].Start(comConf, (ENUMPumpName)i);
            }
        }

        public void Stop(ComConfStatic comConf, ENUMPumpName index)
        {
            MList[(int)index].Stop(comConf, index);
        }

        public EnumWashStatus Update(ComConfStatic comConf)
        {
            for (int i = 0; i < MList.Count; i++)
            {
                MListStatus[i] = MList[i].Update(comConf, (ENUMPumpName)i);
            }

            foreach (var it in MListStatus)
            {
                if (EnumWashStatus.No != it)
                {
                    foreach (var it2 in MListStatus)
                    {
                        if (EnumWashStatus.No != it2 && EnumWashStatus.Over != it2)
                        {
                            return EnumWashStatus.Ing;
                        }
                    }

                    foreach (var it3 in MList)
                    {
                        it3.Clear();
                    }

                    if (-1 != MBPV)
                    {
                        comConf.SetValve(ENUMValveName.BPV, MBPV);
                        MBPV = -1;
                        if (-1 != MCPV)
                        {
                            comConf.SetValve(ENUMValveName.CPV_1, MCPV);
                            MCPV = -1;
                        }
                    }

                    return EnumWashStatus.Over;
                }
            }
            return EnumWashStatus.No;
        }
    }

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
                    if (System.Windows.Visibility.Visible != ItemVisibility.s_listValve[ENUMValveName.BPV] || 0 == comConf.GetValveGet(ENUMValveName.BPV))
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