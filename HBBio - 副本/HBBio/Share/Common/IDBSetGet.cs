using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Share
{
    interface IDBSetGet
    {
        string GetDBInfo(string split);
        void SetDBInfo(string split, string infoStr);
    }
}
