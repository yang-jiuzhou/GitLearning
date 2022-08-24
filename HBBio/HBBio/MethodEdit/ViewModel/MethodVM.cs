using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    public class MethodVM
    {
        /// <summary>
        /// 方法信息
        /// </summary>
        public Method MItem 
        { 
            get
            {
                return m_item;
            }
            set
            {
                m_item = value;

                MMethodSetting.MItem = value.MMethodSetting;

                PhaseFactory phaseFactory = new PhaseFactory();
                foreach (var it in value.MPhaseList)
                {
                    MPhaseList.Add(phaseFactory.GetPhaseVM(it, MMethodSetting.MMethodBaseValue));
                }
            }
        }
        private Method m_item = null;
        /// <summary>
        /// 方法设置VM
        /// </summary>
        public MethodSettingsVM MMethodSetting { get; set; }
        /// <summary>
        /// 阶段VM列表
        /// </summary>
        public List<BasePhaseVM> MPhaseList { get; set; }
        /// <summary>
        /// 阶段副本
        /// </summary>
        public BasePhase MCopyPhase { get; set; }

        
        /// <summary>
        /// 构造函数
        /// </summary>
        public MethodVM()
        {
            MMethodSetting = new MethodSettingsVM();
            MPhaseList = new List<BasePhaseVM>();
            MCopyPhase = null;
        }

        /// <summary>
        /// 初始化委托事件
        /// </summary>
        public void InitHandler()
        {
            MMethodSetting.MColumnVolHandler += BaseValueChanged;
            MMethodSetting.MBaseUnitHandler += BaseValueChanged;
            MMethodSetting.MFlowRateHandler += BaseValueChanged;
            MMethodSetting.MFlowRateUnitHandler += BaseValueChanged;
            MMethodSetting.MValveHandler += BaseValueChanged;
        }

        /// <summary>
        /// 方法设置的参数发生改变
        /// </summary>
        /// <param name="sender"></param>
        private void BaseValueChanged(object sender)
        {
            foreach (var it in MPhaseList)
            {
                it.ChangeEnumBase((MethodBaseValue)sender);
            }
        }

        /// <summary>
        /// 新增阶段
        /// </summary>
        /// <param name="type"></param>
        /// <param name="info"></param>
        public void AddPhase(EnumPhaseType type, string nameType)
        {
            PhaseFactory fac = new PhaseFactory();
            BasePhase basePhase = fac.GetPhase(type, nameType);
            MItem.MPhaseList.Add(basePhase);
            MPhaseList.Add(fac.GetPhaseVM(basePhase, MMethodSetting.MMethodBaseValue));
        }

        /// <summary>
        /// 插入阶段
        /// </summary>
        /// <param name="type"></param>
        /// <param name="info"></param>
        public void InsertPhase(int index, EnumPhaseType type, string nameType)
        {
            PhaseFactory fac = new PhaseFactory();
            BasePhase basePhase = fac.GetPhase(type, nameType);
            MItem.MPhaseList.Insert(index, basePhase);
            MPhaseList.Insert(index, fac.GetPhaseVM(basePhase, MMethodSetting.MMethodBaseValue));
        }

        /// <summary>
        /// 删除阶段
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAtPhase(int index)
        {
            if (-1 < index && index < MPhaseList.Count)
            {
                MItem.MPhaseList.RemoveAt(index);
                MPhaseList.RemoveAt(index);
            }
        }

        /// <summary>
        /// 上移阶段
        /// </summary>
        /// <param name="index"></param>
        public void UpPhase(int index)
        {
            if (0 < index && index < MPhaseList.Count)
            {
                BasePhase curr = MItem.MPhaseList[index];
                MItem.MPhaseList.RemoveAt(index);
                MItem.MPhaseList.Insert(index - 1, curr);

                BasePhaseVM currVM = MPhaseList[index];
                MPhaseList.RemoveAt(index);
                MPhaseList.Insert(index - 1, currVM);
            }
        }

        /// <summary>
        /// 下移阶段
        /// </summary>
        /// <param name="index"></param>
        public void DownPhase(int index)
        {
            if (-1 < index && index < MPhaseList.Count - 1)
            {
                BasePhase curr = MItem.MPhaseList[index];
                MItem.MPhaseList.RemoveAt(index);
                MItem.MPhaseList.Insert(index + 1, curr);

                BasePhaseVM currVM = MPhaseList[index];
                MPhaseList.RemoveAt(index);
                MPhaseList.Insert(index + 1, currVM);
            }
        }

        /// <summary>
        /// 交换阶段
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        public void SwapPhase(int index1, int index2)
        {
            if (-1 < index1 && index1 < MPhaseList.Count && -1 < index2 && index2 < MPhaseList.Count)
            {
                if (index1 > index2)
                {
                    Share.ValueTrans.Swap(ref index1, ref index2);
                }

                BasePhase curr = MItem.MPhaseList[index2];
                MItem.MPhaseList.RemoveAt(index2);
                MItem.MPhaseList.Insert(index1, curr);

                BasePhaseVM currVM = MPhaseList[index2];
                MPhaseList.RemoveAt(index2);
                MPhaseList.Insert(index1, currVM);
            }
        }

        /// <summary>
        /// 复制阶段
        /// </summary>
        /// <param name="index"></param>
        public void CopyPhase(int index)
        {
            if (-1 < index && index < MPhaseList.Count)
            {
                switch (MPhaseList[index].MItemBase.MType)
                {
                    case EnumPhaseType.Miscellaneous: 
                        MCopyPhase = DeepCopy.DeepCopyByXml(((MiscellaneousVM)MPhaseList[index]).MItem); 
                        break;
                    default: 
                        MCopyPhase = DeepCopy.DeepCopyByXml(((DlyPhaseVM)MPhaseList[index]).MItem); 
                        break;
                }
            }
        }

        /// <summary>
        /// 粘贴阶段
        /// </summary>
        public void PastePhase()
        {
            if (null != MCopyPhase)
            {
                PhaseFactory fac = new PhaseFactory();
                BasePhase basePhase = DeepCopy.DeepCopyByXml(MCopyPhase);
                MItem.MPhaseList.Add(basePhase);
                MPhaseList.Add(fac.GetPhaseVM(basePhase, MMethodSetting.MMethodBaseValue));
            }
        }
    }
}
