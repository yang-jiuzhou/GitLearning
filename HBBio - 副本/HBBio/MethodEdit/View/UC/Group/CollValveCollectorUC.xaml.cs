using HBBio.Collection;
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

namespace HBBio.MethodEdit
{
    /// <summary>
    /// CollValveCollectorUC.xaml 的交互逻辑
    /// </summary>
    public partial class CollValveCollectorUC : UserControl
    {
        public new object DataContext
        {
            get
            {
                return base.DataContext;
            }
            set
            {
                base.DataContext = value;
                if (null != value)
                {
                    collectionUC.MCollectionValve = ((CollValveCollectorVM)value).MValve;
                    collectionUC.MCollectionCollector = ((CollValveCollectorVM)value).MCollector;
                    collectionUC.MType = ((CollValveCollectorVM)value).MEnum;
                }
            }
        }

        public CollValveCollectorUC()
        {
            InitializeComponent();
        }

        public void SetVisibility(Visibility valve, Visibility collector)
        {
            rbtnValve.Visibility = valve;
            rbtnCollector.Visibility = collector;
        }

        private void rbtnUOV_Checked(object sender, RoutedEventArgs e)
        {
            if (null != collectionUC.DataContext)
            {
                collectionUC.MType = EnumCollectionType.Valve;
            }
        }

        private void rbtnUFC_Checked(object sender, RoutedEventArgs e)
        {
            if (null != collectionUC.DataContext)
            {
                collectionUC.MType = EnumCollectionType.Collector;
            }
        }
    }
}
