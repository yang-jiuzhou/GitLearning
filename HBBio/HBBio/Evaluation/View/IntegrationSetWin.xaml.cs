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
    /// IntegrationWin.xaml 的交互逻辑
    /// </summary>
    public partial class IntegrationSetWin : Window
    {
        public List<double> MListCboxValue { get; set; }
        public List<string> MListCboxType { get; set; }
        public IntegrationSet MIntegrationSet { get; set; }
        private IntegrationSet MIntegrationSetShow { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public IntegrationSetWin()
        {
            InitializeComponent();

            MListCboxValue = new List<double>();
            MListCboxType = new List<string>();
            MListCboxValue.Add(0);
            MListCboxType.Add("default");

            List<string> listName = Share.ReadXaml.GetEnumList<EnumIntegration>("E_PI_");
            for (int i = 0; i < listName.Count; i++)
            {
                wrapPanel.Children.Add(new CheckBox() { Content = listName[i], Margin = new Thickness(10), MinWidth = 150 });
            }
        }

        /// <summary>
        /// 获取审计跟踪对比信息
        /// </summary>
        /// <returns></returns>
        public string GetCompareInfo()
        {
            Share.StringBuilderSplit sb = new Share.StringBuilderSplit("\n");

            for (int i = 0; i < wrapPanel.Children.Count; i++)
            {
                if ((true == ((CheckBox)wrapPanel.Children[i]).IsChecked) != MIntegrationSet.m_arrShow[i])
                {
                    if (MIntegrationSet.m_arrShow[i])
                    {
                        sb.Append(((CheckBox)wrapPanel.Children[i]).Content + ":" + Share.ReadXaml.S_Enabled + " -> " + Share.ReadXaml.S_Disabled);
                    }
                    else
                    {
                        sb.Append(((CheckBox)wrapPanel.Children[i]).Content + ":" + Share.ReadXaml.S_Disabled + " -> " + Share.ReadXaml.S_Enabled);
                    }
                    MIntegrationSet.m_arrShow[i] = true == ((CheckBox)wrapPanel.Children[i]).IsChecked;
                }
            }

            if (!MIntegrationSet.MIsMin && MIntegrationSetShow.MIsMin)
            {
                sb.Append(labFilter.Text + rbtnCount.Content.ToString() + " -> " + rbtnMix.Content.ToString());
            }
            else if (MIntegrationSet.MIsMin && !MIntegrationSetShow.MIsMin)
            {
                sb.Append(labFilter.Text + rbtnMix.Content.ToString() + " -> " + rbtnCount.Content.ToString());
            }
            MIntegrationSet.MIsMin = MIntegrationSetShow.MIsMin;
            MIntegrationSet.MIsCount = MIntegrationSetShow.MIsCount;

            if (MIntegrationSet.MMinHeight != MIntegrationSetShow.MMinHeight)
            {
                sb.Append(labMinHeight.Text + MIntegrationSet.MMinHeight + " -> " + MIntegrationSetShow.MMinHeight);
                MIntegrationSet.MMinHeight = MIntegrationSetShow.MMinHeight;
            }

            if (MIntegrationSet.MMinArea != MIntegrationSetShow.MMinArea)
            {
                sb.Append(labMinArea.Text + MIntegrationSet.MMinArea + " -> " + MIntegrationSetShow.MMinArea);
                MIntegrationSet.MMinArea = MIntegrationSetShow.MMinArea;
            }

            if (MIntegrationSet.MMinWidth != MIntegrationSetShow.MMinWidth)
            {
                sb.Append(labMinHalfWidth.Text + MIntegrationSet.MMinWidth + " -> " + MIntegrationSetShow.MMinWidth);
                MIntegrationSet.MMinWidth = MIntegrationSetShow.MMinWidth;
            }

            if (MIntegrationSet.MPeakCount != MIntegrationSetShow.MPeakCount)
            {
                sb.Append(labPeakCount.Text + MIntegrationSet.MPeakCount + " -> " + MIntegrationSetShow.MPeakCount);
                MIntegrationSet.MPeakCount = MIntegrationSetShow.MPeakCount;
            }

            MIntegrationSet.MCH = MIntegrationSetShow.MCH;

            MIntegrationSet.MOriginal = MListCboxValue[cboxOriginal.SelectedIndex];

            return sb.ToString();
        }

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (null == MIntegrationSet)
            {
                MIntegrationSet = new IntegrationSet();
            }
            MIntegrationSetShow = Share.DeepCopy.DeepCopyByXml(MIntegrationSet);

            for (int i = 0; i < wrapPanel.Children.Count; i++)
            {
                ((CheckBox)wrapPanel.Children[i]).IsChecked = MIntegrationSet.m_arrShow[i];
            }

            rbtnMix.DataContext = MIntegrationSetShow;
            doubleMinHeight.DataContext = MIntegrationSetShow;
            doubleMinArea.DataContext = MIntegrationSetShow;
            doubleMinHalfWidth.DataContext = MIntegrationSetShow;
            rbtnCount.DataContext = MIntegrationSetShow;
            intPeakCount.DataContext = MIntegrationSetShow;

            doubleCH.DataContext = MIntegrationSetShow;

            List<string> listOriginal = new List<string>();
            for (int i = 0; i < MListCboxValue.Count; i++)
            {
                listOriginal.Add(MListCboxValue[i] + "    " + MListCboxType[i]);
            }
            cboxOriginal.ItemsSource = listOriginal;
            int indexOriginal = MListCboxValue.IndexOf(MIntegrationSetShow.MOriginal);
            if (-1 != indexOriginal)
            {
                cboxOriginal.SelectedIndex = indexOriginal;
            }
            else
            {
                cboxOriginal.SelectedIndex = 0;
            }
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
