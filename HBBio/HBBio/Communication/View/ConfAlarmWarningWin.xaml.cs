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

namespace HBBio.Communication
{
    /// <summary>
    /// ConfAlarmWarningWin.xaml 的交互逻辑
    /// </summary>
    public partial class ConfAlarmWarningWin : Window
    {
        public AlarmWarning MAlarmWarning { get; set; }
        private AlarmWarningVM MAlarmWarningVM { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public ConfAlarmWarningWin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 数据检查
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            foreach (var it in MAlarmWarningVM.MList)
            {
                if (!(it.MValLL <= it.MValL && it.MValL <= it.MValH && it.MValH <= it.MValHH))
                {
                    MessageBoxWin.Show(it.MNameUnit + " " + colLL.Header + "<=" + colL.Header + "<=" + colH.Header + "<=" + colHH.Header);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 获取审计跟踪对比信息
        /// </summary>
        /// <returns></returns>
        private string GetCompareInfo()
        {
            Share.StringBuilderSplit sb = new Share.StringBuilderSplit("\n");

            for (int i = 0; i < MAlarmWarning.MList.Count; i++)
            {
                if (MAlarmWarning.MList[i].MValLL != MAlarmWarningVM.MList[i].MValLL)
                {
                    sb.Append(MAlarmWarning.MList[i].MName + dgvAlarmWarning.Columns[1].Header.ToString() + ":" + MAlarmWarning.MList[i].MValLL + " -> " + MAlarmWarningVM.MList[i].MValLL);
                    MAlarmWarning.MList[i].MValLL = MAlarmWarningVM.MList[i].MValLL;
                }
                if (MAlarmWarning.MList[i].MValL != MAlarmWarningVM.MList[i].MValL)
                {
                    sb.Append(MAlarmWarning.MList[i].MName + dgvAlarmWarning.Columns[2].Header.ToString() + ":" + MAlarmWarning.MList[i].MValL + " -> " + MAlarmWarningVM.MList[i].MValL);
                    MAlarmWarning.MList[i].MValL = MAlarmWarningVM.MList[i].MValL;
                }
                if (MAlarmWarning.MList[i].MValH != MAlarmWarningVM.MList[i].MValH)
                {
                    sb.Append(MAlarmWarning.MList[i].MName + dgvAlarmWarning.Columns[3].Header.ToString() + ":" + MAlarmWarning.MList[i].MValH + " -> " + MAlarmWarningVM.MList[i].MValH);
                    MAlarmWarning.MList[i].MValH = MAlarmWarningVM.MList[i].MValH;
                }
                if (MAlarmWarning.MList[i].MValHH != MAlarmWarningVM.MList[i].MValHH)
                {
                    sb.Append(MAlarmWarning.MList[i].MName + dgvAlarmWarning.Columns[4].Header.ToString() + ":" + MAlarmWarning.MList[i].MValHH + " -> " + MAlarmWarningVM.MList[i].MValHH);
                    MAlarmWarning.MList[i].MValHH = MAlarmWarningVM.MList[i].MValHH;
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (null == MAlarmWarning)
            {
                MAlarmWarning = new AlarmWarning();
            }
            MAlarmWarningVM = new AlarmWarningVM(Share.DeepCopy.DeepCopyByXml(MAlarmWarning));

            dgvAlarmWarning.ItemsSource = MAlarmWarningVM.MList;
        }

        /// <summary>
        /// 最值判断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvAlarmWarning_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (1 <= e.Column.DisplayIndex && e.Column.DisplayIndex <= 4)
            {
                TextBox obj = (TextBox)dgvAlarmWarning.Columns[e.Column.DisplayIndex].GetCellContent(dgvAlarmWarning.Items[e.Row.GetIndex()]);
                double min = Convert.ToDouble(((TextBlock)dgvAlarmWarning.Columns[5].GetCellContent(dgvAlarmWarning.Items[e.Row.GetIndex()])).Text);
                double max = Convert.ToDouble(((TextBlock)dgvAlarmWarning.Columns[6].GetCellContent(dgvAlarmWarning.Items[e.Row.GetIndex()])).Text);
                if (TextLegal.DoubleLegal(obj.Text))
                {
                    if (Convert.ToDouble(obj.Text) > max)
                    {
                        obj.Text = max.ToString();
                    }
                    else if (Convert.ToDouble(obj.Text) < min)
                    {
                        obj.Text = min.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// 通讯警报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnCommun_Click(object sender, RoutedEventArgs e)
        {
            StringBoolWin win = new StringBoolWin(btnCommun.Content.ToString());
            foreach (BaseCommunication item in ComConfStatic.Instance().m_comList)
            {
                win.AddItem(new StringBool(item.MComConf.MName + item.MComConf.MList[0].MDlyName, item.MComConf.MAlarm));
            }
            if (true == win.ShowDialog())
            {
                int index = 0;
                StringBuilderSplit sb = new StringBuilderSplit();
                foreach (BaseCommunication item in ComConfStatic.Instance().m_comList)
                {
                    if (item.MComConf.MAlarm != win.GetItem(index).MCheck)
                    {
                        item.MComConf.MAlarm = win.GetItem(index).MCheck;
                        sb.Append(win.GetItem(index).MName + " : " + (item.MComConf.MAlarm ? Share.ReadXaml.S_Enabled : Share.ReadXaml.S_Disabled));
                    }
                    index++;
                }
                CommunicationSetsManager csManager = new CommunicationSetsManager();
                string error = csManager.EditItem(ComConfStatic.Instance().m_cs, ComConfStatic.Instance().m_cfList);
                if (null == error)
                {
                    AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(btnCommun.Content.ToString(), sb.ToString());
                }
                else
                {
                    MessageBoxWin.Show(error);
                }
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