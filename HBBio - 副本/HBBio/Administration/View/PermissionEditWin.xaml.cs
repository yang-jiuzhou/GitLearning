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
using System.Windows.Shapes;

namespace HBBio.Administration
{
    /// <summary>
    /// PermissionEditWin.xaml 的交互逻辑
    /// </summary>
    public partial class PermissionEditWin : Window
    {
        private PermissionTree m_tree;                      //权限列表生成的树
        private PermissionInfo m_permissionInfo = null;     //当前权限


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="currPermissionInfo"></param>
        /// <param name="permissionInfo"></param>
        public PermissionEditWin(Window parent, PermissionInfo currPermissionInfo, PermissionInfo permissionInfo)
        {
            InitializeComponent();

            this.Owner = parent;
            this.ShowInTaskbar = false;

            m_permissionInfo = permissionInfo;

            txtName.Text = m_permissionInfo.MName;
            txtNote.Text = m_permissionInfo.MNote;

            AdministrationManager manager = new AdministrationManager();
            m_tree = manager.CreatePermissionTree(permissionInfo, currPermissionInfo);
            tree.ItemsSource = m_tree.MPermissionNodes;

            if (1 == permissionInfo.MID)
            {
                //第一个权限不可编辑
                this.btnOK.IsEnabled = false;
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string modify = "";

            if (!m_permissionInfo.MNote.Equals(txtNote.Text))
            {
                modify += this.labNote.Text + this.txtNote.Text + "\n";

                m_permissionInfo.MNote = txtNote.Text;
            }
            
            for (int i = 0; i < m_tree.MBasicNodes.Count; i++)
            {
                if (m_permissionInfo.MList[i] != m_tree.MBasicNodes[i].MYesNo)
                {
                    if (m_tree.MBasicNodes[i].MYesNo)
                    {
                        modify += m_tree.MBasicNodes[i].MName + " : " + Share.ReadXaml.S_Disabled + " -> " + Share.ReadXaml.S_Enabled + "\n";
                    }
                    else
                    {
                        modify += m_tree.MBasicNodes[i].MName + " : " + Share.ReadXaml.S_Enabled + " -> " + Share.ReadXaml.S_Disabled + "\n";
                    }

                    m_permissionInfo.MList[i] = m_tree.MBasicNodes[i].MYesNo;
                }
            }

            if (!string.IsNullOrEmpty(modify))
            {
                modify = this.labName.Text + " : " + this.txtName.Text + "\n" + modify;

                AdministrationManager manager = new AdministrationManager();
                string error = manager.EditPermission(m_permissionInfo);
                if (null == error)
                {
                    AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, modify);

                    DialogResult = true;
                }
                else
                {
                    MessageBoxWin.Show(error);
                }
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
