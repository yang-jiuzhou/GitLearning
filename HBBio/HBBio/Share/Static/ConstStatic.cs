using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HBBio.Share
{
    /**
     * ClassName: ValueTrans
     * Description: 静态数值常量类
     * Version: 1.0
     * Create:  2018/05/28
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class DlyBase
    {
        public const double PI = 3.14;
        public const double DOUBLE = 0.0001;
        public const double MAX = 999999.99;

        public const string SC_Time = "Time";           //时间
        public const string SC_Volume = "Volume";       //体积
        public const string SC_CV = "ColumnVolume";     //柱体积
        public const string SC_TimeCN = "时间";         //时间
        public const string SC_VolumeCN = "体积";       //体积
        public const string SC_CVCN = "柱体积";         //柱体积
        public const string SC_TUNIT = "min";           //时间
        public const string SC_VUNITL = "l";            //体积(l)
        public const string SC_VUNITML = "ml";          //体积(ml)
        public const string SC_VUNITUL = "µl";          //体积(µl)
        public const string SC_CVUNIT = "cv";           //柱体积
        public const string SC_FERUNIT = "%";           //流速百分比
        public const string SC_FITUNITL = "l/h";        //流速(l)
        public const string SC_FITUNITML = "ml/min";    //流速(ml)
        public const string SC_FITUNITUL = "µl/min";    //流速(µl)
        public const string SC_LINEARFITUNIT = "cm/h";  //线性流速
        public const string SC_PTUNITMPA = "MPa";       //压力
        public const string SC_PTUNITPA = "Pa";         //压力
        public const string SC_PHUNIT = "";             //pH值
        public const string SC_PHSLOPEUNIT = "/min";    //pH值斜率
        public const string SC_CDUNIT = "mS/cm";        //电导值
        public const string SC_CDSLOPEUNIT = "mS/cm/min";   //电导值斜率
        public const string SC_TEMPUNIT = "℃";         //温度值
        public const string SC_UVABSUNIT = "mAu";       //紫外吸收值
        public const string SC_UVSLOPEABSUNIT = "mAu/min";  //紫外吸收值斜率
        public const string SC_UVWAVEUNIT = "nm";       //紫外波长
        public const string SC_LENGTHUNIT = "cm";       //长度
        public const string SC_WEIGHTUNIT = "mg";       //重量
        public const string SC_PARTICLEUNIT = "µlm";    //粒子直径
        public const string SC_MOLECULARUNIT = "Mr";    //分子量范围
        public const string SC_CDAreaUNIT = "mS/cm.min";    //电导面积值
        public const string SC_UVABSAreaUNIT = "mAu.min";   //紫外面积值
        public const string SC_RIUNIT = "mAu";          //示差检测器

        public const int c_sleep1 = 100;
        public const int c_sleep2 = 200;
        public const int c_sleep3 = 300;
        public const int c_sleep5 = 500;
        public const int c_sleep10 = 1000;
        public const int c_sleep20 = 2000;
        public const int c_sleep50 = 5000;
        public const int c_sleep100 = 10000;
        public const int c_sleep600 = 60000;
    }


    /// <summary>
    /// 基本单位枚举
    /// </summary>
    public enum EnumBase
    {
        T,
        V,
        CV
    }


    /// <summary>
    /// 流速单位枚举
    /// </summary>
    public enum EnumFlowRate
    {
        MLMIN,
        CMH
    }

    /// <summary>
    /// 手动执行枚举
    /// </summary>
    public enum EnumStatus
    {
        Null,
        Ing,
        Over
    }

    /// <summary>
    /// 逻辑比较枚举
    /// </summary>
    public enum EnumJudge
    {
        Stable,
        MoreThan,
        LessThan
    }

    /// <summary>
    /// 操作枚举
    /// </summary>
    public enum EnumMonitorActionManual
    {
        Ignore,
        Bypass,
        Pause,
        Stop
    }

    /// <summary>
    /// 操作枚举
    /// </summary>
    public enum EnumMonitorActionMethod
    {
        Ignore,
        Bypass,
        Next,
        Pause,
        Stop
    }

    /**
     * ClassName: EnumVolumeInfo
     * Description: 体积单位枚举信息
     * Version: 1.0
     * Create:  2020/06/30
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class EnumBaseInfo
    {
        public const int Count = 3;

        public readonly static string[] NameList = new string[Count]
        {
            DlyBase.SC_TUNIT,
            DlyBase.SC_VUNITML,
            DlyBase.SC_CVUNIT
        };
    }
    class EnumBaseString
    {
        public EnumBase MMode { get; set; }
        public string MName { get; set; }

        public static List<EnumBaseString> GetItemsSource()
        {
            List<EnumBaseString> list = new List<EnumBaseString>();
            foreach (EnumBase it in Enum.GetValues(typeof(EnumBase)))
            {
                list.Add(new EnumBaseString() { MMode = it, MName = EnumBaseInfo.NameList[(int)it] });
            }

            return list;
        }
    }


    /**
     * ClassName: EnumVolumeInfo
     * Description: 体积单位枚举信息
     * Version: 1.0
     * Create:  2020/06/30
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class EnumVolumeInfo
    {
        public const int Count = 3;

        public readonly static string[] NameList = new string[Count]
        {
            DlyBase.SC_VUNITL,
            DlyBase.SC_VUNITML,
            DlyBase.SC_VUNITUL
        };
    }


    /**
     * ClassName: EnumFlowRateInfo
     * Description: 流速单位枚举信息
     * Version: 1.0
     * Create:  2020/06/30
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class EnumFlowRateUnitInfo
    {
        public const int Count = 2;

        public readonly static string[] NameList = new string[Count]
        {
            DlyBase.SC_FITUNITML,
            DlyBase.SC_LINEARFITUNIT
        };
    }
    class EnumFlowRateString
    {
        public EnumFlowRate MMode { get; set; }
        public string MName { get; set; }

        public static List<EnumFlowRateString> GetItemsSource()
        {
            List<EnumFlowRateString> list = new List<EnumFlowRateString>();
            foreach (EnumFlowRate it in Enum.GetValues(typeof(EnumFlowRate)))
            {
                list.Add(new EnumFlowRateString() { MMode = it, MName = EnumFlowRateUnitInfo.NameList[(int)it] });
            }

            return list;
        }
    }


    /**
     * ClassName: EnumFlowInfo
     * Description: 流速单位枚举信息
     * Version: 1.0
     * Create:  2020/06/30
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class EnumFlowInfo
    {
        public const int Count = 3;

        public readonly static string[] NameList = new string[Count]
        {
            DlyBase.SC_FITUNITL,
            DlyBase.SC_FITUNITML,
            DlyBase.SC_FITUNITUL
        };
    }


    /**
     * ClassName: EnumPressureInfo
     * Description: 压力单位枚举信息
     * Version: 1.0
     * Create:  2020/06/30
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class EnumPressureInfo
    {
        public const int Count = 2;

        public readonly static string[] NameList = new string[Count]
        {
            DlyBase.SC_PTUNITMPA,
            DlyBase.SC_PTUNITPA
        };
    }


    /**
     * ClassName: MFontSize
     * Description: 字号类
     * Version: 1.0
     * Create:  2021/01/19
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class MFontSize
    {
        public static List<double> MList
        {
            get
            {
                List<double> fontSizeList = new List<double>();
                fontSizeList.Add(12);
                fontSizeList.Add(16);
                fontSizeList.Add(18);
                fontSizeList.Add(20);
                fontSizeList.Add(22);
                fontSizeList.Add(26);
                fontSizeList.Add(32);
                fontSizeList.Add(48);
                return fontSizeList;
            }
        }
    }


    /**
     * ClassName: ValueTrans
     * Description: 数值转化类
     * Version: 1.0
     * Create:  2018/05/28
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class ValueTrans
    {
        /// <summary>
        /// 返回一个有效值
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static double JudgeRange(double data)
        {
            if (Math.Abs(data) < DlyBase.DOUBLE || Math.Abs(data) > DlyBase.MAX)
            {
                return 0;
            }
            else
            {
                return data;
            }
        }

        /// <summary>
        /// 计算坐标的精度
        /// </summary>
        /// <param name="dis"></param>
        /// <returns></returns>
        public static string CalUnit(double dis)
        {
            if (Math.Abs(dis) < 0.0001)
            {
                return "f5";
            }
            else if (Math.Abs(dis) < 0.001)
            {
                return "f4";
            }
            else if (Math.Abs(dis) < 0.01)
            {
                return "f3";
            }
            else if (Math.Abs(dis) < 0.1)
            {
                return "f2";
            }
            else if (Math.Abs(dis) < 1)
            {
                return "f1";
            }
            else
            {
                return "f0";
            }
        }

        public static void CalMaxMin(ref double min, ref double max, ref double radix)
        {
            double dis = max - min;
            if (dis <= 0.001)
            {
                radix = 0.0001;
            }
            else if (dis <= 0.01)
            {
                radix = 0.001;
            }
            else if (dis <= 0.1)
            {
                radix = 0.01;
            }
            else if (dis <= 1)
            {
                radix = 0.1;
            }
            else
            {
                radix = Math.Pow(10, ((int)dis).ToString().Length - 1);

                if (Math.Abs(dis - radix) < DlyBase.DOUBLE)
                {
                    radix /= 10;
                }
            }

            max = CalGreater(radix, max);
            min = CalLess(radix, min);
        }

        /// <summary>
        /// 1.33->1.4 1.3->1.3 -1.33->-1.3 -1.3->-1.3
        /// </summary>
        /// <param name="radix"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static double CalGreater(double radix, double val)
        {
            double temp = val / radix;
            if (Math.Abs(temp - (int)temp) >= DlyBase.DOUBLE)
            {
                if (val >= 0)
                {
                    return ((int)temp + 1) * radix;
                }
                else
                {
                    return (int)temp * radix;
                }
            }
            else
            {
                return val;
            }
        }

        /// <summary>
        /// 1.33->1.3 1.3->1.3
        /// </summary>
        /// <param name="radix"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static double CalLess(double radix, double val)
        {
            double temp = val / radix;
            if (Math.Abs(temp - (int)temp) >= 0)
            {
                if (val >= 0)
                {
                    return (int)temp * radix;
                }
                else
                {
                    return ((int)temp - 1) * radix;
                }
            }
            else
            {
                return val;
            }
        }

        /// <summary>
        /// 时间间隔转为以分钟为单位的浮点数
        /// </summary>
        /// <param name="span"></param>
        /// <returns></returns>
        public static double TimeSpanToMin(DateTime tBack, DateTime tFront)
        {
            return double.Parse((new TimeSpan(tBack.Ticks - tFront.Ticks)).TotalMinutes.ToString("f2"));
        }

        /// <summary>
        /// 时间间隔转为以小时为单位的浮点数
        /// </summary>
        /// <param name="span"></param>
        /// <returns></returns>
        public static double TimeSpanToHour(DateTime tBack, DateTime tFront)
        {
            return double.Parse((new TimeSpan(tBack.Ticks - tFront.Ticks)).TotalHours.ToString("f2"));
        }

        /// <summary>
        /// 交换数值
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        /// <summary>
        /// 计算面积
        /// </summary>
        /// <param name="diam"></param>
        /// <returns></returns>
        public static double CalArea(double diam)
        {
            if (diam < 0)
            {
                return 0;
            }

            double mlArea = DlyBase.PI * Math.Pow(diam / 2, 2);
            if (diam < 0)
            {
                mlArea = 0 - mlArea;
            }

            return mlArea;
        }

        /// <summary>
        /// 计算高
        /// </summary>
        /// <param name="diam"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static double CalVol(double diam, double height)
        {
            return CalArea(diam) * height;
        }

        /// <summary>
        /// 流速单位换算
        /// </summary>
        /// <param name="value"></param>
        /// <param name="newUnit"></param>
        /// <param name="oldUnit"></param>
        /// <returns></returns>
        public static double CalFlowUnit(double value, string newUnit, string oldUnit)
        {
            int indexNew = EnumVolumeInfo.NameList.ToList().IndexOf(newUnit);
            int indexOld = EnumVolumeInfo.NameList.ToList().IndexOf(oldUnit);
            if (-1 != indexNew)
            {
                value *= Math.Pow(1000, indexNew - indexOld);
            }
            else
            {
                indexNew = EnumFlowInfo.NameList.ToList().IndexOf(newUnit);
                indexOld = EnumFlowInfo.NameList.ToList().IndexOf(oldUnit);
                if (-1 != indexNew)
                {
                    switch (indexNew)
                    {
                        case 0:
                            switch (indexOld)
                            {
                                case 1:// ml/min -> l/h
                                    value = value * 60 / 1000;
                                    break;
                                case 2:// ul/min -> l/h
                                    value = value / 1000 * 60 / 1000;
                                    break;
                            }
                            break;
                        case 1:
                            switch (indexOld)
                            {
                                case 0:// l/h -> ml/min
                                    value = value * 1000 / 60;
                                    break;
                                case 2:// ul/min -> ml/min
                                    value = value / 1000;
                                    break;
                            }
                            break;
                        case 2:
                            switch (indexOld)
                            {
                                case 0:// l/h -> ul/min
                                    value = value * 1000 / 60 * 1000;
                                    break;
                                case 1:// ml/min -> ul/min
                                    value = value * 1000;
                                    break;
                            }
                            break;
                    }
                }
                else
                {
                    indexNew = EnumPressureInfo.NameList.ToList().IndexOf(newUnit);
                    indexOld = EnumPressureInfo.NameList.ToList().IndexOf(oldUnit);
                    if (-1 != indexNew)
                    {
                        value *= Math.Pow(10, indexNew - indexOld);
                    }
                }
            }

            return value;
        }

        /// <summary>
        /// 返回倍数值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double GetTimes(double value)
        {
            if (value < 1)
            {
                return 1;
            }
            else
            {
                return Math.Pow(2, (int)Math.Log(value, 2) + 1);
            }
        }

        public static System.Drawing.Color MediaToDraw(System.Windows.Media.Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public static System.Windows.Media.Color DrawToMedia(System.Drawing.Color color)
        {
            return System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public static double ChangeBaseUnit(EnumBase indexOld, EnumBase indexNew, double colVol, double flow, double val)
        {
            if (flow < 0.01)
            {
                return 0;
            }

            switch (indexNew)
            {
                case EnumBase.T:
                    switch (indexOld)
                    {
                        case EnumBase.V:
                            return val / flow;
                        case EnumBase.CV:
                            return val * colVol / flow;
                    }
                    break;
                case EnumBase.V:
                    switch (indexOld)
                    {
                        case EnumBase.T:
                            return val * flow;
                        case EnumBase.CV:
                            return val * colVol;
                    }
                    break;
                case EnumBase.CV:
                    switch (indexOld)
                    {
                        case EnumBase.T:
                            return val * flow / colVol;
                        case EnumBase.V:
                            return val / colVol;
                    }
                    break;
            }

            return val;
        }
        public static double ChangeFlowRateUnit(EnumFlowRate indexOld, EnumFlowRate indexNew, double colArea, double val)
        {
            switch (indexNew)
            {
                case EnumFlowRate.MLMIN:
                    switch (indexOld)
                    {
                        case EnumFlowRate.CMH:
                            return val * colArea / 60;
                    }
                    break;
                case EnumFlowRate.CMH:
                    switch (indexOld)
                    {
                        case EnumFlowRate.MLMIN:
                            return val * 60 / colArea;
                    }
                    break;
            }

            return val;
        }
    }
}
