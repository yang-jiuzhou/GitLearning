using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class UVValueVM : DlyNotifyPropertyChanged
    {
        public UVValue MItem { get; set; }

        public bool MOnoff 
        { 
            get
            {
                return MItem.MOnoff;
            }
            set
            {
                MItem.MOnoff = value;
            }
        }
        public bool MClear
        {
            get
            {
                return MItem.MClear;
            }
            set
            {
                MItem.MClear = value;
            }
        }

        public int MWave1
        {
            get
            {
                return MItem.MWave1;
            }
            set
            {
                MItem.MWave1= value;
            }
        }
        public int MWave2
        {
            get
            {
                return MItem.MWave2;
            }
            set
            {
                MItem.MWave2 = value;
            }
        }
        public int MWave3
        {
            get
            {
                return MItem.MWave3;
            }
            set
            {
                MItem.MWave3 = value;
            }
        }
        public int MWave4
        {
            get
            {
                return MItem.MWave4;
            }
            set
            {
                MItem.MWave4 = value;
            }
        }

        public bool MEnabledWave2
        {
            get
            {
                return MItem.MEnabledWave2;
            }
            set
            {
                MItem.MEnabledWave2 = value;
                OnPropertyChanged("MEnabledWave2");
            }
        }
    }
}
