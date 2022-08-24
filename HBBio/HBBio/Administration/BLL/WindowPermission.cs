using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Administration
{
    /// <summary>
    /// 接口，设置具体权限可用
    /// </summary>
    interface WindowPermission
    {
        bool SetPermission(PermissionInfo info);
    }
}
