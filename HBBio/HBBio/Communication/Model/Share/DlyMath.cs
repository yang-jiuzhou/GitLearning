using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class CRC
    {
        /// <summary>
        /// CRC校验计算
        /// </summary>
        /// <param name="mCommand"></param>
        /// <returns></returns>
        public static byte[] Cal12(byte[] mCommand)
        {
            //求和
            int mSum = 0;
            for (int i = 0; i < 12; i++)
            {
                mSum = mSum + mCommand[i];
            }

            //取余
            mSum %= 256;

            //得到CRC[3]
            return Encoding.ASCII.GetBytes((1000 + mSum).ToString().Remove(0, 1));
        }

        /// <summary>
        /// CRC校验计算
        /// </summary>
        /// <param name="data"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static byte[] CRCLen(byte[] data, int len)
        {
            if (len > 0)
            {
                ushort crc = 0xFFFF;
                for (int i = 0; i < len; i++)
                {
                    crc = (ushort)(crc ^ data[i]);
                    for (int j = 0; j < 8; j++)
                    {
                        ushort tmp = (ushort)(crc & 0x0001);
                        crc >>= 1;
                        if (tmp > 0)
                        {
                            crc = (ushort)(crc ^ 0xA001);
                        }
                    }
                }
                byte hi = (byte)(crc & 0xFF);
                byte lo = (byte)(crc >> 8);

                return new byte[] { hi, lo };
            }

            return new byte[] { 0, 0 };
        }
    }
}
