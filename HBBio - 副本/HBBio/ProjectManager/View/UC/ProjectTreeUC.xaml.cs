using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HBBio.ProjectManager
{
    /// <summary>
    /// ProjectTreeUC.xaml 的交互逻辑
    /// </summary>
    public partial class ProjectTreeUC : UserControl
    {
        public int MDefaultId { get; set; }
        public string MDefaultUser { get; set; }
        public bool MShowManual { get; set; }
        public string MSelectPath
        {
            get
            {
                string path = ((TreeNode)treeView.SelectedItem).MName;
                TreeNode node = m_tree.GetChild(((TreeNode)treeView.SelectedItem).MId);
                while (0 != node.MParentId)
                {
                    node = m_tree.GetChild(node.MParentId);
                    path = node.MName + "/" + path;
                }
                return path;
            }
        }

        private ProjectTree m_tree = null;
        private TreeViewItem m_select = null;
        private string m_nameOld = null;
        private bool m_isNew = false;


        /// <summary>
        /// 自定义事件，选中节点时触发
        /// </summary>
        public static readonly RoutedEvent MSelectEvent =
             EventManager.RegisterRoutedEvent("MSelectItem", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ProjectTreeUC));
        public event RoutedEventHandler MSelectItem
        {
            add { AddHandler(MSelectEvent, value); }
            remove { RemoveHandler(MSelectEvent, value); }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        public ProjectTreeUC()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 更新方法数量
        /// </summary>
        /// <param name="addDel"></param>
        public void UpdateCountMethod(bool addDel)
        {
            TreeNode temp = (TreeNode)treeView.SelectedItem;
            if (addDel)
            {
                temp.MCountMethod += 1;
            }
            else
            {
                temp.MCountMethod -= 1;
            }

            ProjectTreeManager manager = new ProjectTreeManager();
            manager.UpdateItem(temp);
        }

        /// <summary>
        /// 更新结果数量
        /// </summary>
        public void UpdateCountResult()
        {
            TreeNode temp = (TreeNode)treeView.SelectedItem;
            temp.MCountResult += 1;

            ProjectTreeManager manager = new ProjectTreeManager();
            manager.UpdateItem(temp);
        }

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ProjectTreeManager manager = new ProjectTreeManager();
            if (null == manager.CreateProjectTree(out m_tree))
            {
                treeView.ItemsSource = m_tree.MTreeNodes;
                manager.SetItemType(m_tree, MDefaultId);
                TreeViewItem child = (TreeViewItem)treeView.ItemContainerGenerator.ContainerFromItem(manager.GetItem(m_tree, MDefaultId));
                if (MShowManual)
                {
                    //必须展开后才能查找
                    child.IsExpanded = true;
                    child.UpdateLayout();
                    child = (TreeViewItem)child.ItemContainerGenerator.ContainerFromItem(manager.GetItemManual(m_tree, MDefaultId));
                }

                if (null != child)
                {
                    child.IsSelected = true;
                }
            }
        }

        /// <summary>
        /// 目录筛选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (0 == txtFilter.Text.Length)
            {
                treeView.Visibility = Visibility.Visible;
                listbox.Visibility = Visibility.Hidden;
                btnNew.IsEnabled = true;

                ProjectTreeManager manager = new ProjectTreeManager();
                if (null == manager.CreateProjectTree(out m_tree))
                {
                    treeView.ItemsSource = m_tree.MTreeNodes;
                    manager.SetItemType(m_tree, MDefaultId);
                    TreeViewItem child = (TreeViewItem)treeView.ItemContainerGenerator.ContainerFromItem(manager.GetItem(m_tree, MDefaultId));
                    if (MShowManual)
                    {
                        //必须展开后才能查找
                        child.IsExpanded = true;
                        child.UpdateLayout();
                        child = (TreeViewItem)child.ItemContainerGenerator.ContainerFromItem(manager.GetItemManual(m_tree, MDefaultId));
                    }

                    if (null != child)
                    {
                        child.IsSelected = true;
                    }
                }
            }
            else
            {
                treeView.Visibility = Visibility.Hidden;
                listbox.Visibility = Visibility.Visible;
                btnNew.IsEnabled = false;

                ProjectTreeManager manager = new ProjectTreeManager();
                List<TreeNode> list = null;
                if (null == manager.GetProjectList(txtFilter.Text, out list))
                {
                    listbox.ItemsSource = list;
                    if (0 != listbox.Items.Count)
                    {
                        listbox.SelectedIndex = 0;
                    }
                }
            }
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            if (null != treeView.SelectedItem)
            {
                ProjectTreeManager manager = new ProjectTreeManager();
                manager.AddItem(m_tree, new TreeNode(((TreeNode)treeView.SelectedItem).MId, Administration.AdministrationStatic.Instance().MUserInfo.MID, "NewFolder", ((TreeNode)treeView.SelectedItem).MType));
                m_select.IsExpanded = true;

                Share.MApp.DoEvents();

                m_isNew = true;

                m_select = (TreeViewItem)m_select.ItemContainerGenerator.ContainerFromIndex(m_select.Items.Count - 1);
                m_select.Focus();
                btnRename_Click(null, null);
            }
        }

        /// <summary>
        /// 重命名节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRename_Click(object sender, RoutedEventArgs e)
        {
            //获取在TreeView.ItemTemplate中定义的TextBox控件
            TextBox txt = MVisual.FindVisualChild<TextBox>(m_select as DependencyObject);

            if (null != txt)
            {
                m_nameOld = txt.Text;

                //设置该TextBox的Visibility 属性为Visible
                txt.Visibility = Visibility.Visible;
                txt.Focus();
                txt.SelectAll();
            }
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (null != treeView.SelectedItem)
            {
                if (((TreeNode)treeView.SelectedItem).GetChildCountMethodOrResult())
                {
                    MessageBoxWin.Show(Share.ReadXaml.GetResources("PM_ProjectTreeUC_Msg_NotNull"));
                    return;
                }

                ProjectTreeManager manager = new ProjectTreeManager();
                manager.DeleteItem(m_tree, (TreeNode)treeView.SelectedItem);

                AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(Share.ReadXaml.GetResources("labCatalog") + btnDel.ToolTip, MSelectPath);
            }
        }

        /// <summary>
        /// 节点属性信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            if (Visibility.Visible == treeView.Visibility)
            {
                if (null != treeView.SelectedItem)
                {
                    TreeNodeInformationWin win = new TreeNodeInformationWin();

                    TreeNode item = (TreeNode)treeView.SelectedItem;
                    Administration.AdministrationManager manager = new Administration.AdministrationManager();
                    string userName = null;
                    if (null != manager.GetUser(((TreeNode)treeView.SelectedItem).MUserID, out userName))
                    {
                        userName = "General";
                    }
                    item.MUserName = userName;
                    win.DataContext = item;

                    win.ShowDialog();
                }
            }
            else
            {
                if (null != listbox.SelectedItem)
                {
                    TreeNodeInformationWin win = new TreeNodeInformationWin();

                    TreeNode item = (TreeNode)listbox.SelectedItem;
                    Administration.AdministrationManager manager = new Administration.AdministrationManager();
                    string userName = null;
                    if (null != manager.GetUser(((TreeNode)listbox.SelectedItem).MUserID, out userName))
                    {
                        userName = "General";
                    }
                    item.MUserName = userName;
                    win.DataContext = item;

                    win.ShowDialog();
                }
            }
        }

        /// <summary>
        /// 回车失去焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRename_KeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
            {
                treeView.Focus();
            }
        }

        /// <summary>
        /// 完成重命名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRename_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBlock txb = MVisual.FindVisualChild<TextBlock>(m_select as DependencyObject);
            TextBox txt = MVisual.FindVisualChild<TextBox>(m_select as DependencyObject);

            if (null != txt && Visibility.Collapsed != txt.Visibility)
            {
                if (TextLegal.FileNameLegal(txt.Text) && !(MDefaultId == ((TreeNode)treeView.SelectedItem).MParentId && txt.Text.Equals("Manual")))
                {
                    //用户级结点的下级Manual结点只能有一个
                    ProjectTreeManager manager = new ProjectTreeManager();
                    manager.UpdateItem((TreeNode)treeView.SelectedItem);

                    if (m_isNew)
                    {
                        AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(Share.ReadXaml.GetResources("labCatalog") + btnNew.ToolTip, MSelectPath);
                    }
                    else
                    {
                        AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(Share.ReadXaml.GetResources("labCatalog") + btnRename.ToolTip, MSelectPath.Substring(0, MSelectPath.LastIndexOf("/") + 1) + m_nameOld + " ->" + MSelectPath);
                    }
                }
                else
                {
                    Share.MessageBoxWin.Show(Share.ReadXaml.S_ErrorIllegalName);

                    txb.Text = m_nameOld;
                    txt.Text = m_nameOld;
                }

                txt.Visibility = Visibility.Collapsed;

                m_isNew = false;
            }
        }

        /// <summary>
        /// 切换当前选中节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_Selected(object sender, RoutedEventArgs e)
        {
            if (null != m_select)
            {
                TextBlock txb = MVisual.FindVisualChild<TextBlock>(m_select as DependencyObject);
                TextBox txt = MVisual.FindVisualChild<TextBox>(m_select as DependencyObject);

                if (null != txt && Visibility.Collapsed!= txt.Visibility)
                {
                    txb.Text = m_nameOld;
                    txt.Text = m_nameOld;
                    txt.Visibility = Visibility.Collapsed;

                    m_isNew = false;
                }
            }

            m_select = e.OriginalSource as TreeViewItem;

            switch (((TreeNode)treeView.SelectedItem).MType)
            {
                case EnumType.General:
                    {
                        RoutedEventArgs args = new RoutedEventArgs(MSelectEvent, treeView.SelectedItem);
                        RaiseEvent(args);

                        btnNew.IsEnabled = true;
                        if (1 == ((TreeNode)treeView.SelectedItem).MId)
                        {
                            //第一级结点不可编辑
                            btnRename.IsEnabled = false;
                        }
                        else
                        {
                            btnRename.IsEnabled = true;
                        }
                    }
                    break;
                case EnumType.Self:
                    {
                        RoutedEventArgs args = new RoutedEventArgs(MSelectEvent, treeView.SelectedItem);
                        RaiseEvent(args);

                        btnNew.IsEnabled = true;
                        if (MDefaultId == ((TreeNode)treeView.SelectedItem).MId ||
                            MDefaultId == ((TreeNode)treeView.SelectedItem).MParentId && ((TreeNode)treeView.SelectedItem).MName.Equals("Manual"))
                        {
                            //用户级结点以及其下级Manual结点不可编辑
                            btnRename.IsEnabled = false;
                        }
                        else
                        {
                            btnRename.IsEnabled = true;
                        }
                    }
                    break;
                case EnumType.Other:
                    {
                        //或者用户权限的用户可查看数据
                        if (Administration.AdministrationStatic.Instance().ShowPermission(Administration.EnumPermission.Project_Method_Watch_Other)
                            || Administration.AdministrationStatic.Instance().ShowPermission(Administration.EnumPermission.Project_Evaluation_Watch_Other))
                        {
                            RoutedEventArgs args = new RoutedEventArgs(MSelectEvent, treeView.SelectedItem);
                            RaiseEvent(args);
                        }
                        else
                        {
                            RoutedEventArgs args = new RoutedEventArgs(MSelectEvent, null);
                            RaiseEvent(args);
                        }

                        btnNew.IsEnabled = false;
                        btnRename.IsEnabled = false;
                    }
                    break;
            }
        }

        /// <summary>
        /// 切换当前选中节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null == listbox.SelectedItem)
            {
                RoutedEventArgs args = new RoutedEventArgs(MSelectEvent, null);
                RaiseEvent(args);

                return;
            }

            switch (((TreeNode)listbox.SelectedItem).MType)
            {
                case EnumType.General:
                    {
                        RoutedEventArgs args = new RoutedEventArgs(MSelectEvent, listbox.SelectedItem);
                        RaiseEvent(args);
                    }
                    break;
                case EnumType.Self:
                    {
                        RoutedEventArgs args = new RoutedEventArgs(MSelectEvent, listbox.SelectedItem);
                        RaiseEvent(args);
                    }
                    break;
                case EnumType.Other:
                    {
                        //或者用户权限的用户可查看数据
                        if (Administration.AdministrationStatic.Instance().ShowPermission(Administration.EnumPermission.Project_Method_Watch_Other)
                            || Administration.AdministrationStatic.Instance().ShowPermission(Administration.EnumPermission.Project_Evaluation_Watch_Other))
                        {
                            RoutedEventArgs args = new RoutedEventArgs(MSelectEvent, listbox.SelectedItem);
                            RaiseEvent(args);
                        }
                        else
                        {
                            RoutedEventArgs args = new RoutedEventArgs(MSelectEvent, null);
                            RaiseEvent(args);
                        }
                    }
                    break;
            }
        }
    }
}
