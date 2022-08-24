using HBBio.Collection;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    public class CollValveCollectorVM : BaseGroupVM
    {
        public CollValveCollector MItem 
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

        public bool MEnabledColl
        {
            get
            {
                return MItem.MEnabledColl;
            }
            set
            {
                MItem.MEnabledColl = value;
                OnPropertyChanged("MEnabledColl");
            }
        }
        public EnumCollectionType MEnum
        {
            get
            {
                return MItem.MEnum;
            }
            set
            {
                MItem.MEnum = value;
                switch (value)
                {
                    case EnumCollectionType.Waste:
                        MEnabledColl = false;
                        break;
                    default:
                        MEnabledColl = true;
                        break;
                }
            }
        }
        public CollectionValve MValve
        {
            get
            {
                return MItem.MValve;
            }
            set
            {
                MItem.MValve = value;
            }
        }
        public CollectionCollector MCollector
        {
            get
            {
                return MItem.MCollector;
            }
            set
            {
                MItem.MCollector = value;
            }
        }

        private CollValveCollector m_item = null;


        /// <summary>
        /// 构造函数
        /// </summary>
        public CollValveCollectorVM(MethodBaseValue methodBaseValue) : base(methodBaseValue)
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
