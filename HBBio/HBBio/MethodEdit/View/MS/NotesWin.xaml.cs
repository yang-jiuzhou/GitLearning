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
    /// NoteWin.xaml 的交互逻辑
    /// </summary>
    public partial class NotesWin : Window
    {
        //记录
        public string MNote
        {
            get
            {
                return txtNote.Text;
            }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mn"></param>
        public NotesWin(Window parent, string note)
        {
            InitializeComponent();

            this.Owner = parent;

            txtNote.Text = note;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
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
    }
}
