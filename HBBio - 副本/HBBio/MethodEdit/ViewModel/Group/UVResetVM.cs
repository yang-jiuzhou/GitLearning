using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    class UVResetVM : BaseGroupVM
    {
        public UVReset MItem
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

        public bool MEnableResetUV
        {
            get
            {
                return MItem.MEnableResetUV;
            }
            set
            {
                MItem.MEnableResetUV = value;
            }
        }

        private UVReset m_item = null;

        public UVResetVM(MethodBaseValue methodBaseValue) : base(methodBaseValue)
        {

        }

        /// <summary>
        /// 改变基本单位
        /// </summary>
        /// <param name="methodBaseValue"></param>
        public override void ChangeEnumBase(MethodBaseValue methodBaseValue)
        {

        }
    }
}
