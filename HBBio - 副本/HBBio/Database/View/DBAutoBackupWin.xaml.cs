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

namespace HBBio.Database
{
    /// <summary>
    /// DBAutoBackupWin.xaml 的交互逻辑
    /// </summary>
    public partial class DBAutoBackupWin : Window
    {
        public DBAutoBackupInfo MInfo { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public DBAutoBackupWin()
        {
            InitializeComponent();

            List<int> listmp = new List<int>();
            for (int i = 0; i < 30; i++)
            {
                listmp.Add((i + 1));
            }
            cboxFrequency.ItemsSource = listmp;

            listmp = new List<int>();
            for (int i = 0; i < 7; i++)
            {
                listmp.Add((i + 1));
            }
            cboxMaxCount.ItemsSource = listmp;
        }

        /// <summary>
        /// 加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DBManager manager = new DBManager();
            DBAutoBackupInfo tmp = null;
            if (null == manager.GetDBAutoBackup(out tmp))
            {
                MInfo = tmp;
            }
            else
            {
                MInfo = new DBAutoBackupInfo();
            }

            chboxEnabled.IsChecked = MInfo.MEnabled;
            cboxFrequency.SelectedIndex = MInfo.MFrequency - 1;
            cboxMaxCount.SelectedIndex = MInfo.MCount - 1;
            chboxLocal.IsChecked = MInfo.MLocal;
            chboxRemote.IsChecked = MInfo.MRemote;

            txtPathLocal.Text = MInfo.MPathLocal;

            txtIP.Text = MInfo.MIP;
            txtUserName.Text = MInfo.MUserName;
            txtPwd.Text = MInfo.MPwd;
            txtPathRemote.Text = MInfo.MPathRemote;
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
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Share.StringBuilderSplit sb = new Share.StringBuilderSplit();
            if (MInfo.MEnabled != chboxEnabled.IsChecked)
            {
                sb.Append(labAutoBackup.Text + ((bool)chboxEnabled.IsChecked ? Share.ReadXaml.S_Enabled : Share.ReadXaml.S_Disabled));
                MInfo.MEnabled = (bool)chboxEnabled.IsChecked;
            }
            if (MInfo.MFrequency != cboxFrequency.SelectedIndex + 1)
            {
                sb.Append(labFrequency.Text + MInfo.MFrequency + " -> " + cboxFrequency.Text);
                MInfo.MFrequency = cboxFrequency.SelectedIndex + 1;
            }
            if (MInfo.MCount != cboxMaxCount.SelectedIndex + 1)
            {
                sb.Append(labMaxCount.Text + MInfo.MCount + " -> " + cboxMaxCount.Text);
                MInfo.MCount = cboxMaxCount.SelectedIndex + 1;
            }
            if (MInfo.MLocal != chboxLocal.IsChecked)
            {
                sb.Append(labPathSelect.Text + chboxLocal.Content.ToString() + ((bool)chboxLocal.IsChecked ? Share.ReadXaml.S_Enabled : Share.ReadXaml.S_Disabled));
                MInfo.MLocal = (bool)chboxLocal.IsChecked;
            }
            if (MInfo.MRemote != chboxRemote.IsChecked)
            {
                sb.Append(labPathSelect.Text +chboxRemote.Content.ToString() + ((bool)chboxRemote.IsChecked ? Share.ReadXaml.S_Enabled : Share.ReadXaml.S_Disabled));
                MInfo.MRemote = (bool)chboxRemote.IsChecked;
            }
            if (!MInfo.MPathLocal.Equals(txtPathLocal.Text))
            {
                sb.Append(gboxLocal.Header.ToString() + labPathLocal.Text + MInfo.MPathLocal + " -> " + txtPathLocal.Text);
                MInfo.MPathLocal = txtPathLocal.Text;
            }
            if (!MInfo.MIP.Equals(txtIP.Text))
            {
                sb.Append(gboxRemote.Header.ToString() + labIP.Text + MInfo.MIP + " -> " + txtIP.Text);
                MInfo.MIP = txtIP.Text;
            }
            if (!MInfo.MUserName.Equals(txtUserName.Text))
            {
                sb.Append(gboxRemote.Header.ToString() + labUserName.Text + MInfo.MUserName + " -> " + txtUserName.Text);
                MInfo.MUserName = txtUserName.Text;
            }
            if (!MInfo.MPwd.Equals(txtPwd.Text))
            {
                sb.Append(gboxRemote.Header.ToString() + labPwd.Text + MInfo.MPwd + " -> " + txtPwd.Text);
                MInfo.MPwd = txtPwd.Text;
            }
            if (!MInfo.MPathRemote.Equals(txtPathRemote.Text))
            {
                sb.Append(gboxRemote.Header.ToString() + labPathRemote.Text + MInfo.MPathRemote + " -> " + txtPathRemote.Text);
                MInfo.MPathRemote = txtPathRemote.Text;
            }

            if (0 != sb.MLength)
            {
                DBManager manager = new DBManager();
                string error = manager.SetDBAutoBackup(MInfo);
                if (null == error)
                {
                    AuditTrails.AuditTrailsStatic.Instance().InsertRowOperate(this.Title, sb.ToString());
                }
                else
                {
                    AuditTrails.AuditTrailsStatic.Instance().InsertRowError(this.Title, error);
                }
            }

            DialogResult = true;
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
