using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Administration
{
    /**
     * ClassName: PermissionInfo
     * Description: 权限类
     * Version: 1.0
     * Create:  2020/05/16
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    public class PermissionInfo
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
        /// 权限名
        /// </summary>
        public string MName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string MNote { get; set; }
        /// <summary>
        /// 具体权限项 
        /// </summary>
        public List<bool> MList { get; }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="yesno"></param>
        public PermissionInfo(int ID, int deleteID, string name, string note, bool yesno)
        {
            MID = ID;
            MDeleteID = deleteID;
            MName = name;
            MNote = note;

            MList = new List<bool>();
            for (int i = 0; i < Enum.GetNames(typeof(EnumPermission)).GetLength(0); i++)
            {
                MList.Add(yesno);
            }
        }

        /// <summary>
        /// 获取默认权限名的具体权限项
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static PermissionInfo GetDefault(EnumPermissionName index)
        {
            PermissionInfo item = new PermissionInfo(-1, 0, Share.ReadXaml.GetEnum(index, "A_Permission"), ReadXamlAdministration.S_DefaultPermission, false);
            switch (index)
            {
                case EnumPermissionName.Admin:
                    item = new PermissionInfo(-1, 0, Share.ReadXaml.GetEnum(index, "A_Permission"), ReadXamlAdministration.S_DefaultPermission, true);
                    break;
                case EnumPermissionName.IT:
                    for (int i = 0; i < item.MList.Count; i++)
                    {
                        if (((EnumPermission)i).ToString().Contains("Administration"))
                        {
                            item.MList[i] = true;
                        }
                    }
                    item.MList[(int)EnumPermission.Databases] = true;
                    break;
                case EnumPermissionName.OPER:
                    for (int i = 0; i < item.MList.Count; i++)
                    {
                        if (!((EnumPermission)i).ToString().Contains("Administration")
                            && !((EnumPermission)i).ToString().Contains("Project_Evaluation")
                            && i != (int)EnumPermission.Databases)
                        {
                            item.MList[i] = true;
                        }
                    }
                    break;
                case EnumPermissionName.QA:
                    for (int i = 0; i < item.MList.Count; i++)
                    {
                        if (((EnumPermission)i).ToString().Contains("Project_Evaluation"))
                        {
                            item.MList[i] = true;
                        }
                    }
                    break;
            }

            return item;
        }

        /// <summary>
        /// 复制全部信息
        /// </summary>
        /// <param name="item"></param>
        public void CopyInfo(PermissionInfo item)
        {
            MID = item.MID;
            MDeleteID = item.MDeleteID;
            MName = item.MName;
            MNote = item.MNote;

            for (int i = 0; i < MList.Count; i++)
            {
                MList[i] = item.MList[i];
            }
        }
    }
}
