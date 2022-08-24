using HBBio.Communication;
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

namespace HBBio.Collection
{
    /// <summary>
    /// CollectionObjectUC.xaml 的交互逻辑
    /// </summary>
    public partial class CollectionObjectUC : UserControl
    {
        /// <summary>
        /// 依赖属性，标题高度
        /// </summary>
        public static readonly DependencyProperty MHeaderProperty = DependencyProperty.Register("MHeader", typeof(GridLength), typeof(CollectionObjectUC), new PropertyMetadata(GridLength.Auto));
        public GridLength MHeader
        {
            get
            {
                return (GridLength)GetValue(MHeaderProperty);
            }
            set
            {
                SetValue(MHeaderProperty, value);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public CollectionObjectUC()
        {
            InitializeComponent();

            List<string> list = Communication.EnumMonitorInfo.NameList.ToList();
            list.Insert(0, Share.ReadXaml.GetResources("labT"));
            list.Insert(1, Share.ReadXaml.GetResources("labV"));
            list.Insert(2, Share.ReadXaml.GetResources("labCV"));
            cboxType.ItemsSource = StringInt.GetItemsSource(list);

            cboxTS.ItemsSource = EnumString<EnumThresholdSlope>.GetEnumStringList("Coll_ThresholdSlope_");
            cboxSlopeJudge.ItemsSource = EnumString<EnumGreaterLess>.GetEnumStringList("Coll_GreaterLess_");
        }

        /// <summary>
        /// 返回收集文本显示
        /// </summary>
        /// <returns></returns>
        public string GetShowInfo()
        {
            StringBuilder sb = new StringBuilder();

            CollectionObjectVM item = this.DataContext as CollectionObjectVM;
            sb.Append("{" + labType.Text + ((StringInt)cboxType.SelectedItem).MName);
            if (item.MType < 3)
            {
                sb.Append(labLength.Text + item.MLength);
                sb.Append(labThresholdBegin.Text + item.MTdB);
                sb.Append(labThresholdEnd.Text + item.MTdE + "}");
            }
            else
            {
                sb.Append(labThresholdSlope.Text + ((EnumString<EnumThresholdSlope>)cboxTS.SelectedItem).MString);
                switch (item.MTS)
                {
                    case EnumThresholdSlope.Threshold:
                        sb.Append(labThresholdBegin.Text + item.MTdB);
                        sb.Append(labThresholdEnd.Text + item.MTdE + "}");
                        break;
                    case EnumThresholdSlope.Slope:
                        sb.Append(labSlopeJudge.Text + ((EnumString<EnumGreaterLess>)cboxSlopeJudge.SelectedItem).MString);
                        sb.Append(labSlope.Text + item.MSlope + "}");
                        break;
                    case EnumThresholdSlope.ThresholdSlope:
                        sb.Append(labThresholdBegin.Text + item.MTdB);
                        sb.Append(labThresholdEnd.Text + item.MTdE);
                        sb.Append(labSlopeJudge.Text + ((EnumString<EnumGreaterLess>)cboxSlopeJudge.SelectedItem).MString);
                        sb.Append(labSlope.Text + item.MSlope + "}");
                        break;
                    case EnumThresholdSlope.Greater:
                    case EnumThresholdSlope.Less:
                        sb.Append(labThresholdBegin.Text + item.MTdB + "}");
                        break;
                }
            }

            return sb.ToString();
        }

        public bool IsAccess()
        {
            if (cboxType.SelectedIndex < 3 && !(doubleTdB.Value <= doubleTdE.Value && doubleTdE.Value <= doubleLength.Value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void cboxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((StringInt)cboxType.SelectedItem).MName.Contains("pH"))
            {
                doubleTdB.Minimum = StaticValue.s_minPH;
                doubleTdB.Maximum = StaticValue.s_maxPH;
                doubleTdE.Minimum = StaticValue.s_minPH;
                doubleTdE.Maximum = StaticValue.s_maxPH;
                labSlopeUnit.Text = DlyBase.SC_PHSLOPEUNIT;
            }
            else if (((StringInt)cboxType.SelectedItem).MName.Contains("Cd"))
            {
                doubleTdB.Minimum = StaticValue.s_minCD;
                doubleTdB.Maximum = StaticValue.s_maxCD;
                doubleTdE.Minimum = StaticValue.s_minCD;
                doubleTdE.Maximum = StaticValue.s_maxCD;
                labSlopeUnit.Text = DlyBase.SC_CDSLOPEUNIT;
            }
            else if (((StringInt)cboxType.SelectedItem).MName.Contains("UV"))
            {
                doubleTdB.Minimum = StaticValue.s_minUV;
                doubleTdB.Maximum = StaticValue.s_maxUV;
                doubleTdE.Minimum = StaticValue.s_minUV;
                doubleTdE.Maximum = StaticValue.s_maxUV;
                labSlopeUnit.Text = DlyBase.SC_UVSLOPEABSUNIT;
            }
            else
            {
                doubleTdB.Minimum = 0;
                doubleTdB.Maximum = DlyBase.MAX;
                doubleTdE.Minimum = 0;
                doubleTdE.Maximum = DlyBase.MAX;
            }
        }
    }
}
