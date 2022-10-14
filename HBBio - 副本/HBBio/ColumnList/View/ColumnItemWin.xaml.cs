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

namespace HBBio.ColumnList
{
    /// <summary>
    /// ColumnItemWin.xaml 的交互逻辑
    /// </summary>
    public partial class ColumnItemWin : Window
    {
        public new object DataContext
        {
            set
            {
                ColumnItem item = (ColumnItem)value;
                txtName.DataContext = item;
                txtNote.DataContext = item;
                txtUser.DataContext = item;
                dgvRunParameters.ItemsSource = item.MRP.MList;
                dgvDetails.ItemsSource = item.MDT.MList;
            }
        }

        public ColumnItemWin()
        {
            InitializeComponent();
        }
    }
}
