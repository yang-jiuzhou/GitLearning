using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Collection
{
    /**
     * ClassName: CollectionItem
     * Description: 收集单元
     * Version: 1.0
     * Create:  2021/03/12
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    [Serializable]
    public class CollectionItem
    {
        public CollectionObjectMulti MCond { get; set; }
        public EnumPositionType MPositionType { get; set; }
        public EnumPositionStart MPositionStart { get; set; }
        public int MStartIndex { get; set; }
        public CollectionObjectMulti MLoop { get; set; }
        public string MShowInfo { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public CollectionItem()
        {
            MCond = new CollectionObjectMulti();
            MStartIndex = 1;
            MLoop = new CollectionObjectMulti();
        }
    }

    /// <summary>
    /// 起点选择
    /// </summary>
    public enum EnumPositionStart
    {
        Default,
        Left,
        Right,
        Out
    }

    /// <summary>
    /// 固定循环
    /// </summary>
    public enum EnumPositionType
    {
        Fixed,
        Loop
    }

    /// <summary>
    /// 收集的类型
    /// </summary>
    public enum EnumCollectionType
    {
        Waste,
        Valve,
        Collector
    }

    public enum EnumCollIndexText
    {
        L,
        R,
        Out
    }

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
                    return "WASTE";
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

        public bool Equals(CollTextIndex item)
        {
            return MText == item.MText && MIndex == item.MIndex && MStatus == item.MStatus;
        }
    }
}
