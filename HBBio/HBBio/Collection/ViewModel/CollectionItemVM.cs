using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.Collection
{
    /**
     * ClassName: CollectionItemVM
     * Description: 收集单元
     * Version: 1.0
     * Create:  2021/03/12
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class CollectionItemVM : DlyNotifyPropertyChanged
    {
        #region 属性
        public CollectionItem MItem { get; set; }

        public CollectionObjectMultiVM MCond { get; set; }
        public EnumPositionType MPositionType
        {
            get
            {
                return MItem.MPositionType;
            }
            set
            {
                MItem.MPositionType = value;
                switch (value)
                {
                    case EnumPositionType.Fixed:
                        MLoop.MVisible = Visibility.Collapsed;
                        break;
                    case EnumPositionType.Loop:
                        MLoop.MVisible = Visibility.Visible;
                        break;
                }
            }
        }
        public EnumPositionStart MPositionStart
        {
            get
            {
                return MItem.MPositionStart;
            }
            set
            {
                MItem.MPositionStart = value;
            }
        }
        public string MStart
        {
            get
            {
                switch (MItem.MPositionStart)
                {
                    case EnumPositionStart.Left:
                        return EnumCollIndexText.L.ToString() + MItem.MStartIndex;
                    case EnumPositionStart.Right:
                        return EnumCollIndexText.R.ToString() + MItem.MStartIndex;
                    default:
                        return ReadXamlCollection.S_Default;
                }
            }
            set
            {
                if (value.Contains("L"))
                {
                    MItem.MPositionStart = EnumPositionStart.Left;
                    MItem.MStartIndex = Convert.ToInt32(value.Remove(0, 1));
                }
                else if (value.Contains("R"))
                {
                    MItem.MPositionStart = EnumPositionStart.Right;
                    MItem.MStartIndex = Convert.ToInt32(value.Remove(0, 1));
                }
                else
                {
                    MItem.MPositionStart = EnumPositionStart.Default;
                }
            }
        }
        public CollectionObjectMultiVM MLoop { get; set; }
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="item"></param>
        public CollectionItemVM(CollectionItem item)
        {
            MItem = item;
            MCond = new CollectionObjectMultiVM(item.MCond);
            MLoop = new CollectionObjectMultiVM(item.MLoop);

            MPositionType = item.MPositionType;
        }
    }
}
