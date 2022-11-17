using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    /**
     * ClassName: WashSystem
     * Description: 清洗单元集合
     * Version: 1.0
     * Create:  2021/04/21
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class WashSystem
    {
        private List<WashItem> MList { get; set; }
        public List<EnumWashStatus> MListStatus { get; set; }
        private int MIJV { get; set; }
        private int MBPV { get; set; }
        private int MCPV { get; set; }


        private static class WashSystemInner
        {
            public static WashSystem _stance = new WashSystem();
        }

        /// <summary>
        /// 单例引用
        /// </summary>
        /// <returns></returns>
        public static WashSystem GetInstance()
        {
            return WashSystemInner._stance;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        private WashSystem()
        {
            MList = new List<WashItem>();
            MListStatus = new List<EnumWashStatus>();
            MIJV = -1;
            MBPV = -1;
            MCPV = -1;

            foreach (var it in Enum.GetNames(typeof(ENUMPumpName)))
            {
                MList.Add(new WashItem());
                MListStatus.Add(EnumWashStatus.No);
            }
        }

        /// <summary>
        /// 开始清洗
        /// </summary>
        /// <param name="comConf"></param>
        /// <param name="index"></param>
        public void Start(ComConfStatic comConf, ENUMPumpName index, int wash)
        {
            CheckValve(comConf, index, wash);

            MList[(int)index].Start(comConf, index);
        }

        public void StartAll(ComConfStatic comConf, int wash)
        {
            CheckValve(comConf, ENUMPumpName.FITA, wash);

            for (int i = 1; i < MList.Count; i++)
            {
                MList[i].Start(comConf, (ENUMPumpName)i);
            }
        }

        private void CheckValve(ComConfStatic comConf, ENUMPumpName index, int wash)
        {
            //先判断是哪种进样阀
            if (4 == EnumIJVInfo.Count)
            {
                if (-1 == MIJV)
                {
                    switch (wash)
                    {
                        case 1:
                            //清洗系统
                            switch (index)
                            {
                                case ENUMPumpName.FITS:
                                    {
                                        int tmp = comConf.GetValveSet(ENUMValveName.IJV);
                                        if (2 != tmp)
                                        {
                                            MIJV = tmp;
                                            comConf.SetValve(ENUMValveName.IJV, 2);
                                        }
                                    }
                                    break;
                                default:
                                    {
                                        int tmp = comConf.GetValveSet(ENUMValveName.IJV);
                                        if (0 != tmp && 3 != tmp)
                                        {
                                            MIJV = tmp;
                                            comConf.SetValve(ENUMValveName.IJV, 0);
                                        }
                                    }
                                    break;
                            }
                            break;
                        case 2:
                            //清洗泵
                            switch (index)
                            {
                                case ENUMPumpName.FITS:
                                    {
                                        int tmp = comConf.GetValveSet(ENUMValveName.IJV);
                                        if (1 != tmp)
                                        {
                                            MIJV = tmp;
                                            comConf.SetValve(ENUMValveName.IJV, 1);
                                        }
                                    }
                                    break;
                                default:
                                    {
                                        int tmp = comConf.GetValveSet(ENUMValveName.IJV);
                                        if (2 != tmp)
                                        {
                                            MIJV = tmp;
                                            comConf.SetValve(ENUMValveName.IJV, 2);
                                        }
                                    }
                                    break;
                            }
                            break;
                    }
                }
                if (-1 == MBPV)
                {
                    switch (wash)
                    {
                        case 1:
                            //清洗系统
                            MBPV = comConf.GetValveSet(ENUMValveName.BPV);
                            comConf.SetValve(ENUMValveName.BPV, 0);
                            break;
                    }
                }
            }
            else
            {
                if (-1 == MBPV)
                {
                    switch (wash)
                    {
                        case 1:
                            //清洗系统
                            MBPV = comConf.GetValveSet(ENUMValveName.BPV);
                            comConf.SetValve(ENUMValveName.BPV, 0);
                            break;
                        case 2:
                            //清洗泵
                            MBPV = comConf.GetValveSet(ENUMValveName.BPV);
                            comConf.SetValve(ENUMValveName.BPV, 1);
                            MCPV = comConf.GetValveSet(ENUMValveName.CPV_1);
                            comConf.SetValve(ENUMValveName.CPV_1, 0);
                            break;
                    }
                }
            }
        }

        public void Stop(ComConfStatic comConf, ENUMPumpName index)
        {
            MList[(int)index].Stop(comConf, index);
        }

        public EnumWashStatus Update(ComConfStatic comConf)
        {
            for (int i = 0; i < MList.Count; i++)
            {
                MListStatus[i] = MList[i].Update(comConf, (ENUMPumpName)i);
            }

            foreach (var it in MListStatus)
            {
                if (EnumWashStatus.No != it)
                {
                    foreach (var it2 in MListStatus)
                    {
                        if (EnumWashStatus.No != it2 && EnumWashStatus.Over != it2)
                        {
                            return EnumWashStatus.Ing;
                        }
                    }

                    foreach (var it3 in MList)
                    {
                        it3.Clear();
                    }

                    if (-1 != MIJV)
                    {
                        comConf.SetValve(ENUMValveName.IJV, MIJV);
                        MIJV = -1;
                    }

                    if (-1 != MBPV)
                    {
                        comConf.SetValve(ENUMValveName.BPV, MBPV);
                        MBPV = -1;
                        if (-1 != MCPV)
                        {
                            comConf.SetValve(ENUMValveName.CPV_1, MCPV);
                            MCPV = -1;
                        }
                    }

                    return EnumWashStatus.Over;
                }
            }
            return EnumWashStatus.No;
        }
    }
}
