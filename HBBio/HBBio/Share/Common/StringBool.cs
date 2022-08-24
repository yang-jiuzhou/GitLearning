using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Share
{
    /**
     * ClassName: StringBool
     * Description: 适用于名称+勾选框
     * Version: 1.0
     * Create:  2021/02/03
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class StringBool : DlyNotifyPropertyChanged
    {
        /// <summary>
        /// 名称
        /// </summary>
        private string m_name = "";
        public string MName
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }

        /// <summary>
        /// 颜色
        /// </summary>
        private System.Windows.Media.Brush m_brush = System.Windows.Media.Brushes.Black;
        public System.Windows.Media.Brush MBrush
        {
            get
            {
                return m_brush;
            }
            set
            {
                m_brush = value;
                OnPropertyChanged("MBrush");
            }
        }

        /// <summary>
        /// 勾选
        /// </summary>
        private bool m_check = true;
        public bool MCheck
        {
            get
            {
                return m_check;
            }
            set
            {
                m_check = value;
                OnPropertyChanged("MCheck");
            }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="check"></param>
        public StringBool(string name, bool check)
        {
            MName = name;
            MCheck = check;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="brush"></param>
        /// <param name="check"></param>
        public StringBool(string name, System.Windows.Media.Brush brush, bool check)
        {
            MName = name;
            MBrush = brush;
            MCheck = check;
        }
    }
}
