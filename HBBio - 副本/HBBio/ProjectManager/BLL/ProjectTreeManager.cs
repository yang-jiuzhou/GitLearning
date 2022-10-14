using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.ProjectManager
{
    /**
     * ClassName: ProjectTreeManager
     * Description: 项目树管理
     * Version: 1.0
     * Create:  2020/11/12
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    class ProjectTreeManager
    {
        /// <summary>
        /// 生成项目树
        /// </summary>
        /// <param name="projectTree"></param>
        /// <returns></returns>
        public string CreateProjectTree(out ProjectTree projectTree)
        {
            List<TreeNode> list = null;
            ProjectTreeTable table = new ProjectTreeTable();
            string error = table.SelectListAll(out list);
            if (null == error)
            {
                projectTree = new ProjectTree(list);
            }
            else
            {
                projectTree = null;
            }

            return error;
        }

        /// <summary>
        /// 返回过滤的项目列表
        /// </summary>
        /// <param name="projectTree"></param>
        /// <returns></returns>
        public string GetProjectList(string filter, out List<TreeNode> list)
        {
            ProjectTreeTable table = new ProjectTreeTable();
            string error = table.SelectListFilter(filter, out list);

            return error;
        }

        /// <summary>
        /// 新建第一级结点
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string AddFirstItem(TreeNode item)
        {
            if (null == item || 0 != item.MParentId)
            {
                return Share.ReadXaml.S_ErrorNoData;
            }

            ProjectTreeTable table = new ProjectTreeTable();
            string error = table.InsertRow(item);
            if (null == error)
            {
                int id = -1;
                error = table.GetLastID(out id);
                if (null == error)
                {
                    item.MId = id;

                    //第一级节点默认增加一个手动
                    error = table.InsertRow(new TreeNode(item.MId, item.MUserID, "Manual", EnumType.Other));
                }
            }

            return error;
        }

        /// <summary>
        /// 新建普通结点
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string AddItem(ProjectTree tree, TreeNode item)
        {
            ProjectTreeTable table = new ProjectTreeTable();
            string error = table.InsertRow(item);
            if (null == error)
            {
                int id = -1;
                error = table.GetLastID(out id);
                if (null == error)
                {
                    item.MId = id;

                    if (null != tree)
                    {
                        tree.AddChild(item);
                    }
                }      
            }

            return error;
        }

        /// <summary>
        /// 更新结点名称
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string UpdateItem(TreeNode item)
        {
            ProjectTreeTable table = new ProjectTreeTable();
            return table.UpdateRow(item);
        }
        public string UpdateItemCountResult(int id)
        {
            ProjectTreeTable table = new ProjectTreeTable();
            return table.UpdateRowCountResult(id);
        }

        /// <summary>
        /// 删除结点
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string DeleteItem(ProjectTree tree, TreeNode item)
        {
            ProjectTreeTable table = new ProjectTreeTable();
            string error = table.DeleteRow(item.MId);
            if (null == error)
            {
                if (null != tree)
                {
                    tree.RemoveChild(item);
                }
            }

            return error;
        }

        /// <summary>
        /// 根据ID返回结点
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public TreeNode GetItem(ProjectTree tree, int id)
        {
            if (null != tree)
            {
                return tree.GetChild(id);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据ID返回用户的Manual结点
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public TreeNode GetItemManual(ProjectTree tree, int id)
        {
            if (null != tree)
            {
                return tree.GetManualId(id);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 设置结点为当前用户
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="id"></param>
        public void SetItemType(ProjectTree tree, int id)
        {
            if (null != tree)
            {
                tree.SetChildGeneral(1);
                tree.SetChildSelf(id);
            }
        }

        public int GetFirstNodeID(int id)
        {
            int result = -1;
            int parentID = -1;
            ProjectTreeTable table = new ProjectTreeTable();
            while (0 != parentID)
            {
                if (null == table.SelectRowParentID(id, out parentID))
                {
                    result = id;
                    id = parentID;
                }
            }

            return result;
        }
    }
}
