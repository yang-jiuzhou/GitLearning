using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Evaluation
{
    /**
     * ClassName: OutputSelectSet
     * Description: 导出选项
     * Version: 1.0
     * Create:  2021/03/06
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class OutputSelectSet
    {
        public string m_note = "";                          //附加信息

        public bool m_showUser = true;                      //是否显示用户
        public bool m_showChromatogramName = true;          //是否显示谱图标识
        public bool m_showChromatogram = true;              //是否显示谱图
        public bool m_showIntegration = true;               //是否显示积分
        public bool m_showMethod = true;                    //是否显示方法
        public bool m_showLog = true;                       //是否显示日志
    }
}
