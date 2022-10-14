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
    /// MixerUC.xaml 的交互逻辑
    /// </summary>
    public partial class MixerUC : UserControl
    {
        public MixerUC()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 返回修改信息
        /// </summary>
        /// <returns></returns>
        public string GetLog(MixerValue value, bool deepCopy)
        {
            if (null == this.DataContext)
            {
                return "";
            }
            else
            {
                Share.StringBuilderSplit sb = new Share.StringBuilderSplit();
                MixerValue curr = ((MixerValueVM)this.DataContext).MItem;
                if (curr.MOnoff != value.MOnoff)
                {
                    sb.Append(labMixer.Text + (curr.MOnoff ? rbtnOn.Content.ToString() : rbtnOff.Content.ToString()));
                }

                if (deepCopy)
                {
                    value.DeepCopy(curr);
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
                MixerValueVM vm = new MixerValueVM();
                vm.MItem = new MixerValue();

                this.DataContext = vm;
            }
        }
    }
}
