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
    public enum RIState
    {
        Free = 0,       //空闲
        Version,
        ReadFirst,
        ReadWrite,      //写读
        Abort
    }
}
