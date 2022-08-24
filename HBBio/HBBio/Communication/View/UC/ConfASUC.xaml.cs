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
    /// ConfASUC.xaml 的交互逻辑
    /// </summary>
    public partial class ConfASUC : UserControl
    {
        private ConfAS m_confAS = new ConfAS();
        public ConfAS MConfAS
        {
            get
            {
                return m_confAS;
            }
            set
            {
                m_confAS = value;

                MConfASVM = new ConfASVM(Share.DeepCopy.DeepCopyByXml(value));
                foreach (FrameworkElement it in grid.Children)
                {
                    if (it is TextBlock)
                    {
                        continue;
                    }
                    it.DataContext = MConfASVM;
                }
            }
        }
        private ConfASVM MConfASVM { get; set; }


        public static readonly DependencyProperty MHeaderProperty = DependencyProperty.Register("MHeader", typeof(string), typeof(ConfASUC), new PropertyMetadata(""));
        public string MHeader
        {
            get
            {
                return (string)GetValue(MHeaderProperty);
            }
            set
            {
                SetValue(MHeaderProperty, value);
            }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        public ConfASUC()
        {
            InitializeComponent();

            cboxASDelayUnit.ItemsSource = EnumBaseString.GetItemsSource();
        }

        /// <summary>
        /// 获取审计跟踪对比信息
        /// </summary>
        /// <returns></returns>
        public string GetCompareInfo()
        {
            Share.StringBuilderSplit sb = new Share.StringBuilderSplit("\n");

            if (MConfAS.MSize != MConfASVM.MSize)
            {
                sb.Append(labASMin1.Text + MConfAS.MSize + " -> " + MConfASVM.MSize);
                MConfAS.MSize = MConfASVM.MSize;
            }

            if (MConfAS.MDelayLength != MConfASVM.MDelayLength)
            {
                sb.Append(labASDelayLength1.Text + MConfAS.MDelayLength + " -> " + MConfASVM.MDelayLength);
                MConfAS.MDelayLength = MConfASVM.MDelayLength;
            }

            if (MConfAS.MDelayUnit != MConfASVM.MDelayUnit)
            {
                sb.Append(labASDelayUnit1.Text + EnumBaseInfo.NameList[(int)MConfAS.MDelayUnit] + " -> " + EnumBaseInfo.NameList[(int)MConfASVM.MDelayUnit]);
                MConfAS.MDelayUnit = MConfASVM.MDelayUnit;
            }

            return sb.ToString();
        }
    }
}
