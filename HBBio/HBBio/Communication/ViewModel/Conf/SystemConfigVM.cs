using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class SystemConfigVM : DlyNotifyPropertyChanged
    {
        #region 属性
        public SystemConfig MItem { get; set; }

        public ConfColumnVM MConfColumn { get; set; }
        public ConfWashVM MConfWash { get; set; }
        public ConfCollectorVM MConfCollector { get; set; }
        public List<ConfASVM> MListConfAS { get; set; }
        private double m_delayVol = 0;
        public double MDelayVol
        {
            get
            {
                return m_delayVol;
            }
            set
            {
                if (m_delayVol == value)
                {
                    return;
                }

                m_delayVol = value;
                OnPropertyChanged("MDelayVol");
            }
        }
        public List<ConfpHCdUVVM> MListConfpHCdUV { get; set; }
        public ConfOtherVM MConfOtherVM { get; set; }
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="item"></param>
        public SystemConfigVM(SystemConfig item)
        {
            MItem = item;

            MConfColumn = new ConfColumnVM(item.MConfColumn);
            MConfWash = new ConfWashVM(item.MConfWash);
            MConfCollector = new ConfCollectorVM(item.MConfCollector);
            MListConfAS = new List<ConfASVM>();
            foreach(var it in item.MListConfAS)
            {
                MListConfAS.Add(new ConfASVM(it));
            }
            MDelayVol = item.MDelayVol;
            MListConfpHCdUV = new List<ConfpHCdUVVM>();
            foreach (var it in item.MListConfpHCdUV)
            {
                MListConfpHCdUV.Add(new ConfpHCdUVVM(it));
            }
            MConfOtherVM = new ConfOtherVM(item.MConfOther);
        }
    }
}
