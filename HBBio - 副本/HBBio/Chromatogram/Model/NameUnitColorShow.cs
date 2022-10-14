using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Chromatogram
{
    public class NameUnitColorShow
    {
        public string MName { get; set; }
        public string MUnit { get; set; }
        public System.Drawing.Color MColor { get; set; }
        public bool MShow { get; set; }

        public NameUnitColorShow(string name, string unit, System.Drawing.Color color, bool show)
        {
            MName = name;
            MUnit = unit;
            MColor = color;
            MShow = show;
        }
    }
}
