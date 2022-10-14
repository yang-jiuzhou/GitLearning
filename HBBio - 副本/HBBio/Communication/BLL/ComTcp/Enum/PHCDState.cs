using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    /// <summary>
    /// 阀的状态枚举
    /// </summary>
    public enum PHCDState
    {
        Free = 0,       //空闲
        Version,
        Read,
        WritePH,
        WriteCD,
        Abort
    }
}
