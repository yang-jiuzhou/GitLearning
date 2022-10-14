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
using System.Windows.Shapes;

namespace HBBio.MethodEdit
{
    /// <summary>
    /// NewMethodWin.xaml 的交互逻辑
    /// </summary>
    public partial class NewMethodWin : Window
    {
        public Method MMethod { get; set; }
        private System.Collections.IEnumerable m_list = null;
        private int m_communicationSetsID = -1;
        private int m_projectID = -1;


        public NewMethodWin(Window parent, System.Collections.IEnumerable list, int communicationSetsID, int projectID)
        {
            InitializeComponent();

            this.Owner = parent;

            m_list = list;
            m_communicationSetsID = communicationSetsID;
            m_projectID = projectID;

            cboxPurification.ItemsSource = Share.ReadXaml.GetEnumList<EnumMethodPurification>("ME_EnumMethodPurification_");
            cboxPurification.SelectedIndex = 0;

            cboxMaintenance.ItemsSource = Share.ReadXaml.GetEnumList<EnumMethodMaintenance>("ME_EnumMethodMaintenance_");
            cboxMaintenance.SelectedIndex = 0;
        }

        private bool CheckData()
        {
            if (!TextLegal.FileNameLegal(txtMethodName.Text))
            {
                MessageBoxWin.Show(Share.ReadXaml.S_ErrorIllegalName);
                return false;
            }

            foreach (MethodType item in m_list)
            {
                if (item.MName.Equals(txtMethodName.Text))
                {
                    Share.MessageBoxWin.Show(Share.ReadXaml.S_ErrorSameName);
                    return false;
                }
            }

            return true;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (CheckData())
            {
                MethodFactory factory = new MethodFactory();
                if (true == rbtnPurification.IsChecked)
                {
                    //选择纯化
                    MMethod = factory.GetMethod((EnumMethodPurification)cboxPurification.SelectedIndex, m_communicationSetsID, m_projectID, txtMethodName.Text);
                }
                else if (true == rbtnMaintenance.IsChecked)
                {
                    //选择保养
                    MMethod = factory.GetMethod((EnumMethodMaintenance)cboxMaintenance.SelectedIndex, m_communicationSetsID, m_projectID, txtMethodName.Text);
                }
                else
                {
                    //选择空
                    MMethod = factory.GetMethod(m_communicationSetsID, m_projectID, txtMethodName.Text);
                }

                MMethod.MMethodSetting.MUserName = Administration.AdministrationStatic.Instance().MUserInfo.MUserName;
                MMethod.MMethodSetting.MAlarmWarning = Share.DeepCopy.DeepCopyByXml(StaticAlarmWarning.SAlarmWarningOriginal);

                DialogResult = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
