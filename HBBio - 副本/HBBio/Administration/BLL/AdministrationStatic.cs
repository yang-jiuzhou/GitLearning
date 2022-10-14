using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.Administration
{
    /**
     * ClassName: AdministrationStatic
     * Description: 用户权限静态类
     * Version: 1.0
     * Create:  2029/09/07
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class AdministrationStatic
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfo MUserInfo
        {
            get
            {
                return m_userInfo;
            }
        }
        /// <summary>
        /// 权限信息
        /// </summary>
        public PermissionInfo MPermissionInfo
        {
            get
            {
                return m_permissionInfo;
            }
        }
        /// <summary>
        /// 安全策略信息
        /// </summary>
        public TacticsInfo MTacticsInfo
        {
            get
            {
                return m_tacticsInfo;
            }
        }
        /// <summary>
        /// 电子签名信息
        /// </summary>
        public SignerReviewerInfo MSignerReviewerInfo
        {
            get
            {
                return m_signerReviewerInfo;
            }
        }
        /// <summary>
        /// 签名人名称
        /// </summary>
        public string MSigner
        {
            get
            {
                return m_signer;
            }
        }
        /// <summary>
        /// 审核人名称
        /// </summary>
        public string MReviewer
        {
            get
            {
                return m_reviewer;
            }
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        private UserInfo m_userInfo = null;
        /// <summary>
        /// 权限信息
        /// </summary>
        private PermissionInfo m_permissionInfo = null;
        /// <summary>
        /// 安全策略信息
        /// </summary>
        private TacticsInfo m_tacticsInfo = null;
        /// <summary>
        /// 电子签名信息
        /// </summary>
        private SignerReviewerInfo m_signerReviewerInfo = null;
        /// <summary>
        /// 签名人名称
        /// </summary>
        private string m_signer = "";
        /// <summary>
        /// 审核人名称
        /// </summary>
        private string m_reviewer = "";
        

        /// <summary>
        /// 构造函数
        /// </summary>
        private AdministrationStatic()
        {
            
        }

        /// <summary>
        /// 内部静态类
        /// </summary>
        private static class AdministrationStaticInstance
        {
            internal static readonly AdministrationStatic s_instance = new AdministrationStatic();
        }

        /// <summary>
        /// 单例模式
        /// </summary>
        /// <returns></returns>
        public static AdministrationStatic Instance()
        {
            return AdministrationStaticInstance.s_instance;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            AdministrationManager administrationManager = new AdministrationManager();
            administrationManager.GetTactics(out m_tacticsInfo);
            administrationManager.GetSignerReviewer(out m_signerReviewerInfo);
        }

        /// <summary>
        /// 用户登录验证
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Login(string userName, string password)
        {
            UserInfo userInfo = null;
            PermissionInfo permissionInfo = null;

            AdministrationManager administrationManager = new AdministrationManager();
            string error = administrationManager.GetUser(userName, out userInfo);

            //访问数据库失败
            if (null != error)
            {
                return error;
            }

            //判断状态
            JudgeUserStatus(userInfo, m_tacticsInfo);

            //第一次登录
            if (0 == userInfo.MPwdTime.CompareTo(Convert.ToDateTime("1970/01/01 00:00:00")))
            {
                if (!userInfo.MPwd.Equals(password))
                {
                    //初始密码错误!
                    error = Share.ReadXaml.GetResources("A_ErrorCurrPwd");
                }
                else
                {
                    //error = "用户第一次登录,请先修改密码!";
                    error = Share.ReadXaml.GetResources("A_ErrorFirstModifyPwd");
                }
                return error;
            }

            //重置密码后第一次登录
            if (0 == userInfo.MPwdTime.CompareTo(Convert.ToDateTime("1970/01/02 00:00:00")))
            {
                if (!userInfo.MPwd.Equals(password))
                {
                    //初始密码错误!
                    error = Share.ReadXaml.GetResources("A_ErrorCurrPwd");
                }
                else
                {
                    //error = "用户密码重置后第一次登录,请先修改密码!";
                    error = Share.ReadXaml.GetResources("A_ErrorFirstResetPwd");
                }
                return error;
            }

            //超过密码最大使用期限
            if (0 != userInfo.MPwdDay)
            {
                int days = (DateTime.Now - userInfo.MPwdTime).Days + 1;
                if (days > userInfo.MPwdDay)
                {
                    //error = "超过密码最大使用期限,请先修改密码!";
                    error = Share.ReadXaml.GetResources("A_ErrorTimePwd");
                    return error;
                }
                else if (days + 6 > userInfo.MPwdDay)
                {
                    Share.MessageBoxWin.Show(Share.ReadXaml.GetResources("A_ErrorDayPwd") + (userInfo.MPwdDay - days + 1));
                }
                else
                { }
            }

            //用户被禁用
            if (!userInfo.MEnabled)
            {
                //error = "用户被禁用!请联系管理员!";
                error = Share.ReadXaml.GetResources("A_ErrorDisUser");
                return error;
            }

            //密码错误累计
            if (0 != m_tacticsInfo.NameLock)
            {
                //用户被锁定
                if (userInfo.MErrorNum == m_tacticsInfo.NameLock)
                {
                    //error = "用户被锁定!请联系管理员!";
                    error = Share.ReadXaml.GetResources("A_ErrorLockUser");
                    return error;
                }

                if (!userInfo.MPwd.Equals(password))//密码错误
                {
                    if (userInfo.MErrorNum < m_tacticsInfo.NameLock)
                    {
                        userInfo.MErrorNum += 1;
                        administrationManager.EditUserErrorNum(userInfo);
                        if (userInfo.MErrorNum == m_tacticsInfo.NameLock)
                        {
                            //error = "用户被锁定!请联系管理员!";
                            error = Share.ReadXaml.GetResources("A_ErrorLockUser");
                        }
                        else
                        {
                            //error = "密码错误!剩余" + (tacticsInfo.NameLock - userInfo.MErrorNum - 1) + "次机会!";
                            error = Share.ReadXaml.GetResources("A_ErrorNumPwd") + (m_tacticsInfo.NameLock - userInfo.MErrorNum);
                        }
                    }
                    return error;
                }
                else//密码正确
                {
                    userInfo.MErrorNum = 0;
                    administrationManager.EditUserErrorNum(userInfo);
                }
            }
            else
            {
                if (!userInfo.MPwd.Equals(password))//密码错误
                {
                    //error = "密码错误!";
                    error = Share.ReadXaml.GetResources("A_ErrorPwd");
                    return error;
                }
            }

            error = administrationManager.GetPermission(userInfo.MPermissionNameID, out permissionInfo);
            if (null != error)//权限信息数据库失败
            {
                return error;
            }

            m_userInfo = userInfo;
            m_permissionInfo = permissionInfo;

            return error;
        }

        /// <summary>
        /// 判断用户状态
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="tacticsInfo"></param>
        public void JudgeUserStatus(UserInfo userInfo, TacticsInfo tacticsInfo)
        {
            if (userInfo.MEnabled)
            {
                userInfo.MStatus = Share.ReadXaml.GetResources("A_Active");
            }
            else
            {
                userInfo.MStatus = Share.ReadXaml.GetResources("A_DisActive");
            }

            if (0 != tacticsInfo.NameLock && tacticsInfo.NameLock == userInfo.MErrorNum)
            {
                userInfo.MLock = true;
                userInfo.MStatus += "/" + Share.ReadXaml.GetResources("A_Lock");
            }
            else
            {
                userInfo.MLock = false;
            }
        }

        /// <summary>
        /// 返回权限
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool ShowPermission(EnumPermission index)
        {
            return m_permissionInfo.MList[(int)index];
        }

        /// <summary>
        /// 调用签名审核对话框
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool ShowSignerReviewerWin(Window parent, EnumSignerReviewer index)
        {
            if (m_signerReviewerInfo.MListReviewer[(int)index])
            {
                ReviewerWin dlg = new ReviewerWin(parent, Share.ReadXaml.GetEnum(index), m_userInfo.MUserName, m_userInfo.MPwdSign);
                if (false == dlg.ShowDialog())
                {
                    return false;
                }
                m_signer = m_userInfo.MUserName;
                m_reviewer = dlg.MReviewer;
            }
            else if (m_signerReviewerInfo.MListSigner[(int)index])
            {
                SignerWin dlg = new SignerWin(parent, Share.ReadXaml.GetEnum(index), m_userInfo.MUserName, m_userInfo.MPwdSign);
                if (false == dlg.ShowDialog())
                {
                    return false;
                }
                m_signer = m_userInfo.MUserName;
                m_reviewer = "";
            }
            else
            {
                m_signer = "";
                m_reviewer = "";
            }

            return true;
        }
    }
}
