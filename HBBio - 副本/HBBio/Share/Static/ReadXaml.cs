using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Share
{
    public class ReadXaml
    {
        /// <summary>
        /// 字符串-成功
        /// </summary>
        public static string S_SuccessTxt
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["txtSuccess"];
            }
        }

        /// <summary>
        /// 字符串-失败
        /// </summary>
        public static string S_FailureTxt
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["txtFailure"];
            }
        }

        /// <summary>
        /// 字符串-成功
        /// </summary>
        public static string S_Success
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["labSuccess"];
            }
        }

        /// <summary>
        /// 字符串-失败
        /// </summary>
        public static string S_Failure
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["labFailure"];
            }
        }
        
        /// <summary>
        /// 字符串-启用
        /// </summary>
        public static string S_Enabled
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["labEnable"];
            }
        }

        /// <summary>
        /// 字符串-禁用
        /// </summary>
        public static string S_Disabled
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["labDisable"];
            }
        }

        /// <summary>
        /// 字符串-开
        /// </summary>
        public static string S_On
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["labOn"];
            }
        }

        /// <summary>
        /// 字符串-关
        /// </summary>
        public static string S_Off
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["labOff"];
            }
        }

        /// <summary>
        /// 字符串-是
        /// </summary>
        public static string S_Yes
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["labYes"];
            }
        }

        /// <summary>
        /// 字符串-否
        /// </summary>
        public static string S_No
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["labNo"];
            }
        }

        /// <summary>
        /// 字符串-最大值
        /// </summary>
        public static string S_Max
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["labMax"];
            }
        }

        /// <summary>
        /// 字符串-最小值
        /// </summary>
        public static string S_Min
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["labMin"];
            }
        }

        /// <summary>
        /// 字符串-是否继续
        /// </summary>
        public static string S_Continue
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["labContinue"];
            }
        }

        /// <summary>
        /// 字符串-无数据
        /// </summary>
        public static string S_ErrorNoData
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["labErrorNoData"];
            }
        }

        /// <summary>
        /// 字符串-名称不合法
        /// </summary>
        public static string S_ErrorIllegalName
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["labIllegalName"];
            }
        }

        /// <summary>
        /// 字符串-已存在同名
        /// </summary>
        public static string S_ErrorSameName
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["labSameName"];
            }
        }

        /// <summary>
        /// 字符串-警报弹窗
        /// </summary>
        public static string S_MsgAlarm
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["labMsgAlarm"];
            }
        }

        /// <summary>
        /// 字符串-警告弹窗
        /// </summary>
        public static string S_MsgWarning
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["labMsgWarning"];
            }
        }

        /// <summary>
        /// 字符串-该操作只能在系统空闲状态下执行
        /// </summary>
        public static string S_OnlyFree
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["labOnlyFree"];
            }
        }

        /// <summary>
        /// 字符串-该操作只能在系统非空闲状态下执行
        /// </summary>
        public static string S_OnlyNotFree
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["labOnlyNotFree"];
            }
        }

        /// <summary>
        /// 字符串-请等待当前操作执行完成
        /// </summary>
        public static string S_WaitCurrOper
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["labWaitCurrOper"];
            }
        }

        /// <summary>
        /// 字符串-柱体积(ml)
        /// </summary>
        public static string S_ColumnVol1
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["labColumnVol1"];
            }
        }

        /// <summary>
        /// 字符串-柱体积(ml)
        /// </summary>
        public static string S_ColumnHeight1
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["labColumnHeight1"];
            }
        }

        /// <summary>
        /// 字符串-柱体积(ml)
        /// </summary>
        public static string S_UVWaveLength1
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["labUVWaveLength1"];
            }
        }

        /// <summary>
        /// 字符串-监测到气泡
        /// </summary>
        public static string S_InfoASYes
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["infoASYes"];
            }
        }

        /// <summary>
        /// 字符串-监测无气泡
        /// </summary>
        public static string S_InfoASNo
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["infoASNo"];
            }
        }

        /// <summary>
        /// 字符串-数据超出限制范围
        /// </summary>
        public static string S_InfoIllegalData
        {
            get
            {
                return (string)System.Windows.Application.Current.Resources["infoIllegalData"];
            }
        }

        public static string GetResources(string str)
        {
            return (string)System.Windows.Application.Current.Resources[str];
        }

        public static string GetEnum<EnumType>(EnumType it, string beginStr = "")
        {
            return (string)System.Windows.Application.Current.Resources[beginStr + it.ToString()];
        }

        public static List<string> GetEnumList<EnumType>(string beginStr = "")
        {
            List<string> list = new List<string>();
            foreach (EnumType it in Enum.GetValues(typeof(EnumType)))
            {
                list.Add((string)System.Windows.Application.Current.Resources[beginStr + it.ToString()]);
            }

            return list;
        }
    }  
}
