using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Share
{
    public class EnumString<EnumType>
    {
        public EnumType MEnum { get; set; }
        public string MString { get; set; }

        public static List<EnumString<EnumType>> GetEnumStringList(string beginStr = "")
        {
            List<EnumString<EnumType>> list = new List<EnumString<EnumType>>();
            foreach (EnumType it in Enum.GetValues(typeof(EnumType)))
            {
                EnumString<EnumType> item = new EnumString<EnumType>();
                item.MEnum = it;
                item.MString = (string)System.Windows.Application.Current.Resources[beginStr + it.ToString()];
                list.Add(item);
            }

            return list;
        }
    }
}
