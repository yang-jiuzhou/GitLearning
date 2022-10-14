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

namespace HBBio.Evaluation
{
    /// <summary>
    /// OutputSelectWin.xaml 的交互逻辑
    /// </summary>
    public partial class OutputSelectSetWin : Window
    {
        public OutputSelectSet MOutputSelectSet { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public OutputSelectSetWin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取审计跟踪对比信息
        /// </summary>
        /// <returns></returns>
        public string GetCompareInfo()
        {
            Share.StringBuilderSplit sb = new Share.StringBuilderSplit("\n");

            if (MOutputSelectSet.m_note != txtNote.Text)
            {
                sb.Append(labNote.Text + MOutputSelectSet.m_note + " -> " + txtNote.Text);
                MOutputSelectSet.m_note = txtNote.Text;
            }
            if (MOutputSelectSet.m_showUser != chboxUser.IsChecked)
            {
                MOutputSelectSet.m_showUser = true == chboxUser.IsChecked;
                sb.Append(chboxUser.Content.ToString() + (MOutputSelectSet.m_showUser ? Share.ReadXaml.S_Enabled : Share.ReadXaml.S_Disabled));
            }
            if (MOutputSelectSet.m_showChromatogramName != chboxChromatogramName.IsChecked)
            {
                MOutputSelectSet.m_showChromatogramName = true == chboxChromatogramName.IsChecked;
                sb.Append(chboxChromatogramName.Content.ToString() + (MOutputSelectSet.m_showChromatogramName ? Share.ReadXaml.S_Enabled : Share.ReadXaml.S_Disabled));
            }
            if (MOutputSelectSet.m_showChromatogram != chboxChromatogram.IsChecked)
            {
                MOutputSelectSet.m_showChromatogram = true == chboxChromatogram.IsChecked;
                sb.Append(chboxChromatogram.Content.ToString() + (MOutputSelectSet.m_showChromatogram ? Share.ReadXaml.S_Enabled : Share.ReadXaml.S_Disabled));
            }
            if (MOutputSelectSet.m_showIntegration != chboxIntegration.IsChecked)
            {
                MOutputSelectSet.m_showIntegration = true == chboxIntegration.IsChecked;
                sb.Append(chboxIntegration.Content.ToString() + (MOutputSelectSet.m_showIntegration ? Share.ReadXaml.S_Enabled : Share.ReadXaml.S_Disabled));
            }
            if (MOutputSelectSet.m_showMethod != chboxMethod.IsChecked)
            {
                MOutputSelectSet.m_showMethod = true == chboxMethod.IsChecked;
                sb.Append(chboxMethod.Content.ToString() + (MOutputSelectSet.m_showMethod ? Share.ReadXaml.S_Enabled : Share.ReadXaml.S_Disabled));
            }
            if (MOutputSelectSet.m_showLog != chboxLog.IsChecked)
            {
                MOutputSelectSet.m_showLog = true == chboxLog.IsChecked;
                sb.Append(chboxLog.Content.ToString() + (MOutputSelectSet.m_showLog ? Share.ReadXaml.S_Enabled : Share.ReadXaml.S_Disabled));
            }

            return sb.ToString();
        }

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (null == MOutputSelectSet)
            {
                MOutputSelectSet = new OutputSelectSet();
            }

            txtNote.Text = MOutputSelectSet.m_note;
            chboxUser.IsChecked = MOutputSelectSet.m_showUser;
            chboxChromatogramName.IsChecked = MOutputSelectSet.m_showChromatogramName;
            chboxChromatogram.IsChecked = MOutputSelectSet.m_showChromatogram;
            chboxIntegration.IsChecked = MOutputSelectSet.m_showIntegration;
            chboxMethod.IsChecked = MOutputSelectSet.m_showMethod;
            chboxLog.IsChecked = MOutputSelectSet.m_showLog;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string logInfo = GetCompareInfo();
            if (!string.IsNullOrEmpty(logInfo))
            {
                AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, logInfo);
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
