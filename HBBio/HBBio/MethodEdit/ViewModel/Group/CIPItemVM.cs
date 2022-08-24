using HBBio.Communication;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    public class CIPItemVM : DlyNotifyPropertyChanged
    {
        public CIPItem MItem { get; set; }

        public bool MIsSelected
        {
            get
            {
                return MItem.MIsSelected;
            }
            set
            {
                if (MItem.MIsSelected == value)
                {
                    return;
                }

                MItem.MIsSelected = value;
                OnPropertyChanged("MIsSelected");

                MCheckHandler?.Invoke(value, MType, MValveName);
            }
        }
        public ENUMValveName MType 
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
        public string MValveName
        {
            get
            {
                return MItem.MValveName;
            }
            set
            {
                MItem.MValveName = value;
            }
        }

        //创建一个自定义委托，用于发送勾选状态
        public delegate void MCheckDdelegate(object sender1, object sender2, object sender3);
        public MCheckDdelegate MCheckHandler;


        /// <summary>
        /// 构造函数
        /// </summary>
        public CIPItemVM()
        {

        }
    }
}
