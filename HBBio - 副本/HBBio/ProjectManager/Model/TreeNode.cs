using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.ProjectManager
{
    /**
     * ClassName: TreeNode
     * Description: 项目树结点
     * Version: 1.0
     * Create:  2020/11/12
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    class TreeNode
    {
        //标识号
        public int MId { get; set; }

        //父亲标识号
        public int MParentId { get; set; }

        //用户ID
        public int MUserID { get; set; }

        //名称
        public string MName { get; set; }

        //创建时间
        public string MCreateTime { get; set; }

        //类型
        public EnumType MType { get; set; }

        //用户名
        public string MUserName { get; set; }

        //方法数量
        public int MCountMethod { get; set; }

        //结果数量
        public int MCountResult { get; set; }

        //文件夹图标
        private string m_iconGeneral = "/Bio-LabChrom;component/Image/general.png";
        private string m_iconUser = "/Bio-LabChrom;component/Image/user.png";
        private string m_iconSelf = "/Bio-LabChrom;component/Image/folderClose.png";
        private string m_iconOther = "/Bio-LabChrom;component/Image/folderClose1.png";
        private string m_iconExpandedSelf = "/Bio-LabChrom;component/Image/folderOpen.png";
        private string m_iconExpandedOther = "/Bio-LabChrom;component/Image/folderOpen1.png";
        public string MIcon
        {
            get
            {
                if (0 == MParentId)
                {
                    if (1 == MId)
                    {
                        return m_iconGeneral;
                    }
                    else
                    {
                        return m_iconUser;
                    }
                }
                else
                {
                    switch (MType)
                    {
                        case EnumType.Self:
                            return m_iconSelf;
                        case EnumType.Other:
                            return m_iconOther;
                        default:
                            return m_iconSelf;
                    }
                }
            }
        }
        public string MIconExpanded
        {
            get
            {
                if (0 == MParentId)
                {
                    if (1 == MId)
                    {
                        return m_iconGeneral;
                    }
                    else
                    {
                        return m_iconUser;
                    }
                }
                else
                {
                    switch (MType)
                    {
                        case EnumType.Self:
                            return m_iconExpandedSelf;
                        case EnumType.Other:
                            return m_iconExpandedOther;
                        default:
                            return m_iconExpandedSelf;
                    }
                }
            }
        }

        //子结点列表
        private ObservableCollection<TreeNode> m_childList = new ObservableCollection<TreeNode>();
        public ObservableCollection<TreeNode> MChildList
        {
            get
            {
                return m_childList;
            }
            set
            {
                m_childList = value;
            }
        }


        /// <summary>
        /// 构造函数，新建结点
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="name"></param>
        public TreeNode(int parentId, int userID, string name, EnumType type)
        {
            MParentId = parentId;
            MUserID = userID;
            MName = name;
            MType = type;
            MCreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            MCountMethod = 0;
            MCountResult = 0;
        }

        /// <summary>
        /// 构造函数,从数据库获取
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parentId"></param>
        /// <param name="name"></param>
        public TreeNode(int id, int parentId, int userID, string name, string datetime, int countMethod, int countResult, EnumType type)
        {
            MId = id;
            MParentId = parentId;
            MUserID = userID;
            MName = name;
            MType = EnumType.Other;
            MCreateTime = datetime;
            MCountMethod = countMethod;
            MCountResult = countResult;
        }

        /// <summary>
        /// 插入结点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool AddChild(TreeNode node)
        {
            if (MId == node.MParentId)
            {
                MChildList.Add(node);
                return true;
            }
            else
            {
                for (int i = 0; i < MChildList.Count; i++)
                {
                    if (MChildList[i].AddChild(node))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 删除结点
        /// </summary>
        /// <param name="node"></param>
        public bool RemoveChild(TreeNode node)
        {
            for (int i = 0; i < MChildList.Count; i++)
            {
                if (MChildList[i].MId == node.MId)
                {
                    MChildList.RemoveAt(i);
                    return true;
                }
                else
                {
                    if (MChildList[i].RemoveChild(node))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 根据ID获取指定结点
        /// </summary>
        /// <param name="id"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool GetChild(int id, ref TreeNode node)
        {
            for (int i = 0; i < MChildList.Count; i++)
            {
                if (MChildList[i].MId == id)
                {
                    node = MChildList[i];
                    return true;
                }
                else
                {
                    if (MChildList[i].GetChild(id, ref node))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 设置所有子结点为General
        /// </summary>
        public void SetChildGeneral()
        {
            MType = EnumType.General;
            foreach (var it in MChildList)
            {
                it.SetChildGeneral();
            }
        }

        /// <summary>
        /// 设置所有子结点为当前用户
        /// </summary>
        public void SetChildSelf()
        {
            MType = EnumType.Self;
            foreach (var it in MChildList)
            {
                it.SetChildSelf();
            }
        }

        /// <summary>
        /// 返回是否存在方法或结果数量大于0的节点
        /// </summary>
        /// <returns></returns>
        public bool GetChildCountMethodOrResult()
        {
            if (MCountMethod > 0 || MCountResult > 0)
            {
                return true;
            }
            else
            {
                foreach (var it in MChildList)
                {
                    if (it.GetChildCountMethodOrResult())
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }

    /// <summary>
    /// 结点的类型
    /// </summary>
    public enum EnumType
    {
        General,    //共用
        Self,       //自己的结点
        Other       //别人的结点
    }
}
