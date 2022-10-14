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
    /// RIUC.xaml 的交互逻辑
    /// </summary>
    public partial class RIUC : UserControl
    {
        /// <summary>
        /// 属性，旧值
        /// </summary>
        public RIValue MRIValueOld { get; set; }
        /// <summary>
        /// 属性，新值
        /// </summary>
        public RIValue MRIValueNew { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public RIUC()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 返回修改信息
        /// </summary>
        /// <returns></returns>
        public string GetLog()
        {
            if (null == MRIValueOld && null == MRIValueNew)
            {
                return "";
            }

            Share.StringBuilderSplit sb = new Share.StringBuilderSplit();
            if (MRIValueNew.MOnoff != MRIValueOld.MOnoff)
            {
                sb.Append(MRIValueNew.MOnoff ? rbtnUVOn.Content : rbtnUVOff.Content);
            }
            if (MRIValueNew.MClear != MRIValueOld.MClear)
            {
                sb.Append(chboxUVZero.Content);
            }
            if (MRIValueNew.MTemperature != MRIValueOld.MTemperature)
            {
                sb.Append(labTemperature.Text + MRIValueOld.MTemperature + " -> " + MRIValueNew.MTemperature);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (null == MRIValueOld && null == MRIValueNew)
            {
                return;
            }
            else if (null != MRIValueOld)
            {
                if (0 == MRIValueOld.MTemperature)
                {
                    MRIValueOld.MTemperature = 30;
                }
                MRIValueNew = MRIValueOld.Clone();
            }

            if (0 == MRIValueNew.MTemperature)
            {
                MRIValueNew.MTemperature = 30;
            }

            foreach (FrameworkElement it in gridRI1.Children)
            {
                if (it is TextBlock)
                {
                    continue;
                }
                it.DataContext = MRIValueNew;
            }
            foreach (FrameworkElement it in gridRI2.Children)
            {
                if (it is TextBlock)
                {
                    continue;
                }
                it.DataContext = MRIValueNew;
            }
        }
    }
}
