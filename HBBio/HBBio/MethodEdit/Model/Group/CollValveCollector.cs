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
    }
}
