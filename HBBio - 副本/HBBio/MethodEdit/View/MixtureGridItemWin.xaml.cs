using HBBio.Communication;
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
    /// MixtureGridItemWin.xaml 的交互逻辑
    /// </summary>
    public partial class MixtureGridItemWin : Window
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
                if (null != value)
                {
                    int index = 0;
                    for (int i = 0; i < otherWPanel.Children.Count; i++)
                    {
                        if (otherWPanel.Children[i] is ASMethodParaUC)
                        {
                            ((ASMethodParaUC)otherWPanel.Children[i]).DataContext = ((MixtureGridItemVM)value).MASParaList[index++];
                        }
                    }
                }
            }
        }


        public string MLabTVCV
        {
            get
            {
                return labTVCV.Text;
            }
            set
            {
                labTVCV.Text = value;
            }
        }

        public string MLabSampleFlowRate
        {
            get
            {
                return labSampleFlowRate.Text;
            }
            set
            {
                labSampleFlowRate.Text = value;
            }
        }

        public string MLabSystemFlowRate
        {
            get
            {
                return labSystemFlowRate.Text;
            }
            set
            {
                labSystemFlowRate.Text = value;
            }
        }

        public MixtureGridItemWin()
        {
            InitializeComponent();

            int index = 0;
            foreach (ENUMASName it in Enum.GetValues(typeof(ENUMASName)))
            {
                if (Visibility.Visible == ItemVisibility.s_listAS[it])
                {
                    otherWPanel.Children.Insert(index++, new ASMethodParaUC());
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// 设置可见性
        /// </summary>
        /// <param name="inA"></param>
        /// <param name="inB"></param>
        /// <param name="inC"></param>
        /// <param name="inD"></param>
        /// <param name="bpv"></param>
        /// <param name="outlet"></param>
        /// <param name="pumpB"></param>
        /// <param name="pumpC"></param>
        /// <param name="pumpD"></param>
        public void SetVisibility(Visibility pumpS, Visibility pumpA
            , Visibility pumpB, Visibility pumpC, Visibility pumpD
            , Visibility inS, Visibility inA, Visibility inB, Visibility inC, Visibility inD
            , Visibility ijv, Visibility bpv, Visibility cpv, Visibility outlet
            , Visibility mixer, Visibility uv)
        {
            colSampleFlowRate.Visibility = pumpS;
            colSystemFlowRate.Visibility = pumpA;

            colB.Visibility = pumpB;
            colC.Visibility = pumpC;
            colD.Visibility = pumpD;

            colInS.Visibility = inS;
            colInA.Visibility = inA;
            colInB.Visibility = inB;
            colInC.Visibility = inC;
            colInD.Visibility = inD;
            colIJY.Visibility = ijv;
            colBPV.Visibility = bpv;
            colCPV.Visibility = cpv;
            colOut.Visibility = outlet;

            colMixer.Visibility = mixer;
            colUVClear.Visibility = uv;
        }
    }
}
