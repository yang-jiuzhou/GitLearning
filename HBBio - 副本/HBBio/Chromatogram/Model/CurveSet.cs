using HBBio.Evaluation;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Chromatogram
{
    /**
    * ClassName: CurveSet
    * Description: 曲线集合类，一张运行中所有曲线的参数
    * Version: 1.0
    * Create:  2018/05/21
    * Author:  wangkai
    * Company: jshanbon
    **/
    public class CurveSet
    {
        //曲线列表
        public List<Curve> MItemList { get; }
        public List<PeakValue> MListPeak { get; set; }

        //当前选中曲线
        public int MSelectIndex { get; set; }

        //当前选中信号
        public Curve MSelectItem
        {
            get
            {
                if (-1 == MSelectIndex || MItemList.Count - 1 < MSelectIndex)
                {
                    return null;
                }
                else
                {
                    return MItemList[MSelectIndex];
                }
            }
        }

        public PeakValue MSelectPeakValue
        {
            get
            {
                if (-1 == MSelectIndex || null == MListPeak || 0 == MListPeak.Count)
                {
                    return null;
                }
                else
                {
                    return MListPeak[MSelectIndex];
                }
            }
        }
        public int MSelectPeakIndex { get; set; }

        //当前X轴类型
        public EnumBase MBase { get; set; }

        //当前X轴单位字符串
        public string MUnit
        {
            get
            {
                switch (MBase)
                {
                    case EnumBase.V: return DlyBase.SC_VUNITML;
                    case EnumBase.CV: return DlyBase.SC_CVUNIT;
                    default: return DlyBase.SC_TUNIT;
                }
            }
        }

        //X轴标尺手动自动
        public EnumAxisScale MAxisScale { get; set; }

        //X轴自动标尺上限
        public double MMaxAuto { get; set; }

        //X轴自动标尺下限
        public double MMinAuto { get; set; }

        //X轴手动标尺上限
        public double MMaxFix { get; set; }

        //X轴手动标尺下限
        public double MMinFix { get; set; }

        //最大值
        public double MMax
        {
            get
            {
                switch (MAxisScale)
                {
                    case EnumAxisScale.Fixed: return MMaxFix;
                    default: return MMaxAuto;
                }
            }
        }

        //最小值
        public double MMin
        {
            get
            {
                switch (MAxisScale)
                {
                    case EnumAxisScale.Fixed: return MMinFix;
                    default: return MMinAuto;
                }
            }
        }

        private List<double> m_TList = new List<double>();        //实时的时间数据列表
        private List<double> m_VList = new List<double>();        //实时的体积数据列表
        private List<double> m_CVList = new List<double>();       //实时的柱体积数据列表
        public List<double> MXList
        {
            get
            {
                switch (MBase)
                {
                    case EnumBase.V: return m_VList;
                    case EnumBase.CV: return m_CVList;
                    default: return m_TList;
                }
            }
        }

        public List<string> MListCurveName
        {
            get
            {
                List<string> list = new List<string>();
                list.Add("time(min)");
                list.Add("vol(ml)");
                list.Add("cv");
                foreach (var it in MItemList)
                {
                    list.Add(it.MName);
                }
                return list;
            }
        }

        public List<List<double>> MListList
        {
            get
            {
                List<List<double>> list = new List<List<double>>();
                list.Add(m_TList);
                list.Add(m_VList);
                list.Add(m_CVList);
                foreach (var it in MItemList)
                {
                    list.Add(it.MDrawData);
                }
                return list;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public CurveSet()
        {
            MItemList = new List<Curve>();
            MSelectIndex = -1;
            MSelectPeakIndex = -1;

            Clear();
        }

        /// <summary>
        /// 清除数据
        /// </summary>
        public void Clear()
        {
            MBase = EnumBase.T;

            //MAxisScale = EnumAxisScale.Auto;
            //MMaxAuto = 1.0;
            //MMinAuto = 0.0;
            //MMaxFix = 1.0;
            //MMinFix = 0.0;

            switch (MAxisScale)
            {
                case EnumAxisScale.Auto:
                    MMaxAuto = 1.0;
                    MMinAuto = 0.0;
                    MMaxFix = 1.0;
                    MMinFix = 0.0;
                    break;
            }

            m_TList.Clear();
            m_VList.Clear();
            m_CVList.Clear();

            foreach (var it in MItemList)
            {
                it.Clear();
            }
        }

        /// <summary>
        /// 初始化曲线信号
        /// </summary>
        /// <param name="list"></param>
        public void InitItemList(List<Curve> list)
        {
            MItemList.Clear();
            if (null != list)
            {
                foreach (var it in list)
                {
                    MItemList.Add(it);
                }
            }
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="t"></param>
        /// <param name="v"></param>
        /// <param name="cv"></param>
        /// <param name="dataList"></param>
        public void AddLineItemData(double t, double v, double cv, List<double> dataList)
        {
            m_TList.Add(t);
            m_VList.Add(v);
            m_CVList.Add(cv);

            switch (MAxisScale)
            {
                case EnumAxisScale.Auto:
                    double temp = MXList.Last();
                    if (temp > MMaxAuto * 0.9)
                    {
                        MMaxAuto = ValueTrans.GetTimes(MMaxAuto);
                    }
                    break;
            }

            for (int i = 0; i < MItemList.Count; i++)
            {
                MItemList[i].AddData(dataList[i]);
            }
        }
        public void RestoreLineItemData()
        {
            switch (MAxisScale)
            {
                case EnumAxisScale.Auto:
                    double temp = MXList.Last();
                    if (temp > MMaxAuto)
                    {
                        MMaxAuto = ValueTrans.GetTimes(temp);
                    }
                    break;
            }

            foreach (var it in MItemList)
            {
               it.RestoreData();
            }
        }
        public void ClearLineItemData()
        {
            MMaxAuto = 1.0;
            MMinAuto = 0.0;
            MMaxFix = 1.0;
            MMinFix = 0.0;

            m_TList.Clear();
            m_VList.Clear();
            m_CVList.Clear();

            foreach (var it in MItemList)
            {
                it.Clear();
            }
        }

        /// <summary>
        /// 获取指定类型的X列表
        /// </summary>
        /// <param name="enumBase"></param>
        /// <returns></returns>
        public List<double> GetXList(EnumBase enumBase)
        {
            switch (enumBase)
            {
                case EnumBase.V: return m_VList;
                case EnumBase.CV: return m_CVList;
                default: return m_TList;
            }
        }    
        
        public int GetIndex(double val)
        {
            if (0 == MXList.Count)
            {
                return -1;
            }
            for (int i = 0; i < MXList.Count - 1; i++)
            {
                if (val >= MXList[i] && val < MXList[i + 1])
                {
                    return i;
                }
            }

            return -1;
        } 

        /// <summary>
        /// 初始化源数据
        /// </summary>
        /// <param name="listPeak"></param>
        public void CalPeak(List<PeakValue> listPeak, double ch)
        {
            listPeak.Clear();
            PeakManager manger = new PeakManager();
            for (int i = 0; i < MItemList.Count; i++)
            {
                PeakValue item = new PeakValue();
                if (MItemList[i].MName.Contains("pH") || MItemList[i].MName.Contains("Cd") || MItemList[i].MName.Contains("UV"))
                {
                    item.Init(MXList, MItemList[i].MDrawData, ch);
                }
                listPeak.Add(item);
            }

            MListPeak = listPeak;
        }
    }
}
