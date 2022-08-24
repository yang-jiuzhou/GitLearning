using HBBio.Administration;
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

namespace HBBio.AuditTrails
{
    /// <summary>
    /// AuditTrailWin.xaml 的交互逻辑
    /// </summary>
    public partial class AuditTrailWin : Window, WindowPermission
    {
        /// <summary>
        /// 读数据库的线程
        /// </summary>
        private Thread m_opendb = null;
        /// <summary>
        /// 导出Excel的线程
        /// </summary>
        private Thread m_threadOutputExcel = null;


        /// <summary>
        /// 构造函数
        /// </summary>
        public AuditTrailWin(Window parent)
        {
            InitializeComponent();

            this.Owner = parent;

            cboxType.ItemsSource = MString.GetList(Enum.GetNames(typeof(EnumATType)));

            List<MString> nameList = new List<MString>();
            nameList.Add(new MString("All"));
            AdministrationManager manager = new AdministrationManager();
            List<UserInfo> list = null;
            if (null == manager.GetUserInfoList(out list))
            {
                foreach (var it in list)
                {
                    nameList.Add(new MString(it.MUserName));
                }
            }
            cboxUserName.ItemsSource = nameList;

            SearchInfoVM searchInfo = new SearchInfoVM(EnumATType.All.ToString(), AdministrationStatic.Instance().MUserInfo.MUserName);
            foreach (FrameworkElement it in gridSearch.Children)
            {
                if (it is TextBlock)
                {
                    continue;
                }

                it.DataContext = searchInfo;
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
        /// 设置各模块是否可用
        /// </summary>
        /// <param name="info"></param>
        public bool SetPermission(PermissionInfo info)
        {
            if (info.MList[(int)EnumPermission.AuditTrails])
            {
                btnSearch.IsEnabled = info.MList[(int)EnumPermission.AuditTrails];

                btnPDF.IsEnabled = info.MList[(int)EnumPermission.AuditTrails_Print];
                btnExcel.IsEnabled = info.MList[(int)EnumPermission.AuditTrails_Print];

                return true;
            }
            else
            {
                this.Close();

                return false;
            }
        }

        /// <summary>
        /// 子线程读数据库的操作函数
        /// </summary>
        private void ReadData(object val)
        {
            try
            {
                this.auditTrailsSearchUC.Dispatcher.Invoke(new Action(delegate ()
                {
                    this.auditTrailsSearchUC.LoadingWaitVisibility = Visibility.Visible;
                }));

                SearchInfoVM info = (SearchInfoVM)val;
                AuditTrailsManager manager = new AuditTrailsManager();
                DataTable table = null;
                if (null == manager.SearchAllLog(out table, info.MDateTimeStart, info.MDateTimeStop, info.MType, info.MUserName, info.MFilter))
                {
                    this.auditTrailsSearchUC.Dispatcher.Invoke(new Action(delegate ()
                    {
                        this.auditTrailsSearchUC.Table = table;
                    }));
                }

                this.auditTrailsSearchUC.Dispatcher.Invoke(new Action(delegate ()
                {
                    this.auditTrailsSearchUC.LoadingWaitVisibility = Visibility.Collapsed;
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
                this.auditTrailsSearchUC.Dispatcher.Invoke(new Action(delegate ()
                {
                    this.auditTrailsSearchUC.LoadingWaitVisibility = Visibility.Visible;
                }));

                Print.ExcelManager manager = new Print.ExcelManager();
                manager.SetValue(auditTrailsSearchUC.Table, (string)obj);
            }
            catch (Exception ex)
            {
                SystemLog.SystemLogManager.LogWrite(ex);
            }
            finally
            {
                this.auditTrailsSearchUC.Dispatcher.Invoke(new Action(delegate ()
                {
                    this.auditTrailsSearchUC.LoadingWaitVisibility = Visibility.Collapsed;
                }));
            }
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //启动子线程
            if (null == m_opendb || !m_opendb.IsAlive)
            {
                StringBuilderSplit sb = new StringBuilderSplit();
                sb.Append(labBeginTime.Text + timeBegin.Text);
                sb.Append(labEndTime.Text + timeEnd.Text);
                sb.Append(labType.Text + cboxType.Text);
                sb.Append(labUserName.Text + cboxUserName.Text);
                sb.Append(labFilter.Text + txtFilter.Text);

                AuditTrailsStatic.Instance().InsertRowOperate(this.Title + "-" + btnSearch.ToolTip, sb.ToString());

                m_opendb = new Thread(new ParameterizedThreadStart(ReadData));
                m_opendb.IsBackground = true;
                m_opendb.Start((SearchInfoVM)timeBegin.DataContext);
            }
        }

        /// <summary>
        /// 打印PDF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPDF_Click(object sender, RoutedEventArgs e)
        {
            if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.AuditTrails_Print))
            {
                return;
            }

            if (null == auditTrailsSearchUC.Table)
            {
                return;
            }

            Print.PaginatorHeaderFooter.s_signer = Administration.AdministrationStatic.Instance().MSigner;
            Print.PaginatorHeaderFooter.s_reviewer = Administration.AdministrationStatic.Instance().MReviewer;

            
            AuditTrailsStatic.Instance().InsertRowOperate(this.Title + "-" + btnPDF.ToolTip);

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
            win.SetData(listType, listUser, listDate, listInfo);
            if (true == win.ShowDialog())
            {
                
            }
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            if (null == m_threadOutputExcel || !m_threadOutputExcel.IsAlive)
            {
                if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.AuditTrails_Print))
                {
                    return;
                }

                if (null == auditTrailsSearchUC.Table)
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

                    AuditTrailsStatic.Instance().InsertRowOperate(this.Title + "-" + btnExcel.ToolTip);
                }
            }
            else
            {
                MessageBoxWin.Show(Share.ReadXaml.S_WaitCurrOper);
            }
        }
    }
}