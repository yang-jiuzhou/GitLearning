using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    /// <summary>
    /// 通讯方式的枚举
    /// </summary>
    public enum EnumCommunMode
    {
        Com,
        TCP
    }

    /// <summary>
    /// 自动进样器的型号
    /// </summary>
    public enum ENUMSamplerID
    {
        QBH,
        HB
    }

    /// <summary>
    /// 组分收集器的型号
    /// </summary>
    public enum ENUMCollectorID
    {
        QBH_DLY,
        HB_DLY_W,
        HB_DLY_B
    }

    /// <summary>
    /// 阀的型号
    /// </summary>
    public enum ENUMValveID
    {
        QBH_Coll6,
        QBH_Coll8,
        QBH_Coll12,
        HB_Coll6,
        HB_Coll8,
        HB_Coll10,
        HB_Coll12,
        HB2,
        HB_T2,
        HB_T4,
        HB_GS4,
        VICI4_413,
        VICI4_342,
        VICI6_AB,
        VICI4,
        VICI6,
        VICI8,
        VICI10,
        VICI_T4,
        VICI_T6,
        VICI_T8,
        VICI_T10
    }

    /// <summary>
    /// 泵的型号
    /// </summary>
    public enum ENUMPumpID
    {
        NP7001,
        NP7005,
        NP7010,
        NP7030,
        NP7060,
        P1001L,
        P1003L,
        OEM0025,
        OEM0030,
        OEM0100,
        OEM0300,
        WatsonMarlow
    }

    /// <summary>
    /// 检测器的型号
    /// </summary>
    public enum ENUMDetectorID
    {
        ASABD05,
        ASABD06,
        pHHamilton,
        CdHamilton,
        pHCdOEM,
        pHCdHamilton,
        UVQBH2,
        UVECOM4,
        RIShodex
    }

    /// <summary>
    /// 其它的型号
    /// </summary>
    public enum ENUMOtherID
    {
        Mixer,
        ValveMixer
    }


    public enum ENUMInstrumentType
    {
        Sampler,
        Valve,
        Pump,
        Detector,
        Collector,
        Other
    }
}
