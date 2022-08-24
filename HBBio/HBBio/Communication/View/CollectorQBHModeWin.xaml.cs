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
        public TCPCollectorQBH MItemTCP { get; set; }
        public ComCollectorQBH MItemCom { get; set; }


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

            if (null != MItemCom)
            {
                MItemCom.ThreadStatus(ENUMThreadStatus.Free);

                while (ENUMCommunicationState.Free != MItemCom.m_communState)
                {
                    Thread.Sleep(DlyBase.c_sleep1);
                    DispatcherHelper.DoEvents();
                }

                if (MItemCom.Connect())
                {
                    if (MItemCom.WriteIndex(EnumCollIndexText.L, 1))
                    {
                        if (MItemCom.WriteStyle(cboxSetL.SelectedIndex, cboxSetR.SelectedIndex))
                        {
                            switch (item.MModeL)
                            {
                                case 0: item.MCountL = 60; item.MVolL = 15; break;
                                case 1: item.MCountL = 60; item.MVolL = 15; break;
                                case 2: item.MCountL = 21; item.MVolL = 50; break;
                                case 3: item.MCountL = 21; item.MVolL = 50; break;
                                case 4: item.MCountL = 60; item.MVolL = 5; break;
                            }

                            switch (item.MModeR)
                            {
                                case 0: item.MCountR = 60; item.MVolR = 15; break;
                                case 1: item.MCountR = 60; item.MVolR = 15; break;
                                case 2: item.MCountR = 21; item.MVolR = 50; break;
                                case 3: item.MCountR = 21; item.MVolR = 50; break;
                                case 4: item.MCountR = 60; item.MVolR = 15; break;
                            }

                            EnumCollectorInfo.Init(item.MCountL, item.MCountR);
                            EnumCollectorInfo.SetBottleCollVol(item.MVolL, item.MVolR);
                            EnumCollectorInfo.ReSetBottleCollVol();
                        }
                    }
                    MItemCom.Close();
                }

                MItemCom.ThreadStatus(ENUMThreadStatus.WriteOrRead);
            }
            else if (null != MItemTCP)
            {
                MItemTCP.ThreadStatus(ENUMThreadStatus.Free);

                while (ENUMCommunicationState.Free != MItemTCP.m_communState)
                {
                    Thread.Sleep(DlyBase.c_sleep1);
                    DispatcherHelper.DoEvents();
                }

                if (MItemTCP.Connect())
                {
                    if (MItemTCP.WriteIndex(EnumCollIndexText.L, 1))
                    {
                        if (MItemTCP.WriteStyle(cboxSetL.SelectedIndex, cboxSetR.SelectedIndex))
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
                    MItemTCP.Close();
                }

                MItemTCP.ThreadStatus(ENUMThreadStatus.WriteOrRead);
            }

            this.Close();
        }
    }
}
