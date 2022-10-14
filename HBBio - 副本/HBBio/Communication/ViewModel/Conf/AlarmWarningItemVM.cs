using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class AlarmWarningItemVM : DlyNotifyPropertyChanged
    {
        #region 属性
        public AlarmWarningItem MItem { get; set; }

        public string MNameUnit
        {
            get
            {
                return MItem.MName + "(" + MItem.MUnit + ")";
            }
        }
        public double MValLL
        {
            get
            {
                return MItem.MValLL;
            }
            set
            {
                MItem.MValLL = value;
                MCheckLL = EnumAlarmWarningMode.Dly;
            }
        }
        public EnumAlarmWarningMode MCheckLL
        {
            get
            {
                return MItem.MCheckLL;
            }
            set
            {
                MItem.MCheckLL = value;
                OnPropertyChanged("MCheckLL");
            }
        }
        public double MValL
        {
            get
            {
                return MItem.MValL;
            }
            set
            {
                MItem.MValL = value;
                MCheckL = EnumAlarmWarningMode.Dly;
            }
        }
        public EnumAlarmWarningMode MCheckL
        {
            get
            {
                return MItem.MCheckL;
            }
            set
            {
                MItem.MCheckL = value;
                OnPropertyChanged("MCheckL");
            }
        }
        public double MValH
        {
            get
            {
                return MItem.MValH;
            }
            set
            {
                MItem.MValH = value;
                MCheckH = EnumAlarmWarningMode.Dly;
            }
        }
        public EnumAlarmWarningMode MCheckH
        {
            get
            {
                return MItem.MCheckH;
            }
            set
            {
                MItem.MCheckH = value;
                OnPropertyChanged("MCheckH");
            }
        }
        public double MValHH
        {
            get
            {
                return MItem.MValHH;
            }
            set
            {
                MItem.MValHH = value;
                MCheckHH = EnumAlarmWarningMode.Dly;
            }
        }
        public EnumAlarmWarningMode MCheckHH
        {
            get
            {
                return MItem.MCheckHH;
            }
            set
            {
                MItem.MCheckHH = value;
                OnPropertyChanged("MCheckHH");
            }
        }
        public double MValMin
        {
            get
            {
                return MItem.MValMin;
            }
        }
        public double MValMax
        {
            get
            {
                return MItem.MValMax;
            }
        }
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="item"></param>
        public AlarmWarningItemVM(AlarmWarningItem item)
        {
            MItem = item;
        }
    }
}
