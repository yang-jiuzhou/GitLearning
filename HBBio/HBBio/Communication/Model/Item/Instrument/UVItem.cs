using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class UVItem : BaseInstrument
    {
        private const int c_UVCount = 4;
        public readonly int m_signalCount = 2;                      //通道数量
        public int[] m_waveSet = new int[c_UVCount];                //波长
        public int[] m_waveGet = new int[c_UVCount];                //波长
        public double[] m_absGet = new double[c_UVCount];           //吸收值
        private Queue<double>[] m_arrSmooth = new Queue<double>[c_UVCount];

        private bool m_lamp = false;                                 //灯状态
        public bool MLamp
        {
            get
            {
                return m_lamp;
            }
            set
            {
                m_lamp = value;
                OnPropertyChanged("MLamp");
            }
        }
        private double m_timeGet = 0;                                //亮灯时间
        public double MTime
        {
            get
            {
                return m_timeGet;
            }
            set
            {
                m_timeGet = value;
                OnPropertyChanged("MTime");
            }
        }

        private int m_ref = 0;                               //参比能量
        private int m_sig = 0;                               //样比能量
        public int MRef
        {
            get
            {
                return m_ref;
            }
            set
            {
                m_ref = value;
                OnPropertyChanged("MRef");
            }
        }
        public int MSig
        {
            get
            {
                return m_sig;
            }
            set
            {
                m_sig = value;
                OnPropertyChanged("MSig");
            }
        }

        public volatile bool m_wave = false;             //波长
        public volatile bool m_clear = false;            //清零
        public volatile bool m_lampOn = false;           //开灯
        public volatile bool m_lampOff = false;          //关灯

        //创建一个自定义委托，用于自定义的信号
        public delegate void MHandlerDdelegateIJV(object val);
        //声明一个手动进样阀事件
        public MHandlerDdelegateIJV MIJVHandler;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="signalCount"></param>
        public UVItem(int signalCount)
        {
            m_signalCount = signalCount;

            MConstNameList = Enum.GetNames(typeof(ENUMUVName));
            MConstName = MConstNameList[0];

            for (int i = 0; i < m_arrSmooth.Length; i++)
            {
                m_arrSmooth[i] = new Queue<double>();
            }
        }

        /// <summary>
        /// 返回列表
        /// </summary>
        /// <returns></returns>
        public override List<Signal> CreateSignalList()
        {
            List<Signal> result = new List<Signal>();

            result.Add(new Signal(MConstName, MDlyName, "", 0, 0, false, false));
            if (MModel.Contains("ECOM"))
            {
                for (int i = 0; i < m_signalCount; i++)
                {
                    result.Add(new Signal(MConstName + "_" + (i + 1) + "_Wave", MDlyName + "_" + (i + 1) + "_Wave", DlyBase.SC_UVWAVEUNIT, 200, 800, false, false));
                }
            }
            else
            {
                for (int i = 0; i < m_signalCount; i++)
                {
                    result.Add(new Signal(MConstName + "_" + (i + 1) + "_Wave", MDlyName + "_" + (i + 1) + "_Wave", DlyBase.SC_UVWAVEUNIT, 190,700, false, false));
                }
            }
            for (int i = 0; i < m_signalCount; i++)
            {
                result.Add(new Signal(MConstName + "_" + (i + 1), MDlyName + "_" + (i + 1), DlyBase.SC_UVABSUNIT, -5000, 5000, true, true));
            }

            return result;
        }

        public void UpdateAbs(double[] val)
        {
            for (int i = 0; i < c_UVCount; i++)
            {
                m_arrSmooth[i].Enqueue(val[i]);
            }
            if (m_arrSmooth[0].Count > 5)
            {
                foreach (var it in m_arrSmooth)
                {
                    it.Dequeue();
                }
            }
            for (int i = 0; i < c_UVCount; i++)
            {
                m_absGet[i] = Math.Round(m_arrSmooth[i].Average(), 2);
            }
        }
    }
}
