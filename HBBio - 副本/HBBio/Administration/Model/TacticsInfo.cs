using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.Administration
{
    /**
     * ClassName: TacticsInfo
     * Description: 安全策略类
     * Version: 1.0
     * Create:  2018/05/28
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class TacticsInfo
    {
        /// <summary>
        /// 名称复杂
        /// </summary>
        public int NameReg { get; set; }
        /// <summary>
        /// 用户锁定阈值
        /// </summary>
        public int NameLock { get; set; }
        /// <summary>
        /// 密码复杂启用
        /// </summary>
        public int PwdReg { get; set; }
        /// <summary>
        /// 密码长度
        /// </summary>
        public int PwdLength { get; set; }
        /// <summary>
        /// 密码有效期
        /// </summary>
        public int PwdMaxTime { get; set; }
        /// <summary>
        /// 锁屏时间
        /// </summary>
        public int ScreenLock { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public TacticsInfo()
        {
            NameReg = 1;
            NameLock = 5;
            PwdReg = 0;
            PwdLength = 1;
            PwdMaxTime = 90;
            ScreenLock = 60;
        }

        /// <summary>
        /// 返回值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetValue(EnumTactics index)
        {
            switch (index)
            {
                case EnumTactics.NameLock: return NameLock;
                case EnumTactics.PwdReg: return PwdReg;
                case EnumTactics.PwdLength: return PwdLength;
                case EnumTactics.PwdMaxTime: return PwdMaxTime;
                case EnumTactics.ScreenLock: return ScreenLock;
                default: return NameReg;
            }
        }

        /// <summary>
        /// 修改值
        /// </summary>
        /// <param name="index"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public void SetValue(EnumTactics index, int val)
        {
            switch (index)
            {
                case EnumTactics.NameReg: NameReg = val; break;
                case EnumTactics.NameLock: NameLock = val; break;
                case EnumTactics.PwdReg: PwdReg = val; break;
                case EnumTactics.PwdLength: PwdLength = val; break;
                case EnumTactics.PwdMaxTime: PwdMaxTime = val; break;
                case EnumTactics.ScreenLock: ScreenLock = val; break;
            }
        }
    }
}
