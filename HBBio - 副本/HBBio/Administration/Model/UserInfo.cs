using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Administration
{
    /**
     * ClassName: UserInfo
     * Description: 用户信息类
     * Version: 1.0
     * Create:  2018/05/28
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    [Serializable]
    public class UserInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public int MID { get; set; }
        /// <summary>
        /// 正常用户值为0，用户被删除后值为ID
        /// </summary>
        public int MDeleteID { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string MUserName { get; set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        public int MPermissionNameID { get; set; }
        /// <summary>
        /// 权限名称（根据ID去寻找）
        /// </summary>
        public string MPermissionName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string MNote { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string MPwd { get; set; }
        /// <summary>
        /// 签名密码
        /// </summary>
        public string MPwdSign { get; set; }
        /// <summary>
        /// 上次修改登录密码时间
        /// </summary>
        public DateTime MPwdTime { get; set; }
        /// <summary>
        /// 登录密码有效期（天）
        /// </summary>
        public int MPwdDay { get; set; }
        /// <summary>
        /// true活动，false锁定
        /// </summary>
        public bool MEnabled { get; set; }
        /// <summary>
        /// 密码输入错误次数
        /// </summary>
        public int MErrorNum { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        public int MProjectID { get; set; }

        /// <summary>
        /// 剩余天数（推算）
        /// </summary>
        public int MDaysRemaining { get; set; }
        /// <summary>
        /// 是否锁定（推算）
        /// </summary>
        public bool MLock { get; set; }
        /// <summary>
        /// 状态（推算）
        /// </summary>
        public string MStatus { get; set; }


        /// <summary>
        /// 构造函数(新建非管理员用户)
        /// </summary>
        /// <param name="permissionNameID"></param>
        public UserInfo(int permissionNameID)
        {
            MDeleteID = 0;
            MPermissionNameID = permissionNameID;
            MPwdTime = Convert.ToDateTime("1970/01/01 00:00:00");
            MEnabled = true;
            MErrorNum = 0;
        }

        /// <summary>
        /// 构造函数(新建非管理员用户)
        /// </summary>
        public UserInfo(string userName, int permissionNameID, string note, string pwd, string pwdSign, int pwdDay)
        {
            MDeleteID = 0;
            MUserName = userName;
            MPermissionNameID = permissionNameID;
            MNote = note;
            MPwd = pwd;
            MPwdSign = pwdSign;
            MPwdTime = Convert.ToDateTime("1970/01/01 00:00:00");
            MPwdDay = pwdDay;
            MEnabled = true;
            MErrorNum = 0;
        }

        /// <summary>
        /// 构造函数(初始新建管理员用户)
        /// </summary>
        public UserInfo(string userName, int permissionNameID, string note, string pwd, string pwdSign)
        {
            MDeleteID = 0;
            MUserName = userName;
            MPermissionNameID = permissionNameID;
            MNote = note;
            MPwd = pwd;
            MPwdSign = pwdSign;
            MPwdTime = DateTime.Now;
            MPwdDay = 3650;
            MEnabled = true;
            MErrorNum = 0;
        }

        /// <summary>
        /// 构造函数(数据库读取)
        /// </summary>
        public UserInfo(int ID, int deleteID, string userName, int permissionNameID, string note, string pwd, string pwdSign,
            DateTime pwdTime, int pwdDay, bool enabled, int errorNum, int projectID)
        {
            MID = ID;
            MDeleteID = deleteID;
            MUserName = userName;
            MPermissionNameID = permissionNameID;
            MNote = note;
            MPwd = pwd;
            MPwdSign = pwdSign;
            MPwdTime = pwdTime;
            MPwdDay = pwdDay;
            MEnabled = enabled;
            MErrorNum = errorNum;

            MProjectID = projectID;

            MDaysRemaining = (pwdTime.AddDays(pwdDay) - DateTime.Now).Days;
            if (MDaysRemaining < 0)
            {
                MDaysRemaining = 0;
            }
        }
    }
}