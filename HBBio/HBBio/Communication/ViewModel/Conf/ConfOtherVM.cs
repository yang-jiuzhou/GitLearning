using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class ConfOtherVM : DlyNotifyPropertyChanged
    {
        #region 属性
        public ConfOther MItem { get; set; }

        public bool MResetValve
        {
            get
            {
                return MItem.MResetValve;
            }
            set
            {
                if (MItem.MResetValve == value)
                {
                    return;
                }

                MItem.MResetValve = value;
                OnPropertyChanged("MResetValve");
            }
        }
        public bool MCloseUV
        {
            get
            {
                return MItem.MCloseUV;
            }
            set
            {
                if (MItem.MCloseUV == value)
                {
                    return;
                }

                MItem.MCloseUV = value;
                OnPropertyChanged("MCloseUV");
            }
        }
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="item"></param>
        public ConfOtherVM(ConfOther item)
        {
            MItem = item;
        }
    }
}
