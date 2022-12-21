using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.Administration
{
    /**
     * ClassName: AdministrationManager
     * Description: 用户以及权限管理类
     * Version: 1.0
     * Create:  2018/05/16
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class AdministrationManager
    {
        /// <summary>
        /// 判断用户列表是否为空
        /// </summary>
        /// <returns></returns>
        public bool GetUserIsNull()
        {
            List<UserInfo> list = null;
            if (null == GetUserInfoList(out list))
            {
                return 0 == list.Count ? true : false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 获取所有用户列表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string GetUserInfoList(out List<UserInfo> list)
        {
            string error = null;
            UserTable table = new UserTable();
            error = table.SelectList(out list);
            if (null == error)
            {
                foreach (var it in list)
                {
                    PermissionInfo item = null;
                    error = GetPermission(it.MPermissionNameID, out item);
                    if (null == error)
                    {
                        it.MPermissionName = item.MName;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return error;
        }

        /// <summary>
        /// 获取所有用户列表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string GetUserInfoListSignerReviewer(out List<UserInfo> list)
        {
            list = new List<UserInfo>();
            string error = null;
            PermissionTable pTable = new PermissionTable();
            List<int> listPermissionID = new List<int>();
            error = pTable.SelectListSignerReviewer(out listPermissionID);
            if (null != error)
            {
                return error;
            }
            UserTable table = new UserTable();
            error = table.SelectListSignerReviewer(listPermissionID, out list);
            if (null == error)
            {
                foreach (var it in list)
                {
                    PermissionInfo item = null;
                    error = GetPermission(it.MPermissionNameID, out item);
                    if (null == error)
                    {
                        it.MPermissionName = item.MName;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return error;
        }

        /// <summary>
        /// 获取指定用户的详细信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetUser(string userName, out UserInfo item)
        {
            item = null;

            UserTable table = new UserTable();
            int id = -1;
            string error = table.SelectRowID(userName, out id);
            if (null == error)
            {
                error = table.SelectRow(id, out item);
                if (null == error)
                {
                    PermissionInfo info = null;
                    error = GetPermission(item.MPermissionNameID, out info);
                    if (null == error)
                    {
                        item.MPermissionName = info.MName;
                    }
                }
            }

            return error;
        }

        /// <summary>
        /// 根据ID获取用户名
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetUser(int id, out string userName)
        {
            UserTable table = new UserTable();
            UserInfo item = null;
            string error = table.SelectRow(id, out item);
            if (null== error)
            {
                userName = item.MUserName;
            }
            else
            {
                userName = null;
            }

            return error;
        }

        /// <summary>
        /// 获取最新的用户
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetUserNew(out UserInfo item)
        {
            item = null;

            UserTable table = new UserTable();
            int id = -1;
            string error = table.GetLastID(out id);
            if (null == error)
            {
                error = table.SelectRow(id, out item);
                if (null == error)
                {
                    PermissionInfo info = null;
                    error = GetPermission(item.MPermissionNameID, out info);
                    if (null == error)
                    {
                        item.MPermissionName = info.MName;
                    }
                }
            }

            return error;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string AddUser(UserInfo item)
        {
            UserTable table = new UserTable();
            return table.InsertRow(item);
        }

        /// <summary>
        /// 修改用户的权限
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string EditUserPermission(UserInfo item)
        {
            UserTable table = new UserTable();
            return table.UpdateRowPermission(item.MID, item.MPermissionNameID);
        }

        /// <summary>
        /// 修改用户的备注
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string EditUserNote(UserInfo item)
        {
            UserTable table = new UserTable();
            return table.UpdateRowNote(item.MID, item.MNote);
        }

        /// <summary>
        /// 修改用户的登录密码
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string EditUserPwd(UserInfo item)
        {
            UserTable table = new UserTable();
            return table.UpdateRowPwd(item.MID, item.MPwd);
        }

        /// <summary>
        /// 重置用户的登录密码
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string EditUserPwdReset(UserInfo item, string pwd)
        {
            UserTable table = new UserTable();
            return table.UpdateRowPwdReset(item.MID, pwd);
        }

        /// <summary>
        /// 修改用户的签名密码
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string EditUserPwdSign(UserInfo item)
        {
            UserTable table = new UserTable();
            return table.UpdateRowPwdSign(item.MID, item.MPwdSign);
        }

        /// <summary>
        /// 重置用户的签名密码
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string EditUserPwdSignReset(UserInfo item, string pwdSign)
        {
            UserTable table = new UserTable();
            return table.UpdateRowPwdSignReset(item.MID, pwdSign);
        }

        /// <summary>
        /// 修改用户的状态
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string EditUserEnabled(UserInfo item)
        {
            UserTable table = new UserTable();
            return table.UpdateRowEnabled(item.MID, item.MEnabled);
        }

        /// <summary>
        /// 修改用户的输错次数
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string EditUserErrorNum(UserInfo item)
        {
            UserTable table = new UserTable();
            return table.UpdateRowErrorNum(item.MID, item.MErrorNum);
        }

        /// <summary>
        /// 修改用户的项目树ID
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string EditUserProjectID(UserInfo item)
        {
            UserTable table = new UserTable();
            return table.UpdateRowProjectID(item.MID, item.MProjectID);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string DelUser(UserInfo item)
        {
            UserTable table = new UserTable();
            return table.DeleteRow(item.MID);
        }


        /// <summary>
        /// 添加默认权限
        /// </summary>
        /// <returns></returns>
        public void AddDefault()
        {
            PermissionTable table = new PermissionTable();
            table.AddDefault();
        }

        /// <summary>
        /// 创建权限树
        /// </summary>
        /// <param name="item">权限来源</param>
        /// <param name="itemCurr">权限依赖（当前登录的权限）</param>
        /// <returns></returns>
        public PermissionTree CreatePermissionTree(PermissionInfo item, PermissionInfo itemCurr)
        {
            int index = 0;
            List<PermissionNode> nodeList = new List<PermissionNode>();
            List<string> listName = new List<string>();
            listName.Add("");
            for (int i = 0; i < Enum.GetNames(typeof(EnumPermission)).GetLength(0); i++)
            {
                int count = FindCharCount(((EnumPermission)i).ToString(), '_');
                string name = Share.ReadXaml.GetEnum((EnumPermission)i);
                if (count >= listName.Count - 1)
                {
                    listName.Add(name);
                }
                else
                {
                    listName[count + 1] = name;
                }
                nodeList.Add(new PermissionNode(name, listName[count], item.MList[index], itemCurr.MList[index++]));
            }

            return new PermissionTree(nodeList);
        }

        /// <summary>
        /// 获取所有权限列表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string GetPermissionInfoList(out List<PermissionInfo> list)
        {
            PermissionTable table = new PermissionTable();
            return table.SelectList(out list);
        }

        /// <summary>
        /// 获取指定的权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetPermission(int id, out PermissionInfo item)
        {
            PermissionTable table = new PermissionTable();
            return table.SelectRow(id, out item);
        }

        /// <summary>
        /// 获取指定的权限
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetPermission(string name, out PermissionInfo item)
        {
            PermissionTable table = new PermissionTable();
            return table.SelectRow(name, out item);
        }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string AddPermission(PermissionInfo item)
        {
            string error = null;
            PermissionTable table = new PermissionTable();
            error = table.InsertRow(item);
            if (null == error)
            {
                int id = -1;
                error = table.GetLastID(out id);
                item.MID = id;
            }

            return error;
        }

        /// <summary>
        /// 编辑权限
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string EditPermission(PermissionInfo item)
        {
            PermissionTable table = new PermissionTable();
            return table.UpdateRow(item);
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string DelPermission(PermissionInfo item)
        {
            List<string> listUserName = null;
            UserTable tableU = new UserTable();
            string error = tableU.SelectRowPermissionNameID(item.MID, out listUserName);
            if (null != error)
            {
                return error;
            }

            if (null != listUserName && 0 < listUserName.Count)
            {
                return Share.ReadXaml.GetResources("labPermissionDelFailed");
            }

            PermissionTable tableP = new PermissionTable();
            return tableP.DeleteRow(item.MID);
        }


        /// <summary>
        /// 获取安全策略列表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<TacticsRow> GetTacticsRowList(TacticsInfo tacticsInfo)
        {
            List<TacticsRow> list = new List<TacticsRow>();
            foreach (EnumTactics it in Enum.GetValues(typeof(EnumTactics)))
            {
                list.Add(new TacticsRow(tacticsInfo, it));
            }

            return list;
        }

        /// <summary>
        /// 获取安全策略
        /// </summary>
        /// <returns></returns>
        public string GetTactics(out TacticsInfo tacticsInfo)
        {
            TacticsTable tdb = new TacticsTable();
            return tdb.SelectRow(out tacticsInfo);
        }

        /// <summary>
        /// 编辑安全策略
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public string EditTacticsRow(TacticsRow row)
        {
            TacticsTable table = new TacticsTable();
            return table.UpdateRow(row.MIndex, row.MValue);
        }


        /// <summary>
        /// 获取签名审核列表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<SignerReviewerRow> GetSignerReviewerRowList(SignerReviewerInfo signerReviewerInfo)
        {
            List<SignerReviewerRow> list = new List<SignerReviewerRow>();
            foreach (EnumSignerReviewer it in Enum.GetValues(typeof(EnumSignerReviewer)))
            {
                list.Add(new SignerReviewerRow(it, Share.ReadXaml.GetEnum(it), signerReviewerInfo.MListSigner[(int)it], signerReviewerInfo.MListReviewer[(int)it]));
            }

            return list;
        }

        /// <summary>
        /// 获取签名审核
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetSignerReviewer(out SignerReviewerInfo item)
        {
            SignerReviewerTable sdb = new SignerReviewerTable();
            return sdb.SelectRow(out item);
        }

        /// <summary>
        /// 编辑签名审核
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public string EditSignerReviewer(SignerReviewerInfo item)
        {
            SignerReviewerTable table = new SignerReviewerTable();
            return table.UpdateRow(item);
        }


        /// <summary>
        /// 统计字符出现的次数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private int FindCharCount(string str, char value)
        {
            if (null == str)
            {
                return 0;
            }

            int count = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == value)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
