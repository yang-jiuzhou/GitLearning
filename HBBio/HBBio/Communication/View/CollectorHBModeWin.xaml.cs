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
    /// CollectorHBModeWin.xaml 的交互逻辑
    /// </summary>
    public partial class CollectorHBModeWin : Window
    {
        public ComCollectorHB MItem { get; set; }
        public ENUMCollectorID MMode { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public CollectorHBModeWin()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] arrMode = null;
            switch (MMode)
            {
                case ENUMCollectorID.HB_DLY_W:
                    arrMode = new string[]
                                {"Null",
                                "40 * 15 mL",
                                "24 * 25 mL",
                                "12 * 30 ml"};
                    break;
                case ENUMCollectorID.HB_DLY_B:
                    arrMode = new string[]
                                {"Null",
                                "84 *  2 mL",
                                "60 *  5 mL",
                                "44 *  7 mL",
                                "40 * 10 mL",
                                "40 * 15 mL",
                                "24 * 25 mL",
                                "12 * 50 ml"};
                    break;
            }

            cboxSetL.ItemsSource = (string[])arrMode.Clone();
            cboxSetR.ItemsSource = (string[])arrMode.Clone();
        }

        private void btnGet_Click(object sender, RoutedEventArgs e)
        {
            EnumCollIndexText left = EnumCollIndexText.L;
            int index = 0;
            bool on = false;
            int countL = 0;
            int countR = 0;
            int modeL = 0;
            int modeR = 0;
            double volL = 0;
            double volR = 0;
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
                    if (MItem.ReadStatus(ref left, ref index, ref on, ref countL, ref countR, ref modeL, ref modeR, ref volL, ref volR))
                    {
                        item.MCountL = countL;
                        item.MCountR = countR;
                        item.MModeL = modeL;
                        item.MModeR = modeR;
                        EnumCollectorInfo.Init(Convert.ToInt32(txtGetL.Text), Convert.ToInt32(txtGetR.Text));
                    }
                    MItem.Close();
                }

                MItem.ThreadStatus(ENUMThreadStatus.WriteOrRead);
            }
        }

        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
