using HBBio.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HBBio.Communication
{
    /**
     * ClassName: BaseCommunication
     * Description: 系统配置基本信息
     * Version: 1.0
     * Create:  2020/05/16
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    public class CommunicationSets : DlyNotifyPropertyChanged
    {
        private int m_id = -1;
        private string m_name = "";
        private string m_communMode = "";
        private string m_note = "";
        private DateTime m_datetime = DateTime.Now;
        private bool m_isEnabled = false;

        public int MId
        {
            get
            {
                return m_id;
            }
        }
        public string MName
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
                OnPropertyChanged("MName");
            }
        }
        public string MCommunMode
        {
            get
            {
                return m_communMode;
            }
            set
            {
                m_communMode = value;
                OnPropertyChanged("MCommunMode");
            }
        }
        public string MNote
        {
            get
            {
                return m_note;
            }
            set
            {
                m_note = value;
                OnPropertyChanged("MNote");
            }
        }
        public DateTime MDatetime
        {
            get
            {
                return m_datetime;
            }
        }
        public string MDatetimeStr
        {
            get
            {
                return m_datetime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
        public bool MIsEnabled
        {
            get
            {
                return m_isEnabled;
            }
            set
            {
                m_isEnabled = value;
                MIsEnabledIcon = null;
            }
        }

        private string m_isEnabledIconOn = "/Bio-LabChrom;component/Image/on.png";     //仅仅显示作用
        private string m_isEnabledIconOff = "/Bio-LabChrom;component/Image/off.png";   //仅仅显示作用
        public string MIsEnabledIcon
        {
            get
            {
                return MIsEnabled ? m_isEnabledIconOn : m_isEnabledIconOff;
            }
            set
            {
                OnPropertyChanged("MIsEnabledIcon");
            }
        }                                           //仅仅显示作用


        /// <summary>
        /// 构造函数，新建使用
        /// </summary>
        public CommunicationSets()
        {
        }

        /// <summary>
        /// 构造函数，数据库使用
        /// </summary>
        /// <param name="name"></param>
        /// <param name="note"></param>
        public CommunicationSets(int id, string name, string communMode, string note, DateTime datetime, bool isEnabled)
        {
            m_id = id;
            MName = name;
            MCommunMode = communMode;
            MNote = note;
            m_datetime = datetime;
            MIsEnabled = isEnabled;
        }
    }   
}
