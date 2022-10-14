using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class StringWrapper : DlyNotifyPropertyChanged
    {
        public string MName { get; set; }

        private string m_value;
        public string MValue
        {
            get
            {
                return m_value;
            }
            set
            {
                m_value = value;
                OnPropertyChanged("MValue");
            }
        }
    }

    class DoubleWrapper : DlyNotifyPropertyChanged
    {
        public string MName { get; set; }

        private double m_value;
        public double MValue
        {
            get
            {
                return m_value;
            }
            set
            {
                m_value = value;
                OnPropertyChanged("MValue");
            }
        }
    }
}
