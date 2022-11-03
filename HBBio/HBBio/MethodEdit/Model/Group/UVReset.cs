using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    [Serializable]
    public class UVReset : BaseGroup
    {
        public bool MEnableResetUV { get; set; }

        public UVReset()
        {
            MType = EnumGroupType.UVReset;

            MEnableResetUV = true;
        }

        public override bool Compare(BaseGroup baseItem)
        {
            UVReset item = (UVReset)baseItem;
            return MEnableResetUV == item.MEnableResetUV;
        }
    }
}
