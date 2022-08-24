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

namespace HBBio.Collection
{
    /// <summary>
    /// CollectionItemWin.xaml 的交互逻辑
    /// </summary>
    public partial class CollectionItemWin : Window
    {
        public CollectionItem MItem { get; set; }
        private CollectionItem MItemShow { get; set; }

        public List<string> MTubeNameList
        {
            set
            {
                List<MString> tmp = new List<MString>();
                tmp.Add(new MString(ReadXamlCollection.S_Default));
                value.ForEach(i => tmp.Add(new MString(i)));
                cboxPositionStart.ItemsSource = tmp;
            }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        public CollectionItemWin()
        {
            InitializeComponent();

            cboxPositionType.ItemsSource = EnumString<EnumPositionType>.GetEnumStringList("Coll_PositionType_");
        }

        /// <summary>
        /// 返回收集文本显示
        /// </summary>
        /// <returns></returns>
        public string GetShowInfo()
        {
            StringBuilderSplit sb = new StringBuilderSplit("\n");

            CollectionItemVM item = this.DataContext as CollectionItemVM;
            sb.Append(ucObjectMulti1.GetShowInfo());
            sb.Append(labPositionType.Text + cboxPositionType.Text);
            sb.Append(labPositionStart.Text + cboxPositionStart.Text);
            sb.Append(labPositionType.Text + ((EnumString<EnumPositionType>)cboxPositionType.SelectedItem).MString);
            switch (item.MPositionType)
            {
                case EnumPositionType.Loop:
                    sb.Append(ucObjectMulti2.GetShowInfo());
                    break;
            }

            return sb.ToString();
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
                MItem = new CollectionItem();
            }
            MItemShow = Share.DeepCopy.DeepCopyByXml(MItem);

            CollectionItemVM dataContext = new CollectionItemVM(MItemShow);
            this.DataContext = dataContext;
            ucObjectMulti1.DataContext = dataContext.MCond;
            ucObjectMulti2.DataContext = dataContext.MLoop;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            MItemShow.MShowInfo = GetShowInfo();
            MItem = MItemShow;
            
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