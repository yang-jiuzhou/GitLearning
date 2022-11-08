using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: MixtureGrid
     * Description: 混合列表
     * Version: 1.0
     * Create:  2020/05/27
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    [Serializable]
    public class MixtureGrid : BaseGroup
    {
        /// <summary>
        /// 行数据列表
        /// </summary>
        public List<MixtureGridItem> MList { get; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public MixtureGrid()
        {
            MType = EnumGroupType.MixtureGrid;

            MList = new List<MixtureGridItem>();
        }

        public override bool Compare(BaseGroup baseItem)
        {
            MixtureGrid item = (MixtureGrid)baseItem;

            if (MList.Count != item.MList.Count)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < MList.Count; i++)
                {
                    if (!MList[i].Compare(item.MList[i]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
