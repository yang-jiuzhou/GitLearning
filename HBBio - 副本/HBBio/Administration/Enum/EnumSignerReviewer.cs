using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Administration
{
    /**
     * ClassName: EnumSignerReviewer
     * Description: 签名审核枚举
     * Version: 1.0
     * Create:  2021/01/08
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    public enum EnumSignerReviewer
    {
        //ChromatogramSet,                        //色谱信号
        //Communication,                          //通信配置
        Communication_Add,                      //通信配置-添加
        Communication_Edit,                     //通信配置-编辑
        Communication_Del,                      //通信配置-删除
        Communication_Activate,                 //通信配置-激活
        //InstrumentParameters,                   //仪表参数
        InstrumentParameters_Edit,              //仪表参数-编辑
        //Administration,                         //用户权限
        //Administration_User,                    //用户权限-用户
        Administration_User_Add,                //用户权限-用户-添加
        Administration_User_Edit,               //用户权限-用户-编辑
        Administration_User_Del,                //用户权限-用户-删除
        //Administration_Permission,              //用户权限-权限
        Administration_Permission_Add,          //用户权限-权限-添加
        Administration_Permission_Edit,         //用户权限-权限-编辑
        Administration_Permission_Del,          //用户权限-权限-删除
        //Administration_Tactics,                 //用户权限-安全策略
        Administration_Tactics_Edit,            //用户权限-安全策略-编辑
        //Administration_SignerReviewer,          //用户权限-签名审核
        Administration_SignerReviewer_Edit,     //用户权限-签名审核-编辑
        //ColumnHandling,                         //色谱柱管理
        ColumnHandling_Edit,                    //色谱柱管理-编辑
        ColumnHandling_ImportExport,            //色谱柱管理-导入导出
        ColumnHandling_Print,                   //色谱柱管理-打印
        //TubeStand,                             //色谱柱管理
        TubeStand_Edit,                         //试管架管理-编辑
        Manual,                                 //手动编辑
        //Project,                                //项目管理
        //Project_Method,                         //项目管理-方法
        //Project_Method_Watch,                   //项目管理-方法-查看
        Project_Method_Watch_Edit,              //项目管理-方法-查看-编辑
        Project_Method_Watch_Print,             //项目管理-方法-查看-打印
        Project_Method_Watch_ImportExport,      //项目管理-方法-查看-导入导出
        Project_Method_Watch_Other,             //项目管理-方法-查看-查看他人
        Project_Method_Run,                     //项目管理-方法-运行
        Project_Method_Intervene,               //项目管理-方法-干预
        //Project_Evaluation,                     //项目管理-结果分析
        Project_Evaluation_Watch_Print,               //项目管理-结果分析-查看-打印
        //Project_Evaluation_Watch_Other,               //项目管理-结果分析-查看-查看他人
        Project_Evaluation_Rename,              //项目管理-结果分析-重命名
        //AuditTrails,                            //审计跟踪
        AuditTrails_Print,                      //审计跟踪-打印
        //MonitorSet,                             //实时监控
        //Databases,                              //数据管理
        Databases_Backup,                       //数据管理-备份
        Databases_Restore                       //数据管理-还原
    }
}
