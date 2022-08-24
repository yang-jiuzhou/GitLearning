using HBBio.Administration;
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

namespace HBBio.ColumnList
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class ColumnHandlingWin : Window, WindowPermission
    {
        private ColumnItem m_item = new ColumnItem();
        private readonly string m_userName = null;


        /// <summary>
        /// 构造函数
        /// </summary>
        public ColumnHandlingWin(Window parent, string userName)
        {
            InitializeComponent();

            this.Owner = parent;

            m_userName = userName;

            m_item.MUser = userName;
            LoadItem(m_item);

            UpdateItemList(true);
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
        /// 设置各模块是否可用
        /// </summary>
        /// <param name="info"></param>
        public bool SetPermission(PermissionInfo info)
        {
            if (info.MList[(int)EnumPermission.ColumnHandling])
            {
                btnDel.IsEnabled = info.MList[(int)EnumPermission.ColumnHandling_Edit];
                btnSave.IsEnabled = info.MList[(int)EnumPermission.ColumnHandling_Edit];

                btnExport.IsEnabled = info.MList[(int)EnumPermission.ColumnHandling_ImportExport];
                btnImport.IsEnabled = info.MList[(int)EnumPermission.ColumnHandling_ImportExport];

                btnPrint.IsEnabled = info.MList[(int)EnumPermission.ColumnHandling_Print];

                return true;
            }
            else
            {
                this.Close();

                return false;
            }
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void LoadItem(ColumnItem item)
        {
            txtName.DataContext = item;
            txtNote.DataContext = item;
            txtUser.DataContext = item;
            dgvRunParameters.ItemsSource = item.MRP.MList;
            dgvDetails.ItemsSource = item.MDT.MList;
        }

        /// <summary>
        /// 数据绑定-文本框需要每次绑定
        /// </summary>
        /// <param name="item"></param>
        private void LoadItemEvery(ColumnItem item)
        {
            txtName.DataContext = null;
            txtName.DataContext = item;

            txtNote.DataContext = null;
            txtNote.DataContext = item;

            txtUser.DataContext = null;
            txtUser.DataContext = item;
        }

        /// <summary>
        /// 更新柱子列表，默认选中第一个
        /// </summary>
        /// <param name="showFirst"></param>
        private void UpdateItemList(bool showFirst, string filter = null)
        {
            ColumnManager manager = new ColumnManager();
            List<string> nameList = null;
            if (null == manager.GetNameList(out nameList, filter))
            {
                listColumnList.ItemsSource = nameList;

                if (0 == nameList.Count)
                {
                    listColumnList.SelectedIndex = -1;
                }
                else
                {
                    if (showFirst)
                    {
                        listColumnList.SelectedIndex = 0;
                    }
                    else
                    {
                        listColumnList.SelectedIndex = nameList.Count - 1;
                    }
                }
            }
        }

        /// <summary>
        /// 柱信息列表-过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateItemList(true, txtFilter.Text);
        }

        /// <summary>
        /// 柱信息列表-切换选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listColumnList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (-1 == listColumnList.SelectedIndex)
            {
                m_item.Reset(m_userName);
                LoadItemEvery(m_item);
                return;
            }

            ColumnManager manager = new ColumnManager();
            if (null == manager.GetColumn(listColumnList.SelectedItem.ToString(), m_item))
            {
                LoadItemEvery(m_item);
                txtName.IsReadOnly = true;
            }
        }

        /// <summary>
        /// 柱信息列表-打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (-1 == listColumnList.SelectedIndex)
            {
                return;
            }

            if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.ColumnHandling_Print))
            {
                return;
            }

            AuditTrails.AuditTrailsStatic.Instance().InsertRowOperate(this.Title + "-" + btnPrint.ToolTip, labName.Text + txtName.Text);

            Print.PaginatorHeaderFooter.s_signer = Administration.AdministrationStatic.Instance().MSigner;
            Print.PaginatorHeaderFooter.s_reviewer = Administration.AdministrationStatic.Instance().MReviewer;

            OutputWin win = new OutputWin(this);
            win.SetData(m_item);
            if (true == win.ShowDialog())
            {
                
            }
        }

        /// <summary>
        /// 柱信息列表-导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            if (null == listColumnList.SelectedItems || 0 == listColumnList.SelectedItems.Count)
            {
                return;
            }

            if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.ColumnHandling_ImportExport))
            {
                return;
            }

            Microsoft.Win32.SaveFileDialog ofd = new Microsoft.Win32.SaveFileDialog();
            ofd.DefaultExt = ".db";
            ofd.Filter = "db file|*.db";
            if (ofd.ShowDialog() == true)
            {
                ColumnManager manager = new ColumnManager();
                List<ColumnItem> list = new List<ColumnItem>();
                foreach (var it in listColumnList.SelectedItems)
                {
                    ColumnItem item = new ColumnItem();
                    if (null == manager.GetColumn(it.ToString(), item))
                    {
                        list.Add(item);
                    }
                }

                ColumnExImManager managerExIm = new ColumnExImManager();
                string error = managerExIm.Export(ofd.FileName, list);
                if (null == error)
                {
                    StringBuilderSplit sb = new StringBuilderSplit("\n");
                    sb.Append(Share.ReadXaml.GetResources("labFilePath1") + ofd.FileName);
                    foreach (var it in list)
                    {
                        sb.Append(labName.Text + it.MName);
                    }
                    AuditTrails.AuditTrailsStatic.Instance().InsertRowColumnList(btnExport.ToolTip.ToString(), sb.ToString());

                    MessageBoxWin.Show(btnExport.ToolTip + Share.ReadXaml.S_Success);
                }
                else
                {
                    MessageBoxWin.Show(error, btnExport.ToolTip + Share.ReadXaml.S_Failure);
                }
            }
        }

        /// <summary>
        /// 柱信息列表-导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.ColumnHandling_ImportExport))
            {
                return;
            }

            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.DefaultExt = ".db";
            ofd.Filter = "db file|*.db";
            if (ofd.ShowDialog() == true)
            {
                ColumnExImManager manager = new ColumnExImManager();
                List<ColumnItem> list = new List<ColumnItem>();
                if (null == manager.Import(ofd.FileName, list))
                {
                    List<string> nameList = (List<string>)listColumnList.ItemsSource;
                    foreach (var it in list)
                    {
                        while (nameList.Contains(it.MName))
                        {
                            it.MName += "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                        }
                    }
                    for (int i = 0; i < list.Count; i++)
                    {
                        list[i].MNote += "\n导入";
                        list[i].MUser = m_userName;
                    }
                    ColumnManager columnManager = new ColumnManager();
                    string error = null;
                    foreach (var it in list)
                    {
                        error += columnManager.InsertColumn(it);
                    }
                    if (string.IsNullOrEmpty(error))
                    {
                        error = null;
                    }
                    if (null == error)
                    {
                        UpdateItemList(false);
                        txtName.IsReadOnly = true;

                        StringBuilderSplit sb = new StringBuilderSplit("\n");
                        sb.Append(Share.ReadXaml.GetResources("labFilePath1") + ofd.FileName);
                        foreach (var it in list)
                        {
                            sb.Append(labName.Text + it.MName);
                        }
                        AuditTrails.AuditTrailsStatic.Instance().InsertRowColumnList(btnImport.ToolTip.ToString(), sb.ToString());

                        MessageBoxWin.Show(btnImport.ToolTip + Share.ReadXaml.S_Success);
                    }
                    else
                    {
                        MessageBoxWin.Show(error, btnImport.ToolTip + Share.ReadXaml.S_Failure);
                    }
                }
            }
        }

        /// <summary>
        /// 柱信息列表-复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            if (-1 == listColumnList.SelectedIndex)
            {
                return;
            }

            listColumnList.SelectedIndex = -1;

            txtName.Focus();
            txtName.SelectAll();
            txtName.IsReadOnly = false;
        }

        /// <summary>
        /// 柱信息列表-添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            listColumnList.SelectedIndex = -1;

            txtName.Focus();
            txtName.SelectAll();
            txtName.IsReadOnly = false;
        }

        /// <summary>
        /// 柱信息列表-删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (null == listColumnList.SelectedItems || 0 == listColumnList.SelectedItems.Count)
            {
                return;
            }

            if (MessageBoxResult.No == Share.MessageBoxWin.Show(Share.ReadXaml.GetResources("labDel"), Share.ReadXaml.GetResources("btnDel"), MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                return;
            }

            if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.ColumnHandling_Edit))
            {
                return;
            }

            string error = null;
            StringBuilderSplit sb = new StringBuilderSplit("\n");
            ColumnManager manager = new ColumnManager();
            foreach (var it in listColumnList.SelectedItems)
            {
                error+= manager.DelColumn(it.ToString());
                sb.Append(it.ToString());
            }
            if (string.IsNullOrEmpty(error))
            {
                error = null;
            }
            if (null == error)
            {
                UpdateItemList(true);

                AuditTrails.AuditTrailsStatic.Instance().InsertRowColumnList(btnDel.ToolTip.ToString(), sb.ToString());

                MessageBoxWin.Show(btnDel.ToolTip + Share.ReadXaml.S_Success);
            }
            else
            {
                MessageBoxWin.Show(error, btnDel.ToolTip + Share.ReadXaml.S_Failure);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.ColumnHandling_Edit))
            {
                return;
            }

            foreach (var it in m_item.MRP.MList)
            {
                if (it.MName.Contains("*") && string.IsNullOrEmpty(it.MStr))
                {
                    Share.MessageBoxWin.Show(it.MName + " " + Share.ReadXaml.S_ErrorNoData);
                    return;
                }
            }

            ColumnManager manager = new ColumnManager();

            if (txtName.IsReadOnly)
            {
                //修改,需比较是否修改
                ColumnItem oldItem = new ColumnItem();
                string error = manager.GetColumn(listColumnList.SelectedItem.ToString(), oldItem);
                if (null != error)
                {
                    Share.MessageBoxWin.Show(error);
                }
                else
                {
                    if (!oldItem.Compared(m_item))
                    {
                        error = manager.UpdateColumn(m_item);
                        if (null != error)
                        {
                            Share.MessageBoxWin.Show(error);
                        }
                        else
                        {
                            StringBuilderSplit sb = new StringBuilderSplit("\n");
                            sb.Append(labName.Text + m_item.MName);
                            if (!m_item.MNote.Equals(oldItem.MNote))
                            {
                                sb.Append(labNote.Text + oldItem.MNote + " -> " + m_item.MNote);
                            }
                            for (int i = 0; i < m_item.MRP.MList.Count; i++)
                            {
                                if (m_item.MRP.MList[i].MText != oldItem.MRP.MList[i].MText
                                    || m_item.MRP.MList[i].MUnit != oldItem.MRP.MList[i].MUnit)
                                {
                                    sb.Append(m_item.MRP.MList[i].MName + " : " + oldItem.MRP.MList[i].MText + " " + oldItem.MRP.MList[i].MUnit + " -> " + m_item.MRP.MList[i].MText + " " + m_item.MRP.MList[i].MUnit);
                                }
                            }
                            for (int i = 0; i < m_item.MDT.MList.Count; i++)
                            {
                                if (m_item.MDT.MList[i].MText != oldItem.MDT.MList[i].MText
                                    || m_item.MDT.MList[i].MUnit != oldItem.MDT.MList[i].MUnit)
                                {
                                    sb.Append(oldItem.MDT.MList[i].MName + " : " + oldItem.MDT.MList[i].MText + " " + oldItem.MDT.MList[i].MUnit + " -> " + m_item.MDT.MList[i].MText + " " + m_item.MDT.MList[i].MUnit);
                                }
                            }

                            AuditTrails.AuditTrailsStatic.Instance().InsertRowColumnList(btnSave.ToolTip.ToString(), sb.ToString());
                            Share.MessageBoxWin.Show(btnSave.ToolTip + Share.ReadXaml.S_Success);
                        }
                    }
                }
            }
            else
            {
                //添加
                string error = manager.InsertColumn(m_item);
                if (null != error)
                {
                    MessageBoxWin.Show(error);
                }
                else
                {
                    UpdateItemList(false);
                    txtName.IsReadOnly = true;

                    StringBuilderSplit sb = new StringBuilderSplit("\n");
                    sb.Append(labName.Text + m_item.MName);
                    sb.Append(labNote.Text + m_item.MNote);
                    sb.Append(labUser.Text + m_item.MUser);
                    foreach(var it in m_item.MRP.MList)
                    {
                        sb.Append(it.MName + " : " + it.MText + " " + it.MUnit);
                    }
                    foreach (var it in m_item.MDT.MList)
                    {
                        sb.Append(it.MName + " : " + it.MText + " " + it.MUnit);
                    }

                    AuditTrails.AuditTrailsStatic.Instance().InsertRowColumnList(Share.ReadXaml.GetResources("btnAdd"), sb.ToString());
                    Share.MessageBoxWin.Show(btnSave.ToolTip + Share.ReadXaml.S_Success);
                }
            }
        }
    }
}