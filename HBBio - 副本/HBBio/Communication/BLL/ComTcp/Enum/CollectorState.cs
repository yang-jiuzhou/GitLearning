using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    /// <summary>
    /// 组分收集器的状态枚举
    /// </summary>
    public enum CollectorState
    {
        Free,           //空闲
        FreeFirst,
        Version,        //版本
        ReadWrite,
        ReadFirst,
        Start,
        Stop,
        Abort           //结束
    }
}
