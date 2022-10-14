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
     * ClassName: CollectionObjectMultiVM
     * Description: 收集对象集合
     * Version: 1.0
     * Create:  2021/03/12
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class CollectionObjectMultiVM : DlyNotifyPropertyChanged
    {
        #region 属性
        public CollectionObjectMulti MItem { get; set; }

        public CollectionObjectVM MObj1 { get; set; }
        public CollectionObjectVM MObj2 { get; set; }
        public EnumRelation MRelation
        {
            get
            {
                return MItem.MRelation;
            }
            set
            {
                MItem.MRelation = value;
                switch (MRelation)
                {
                    case EnumRelation.Only:
                        MVisibilityObj2 = Visibility.Collapsed;
                        break;
                    default:
                        MVisibilityObj2 = Visibility.Visible;
                        break;
                }
            }
        }

        public Visibility MVisibilityObj2
        {
            get
            {
                switch (MRelation)
                {
                    case EnumRelation.Only:
                        return Visibility.Collapsed;
                    default:
                        return Visibility.Visible;
                }
            }
            set
            {
                OnPropertyChanged("MVisibilityObj2");
            }
        }

        private Visibility m_visible = Visibility.Collapsed;
        public Visibility MVisible
        {
            get
            {
                return m_visible;
            }
            set
            {
                m_visible = value;
                OnPropertyChanged("MVisible");
            }
        }
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        public CollectionObjectMultiVM(CollectionObjectMulti item)
        {
            MItem = item;
            MObj1 = new CollectionObjectVM(MItem.MObj1);
            MObj2 = new CollectionObjectVM(MItem.MObj2);
        }
    }
}
