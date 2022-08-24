using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.SystemControl
{
    public class RunData
    {
        public List<RunDataItem> MList { get; set; }


        public RunData()
        {
            MList = new List<RunDataItem>();
        }
    }

    public class RunDataItem : DlyNotifyPropertyChanged
    {
        public string MName { get; set; }
        private bool m_show = true;
        public bool MShow
        {
            get
            {
                return m_show;
            }
            set
            {
                m_show = value;
                OnPropertyChanged("MShow");
            }
        }
    }
}
