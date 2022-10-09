using HBBio.AuditTrails;
using HBBio.Communication;
using HBBio.Chromatogram;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HBBio.Result;
using HBBio.MethodEdit;
using HBBio.SystemControl;
using HBBio.Administration;

namespace HBBio.Evaluation
{
    /// <summary>
    /// EvaluationWin.xaml 的交互逻辑
    /// </summary>
    public partial class EvaluationWin : Window, WindowPermission
    {
        private IntegrationSet m_integrationSet = null;
        private OutputSelectSet m_outputSelectSet = null;
        private ResultTitle m_resultTitle = null;
        private List<string> m_listConstName = new List<string>();
        private List<string> m_listUnit = new List<string>();
        public List<PeakValue> MListPeak = new List<PeakValue>();
        public List<bool> MListVisible = new List<bool>();
        private int m_selectIndex = 0;
        private Thread m_threadOutputExcel = null;


        /// <summary>
        /// 构造函数
        /// </summary>
        public EvaluationWin()
        {
            InitializeComponent();

            EvaluationManager manager = new EvaluationManager();
            if (null == manager.GetIntegrationSet(out m_integrationSet))
            {
                UpdateDGVIntegrationVisibility();
            }
            manager.GetOutputSelectSet(out m_outputSelectSet);

            chromatogramUC.IsReal = false;
            chromatogramUC.MUpdateCurve += DlyUpdateCurve;
            chromatogramUC.MUpdateSelect += DlyUpdateSelect;
        }

        /// <summary>
        /// 设置各模块是否可用
        /// </summary>
        /// <param name="info"></param>
        public bool SetPermission(PermissionInfo info)
        {
            if (info.MList[(int)EnumPermission.Project_Evaluation_Watch])
            {
                btnOutputSelect.IsEnabled = info.MList[(int)EnumPermission.Project_Evaluation_Watch_Print];
                btnPrint.IsEnabled = info.MList[(int)EnumPermission.Project_Evaluation_Watch_Print];
                btnPrintExcel.IsEnabled = info.MList[(int)EnumPermission.Project_Evaluation_Watch_Print];

                btnIntegration.IsEnabled = info.MList[(int)EnumPermission.Project_Evaluation_Watch_IntegrationAuto];

                btnPeakWidth.IsEnabled = info.MList[(int)EnumPermission.Project_Evaluation_Watch_IntegrationManual];
                btnPeakBegin.IsEnabled = info.MList[(int)EnumPermission.Project_Evaluation_Watch_IntegrationManual];
                btnPeakEnd.IsEnabled = info.MList[(int)EnumPermission.Project_Evaluation_Watch_IntegrationManual];
                btnPeakAdd.IsEnabled = info.MList[(int)EnumPermission.Project_Evaluation_Watch_IntegrationManual];
                btnPeakDel.IsEnabled = info.MList[(int)EnumPermission.Project_Evaluation_Watch_IntegrationManual];
                btnPeakFrontCut.IsEnabled = info.MList[(int)EnumPermission.Project_Evaluation_Watch_IntegrationManual];
                btnPeakBackCut.IsEnabled = info.MList[(int)EnumPermission.Project_Evaluation_Watch_IntegrationManual];
                btnPeakSeververtical.IsEnabled = info.MList[(int)EnumPermission.Project_Evaluation_Watch_IntegrationManual];
                btnPeakSeverPeakValley.IsEnabled = info.MList[(int)EnumPermission.Project_Evaluation_Watch_IntegrationManual];
                return true;
            }
            else
            {
                this.Close();

                return false;
            }
        }

        /// <summary>
        /// 加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
        }

        /// <summary>
        /// 打开指定ID的结果
        /// </summary>
        /// <param name="id"></param>
        public void Open(List<Signal> signalList, ResultTitle item)
        {
            List<Curve> list = new List<Curve>();
            foreach (var it in signalList)
            {
                list.Add(new Curve(it.MDlyName, it.MUnit, ValueTrans.MediaToDraw(it.MColorOld), it.MShowOld));

                m_listConstName.Add(it.MConstName);
                m_listUnit.Add(it.MUnit);
            }
            this.chromatogramUC.InitDataFrame(list, null, null);

            Thread threadOpen = new Thread(new ParameterizedThreadStart(OpenThread));
            threadOpen.IsBackground = true;
            threadOpen.Start(item);
        }

