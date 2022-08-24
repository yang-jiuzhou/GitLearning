using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.ProjectManager
{
    /**
     * ClassName: ProjectTree
     * Description: 项目树
     * Version: 1.0
     * Create:  2020/11/12
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    class ProjectTree
    {
        //第一级结点，所有用户的名称，包括以删除的用户
        public ObservableCollection<TreeNode> MTreeNodes { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nodes"></param>
        public ProjectTree(List<TreeNode> nodes)
        {
            MTreeNodes = new ObservableCollection<TreeNode>(SortNodes(0, nodes));
        }

        /// <summary>
        /// 添加结点
        /// </summary>
        /// <param name="node"></param>
        public void AddChild(TreeNode node)
        {
            foreach (var it in MTreeNodes)
            {
                if (it.AddChild(node))
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 删除结点
        /// </summary>
        /// <param name="node"></param>
        public void RemoveChild(TreeNode node)
        {
            foreach (var it in MTreeNodes)
            {
                if (it.RemoveChild(node))
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 获取指定ID的结点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TreeNode GetChild(int id)
        {
            TreeNode node = null;

            foreach (var it in MTreeNodes)
            {
                if (it.MId == id)
                {
                    return it;
                }

                if (it.GetChild(id, ref node))
                {
                    return node;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取用户的manual的ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TreeNode GetManualId(int id)
        {
            foreach (var it in MTreeNodes)
            {
                if (it.MId == id)
                {
                    foreach (var itt in it.MChildList)
                    {
                        if (itt.MName.Equals("Manual"))
                        {
                            return itt;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 设置General所有子结点
        /// </summary>
        /// <param name="id"></param>
        public void SetChildGeneral(int id)
        {
            foreach (var it in MTreeNodes)
            {
                if (it.MId == id)
                {
                    it.SetChildGeneral();
                    return;
                }
            }
        }

        /// <summary>
        /// 设置当前用户所有子结点
        /// </summary>
        /// <param name="id"></param>
        public void SetChildSelf(int id)
        {
            foreach (var it in MTreeNodes)
            {
                if (it.MId == id)
                {
                    it.SetChildSelf();
                    return;
                }
            }
        }


        /// <summary>
        /// 结点排序（链表转化为树）
        /// </summary>
        /// <param name="parentID"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private List<TreeNode> SortNodes(int parentID, List<TreeNode> nodes)
        {
            List<TreeNode> mainNodes = nodes.Where(x => x.MParentId == parentID).ToList();
            List<TreeNode> otherNodes = nodes.Where(x => x.MParentId != parentID).ToList();

            foreach (TreeNode node in mainNodes)
            {
                node.MChildList = new ObservableCollection<TreeNode>(SortNodes(node.MId, otherNodes));

                foreach (TreeNode child in node.MChildList)
                {
                    child.MParentId = node.MId;
                }
            }
            return mainNodes;
        }
    }
}
