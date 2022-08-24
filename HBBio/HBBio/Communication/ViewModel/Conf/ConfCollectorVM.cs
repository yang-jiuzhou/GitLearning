using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class ConfCollectorVM : DlyNotifyPropertyChanged
    {
        #region 属性
        public ConfCollector MItem { get; set; }

        public double MGLTJ
        {
            get
            {
                return MItem.MGLTJ;
            }
            set
            {
                MItem.MGLTJ = value;
            }
        }

        public double MVolL
        {
            get
            {
                return MItem.MVolL;
            }
            set
            {
                MItem.MVolL = value;

                OnPropertyChanged("MVolL");
            }
        }
        public double MVolR
        {
            get
            {
                return MItem.MVolR;
            }
            set
            {
                MItem.MVolR = value;

                OnPropertyChanged("MVolR");
            }
        }

        public int MCountL
        {
            get
            {
                return MItem.MCountL;
            }
            set
            {
                MItem.MCountL = value;

                OnPropertyChanged("MCountL");
            }
        }
        public int MCountR
        {
            get
            {
                return MItem.MCountR;
            }
            set
            {
                MItem.MCountR = value;

                OnPropertyChanged("MCountR");
            }
        }
        public int MModeL
        {
            get
            {
                return MItem.MModeL;
            }
            set
            {
                MItem.MModeL = value;

                OnPropertyChanged("MModeL");
            }
        }
        public int MModeR
        {
            get
            {
                return MItem.MModeR;
            }
            set
            {
                MItem.MModeR = value;

                OnPropertyChanged("MModeR");
            }
        }
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="item"></param>
        public ConfCollectorVM(ConfCollector item)
        {
            MItem = item;
        }
    }
}
