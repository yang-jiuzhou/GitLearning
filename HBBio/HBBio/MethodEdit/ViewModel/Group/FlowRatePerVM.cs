using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    public class FlowRatePerVM : BaseGroupVM
    {
        public FlowRatePer MItem
        { 
            get
            {
                return m_item;
            }
            set
            {
                m_item = value;
                MType = value.MType;

                MBaseStr = MMethodBaseValue.MBaseStr;
                MBaseUnitStr = MMethodBaseValue.MBaseUnitStr;

                foreach (var it in m_item.MList)
                {
                    FlowRatePerItemVM item = new FlowRatePerItemVM(MMethodBaseValue);
                    item.MItem = it;
                    MList.Add(item);
                }
                if (0 == MList.Count)
                {
                    Add();
                }
            }
        }

        /// <summary>
        /// 行数据列表
        /// </summary>
        public ObservableCollection<FlowRatePerItemVM> MList { get; set; }

        public string MBaseStr
        {
            get
            {
                return m_baseStr;
            }
            set
            {
                m_baseStr = value;
                OnPropertyChanged("MBaseStr");
            }
        }
        public string MBaseUnitStr
        {
            get
            {
                return m_baseUnitStr;
            }
            set
            {
                m_baseUnitStr = value;
                OnPropertyChanged("MBaseUnitStr");
            }
        }

        public double MFlowVol
        {
            get
            {
                return m_flowVol;
            }
            set
            {
                m_flowVol = value;
                foreach (var it in MList)
                {
                    it.MFlowVol = value;
                }
            }
        }
        private double m_flowVol = 1;

        /// <summary>
        /// 
        /// </summary>
        private FlowRatePer m_item = null;
        /// <summary>
        /// 保存复制的副本
        /// </summary>
        private FlowRatePerItem m_copy = null;

        private string m_baseStr = "";
        private string m_baseUnitStr = "";


        /// <summary>
        /// 构造函数
        /// </summary>
        public FlowRatePerVM(MethodBaseValue methodBaseValue) : base(methodBaseValue)
        {
            MList = new ObservableCollection<FlowRatePerItemVM>();
        }

        /// <summary>
        /// 添加行
        /// </summary>
        public void Add()
        {
            FlowRatePerItem item = new FlowRatePerItem();
            MItem.MList.Add(item);

            FlowRatePerItemVM itemVM = new FlowRatePerItemVM(MMethodBaseValue);
            itemVM.MItem = item;
            MList.Add(itemVM);
        }

        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="index"></param>
        public void Del(int index)
        {
            if (-1 != index && 1 < MList.Count)
            {
                MItem.MList.RemoveAt(index);
                MList.RemoveAt(index);
            }
        }

        /// <summary>
        /// 上移行
        /// </summary>
        /// <param name="index"></param>
        public void Up(int index)
        {
            if (0 < index)
            {
                FlowRatePerItem temp = MItem.MList[index];
                MItem.MList[index] = MItem.MList[index - 1];
                MItem.MList[index - 1] = temp;

                FlowRatePerItemVM tempVM = MList[index];
                MList[index] = MList[index - 1];
                MList[index - 1] = tempVM;
            }
        }

        /// <summary>
        /// 下移行
        /// </summary>
        /// <param name="index"></param>
        public void Down(int index)
        {
            if (-1 != index && MList.Count - 1 > index)
            {
                FlowRatePerItem temp = MItem.MList[index];
                MItem.MList[index] = MItem.MList[index + 1];
                MItem.MList[index + 1] = temp;

                FlowRatePerItemVM tempVM = MList[index];
                MList[index] = MList[index + 1];
                MList[index + 1] = tempVM;
            }
        }

        /// <summary>
        /// 复制行
        /// </summary>
        /// <param name="index"></param>
        public void Copy(int index)
        {
            m_copy = DeepCopy.DeepCopyByXml(MList[index].MItem);
        }

        /// <summary>
        /// 粘贴行
        /// </summary>
        public void Paste()
        {
            if (null != m_copy)
            {
                FlowRatePerItem item = DeepCopy.DeepCopyByXml(m_copy);
                MItem.MList.Add(item);

                FlowRatePerItemVM itemVM = new FlowRatePerItemVM(MMethodBaseValue);
                itemVM.MItem = item;
                MList.Add(itemVM);
            }
        }

        /// <summary>
        /// 改变基本单位
        /// </summary>
        /// <param name="methodBaseValue"></param>
        public override void ChangeEnumBase(MethodBaseValue methodBaseValue)
        {
            MMethodBaseValue = methodBaseValue;

            if (methodBaseValue.MChangeBaseUnit)
            {
                MBaseStr = methodBaseValue.MBaseStr;
                MBaseUnitStr = methodBaseValue.MBaseUnitStr;
                foreach (var it in MList)
                {
                    it.MItem.MBaseTVCV.MEnumBase = methodBaseValue.MEnumBaseNew;
                    it.MBaseTVCV = it.MBaseTVCV;
                }
            } 
        }
    }
}
