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

namespace HBBio.Collection
{
    /// <summary>
    /// CollectionObjectSetUC.xaml 的交互逻辑
    /// </summary>
    public partial class CollectionObjectMultiUC : UserControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CollectionObjectMultiUC()
        {
            InitializeComponent();

            cboxRelation.ItemsSource = EnumString<EnumRelation>.GetEnumStringList("Coll_Relation_");
        }

        /// <summary>
        /// 返回收集文本显示
        /// </summary>
        /// <returns></returns>
        public string GetShowInfo()
        {
            StringBuilderSplit sb = new StringBuilderSplit("\n");

            CollectionObjectMultiVM item = this.DataContext as CollectionObjectMultiVM;
            switch (item.MRelation)
            {
                case EnumRelation.Only:
                    sb.Append(labObjOne.Text + ucObject1.GetShowInfo());
                    break;
                default:
                    sb.Append(labObjOne.Text + ucObject1.GetShowInfo());
                    sb.Append(labObjTwo.Text + ucObject2.GetShowInfo());
                    sb.Append(labRelation.Text + ((EnumString<EnumRelation>)cboxRelation.SelectedItem).MString);
                    break;
            }

            return sb.ToString();
        }

        /// <summary>
        /// 加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CollectionObjectMultiVM item = this.DataContext as CollectionObjectMultiVM;
            if (null != item)
            {
                ucObject1.DataContext = item.MObj1;
                ucObject2.DataContext = item.MObj2;
            }
        }
    }
}
