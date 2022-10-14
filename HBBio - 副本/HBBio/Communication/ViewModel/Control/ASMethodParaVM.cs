using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.Communication
{
    public class ASMethodParaVM : DlyNotifyPropertyChanged
    {
        #region 属性
        private ASMethodPara m_item = null;
        public ASMethodPara MItem
        {
            get
            {
                return m_item;
            }
            set
            {
                m_item = value;
                if (null != value)
                {
                    switch (value.MAction)
                    {
                        case EnumMonitorActionMethod.Bypass:
                            MVisibleLengthUnit = Visibility.Visible;
                            break;
                        default:
                            MVisibleLengthUnit = Visibility.Collapsed;
                            break;
                    }
                }
            }
        }

        public string MHeader
        {
            get
            {
                return MItem.MHeader;
            }
            set
            {
                MItem.MHeader = value;
            }
        }
        public EnumMonitorActionMethod MAction
        {
            get
            {
                return MItem.MAction;
            }
            set
            {
                MItem.MAction = value;
                switch (value)
                {
                    case EnumMonitorActionMethod.Bypass:
                        MVisibleLengthUnit = Visibility.Visible;
                        break;
                    default:
                        MVisibleLengthUnit = Visibility.Collapsed;
                        MLength = 0;
                        MUnit = EnumBase.T;
                        break;
                }
            }
        }
        public double MLength
        {
            get
            {
                return MItem.MLength;
            }
            set
            {
                MItem.MLength = value;
                OnPropertyChanged("MLength");
            }
        }
        public EnumBase MUnit
        {
            get
            {
                return MItem.MUnit;
            }
            set
            {
                MItem.MUnit = value;
                OnPropertyChanged("MUnit");
            }
        }

        private Visibility m_visibleLengthUnit = Visibility.Collapsed;
        public Visibility MVisibleLengthUnit
        {
            get
            {
                return m_visibleLengthUnit;
            }
            set
            {
                m_visibleLengthUnit = value;
                OnPropertyChanged("MVisibleLengthUnit");
            }
        }
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="item"></param>
        public ASMethodParaVM(ASMethodPara item)
        {
            MItem = item;
        }
    }
}
