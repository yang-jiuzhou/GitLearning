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
    /// DefineQuestionsWin.xaml 的交互逻辑
    /// </summary>
    public partial class DefineQuestionsWin : Window
    {
        public DefineQuestionsVM MItem { get; set; }
        private DefineQuestionsVM MItemNew { get; set; }


        public DefineQuestionsWin()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (null == MItem)
            {
                MItem = new DefineQuestionsVM();
            }
            MItemNew = new DefineQuestionsVM();
            MItemNew.MItem = Share.DeepCopy.DeepCopyByXml(MItem.MItem);

            DataContext = MItemNew;
            listChoice.ItemsSource = MItemNew.MChoiceList;
        }

        private void btnAddChoice_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtChoice.Text))
            {
                return;
            }

            MItemNew.Add(txtChoice.Text);

            txtChoice.Text = "";
            listChoice.SelectedIndex = listChoice.Items.Count - 1;
        }

        private void btnDelChoice_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = listChoice.SelectedIndex;

            MItemNew.Delete(selectedIndex);

            if (0 < selectedIndex)
            {
                listChoice.SelectedIndex = selectedIndex - 1;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            StringBuilderSplit sb = new StringBuilderSplit();
            if (MItem.MQuestion != MItemNew.MQuestion)
            {
                sb.Append(labQuestion.Text + MItem.MQuestion + " -> " + MItemNew.MQuestion);
            }
            if (MItem.MType != MItemNew.MType)
            {
                sb.Append(labType.Text + GetRadioButtonContent(MItem.MType) + " -> " + GetRadioButtonContent(MItemNew.MType));
            }

            string log = sb.ToString();
            if (string.IsNullOrEmpty(log))
            {
                DialogResult = false;
            }
            else
            {
                AuditTrails.AuditTrailsStatic.Instance().InsertRowMethod(this.Title, log);

                MItem.MQuestion = MItemNew.MQuestion;
                MItem.MType = MItemNew.MType;
                MItem.MDefaultAnswer = MItemNew.MDefaultAnswer;
                MItem.MMin = MItemNew.MMin;
                MItem.MMax = MItemNew.MMax;
                MItem.Clear();
                foreach(var it in MItemNew.MChoiceList)
                {
                    MItem.Add(it);
                }

                DialogResult = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// 返回单选按钮的文本
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetRadioButtonContent(EnumAnswerType type)
        {
            switch (type)
            {
                case EnumAnswerType.NoAnswer: return rbtnNA.Content.ToString();
                case EnumAnswerType.NumericValue: return rbtnNV.Content.ToString();
                case EnumAnswerType.MultipleChoice: return rbtnMC.Content.ToString();
                default: return rbtnTI.Content.ToString();
            }
        }
    }
}
