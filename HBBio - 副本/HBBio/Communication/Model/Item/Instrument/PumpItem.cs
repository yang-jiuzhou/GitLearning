using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class PumpItem : BaseInstrument
    {
        public double m_maxFlowVol = 0; //最大流速
        public bool m_pause = false;    //是否暂停
        public double m_flowSet;        //流速(写)
        public double m_flowGet;        //流速(读)


        public PumpItem()
        {
            MConstNameList = Enum.GetNames(typeof(ENUMPumpName));
            MConstName = MConstNameList[0];
        }

        /// <summary>
        /// 返回列表
        /// </summary>
        /// <returns></returns>
        public override List<Signal> CreateSignalList()
        {
            List<Signal> result = new List<Signal>();

            //泵A关联总流速
            if (MConstName.Equals(ENUMPumpName.FITA.ToString()))
            {
                result.Add(new Signal("FIT_Total", "FIT_Total", DlyBase.SC_FITUNITML, 0, GetFlowMax(MModel), true, true));//体积流速
                result.Add(new Signal("FITLinear_Total", "FITLinear_Total", DlyBase.SC_LINEARFITUNIT, 0, 999999, true, false));//线性流速
            }

            //泵S没有百分比
            if (!MConstName.Equals(ENUMPumpName.FITS.ToString()))
            {
                result.Add(new Signal(MConstName + "_Per", MDlyName + "_Per", DlyBase.SC_FERUNIT, 0, 100, true, false));
            }

            result.Add(new Signal(MConstName, MDlyName, DlyBase.SC_FITUNITML, 0, GetFlowMax(MModel), true, true));//体积流速
            result.Add(new Signal(MConstName + "Linear", MDlyName + "Linear", DlyBase.SC_LINEARFITUNIT,  0, 999999, true, false));//线性流速

            return result;
        }

        public double GetFlowMax(string model)
        {
            double flowMax = 0;

            ENUMPumpID id = (ENUMPumpID)Enum.Parse(typeof(ENUMPumpID), model);

            switch (id)
            {
                case ENUMPumpID.NP7001: flowMax = 10; break;
                case ENUMPumpID.NP7005: flowMax = 50; break;
                case ENUMPumpID.NP7010: flowMax = 100; break;
                case ENUMPumpID.NP7030: flowMax = 300; break;
                case ENUMPumpID.NP7060: flowMax = 600; break;
                case ENUMPumpID.P1001L: flowMax = 1000; break;
                case ENUMPumpID.P1003L: flowMax = 3000; break;
                case ENUMPumpID.OEM0025: flowMax = 30; break;
                case ENUMPumpID.OEM0030: flowMax = 30; break;
                case ENUMPumpID.OEM0100: flowMax = 100; break;
                case ENUMPumpID.OEM0300: flowMax = 300; break;
                case ENUMPumpID.HB0030: flowMax = 30; break;
            }

            return flowMax;
        }
    }
}
