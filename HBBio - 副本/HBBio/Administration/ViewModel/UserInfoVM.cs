using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Administration
{
    class UserInfoVM
    {
        public UserInfo MUserInfo { get; set; }
        public List<PermissionInfo> MListPermissionInfo { get; set; }


        /// <summary>
        /// 用户名称
        /// </summary>
        public string MUserName 
        { 
            get
            {
                return MUserInfo.MUserName;
            }
            set
            {
                MUserInfo.MUserName = value;
            }
        }
        /// <summary>
        /// 权限名称
        /// </summary>
        public int MPermissionName
        {
            get
            {
                return MUserInfo.MPermissionNameID;
            }
            set
            {
                MUserInfo.MPermissionNameID = value;

                foreach (var it in MListPermissionInfo)
                {
                    if (it.MName.Equals(value))
                    {
                        MUserInfo.MPermissionName = it.MName;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string MNote
        {
            get
            {
                return MUserInfo.MNote;
            }
            set
            {
                MUserInfo.MNote = value;
            }
        }
        /// <summary>
        /// true活动，false锁定
        /// </summary>
        public bool MEnabled
        {
            get
            {
                return MUserInfo.MEnabled;
            }
            set
            {
                MUserInfo.MEnabled = value;
            }
        }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool MLock
        {
            get
            {
                return MUserInfo.MLock;
            }
            set
            {
                MUserInfo.MLock = value;
            }
        }
        public bool MUnlock { get; set; }
        public bool MResetPwd { get; set; }
        public bool MResetPwdSign { get; set; }
        public string MResetPwdStr { get; set; }
        public string MResetPwdSignStr { get; set; }
    }
}
