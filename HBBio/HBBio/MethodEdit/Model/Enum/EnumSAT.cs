using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    /// <summary>
    /// 上样方式枚举
    /// </summary>
    public enum EnumSAT
    {
        ManualLoopFilling,          //手动循环填充
        SamplePumpLoopFilling,      //上样泵循环填充
        ISDOC                       //直接注入
    }
}
