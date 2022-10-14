using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    class MixerVM : BaseGroupVM
    {
        public Mixer MItem
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

        public bool MOnoff
        {
            get
            {
                return MItem.MOnoff;
            }
            set
            {
                MItem.MOnoff = value;
            }
        }

        private Mixer m_item = null;

        public MixerVM(MethodBaseValue methodBaseValue) : base(methodBaseValue)
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
