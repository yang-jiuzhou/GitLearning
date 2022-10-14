using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Administration
{
    /**
     * ClassName: PermissionTree
     * Description: 权限树
     * Version: 1.0
     * Create:  2020/05/16
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    class PermissionTree
    {
        /// <summary>
        /// 权限列表
        /// </summary>
        public List<PermissionNode> MBasicNodes { get; set; }
        /// <summary>
        /// 权限树
        /// </summary>
        public List<PermissionNode> MPermissionNodes { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nodes"></param>
        public PermissionTree(List<PermissionNode> nodes)
        {
            MBasicNodes = nodes;
            MPermissionNodes = SortNodes("", nodes);
        }

        /// <summary>
        /// 从列表转化为树
        /// </summary>
        /// <param name="parentName"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private List<PermissionNode> SortNodes(string parentName, List<PermissionNode> nodes)
        {
            List<PermissionNode> mainNodes = nodes.Where(x => x.MParentName == parentName).ToList();
            List<PermissionNode> otherNodes = nodes.Where(x => x.MParentName != parentName).ToList();
            foreach (PermissionNode node in mainNodes)
            {
                node.MChildList = SortNodes(node.MName, otherNodes);

                foreach(PermissionNode child in node.MChildList)
                {
                    child.MParent = node;
                }
            } 

            return mainNodes;
        }
    }
}
