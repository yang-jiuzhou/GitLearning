using HBBio.Collection;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HBBio.Communication
{
    /// <summary>
    /// CollectorQBHModeWin.xaml 的交互逻辑
    /// </summary>
    public partial class CollectorQBHModeWin : Window
    {
        public ComCollectorQBH MItem { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public CollectorQBHModeWin()
        {
            InitializeComponent();

            string[] arrMode = new string[] {"120*15 mL Test Tube",
                "120*15 mL Cent Tube",
                "42*50 mL Test Tube",
                "42*50 mL Cent Tube",
                "120*5 ml Cent Tube"};

            cboxSetL.ItemsSource = (string[])arrMode.Clone();
            cboxSetR.ItemsSource = (string[])arrMode.Clone();
        }

        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            ConfCollectorVM item = (ConfCollectorVM)this.DataContext;

            if (null != MItem)
            {
                MItem.ThreadStatus(ENUMThreadStatus.Free);

                while (ENUMCommunicationState.Free != MItem.m_communState)
                {
                    Thread.Sleep(DlyBase.c_sleep1);
                    DispatcherHelper.DoEvents();
                }

                if (MItem.Connect())
                {
                    if (MItem.WriteIndex(EnumCollIndexText.L, 1))
                    {
                        if (MItem.WriteStyle(cboxSetL.SelectedIndex, cboxSetR.SelectedIndex))
                        {
                            double volL = 0;
                            double volR = 0;
                            switch (item.MModeL)
                            {
                                case 0: item.MCountL = 60; volL = 15; break;
                                case 1: item.MCountL = 60; volL = 15; break;
                                case 2: item.MCountL = 21; volL = 50; break;
                                case 3: item.MCountL = 21; volL = 50; break;
                                case 4: item.MCountL = 60; volL = 5; break;
                            }

                            switch (item.MModeR)
                            {
                                case 0: item.MCountR = 60; volR = 15; break;
                                case 1: item.MCountR = 60; volR = 15; break;
                                case 2: item.MCountR = 21; volR = 50; break;
                                case 3: item.MCountR = 21; volR = 50; break;
                                case 4: item.MCountR = 60; volR = 15; break;
                            }

                            EnumCollectorInfo.Init(item.MCountL, item.MCountR);
                            EnumCollectorInfo.SetBottleCollVol(volL, volR);
                            EnumCollectorInfo.ReSetBottleCollVol();
                        }
                    }
                    MItem.Close();
                }

                MItem.ThreadStatus(ENUMThreadStatus.WriteOrRead);
            }

            this.Close();
        }
    }
}
