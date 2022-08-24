using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    /// <summary>
    /// 状态枚举
    /// </summary>
    public enum EnumValveMixerState
    {
        Free = 0,       //空闲
        Version,
        First,
        ReadWrite,      //写读
        Abort
    }
}
