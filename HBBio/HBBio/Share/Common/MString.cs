using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Share
{
    public class MString
    {
        public string MName { get; set; }

        public MString(string name)
        {
            MName = name;
        }

        public static List<MString> GetList(List<string> list)
        {
            List<MString> tmp = new List<MString>();
            foreach (var it in list)
            {
                tmp.Add(new MString(it));
            }

            return tmp;
        }

        public static List<MString> GetList(string[] arr)
        {
            List<MString> tmp = new List<MString>();
            foreach (var it in arr)
            {
                tmp.Add(new MString(it));
            }

            return tmp;
        }
    }
}