        /// <summary>
        /// 打开结果的子线程
        /// </summary>
        /// <param name="obj"></param>
        private void OpenThread(object obj)
        {
            try
            {
                this.loadingWaitUC.Dispatcher.Invoke(new Action(delegate ()
                {
                    this.loadingWaitUC.Visibility = Visibility.Visible;
                }));

                ResultManager manager = new ResultManager();

                System.IO.Stream ms = null;
                double cv = 1;
                double ch = 1;
                string attachment = "";
                string markerInfo = null;
                //信号数据
                if (null == manager.GetCurveData(((ResultTitle)obj).MID, SystemControl.SystemControlManager.s_comconfStatic.m_listSmooth, this.chromatogramUC.MListList, out ms, out cv, out ch, out attachment, out markerInfo))
                {
                    m_resultTitle = (ResultTitle)obj;
                    this.chromatogramUC.RestoreLineItemData();
                    this.chromatogramUC.SetMarkerInfo(markerInfo);
                    //设置柱高
                    m_integrationSet.MCH = ch;
                    //寻峰
                    this.chromatogramUC.CalPeak(MListPeak, ch);
                    PeakManager managerPeak = new PeakManager();
                    foreach (var it in MListPeak)
                    {
                        managerPeak.AutoParaOptimizing(it.m_x, it.m_y, it.m_listOriginal, PeakType.VerticalSeparation);
                        it.CalIntegration(m_integrationSet);
                    }
                    if (0 == MListVisible.Count)
                    {
                        foreach (var it in MListPeak)
                        {
                            MListVisible.Add(true);
                        }
                    }

                    this.chromatogramUC.UpdateDraw();
                }

                //审计跟踪
                AuditTrailsManager atManager = new AuditTrailsManager();
                DataTable logdt = null;
                if (null == atManager.SearchAllLog(out logdt, ((ResultTitle)obj).MBeginTime, ((ResultTitle)obj).MEndTime, "All", "All", ""))
                {
                    this.auditTrailsSearchUC.Dispatcher.Invoke(new Action(delegate ()
                    {
                        this.auditTrailsSearchUC.Table = logdt;
                    }));

                    //从日志中寻找收集信息
                    foreach (DataRow it in logdt.Rows)
                    {
                        if (TextLegal.DoubleLegal(it[3].ToString()))
                        {
                            switch (it[1].ToString())
                            {
                                case "Coll":
                                    if (it[7].ToString().Equals(Collection.ReadXamlCollection.S_CollMarkM))
                                    {
                                        this.chromatogramUC.RestoreCollM(new MarkerInfo(it[8].ToString(), Convert.ToDouble(it[3])));
                                    }
                                    else if (it[7].ToString().Equals(Collection.ReadXamlCollection.S_CollMarkA))
                                    {
                                        this.chromatogramUC.RestoreCollA(new MarkerInfo(it[8].ToString(), Convert.ToDouble(it[3])));
                                    }
                                    break;
                                case "Marker":
                                    if (it[7].ToString().Equals(Manual.ReadXamlManual.S_ValveSwitch))
                                    {
                                        this.chromatogramUC.RestoreValve(new MarkerInfo(it[8].ToString(), Convert.ToDouble(it[3])));
                                    }
                                    else if (it[7].ToString().Equals(MethodEdit.ReadXamlMethod.S_PhaseName))
                                    {
                                        this.chromatogramUC.RestorePhase(new MarkerInfo(it[8].ToString(), Convert.ToDouble(it[3])));
                                    }
                                    break;
                            }
                        }
                    }
                }

                //积分
                if (0 != MListPeak.Count)
                {
                    this.dgvIntegration.Dispatcher.Invoke(new Action(delegate ()
                    {
                        this.dgvIntegration.ItemsSource = MListPeak[m_selectIndex].m_list;
                    }));
                }

                //方法信息
                if (null == ms)
                {
                    this.tabItemMethod.Dispatcher.Invoke(new Action(delegate ()
                    {
                        tabItemMethod.Visibility = Visibility.Collapsed;
                    }));
                }
                else
                {
                    Method item = Share.DeepCopy.SetMemoryStream<Method>(ms);
                    if (null != item)
                    {
                        this.tabItemMethod.Dispatcher.Invoke(new Action(delegate ()
                        {
                            MathodFlowDocument mathodFlowDocument = new MathodFlowDocument();
                            FlowDocument doc = mathodFlowDocument.GetFlowDocument(item);
                            doc.DataContext = item;
                            docReader.Document = doc;
                        }));
                    }
                }

                //附加信息
                StringBuilderSplit sbAttachment = new StringBuilderSplit("\n");
                sbAttachment.Append(Share.ReadXaml.S_ColumnVol1 + cv);
                sbAttachment.Append(Share.ReadXaml.S_ColumnHeight1 + ch);
                if (0 != attachment.Length)
                {
                    string[] arr = attachment.Split(';');
                    for (int i = 0; i < arr.Length - 1; i++)
                    {
                        if (0 != arr[i].Length)
                        {
                            sbAttachment.Append("UV" + (i + 1) + Share.ReadXaml.S_UVWaveLength1 + arr[i]);
                        }
                    }
                }
                this.txtAttachment.Dispatcher.Invoke(new Action(delegate ()
                {
                    txtAttachment.Text = sbAttachment.ToString();
                }));
                


                this.loadingWaitUC.Dispatcher.Invoke(new Action(delegate ()
                {
                    this.loadingWaitUC.Visibility = Visibility.Collapsed;
                }));
            }
            catch (Exception ex)
            {
                SystemLog.SystemLogManager.LogWrite(ex);
            }
        }

