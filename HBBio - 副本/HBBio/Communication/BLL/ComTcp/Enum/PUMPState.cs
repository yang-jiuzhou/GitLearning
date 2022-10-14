using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    /// <summary>
    /// 泵的状态枚举
    /// </summary>
    public enum PUMPState
    {
        Free = 0,       //空闲
        Version,        //版本号
        Start,
        ReadWrite,      //读写
        MaxPress,       //最大压力
        MinPress,       //最小压力
        Zero,           //清零
        Abort           //终止
    }
}
