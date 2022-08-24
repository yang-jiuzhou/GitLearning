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

namespace HBBio.MethodEdit
{
    /// <summary>
    /// PersonalPhaseWin.xaml 的交互逻辑
    /// </summary>
    public partial class PersonalPhaseWin : Window
    {
        public PersonalPhaseWin()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            StringBuilderSplit sb = new StringBuilderSplit(";");

            int flow = 0;
            if (true == chboxFlowRate.IsChecked)
            {
                sb.Append(EnumGroupType.FlowRate);
                flow++;
            }
            if (true == chboxValveSelection.IsChecked)
            {
                StringBuilderSplit sb1 = new StringBuilderSplit("&");
                sb1.Append(EnumGroupType.ValveSelection);
                sb1.Append(rbtnValveSelectionUCPerShow.IsChecked);
                sb1.Append(rbtnValveSelectionUCFillSystemShow.IsChecked);
                sb.Append(sb1.ToString());
            }
            if (true == chboxMixer.IsChecked)
            {
                sb.Append(EnumGroupType.Mixer);
            }
            if (true == chboxBPV.IsChecked)
            {
                sb.Append(EnumGroupType.BPV);
            }
            if (true == chboxUVReset.IsChecked)
            {
                sb.Append(EnumGroupType.UVReset);
            }
            int tvcv = 0;
            if (true == chboxSampleApplicationTech.IsChecked)
            {
                sb.Append(EnumGroupType.SampleApplicationTech);
                tvcv++;
            }
            if (true == chboxTVCV.IsChecked)
            {
                sb.Append(EnumGroupType.TVCV);
                tvcv++;
            }
            if (true == chboxFlowValveLength.IsChecked)
            {
                sb.Append(EnumGroupType.FlowValveLength);
                tvcv++;
                flow++;
            }
            if (0 == flow)
            {
                MessageBoxWin.Show(ReadXaml.GetResources("ME_Msg_ErrorConf"));
                return;
            }
            if (true == chboxFlowRatePer.IsChecked)  
            {
                sb.Append(EnumGroupType.FlowRatePer);
                tvcv++;
            }
            if (true == chboxPHCDUVUntil.IsChecked)
            {
                if (!CheckData(txtpHCdUVUnitUCHeader.Text))
                {
                    MessageBoxWin.Show(ReadXaml.GetResources("ME_Msg_ErrorConf"));
                    return;
                }
                StringBuilderSplit sb1 = new StringBuilderSplit("&");
                sb1.Append(EnumGroupType.PHCDUVUntil);
                sb1.Append(txtpHCdUVUnitUCHeader.Text);
                sb.Append(sb1.ToString());
                tvcv++;
            }
            if (1 < tvcv)
            {
                MessageBoxWin.Show(ReadXaml.GetResources("ME_Msg_ErrorConf"));
                return;
            }
            if (true == chboxCollValveCollector.IsChecked)
            {
                sb.Append(EnumGroupType.CollValveCollector);
            }
            if (true == chboxCIP.IsChecked)
            {
                sb.Append(EnumGroupType.CIP);
            }

            MethodManager methodManager = new MethodManager();
            string error = methodManager.AddPhase(txtName.Text, sb.ToString());
            if (null != error)
            {
                MessageBoxWin.Show(error);
            }
            else
            {
                MessageBoxWin.Show(Share.ReadXaml.GetResources("ME_Desc_Phase_New"));
                DialogResult = true;
            }  
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private bool CheckData(string text)
        {
            if (text.Contains(";") || text.Contains("&"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