        /// <summary>
        /// 导出谱图的子线程处理函数
        /// </summary>
        private void OutputExcelFun(object obj)
        {
            try
            {
                this.loadingWaitUC.Dispatcher.Invoke(new Action(delegate ()
                {
                    this.loadingWaitUC.Visibility = Visibility.Visible;
                }));

                Print.ExcelManager manager = new Print.ExcelManager();
                manager.SetValue(this.chromatogramUC.MListCurveName, this.chromatogramUC.MListList, (string)obj);
            }
            catch (Exception ex)
            {
                SystemLog.SystemLogManager.LogWrite(ex);
            }
            finally
            {
                this.loadingWaitUC.Dispatcher.Invoke(new Action(delegate ()
                {
                    this.loadingWaitUC.Visibility = Visibility.Collapsed;
                }));
            }
        }

        /// <summary>
        /// 更新积分表的列的显隐
        /// </summary>
        private void UpdateDGVIntegrationVisibility()
        {
            for (int i = 0; i < dgvIntegration.Columns.Count; i++)
            {
                dgvIntegration.Columns[i].Visibility = m_integrationSet.m_arrShow[i] ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 设置按钮是否可用
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="index"></param>
        private void SetBtnEnabled(bool flag, int index)
        {
            btnPeakWidth.IsEnabled = (flag || 0 == index);
            btnPeakBegin.IsEnabled = (flag || 1 == index);
            btnPeakEnd.IsEnabled = (flag || 2 == index);
            btnPeakAdd.IsEnabled = (flag || 3 == index);
            btnPeakDel.IsEnabled = (flag || 4 == index);
            btnPeakFrontCut.IsEnabled = (flag || 5 == index);
            btnPeakBackCut.IsEnabled = (flag || 6 == index);
        }

        /// <summary>
        /// 修改曲线信号(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DlyUpdateCurve(object sender, RoutedEventArgs e)
        {
            List<NameUnitColorShow> list = (List<NameUnitColorShow>)e.OriginalSource;

            for (int i = 0; i < list.Count; i++)
            {
                SystemControlManager.s_comconfStatic.m_signalList[i].MDlyName = list[i].MName;
                SystemControlManager.s_comconfStatic.m_signalList[i].MColorOld = ValueTrans.DrawToMedia(list[i].MColor);
                SystemControlManager.s_comconfStatic.m_signalList[i].MShowOld = list[i].MShow;
            }
            SystemControlManager.s_comconfStatic.UpdateSignalList();
        }

        /// <summary>
        /// 修改当前选中的曲线(自定义事件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DlyUpdateSelect(object sender, RoutedEventArgs e)
        {
            if (0 < m_listConstName.Count)
            {
                m_selectIndex = (int)e.OriginalSource;

                this.dgvIntegration.ItemsSource = null;
                if (0 < MListPeak.Count)
                {
                    this.dgvIntegration.ItemsSource = MListPeak[m_selectIndex].m_list;
                }

                if (m_listConstName[m_selectIndex].Contains("Cd"))
                {
                    this.colTopValUnit.Text = DlyBase.SC_CDUNIT;
                    this.colStartValUnit.Text = DlyBase.SC_CDUNIT;
                    this.colEndValUnit.Text = DlyBase.SC_CDUNIT;
                    this.colHeightUnit.Text = DlyBase.SC_CDUNIT;
                    this.colAreaUnit.Text = DlyBase.SC_CDAreaUNIT;
                }
                else if (m_listConstName[m_selectIndex].Contains("UV"))
                {
                    this.colTopValUnit.Text = DlyBase.SC_UVABSUNIT;
                    this.colStartValUnit.Text = DlyBase.SC_UVABSUNIT;
                    this.colEndValUnit.Text = DlyBase.SC_UVABSUNIT;
                    this.colHeightUnit.Text = DlyBase.SC_UVABSUNIT;
                    this.colAreaUnit.Text = DlyBase.SC_UVABSAreaUNIT;
                }
                else
                {
                    this.colTopValUnit.Text = DlyBase.SC_CDUNIT;
                    this.colStartValUnit.Text = "";
                    this.colEndValUnit.Text = "";
                    this.colHeightUnit.Text = "";
                    this.colAreaUnit.Text = "";
                }
            }
        }

        /// <summary>
        /// 积分条件设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIntegration_Click(object sender, RoutedEventArgs e)
        {
            IntegrationSetWin win = new IntegrationSetWin();
            win.Owner = this;
            win.MIntegrationSet = m_integrationSet;
            foreach (var it in this.chromatogramUC.MListMarker)
            {
                win.MListCboxValue.Add(it.GetValByBase(this.chromatogramUC.MEnumBase));
                win.MListCboxType.Add(it.MType);
            }

            if (true == win.ShowDialog())
            {
                EvaluationManager manager = new EvaluationManager();
                if (null == manager.SetIntegrationSet(m_integrationSet))
                {
                    UpdateDGVIntegrationVisibility();
                    foreach(var it in MListPeak)
                    {
                        it.CalIntegration(m_integrationSet);
                    }
                }
            }
        }

        /// <summary>
        /// 导出选项设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOutputSelect_Click(object sender, RoutedEventArgs e)
        {
            OutputSelectSetWin win = new OutputSelectSetWin();
            win.Owner = this;
            win.MOutputSelectSet = m_outputSelectSet;
            if (true == win.ShowDialog())
            {
                EvaluationManager manager = new EvaluationManager();
                manager.SetOutputSelectSet(m_outputSelectSet);
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.Project_Evaluation_Watch_Print))
            {
                return;
            }  

            AuditTrails.AuditTrailsStatic.Instance().InsertRowOperate(this.Title + "-" + btnPrint.ToolTip, m_resultTitle.MName);

            Print.PaginatorHeaderFooter.s_signer = Administration.AdministrationStatic.Instance().MSigner;
            Print.PaginatorHeaderFooter.s_reviewer = Administration.AdministrationStatic.Instance().MReviewer;

            for (int i = 0; i < MListVisible.Count; i++)
            {
                MListVisible[i] = this.chromatogramUC.GetCurveVisible(i);
            }

            List<string> listType = new List<string>();
            List<string> listUser = new List<string>();
            List<string> listDate = new List<string>();
            List<string> listInfo = new List<string>();
            foreach (DataRow dataRow in auditTrailsSearchUC.Table.Rows)
            {
                listType.Add(dataRow[1].ToString());
                listUser.Add(dataRow[6].ToString());
                listDate.Add(dataRow[2].ToString());

                StringBuilder sb = new StringBuilder();
                if (!dataRow[3].ToString().Equals("N/A"))
                {
                    sb.Append(dataRow[3].ToString() + "\t");        //批处理(时间)
                    sb.Append(dataRow[4].ToString() + "\t");        //批处理(体积)
                    sb.Append(dataRow[5].ToString() + "\n");        //批处理(柱体积)
                }
                sb.Append(dataRow[7].ToString() + "\n");            //描述
                if (!dataRow[8].ToString().Equals("N/A"))
                {
                    sb.Append(dataRow[8].ToString());               //旧值->新值 
                }
                listInfo.Add(sb.ToString());
            }

            OutputWin win = new OutputWin(this);
            win.SetData(m_outputSelectSet.m_showUser ? m_resultTitle.MUserName : null,
                m_outputSelectSet.m_showChromatogramName ? m_resultTitle.MName : null,
                m_outputSelectSet.m_showChromatogram ? this.chromatogramUC.GetBitmapUp(500, 500) : null,
                m_outputSelectSet.m_showChromatogram ? this.chromatogramUC.GetBitmapDown(500, 500) : null,
                m_outputSelectSet.m_showIntegration ? MListPeak : null,
                MListVisible,
                m_listConstName,
                m_outputSelectSet.m_showMethod ? (null != docReader.Document ? (Method)docReader.Document.DataContext : null) : null,
                m_outputSelectSet.m_showLog ? listType : null,
                m_outputSelectSet.m_showLog ? listUser : null,
                m_outputSelectSet.m_showLog ? listDate : null,
                m_outputSelectSet.m_showLog ? listInfo : null);
            win.ShowDialog();
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintExcel_Click(object sender, RoutedEventArgs e)
        {
            if (null == m_threadOutputExcel || !m_threadOutputExcel.IsAlive)
            {
                if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.Project_Evaluation_Watch_Print))
                {
                    return;
                }

                Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
                sfd.InitialDirectory = @"D:\";
                sfd.Filter = "Excel(*.xls;)|*.xls;";
                sfd.RestoreDirectory = true;
                if (true == sfd.ShowDialog())
                {
                    m_threadOutputExcel = new Thread(new ParameterizedThreadStart(OutputExcelFun));
                    m_threadOutputExcel.IsBackground = true;
                    m_threadOutputExcel.Start(sfd.FileName);

                    AuditTrailsStatic.Instance().InsertRowOperate(this.Title + "-" + btnPrintExcel.ToolTip, m_resultTitle.MName);
                }
            }
            else
            {
                MessageBoxWin.Show(Share.ReadXaml.S_WaitCurrOper);
            }
        }

