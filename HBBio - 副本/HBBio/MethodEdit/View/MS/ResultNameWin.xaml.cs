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
    /// ResultWin.xaml 的交互逻辑
    /// </summary>
    public partial class ResultNameWin : Window
    {
        public ResultNameVM MItem { get; set; }
        private ResultNameVM MItemNew { get; set; }



        /// <summary>
        /// 构造函数
        /// </summary>
        public ResultNameWin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (null == MItem)
            {
                MItem = new ResultNameVM();
            }
            MItemNew = new ResultNameVM();
            MItemNew.MItem = Share.DeepCopy.DeepCopyByXml(MItem.MItem);

            foreach (FrameworkElement it in grid.Children)
            {
                if (it is TextBlock)
                {
                    continue;
                }
                it.DataContext = MItemNew;
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (true == rbtnName.IsChecked)
            {
                if (!Share.TextLegal.FileNameLegal(txtName.Text))
                {
                    MessageBoxWin.Show(Share.ReadXaml.S_ErrorIllegalName);
                    return;
                }
            }

            StringBuilderSplit sb = new StringBuilderSplit();
            if (MItem.MType != MItemNew.MType)
            {
                sb.Append(labType.Text + GetRadioButtonContent(MItem.MType) + " -> " + GetRadioButtonContent(MItemNew.MType));
            }
            if (MItem.MDlyName != MItemNew.MDlyName)
            {
                sb.Append(rbtnName.Content.ToString() + MItem.MDlyName + " -> " + MItemNew.MDlyName);
            }
            if (MItem.MUnique != MItemNew.MUnique)
            {
                if (MItemNew.MUnique)
                {
                    sb.Append(chboxAUI.Content.ToString() + ":" + ReadXaml.S_Disabled + " -> " + ReadXaml.S_Enabled);
                }
                else
                {
                    sb.Append(chboxAUI.Content.ToString() + ":" + ReadXaml.S_Enabled + " -> " + ReadXaml.S_Disabled);
                }
            }
            string log = sb.ToString();
            if (string.IsNullOrEmpty(log))
            {
                DialogResult = false;
            }
            else
            {
                AuditTrails.AuditTrailsStatic.Instance().InsertRowMethod(this.Title, log);

                MItem.MType = MItemNew.MType;
                MItem.MDlyName = MItemNew.MDlyName;
                MItem.MUnique = MItemNew.MUnique;

                DialogResult = true;
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// 返回单选按钮的文本
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetRadioButtonContent(EnumResultType type)
        {
            switch (type)
            {
                case EnumResultType.DlyName: return rbtnName.Content.ToString();
                case EnumResultType.MethodName: return rbtnMethodName.Content.ToString();
                case EnumResultType.DateTime: return rbtnDate.Content.ToString();
                default: return rbtnNoName.Content.ToString();
            }
        }
    }
}