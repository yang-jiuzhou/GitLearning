using Aladdin.HASP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.PassDog
{
    class CSentinel
    {
        /// <summary>
        /// 供应商代码，主锁代码
        /// </summary>
        private static string vendorCode =
            "zUY8T7N/OBWgdbHbhST3EJmKqJT3+h5zHBdcX+Wl9ixwZVWfOMTUDoSuI3A6uKPVcQaDRLErUbxEPU1b" +
            "3BdQzRuU0dfVM7b133jwdlFvCX8PD1Ao9y++RMpVXK+kr05hQW/40glzfiyL69XOTXOCQ51PMBqOdQvE" +
            "6hH7jwhHNwlouxSLNT0jVv2DXyK7orNAhMjSQf3wuCx12Z+2XsVhZ88YnZ5qShx050O0tzpvDzZ9SJLk" +
            "FhRCk9ZavzEkG5axDfEzC11U3BQ4Lq+8xTFjX6yd94OXiLKfk/8dHHcG2xzzXdddDiW4IFhsp4V6iRfi" +
            "tAnnkNPqENwRWXQMBAbHX0V9HwOKASwcI22zzMbyAS6NgVZLTb7BN3BeDA/M3O6tpa5aNxSR7rgQRTsq" +
            "+rMuSCY+vcsODARapPi6DIEdAHbPj1V9f/JKj9WmwGFPAPj0MpuSU0BLT/xpeuXJO+rrAqkZubHadt2+" +
            "taJs72rd7Gy9tkKwhDnODilnZt7l58yN7nWUe365LbBIANE4Pugn0TZTuAWhAWkV5jh6ILOk2t3HRffw" +
            "wv1NiX4rxUytmzex66Co4Yo3xyBa+2wJ7CSUrkOUOvNZfHhZh7zByOH/DWreJKFE7GulnMhm+LnwKpyA" +
            "WWRkqbE3lE0cNMDXGb6nNTItGPbOrryKtaT63HP9qFHfGyMEMUyCN22oAfS3YmTz8wRbDbXkrinoSbOK" +
            "rIZ6tjqTW56RStwnJPkFIRksob5qseXt6y0B6PBEQKsnoSPnWDsZmJG1v5oSzFcMjEU2ZCElIx3AL25A" +
            "FtckN2StxW9DrXYAk9eF9xWtgPuMA7opYRu6hTQVXTRFuJe4+cW3HnOxQK86Y2KrRLHBybq5k8nA0zK4" +
            "ZNP+Y7qD8yLhq4hU7OOnw1+IIatjYZsghOJDzh3snr33ZHSimnSwwEUPZ3IcHUCCfi/a8FE9lmZCh3co" +
            "1WpeVDv1+y+P7v3QYtQGhQ==";

        /// <summary>
        /// 加密狗API
        /// </summary>
        private static Hasp hasp = new Hasp(HaspFeature.FromFeature(0));

        //------------------加密狗 内存 读写函数-----------------------------//

        /// <summary>
        /// 读加密狗信息
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        public static bool ReadInfo(ref string Info, ref string errorInfo)
        {
            //1.登录
            HaspStatus status = hasp.Login(vendorCode);
            if (HaspStatus.StatusOk == status)
            {
                //登录成功
            }
            else
            {
                errorInfo = RealXmlPassDog.S_LoginFail + "," + status.ToString();
                return false;
            }
            //2.读信息
            string info = null;
            status = hasp.GetSessionInfo(Hasp.KeyInfo, ref info);
            if (HaspStatus.StatusOk == status)
            {
                //读信息成功
                Info = info;
            }
            else
            {
                errorInfo = RealXmlPassDog.S_ReadFail + "," + status.ToString();
                return false;
            }
            //3.退出
            //退出
            status = hasp.Logout();
            if (HaspStatus.StatusOk != status)
            {
                errorInfo = RealXmlPassDog.S_ExitFail + "," + status.ToString();
                return false;
            }
            else
            {
                //退出成功
            }
            return true;
        }

        /// <summary>
        /// 加密狗 SN 对比，上位机SN 和 加密狗内存中 对比
        /// </summary>
        /// <param name="Memery"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public static bool CompareMemery(string MemeryFromSoft, ref string errorInfo)
        {
            string Memery = "";//读内存字符串
            //1.登录
            HaspStatus status = hasp.Login(vendorCode);
            if (HaspStatus.StatusOk == status)
            {
                //登录成功
            }
            else
            {
                errorInfo = RealXmlPassDog.S_LoginFail + "," + status.ToString();
                return false;
            }
            //2.读内存字符串
            HaspFile file = hasp.GetFile(HaspFileId.ReadWrite);
            string str1 = "";
            status = file.Read(ref str1);
            if (HaspStatus.StatusOk == status)
            {
                //读成功
                Memery = str1;
            }
            else
            {
                errorInfo = RealXmlPassDog.S_MemoryFail + "," + status.ToString();
                return false;
            }
            //3.退出
            //退出
            status = hasp.Logout();
            if (HaspStatus.StatusOk != status)
            {
                errorInfo = RealXmlPassDog.S_ExitFail + "," + status.ToString();
                return false;
            }
            else
            {
                //退出成功
            }
            //4.对比字符串
            string mMemeryFromSoft = "";//存在可能读不到第一个字符
            if (MemeryFromSoft.Length > 1)
            {
                mMemeryFromSoft = MemeryFromSoft.Substring(1);
            }
            else
            {
                mMemeryFromSoft = MemeryFromSoft;
            }
            Memery = Memery.Replace("\0", "");//去掉“\0”
            if (MemeryFromSoft.Equals(Memery) || mMemeryFromSoft.Equals(Memery))
            {
                //对比成功
            }
            else
            {
                errorInfo = RealXmlPassDog.S_SNFail;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 从数据库读取SN
        /// </summary>
        /// <param name="dogSN"></param>
        /// <returns></returns>
        public static string GetDogInfo(ref PassDogValue dogSN)
        {
            PassDogTable table = new PassDogTable();
            return table.GetRow(ref dogSN);
        }

        /// <summary>
        /// 写SN到数据库
        /// </summary>
        /// <param name="dogSN"></param>
        /// <returns></returns>
        public static string SetDogInfo(PassDogValue dogSN)
        {
            PassDogTable table = new PassDogTable();
            return table.UpdateRow(dogSN);
        }
    }
}
