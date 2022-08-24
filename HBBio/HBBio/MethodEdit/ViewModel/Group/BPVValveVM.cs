using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    class BPVValveVM : BaseGroupVM
    {
        public BPVValve MItem 
        {
            get
            {
                return m_item;
            }
            set
            {
                m_item = value;
                MType = value.MType;
            }
        }

        public int MBPV
        {
            get
            {
                return MItem.MBPV;
            }
            set
            {
                MItem.MBPV = value;
            }
        }

        private BPVValve m_item = null;

        public BPVValveVM(MethodBaseValue methodBaseValue) : base(methodBaseValue)
        {

        }

        /// <summary>
        /// 改变基本单位
        /// </summary>
        /// <param name="methodBaseValue"></param>
        public override void ChangeEnumBase(MethodBaseValue methodBaseValue)
        {
            MMethodBaseValue = methodBaseValue;
        }
    }
}
