using HBBio.Communication;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Manual
{
    [Serializable]
    public class AlarmWarningValve
    {
        public bool m_update = false;

        public AlarmWarning m_alarmWarning = new AlarmWarning();


        /// <summary>
        /// 清除临时变量
        /// </summary>
        public void Clear()
        {
            m_alarmWarning.MList.Clear();
        }
    }
}
