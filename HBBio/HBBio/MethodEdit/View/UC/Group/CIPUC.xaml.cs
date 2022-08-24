using HBBio.Communication;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace HBBio.MethodEdit
{
    /// <summary>
    /// CIPUC.xaml 的交互逻辑
    /// </summary>
    public partial class CIPUC : UserControl
    {
        public new object DataContext
        {
            get
            {
                return base.DataContext;
            }
            set
            {
                base.DataContext = value;
                if (null == value)
                {
                    flowRateUC.DataContext = null;
                }
                else
                {
                    flowRateUC.DataContext = ((CIPVM)value).MFlowRate;
                }
            }
        }



        /// <summary>
        /// 构造函数
        /// </summary>
        public CIPUC()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置可见性
        /// </summary>
        /// <param name="inA"></param>
        /// <param name="inB"></param>
        /// <param name="inC"></param>
        /// <param name="inD"></param>
        /// <param name="inS"></param>
        /// <param name="cpv"></param>
        /// <param name="outlet"></param>
        public void SetVisibility(Visibility pumpA, Visibility pumpB, Visibility pumpC, Visibility pumpD, Visibility pumpS
            , Visibility inA, Visibility inB, Visibility inC, Visibility inD, Visibility inS
            , Visibility cpv, Visibility outlet)
        {
            PumpA.Visibility = pumpA;
            InA.Visibility = inA;
            boxInA.Visibility = pumpA;
            chboxInA.Visibility = pumpA;

            PumpB.Visibility = pumpB;
            InB.Visibility = inB;
            boxInB.Visibility = pumpB;
            chboxInB.Visibility = pumpB;

            PumpC.Visibility = pumpC;
            InC.Visibility = inC;
            boxInC.Visibility = pumpC;
            chboxInC.Visibility = pumpC;

            PumpD.Visibility = pumpD;
            InD.Visibility = inD;
            boxInD.Visibility = pumpD;
            chboxInD.Visibility = pumpD;

            PumpS.Visibility = pumpS;
            InS.Visibility = inS;
            boxInS.Visibility = pumpS;
            chboxInS.Visibility = pumpS;

            CPV.Visibility = cpv;
            boxCPV.Visibility = cpv;
            chboxCPV.Visibility = cpv;

            Out.Visibility = outlet;
            boxOut.Visibility = outlet;
            chboxOut.Visibility = outlet;
        }

        private void chboxInA_Click(object sender, RoutedEventArgs e)
        {
            if (true == chboxInA.IsChecked)
            {
                for (int i = 0; i < boxInA.Items.Count; i++)
                {
                    ((CIPItemVM)boxInA.Items[i]).MIsSelected = true;
                }
            }
            else
            {
                for (int i = boxInA.Items.Count - 1; i >= 0; i--)
                {
                    ((CIPItemVM)boxInA.Items[i]).MIsSelected = false;
                }
            }
        }

        private void chboxInB_Click(object sender, RoutedEventArgs e)
        {
            if (true == chboxInB.IsChecked)
            {
                for (int i = 0; i < boxInB.Items.Count; i++)
                {
                    ((CIPItemVM)boxInB.Items[i]).MIsSelected = true;
                }
            }
            else
            {
                for (int i = boxInB.Items.Count - 1; i >= 0; i--)
                {
                    ((CIPItemVM)boxInB.Items[i]).MIsSelected = false;
                }
            }
        }

        private void chboxInC_Click(object sender, RoutedEventArgs e)
        {
            if (true == chboxInC.IsChecked)
            {
                for (int i = 0; i < boxInC.Items.Count; i++)
                {
                    ((CIPItemVM)boxInC.Items[i]).MIsSelected = true;
                }
            }
            else
            {
                for (int i = boxInC.Items.Count - 1; i >= 0; i--)
                {
                    ((CIPItemVM)boxInC.Items[i]).MIsSelected = false;
                }
            }
        }

        private void chboxInD_Click(object sender, RoutedEventArgs e)
        {
            if (true == chboxInD.IsChecked)
            {
                for (int i = 0; i < boxInD.Items.Count; i++)
                {
                    ((CIPItemVM)boxInD.Items[i]).MIsSelected = true;
                }
            }
            else
            {
                for (int i = boxInD.Items.Count - 1; i >= 0; i--)
                {
                    ((CIPItemVM)boxInD.Items[i]).MIsSelected = false;
                }
            }
        }

        private void chboxInS_Click(object sender, RoutedEventArgs e)
        {
            if (true == chboxInS.IsChecked)
            {
                for (int i = 0; i < boxInS.Items.Count; i++)
                {
                    ((CIPItemVM)boxInS.Items[i]).MIsSelected = true;
                }
            }
            else
            {
                for (int i = boxInS.Items.Count - 1; i >= 0; i--)
                {
                    ((CIPItemVM)boxInS.Items[i]).MIsSelected = false;
                }
            }
        }

        private void chboxCPV_Click(object sender, RoutedEventArgs e)
        {
            if (true == chboxCPV.IsChecked)
            {
                for (int i = 0; i < boxCPV.Items.Count; i++)
                {
                    ((CIPItemVM)boxCPV.Items[i]).MIsSelected = true;
                }
            }
            else
            {
                for (int i = boxCPV.Items.Count - 1; i >= 0; i--)
                {
                    ((CIPItemVM)boxCPV.Items[i]).MIsSelected = false;
                }
            }
        }

        private void chboxOut_Click(object sender, RoutedEventArgs e)
        {
            if (true == chboxOut.IsChecked)
            {
                for (int i = 0; i < boxOut.Items.Count; i++)
                {
                    ((CIPItemVM)boxOut.Items[i]).MIsSelected = true;
                }
            }
            else
            {
                for (int i = boxOut.Items.Count - 1; i >= 0; i--)
                {
                    ((CIPItemVM)boxOut.Items[i]).MIsSelected = false;
                }
            }
        }
    }
}
