﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    [Serializable]
    public class Mixer : BaseGroup
    {
        public bool MOnoff { get; set; }

        public Mixer()
        {
            MType = EnumGroupType.Mixer;

            MOnoff = false;
        }

        public override bool Compare(BaseGroup baseItem)
        {
            Mixer item = (Mixer)baseItem;
            return MOnoff == item.MOnoff;
        }
    }
}
