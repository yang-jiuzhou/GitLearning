using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Administration
{
    /**
     * ClassName: PermissionNode
     * Description: 权限树结点
     * Version: 1.0
     * Create:  2020/05/16
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    class PermissionNode : DlyNotifyPropertyChanged
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string MName { get; set; }
        /// <summary>
        /// 父亲名称
        /// </summary>
        public string MParentName { get; set; }
        /// <summary>
        /// 是否勾选
        /// </summary>
        public bool MYesNo
        {
            get
            {
                return m_yesno;
            }
            set
            {
                m_yesno = value;

                OnPropertyChanged("MYesNo");

                if (!m_yesno)
                {
                    //取消勾选，则子结点都被取消
                    if (null != MChildList)
                    {
                        foreach (var it in MChildList)
                        {
                            it.MYesNo = false;
                        }
                    }
                }
                else
                {
                    //勾选，则父结点也应该被勾选
                    if (null != MParent)
                    {
                        MParent.MYesNo = true;
                    }
                }
            }
        }
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool MEnabled { get; set; }
        /// <summary>
        /// 父亲结点
        /// </summary>
        public PermissionNode MParent { get; set; }
        /// <summary>
        /// 子结点列表
        /// </summary>
        public List<PermissionNode> MChildList { get; set; }

        /// <summary>
        /// 是否勾选
        /// </summary>
        private bool m_yesno = false;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        /// <param name="yesno"></param>
        /// <param name="enabled"></param>
        public PermissionNode(string name, string parentName, bool yesno, bool enabled)
        {
            MName = name;
            MParentName = parentName;        
            MYesNo = yesno;
            MEnabled = enabled;
            MChildList = new List<PermissionNode>();
        }
    }
}
