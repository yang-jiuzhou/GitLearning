using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: PhaseFactory
     * Description: 阶段工厂，创建预定义阶段
     * Version: 1.0
     * Create:  2020/07/31
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    class PhaseFactory
    {
        /// <summary>
        /// 创建预定义阶段
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public BasePhase GetPhase(EnumPhaseType type, string nameType = null)
        {
            BasePhase item = null;
            switch (type)
            {
                case EnumPhaseType.Miscellaneous: 
                    item = new Miscellaneous(); 
                    break;
                default:
                    item = new DlyPhase();
                    break;
            }
            item.Init(type, nameType);

            return item;
        }

        /// <summary>
        /// 创建预定义阶段
        /// </summary>
        /// <param name="basePhase"></param>
        /// <returns></returns>
        public BasePhaseVM GetPhaseVM(BasePhase basePhase, MethodBaseValue methodBaseValue)
        {
            BasePhaseVM item = null;
            switch (basePhase.MType)
            {
                case EnumPhaseType.Miscellaneous:
                    item = new MiscellaneousVM(methodBaseValue);
                    ((MiscellaneousVM)item).MItem = (Miscellaneous)basePhase;
                    break;
                default:
                    item = new DlyPhaseVM(methodBaseValue);
                    ((DlyPhaseVM)item).MItem = (DlyPhase)basePhase;
                    break;
            }

            return item;
        }
    }
}
