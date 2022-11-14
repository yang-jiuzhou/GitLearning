using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Share
{
    [Serializable]
    public class Dispensing
    {
        //private bool m_PumpAIng = false;
        //private bool m_PumpAEnableFlag = false;
        //private DateTime m_PumpAENableTime = DateTime.Now;
        //private bool m_PumpADisableFlag = false;
        //private DateTime m_PumpADisableTime = DateTime.Now;
        //private int m_hold = 5;                 //稳定5秒
        //private int m_error = 2;                //超过2秒异常，则认为变成不合格
        private Incremental m_incremental = null;
        

        public Dispensing()
        {

        }

        public Dispensing(double p, double i, double d)
        {
            m_incremental = new Incremental(p, i, d);
        }

        /// <summary>
        /// 更新实时流速
        /// </summary>
        public void PTControlFlow(double ptBase, double ptNow, ref double flow, double flowMax)
        {
            double flowNew = flow + m_incremental.Control(ptBase, ptNow);

            if (flowNew < 0)
            {
                flowNew = 0;
            }
            else if (flowNew > flowMax)
            {
                flowNew = flowMax;
            }

            flow = flowNew;

            //if (m_PumpAIng)//满足条件
            //{
            //    if (ptBase != ptNow)//出现连续不满足的意外
            //    {
            //        if (m_PumpADisableFlag)
            //        {
            //            if ((DateTime.Now - m_PumpADisableTime).TotalSeconds >= m_error)
            //            {
            //                m_PumpAIng = false;
            //                m_PumpADisableFlag = false;
            //            }
            //        }
            //        else
            //        {
            //            m_PumpADisableTime = DateTime.Now;
            //            m_PumpADisableFlag = true;
            //        }
            //    }
            //    else
            //    {
            //        m_PumpADisableFlag = false;
            //    }
            //}
            //else
            //{
            //    if (ptBase == ptNow)
            //    {
            //        if (m_PumpAEnableFlag)
            //        {
            //            if ((DateTime.Now - m_PumpAENableTime).TotalSeconds >= m_hold)
            //            {
            //                m_PumpAIng = true;
            //                m_PumpAEnableFlag = false;
            //            }
            //        }
            //        else
            //        {
            //            m_PumpAENableTime = DateTime.Now;
            //            m_PumpAEnableFlag = true;
            //        }
            //    }
            //    else
            //    {
            //        m_PumpAEnableFlag = false;
            //    }
            //}
        }
    }
}
