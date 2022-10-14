using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Administration
{
    /**
     * ClassName: TacticsRow
     * Description: 安全策略信息行类
     * Version: 1.0
     * Create:  2018/05/28
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class TacticsRow
    {
        /// <summary>
        /// 安全策略项
        /// </summary>
        public EnumTactics MIndex { get; set; }
        /// <summary>
        /// 安全策略值
        /// </summary>
        public int MValue { get; set; }
        /// <summary>
        /// 安全策略项字符文本
        /// </summary>
        public string MColumnName { get; set; }
        /// <summary>
        /// 安全策略值字符文本
        /// </summary>
        public string MColumnValue { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="info"></param>
        /// <param name="index"></param>
        public TacticsRow(TacticsInfo info, EnumTactics index)
        {
            MIndex = index;
            MValue = info.GetValue(index);
            MColumnName = Share.ReadXaml.GetEnum(index, "A_T_");
            MColumnValue = GetValueString(info, index);
        }

        /// <summary>
        /// 获取每行第二列的文本信息
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private string GetValueString(TacticsInfo info, EnumTactics index)
        {
            switch (index)
            {
                case EnumTactics.NameReg: return 1 == info.NameReg ? Share.ReadXaml.S_Enabled : Share.ReadXaml.S_Disabled;
                case EnumTactics.NameLock: return info.NameLock.ToString() + ReadXamlAdministration.GetUnit(index);
                case EnumTactics.PwdReg: return 1 == info.PwdReg ? Share.ReadXaml.S_Enabled : Share.ReadXaml.S_Disabled;
                case EnumTactics.PwdLength: return info.PwdLength.ToString() + ReadXamlAdministration.GetUnit(index);
                case EnumTactics.PwdMaxTime: return info.PwdMaxTime.ToString() + ReadXamlAdministration.GetUnit(index);
                case EnumTactics.ScreenLock: return info.ScreenLock.ToString() + ReadXamlAdministration.GetUnit(index);
            }

            return "";
        }
    }
}
