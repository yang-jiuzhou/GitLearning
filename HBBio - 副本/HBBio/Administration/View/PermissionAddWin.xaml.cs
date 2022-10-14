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
    /// PermissionAddWin.xaml 的交互逻辑
    /// </summary>
    public partial class PermissionAddWin : Window
    {
        private PermissionTree m_tree;      //权限列表生成的树


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="permissionInfo"></param>
        public PermissionAddWin(Window parent, PermissionInfo permissionInfo)
        {
            InitializeComponent();

            this.Owner = parent;
            this.ShowInTaskbar = false;

            AdministrationManager manager = new AdministrationManager();
            m_tree = manager.CreatePermissionTree(permissionInfo, permissionInfo);
            tree.ItemsSource = m_tree.MPermissionNodes;
        }

        /// <summary>
        /// 数据检查
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            if (TextLegal.NameLegal(txtName.Text))
            {
                return true;
            }
            else
            {
                MessageBoxWin.Show(Share.ReadXaml.S_ErrorIllegalName);
                return false;
            }         
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckData())
            {
                return;
            }

            PermissionInfo item = new PermissionInfo(-1, 0, txtName.Text, txtNote.Text, false);
            for (int i = 0; i < m_tree.MBasicNodes.Count; i++)
            {
                item.MList[i] = m_tree.MBasicNodes[i].MYesNo;
            }

            AdministrationManager manager = new AdministrationManager();
            string error = manager.AddPermission(item);
            if (null == error)
            {
                string modify = this.labName.Text + this.txtName.Text + "\n" +
                    this.labNote.Text + this.txtNote.Text + "\n";
                foreach(var it in m_tree.MBasicNodes)
                {
                    modify += it.MName + " : " + (it.MYesNo ? Share.ReadXaml.S_Enabled : Share.ReadXaml.S_Disabled) + "\n";
                }
                AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, modify);

                DialogResult = true;
            }
            else
            {
                MessageBoxWin.Show(error);
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
