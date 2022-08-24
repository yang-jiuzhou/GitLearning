using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    /// <summary>
    /// 紫外的状态枚举
    /// </summary>
    public enum UVState
    {
        Free,           //空闲
        Version,        //版本
        Read,           //读值
        ReadFirst,      //初始读值
        Abort           //结束
    }
}
