using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Collection
{
    /**
     * ClassName: CollectionObjectSet
     * Description: 收集对象集合
     * Version: 1.0
     * Create:  2021/03/12
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    [Serializable]
    public class CollectionObjectMulti
    {
        public CollectionObject MObj1 { get; set; }
        public CollectionObject MObj2 { get; set; }
        public EnumRelation MRelation { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public CollectionObjectMulti()
        {
            MObj1 = new CollectionObject();
            MObj2 = new CollectionObject();
        }
    }

    /// <summary>
    /// 与或
    /// </summary>
    public enum EnumRelation
    {
        Only,
        With,
        Or,
        ContainOnly,
        ContainMulti
    }
}
