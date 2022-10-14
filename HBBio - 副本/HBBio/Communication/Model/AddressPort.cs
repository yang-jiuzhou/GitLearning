using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class AddressPort
    {
        public string MAddress { get; set; }
        public string MPort { get; set; }


        public AddressPort Clone()
        {
            AddressPort item = new AddressPort();
            item.MAddress = MAddress;
            item.MPort = MPort;
            return item;
        }

        public static string[] GetAddressNames()
        {
            return new string[] { "192.168.1.253", "192.168.1.254" };
        }

        public static string[] GetPortNames()
        {
            return new string[] { "1030", "1031", "1032", "1033", "1034", "1035", "1036", "1037" };
        }
    }
}
