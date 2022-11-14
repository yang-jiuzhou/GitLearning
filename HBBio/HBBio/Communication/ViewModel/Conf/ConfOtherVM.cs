using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class ConfOtherVM
    {
        #region 属性
        public ConfOther MItem { get; set; }

        public bool MResetValve
        {
            get
            {
                return MItem.MResetValve;
            }
            set
            {
                MItem.MResetValve = value;
            }
        }
        public bool MCloseUV
        {
            get
            {
                return MItem.MCloseUV;
            }
            set
            {
                MItem.MCloseUV = value;
            }
        }
        public bool MOpenMixer 
        {
            get
            {
                return MItem.MOpenMixer;
            }
            set
            {
                MItem.MOpenMixer = value;
            }
        }
        /// <summary>
        /// PID参数的P
        /// </summary>
        public double MPIDP
        {
            get
            {
                return MItem.MPIDP;
            }
            set
            {
                MItem.MPIDP = value;
            }
        }
        /// <summary>
        /// PID参数的P
        /// </summary>
        public double MPIDI
        {
            get
            {
                return MItem.MPIDI;
            }
            set
            {
                MItem.MPIDI = value;
            }
        }
        /// <summary>
        /// PID参数的P
        /// </summary>
        public double MPIDD
        {
            get
            {
                return MItem.MPIDD;
            }
            set
            {
                MItem.MPIDD = value;
            }
        }
        public bool MUVIJV
        {
            get
            {
                return MItem.MUVIJV;
            }
            set
            {
                MItem.MUVIJV = value;
            }
        }
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="item"></param>
        public ConfOtherVM(ConfOther item)
        {
            MItem = item;
        }
    }
}
