using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace HBBio.ColumnList
{
    public delegate void PVUEventHandler(object sender);

    /**
     * ClassName: ParametersValueUnit
     * Description: 具体参数项属性
     * Version: 1.0
     * Create:  2018/05/16
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class ParametersValueUnit : DlyNotifyPropertyChanged
    {
        public event PVUEventHandler MChangedEvent;

        /// <summary>
        /// 参数
        /// </summary>
        public string MName { get; set; }

        /// <summary>
        /// 值(文本输入)
        /// </summary>
        private string m_text = null;
        public string MText
        {
            get
            {
                return m_text;
            }
            set
            {
                if (null != m_text && m_text.Equals(value))
                {
                    return;
                }

                m_text = value;
                OnPropertyChanged("MText");

                if (Visibility.Visible == MShowValue)
                {
                    if (string.IsNullOrEmpty(m_text))
                    {
                        m_value = -1;
                    }
                    else
                    {
                        m_value = Convert.ToDouble(m_text);
                    }

                    MChangedEvent(m_number);
                }
            }
        }

        /// <summary>
        /// 值(下拉框选择)
        /// </summary>
        private int m_index = -1;
        public int MIndex
        {
            get
            {
                return m_index;
            }
            set
            {
                m_showText = Visibility.Hidden;
                m_showCombobox = Visibility.Visible;

                if (-1 != m_index && m_index == value)
                {
                    return;
                }

                m_index = value;
                OnPropertyChanged("MIndex");

                if (null != MCbox)
                {
                    m_text = MCbox[m_index];
                }

                MChangedEvent(m_number);
            }
        }

        /// <summary>
        /// 单位
        /// </summary>
        private string m_unit = null;
        public string MUnit
        {
            get
            {
                return m_unit;
            }
            set
            {
                if (null == m_unit)
                {
                    m_unit = value;
                    OnPropertyChanged("MUnit");
                }
                else
                {
                    if (m_unit.Equals(value))
                    {
                        return;
                    }

                    MValue = ValueTrans.CalFlowUnit(MValue, value, m_unit);

                    m_unit = value;
                    OnPropertyChanged("MUnit");
                }
            }
        }

        /// <summary>
        /// 以文本方式显示(默认)
        /// </summary>
        private Visibility m_showText = Visibility.Hidden;
        public Visibility MShowText
        {
            get
            {
                return m_showText;
            }
        }
        public string MShowTextStr
        {
            set
            {
                m_showText = Visibility.Visible;
                MText = value;
            }
        }

        /// <summary>
        /// 以数值方式显示
        /// </summary>
        private Visibility m_showValue = Visibility.Hidden;
        public Visibility MShowValue
        {
            get
            {
                return m_showValue;
            }
        }
        public string MshowValueStr
        {
            set
            {
                m_showValue = Visibility.Visible;
                MText = value;
            }
        }

        /// <summary>
        /// 以下拉框方式显示
        /// </summary>
        private Visibility m_showCombobox = Visibility.Hidden;
        public Visibility MShowCombobox
        {
            get
            {
                return m_showCombobox;
            }
        }
        public string MshowComboboxStr
        {
            set
            {
                m_showCombobox = Visibility.Visible;
                if (string.IsNullOrEmpty(value))
                {
                    MIndex = 0;
                }
                else
                {
                    MIndex = Convert.ToInt32(value);
                }
            }
        }

        /// <summary>
        /// 转换成字符串进行存储
        /// </summary>
        public string MStr
        {
            get
            {
                if (Visibility.Visible == MShowCombobox)
                {
                    return MIndex.ToString();
                }
                else
                {
                    return MText;
                }
            }
        }

        /// <summary>
        /// 数值计算
        /// </summary>
        private double m_value = -1;
        public double MValue
        {
            get
            {
                return m_value;
            }
            set
            {
                m_value = value;
                if (m_value > 0)
                {
                    MText = m_value.ToString("f2");
                }
                else
                {
                    MText = "";
                }
            }
        }

        /// <summary>
        /// 下拉框列表
        /// </summary>
        public string[] MCbox { get; set; }

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool MReadOnly { get; set; }

        /// <summary>
        /// 用于判断是哪个参数发生变化
        /// </summary>
        private readonly int m_number = -1;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="number"></param>
        public ParametersValueUnit(int number)
        {
            m_number = number;

            MChangedEvent += new PVUEventHandler(NullChanged);
        }

        /// <summary>
        /// 比较是否相等
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Compared(ParametersValueUnit other)
        {
            if (null == other)
            {
                return false;
            }
            else
            {
                if (!string.Equals(MName, other.MName))
                {
                    return false;
                }

                if (!string.Equals(MText, other.MText))
                {
                    return false;
                }

                if (MIndex != other.MIndex)
                {
                    return false;
                }

                if (!string.Equals(MUnit, other.MUnit))
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// 空处理
        /// </summary>
        /// <param name="sender"></param>
        private void NullChanged(object sender)
        {
    
        }
    }
}
