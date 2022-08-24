using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class InstrumentSize
    {
        public int MWidth { get; set; }
        public int MHeight { get; set; }

        public InstrumentSize(int width = 700, int height = 250)
        {
            MWidth = width;
            MHeight = height;
        }
    }
}
