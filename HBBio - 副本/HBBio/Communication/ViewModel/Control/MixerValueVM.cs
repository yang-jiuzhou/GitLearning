using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class MixerValueVM
    {
        public MixerValue MItem { get; set; }

        public bool MOnoff
        {
            get
            {
                return MItem.MOnoff;
            }
            set
            {
                MItem.MOnoff = value;
            }
        }
    }
}