        /// <summary>
        /// 峰宽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPeakWidth_Click(object sender, RoutedEventArgs e)
        {
            if (true == btnPeakWidth.IsChecked)
            {
                SetBtnEnabled(false, 0);
                this.chromatogramUC.MVisiblePeakStart = true;
                this.chromatogramUC.MVisiblePeakEnd = true;
            }
            else
            {
                SetBtnEnabled(true, 0);
                this.chromatogramUC.MVisiblePeakStart = false;
                this.chromatogramUC.MVisiblePeakEnd = false;

                if (MListPeak[m_selectIndex].ModifyPeakWidth(this.chromatogramUC.GetPeakStartIndex(), this.chromatogramUC.GetPeakEndIndex()))
                {
                    this.chromatogramUC.UpdateDraw();
                }

                AuditTrailsStatic.Instance().InsertRowOperate(this.Title + "-" + btnPeakWidth.ToolTip);
            }
        }

        /// <summary>
        /// 峰起点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPeakBegin_Click(object sender, RoutedEventArgs e)
        {
            if (true == btnPeakBegin.IsChecked)
            {
                SetBtnEnabled(false, 1);
                this.chromatogramUC.MVisiblePeakStart = true;
            }
            else
            {
                SetBtnEnabled(true, 1);
                this.chromatogramUC.MVisiblePeakStart = false;

                if (MListPeak[m_selectIndex].ModifyPeakStart(this.chromatogramUC.GetPeakStartIndex()))
                {
                    this.chromatogramUC.UpdateDraw();
                }
                AuditTrailsStatic.Instance().InsertRowOperate(this.Title + "-" + btnPeakBegin.ToolTip);
            }
        }

