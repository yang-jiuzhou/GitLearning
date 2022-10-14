using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    /**
     * ClassName: AlarmWarningItem
     * Description: 警报警告元素项
     * Version: 1.0
     * Create:  2021/01/29
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    [Serializable]
    public class AlarmWarningItem
    {
        public string MTypeName { get; set; }
        public string MName { get; set; }
        public string MUnit { get; set; }
        public double MValLL { get; set; }
        public double MValL { get; set; }
        public double MValH { get; set; }
        public double MValHH { get; set; }
        public double MValMin { get; set; }
        public double MValMax { get; set; }

        public EnumAlarmWarningMode MCheckLL { get; set; }
        public EnumAlarmWarningMode MCheckL { get; set; }
        public EnumAlarmWarningMode MCheckH { get; set; }
        public EnumAlarmWarningMode MCheckHH { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public AlarmWarningItem()
        {
            MCheckLL = EnumAlarmWarningMode.Enabled;
            MCheckL = EnumAlarmWarningMode.Enabled;
            MCheckH = EnumAlarmWarningMode.Enabled;
            MCheckHH = EnumAlarmWarningMode.Enabled;
        }

        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="item"></param>
        public void SetValue(AlarmWarningItem item)
        {
            MCheckLL = item.MCheckLL;
            switch (MCheckLL)
            {
                case EnumAlarmWarningMode.Dly: MValLL = item.MValLL; break;
            }

            MCheckL = item.MCheckL;
            switch (MCheckL)
            {
                case EnumAlarmWarningMode.Dly: MValL = item.MValL; break;
            }

            MCheckH = item.MCheckH;
            switch (MCheckH)
            {
                case EnumAlarmWarningMode.Dly: MValH = item.MValH; break;
            }

            MCheckHH = item.MCheckHH;
            switch (MCheckHH)
            {
                case EnumAlarmWarningMode.Dly: MValHH = item.MValHH; break;
            }
        }

        /// <summary>
        /// 从配置还原
        /// </summary>
        /// <param name="item"></param>
        public void ResetValue(AlarmWarningItem item)
        {
            switch (MCheckLL)
            {
                case EnumAlarmWarningMode.Enabled: MValLL = item.MValLL; break;
            }

            switch (MCheckL)
            {
                case EnumAlarmWarningMode.Enabled: MValL = item.MValL; break;
            }

            switch (MCheckH)
            {
                case EnumAlarmWarningMode.Enabled: MValH = item.MValH; break;
            }

            switch (MCheckHH)
            {
                case EnumAlarmWarningMode.Enabled: MValHH = item.MValHH; break;
            }
        }
    }


    public enum EnumAlarmWarningMode
    {
        Disabled,
        Enabled,
        Dly
    }
}
