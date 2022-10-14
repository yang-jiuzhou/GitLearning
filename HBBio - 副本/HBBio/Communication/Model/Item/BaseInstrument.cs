using HBBio.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HBBio.Communication
{
    /**
     * ClassName: BaseInstrument
     * Description: 仪器元素基类
     * Version: 1.0
     * Create:  2020/05/16
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    public class BaseInstrument : DlyNotifyPropertyChanged
    {
        private ENUMInstrumentType m_type = ENUMInstrumentType.Valve;       //类型(仅作显示用途)(来自上级)  
        public ENUMInstrumentType MType
        {
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
                List<string> list = Share.ReadXaml.GetEnumList<ENUMInstrumentType>("Com_");
                if (null != list && (int)MType < list.Count)
                {
                    MName = list[(int)MType];
                }
                else
                {
                    MName = MType.ToString();
                }
            }
        }
        public string MName { get; set; }               //类型字符(仅作显示用途)(来自上级)
        public string MModel { get; set; }              //型号(仅作显示用途)(来自上级)
        public string MPortName { get; set; }           //串口号(仅作显示用途)(来自上级)
        public string MAddress { get; set; }            //IP地址(仅作显示用途)(来自上级)
        public string MPort { get; set; }               //端口号(仅作显示用途)(来自上级)

        public double MSetTime { get; set; }            //设定时间(来自时间表)
        public double MRunTime { get; set; }            //运行时间(来自时间表)
        public DateTime MCalibration { get; set; }      //校准日期(来自时间表)


        public string[] MConstNameList { get; set; }    //标识名集合(仅作显示用途)
        public bool MVisible { get; set; }              //是否可用，可能被删除


        private int m_id = -1;                          //-1表示新建，非-1表示从数据库读取(数据库)                                 
        public int MId
        {
            get
            {
                return m_id;
            }
            set
            {
                m_id = value;
            }
        }

        public int MIndex { get; set; }                 //一个ComConf可能包含多个Instrument(数据库) 

        protected string m_constName = "";              //标识名(数据库) 
        public string MConstName
        {
            get
            {
                return m_constName;
            }
            set
            {
                m_constName = value;

                MDlyName = m_constName;
            }
        }

        protected string m_dlyName = "";                //自定义名(数据库) 
        public string MDlyName
        {
            get
            {
                return m_dlyName;
            }
            set
            {
                m_dlyName = value;
                OnPropertyChanged("MDlyName");
            }
        }
 
        public bool MEnableRunTime { get; set; }        //启用运行报警(数据库)      
        public bool MEnableCalibration { get; set; }    //启用校准报警(数据库) 

        public Point MPt { get; set; }                  //流路图位置(数据库) 

        public int MComConfId { get; set; }             //配置表中的ID(数据库) 
        public int MTimeSetId { get; set; }             //时间表中的ID(数据库) 

        private SolidColorBrush m_brush = Brushes.White;
        public SolidColorBrush MBrush
        {
            get
            {
                return m_brush;
            }
            set
            {
                if (m_brush == value)
                {
                    return;
                }
                m_brush = value;
                OnPropertyChanged("MBrush");
            }
        }

        private SolidColorBrush m_foreground = Brushes.Gray;
        public SolidColorBrush MForeground
        {
            get
            {
                return m_foreground;
            }
            set
            {
                if (m_foreground == value)
                {
                    return;
                }
                m_foreground = value;
                OnPropertyChanged("MForeground");
            }
        }

        public List<Signal> m_list = new List<Signal>();       //列表


        /// <summary>
        /// 构造函数，默认使用
        /// </summary>
        public BaseInstrument()
        {
            MEnableRunTime = true;
            MEnableCalibration = true;
            MPt = new Point(0, 0);
            MComConfId = -1;
            MTimeSetId = -1;

            MVisible = true;
        }

        /// <summary>
        /// 构造函数，数据库使用
        /// </summary>
        /// <param name="id"></param>
        /// <param name="constName"></param>
        /// <param name="dlyName"></param>
        /// <param name="setTime"></param>
        /// <param name="enableRunTime"></param>
        /// <param name="enableCalibration"></param>
        /// <param name="comConfID"></param>
        /// <param name="runTimeID"></param>
        public void GetDataFromDB(int id, int index, string constName, string dlyName, bool enableRunTime, bool enableCalibration, Point pt, int comConfID, int runTimeID)
        {
            m_id = id;
            MIndex = index;
            MConstName = constName;
            MDlyName = dlyName;
            MEnableRunTime = enableRunTime;
            MEnableCalibration = enableCalibration;
            MPt = pt;
            MComConfId = comConfID;
            MTimeSetId = runTimeID;

            MVisible = true;
        }

        /// <summary>
        /// 返回信号列表(基类需继承)
        /// </summary>
        /// <returns></returns>
        public virtual List<Signal> CreateSignalList()
        {
            List<Signal> result = new List<Signal>();

            result.Add(new Signal(MConstName, MDlyName, "", 0, 0, false, false));

            return result;
        }
    }
}
