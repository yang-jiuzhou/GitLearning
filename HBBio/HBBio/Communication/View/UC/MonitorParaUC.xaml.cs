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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HBBio.Communication
{
    /// <summary>
    /// MonitorParaUC.xaml 的交互逻辑
    /// </summary>
    public partial class MonitorParaUC : UserControl
    {
        private MonitroPara m_monitroPara = new MonitroPara();
        public MonitroPara MMonitroPara
        {
            get
            {
                return m_monitroPara;
            }
            set
            {
                m_monitroPara = value;

                MMonitroParaVM = new MonitroParaVM(Share.DeepCopy.DeepCopyByXml(value));
                this.DataContext = MMonitroParaVM;
            }
        }
        private MonitroParaVM MMonitroParaVM { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public MonitorParaUC()
        {
            InitializeComponent();
            
            cboxJudge.ItemsSource = EnumString<EnumJudge>.GetEnumStringList();
            cboxStabilityUnit.ItemsSource = EnumBaseString.GetItemsSource();
        }

        /// <summary>
        /// 返回修改信息
        /// </summary>
        /// <returns></returns>
        public string GetLog()
        {
            MMonitroPara.MJudge = MMonitroParaVM.MJudge;
            MMonitroPara.MName = MMonitroParaVM.MName;
            MMonitroPara.MMoreThan = MMonitroParaVM.MMoreThan;
            MMonitroPara.MLessThan = MMonitroParaVM.MLessThan;
            MMonitroPara.MStabilityLength = MMonitroParaVM.MStabilityLength;
            MMonitroPara.MStabilityUnit = MMonitroParaVM.MStabilityUnit;

            StringBuilderSplit sb = new StringBuilderSplit();
            sb.Append(labJudge.Text + cboxJudge.Text);
            switch (MMonitroParaVM.MJudge)
            {
                case EnumJudge.Stable:
                    sb.Append(labMoreThan.Text + MMonitroParaVM.MMoreThan);
                    sb.Append(labLessThan.Text + MMonitroParaVM.MLessThan);
                    break;
                case EnumJudge.MoreThan:
                    sb.Append(labMoreThan.Text + MMonitroParaVM.MMoreThan);
                    break;
                case EnumJudge.LessThan:
                    sb.Append(labLessThan.Text + MMonitroParaVM.MLessThan);
                    break;
            }
            sb.Append(labStability.Text + MMonitroParaVM.MStabilityLength + cboxStabilityUnit.Text);
            return sb.ToString();
        }

        private void cboxJudge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((EnumJudge)cboxJudge.SelectedIndex)
            {
                case EnumJudge.Stable:
                    doubleMoreThan.IsEnabled = true;
                    doubleLessThan.IsEnabled = true;
                    break;
                case EnumJudge.MoreThan:
                    doubleMoreThan.IsEnabled = true;
                    doubleLessThan.IsEnabled = false;
                    break;
                case EnumJudge.LessThan:
                    doubleMoreThan.IsEnabled = false;
                    doubleLessThan.IsEnabled = true;
                    break;
            }
        }
    }
}
