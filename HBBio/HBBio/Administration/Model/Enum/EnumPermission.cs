using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Administration
{
    /**
     * ClassName: EnumPermission
     * Description: 权限枚举
     * Version: 1.0
     * Create:  2020/05/16
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    public enum EnumPermission
    {
        SoftExit,                               //退出                            软件系统的退出
        ChromatogramSet,                        //色谱图曲线                      实时谱图右键菜单的“曲线” 
        Communication,                          //通信配置                        主界面的按钮“通信配置”是否可用
        Communication_Add,                      //通信配置-添加                   子界面“通信配置”的按钮“添加”是否可用
        Communication_Edit,                     //通信配置-编辑                   子界面“通信配置”的按钮“编辑”是否可用
        Communication_Del,                      //通信配置-删除                   子界面“通信配置”的按钮“删除”是否可用
        Communication_Activate,                 //通信配置-激活                   子界面“通信配置”的按钮“激活”是否可用
        InstrumentParameters,                   //仪表参数                        主界面的按钮“仪表参数”是否可用
        InstrumentParameters_Edit,              //仪表参数-编辑                   子界面“仪表参数”的按钮“确定”是否可用
        Administration,                         //用户权限                        主界面的按钮“用户权限”是否可用
        Administration_User,                    //用户权限-用户                   子界面“用户权限”的页面“用户”是否可见
        Administration_User_Add,                //用户权限-用户-添加              子界面“用户权限”的页面“用户”的按钮“添加”是否可用
        Administration_User_Edit,               //用户权限-用户-编辑              子界面“用户权限”的页面“用户”的按钮“编辑”是否可用
        Administration_User_Del,                //用户权限-用户-删除              子界面“用户权限”的页面“用户”的按钮“删除”是否可用
        Administration_Permission,              //用户权限-权限                   子界面“用户权限”的页面“权限”是否可见
        Administration_Permission_Add,          //用户权限-权限-添加              子界面“用户权限”的页面“权限”的按钮“添加”是否可用
        Administration_Permission_Edit,         //用户权限-权限-编辑              子界面“用户权限”的页面“权限”的按钮“编辑”是否可用
        Administration_Permission_Del,          //用户权限-权限-删除              子界面“用户权限”的页面“权限”的按钮“删除”是否可用
        Administration_Tactics,                 //用户权限-安全策略               子界面“用户权限”的页面“安全策略”是否可见
        Administration_Tactics_Edit,            //用户权限-安全策略-编辑          子界面“用户权限”的页面“安全策略”的按钮“编辑”是否可用
        Administration_SignerReviewer,          //用户权限-签名审核               子界面“用户权限”的页面“签名审核”是否可见
        Administration_SignerReviewer_Edit,     //用户权限-签名审核-编辑          子界面“用户权限”的页面“签名审核”的按钮“修改”是否可用
        ColumnHandling,                         //色谱柱管理
        ColumnHandling_Edit,                    //色谱柱管理-编辑
        ColumnHandling_ImportExport,            //色谱柱管理-导入导出
        ColumnHandling_Print,                   //色谱柱管理-打印
        TubeStand,                              //试管架管理
        TubeStand_Edit,                         //试管架管理-编辑
        Manual,                                 //手动编辑                        主界面的按钮“手动编辑”是否可用
        Project,                                //项目管理                        主界面的按钮“项目管理”是否可用
        Project_Method,                         //项目管理-方法                   子界面“项目管理”的页面“方法”是否可见
        Project_Method_Watch,                   //项目管理-方法-查看              子界面“项目管理”的页面“方法”在目录“General”和本人下的按钮“编辑”是否可用，用于打开方法编辑界面（只读，不能修改）
        Project_Method_Watch_Edit,              //项目管理-方法-查看-编辑         子界面“项目管理”的页面“方法”在目录“General”和本人下的按钮“新建方法”、“新建序列”、“重命名”、“编辑”、“删除”、“复制”、“粘贴”是否可用
        Project_Method_Watch_Print,             //项目管理-方法-查看-打印         子界面“项目管理”的页面“方法”在目录“General”和本人下的按钮“打印”是否可用
        Project_Method_Watch_ImportExport,      //项目管理-方法-查看-导入导出     子界面“项目管理”的页面“方法”在目录“General”和本人下的按钮“导入”、“导出”是否可用
        Project_Method_Watch_Other,             //项目管理-方法-查看-查看他人     子界面“项目管理”的页面“方法”在目录非本人下的按钮“编辑”是否可用，用于打开方法编辑界面（只读，不能修改）
        Project_Method_Run,                     //项目管理-方法-运行              子界面“运行”是否可用，用于将已经发送的方法启动运行
        Project_Method_Intervene,               //项目管理-方法-干预              主界面的流路图中的泵、收集器在方法运行的过程中，点击时是否可用弹出干预设置对话框
        Project_Evaluation,                     //项目管理-结果分析               子界面“项目管理”的页面“结果分析”是否可见
        Project_Evaluation_Watch,               //项目管理-结果分析-查看          子界面“项目管理”的页面“结果分析”在目录“General”和本人下的的按钮“打开”、“对比”是否可用
        Project_Evaluation_Watch_Print,               //项目管理-结果分析-查看-打印          子界面“结果分析”的按钮“打印设置”、“打印”是否可用
        Project_Evaluation_Watch_IntegrationAuto,     //项目管理-结果分析-查看-自动积分      子界面“结果分析”的按钮“积分”是否可用
        Project_Evaluation_Watch_IntegrationManual,   //项目管理-结果分析-查看-手动积分      子界面“结果分析”的按钮“峰宽”、“峰起点”、“峰终点”、“添加正峰”、“删除峰”、“前切”、“后切”、“垂直分离”、“峰谷分离”是否可用
        Project_Evaluation_Watch_Other,               //项目管理-结果分析-查看-查看他人      子界面“项目管理”的页面“结果分析”在目录非本人下的的按钮“打开”、“对比”是否可用
        Project_Evaluation_Rename,                    //项目管理-结果分析-重命名             子界面“项目管理”的页面“结果分析”在目录“General”和本人下的的按钮“重命名”是否可用
        AuditTrails,                            //审计跟踪                        主界面的按钮“审计跟踪”是否可用
        AuditTrails_Print,                      //审计跟踪-打印                   子界面“审计跟踪”的按钮“打印”是否可用
        MonitorSet,                             //实时监控                        主界面的按钮“实时监控”是否可用
        Databases,                              //数据管理                       主界面的按钮“数据管理”是否可用
        Databases_Backup,                       //数据管理-备份
        Databases_Restore                       //数据管理-还原
    }
}
