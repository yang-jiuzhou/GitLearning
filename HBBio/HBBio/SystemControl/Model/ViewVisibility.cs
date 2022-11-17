using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.SystemControl
{
    public class ViewVisibility
    {
        public bool MToolBar { get; set; }
        public bool MCommunication { get; set; }
        public bool MInstrumentParameters { get; set; }
        public bool MAdministration { get; set; }
        public bool MColumnHandling { get; set; }
        public bool MTubeStand { get; set; }
        public bool MManual { get; set; }
        public bool MProject { get; set; }
        public bool MAuditTrails { get; set; }
        public bool MSystemMonitor { get; set; }
        public bool MDB { get; set; }
        public bool MRunData { get; set; }
        public bool MChromatogram { get; set; }
        public bool MProcessPicture { get; set; }
        public bool MMonitor { get; set; }
        public bool MStatusBar { get; set; }

        private static class ViewVisibilityInner
        {
            public static ViewVisibility _stance = new ViewVisibility();
        }

        /// <summary>
        /// 单例引用
        /// </summary>
        /// <returns></returns>
        public static ViewVisibility GetInstance()
        {
            return ViewVisibilityInner._stance;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        private ViewVisibility()
        {
            MToolBar = true;
            MCommunication = true;
            MInstrumentParameters = true;
            MAdministration = true;
            MColumnHandling = true;
            MTubeStand = true;
            MManual = true;
            MProject = true;
            MAuditTrails = true;
            MSystemMonitor = true;
            MDB = true;
            MRunData = true;
            MChromatogram = true;
            MProcessPicture = true;
            MMonitor = true;
            MStatusBar = true;
        }
    }
}