        /// <summary>
        /// 峰终点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPeakEnd_Click(object sender, RoutedEventArgs e)
        {
            if (true == btnPeakEnd.IsChecked)
            {
                SetBtnEnabled(false, 2);
                this.chromatogramUC.MVisiblePeakEnd = true;
            }
            else
            {
                SetBtnEnabled(true, 2);
                this.chromatogramUC.MVisiblePeakEnd = false;

                if (MListPeak[m_selectIndex].ModifyPeakEnd(this.chromatogramUC.GetPeakEndIndex()))
                {
                    this.chromatogramUC.UpdateDraw();
                }
                AuditTrailsStatic.Instance().InsertRowOperate(this.Title + "-" + btnPeakEnd.ToolTip);
            }
        }

        /// <summary>
        /// 峰添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPeakAdd_Click(object sender, RoutedEventArgs e)
        {
            if (true == btnPeakAdd.IsChecked)
            {
                SetBtnEnabled(false, 3);
                this.chromatogramUC.MVisiblePeakStart = true;
                this.chromatogramUC.MVisiblePeakEnd = true;
            }
            else
            {
                SetBtnEnabled(true, 3);
                this.chromatogramUC.MVisiblePeakStart = false;
                this.chromatogramUC.MVisiblePeakEnd = false;

                if (MListPeak[m_selectIndex].ModifyPeakPlus(this.chromatogramUC.GetPeakStartIndex(), this.chromatogramUC.GetPeakEndIndex()))
                {
                    this.chromatogramUC.UpdateDraw();
                }
                AuditTrailsStatic.Instance().InsertRowOperate(this.Title + "-" + btnPeakAdd.ToolTip);
            }
        }

