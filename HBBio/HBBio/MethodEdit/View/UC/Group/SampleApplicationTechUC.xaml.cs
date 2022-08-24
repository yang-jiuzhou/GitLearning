﻿using HBBio.Share;
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
    /// SampleApplicationTechUC.xaml 的交互逻辑
    /// </summary>
    public partial class SampleApplicationTechUC : UserControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SampleApplicationTechUC()
        {
            InitializeComponent();
        }

        public void SetVisibility(Visibility inS)
        {
            if (Visibility.Visible != inS)
            {
                grid.RowDefinitions[10].Height = new GridLength(0);
            }
        }
    }
}
