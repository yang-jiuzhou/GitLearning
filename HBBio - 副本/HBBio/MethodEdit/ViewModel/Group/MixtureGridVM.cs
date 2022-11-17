using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    public class MixtureGridVM : BaseGroupVM
    {
        public MixtureGrid MItem
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
                MFlowRateUnitStr = MMethodBaseValue.MFlowRateUnitStr;

                foreach (var it in m_item.MList)
                {
                    MixtureGridItemVM item = new MixtureGridItemVM(MMethodBaseValue);
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
        public ObservableCollection<MixtureGridItemVM> MList { get; set; }

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
        public string MFlowRateUnitStr
        {
            get
            {
                return m_flowRateUnitStr;
            }
            set
            {
                m_flowRateUnitStr = value;
                OnPropertyChanged("MFlowRateUnitStr");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private MixtureGrid m_item = null;
        /// <summary>
        /// 保存复制的副本
        /// </summary>
        private MixtureGridItem m_copy = null;

        private string m_baseStr = "";
        private string m_baseUnitStr = "";
        private string m_flowRateUnitStr = "";


        /// <summary>
        /// 构造函数
        /// </summary>
        public MixtureGridVM(MethodBaseValue methodBaseValue) : base(methodBaseValue)
        {
            MList = new ObservableCollection<MixtureGridItemVM>();
        }

        /// <summary>
        /// 添加行
        /// </summary>
        public void Add()
        {
            MixtureGridItem item = new MixtureGridItem();
            MItem.MList.Add(item);

            MixtureGridItemVM itemVM = new MixtureGridItemVM(MMethodBaseValue);
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
                MixtureGridItem temp = MItem.MList[index];
                MItem.MList[index] = MItem.MList[index - 1];
                MItem.MList[index - 1] = temp;

                MixtureGridItemVM tempVM = MList[index];
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
                MixtureGridItem temp = MItem.MList[index];
                MItem.MList[index] = MItem.MList[index + 1];
                MItem.MList[index + 1] = temp;

                MixtureGridItemVM tempVM = MList[index];
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
                MixtureGridItem item = DeepCopy.DeepCopyByXml(m_copy);
                MItem.MList.Add(item);

                MixtureGridItemVM itemVM = new MixtureGridItemVM(MMethodBaseValue);
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

            if (methodBaseValue.MChangeFlowRateUnit)
            {
                MFlowRateUnitStr = methodBaseValue.MFlowRateUnitStr;
                foreach (var it in MList)
                {
                    it.MFlowVolLenSample = ValueTrans.ChangeFlowRateUnit(methodBaseValue.MEnumFlowRateOld, methodBaseValue.MEnumFlowRateNew, methodBaseValue.MColumnArea, it.MFlowVolLenSample);
                    it.MFlowVolLenSystem = ValueTrans.ChangeFlowRateUnit(methodBaseValue.MEnumFlowRateOld, methodBaseValue.MEnumFlowRateNew, methodBaseValue.MColumnArea, it.MFlowVolLenSystem);
                }
            }

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
