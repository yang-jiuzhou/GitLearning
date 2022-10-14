using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    /// <summary>
    /// 气泡传感器的状态枚举
    /// </summary>
    public enum ASState
    {
        Free,           //空闲
        Version,        //版本
        First,
        Read,           //读值
        Abort           //结束
    }
}
