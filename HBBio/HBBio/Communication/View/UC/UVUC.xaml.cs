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
    /// UVUC.xaml 的交互逻辑
    /// </summary>
    public partial class UVUC : UserControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UVUC()
        {
            InitializeComponent();

            labWaveLength.Text = "[" + StaticValue.s_minWaveLength + "-" + StaticValue.s_maxWaveLength + "]" + DlyBase.SC_UVWAVEUNIT;
            intUV1.Minimum = StaticValue.s_minWaveLength;
            intUV1.Maximum = StaticValue.s_maxWaveLength;
            chboxEnabledWave2.Visibility = StaticValue.s_waveEnabledVisible2;
            labUV3.Visibility = StaticValue.s_waveVisible3;
            labUV4.Visibility = StaticValue.s_waveVisible4;
        }

        /// <summary>
        /// 返回修改信息
        /// </summary>
        /// <returns></returns>
        public string GetLog(UVValue uvValue, bool deepCopy)
        {
            if (null == this.DataContext)
            {
                return "";
            }
            else
            {
                Share.StringBuilderSplit sb = new Share.StringBuilderSplit();
                UVValue curr = ((UVValueVM)this.DataContext).MItem;
                if (curr.MOnoff != uvValue.MOnoff)
                {
                    sb.Append(curr.MOnoff ? rbtnUVOn.Content : rbtnUVOff.Content);
                }
                if (curr.MClear != uvValue.MClear)
                {
                    sb.Append(chboxUVZero.Content);
                }
                if (curr.MWave1 != uvValue.MWave1)
                {
                    sb.Append(labUV1.Text + uvValue.MWave1 + " -> " + curr.MWave1);
                }
                if (curr.MWave2 != uvValue.MWave2)
                {
                    sb.Append(labUV2.Text + uvValue.MWave2 + " -> " + curr.MWave2);
                }
                else if (curr.MEnabledWave2 != uvValue.MEnabledWave2)
                {
                    sb.Append(labUV2.Text + (curr.MEnabledWave2 ? (0 + " -> " + curr.MWave2) : (uvValue.MWave2 + " -> " + 0)));
                }
                if (curr.MWave3 != uvValue.MWave3)
                {
                    sb.Append(labUV3.Text + uvValue.MWave3 + " -> " + curr.MWave3);
                }
                if (curr.MWave4 != uvValue.MWave4)
                {
                    sb.Append(labUV4.Text + uvValue.MWave4 + " -> " + curr.MWave4);
                }

                if (deepCopy)
                {
                    uvValue.DeepCopy(curr);
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// 加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (null == this.DataContext)
            {
                UVValueVM uvValueVM = new UVValueVM();
                uvValueVM.MItem = new UVValue();

                this.DataContext = uvValueVM;
            }
        }
    }
}
