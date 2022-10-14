using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.Collection
{
    /**
     * ClassName: CollectionObjectVM
     * Description: 收集对象
     * Version: 1.0
     * Create:  2021/03/12
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class CollectionObjectVM : DlyNotifyPropertyChanged
    {
        #region 属性
        public CollectionObject MItem { get; set; }

        public int MType
        {
            get
            {
                return MItem.MType;
            }
            set
            {
                MItem.MType = value;

                if (3 > MType)
                {
                    MTdB = 0;
                    MTdE = MLength;
                }
                MEnabledLength = Visibility.Hidden;
                MEnabledTS = Visibility.Hidden;
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
                if (4 > MType)
                {
                    MTdB = 0;
                    MTdE = value;
                }
            }
        }
        public EnumThresholdSlope MTS
        {
            get
            {
                return MItem.MTS;
            }
            set
            {
                MItem.MTS = value;

                MEnabledTS = Visibility.Hidden;
            }
        }
        public double MTdB
        {
            get
            {
                return MItem.MTdB;
            }
            set
            {
                MItem.MTdB = value;
                OnPropertyChanged("MTdB");
            }
        }
        public double MTdE
        {
            get
            {
                return MItem.MTdE;
            }
            set
            {
                MItem.MTdE = value;
                OnPropertyChanged("MTdE");
            }
        }
        public EnumGreaterLess MSJ
        {
            get
            {
                return MItem.MSJ;
            }
            set
            {
                MItem.MSJ = value;
            }
        }
        public double MSlope
        {
            get
            {
                return MItem.MSlope;
            }
            set
            {
                MItem.MSlope = value;
            }
        }

        public Visibility MEnabledLength
        {
            get
            {
                if (MType < 3)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Hidden;
                }
            }
            set
            {
                OnPropertyChanged("MEnabledLength");

                MEnabledTdB = Visibility.Hidden;
                MEnabledTdE = Visibility.Hidden;
            }
        }
        public Visibility MEnabledTS
        {
            get
            {
                if (MType < 3)
                {
                    return Visibility.Hidden;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
            set
            {
                OnPropertyChanged("MEnabledTS");

                MEnabledTdB = Visibility.Hidden;
                MEnabledTdE = Visibility.Hidden;
                MEnabledSJ = Visibility.Hidden;
            }
        }
        public Visibility MEnabledTdB
        {
            get
            {
                if (MType < 3)
                {
                    return Visibility.Visible;
                }
                else
                {
                    switch (MTS)
                    {
                        case EnumThresholdSlope.Slope:
                            return Visibility.Hidden;
                    }
                    return Visibility.Visible;
                }
            }
            set
            {
                OnPropertyChanged("MEnabledTdB");
            }
        }
        public Visibility MEnabledTdE
        {
            get
            {
                if (MType < 3)
                {
                    return Visibility.Visible;
                }
                else
                {
                    switch (MTS)
                    {
                        case EnumThresholdSlope.Threshold:
                        case EnumThresholdSlope.ThresholdSlope:
                            return Visibility.Visible;
                    }
                    return Visibility.Hidden;
                }
            }
            set
            {
                OnPropertyChanged("MEnabledTdE");
            }
        }
        public Visibility MEnabledSJ
        {
            get
            {
                if (MType > 2)
                {
                    switch (MTS)
                    {
                        case EnumThresholdSlope.Slope:
                        case EnumThresholdSlope.ThresholdSlope:
                            return Visibility.Visible;
                    }
                }
                return Visibility.Hidden;
            }
            set
            {
                OnPropertyChanged("MEnabledSJ");
            }
        }
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="item"></param>
        public CollectionObjectVM(CollectionObject item)
        {
            MItem = item;
        }
    }
}
