using HBBio.Administration;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

namespace HBBio.Communication
{
    /// <summary>
    /// CommunSetsWin.xaml 的交互逻辑
    /// </summary>
    public partial class CommunSetsWin : Window, WindowPermission
    {
        private ObservableCollection<CommunicationSets> m_comconfList = new ObservableCollection<CommunicationSets>();


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        public CommunSetsWin(Window parent)
        {
            InitializeComponent();

            this.Owner = parent;

            InitDGV();
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
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.Communication_Add))
            {
                return;
            }

            NewCommunWin win = new NewCommunWin(this);
            if (true == win.ShowDialog())
            {
                InitDGV();
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (null == dgv.SelectedItem)
            {
                return;
            }

            if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.Communication_Edit))
            {
                return;
            }

            EditCommunWin win = new EditCommunWin(this);
            win.InitWin(m_comconfList[dgv.SelectedIndex]);
            win.ShowDialog();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (null == dgv.SelectedItem)
            {
                return;
            }

            if (m_comconfList[dgv.SelectedIndex].MIsEnabled)
            {
                return;
            }

            if (MessageBoxResult.No == Share.MessageBoxWin.Show(Share.ReadXaml.S_Continue, btnDel.Content.ToString(), MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                return;
            }

            if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.Communication_Del))
            {
                return;
            }

            CommunicationSetsManager csManager = new CommunicationSetsManager();
            if (csManager.DeleteItem(m_comconfList[dgv.SelectedIndex]))
            {
                AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(btnDel.Content.ToString(), m_comconfList[dgv.SelectedIndex].MName);

                m_comconfList.RemoveAt(dgv.SelectedIndex);
            }
            else
            {
                MessageBoxWin.Show(ReadXamlCommunication.S_DelComError);
            }
        }

        /// <summary>
        /// 激活
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnabled_Click(object sender, RoutedEventArgs e)
        {
            if (null == dgv.SelectedItem)
            {
                return;
            }

            if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.Communication_Activate))
            {
                return;
            }

            CommunicationSetsManager csManager = new CommunicationSetsManager();
            for (int i = 0; i < m_comconfList.Count; i++)
            {
                if (m_comconfList[i].MIsEnabled)
                {
                    m_comconfList[i].MIsEnabled = false;
                    csManager.EnabledItem(m_comconfList[i]);
                    break;
                }
            }
            m_comconfList[dgv.SelectedIndex].MIsEnabled = true;
            csManager.EnabledItem(m_comconfList[dgv.SelectedIndex]);

            AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(btnEnabled.Content.ToString(), m_comconfList[dgv.SelectedIndex].MName);
        }

        /// <summary>
        /// 修改行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        /// <summary>
        /// 编辑（双击快捷）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (btnEdit.IsEnabled)
            {
                btnEdit_Click(null, null);
            }
        }


        /// <summary>
        /// 设置各模块是否可用
        /// </summary>
        /// <param name="info"></param>
        public bool SetPermission(PermissionInfo info)
        {
            if (info.MList[(int)EnumPermission.Communication])
            {
                btnAdd.IsEnabled = info.MList[(int)EnumPermission.Communication_Add];
                btnEdit.IsEnabled = info.MList[(int)EnumPermission.Communication_Edit];
                btnDel.IsEnabled = info.MList[(int)EnumPermission.Communication_Del];
                btnEnabled.IsEnabled = info.MList[(int)EnumPermission.Communication_Activate];

                return true;
            }
            else
            {
                this.Close();

                return false;
            }
        }

        /// <summary>
        /// 加载表格数据
        /// </summary>
        private void InitDGV()
        {
            CommunicationSetsManager csManager = new CommunicationSetsManager();

            List<CommunicationSets> csList = null;
            if (null == csManager.GetList(out csList))
            {
                m_comconfList = new ObservableCollection<CommunicationSets>(csList);
            }

            dgv.ItemsSource = m_comconfList;
        }
    }
}
