using HBBio.Communication;
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

namespace HBBio.SystemControl
{
    /// <summary>
    /// SystemMonitorWin.xaml 的交互逻辑
    /// </summary>
    public partial class SystemMonitorWin : Window
    {
        public SystemMonitorWin(List<StringWrapper> runDataShowList)
        {
            InitializeComponent();

            dgvRunData.ItemsSource = runDataShowList;
        }

        /// <summary>
        /// 运行数据行的显隐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRunData_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Visibility = SystemControlManager.s_comconfStatic.m_runDataList[e.Row.GetIndex()].MIsShow ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
