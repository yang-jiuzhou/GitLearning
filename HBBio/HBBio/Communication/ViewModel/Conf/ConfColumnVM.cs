using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class ConfColumnVM : DlyNotifyPropertyChanged
    {
        #region 属性
        public ConfColumn MItem { get; set; }

        public double MColumnVol
        {
            get
            {
                return MItem.MColumnVol;
            }
            set
            {
                if (MItem.MColumnVol == value)
                {
                    return;
                }

                MItem.MColumnVol = value;
                OnPropertyChanged("MColumnVol");
            }
        }
        public double MColumnDiameter
        {
            get
            {
                return MItem.MColumnDiameter;
            }
            set
            {
                if (MItem.MColumnDiameter == value)
                {
                    return;
                }

                MItem.MColumnDiameter = value;

                MColumnVol = DlyBase.PI * MColumnDiameter * MColumnDiameter / 4 * MColumnHeight;
            }
        }
        public double MColumnHeight
        {
            get
            {
                return MItem.MColumnHeight;
            }
            set
            {
                if (MItem.MColumnHeight == value)
                {
                    return;
                }

                MItem.MColumnHeight = value;

                MColumnVol = DlyBase.PI * MColumnDiameter * MColumnDiameter / 4 * MColumnHeight;
            }
        }
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="item"></param>
        public ConfColumnVM(ConfColumn item)
        {
            MItem = item;
        }
    }
}