        /// <summary>
        /// 峰删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPeakDel_Click(object sender, RoutedEventArgs e)
        {
            if (true == btnPeakDel.IsChecked)
            {
                SetBtnEnabled(false, 4);
                this.chromatogramUC.MVisiblePeakStart = true;
                this.chromatogramUC.MVisiblePeakEnd = true;
            }
            else
            {
                SetBtnEnabled(true, 4);
                this.chromatogramUC.MVisiblePeakStart = false;
                this.chromatogramUC.MVisiblePeakEnd = false;

                if (MListPeak[m_selectIndex].ModifyPeakDel(this.chromatogramUC.GetPeakStartIndex(), this.chromatogramUC.GetPeakEndIndex()))
                {
                    this.chromatogramUC.UpdateDraw();
                }
                AuditTrailsStatic.Instance().InsertRowOperate(this.Title + "-" + btnPeakDel.ToolTip);
            }
        }

        /// <summary>
        /// 峰前切
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPeakFrontCut_Click(object sender, RoutedEventArgs e)
        {
            if (true == btnPeakFrontCut.IsChecked)
            {
                SetBtnEnabled(false, 5);
                this.chromatogramUC.MVisiblePeakStart = true;
                this.chromatogramUC.MVisiblePeakEnd = true;
            }
            else
            {
                SetBtnEnabled(true, 5);
                this.chromatogramUC.MVisiblePeakStart = false;
                this.chromatogramUC.MVisiblePeakEnd = false;

                if (MListPeak[m_selectIndex].ModifyFrontCut(this.chromatogramUC.GetPeakStartIndex(), this.chromatogramUC.GetPeakEndIndex()))
                {
                    this.chromatogramUC.UpdateDraw();
                }
                AuditTrailsStatic.Instance().InsertRowOperate(this.Title + "-" + btnPeakFrontCut.ToolTip);
            }
        }

        /// <summary>
        /// 峰后切
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPeakBackCut_Click(object sender, RoutedEventArgs e)
        {
            if (true == btnPeakBackCut.IsChecked)
            {
                SetBtnEnabled(false, 6);
                this.chromatogramUC.MVisiblePeakStart = true;
                this.chromatogramUC.MVisiblePeakEnd = true;
            }
            else
            {
                SetBtnEnabled(true, 6);
                this.chromatogramUC.MVisiblePeakStart = false;
                this.chromatogramUC.MVisiblePeakEnd = false;

                if (MListPeak[m_selectIndex].ModifyBackCut(this.chromatogramUC.GetPeakStartIndex(), this.chromatogramUC.GetPeakEndIndex()))
                {
                    this.chromatogramUC.UpdateDraw();
                }
                AuditTrailsStatic.Instance().InsertRowOperate(this.Title + "-" + btnPeakBackCut.ToolTip);
            }
        }

        /// <summary>
        /// 峰垂直分离
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPeakSeververtical_Click(object sender, RoutedEventArgs e)
        {
            PeakManager manager = new PeakManager();
            manager.AutoParaOptimizing(MListPeak[m_selectIndex].m_x, MListPeak[m_selectIndex].m_y, MListPeak[m_selectIndex].m_listOriginal, PeakType.VerticalSeparation);
            MListPeak[m_selectIndex].CalIntegration(m_integrationSet);
            this.chromatogramUC.UpdateDraw();
            AuditTrailsStatic.Instance().InsertRowOperate(this.Title + "-" + btnPeakSeververtical.ToolTip);
        }

        /// <summary>
        /// 峰峰谷分离
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPeakSeverPeakValley_Click(object sender, RoutedEventArgs e)
        {
            PeakManager manager = new PeakManager();
            manager.AutoParaOptimizing(MListPeak[m_selectIndex].m_x, MListPeak[m_selectIndex].m_y, MListPeak[m_selectIndex].m_listOriginal, PeakType.PeakValleySeparation);
            MListPeak[m_selectIndex].CalIntegration(m_integrationSet);
            this.chromatogramUC.UpdateDraw();
            AuditTrailsStatic.Instance().InsertRowOperate(this.Title + "-" + btnPeakSeverPeakValley.ToolTip);
        }

        /// <summary>
        /// 当前选中积分的变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvIntegration_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.chromatogramUC.SetPeakIndex(dgvIntegration.SelectedIndex);
        }
    }
}