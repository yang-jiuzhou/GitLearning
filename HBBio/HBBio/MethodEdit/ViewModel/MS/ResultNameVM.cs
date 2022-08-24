using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    public class ResultNameVM
    {
        public ResultName MItem { get; set; }

        public EnumResultType MType
        {
            get
            {
                return MItem.MType;
            }
            set
            {
                MItem.MType = value;
            }
        }
        public string MDlyName
        {
            get
            {
                return MItem.MDlyName;
            }
            set
            {
                MItem.MDlyName = value;
            }
        }
        public bool MUnique
        {
            get
            {
                return MItem.MUnique;
            }
            set
            {
                MItem.MUnique = value;
            }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        public ResultNameVM()
        {
            MItem = new ResultName();
        }
    }
}
