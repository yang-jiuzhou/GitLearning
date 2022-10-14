using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// ValveSelectionUC.xaml 的交互逻辑
    /// </summary>
    public partial class ValveSelectionUC : UserControl
    {
        public static readonly DependencyProperty VisibilityPerProperty = DependencyProperty.Register("VisibilityPer", typeof(Visibility), typeof(ValveSelectionUC), new PropertyMetadata(Visibility.Visible));
        public Visibility VisibilityPer
        {
            get
            {
                return (Visibility)GetValue(VisibilityPerProperty);
            }
            set
            {
                SetValue(VisibilityPerProperty, value);
            }
        }

        public static readonly DependencyProperty VisibilityFillSystemProperty = DependencyProperty.Register("VisibilityFillSystem", typeof(Visibility), typeof(ValveSelectionUC), new PropertyMetadata(Visibility.Visible));
        public Visibility VisibilityFillSystem
        {
            get
            {
                return (Visibility)GetValue(VisibilityFillSystemProperty);
            }
            set
            {
                SetValue(VisibilityFillSystemProperty, value);
            }
        }

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
                    if (((ValveSelectionVM)value).MVisibPer)
                    {
                        grid.ColumnDefinitions[4].Width = GridLength.Auto;
                        grid.ColumnDefinitions[6].Width = GridLength.Auto;
                    }
                    else
                    {
                        grid.ColumnDefinitions[4].Width = new GridLength(0);
                        grid.ColumnDefinitions[6].Width = new GridLength(0);
                    }

                    if (((ValveSelectionVM)value).MVisibWash)
                    {
                        grid.RowDefinitions[12].Height = GridLength.Auto;
                    }
                    else
                    {
                        grid.RowDefinitions[12].Height = new GridLength(0);
                    }
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ValveSelectionUC()
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
        /// <param name="pumpB"></param>
        /// <param name="pumpC"></param>
        /// <param name="pumpD"></param>
        public void SetVisibility(Visibility inA, Visibility inB, Visibility inC, Visibility inD, Visibility bpv
            , Visibility pumpB, Visibility pumpC, Visibility pumpD)
        {
            if (Visibility.Visible == inA || Visibility.Visible == inB || Visibility.Visible == inC || Visibility.Visible == inD || Visibility.Visible == bpv)
            {
                chboxUTSIAIMS.Visibility = Visibility.Visible;
            }
            else
            {
                chboxUTSIAIMS.Visibility = inA;
            }

            labInA.Visibility = inA;
            cboxInA.Visibility = inA;

            labInB.Visibility = inB;
            cboxInB.Visibility = inB;

            labInC.Visibility = inC;
            cboxInC.Visibility = inC;

            labInD.Visibility = inD;
            cboxInD.Visibility = inD;

            labBPV.Visibility = bpv;
            cboxBPV.Visibility = bpv;

            labPerB.Visibility = pumpB;
            doublePerB.Visibility = pumpB;

            labPerC.Visibility = pumpC;
            doublePerC.Visibility = pumpC;

            labPerD.Visibility = pumpD;
            doublePerD.Visibility = pumpD; 
        }
    }
}
