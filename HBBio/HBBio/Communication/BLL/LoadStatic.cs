﻿using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.Communication
{
    public enum ENUMSamplerName
    {
        Sampler01
    }

    public enum ENUMASName
    {
        AS01,
        AS02,
        AS03,
        AS04
    }

    public enum ENUMValveName
    {
        InS,
        InA,
        InB,
        InC,
        InD,
        IJV,
        BPV,
        CPV_1,
        CPV_2,
        Out
    }

    public enum ENUMPumpName
    {
        FITS,
        FITA,
        FITB,
        FITC,
        FITD
    }

    public enum ENUMPTName
    {
        PTS,
        PTA,
        PTB,
        PTC,
        PTD,
        PTTotal,
        PTColumnFront,
        PTColumnBack
    }

    public enum ENUMMixerName
    {
        Mixer01
    }

    public enum ENUMPHName
    {
        pH01,
        pH02
    }

    public enum ENUMCDName
    {
        Cd01,
        Cd02
    }

    public enum ENUMTTName
    {
        TT01,
        TT02,
        TT03,
        TT04
    }

    public enum ENUMUVName
    {
        UV01
    }

    public enum ENUMRIName
    {
        RI01
    }

    public enum ENUMCollectorName
    {
        Collector01
    }

    
    /**
     * ClassName: ItemVisibility
     * Description: 所有支持的通信设备集合
     * Version: 1.0
     * Create:  2020/11/12
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    public static class ItemVisibility
    {
        public static readonly Dictionary<ENUMSamplerName, Visibility> s_listSampler = new Dictionary<ENUMSamplerName, Visibility>();
        public static readonly Dictionary<ENUMASName, Visibility> s_listAS = new Dictionary<ENUMASName, Visibility>();
        public static readonly Dictionary<ENUMValveName, Visibility> s_listValve = new Dictionary<ENUMValveName, Visibility>();
        public static readonly Dictionary<ENUMPumpName, Visibility> s_listPump = new Dictionary<ENUMPumpName, Visibility>();
        public static readonly Dictionary<ENUMPTName, Visibility> s_listPT = new Dictionary<ENUMPTName, Visibility>();
        public static readonly Dictionary<ENUMMixerName, Visibility> s_listMixer = new Dictionary<ENUMMixerName, Visibility>();
        public static readonly Dictionary<ENUMPHName, Visibility> s_listPH = new Dictionary<ENUMPHName, Visibility>();
        public static readonly Dictionary<ENUMCDName, Visibility> s_listCD = new Dictionary<ENUMCDName, Visibility>();
        public static readonly Dictionary<ENUMTTName, Visibility> s_listTEMP = new Dictionary<ENUMTTName, Visibility>();
        public static readonly Dictionary<ENUMUVName, Visibility> s_listUV = new Dictionary<ENUMUVName, Visibility>();
        public static readonly Dictionary<ENUMRIName, Visibility> s_listRI = new Dictionary<ENUMRIName, Visibility>();
        public static readonly Dictionary<ENUMCollectorName, Visibility> s_listCollector = new Dictionary<ENUMCollectorName, Visibility>();
        

        /// <summary>
        /// 构造函数
        /// </summary>
        static ItemVisibility()
        {
            for (int i = 0; i < Enum.GetNames(typeof(ENUMSamplerName)).GetLength(0); i++)
            {
                s_listSampler.Add((ENUMSamplerName)i, Visibility.Collapsed);
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMASName)).GetLength(0); i++)
            {
                s_listAS.Add((ENUMASName)i, Visibility.Collapsed);
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMValveName)).GetLength(0); i++)
            {
                s_listValve.Add((ENUMValveName)i, Visibility.Collapsed);
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMPumpName)).GetLength(0); i++)
            {
                s_listPump.Add((ENUMPumpName)i, Visibility.Collapsed);
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMPTName)).GetLength(0); i++)
            {
                s_listPT.Add((ENUMPTName)i, Visibility.Collapsed);
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMMixerName)).GetLength(0); i++)
            {
                s_listMixer.Add((ENUMMixerName)i, Visibility.Collapsed);
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMPHName)).GetLength(0); i++)
            {
                s_listPH.Add((ENUMPHName)i, Visibility.Collapsed);
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMCDName)).GetLength(0); i++)
            {
                s_listCD.Add((ENUMCDName)i, Visibility.Collapsed);
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMTTName)).GetLength(0); i++)
            {
                s_listTEMP.Add((ENUMTTName)i, Visibility.Collapsed);
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMUVName)).GetLength(0); i++)
            {
                s_listUV.Add((ENUMUVName)i, Visibility.Collapsed);
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMRIName)).GetLength(0); i++)
            {
                s_listRI.Add((ENUMRIName)i, Visibility.Collapsed);
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMCollectorName)).GetLength(0); i++)
            {
                s_listCollector.Add((ENUMCollectorName)i, Visibility.Collapsed);
            }
        }

        /// <summary>
        /// 重设状态
        /// </summary>
        /// <param name="visib"></param>
        public static void ResetAll(Visibility visib)
        {
            for (int i = 0; i < Enum.GetNames(typeof(ENUMSamplerName)).GetLength(0); i++)
            {
                s_listSampler[(ENUMSamplerName)i] = visib;
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMASName)).GetLength(0); i++)
            {
                s_listAS[(ENUMASName)i] = visib;
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMValveName)).GetLength(0); i++)
            {
                s_listValve[(ENUMValveName)i] = visib;
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMPumpName)).GetLength(0); i++)
            {
                s_listPump[(ENUMPumpName)i] = visib;
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMPTName)).GetLength(0); i++)
            {
                s_listPT[(ENUMPTName)i] = visib;
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMMixerName)).GetLength(0); i++)
            {
                s_listMixer[(ENUMMixerName)i] = visib;
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMPHName)).GetLength(0); i++)
            {
                s_listPH[(ENUMPHName)i] = visib;
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMCDName)).GetLength(0); i++)
            {
                s_listCD[(ENUMCDName)i] = visib;
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMTTName)).GetLength(0); i++)
            {
                s_listTEMP[(ENUMTTName)i] = visib;
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMUVName)).GetLength(0); i++)
            {
                s_listUV[(ENUMUVName)i] = visib;
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMRIName)).GetLength(0); i++)
            {
                s_listRI[(ENUMRIName)i] = visib;
            }
            for (int i = 0; i < Enum.GetNames(typeof(ENUMCollectorName)).GetLength(0); i++)
            {
                s_listCollector[(ENUMCollectorName)i] = visib;
            }
        }
    }

    public class EnumInSInfo
    {
        private static int s_Count = 1;
        public static int Count
        {
            get
            {
                return s_Count;
            }
        }

        private static string[] s_NameList = new string[1] { "null" };
        public static string[] NameList
        {
            get
            {
                return s_NameList;
            }
        }

        public static void Init(int count, bool off = false)
        {
            s_Count = count;
            s_NameList = new string[count];
            if (0 < count)
            {
                s_NameList[0] = "Buffer";
                for (int i = 1; i < count; i++)
                {
                    s_NameList[i] = "S" + i;
                }
                if (off)
                {
                    s_NameList[count - 1] = "Off";
                }
            }
        }
    }

    public static class EnumInAInfo
    {
        private static int s_Count = 1;
        public static int Count
        {
            get
            {
                return s_Count;
            }
        }

        private static string[] s_NameList = new string[1] { "null" };
        public static string[] NameList
        {
            get
            {
                return s_NameList;
            }
        }

        public static void Init(int count, bool off = false)
        {
            s_Count = count;
            s_NameList = new string[count];
            if (0 < count)
            {
                for (int i = 0; i < count; i++)
                {
                    s_NameList[i] = "A" + (i + 1);
                }
                if (off)
                {
                    s_NameList[count - 1] = "Off";
                }
            }
        }
    }

    public static class EnumInBInfo
    {
        private static int s_Count = 1;
        public static int Count
        {
            get
            {
                return s_Count;
            }
        }

        private static string[] s_NameList = new string[1] { "null" };
        public static string[] NameList
        {
            get
            {
                return s_NameList;
            }
        }

        public static void Init(int count, bool off = false)
        {
            s_Count = count;
            s_NameList = new string[count];
            if (0 < count)
            {
                for (int i = 0; i < count; i++)
                {
                    s_NameList[i] = "B" + (i + 1);
                }
                if (off)
                {
                    s_NameList[count - 1] = "Off";
                }
            }
        }
    }

    public static class EnumInCInfo
    {
        private static int s_Count = 1;
        public static int Count
        {
            get
            {
                return s_Count;
            }
        }

        private static string[] s_NameList = new string[1] { "null" };
        public static string[] NameList
        {
            get
            {
                return s_NameList;
            }
        }

        public static void Init(int count, bool off = false)
        {
            s_Count = count;
            s_NameList = new string[count];
            if (0 < count)
            {
                for (int i = 0; i < count; i++)
                {
                    s_NameList[i] = "C" + (i + 1);
                }
                if (off)
                {
                    s_NameList[count - 1] = "Off";
                }
            }
        }
    }

    public static class EnumInDInfo
    {
        private static int s_Count = 1;
        public static int Count
        {
            get
            {
                return s_Count;
            }
        }

        private static string[] s_NameList = new string[1] { "null" };
        public static string[] NameList
        {
            get
            {
                return s_NameList;
            }
        }

        public static void Init(int count, bool off = false)
        {
            s_Count = count;
            s_NameList = new string[count];
            if (0 < count)
            {
                for (int i = 0; i < count; i++)
                {
                    s_NameList[i] = "D" + (i + 1);
                }
                if (off)
                {
                    s_NameList[count - 1] = "Off";
                }
            }
        }
    }

    public static class EnumOutInfo
    {
        private static int s_Count = 1;
        public static int Count
        {
            get
            {
                return s_Count;
            }
        }

        private static string[] s_NameList = new string[1] { "null" };
        public static string[] NameList
        {
            get
            {
                return s_NameList;
            }
        }

        public static void Init(int count, bool off = false)
        {
            s_Count = count;
            s_NameList = new string[count];
            if (0 < count)
            {
                s_NameList[0] = "Waste";
                for (int i = 1; i < count; i++)
                {
                    s_NameList[i] = "Out" + (i + 1);
                }
                if (off)
                {
                    s_NameList[count - 1] = "Off";
                }
            }
        }
    }

    public static class EnumIJVInfo
    {
        private static int s_Count = 2;
        public static int Count
        {
            get
            {
                return s_Count;
            }
        }

        private static string[] s_NameList = new string[2] { "Load", "Inject" };
        public static string[] NameList
        {
            get
            {
                return s_NameList;
            }
        }
    }

    public static class EnumBPVInfo
    {
        private static int s_Count = 3;
        public static int Count
        {
            get
            {
                return s_Count;
            }
        }

        private static string[] s_NameList = new string[3] { "Bypass", "Forward", "Recoil" };
        public static string[] NameList
        {
            get
            {
                return s_NameList;
            }
        }
    }

    public static class EnumCPVInfo
    {
        private static int s_Count = 1;
        public static int Count
        {
            get
            {
                return s_Count;
            }
        }

        private static string[] s_NameList = new string[1] { "null" };
        public static string[] NameList
        {
            get
            {
                return s_NameList;
            }
        }

        public static void Init(int count)
        {
            s_Count = count;
            s_NameList = new string[count];
            if (0 < count)
            {
                s_NameList[0] = "Bypass";
                for (int i = 1; i < count; i++)
                {
                    s_NameList[i] = "Position" + (i + 1);
                }
            }
        }
    }

    public static class EnumCollectorInfo
    {
        private static int s_Count = 1;
        public static int Count
        {
            get
            {
                return s_Count;
            }
        }

        private static int s_CountL = 1;
        public static int CountL
        {
            get
            {
                return s_CountL;
            }
        }

        private static int s_CountR = 0;
        public static int CountR
        {
            get
            {
                return s_CountR;
            }
        }

        private static List<string> s_NameList = new List<string>() { "null" };
        public static List<string> NameList
        {
            get
            {
                return s_NameList;
            }
        }

        public static double s_BottleL = 0;   //试管体积
        public static double s_BottleR = 0;   //试管体积
        public static double s_btjL = 999999.99;   //试管体积
        public static double s_btjR = 999999.99;   //试管体积

        public static void Init(int countL, int countR)
        {
            s_CountL = countL;
            s_CountR = countR;
            s_Count = countL + countR;
            s_NameList.Clear();
            for (int i = 0; i < countL; i++)
            {
                s_NameList.Add("L" + (i + 1));
            }
            for (int i = 0; i < countR; i++)
            {
                s_NameList.Add("R" + (i + 1));
            }
            if (0 == s_NameList.Count)
            {
                s_Count = 1;
                s_NameList.Add("null");
            }
        }
        public static void Init(int count)
        {
            s_CountL = 0;
            s_CountR = 0;
            s_Count = count - 1;
            s_NameList.Clear();
            for (int i = 0; i < count - 1; i++)
            {
                s_NameList.Add("Out" + (i + 1));
            }
            if (0 == s_NameList.Count)
            {
                s_Count = 1;
                s_NameList.Add("null");
            }
        }

        public static void SetBottleCollVol(double volL, double volR)
        {
            s_BottleL = volL;
            s_BottleR = volR;
        }
        public static void ReSetBottleCollVol()
        {
            TubeStand.TubeStandManager manager = new TubeStand.TubeStandManager();
            TubeStand.TubeStandItem item = new TubeStand.TubeStandItem();
            manager.GetItem(s_BottleL, s_CountL, item);
            s_btjL = item.MCollVolume;
            manager.GetItem(s_BottleR, s_CountR, item);
            s_btjR = item.MCollVolume;
        }
    }

    public static class EnumMonitorInfo
    {
        private static int s_Count = 0;
        public static int Count
        {
            get
            {
                return s_Count;
            }
        }

        private static string[] s_NameList = new string[0];
        public static string[] NameList
        {
            get
            {
                return s_NameList;
            }
        }

        private static List<double> s_ValueList = new List<double>();
        public static List<double> ValueList
        {
            get
            {
                return s_ValueList;
            }
        }

        private static List<double> s_SlopeList = new List<double>();
        public static List<double> SlopeList
        {
            get
            {
                return s_SlopeList;
            }
        }

        private static List<Queue<double>> s_ArrList = new List<Queue<double>>();
        private static List<Queue<double>> ArList
        {
            get
            {
                return s_ArrList;
            }
        }

        public static void Init(List<ConfpHCdUV> list)
        {
            s_Count = list.Count;
            s_NameList = new string[list.Count];
            if (0 < list.Count)
            {
                s_ValueList.Clear();
                s_SlopeList.Clear();
                s_ArrList.Clear();
                for (int i = 0; i < list.Count; i++)
                {
                    s_NameList[i] = list[i].MName;
                    s_ValueList.Add(0);
                    s_SlopeList.Add(0);
                    s_ArrList.Add(new Queue<double>());
                }
            }
        }

        public static void SetValue(int index, double val)
        {
            ValueList[index] = val;
            ArList[index].Enqueue(val);
            if (9 < ArList[index].Count)
            {
                ArList[index].Dequeue();
            }
            s_SlopeList[index] = LeastSquaresFitLine(ArList[index]);
        }

        /// <summary>
        /// 计算斜率
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static double LeastSquaresFitLine(Queue<double> list)
        {
            int count = list.Count;

            if (count < 2)
            {
                return 0;
            }
            else
            {
                double Xmean = 0;
                double Ymean = 0;
                double X_X = 0;
                double X_Y = 0;
                double sumX = 0;
                double sumY = 0;
                double x = 0;
                foreach(var it in list)
                {
                    double px = x;
                    double py = it;
                    sumX += x;
                    sumY += it;
                    X_X += px * px;
                    X_Y += px * py;
                    x += 0.01;
                }
                Xmean = sumX / count;
                Ymean = sumY / count;
                return Math.Round((X_Y - count * Xmean * Ymean) / (X_X - count * Xmean * Xmean) / 100, 2);
            }
        }
    }

    /**
     * ClassName: StaticAlarmWarning
     * Description: 系统的默认警报警告参数
     * Version: 1.0
     * Create:  2021/04/21
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    public static class StaticAlarmWarning
    {
        private static AlarmWarning s_alarmWarningOriginal = new AlarmWarning();
        public static AlarmWarning SAlarmWarningOriginal
        {
            get
            {
                return s_alarmWarningOriginal;
            }
        }

        private static AlarmWarning s_alarmWarning = new AlarmWarning();
        public static AlarmWarning SAlarmWarning
        {
            get
            {
                return s_alarmWarning;
            }
            set
            {
                lock (s_alarmWarning)
                {
                    foreach (var it in value.MList)
                    {
                        foreach (var itM in s_alarmWarning.MList)
                        {
                            if (it.MName.Equals(itM.MName))
                            {
                                itM.SetValue(it);
                                break;
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="alarmWarning"></param>
        public static void Init(AlarmWarning alarmWarning)
        {
            s_alarmWarningOriginal = alarmWarning;

            lock (s_alarmWarning)
            {
                s_alarmWarning.MList.Clear();
                foreach (var it in s_alarmWarningOriginal.MList)
                {
                    s_alarmWarning.MList.Add(Share.DeepCopy.DeepCopyByXml(it));
                }
            }
        }

        /// <summary>
        /// 还原警报警告参数
        /// </summary>
        public static void Reset()
        {
            lock (s_alarmWarning)
            {
                for (int i = 0; i < s_alarmWarningOriginal.MList.Count; i++)
                {
                    s_alarmWarning.MList[i].MValLL = s_alarmWarningOriginal.MList[i].MValLL;
                    s_alarmWarning.MList[i].MValL = s_alarmWarningOriginal.MList[i].MValL;
                    s_alarmWarning.MList[i].MValH = s_alarmWarningOriginal.MList[i].MValH;
                    s_alarmWarning.MList[i].MValHH = s_alarmWarningOriginal.MList[i].MValHH;

                    s_alarmWarning.MList[i].MCheckLL = EnumAlarmWarningMode.Enabled;
                    s_alarmWarning.MList[i].MCheckL = EnumAlarmWarningMode.Enabled;
                    s_alarmWarning.MList[i].MCheckH = EnumAlarmWarningMode.Enabled;
                    s_alarmWarning.MList[i].MCheckHH = EnumAlarmWarningMode.Enabled;
                }
            }
        }
    }

    /**
     * ClassName: StaticSystemConfig
     * Description: 系统的默认基本参数
     * Version: 1.0
     * Create:  2021/04/22
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    public static class StaticSystemConfig
    {
        private static SystemConfig s_systemConfig = new SystemConfig();
        public static SystemConfig SSystemConfig
        {
            get
            {
                return s_systemConfig;
            }
        }


        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="conf"></param>
        public static void Init(SystemConfig conf)
        {
            s_systemConfig = conf;

            EnumCollectorInfo.Init(conf.MConfCollector.MCountL, conf.MConfCollector.MCountR);
            EnumCollectorInfo.SetBottleCollVol(conf.MConfCollector.MVolL, conf.MConfCollector.MVolR);
            EnumCollectorInfo.ReSetBottleCollVol();
            EnumMonitorInfo.Init(conf.MListConfpHCdUV);
        }
    }

    /**
     * ClassName: StaticValue
     * Description: 系统加载之后确定的公共变量
     * Version: 1.0
     * Create:  2021/04/22
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    public static class StaticValue
    {
        private static double s_volToLen = 1;
        public static double SVolToLen
        {
            get
            {
                return s_volToLen;
            }
            set
            {
                s_volToLen = value;
                s_lenToVol = 1 / value;
                s_maxFlowLen = Math.Round(s_maxFlowVol * s_volToLen, 2);
                s_maxFlowSLen = Math.Round(s_maxFlowSVol * s_volToLen, 2);
                s_maxFlowALen = Math.Round(s_maxFlowAVol * s_volToLen, 2);
                s_maxFlowBLen = Math.Round(s_maxFlowBVol * s_volToLen, 2);
                s_maxFlowCLen = Math.Round(s_maxFlowCVol * s_volToLen, 2);
                s_maxFlowDLen = Math.Round(s_maxFlowDVol * s_volToLen, 2);
            }
        }
        
        private static double s_lenToVol = 1;
        public static double SLenToVol
        {
            get
            {
                return s_lenToVol;
            }
        }

        public static double s_maxFlowVol = 100;
        public static string s_maxFlowVolUnit
        {
            get
            {
                return " " + DlyBase.SC_FITUNITML + "[0- " + s_maxFlowVol + "]";
            }
        }
        public static double s_maxFlowLen = 100;
        public static string s_maxFlowLenUnit
        {
            get
            {
                return " " + DlyBase.SC_LINEARFITUNIT + "[0- " + s_maxFlowLen + "]";
            }
        }
        
        public static double s_maxFlowSVol = 100;
        public static string s_maxFlowSVolUnit
        {
            get
            {
                return "(" + DlyBase.SC_FITUNITML + ")[0- " + s_maxFlowSVol + "]";
            }
        }
        public static double s_maxFlowSLen = 100;
        public static string s_maxFlowSLenUnit
        {
            get
            {
                return "(" + DlyBase.SC_LINEARFITUNIT + ")[0- " + s_maxFlowSLen + "]";
            }
        }
        
        public static double s_maxFlowAVol = 100;
        public static string s_maxFlowAVolUnit
        {
            get
            {
                return "(" + DlyBase.SC_FITUNITML + ")[0- " + s_maxFlowAVol + "]";
            }
        }
        public static double s_maxFlowALen = 100;
        public static string s_maxFlowALenUnit
        {
            get
            {
                return "(" + DlyBase.SC_LINEARFITUNIT + ")[0- " + s_maxFlowALen + "]";
            }
        }
        
        public static double s_maxFlowBVol = 100;
        public static string s_maxFlowBVolUnit
        {
            get
            {
                return "(" + DlyBase.SC_FITUNITML + ")[0- " + s_maxFlowBVol + "]";
            }
        }
        public static double s_maxFlowBLen = 100;
        public static string s_maxFlowBLenUnit
        {
            get
            {
                return "(" + DlyBase.SC_LINEARFITUNIT + ")[0- " + s_maxFlowBLen + "]";
            }
        }
        
        public static double s_maxFlowCVol = 100;
        public static string s_maxFlowCVolUnit
        {
            get
            {
                return "(" + DlyBase.SC_FITUNITML + ")[0- " + s_maxFlowCVol + "]";
            }
        }
        public static double s_maxFlowCLen = 100;
        public static string s_maxFlowCLenUnit
        {
            get
            {
                return "(" + DlyBase.SC_LINEARFITUNIT + ")[0- " + s_maxFlowCLen + "]";
            }
        }
        
        public static double s_maxFlowDVol = 100;
        public static string s_maxFlowDVolUnit
        {
            get
            {
                return "(" + DlyBase.SC_FITUNITML + ")[0- " + s_maxFlowDVol + "]";
            }
        }
        public static double s_maxFlowDLen = 100;
        public static string s_maxFlowDLenUnit
        {
            get
            {
                return "(" + DlyBase.SC_LINEARFITUNIT + ")[0- " + s_maxFlowDLen + "]";
            }
        }

        public static double s_maxPT = 40.00;
        public static double s_minPT = 0.02;

        public static double s_maxPH = 14;
        public static double s_minPH = 0;

        public static double s_maxCD = 999;
        public static double s_minCD = 0;

        public static double s_maxUV = 5000;
        public static double s_minUV = -5000;
        public static string s_UVUnit
        {
            get
            {
                return DlyBase.SC_UVABSUNIT + "[" + StaticValue.s_minUV + " - " + StaticValue.s_maxUV + "]";
            }
        }
        public static int s_maxWaveLength = 700;
        public static int s_minWaveLength = 190;
        public static Visibility s_waveEnabledVisible2 = Visibility.Collapsed;
        public static Visibility s_waveVisible3 = Visibility.Collapsed;
        public static Visibility s_waveVisible4 = Visibility.Collapsed;

        public static void Init(double flowSystem, double flowSample, double flowA, double flowB, double flowC, double flowD)
        {
            s_maxFlowVol = flowSystem;
            s_maxFlowSVol = flowSample;
            s_maxFlowAVol = flowA;
            s_maxFlowBVol = flowB;
            s_maxFlowCVol = flowC;
            s_maxFlowDVol = flowD;
            Reset();
        }

        public static void Reset()
        {
            SVolToLen = 60.0 / StaticSystemConfig.SSystemConfig.MConfColumn.MColumnArea;
        }

        public static string CheckData(EnumFlowRate enumFlowRate, double? flow, double? bs, double? be, double? cs, double? ce, double? ds, double? de)
        {
            switch(enumFlowRate)
            {
                case EnumFlowRate.CMH:
                    return CheckDataCMH(flow, bs, be, cs, ce, ds, de);
                default:
                    return CheckDataMLMIN(flow, bs, be, cs, ce, ds, de);
            }
        }

        private static string CheckDataMLMIN(double? flow, double? bs, double? be, double? cs, double? ce, double? ds, double? de)
        {
            StringBuilderSplit sb = new StringBuilderSplit();
            if (flow * (100 - bs - cs - ds) / 100 > StaticValue.s_maxFlowAVol)
            {
                sb.Append(Share.ReadXaml.GetResources("LabAS1") + Share.ReadXaml.S_InfoIllegalData);
            }
            if (flow * (100 - be - ce - de) / 100 > StaticValue.s_maxFlowAVol)
            {
                sb.Append(Share.ReadXaml.GetResources("LabAE1") + Share.ReadXaml.S_InfoIllegalData);
            }
            if (flow * bs / 100 > StaticValue.s_maxFlowBVol)
            {
                sb.Append(Share.ReadXaml.GetResources("LabBS1") + Share.ReadXaml.S_InfoIllegalData);
            }
            if (flow * be / 100 > StaticValue.s_maxFlowBVol)
            {
                sb.Append(Share.ReadXaml.GetResources("LabBE1") + Share.ReadXaml.S_InfoIllegalData);
            }
            if (flow * cs / 100 > StaticValue.s_maxFlowCVol)
            {
                sb.Append(Share.ReadXaml.GetResources("LabCS1") + Share.ReadXaml.S_InfoIllegalData);
            }
            if (flow * ce / 100 > StaticValue.s_maxFlowCVol)
            {
                sb.Append(Share.ReadXaml.GetResources("LabCE1") + Share.ReadXaml.S_InfoIllegalData);
            }
            if (flow * ds / 100 > StaticValue.s_maxFlowDVol)
            {
                sb.Append(Share.ReadXaml.GetResources("LabDS1") + Share.ReadXaml.S_InfoIllegalData);
            }
            if (flow * de / 100 > StaticValue.s_maxFlowDVol)
            {
                sb.Append(Share.ReadXaml.GetResources("LabDE1") + Share.ReadXaml.S_InfoIllegalData);
            }

            return sb.ToString();
        }

        private static string CheckDataCMH(double? flow, double? bs, double? be, double? cs, double? ce, double? ds, double? de)
        {
            StringBuilderSplit sb = new StringBuilderSplit();
            if (flow * (100 - bs - cs - ds) / 100 > StaticValue.s_maxFlowALen)
            {
                sb.Append(Share.ReadXaml.GetResources("LabAS1") + Share.ReadXaml.S_InfoIllegalData);
            }
            if (flow * (100 - be - ce - de) / 100 > StaticValue.s_maxFlowALen)
            {
                sb.Append(Share.ReadXaml.GetResources("LabAE1") + Share.ReadXaml.S_InfoIllegalData);
            }
            if (flow * bs / 100 > StaticValue.s_maxFlowBLen)
            {
                sb.Append(Share.ReadXaml.GetResources("LabBS1") + Share.ReadXaml.S_InfoIllegalData);
            }
            if (flow * be / 100 > StaticValue.s_maxFlowBLen)
            {
                sb.Append(Share.ReadXaml.GetResources("LabBE1") + Share.ReadXaml.S_InfoIllegalData);
            }
            if (flow * cs / 100 > StaticValue.s_maxFlowCLen)
            {
                sb.Append(Share.ReadXaml.GetResources("LabCS1") + Share.ReadXaml.S_InfoIllegalData);
            }
            if (flow * ce / 100 > StaticValue.s_maxFlowCLen)
            {
                sb.Append(Share.ReadXaml.GetResources("LabCE1") + Share.ReadXaml.S_InfoIllegalData);
            }
            if (flow * ds / 100 > StaticValue.s_maxFlowDLen)
            {
                sb.Append(Share.ReadXaml.GetResources("LabDS1") + Share.ReadXaml.S_InfoIllegalData);
            }
            if (flow * de / 100 > StaticValue.s_maxFlowDLen)
            {
                sb.Append(Share.ReadXaml.GetResources("LabDE1") + Share.ReadXaml.S_InfoIllegalData);
            }

            return sb.ToString();
        }

        public static string CheckData(EnumFlowRate enumFlowRate, double? flow, double? bs, double? cs, double? ds)
        {
            switch (enumFlowRate)
            {
                case EnumFlowRate.CMH:
                    return CheckDataCMH(flow, bs, cs, ds);
                default:
                    return CheckDataMLMIN(flow, bs, cs, ds);
            }
        }

        private static string CheckDataMLMIN(double? flow, double? bs, double? cs, double? ds)
        {
            StringBuilderSplit sb = new StringBuilderSplit();
            if (flow * (100 - bs - cs - ds) / 100 > StaticValue.s_maxFlowAVol)
            {
                sb.Append(Share.ReadXaml.GetResources("LabAS1") + Share.ReadXaml.S_InfoIllegalData);
            }
            if (flow * bs / 100 > StaticValue.s_maxFlowBVol)
            {
                sb.Append(Share.ReadXaml.GetResources("LabBS1") + Share.ReadXaml.S_InfoIllegalData);
            }
            if (flow * cs / 100 > StaticValue.s_maxFlowCVol)
            {
                sb.Append(Share.ReadXaml.GetResources("LabCS1") + Share.ReadXaml.S_InfoIllegalData);
            }
            if (flow * ds / 100 > StaticValue.s_maxFlowDVol)
            {
                sb.Append(Share.ReadXaml.GetResources("LabDS1") + Share.ReadXaml.S_InfoIllegalData);
            }

            return sb.ToString();
        }

        private static string CheckDataCMH(double? flow, double? bs, double? cs, double? ds)
        {
            StringBuilderSplit sb = new StringBuilderSplit();
            if (flow * (100 - bs - cs - ds) / 100 > StaticValue.s_maxFlowALen)
            {
                sb.Append(Share.ReadXaml.GetResources("LabAS1") + Share.ReadXaml.S_InfoIllegalData);
            }
            if (flow * bs / 100 > StaticValue.s_maxFlowBLen)
            {
                sb.Append(Share.ReadXaml.GetResources("LabBS1") + Share.ReadXaml.S_InfoIllegalData);
            }
            if (flow * cs / 100 > StaticValue.s_maxFlowCLen)
            {
                sb.Append(Share.ReadXaml.GetResources("LabCS1") + Share.ReadXaml.S_InfoIllegalData);
            }
            if (flow * ds / 100 > StaticValue.s_maxFlowDLen)
            {
                sb.Append(Share.ReadXaml.GetResources("LabDS1") + Share.ReadXaml.S_InfoIllegalData);
            }

            return sb.ToString();
        }

        public static string[] GetNameList(ENUMValveName index)
        {
            switch (index)
            {
                case ENUMValveName.InS: return EnumInSInfo.NameList;
                case ENUMValveName.InA: return EnumInAInfo.NameList;
                case ENUMValveName.InB: return EnumInBInfo.NameList;
                case ENUMValveName.InC: return EnumInCInfo.NameList;
                case ENUMValveName.InD: return EnumInDInfo.NameList;
                case ENUMValveName.IJV:return EnumIJVInfo.NameList;
                case ENUMValveName.BPV:return EnumBPVInfo.NameList;
                case ENUMValveName.CPV_1:
                case ENUMValveName.CPV_2: return EnumCPVInfo.NameList;
                default: return EnumOutInfo.NameList;
            }
        }
    }
}