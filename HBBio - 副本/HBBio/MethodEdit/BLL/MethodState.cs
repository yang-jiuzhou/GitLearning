using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    /// <summary>
    /// 方法运行的状态
    /// </summary>
    public enum MethodState
    {
        Free,       	//空闲
        Run,            //运行
        Pause,          //暂停
        Stop,           //停止
        FreeToRun,      //空闲->运行 
        RunToNext,      //运行->下一阶段
        RunToPause,     //运行->暂停
        PauseToRun,     //暂停->运行
        BreakToPause    //中断->暂停
    }
}
