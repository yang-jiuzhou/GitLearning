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
    /// AlarmWarningWin.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmWarningWin : Window
    {
        public AlarmWarning MAlarmWarning { get; set; }
        private AlarmWarningVM MAlarmWarningVM { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public AlarmWarningWin()
        {
            InitializeComponent();

            colLL.ItemsSource = EnumString<EnumAlarmWarningMode>.GetEnumStringList("Com_AW_"); ;
            colL.ItemsSource = EnumString<EnumAlarmWarningMode>.GetEnumStringList("Com_AW_"); ;
            colH.ItemsSource = EnumString<EnumAlarmWarningMode>.GetEnumStringList("Com_AW_"); ;
            colHH.ItemsSource = EnumString<EnumAlarmWarningMode>.GetEnumStringList("Com_AW_"); ;
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
                    MessageBoxWin.Show(it.MNameUnit + " " + labLL.Header + "<=" + labL.Header + "<=" + labH.Header + "<=" + labHH.Header);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 获取审计跟踪对比信息
        /// </summary>
        /// <returns></returns>
        public string GetCompareInfo()
        {
            List<string> listName = Share.ReadXaml.GetEnumList<EnumAlarmWarningMode>("Com_AW_");

            Share.StringBuilderSplit sb = new Share.StringBuilderSplit("\n");

            for (int i = 0; i < MAlarmWarning.MList.Count; i++)
            {
                if (MAlarmWarning.MList[i].MValLL != MAlarmWarningVM.MList[i].MValLL)
                {
                    sb.Append(MAlarmWarning.MList[i].MName + dgvAlarmWarning.Columns[1].Header.ToString() + ":" + MAlarmWarning.MList[i].MValLL + " -> " + MAlarmWarningVM.MList[i].MValLL);
                    MAlarmWarning.MList[i].MValLL = MAlarmWarningVM.MList[i].MValLL;
                }
                if (MAlarmWarning.MList[i].MCheckLL != MAlarmWarningVM.MList[i].MCheckLL)
                {
                    sb.Append(MAlarmWarning.MList[i].MName + dgvAlarmWarning.Columns[2].Header.ToString() + ":" + listName[(int)MAlarmWarning.MList[i].MCheckLL] + " -> " + listName[(int)MAlarmWarningVM.MList[i].MCheckLL]);
                    MAlarmWarning.MList[i].MCheckLL = MAlarmWarningVM.MList[i].MCheckLL;
                }

                if (MAlarmWarning.MList[i].MValL != MAlarmWarningVM.MList[i].MValL)
                {
                    sb.Append(MAlarmWarning.MList[i].MName + dgvAlarmWarning.Columns[3].Header.ToString() + ":" + MAlarmWarning.MList[i].MValL + " -> " + MAlarmWarningVM.MList[i].MValL);
                    MAlarmWarning.MList[i].MValL = MAlarmWarningVM.MList[i].MValL;
                }
                if (MAlarmWarning.MList[i].MCheckL != MAlarmWarningVM.MList[i].MCheckL)
                {
                    sb.Append(MAlarmWarning.MList[i].MName + dgvAlarmWarning.Columns[4].Header.ToString() + ":" + listName[(int)MAlarmWarning.MList[i].MCheckL] + " -> " + listName[(int)MAlarmWarningVM.MList[i].MCheckL]);
                    MAlarmWarning.MList[i].MCheckL = MAlarmWarningVM.MList[i].MCheckL;
                }

                if (MAlarmWarning.MList[i].MValH != MAlarmWarningVM.MList[i].MValH)
                {
                    sb.Append(MAlarmWarning.MList[i].MName + dgvAlarmWarning.Columns[5].Header.ToString() + ":" + MAlarmWarning.MList[i].MValH + " -> " + MAlarmWarningVM.MList[i].MValH);
                    MAlarmWarning.MList[i].MValH = MAlarmWarningVM.MList[i].MValH;
                }
                if (MAlarmWarning.MList[i].MCheckH != MAlarmWarningVM.MList[i].MCheckH)
                {
                    sb.Append(MAlarmWarning.MList[i].MName + dgvAlarmWarning.Columns[6].Header.ToString() + ":" + listName[(int)MAlarmWarning.MList[i].MCheckH] + " -> " + listName[(int)MAlarmWarningVM.MList[i].MCheckH]);
                    MAlarmWarning.MList[i].MCheckH = MAlarmWarningVM.MList[i].MCheckH;
                }

                if (MAlarmWarning.MList[i].MValHH != MAlarmWarningVM.MList[i].MValHH)
                {
                    sb.Append(MAlarmWarning.MList[i].MName + dgvAlarmWarning.Columns[7].Header.ToString() + ":" + MAlarmWarning.MList[i].MValHH + " -> " + MAlarmWarningVM.MList[i].MValHH);
                    MAlarmWarning.MList[i].MValHH = MAlarmWarningVM.MList[i].MValHH;
                }
                if (MAlarmWarning.MList[i].MCheckHH != MAlarmWarningVM.MList[i].MCheckHH)
                {
                    sb.Append(MAlarmWarning.MList[i].MName + dgvAlarmWarning.Columns[8].Header.ToString() + ":" + listName[(int)MAlarmWarning.MList[i].MCheckHH] + " -> " + listName[(int)MAlarmWarningVM.MList[i].MCheckHH]);
                    MAlarmWarning.MList[i].MCheckHH = MAlarmWarningVM.MList[i].MCheckHH;
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
                AuditTrails.AuditTrailsStatic.Instance().InsertRowAlarmWarning(this.Title, logInfo);
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

        private void dgvAlarmWarning_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (1 == e.Column.DisplayIndex || 3 == e.Column.DisplayIndex || 5 == e.Column.DisplayIndex || 7 == e.Column.DisplayIndex)
            {
                TextBox obj = (TextBox)dgvAlarmWarning.Columns[e.Column.DisplayIndex].GetCellContent(dgvAlarmWarning.Items[e.Row.GetIndex()]);
                double min = Convert.ToDouble(((TextBlock)dgvAlarmWarning.Columns[9].GetCellContent(dgvAlarmWarning.Items[e.Row.GetIndex()])).Text);
                double max = Convert.ToDouble(((TextBlock)dgvAlarmWarning.Columns[10].GetCellContent(dgvAlarmWarning.Items[e.Row.GetIndex()])).Text);
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
    }
}
