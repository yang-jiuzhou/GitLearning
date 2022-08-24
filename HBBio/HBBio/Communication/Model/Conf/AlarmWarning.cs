using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    /**
     * ClassName: ConfAlarmWarning
     * Description: 警报警告集合
     * Version: 1.0
     * Create:  2021/01/29
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    [Serializable]
    public class AlarmWarning
    {
        public List<AlarmWarningItem> MList { get; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public AlarmWarning()
        {
            MList = new List<AlarmWarningItem>();
        }

        public void SetPTColumn(double val)
        {
            foreach (var it in MList)
            {
                if (it.MTypeName.Contains("PT") && !it.MTypeName.Contains("Delta"))
                {
                    it.MValHH = val;
                    it.MCheckHH = EnumAlarmWarningMode.Dly;
                }
            }
        }

        public void SetPTColumnDelta(double val)
        {
            foreach (var it in MList)
            {
                if (it.MTypeName.Contains("PT") && it.MTypeName.Contains("Delta"))
                {
                    it.MValHH = val;
                    it.MCheckHH = EnumAlarmWarningMode.Dly;
                    break;
                }
            }
        }

        public void SetpH(double valLL, double valL, double valH, double valHH)
        {
            foreach (var it in MList)
            {
                if (it.MTypeName.Contains("pH"))
                {
                    if (-1 != valLL)
                    {
                        it.MValLL = valLL;
                        it.MCheckLL = EnumAlarmWarningMode.Dly;
                    }
                    if (-1 != valL)
                    {
                        it.MValL = valL;
                        it.MCheckL = EnumAlarmWarningMode.Dly;
                    }
                    if (-1 != valH)
                    {
                        it.MValH = valH;
                        it.MCheckH = EnumAlarmWarningMode.Dly;
                    }
                    if (-1 != valHH)
                    {
                        it.MValHH = valHH;
                        it.MCheckHH = EnumAlarmWarningMode.Dly;
                    }
                }
            }
        }

        public void ClearPT()
        {
            foreach (var it in MList)
            {
                if (it.MTypeName.Contains("PT"))
                {
                    it.MCheckHH = EnumAlarmWarningMode.Enabled;
                }
            }
        }

        public void ClearpH()
        {
            foreach (var it in MList)
            {
                if (it.MTypeName.Contains("pH"))
                {
                    it.MCheckLL = EnumAlarmWarningMode.Enabled;
                    it.MCheckL = EnumAlarmWarningMode.Enabled;
                    it.MCheckH = EnumAlarmWarningMode.Enabled;
                    it.MCheckHH = EnumAlarmWarningMode.Enabled;
                }
            }
        }
    }
}
