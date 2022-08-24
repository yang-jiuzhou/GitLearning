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
    /// ASParaUC.xaml 的交互逻辑
    /// </summary>
    public partial class ASManualParaUC : UserControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ASManualParaUC()
        {
            InitializeComponent();

            cboxAction.ItemsSource = EnumString<EnumMonitorActionManual>.GetEnumStringList("EnumMonitorAction_");
            cboxUnit.ItemsSource = EnumBaseString.GetItemsSource();
        }

        /// <summary>
        /// 返回修改信息
        /// </summary>
        /// <returns></returns>
        public string GetLog(ASManualPara value, bool deepCopy)
        {
            if (null == this.DataContext)
            {
                return "";
            }
            else
            {
                Share.StringBuilderSplit sb = new Share.StringBuilderSplit();
                ASManualPara curr = ((ASManualParaVM)this.DataContext).MItem;
                if (curr.MAction != value.MAction)
                {
                    sb.Append(labAction.Text + cboxAction.Text);
                }
                if (curr.MLength != value.MLength || curr.MUnit != value.MUnit)
                {
                    sb.Append(labDelay.Text + doubleLength.Value + cboxUnit.Text);
                }

                if (deepCopy)
                {
                    value.DeepCopy(curr);
                    value.m_update = true;
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
                this.DataContext = new ASManualParaVM(new ASManualPara());
            }
        }
    }
}
