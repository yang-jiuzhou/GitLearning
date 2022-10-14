using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace HBBio.Communication
{
    /**
     * ClassName: Signal
     * Description: 信号
     * Version: 1.0
     * Create:  2020/05/16
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    public class Signal
    {
        public int MBaseInstrumentId { get; set; }

        private int m_id = -1;
        private string m_constName = "";
        private string m_dlyName = "";
        private Brush m_brush = Brushes.Black;
        private string m_unit = "";
        private Color m_colorNew = Colors.Red;
        private bool m_showNew = true;
        private Color m_colorOld = Colors.Red;
        private bool m_showOld = true;
        private bool m_contrastOld = false;
        private double m_valLL = 0;
        private double m_valL = 0;
        private double m_valH = 0;
        private double m_valHH = 0;
        private double m_valMin = 0;
        private double m_valMax = 0;
        private int m_smooth = 1;

        public int MId
        {
            get
            {
                return m_id;
            }
        }
        public string MConstName
        {
            get
            {
                return m_constName;
            }
            set
            {
                m_constName = value;
            }
        }
        public string MDlyName
        {
            get
            {
                return m_dlyName;
            }
            set
            {
                m_dlyName = value;
            }
        }
        public Brush MBrush
        {
            get
            {
                return m_brush;
            }
            set
            {
                m_brush = value;
            }
        }
        public string MUnit
        {
            get
            {
                return m_unit;
            }
        }
        public Color MColorNew
        {
            get
            {
                return m_colorNew;
            }
            set
            {
                m_colorNew = value;
            }
        }
        public bool MShowNew
        {
            get
            {
                return m_showNew;
            }
            set
            {
                m_showNew = value;
            }
        }
        public Color MColorOld
        {
            get
            {
                return m_colorOld;
            }
            set
            {
                m_colorOld = value;
            }
        }
        public bool MShowOld
        {
            get
            {
                return m_showOld;
            }
            set
            {
                m_showOld = value;
            }
        }
        public bool MContrastOld
        {
            get
            {
                return m_contrastOld;
            }
            set
            {
                m_contrastOld = value;
            }
        }
        public double MValLL
        {
            get
            {
                return m_valLL;
            }
            set
            {
                m_valLL = value;
            }
        }
        public double MValL
        {
            get
            {
                return m_valL;
            }
            set
            {
                m_valL = value;
            }
        }
        public double MValH
        {
            get
            {
                return m_valH;
            }
            set
            {
                m_valH = value;
            }
        }
        public double MValHH
        {
            get
            {
                return m_valHH;
            }
            set
            {
                m_valHH = value;
            }
        }
        public double MValMin
        {
            get
            {
                return m_valMin;
            }
            set
            {
                m_valMin = value;
            }
        }
        public double MValMax
        {
            get
            {
                return m_valMax;
            }
            set
            {
                m_valMax = value;
            }
        }
        public int MSmooth
        {
            get
            {
                return m_smooth;
            }
            set
            {
                m_smooth = value;
            }
        }

        public bool MIsLine { get; set; }           //是否是曲线（例如紫外亮灯时间不需要）
        public bool MIsAlarmWarning { get; set; }
        public bool MIsShow { get; set; }           //是否显示

        private double m_realValue = 0;
        private double m_smoothValue = 0;
        public double MRealValue
        {
            get
            {
                return m_smoothValue;
            }
            set
            {
                m_realValue = value;
                m_queue.Enqueue(value);
                while (m_queue.Count > MSmooth)
                {
                    m_queue.Dequeue();
                }
                m_smoothValue = m_queue.Average();
            }
        }
        public Queue<double> m_queue = new Queue<double>();

        private static int s_random = 1;


        /// <summary>
        /// 构造函数，新建对象使用
        /// </summary>
        /// <param name="constName"></param>
        /// <param name="dlyName"></param>
        /// <param name="signalName"></param>
        /// <param name="unit"></param>
        public Signal(string constName, string dlyName, string unit, double valMin, double valMax, bool isLine, bool isAlarmWarning)
        {
            m_constName = constName;
            m_dlyName = dlyName;
            m_unit = unit;

            Random rd = new Random(s_random++);
            int int_Red = rd.Next(256);
            int int_Green = rd.Next(256);
            int int_Blue = (int_Red + int_Green > 400) ? 0 : 400 - int_Red - int_Green;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;
            MColorNew = Color.FromArgb(255, (byte)int_Red, (byte)int_Green, (byte)int_Blue);
            MColorOld = MColorNew;

            MValMin = valMin;
            MValL = valMin;
            MValLL = valMin;
            MValMax = valMax;
            MValHH = valMax;
            MValH = valMax;
            MIsLine = isLine;
            MIsAlarmWarning = isAlarmWarning;
            MIsShow = true;

            if (constName.Contains("PT") || constName.Contains("pH") || constName.Contains("Cd") || constName.Contains("UV"))
            {
                m_smooth = 1;
            }
        }

        /// <summary>
        /// 构造函数，数据库使用
        /// </summary>
        /// <param name="constName"></param>
        /// <param name="dlyName"></param>
        /// <param name="signalName"></param>
        /// <param name="unit"></param>
        /// <param name="colorNew"></param>
        /// <param name="showNew"></param>
        /// <param name="colorOld"></param>
        /// <param name="showOld"></param>
        public Signal(int id, string constName, string dlyName, Brush brush, string unit, Color colorNew, bool showNew, Color colorOld, bool showOld, bool contrastOld
            , double valLL, double valL, double valH, double valHH, double valMin, double valMax
            , int smooth
            , bool isLine, bool isAlarmWarning, bool isShow
            , int baseInstrumentId)
        {
            m_id = id;
            MConstName = constName;
            MDlyName = dlyName;
            m_brush = brush;
            m_unit = unit;
            MColorNew = colorNew;
            MShowNew = showNew;
            MColorOld = colorOld;
            MShowOld = showOld;
            MContrastOld = contrastOld;

            MValLL = valLL;
            MValL = valL;
            MValH = valH;
            MValHH = valHH;
            MValMin = valMin;
            MValMax = valMax;
            MSmooth = smooth;
            MIsLine = isLine;
            MIsAlarmWarning = isAlarmWarning;
            MIsShow = isShow;

            MBaseInstrumentId = baseInstrumentId;
        }
    }
}
