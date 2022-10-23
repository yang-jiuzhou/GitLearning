using HBBio.Communication;
using HBBio.Result;
using HBBio.Evaluation;
using HBBio.MethodEdit;
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
using HBBio.Administration;

namespace HBBio.ProjectManager
{
    /// <summary>
    /// ProjectManagerWin.xaml 的交互逻辑
    /// </summary>
    public partial class ProjectManagerWin : Window, WindowPermission
    {
        private List<Signal> m_signalList = null;               //传入参数，给结果分析使用
        private bool m_self = true;
        private int m_projectID = -1;
        private int m_communicationSetsID = -1;
        private string m_nameOld = null;
        private Method m_copyMethod = null;
        private MethodQueue m_copyMethodQueue = null;
        private bool m_isNewM = false;
        private bool m_isNewMQ = false;


        /// <summary>
        /// 自定义事件，发送方法或者方法序列时触发
        /// </summary>
        public static readonly RoutedEvent MSelectEvent = EventManager.RegisterRoutedEvent("MSelectItem", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ProjectManagerWin));
        public event RoutedEventHandler MSelectItem
        {
            add { AddHandler(MSelectEvent, value); }
            remove { RemoveHandler(MSelectEvent, value); }
        }

        /// <summary>
        /// 自定义事件，发送谱图时触发
        /// </summary>
        public static readonly RoutedEvent MSelectCurveEvent = EventManager.RegisterRoutedEvent("MSelectCurve", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ProjectManagerWin));
        public event RoutedEventHandler MSelectCurve
        {
            add { AddHandler(MSelectCurveEvent, value); }
            remove { RemoveHandler(MSelectCurveEvent, value); }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="comconfStatic"></param>
        /// <param name="methodRun"></param>
        /// <param name="signalList"></param>
        public ProjectManagerWin(Window parent, int projectId, int communicationSetsId, List<Signal> signalList)
        {
            InitializeComponent();

            this.Owner = parent;

            projectTreeUC.MDefaultId = projectId;
            projectTreeUC.MDefaultUser = AdministrationStatic.Instance().MUserInfo.MUserName;
            m_communicationSetsID = communicationSetsId;
            m_signalList = signalList;

            projectTreeUC.MSelectItem += TreeItemSelected;  
        }

        /// <summary>
        /// 设置各模块是否可用
        /// </summary>
        /// <param name="info"></param>
        public bool SetPermission(PermissionInfo info)
        {
            if (info.MList[(int)EnumPermission.Project])
            {
                if (info.MList[(int)EnumPermission.Project_Method])
                {
                    tabMethod.Visibility = Visibility.Visible;
                    btnNewMethod.IsEnabled = m_self && info.MList[(int)EnumPermission.Project_Method_Watch_Edit];
                    btnNewMethodQueue.IsEnabled = m_self && info.MList[(int)EnumPermission.Project_Method_Watch_Edit];
                    btnEditMethod.IsEnabled = m_self && info.MList[(int)EnumPermission.Project_Method_Watch] || !m_self && info.MList[(int)EnumPermission.Project_Method_Watch_Other];
                    btnRenameMethod.IsEnabled = m_self && info.MList[(int)EnumPermission.Project_Method_Watch_Edit];
                    btnDelMethod.IsEnabled = m_self && info.MList[(int)EnumPermission.Project_Method_Watch_Edit];
                    btnCopyMethod.IsEnabled = m_self && info.MList[(int)EnumPermission.Project_Method_Watch_Edit];
                    //btnPasteMethod.IsEnabled = m_self && info.MList[(int)EnumPermission.Project_Method_Edit];

                    btnPrintMethod.IsEnabled = m_self && info.MList[(int)EnumPermission.Project_Method_Watch_Print];

                    btnExportMethod.IsEnabled = m_self && info.MList[(int)EnumPermission.Project_Method_Watch_ImportExport];
                    btnImportMethod.IsEnabled = m_self && info.MList[(int)EnumPermission.Project_Method_Watch_ImportExport];

                    btnSendMethod.IsEnabled = m_self && info.MList[(int)EnumPermission.Project_Method_Run];
                }
                else
                {
                    tabMethod.Visibility = Visibility.Collapsed;
                }

                if (info.MList[(int)EnumPermission.Project_Evaluation])
                {
                    tabEvaluation.Visibility = Visibility.Visible;

                    btnOpenResult.IsEnabled = m_self && info.MList[(int)EnumPermission.Project_Evaluation_Watch] || !m_self && info.MList[(int)EnumPermission.Project_Evaluation_Watch_Other];
                    btnContrastResult.IsEnabled = m_self && info.MList[(int)EnumPermission.Project_Evaluation_Watch] || !m_self && info.MList[(int)EnumPermission.Project_Evaluation_Watch_Other];
                    btnBG.IsEnabled = m_self && info.MList[(int)EnumPermission.Project_Evaluation_Watch] || !m_self && info.MList[(int)EnumPermission.Project_Evaluation_Watch_Other];
                    btnRenameResult.IsEnabled = m_self && info.MList[(int)EnumPermission.Project_Evaluation_Rename];
                }
                else
                {
                    tabEvaluation.Visibility = Visibility.Collapsed;
                }

                if (Visibility.Visible == tabMethod.Visibility && Visibility.Visible != tabEvaluation.Visibility)
                {
                    tabControl.SelectedItem = tabMethod;
                }
                else if (Visibility.Visible != tabMethod.Visibility && Visibility.Visible == tabEvaluation.Visibility)
                {
                    tabControl.SelectedItem = tabEvaluation;
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
        /// 加载初始界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

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
        /// 选择项目触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeItemSelected(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is TreeNode)
            {
                m_projectID = ((TreeNode)e.OriginalSource).MId;
                if (AdministrationStatic.Instance().MUserInfo.MID.Equals(((TreeNode)e.OriginalSource).MUserID))
                {
                    m_self = true;
                }
                else if(((TreeNode)e.OriginalSource).MType == EnumType.General)
                {
                    m_self = true;
                }
                else
                {
                    m_self = false;
                }
            }
            else
            {
                m_projectID = -1;
                m_self = false;
            }

            UpdateListMethod(txtFilterMethod.Text);
            UpdateListResult(txtFilterResult.Text);

            SetPermission(AdministrationStatic.Instance().MPermissionInfo);
        }

        /// <summary>
        /// 切换当前项目中的方法(是否定位到最后一个)
        /// </summary>
        private void UpdateListMethod(string filter, bool gotoLast = false)
        {
            MethodManager manager = new MethodManager();
            List<MethodType> list = null;
            manager.GetListName(m_communicationSetsID, m_projectID, filter, out list);
            listMethod.ItemsSource = list;

            if (gotoLast)
            {
                listMethod.SelectedIndex = list.Count - 1;
            }
        }

        /// <summary>
        /// 切换当前项目中的结果
        /// </summary>
        /// <param name="filter"></param>
        private void UpdateListResult(string filter)
        {
            ResultManager manager = new ResultManager();
            List<ResultTitle> list = null;
            manager.GetListName(m_communicationSetsID, m_projectID, filter, out list);

            //根据ID寻找用户名
            AdministrationManager managerAd = new AdministrationManager();
            string userName = null;
            foreach (var it in list)
            {  
                if (null != managerAd.GetUser(it.MUserID, out userName))
                {
                    userName = "General";
                }
                it.MUserName = userName;
            }

            listResult.ItemsSource = list;
        }

        /// <summary>
        /// 发送当前需要执行的方法或者方法序列(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendMethod(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(MSelectEvent, (MethodType)e.OriginalSource);
            RaiseEvent(args);
        }
        private void AddMethod(object sender, RoutedEventArgs e)
        {
            UpdateListMethod(txtFilterMethod.Text, true);
            projectTreeUC.UpdateCountMethod(true);
        }
        private void AddMethodQueue(object sender, RoutedEventArgs e)
        {
            UpdateListMethod(txtFilterMethod.Text, true);
            projectTreeUC.UpdateCountMethod(true);
        }

        /****方法编辑模块 ****/

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilterMethod_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateListMethod(txtFilterMethod.Text);
        }

        /// <summary>
        /// 添加方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewMethod_Click(object sender, RoutedEventArgs e)
        {
            if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.Project_Method_Watch_Edit))
            {
                return;
            }

            MethodManager manager = new MethodManager();
            List<MethodType> list = null;
            manager.GetListName(m_communicationSetsID, m_projectID, null, out list);
            NewMethodWin winNewMethod = new NewMethodWin(this, list, m_communicationSetsID, m_projectID);
            if (true == winNewMethod.ShowDialog())
            {
                MethodEditorWin win = new MethodEditorWin(this, winNewMethod.MMethod);
                win.MSelectItem += SendMethod;
                win.MAddMethod += AddMethod;
                win.Show();
            }
        }

        /// <summary>
        /// 添加方法序列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewMethodQueue_Click(object sender, RoutedEventArgs e)
        {
            if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.Project_Method_Watch_Edit))
            {
                return;
            }

            MethodManager manager = new MethodManager();
            List<MethodType> list = null;
            string error = manager.GetListNameMethod(m_communicationSetsID, m_projectID, out list);
            if (null == error)
            {
                MethodQueueWin win = new MethodQueueWin(this, m_communicationSetsID, m_projectID, list);
                win.MAddMethod += AddMethodQueue;
                win.Show();
            }
            else
            {
                Share.MessageBoxWin.Show(error);
            }
        }

        /// <summary>
        /// 重命名方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRenameMethod_Click(object sender, RoutedEventArgs e)
        {
            if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.Project_Method_Watch_Edit))
            {
                return;
            }

            if (-1 != listMethod.SelectedIndex)
            {
                ListBoxItem item = listMethod.ItemContainerGenerator.ContainerFromItem(listMethod.SelectedItem) as ListBoxItem;
                TextBox txt = MVisual.FindVisualChild<TextBox>(item as DependencyObject);

                if (null != txt)
                {
                    m_nameOld = txt.Text;

                    txt.Visibility = Visibility.Visible;
                    txt.Focus();
                    txt.SelectAll();
                }
            }   
        }

        /// <summary>
        /// 重命名方法生效
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRenameMethod_LostFocus(object sender, RoutedEventArgs e)
        {
            int index = listMethod.SelectedIndex;
            ListBoxItem item = listMethod.ItemContainerGenerator.ContainerFromItem(listMethod.SelectedItem) as ListBoxItem;
            TextBlock txb = MVisual.FindVisualChild<TextBlock>(item as DependencyObject);
            TextBox txt = MVisual.FindVisualChild<TextBox>(item as DependencyObject);

            if (null != txt)
            {
                if (TextLegal.FileNameLegal(txt.Text) && !txt.Text.Equals("NewMethod"))
                {
                    MethodManager manager = new MethodManager();
                    
                    string error = manager.UpdateMethodName((MethodType)listMethod.SelectedItem);
                    {
                        Method method = null;
                        manager.GetMethod(((MethodType)listMethod.SelectedItem).MID, out method);
                        method.MName = txt.Text;
                        method.MMethodSetting.MModifyTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        manager.UpdateMethod(method);
                    }
                    if (null != error)
                    {
                        Share.MessageBoxWin.Show(error);

                        txb.Text = m_nameOld;
                        txt.Text = m_nameOld;
                        ((MethodType)listMethod.SelectedItem).MName = m_nameOld;
                    }
                    else
                    {
                        if (m_isNewM)
                        {
                            AuditTrails.AuditTrailsStatic.Instance().InsertRowMethod(btnNewMethod.ToolTip.ToString(), projectTreeUC.MSelectPath + " : " + txt.Text);
                        }
                        else if (m_isNewMQ)
                        {
                            AuditTrails.AuditTrailsStatic.Instance().InsertRowMethod(btnNewMethodQueue.ToolTip.ToString(), projectTreeUC.MSelectPath + " : " + txt.Text);
                        }
                        else
                        {
                            switch(((MethodType)listMethod.SelectedItem).MType)
                            {
                                case EnumMethodType.Method:
                                    AuditTrails.AuditTrailsStatic.Instance().InsertRowMethod(Share.ReadXaml.GetResources("ME_Desc_Rename"), projectTreeUC.MSelectPath + " : " + m_nameOld + "->" + txt.Text);
                                    break;
                                case EnumMethodType.MethodQueue:
                                    AuditTrails.AuditTrailsStatic.Instance().InsertRowMethod(Share.ReadXaml.GetResources("ME_Desc_Queue_Rename"), projectTreeUC.MSelectPath + " : " + m_nameOld + "->" + txt.Text);
                                    break;
                            }  
                        }
                        UpdateListMethod(txtFilterMethod.Text);
                    }
                }
                else
                {
                    Share.MessageBoxWin.Show(Share.ReadXaml.S_ErrorIllegalName);

                    txb.Text = m_nameOld;
                    txt.Text = m_nameOld;
                }

                m_isNewM = false;
                m_isNewMQ = false;
                txt.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 编辑方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditMethod_Click(object sender, RoutedEventArgs e)
        {
            if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.Project_Method_Watch_Edit))
            {
                return;
            }

            if (-1 != listMethod.SelectedIndex)
            {
                MethodType curr = (MethodType)listMethod.SelectedItem;

                MethodManager manager = new MethodManager();
                switch (curr.MType)
                {
                    case EnumMethodType.Method:
                        {
                            Method item = null;
                            string error = manager.GetMethod(curr.MID, out item);
                            if (null == error)
                            {
                                MethodEditorWin win = new MethodEditorWin(this, item);
                                if (m_self)
                                {
                                    win.SaveSend(btnNewMethod.IsEnabled, btnSendMethod.IsEnabled);
                                }
                                else
                                {
                                    win.SaveSend(false, false);
                                }
                                win.MSelectItem += SendMethod;
                                win.Show();
                            }
                            else
                            {
                                Share.MessageBoxWin.Show(error);
                            }
                        }
                        break;
                    case EnumMethodType.MethodQueue:
                        {
                            List<MethodType> list = null;
                            string error = manager.GetListNameMethod(m_communicationSetsID, m_projectID, out list);
                            if (null == error)
                            {
                                MethodQueueWin win = new MethodQueueWin(curr.MID, list, m_self);
                                win.Show();
                            }
                            else
                            {
                                Share.MessageBoxWin.Show(error);
                            }
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelMethod_Click(object sender, RoutedEventArgs e)
        {
            if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.Project_Method_Watch_Edit))
            {
                return;
            }

            if (-1 != listMethod.SelectedIndex)
            {
                if (MessageBoxResult.No == Share.MessageBoxWin.Show(Share.ReadXaml.GetResources("labDel"), Share.ReadXaml.GetResources("btnDel"), MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    return;
                }

                MethodManager manager = new MethodManager();
                List<MethodType> listSelect = new List<MethodType>();
                foreach (MethodType it in listMethod.SelectedItems)
                {
                    listSelect.Add(it);
                }
                bool update = false;
                StringBuilderSplit infoShow = new StringBuilderSplit();
                foreach (MethodType it in listSelect)
                {
                    string error = manager.DelMethod(it.MID);
                    if (null == error)
                    {
                        switch (it.MType)
                        {
                            case EnumMethodType.Method:
                                AuditTrails.AuditTrailsStatic.Instance().InsertRowMethod(Share.ReadXaml.GetResources("ME_Desc_Delete"), projectTreeUC.MSelectPath + " : " + it.MName);
                                infoShow.Append(it.MName + " " + Share.ReadXaml.GetResources("ME_Msg_DeleteYes"));
                                break;
                            case EnumMethodType.MethodQueue:
                                AuditTrails.AuditTrailsStatic.Instance().InsertRowMethod(Share.ReadXaml.GetResources("ME_Desc_Queue_Delete"), projectTreeUC.MSelectPath + " : " + it.MName);
                                infoShow.Append(it.MName + " " + Share.ReadXaml.GetResources("ME_Msg_Queue_DeleteYes"));
                                break;
                        }
                        update = true;
                    }
                    else
                    {
                        infoShow.Append(error);
                    }
                }

                Share.MessageBoxWin.Show(infoShow.ToString());

                if (update)
                {
                    UpdateListMethod(txtFilterMethod.Text);

                    projectTreeUC.UpdateCountMethod(false);
                }

                //MethodManager manager = new MethodManager();
                //string error = manager.DelMethod(((MethodType)listMethod.SelectedItem).MID);
                //if (null == error)
                //{
                //    switch(((MethodType)listMethod.SelectedItem).MType)
                //    {
                //        case EnumMethodType.Method:
                //            AuditTrails.AuditTrailsStatic.Instance().InsertRowMethod(Share.ReadXaml.GetResources("ME_Desc_Delete"), projectTreeUC.MSelectPath + " : " + ((MethodType)listMethod.SelectedItem).MName);
                //            Share.MessageBoxWin.Show(Share.ReadXaml.GetResources("ME_Msg_DeleteYes"));
                //            break;
                //        case EnumMethodType.MethodQueue:
                //            AuditTrails.AuditTrailsStatic.Instance().InsertRowMethod(Share.ReadXaml.GetResources("ME_Desc_Queue_Delete"), projectTreeUC.MSelectPath + " : " + ((MethodType)listMethod.SelectedItem).MName);
                //            Share.MessageBoxWin.Show(Share.ReadXaml.GetResources("ME_Msg_Queue_DeleteYes"));
                //            break;
                //    }

                //    UpdateListMethod(txtFilterMethod.Text);

                //    projectTreeUC.UpdateCountMethod(false);
                //}
                //else
                //{
                //    Share.MessageBoxWin.Show(error);
                //}
            }
        }

        /// <summary>
        /// 复制方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopyMethod_Click(object sender, RoutedEventArgs e)
        {
            if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.Project_Method_Watch_Edit))
            {
                return;
            }

            if (-1 != listMethod.SelectedIndex)
            {
                MethodType curr = (MethodType)listMethod.SelectedItem;

                switch (curr.MType)
                {
                    case EnumMethodType.Method:
                        {
                            m_copyMethodQueue = null;

                            MethodManager manager = new MethodManager();
                            string error = manager.GetMethod(curr.MID, out m_copyMethod);
                            if (null == error)
                            {
                                btnPasteMethod.IsEnabled = true;
                                AuditTrails.AuditTrailsStatic.Instance().InsertRowMethod(btnCopyMethod.ToolTip.ToString(), curr.MName);
                                Share.MessageBoxWin.Show(Share.ReadXaml.GetResources("ME_Msg_CopyYes"));
                            }
                            else
                            {
                                btnPasteMethod.IsEnabled = false;
                                Share.MessageBoxWin.Show(error);
                            }
                        }
                        break;
                    case EnumMethodType.MethodQueue:
                        {
                            m_copyMethod = null;

                            MethodManager manager = new MethodManager();
                            string error = manager.GetMethodQueue(curr.MID, out m_copyMethodQueue);
                            if (null == error)
                            {
                                btnPasteMethod.IsEnabled = true;
                                AuditTrails.AuditTrailsStatic.Instance().InsertRowMethod(btnCopyMethod.ToolTip.ToString(), curr.MName);
                                Share.MessageBoxWin.Show(Share.ReadXaml.GetResources("ME_Msg_Queue_CopyYes"));
                            }
                            else
                            {
                                btnPasteMethod.IsEnabled = false;
                                Share.MessageBoxWin.Show(error);
                            }
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 粘贴方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPasteMethod_Click(object sender, RoutedEventArgs e)
        {
            if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.Project_Method_Watch_Edit))
            {
                return;
            }

            if (null != m_copyMethod)
            {
                m_copyMethod.MID = -1;
                m_copyMethod.MName = "NewMethod";
                m_copyMethod.MProjectID = m_projectID;
                MethodManager manager = new MethodManager();
                string error = manager.AddMethod(m_copyMethod);
                if (null == error)
                {
                    UpdateListMethod(txtFilterMethod.Text, true);

                    projectTreeUC.UpdateCountMethod(true);

                    Share.MApp.DoEvents();

                    m_isNewM = true;
                    btnRenameMethod_Click(null, null);
                }
                else
                {
                    Share.MessageBoxWin.Show(error);
                }
            }
            else if (null != m_copyMethodQueue)
            {
                m_copyMethodQueue.MID = -1;
                m_copyMethodQueue.MName = "NewMethodQueue";
                m_copyMethodQueue.MProjectID = m_projectID;
                MethodManager manager = new MethodManager();
                string error = manager.AddMethodQueue(m_copyMethodQueue);
                if (null == error)
                {
                    UpdateListMethod(txtFilterMethod.Text, true);

                    projectTreeUC.UpdateCountMethod(true);

                    Share.MApp.DoEvents();

                    m_isNewMQ = true;
                    btnRenameMethod_Click(null, null);
                }
                else
                {
                    Share.MessageBoxWin.Show(error);
                }
            }
        }

        /// <summary>
        /// 发送方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendMethod_Click(object sender, RoutedEventArgs e)
        {
            if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.Project_Method_Run))
            {
                return;
            }

            if (-1 != listMethod.SelectedIndex)
            {
                RoutedEventArgs args = new RoutedEventArgs(MSelectEvent, (MethodType)listMethod.SelectedItem);
                RaiseEvent(args);

                switch (((MethodType)listMethod.SelectedItem).MType)
                {
                    case EnumMethodType.Method:
                        AuditTrails.AuditTrailsStatic.Instance().InsertRowMethod(Share.ReadXaml.GetResources("ME_Desc_Send"), projectTreeUC.MSelectPath + " : " + ((MethodType)listMethod.SelectedItem).MName);
                        break;
                    case EnumMethodType.MethodQueue:
                        AuditTrails.AuditTrailsStatic.Instance().InsertRowMethod(Share.ReadXaml.GetResources("ME_Desc_Queue_Send"), projectTreeUC.MSelectPath + " : " + ((MethodType)listMethod.SelectedItem).MName);
                        break;
                }
            }
        }

        /// <summary>
        /// 打印方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintMethod_Click(object sender, RoutedEventArgs e)
        {
            if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.Project_Method_Watch_Print))
            {
                return;
            }

            if (-1 != listMethod.SelectedIndex)
            {
                MethodType curr = (MethodType)listMethod.SelectedItem;

                MethodManager manager = new MethodManager();
                switch (curr.MType)
                {
                    case EnumMethodType.Method:
                        {
                            AuditTrails.AuditTrailsStatic.Instance().InsertRowOperate(this.Title + "-" + btnPrintMethod.ToolTip,
                                        projectTreeUC.MSelectPath + " : " + ((MethodType)listMethod.SelectedItem).MName);

                            Method item = null;
                            string error = manager.GetMethod(curr.MID, out item);
                            if (null == error)
                            {
                                MethodEdit.OutputWin win = new MethodEdit.OutputWin(this);
                                win.SetData(item);
                                if (true == win.ShowDialog())
                                {
                                    
                                }
                            }
                            else
                            {
                                Share.MessageBoxWin.Show(error);
                            }
                        }
                        break;
                    case EnumMethodType.MethodQueue:
                        {
                            AuditTrails.AuditTrailsStatic.Instance().InsertRowOperate(this.Title + "-" + btnPrintMethod.ToolTip,
                                        projectTreeUC.MSelectPath + " : " + ((MethodType)listMethod.SelectedItem).MName);

                            MethodQueue item = null;
                            string error = manager.GetMethodQueue(curr.MID, out item);
                            if (null == error)
                            {
                                MethodEdit.OutputWin win = new MethodEdit.OutputWin(this);
                                win.SetData(item);
                                if (true == win.ShowDialog())
                                {
                                    
                                }
                            }
                            else
                            {
                                Share.MessageBoxWin.Show(error);
                            }
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportMethod_Click(object sender, RoutedEventArgs e)
        {
            if (null == listMethod.SelectedItems || 0 == listMethod.SelectedItems.Count)
            {
                return;
            }

            if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.Project_Method_Watch_ImportExport))
            {
                return;
            }

            Microsoft.Win32.SaveFileDialog ofd = new Microsoft.Win32.SaveFileDialog();
            ofd.DefaultExt = ".db";
            ofd.Filter = "db file|*.db";
            if (ofd.ShowDialog() == true)
            {
                MethodManager manager = new MethodManager();
                List<Method> list = new List<Method>();
                foreach (MethodType it in listMethod.SelectedItems)
                {
                    if (EnumMethodType.MethodQueue == it.MType)
                    {
                        continue;
                    }
                    Method item = null;
                    if (null == manager.GetMethod(it.MID, out item))
                    {
                        list.Add(item);
                    }
                }

                MethodExImManager managerExIm = new MethodExImManager();
                string error = managerExIm.Export(ofd.FileName, list);
                if (null == error)
                {
                    StringBuilderSplit sb = new StringBuilderSplit("\n");
                    foreach (var it in list)
                    {
                        sb.Append(it.MName);
                    }
                    AuditTrails.AuditTrailsStatic.Instance().InsertRowMethod(btnExportMethod.ToolTip.ToString(), sb.ToString());

                    Share.MessageBoxWin.Show(btnExportMethod.ToolTip + Share.ReadXaml.S_Success);
                }
                else
                {
                    Share.MessageBoxWin.Show(error, btnExportMethod.ToolTip + Share.ReadXaml.S_Failure);
                }
            }
        }

        /// <summary>
        /// 导入方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportMethod_Click(object sender, RoutedEventArgs e)
        {
            if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.Project_Method_Watch_ImportExport))
            {
                return;
            }

            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.DefaultExt = ".db";
            ofd.Filter = "db file|*.db";
            if (ofd.ShowDialog() == true)
            {
                MethodExImManager manager = new MethodExImManager();
                List<Method> list = new List<Method>();
                if (null == manager.Import(ofd.FileName, list))
                {
                    if (0 == list.Count)
                    {
                        Share.MessageBoxWin.Show(Share.ReadXaml.S_ErrorNoData, btnImportMethod.ToolTip + Share.ReadXaml.S_Failure);
                        return;
                    }

                    foreach (var it in list)
                    {
                        bool flag = true;
                        while (flag)
                        {
                            flag = false;
                            foreach (MethodType itBC in listMethod.ItemsSource)
                            {
                                if (it.MName.Equals(itBC.MName))
                                {
                                    it.MName += "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                                    flag = true;
                                    break;
                                }
                            }
                        }
                    }

                    for (int i = 0; i < list.Count; i++)
                    {
                        list[i].MCommunicationSetsID = m_communicationSetsID;
                        list[i].MProjectID = m_projectID;
                    }
                    MethodManager columnManager = new MethodManager();
                    string error = null;
                    foreach (var it in list)
                    {
                        error += columnManager.AddMethod(it);
                    }
                    if (string.IsNullOrEmpty(error))
                    {
                        error = null;
                    }
                    if (null == error)
                    {
                        UpdateListMethod(txtFilterMethod.Text, true);

                        StringBuilderSplit sb = new StringBuilderSplit("\n");
                        foreach (var it in list)
                        {
                            sb.Append(it.MName);
                        }
                        AuditTrails.AuditTrailsStatic.Instance().InsertRowMethod(btnImportMethod.ToolTip.ToString(), sb.ToString());

                        Share.MessageBoxWin.Show(btnImportMethod.ToolTip + Share.ReadXaml.S_Success);
                    }
                    else
                    {
                        Share.MessageBoxWin.Show(error, btnImportMethod.ToolTip + Share.ReadXaml.S_Failure);
                    }
                }
            }
        }

        /// <summary>
        /// 双击编辑方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listMethod_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (btnEditMethod.IsEnabled)
            {
                btnEditMethod_Click(null, null);
            }
        }

        /****结果分析模块 ****/

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilterResult_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateListResult(txtFilterResult.Text);
        }

        /// <summary>
        /// 打开结果分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenResult_Click(object sender, RoutedEventArgs e)
        {
            if (-1 != listResult.SelectedIndex)
            {
                EvaluationWin win = new EvaluationWin();
                win.SetPermission(AdministrationStatic.Instance().MPermissionInfo);
                win.Show();

                win.Open(m_signalList, (ResultTitle)listResult.SelectedItem);
            }
        }

        /// <summary>
        /// 对比结果分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnContrastResult_Click(object sender, RoutedEventArgs e)
        {
            List<ResultTitle> list = new List<ResultTitle>();
            foreach (ResultTitle it in listResult.Items)
            {
                if (it.MCheck)
                {
                    list.Add(it);
                }
            }

            if (1 < list.Count)
            {
                ContrastWin win = new ContrastWin();
                win.Show();

                win.Contrast(m_signalList, list);
            }
        }

        /// <summary>
        /// 发送背景谱图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBG_Click(object sender, RoutedEventArgs e)
        {
            if (-1 != listResult.SelectedIndex)
            {
                RoutedEventArgs args = new RoutedEventArgs(MSelectCurveEvent, (ResultTitle)listResult.SelectedItem);
                RaiseEvent(args);

                AuditTrails.AuditTrailsStatic.Instance().InsertRowOperate(btnBG.ToolTip.ToString(), projectTreeUC.MSelectPath + " : " + ((ResultTitle)listResult.SelectedItem).MName);
            }
        }

        /// <summary>
        /// 重命名谱图名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRenameResult_Click(object sender, RoutedEventArgs e)
        {
            if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.Project_Evaluation_Rename))
            {
                return;
            }

            if (-1 != listResult.SelectedIndex)
            {
                ListBoxItem item = listResult.ItemContainerGenerator.ContainerFromItem(listResult.SelectedItem) as ListBoxItem;
                TextBox txt = MVisual.FindVisualChild<TextBox>(item as DependencyObject);

                if (null != txt)
                {
                    m_nameOld = txt.Text;

                    txt.Visibility = Visibility.Visible;
                    txt.Focus();
                    txt.SelectAll();
                }
            }
        }

        /// <summary>
        /// 重命名谱图名称生效
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRenameResult_LostFocus(object sender, RoutedEventArgs e)
        {
            int index = listResult.SelectedIndex;
            ListBoxItem item = listResult.ItemContainerGenerator.ContainerFromItem(listResult.SelectedItem) as ListBoxItem;
            TextBlock txb = MVisual.FindVisualChild<TextBlock>(item as DependencyObject);
            TextBox txt = MVisual.FindVisualChild<TextBox>(item as DependencyObject);

            if (null != txt)
            {
                if (TextLegal.FileNameLegal(txt.Text))
                {
                    ResultManager manager = new ResultManager();
                    string error = manager.UpdateName((ResultTitle)listResult.SelectedItem);
                    if (null != error)
                    {
                        Share.MessageBoxWin.Show(error);

                        txb.Text = m_nameOld;
                        txt.Text = m_nameOld;
                    }
                    else
                    {
                        AuditTrails.AuditTrailsStatic.Instance().InsertRowOperate(tabEvaluation.Header.ToString() + btnRenameResult.ToolTip.ToString(), projectTreeUC.MSelectPath + " : " + m_nameOld + "->" + txt.Text);
                    }
                }
                else
                {
                    Share.MessageBoxWin.Show(Share.ReadXaml.S_ErrorIllegalName);

                    txb.Text = m_nameOld;
                    txt.Text = m_nameOld;
                }

                txt.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 双击打开结果分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listResult_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (btnOpenResult.IsEnabled)
            {
                btnOpenResult_Click(null, null);
            }
        }
    }
}
