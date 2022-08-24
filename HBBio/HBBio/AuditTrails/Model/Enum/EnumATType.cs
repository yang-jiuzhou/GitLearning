using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.AuditTrails
{
    /// <summary>
    /// 日志行类别枚举
    /// </summary>
    public enum EnumATType
    {
        All,
        System,
        Error,
        AlarmWarning,
        ColumnList,
        Operate,
        SignerReviewer,
        Method,
        Manual,
        Coll,
        Marker
    }
}
