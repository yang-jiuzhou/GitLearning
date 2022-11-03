using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: FlowRatePer
     * Description: 流速百分比梯度
     * Version: 1.0
     * Create:  2020/05/27
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    [Serializable]
    public class FlowRatePer : BaseGroup
    {
        //行数据列表
        public ObservableCollection<FlowRatePerItem> MList { get; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public FlowRatePer()
        {
            MType = EnumGroupType.FlowRatePer;

            MList = new ObservableCollection<FlowRatePerItem>();
        }

        public override bool Compare(BaseGroup baseItem)
        {
            FlowRatePer item = (FlowRatePer)baseItem;
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

                return true;
            }
        }
    }
}
