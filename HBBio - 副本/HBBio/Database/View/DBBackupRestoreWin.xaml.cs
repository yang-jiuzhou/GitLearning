using HBBio.Administration;
using System;
using System.Collections.Generic;
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

namespace HBBio.Database
{
    /// <summary>
    /// DBBackupRestoreWin.xaml 的交互逻辑
    /// </summary>
    public partial class DBBackupRestoreWin : Window, WindowPermission
    {
        private DBBackupRestoreInfo m_dbPath = null;
        private Thread m_threadBackup;                  //数据库备份的线程
        private Thread m_threadRestore;                 //数据库还原的线程


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        public DBBackupRestoreWin(Window parent)
        {
            InitializeComponent();

            this.Owner = parent;

            DataToUI();
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
        /// 本地备份的路径选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPathLocal_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
            dlg.Description = ReadXamlDatabase.GetResources(ReadXamlDatabase.C_PathLocal);
            dlg.SelectedPath = txtPathLocal.Text;
            if (System.Windows.Forms.DialogResult.OK == dlg.ShowDialog())
            {
                txtPathLocal.Text = dlg.SelectedPath;
            }
        }

        /// <summary>
        /// 远程备份的路径选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPathRemote_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
                dlg.Description = ReadXamlDatabase.GetResources(ReadXamlDatabase.C_PathRemote);
                if (System.Windows.Forms.DialogResult.OK == dlg.ShowDialog())
                {
                    string[] strArr = dlg.SelectedPath.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                    txtIP.Text = strArr[0];
                    txtPathRemote.Text = "";
                    for (int i = 1; i < strArr.Length; i++)
                    {
                        txtPathRemote.Text += strArr[i] + "\\";
                    }
                    txtPathRemote.Text = txtPathRemote.Text.Remove(txtPathRemote.Text.Length - 1, 1).Replace("$", ":");
                }
            }
            catch (Exception msg)
            {
                txtIP.Text = "";
                txtPathRemote.Text = "";
                Share.MessageBoxWin.Show(msg.Message);
            }
        }

        /// <summary>
        /// 本地还原的路径选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestoreLocal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                if (true == dlg.ShowDialog())
                {
                    txtRestoreLocalPath.Text = dlg.FileName.Substring(0, dlg.FileName.LastIndexOf("\\"));
                    txtRestoreLocalFile.Text = dlg.FileName.Replace(txtRestoreLocalPath.Text, "");
                    txtRestoreLocalFile.Text = txtRestoreLocalFile.Text.Substring(1, txtRestoreLocalFile.Text.LastIndexOf("_") - 1);
                }
            }
            catch (Exception msg)
            {
                txtRestoreLocalPath.Text = "";
                txtRestoreLocalFile.Text = "";
                Share.MessageBoxWin.Show(msg.Message);
            }
        }

        /// <summary>
        /// 远程还原的路径选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestoreRemote_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                if (true == dlg.ShowDialog())
                {
                    string[] strArr = dlg.FileName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                    txtRestoreRemoteIP.Text = strArr[0];
                    txtRestoreRemotePath.Text = "";
                    for (int i = 1; i < strArr.Length - 1; i++)
                    {
                        txtRestoreRemotePath.Text += strArr[i] + "\\";
                    }
                    txtRestoreRemotePath.Text = txtRestoreRemotePath.Text.Remove(txtRestoreRemotePath.Text.Length - 1, 1).Replace("$", ":");
                    txtRestoreRemoteFile.Text = strArr[strArr.Length - 1].Substring(0, strArr[strArr.Length - 1].LastIndexOf("_"));
                }
            }
            catch (Exception msg)
            {
                txtRestoreLocalPath.Text = "";
                txtRestoreLocalFile.Text = "";
                Share.MessageBoxWin.Show(msg.Message);
            }
        }

        /// <summary>
        /// 复制用户密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            txtRestoreRemoteUserName.Text = txtUserName.Text;
            txtRestoreRemotePwd.Text = txtPwd.Text;
        }

        /// <summary>
        /// 自动备份
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAuto_Click(object sender, RoutedEventArgs e)
        {
            DBAutoBackupWin win = new DBAutoBackupWin();
            win.ShowDialog();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (true == rbtnBackup.IsChecked)
            {
                if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.Databases_Backup))
                {
                    return;
                }
            }

            if (true == rbtnRestore.IsChecked)
            {
                if (!Administration.AdministrationStatic.Instance().ShowSignerReviewerWin(this, Administration.EnumSignerReviewer.Databases_Restore))
                {
                    return;
                }
            }


            DataFromUI();

            Backup();
            Restore();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// 设置各模块是否可用
        /// </summary>
        /// <param name="info"></param>
        public bool SetPermission(PermissionInfo info)
        {
            if (info.MList[(int)EnumPermission.Databases])
            {
                rbtnBackup.IsEnabled = info.MList[(int)EnumPermission.Databases_Backup];
                rbtnRestore.IsEnabled = info.MList[(int)EnumPermission.Databases_Restore];
                btnAuto.IsEnabled = info.MList[(int)EnumPermission.Databases_Backup];
                return true;
            }
            else
            {
                this.Close();

                return false;
            }
        }

        /// <summary>
        /// 数据->界面
        /// </summary>
        private void DataToUI()
        {
            DBManager manager = new DBManager();
            manager.GetDBBackupRestore(out m_dbPath);

            txtPathLocal.Text = m_dbPath.MBackupPathLocal;
            txtIP.Text = m_dbPath.MBackupIP;
            txtUserName.Text = m_dbPath.MBackupUserName;
            txtPwd.Text = m_dbPath.MBackupPwd;
            txtPathRemote.Text = m_dbPath.MBackupPathRemote;

            txtRestoreLocalPath.Text = m_dbPath.MRestorePathLocal;
            txtRestoreRemoteIP.Text = m_dbPath.MRestoreIP;
            txtRestoreRemoteUserName.Text = m_dbPath.MRestoreUserName;
            txtRestoreRemotePwd.Text = m_dbPath.MRestorePwd;
            txtRestoreRemotePath.Text = m_dbPath.MRestorePathRemote;
        }

        /// <summary>
        /// 界面->数据
        /// </summary>
        private void DataFromUI()
        {
            Share.StringBuilderSplit sb = new Share.StringBuilderSplit();
            if (!m_dbPath.MBackupPathLocal.Equals(txtPathLocal.Text))
            {
                sb.Append(rbtnBackup.Content.ToString() + checkBackupLocal.Content.ToString() + labPathLocal.Text + m_dbPath.MBackupPathLocal + " -> " + txtPathLocal.Text);
                m_dbPath.MBackupPathLocal = txtPathLocal.Text;
            }
            if (!m_dbPath.MBackupIP.Equals(txtIP.Text))
            {
                sb.Append(rbtnBackup.Content.ToString() + checkBackupRemote.Content.ToString() + labIP.Text + m_dbPath.MBackupIP + " -> " + txtIP.Text);
                m_dbPath.MBackupIP = txtIP.Text;
            }
            if (!m_dbPath.MBackupUserName.Equals(txtUserName.Text))
            {
                sb.Append(rbtnBackup.Content.ToString() + checkBackupRemote.Content.ToString() + labUserName.Text + m_dbPath.MBackupUserName + " -> " + txtUserName.Text);
                m_dbPath.MBackupUserName = txtUserName.Text;
            }
            if (!m_dbPath.MBackupPwd.Equals(txtPwd.Text))
            {
                sb.Append(rbtnBackup.Content.ToString() + checkBackupRemote.Content.ToString() + labPwd.Text + m_dbPath.MBackupPwd + " -> " + txtPwd.Text);
                m_dbPath.MBackupPwd = txtPwd.Text;
            }
            if (!m_dbPath.MBackupPathRemote.Equals(txtPathRemote.Text))
            {
                sb.Append(rbtnBackup.Content.ToString() + checkBackupRemote.Content.ToString() + labPathRemote.Text + m_dbPath.MBackupPathRemote + " -> " + txtPathRemote.Text);
                m_dbPath.MBackupPathRemote = txtPathRemote.Text;
            }

            if (!m_dbPath.MRestorePathLocal.Equals(txtRestoreLocalPath.Text))
            {
                sb.Append(rbtnBackup.Content.ToString() + rbtnRestoreLocal.Content.ToString() + labRestoreLocalPath.Text + m_dbPath.MRestorePathLocal + " -> " + txtRestoreLocalPath.Text);
                m_dbPath.MRestorePathLocal = txtRestoreLocalPath.Text;
            }
            m_dbPath.MRestoreFileLocal = txtRestoreLocalFile.Text;
            if (!m_dbPath.MRestoreIP.Equals(txtRestoreRemoteIP.Text))
            {
                sb.Append(rbtnBackup.Content.ToString() + rbtnRestoreRemote.Content.ToString() + labRestoreRemoteIP.Text + m_dbPath.MRestoreIP + " -> " + txtRestoreRemoteIP.Text);
                m_dbPath.MRestoreIP = txtRestoreRemoteIP.Text;
            }
            if (!m_dbPath.MRestoreUserName.Equals(txtRestoreRemoteUserName.Text))
            {
                sb.Append(rbtnBackup.Content.ToString() + rbtnRestoreRemote.Content.ToString() + labRemoteUserName.Text + m_dbPath.MRestoreUserName + " -> " + txtRestoreRemoteUserName.Text);
                m_dbPath.MRestoreUserName = txtRestoreRemoteUserName.Text;
            }
            if (!m_dbPath.MRestorePwd.Equals(txtRestoreRemotePwd.Text))
            {
                sb.Append(rbtnBackup.Content.ToString() + rbtnRestoreRemote.Content.ToString() + labRestoreRemotePwd.Text + m_dbPath.MRestorePwd + " -> " + txtRestoreRemotePwd.Text);
                m_dbPath.MRestorePwd = txtRestoreRemotePwd.Text;
            }
            if (!m_dbPath.MRestorePathRemote.Equals(txtRestoreRemotePath.Text))
            {
                sb.Append(rbtnBackup.Content.ToString() + rbtnRestoreRemote.Content.ToString() + labRestoreRemotePath.Text + m_dbPath.MRestorePathRemote + " -> " + txtRestoreRemotePath.Text);
                m_dbPath.MRestorePathRemote = txtRestoreRemotePath.Text;
            }
            m_dbPath.MRestoreFileRemote = txtRestoreRemoteFile.Text;

            if (0 != sb.MLength)
            {
                DBManager manager = new DBManager();
                string error = manager.SetDBBackupRestore(m_dbPath);
                if (null == error)
                {
                    AuditTrails.AuditTrailsStatic.Instance().InsertRowOperate(this.Title, sb.ToString());
                }
                else
                {
                    AuditTrails.AuditTrailsStatic.Instance().InsertRowError(this.Title, error);
                }
            }
        }

        /// <summary>
        /// 备份
        /// </summary>
        private void Backup()
        {
            if (true == rbtnBackup.IsChecked)
            {
                if (null != m_threadBackup && m_threadBackup.IsAlive)
                {
                    return;
                }

                m_threadBackup = new Thread(ThreadBackup);
                m_threadBackup.IsBackground = true;
                m_threadBackup.Start();
            }
        }

        /// <summary>
        /// 备份的子线程操作
        /// </summary>
        private void ThreadBackup()
        {
            try
            {
                this.loadingWaitUC.Dispatcher.Invoke(new Action(delegate ()
                {
                    this.loadingWaitUC.Visibility = Visibility.Visible;
                }));

                bool checkLocal = false;
                bool checkRemote = false;
                this.Dispatcher.Invoke(new Action(delegate ()
                {
                    checkLocal = true == checkBackupLocal.IsChecked;
                    checkRemote = true == checkBackupRemote.IsChecked;
                }));

                bool resultLocal = false;
                bool resultRemote = false;
                string notime = DateTime.Now.ToString("yyyyMMddHHmmss");
                if (checkLocal)
                {
                    BaseDB db = new BaseDB();
                    resultLocal = db.BackupLocal(notime, m_dbPath.MBackupPathLocal);
                }
                if (checkRemote)
                {
                    BaseDB db = new BaseDB();
                    resultRemote = db.BackupServer(notime, m_dbPath.MBackupIP, m_dbPath.MBackupUserName, m_dbPath.MBackupPwd, m_dbPath.MBackupPathRemote);
                }

                this.Dispatcher.Invoke(new Action(delegate ()
                {
                    if (resultLocal)
                    {
                        AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(rbtnBackup.Content.ToString() + "-" + checkBackupLocal.Content.ToString(), notime);
                    }
                    if (resultRemote)
                    {
                        AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(rbtnBackup.Content.ToString() + "-" + checkBackupRemote.Content.ToString(), notime);
                    }

                    string info = null;
                    if (true == checkBackupLocal.IsChecked)
                    {
                        info += checkBackupLocal.Content.ToString() + Share.ReadXaml.GetResources(resultLocal ? ReadXamlDatabase.C_BackupSuccess : ReadXamlDatabase.C_BackupFail) + "\n";
                    }

                    if (true == checkBackupRemote.IsChecked)
                    {
                        info += checkBackupRemote.Content.ToString() + Share.ReadXaml.GetResources(resultRemote ? ReadXamlDatabase.C_BackupSuccess : ReadXamlDatabase.C_BackupFail);
                    }

                    if (null != info)
                    {
                        Share.MessageBoxWin.Show(info);
                    }
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
        /// 还原
        /// </summary>
        private void Restore()
        {
            if (true == rbtnRestore.IsChecked)
            {
                if (null != m_threadRestore && m_threadRestore.IsAlive)
                {
                    return;
                }

                m_threadRestore = new Thread(ThreadRestore);
                m_threadRestore.IsBackground = true;
                m_threadRestore.Start();
            }
        }

        /// <summary>
        /// 还原的子线程操作
        /// </summary>
        private void ThreadRestore()
        {
            try
            {
                this.loadingWaitUC.Dispatcher.Invoke(new Action(delegate ()
                {
                    this.loadingWaitUC.Visibility = Visibility.Visible;
                }));


                bool checkLocal = false;
                bool checkRemote = false;
                this.Dispatcher.Invoke(new Action(delegate ()
                {
                    checkLocal = true == rbtnRestoreLocal.IsChecked;
                    checkRemote = true == rbtnRestoreRemote.IsChecked;
                }));

                if (checkLocal)
                {
                    BaseDB db = new BaseDB();
                    bool result = db.RestoreLocal(m_dbPath.MRestorePathLocal, m_dbPath.MRestoreFileLocal);
                    this.Dispatcher.Invoke(new Action(delegate ()
                    {
                        if (result)
                        {
                            AuditTrails.AuditTrailsStatic.Instance().CreateTable();
                            AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(rbtnRestore.Content.ToString() + "-" + rbtnRestoreLocal.Content.ToString(), m_dbPath.MRestorePathLocal + "\\" + m_dbPath.MRestoreFileLocal);
                            Share.MessageBoxWin.Show(rbtnRestoreLocal.Content.ToString() + Share.ReadXaml.GetResources(ReadXamlDatabase.C_RestoreSuccess));
                        }
                        else
                        {
                            Share.MessageBoxWin.Show(rbtnRestoreLocal.Content.ToString() + Share.ReadXaml.GetResources(ReadXamlDatabase.C_RestoreFail));
                        }
                    }));
                }
                if (checkRemote)
                {
                    BaseDB db = new BaseDB();
                    bool result = db.RestoreServer(m_dbPath.MRestoreIP, m_dbPath.MRestoreUserName, m_dbPath.MRestorePwd, m_dbPath.MRestorePathRemote, m_dbPath.MRestoreFileRemote);
                    this.Dispatcher.Invoke(new Action(delegate ()
                    {
                        if (result)
                        {
                            AuditTrails.AuditTrailsStatic.Instance().CreateTable();
                            AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(rbtnRestore.Content.ToString() + "-" + rbtnRestoreRemote.Content.ToString(), m_dbPath.MRestoreIP + "\\" + m_dbPath.MRestorePathRemote + "\\" + m_dbPath.MRestoreFileRemote);
                            Share.MessageBoxWin.Show(rbtnRestoreRemote.Content.ToString() + Share.ReadXaml.GetResources(ReadXamlDatabase.C_RestoreSuccess));
                        }
                        else
                        {
                            Share.MessageBoxWin.Show(rbtnRestoreRemote.Content.ToString() + Share.ReadXaml.GetResources(ReadXamlDatabase.C_RestoreFail));
                        }
                    }));
                }

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
    }
}
