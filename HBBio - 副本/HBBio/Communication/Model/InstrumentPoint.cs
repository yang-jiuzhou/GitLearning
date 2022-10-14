using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.Communication
{
    public class InstrumentPoint
    {
        public string MName { get; set; }
        public Point MPt1 { get; set; }
        public Point MPt2 { get; set; }
        public bool MIsHV { get; set; }
        public EnumLineType MType { get; set; }

        public InstrumentPoint(string name, Point pt1, Point pt2, bool isHV, EnumLineType type)
        {
            MName = name;
            MPt1 = pt1;
            MPt2 = pt2;
            MIsHV = isHV;
            MType = type;
        }
    }

    public enum EnumLineType
    {
        All,
        ABCD,
        S,
        A,
        B,
        C,
        D,
        BPV
    }
}
