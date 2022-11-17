using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    /// <summary>
    /// 系统运行状态
    /// </summary>
    public enum SystemState
    {
        Free,       	//空闲
        Wash,           //清洗
        Manual,         //手动
        Method,         //自动
        FreeToManual,
        FreeToMethod,
        FreeToManualBreak,
        FreeToMethodBreak,
        Stop
    }
}
