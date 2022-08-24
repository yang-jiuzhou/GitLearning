using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class AlarmWarningVM
    {
        public AlarmWarning MItem { get; set; }

        public List<AlarmWarningItemVM> MList { get; set; }

        public AlarmWarningVM(AlarmWarning item)
        {
            MItem = item;

            MList = new List<AlarmWarningItemVM>();
            foreach (var it in MItem.MList)
            {
                MList.Add(new AlarmWarningItemVM(it));
            }
        }
    }
}
