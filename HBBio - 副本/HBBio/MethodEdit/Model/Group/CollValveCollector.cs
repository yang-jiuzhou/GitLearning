using HBBio.Collection;
using HBBio.Communication;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    [Serializable]
    public class CollValveCollector : BaseGroup
    {
        public bool MEnabledColl { get; set; }
        public EnumCollectionType MEnum { get; set; }
        public CollectionValve MValve { get; set; }
        public CollectionCollector MCollector { get; set; }
        

        /// <summary>
        /// 构造函数
        /// </summary>
        public CollValveCollector()
        {
            MType = EnumGroupType.CollValveCollector;

            MEnabledColl = false;
            MValve = new CollectionValve();
            MCollector = new CollectionCollector();
            MEnum = EnumCollectionType.Waste;
        }

        public override bool Compare(BaseGroup baseItem)
        {
            CollValveCollector item = (CollValveCollector)baseItem;

            if (MEnabledColl != item.MEnabledColl
                || MEnum != item.MEnum)
            {
                return false;
            }

            if (MValve.MList.Count != item.MValve.MList.Count)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < MValve.MList.Count; i++)
                {
                    if (!MValve.MList[i].MShowInfo.Equals(item.MValve.MList[i].MShowInfo))
                    {
                        return false;
                    }
                }
            }

            if (MCollector.MList.Count != item.MCollector.MList.Count)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < MCollector.MList.Count; i++)
                {
                    if (!MCollector.MList[i].MShowInfo.Equals(item.MCollector.MList[i].MShowInfo))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
