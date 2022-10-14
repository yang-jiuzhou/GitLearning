using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// AdministrationWin.xaml 的交互逻辑
    /// </summary>
    public partial class AdministrationWin : Window, WindowPermission
    {
        private UserInfo m_userInfo = null;                         //用户信息（传入，只能修改，不能赋值）
        private PermissionInfo m_permissionInfo = null;             //权限信息（传入，只能修改，不能赋值）
        private TacticsInfo m_tacticsInfo = null;                   //安全策略信息（传入，只能修改，不能赋值）
        private SignerReviewerInfo m_signerReviewerInfo = null;     //电子签名信息（传入，只能修改，不能赋值）

        /// <summary>
        /// 自定义事件，点击自检按钮时触发
        /// </summary>
        public static readonly RoutedEvent MAddUserEvent =
             EventManager.RegisterRoutedEvent("MAddUser", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(AdministrationWin));
        public event RoutedEventHandler MAddUser
        {
            add { AddHandler(MAddUserEvent, value); }
            remove { RemoveHandler(MAddUserEvent, value); }
        }

        /// <summary>
        /// 自定义事件，修改当前用户权限时触发
        /// </summary>
        public static readonly RoutedEvent MUpdatePermissionEvent =
             EventManager.RegisterRoutedEvent("UpdatePermission", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(AdministrationWin));
        public event RoutedEventHandler MUpdatePermission
        {
            add { AddHandler(MUpdatePermissionEvent, value); }
            remove { RemoveHandler(MUpdatePermissionEvent, value); }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        public AdministrationWin(Window parent)
        {
            InitializeComponent();

            this.Owner = parent;

            m_userInfo = AdministrationStatic.Instance().MUserInfo;
            m_permissionInfo = AdministrationStatic.Instance().MPermissionInfo;
            m_tacticsInfo = AdministrationStatic.Instance().MTacticsInfo;
            m_signerReviewerInfo = AdministrationStatic.Instance().MSignerReviewerInfo;

            InitDGVUser();
            InitDGVPermission();
            InitDGVTactics();
            InitDGVSignerReviewer();
        }

        /// <summary>
        /// 设置各模块是否可用
        /// </summary>
        /// <param name="info"></param>
        public bool SetPermission(PermissionInfo info)
        {
            if (info.MList[(int)EnumPermission.Administration])
            {
                tabItemUser.Visibility = info.MList[(int)EnumPermission.Administration_User] ? Visibility.Visible : Visibility.Collapsed;
                if (info.MList[(int)EnumPermission.Administration_User])
                {
                    btnAddUser.IsEnabled = info.MList[(int)EnumPermission.Administration_User_Add];
                    btnEditUser.IsEnabled = info.MList[(int)EnumPermission.Administration_User_Edit];
                    btnDelUser.IsEnabled = info.MList[(int)EnumPermission.Administration_User_Del];
                }
                tabItemPermission.Visibility = info.MList[(int)EnumPermission.Administration_Permission] ? Visibility.Visible : Visibility.Collapsed;
                if (info.MList[(int)EnumPermission.Administration_Permission])
                {
                    btnAddPermission.IsEnabled = info.MList[(int)EnumPermission.Administration_Permission_Add];
                    btnEditPermission.IsEnabled = info.MList[(int)EnumPermission.Administration_Permission_Edit];
                    btnDelPermission.IsEnabled = info.MList[(int)EnumPermission.Administration_Permission_Del];
                }
                tabItemTactics.Visibility = info.MList[(int)EnumPermission.Administration_Tactics] ? Visibility.Visible : Visibility.Collapsed;
                if (info.MList[(int)EnumPermission.Administration_Tactics])
                {
                    btnEditTactics.IsEnabled = info.MList[(int)EnumPermission.Administration_Tactics_Edit];
                }
                tabItemSignerReviewer.Visibility = info.MList[(int)EnumPermission.Administration_SignerReviewer] ? Visibility.Visible : Visibility.Collapsed;
                if (info.MList[(int)EnumPermission.Administration_SignerReviewer])
                {
                    btnEditSignerReviewer.IsEnabled = info.MList[(int)EnumPermission.Administration_SignerReviewer_Edit];
                }

                return true;
            }
            else
            {
                this.Close();

                return false;
            }
        }

        /// <summary>
        /// 加载用户列表
        /// </summary>
        private void InitDGVUser()
        {
            List<UserInfo> list = null;
            AdministrationManager manager = new AdministrationManager();
            if (null == manager.GetUserInfoList(out list))
            {
                foreach (var it in list)
                {
                    AdministrationStatic.Instance().JudgeUserStatus(it, m_tacticsInfo);
                }
                dgvUser.ItemsSource = list;
            }
        }

        /// <summary>
        /// 加载权限列表
        /// </summary>
        private void InitDGVPermission()
        {
            List<PermissionInfo> list = null;
            AdministrationManager manager = new AdministrationManager();
            if (null == manager.GetPermissionInfoList(out list))
            {
                dgvPermission.ItemsSource = list;
            }
        }

        /// <summary>
        /// 加载安全策略列表
        /// </summary>
        private void InitDGVTactics()
        {
            AdministrationManager manager = new AdministrationManager();
            dgvTactics.ItemsSource = manager.GetTacticsRowList(m_tacticsInfo);
        }

        /// <summary>
        /// 加载签名审核列表
        /// </summary>
        private void InitDGVSignerReviewer()
        {
            AdministrationManager manager = new AdministrationManager();
            dgvSignerReviewer.ItemsSource = manager.GetSignerReviewerRowList(m_signerReviewerInfo);
        }

        /// <summary>
        /// 加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = -1;
            foreach (TabItem it in tabControl.Items)
            {
                if (Visibility.Visible == it.Visibility)
                {
                    tabControl.SelectedItem = it;
                    break;
                }
            }
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            if (null != this.Owner)
            {
                this.Owner.Focus();
            }
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            if (!AdministrationStatic.Instance().ShowSignerReviewerWin(this, EnumSignerReviewer.Administration_User_Add))
            {
                return;
            }

            UserAddWin dlg = new UserAddWin(this, m_tacticsInfo);
            if (true == dlg.ShowDialog())
            {
                AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.tabItemUser.Header.ToString() + btnAddUser.Content, dlg.MOper);

                InitDGVUser();

                RoutedEventArgs args = new RoutedEventArgs(MAddUserEvent, null);
                RaiseEvent(args);
            }
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            if (-1 != dgvUser.SelectedIndex)
            {
                if (!AdministrationStatic.Instance().ShowSignerReviewerWin(this, EnumSignerReviewer.Administration_User_Edit))
                {
                    return;
                }

                UserEditWin dlg = new UserEditWin(this);
                dlg.MUserInfo = (UserInfo)dgvUser.SelectedItem;
                //如果是当前用户，直接传静态变量
                if (dlg.MUserInfo.MID.Equals(m_userInfo.MID))
                {
                    dlg.MUserInfo = m_userInfo;
                }
                if (true == dlg.ShowDialog())
                {
                    AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.tabItemUser.Header.ToString() + btnEditUser.Content, dlg.MOper);

                    InitDGVUser();

                    //如果是当前用户，需要刷新权限
                    if (dlg.MUserInfo.MID.Equals(m_userInfo.MID))
                    {
                        RoutedEventArgs args = new RoutedEventArgs(MUpdatePermissionEvent, null);
                        RaiseEvent(args);
                    }
                }
            }
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvUser_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnEditUser_Click(null, null);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelUser_Click(object sender, RoutedEventArgs e)
        {
            if (-1 != dgvUser.SelectedIndex)
            {
                if (1 == ((UserInfo)dgvUser.SelectedItem).MID)
                {
                    MessageBoxWin.Show(Share.ReadXaml.GetResources("A_UserDelNot"));
                    return;
                }

                if (m_userInfo.MID == ((UserInfo)dgvUser.SelectedItem).MID)
                {
                    MessageBoxWin.Show(Share.ReadXaml.GetResources("A_UserCurrDelNot"));
                    return;
                }

                if (MessageBoxResult.Yes == Share.MessageBoxWin.Show(Share.ReadXaml.S_Continue, this.tabItemUser.Header.ToString() + btnDelUser.Content, MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    if (!AdministrationStatic.Instance().ShowSignerReviewerWin(this, EnumSignerReviewer.Administration_User_Del))
                    {
                        return;
                    }

                    AdministrationManager manager = new AdministrationManager();
                    if (null == manager.DelUser((UserInfo)dgvUser.SelectedItem))
                    {
                        AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.tabItemUser.Header.ToString() + btnDelUser.Content, ((UserInfo)dgvUser.SelectedItem).MUserName);

                        InitDGVUser();
                    }
                }
            }
        }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddPermission_Click(object sender, RoutedEventArgs e)
        {
            if (!AdministrationStatic.Instance().ShowSignerReviewerWin(this, EnumSignerReviewer.Administration_Permission_Add))
            {
                return;
            }

            PermissionAddWin win = new PermissionAddWin(this, m_permissionInfo);
            if (true == win.ShowDialog())
            {
                InitDGVPermission();
            }
        }

        /// <summary>
        /// 编辑权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditPermission_Click(object sender, RoutedEventArgs e)
        {
            if (-1 != dgvPermission.SelectedIndex)
            {
                if (!AdministrationStatic.Instance().ShowSignerReviewerWin(this, EnumSignerReviewer.Administration_Permission_Edit))
                {
                    return;
                }

                PermissionInfo tmp = (PermissionInfo)dgvPermission.SelectedItem;
                PermissionEditWin win = new PermissionEditWin(this, m_permissionInfo, tmp);
                if (true == win.ShowDialog())
                {
                    InitDGVPermission();

                    //如果是当前权限，需要判断是否修改权限
                    if (tmp.MID.Equals(m_permissionInfo.MID))
                    {
                        RoutedEventArgs args = new RoutedEventArgs(MUpdatePermissionEvent, null);
                        RaiseEvent(args);

                        m_permissionInfo.CopyInfo(tmp);
                    }
                }
            }
        }

        /// <summary>
        /// 编辑权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvPermission_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnEditPermission_Click(null, null);
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelPermission_Click(object sender, RoutedEventArgs e)
        {
            if (-1 != dgvPermission.SelectedIndex)
            {
                if (1 == ((PermissionInfo)dgvPermission.SelectedItem).MID)
                {
                    MessageBoxWin.Show(Share.ReadXaml.GetResources("A_PermissionDelNot"));
                    return;
                }

                if (MessageBoxResult.Yes == Share.MessageBoxWin.Show(Share.ReadXaml.S_Continue, this.tabItemPermission.Header.ToString() + btnDelPermission.Content, MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    if (!AdministrationStatic.Instance().ShowSignerReviewerWin(this, EnumSignerReviewer.Administration_Permission_Del))
                    {
                        return;
                    }

                    AdministrationManager manager = new AdministrationManager();
                    string error = manager.DelPermission((PermissionInfo)dgvPermission.SelectedItem);
                    if (null == error)
                    {
                        AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.tabItemPermission.Header.ToString() + btnDelPermission.Content, ((PermissionInfo)dgvPermission.SelectedItem).MName);

                        InitDGVPermission();
                    }
                    else
                    {
                        MessageBoxWin.Show(error);
                    }
                }
            }
        }

        /// <summary>
        /// 编辑安全策略
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditTactics_Click(object sender, RoutedEventArgs e)
        {
            if (-1 != dgvTactics.SelectedIndex)
            {
                if (!AdministrationStatic.Instance().ShowSignerReviewerWin(this, EnumSignerReviewer.Administration_Tactics_Edit))
                {
                    return;
                }

                TacticsEditWin win = new TacticsEditWin(this, (TacticsRow)dgvTactics.SelectedItem);
                if (true == win.ShowDialog())
                {
                    m_tacticsInfo.SetValue(((TacticsRow)dgvTactics.SelectedItem).MIndex, ((TacticsRow)dgvTactics.SelectedItem).MValue);
                    InitDGVTactics();
                }
            }
        }

        /// <summary>
        /// 编辑安全策略
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvTactics_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (btnEditTactics.IsEnabled)
            {
                btnEditTactics_Click(null, null);
            }
        }

        /// <summary>
        /// 编辑签名审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditSignerReviewer_Click(object sender, RoutedEventArgs e)
        {
            if (!AdministrationStatic.Instance().ShowSignerReviewerWin(this, EnumSignerReviewer.Administration_SignerReviewer_Edit))
            {
                return;
            }

            StringBuilderSplit sb = new StringBuilderSplit();
            for (int i = 0; i < dgvSignerReviewer.Items.Count; i++)
            {
                //签名列
                if (((SignerReviewerRow)dgvSignerReviewer.Items[i]).MSigner != m_signerReviewerInfo.MListSigner[i])
                {
                    if (m_signerReviewerInfo.MListSigner[i])
                    {
                        sb.Append(((SignerReviewerRow)dgvSignerReviewer.Items[i]).MName + "-" + dgvSignerReviewer.Columns[1].Header + " : " + Share.ReadXaml.S_Enabled + " -> " + Share.ReadXaml.S_Disabled);
                    }
                    else
                    {
                        sb.Append(((SignerReviewerRow)dgvSignerReviewer.Items[i]).MName + "-" + dgvSignerReviewer.Columns[1].Header + " : " + Share.ReadXaml.S_Disabled + " -> " + Share.ReadXaml.S_Enabled);
                    }
                }
                //审核列
                if (((SignerReviewerRow)dgvSignerReviewer.Items[i]).MReviewer != m_signerReviewerInfo.MListReviewer[i])
                {
                    if (m_signerReviewerInfo.MListReviewer[i])
                    {
                        sb.Append(((SignerReviewerRow)dgvSignerReviewer.Items[i]).MName + "-" + dgvSignerReviewer.Columns[2].Header + " : " + Share.ReadXaml.S_Enabled + " -> " + Share.ReadXaml.S_Disabled);
                    }
                    else
                    {
                        sb.Append(((SignerReviewerRow)dgvSignerReviewer.Items[i]).MName + "-" + dgvSignerReviewer.Columns[2].Header + " : " + Share.ReadXaml.S_Disabled + " -> " + Share.ReadXaml.S_Enabled);
                    }
                }
            }
            string changeStr = sb.ToString();
            if (!string.IsNullOrEmpty(changeStr))
            {
                SignerReviewerInfo info = new SignerReviewerInfo();
                for (int i = 0; i < dgvSignerReviewer.Items.Count; i++)
                {
                    info.MListSigner[i] = ((SignerReviewerRow)dgvSignerReviewer.Items[i]).MSigner;
                    info.MListReviewer[i] = ((SignerReviewerRow)dgvSignerReviewer.Items[i]).MReviewer;
                }

                AdministrationManager manager = new AdministrationManager();
                string error = manager.EditSignerReviewer(info);
                if (null == error)
                {
                    for (int i = 0; i < dgvSignerReviewer.Items.Count; i++)
                    {
                        m_signerReviewerInfo.MListSigner[i] = info.MListSigner[i];
                        m_signerReviewerInfo.MListReviewer[i] = info.MListReviewer[i];
                    }
                    AuditTrails.AuditTrailsStatic.Instance().InsertRowOperate(this.tabItemSignerReviewer.Header + "-" + this.btnEditSignerReviewer.Content, changeStr);
                    MessageBoxWin.Show(btnEditSignerReviewer.Content + Share.ReadXaml.S_Success);
                }
                else
                {
                    InitDGVSignerReviewer();
                    AuditTrails.AuditTrailsStatic.Instance().InsertRowOperate(this.tabItemSignerReviewer.Header + "-" + this.btnEditSignerReviewer.Content, error);
                    MessageBoxWin.Show(error);
                }
            }
        }

        /// <summary>
        /// 签名审核的勾选逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSignerReviewer_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (-1 != dgvSignerReviewer.SelectedIndex)
            {
                if (1 == dgvSignerReviewer.CurrentCell.Column.DisplayIndex)
                {
                    if (((SignerReviewerRow)dgvSignerReviewer.SelectedItem).MSigner)
                    {
                        ((SignerReviewerRow)dgvSignerReviewer.SelectedItem).MSigner = false;
                        ((SignerReviewerRow)dgvSignerReviewer.SelectedItem).MReviewer = false;
                    }
                    else
                    {
                        ((SignerReviewerRow)dgvSignerReviewer.SelectedItem).MSigner = true;
                    }
                }
                else if (2 == dgvSignerReviewer.CurrentCell.Column.DisplayIndex)
                {
                    if (((SignerReviewerRow)dgvSignerReviewer.SelectedItem).MReviewer)
                    {
                        ((SignerReviewerRow)dgvSignerReviewer.SelectedItem).MReviewer = false;
                    }
                    else
                    {
                        ((SignerReviewerRow)dgvSignerReviewer.SelectedItem).MReviewer = true;
                        ((SignerReviewerRow)dgvSignerReviewer.SelectedItem).MSigner = true;
                    }
                }
            }
        }

        private void btnSignatureClear_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < dgvSignerReviewer.Items.Count; i++)
            {
                ((SignerReviewerRow)dgvSignerReviewer.Items[i]).MSigner = false;
                ((SignerReviewerRow)dgvSignerReviewer.Items[i]).MReviewer = false;
            }
        }

        private void btnSignatureAll_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < dgvSignerReviewer.Items.Count; i++)
            {
                ((SignerReviewerRow)dgvSignerReviewer.Items[i]).MSigner = true;
            }
        }

        private void btnReviewerClear_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < dgvSignerReviewer.Items.Count; i++)
            {
                ((SignerReviewerRow)dgvSignerReviewer.Items[i]).MReviewer = false;
            }
        }

        private void btnReviewerAll_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < dgvSignerReviewer.Items.Count; i++)
            {
                ((SignerReviewerRow)dgvSignerReviewer.Items[i]).MSigner = true;
                ((SignerReviewerRow)dgvSignerReviewer.Items[i]).MReviewer = true;
            }
        }
    }
}
