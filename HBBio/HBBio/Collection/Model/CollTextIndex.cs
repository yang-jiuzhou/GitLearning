using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Collection
{
    [Serializable]
    public struct CollTextIndex
    {
        public EnumCollIndexText MText;
        public int MIndex;
        public bool MStatus;

        public string MStr
        {
            get
            {
                if (MStatus)
                {
                    return MText.ToString() + MIndex;
                }
                else
                {
                    return "WASTE(" + MText.ToString() + MIndex + ")";
                }
            }
        }

        public CollTextIndex(EnumCollIndexText text, int index, bool status)
        {
            MText = text;
            MIndex = index;
            MStatus = status;
        }

        public CollTextIndex(EnumCollIndexText text, int index)
        {
            MText = text;
            MIndex = index;
            MStatus = true;
        }

        public CollTextIndex(bool status)
        {
            MText = EnumCollIndexText.L;
            MIndex = 1;
            MStatus = status;
        }

        public CollTextIndex(string textIndex, bool status)
        {
            if (textIndex.Contains("WASTE"))
            {
                textIndex = textIndex.Replace("WASTE(", "").Replace(")", "");
                if (textIndex.Contains("L"))
                {
                    MText = EnumCollIndexText.L;
                    MIndex = Convert.ToInt32(textIndex.Remove(0, 1));
                }
                else
                {
                    MText = EnumCollIndexText.R;
                    MIndex = Convert.ToInt32(textIndex.Remove(0, 1));
                }
            }
            else if (textIndex.Contains("L"))
            {
                MText = EnumCollIndexText.L;
                MIndex = Convert.ToInt32(textIndex.Remove(0, 1));
            }
            else if (textIndex.Contains("R"))
            {
                MText = EnumCollIndexText.R;
                MIndex = Convert.ToInt32(textIndex.Remove(0, 1));
            }
            else
            {
                MText = EnumCollIndexText.L;
                MIndex = 1;
            }

            MStatus = status;
        }

        public bool Equals(CollTextIndex item)
        {
            return MText == item.MText && MIndex == item.MIndex && MStatus == item.MStatus;
        }
    }
}
