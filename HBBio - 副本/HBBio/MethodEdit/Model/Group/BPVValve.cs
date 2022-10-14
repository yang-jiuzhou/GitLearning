﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    [Serializable]
    public class BPVValve : BaseGroup
    {
        public int MBPV { get; set; }

        public BPVValve()
        {
            MType = EnumGroupType.BPV;

            MBPV = 0;
        }
    }
}
