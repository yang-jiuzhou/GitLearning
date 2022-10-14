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
using System.Threading;
using System.IO.Ports;
using System.Collections.ObjectModel;
using HBBio.Share;

namespace HBBio.Communication
{
    /// <summary>
    /// NewCommunWin.xaml 的交互逻辑
    /// </summary>
    public partial class NewCommunWin : Window
    {
        private string m_leftName = null;
        private string m_leftModel = null;
        private string m_rightName = null;
        private string m_rightModel = null;
        private InstrumentCallBack m_tws = null;
        private ObservableCollection<ComConf> m_comconfList = new ObservableCollection<ComConf>();
        private ObservableCollection<BaseInstrument> m_baseinstrumentList = new ObservableCollection<BaseInstrument>();
        private List<string> MListAddress { get; set; }
        private List<string> MListPort { get; set; }
        private List<AddressPort> m_listAddressPort = new List<AddressPort>();


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        public NewCommunWin(Window parent)
        {
            InitializeComponent();

            this.Owner = parent;

            MListAddress = AddressPort.GetAddressNames().ToList();
            MListPort = AddressPort.GetPortNames().ToList();
            m_listAddressPort.Clear();
            foreach (var itAddress in MListAddress)
            {
                foreach (var itPort in MListPort)
                {
                    m_listAddressPort.Add(new AddressPort() { MAddress = itAddress, MPort = itPort });
                }
            }

            cboxCommunMode.ItemsSource = Enum.GetNames(typeof(EnumCommunMode));
            cboxCommunMode.SelectedIndex = 0;

            UpdateCurrTab(0);
        }

        /// <summary>
        /// 上一步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (0 < tab.SelectedIndex)
            {
                tab.SelectedIndex--;
                UpdateCurrTab(tab.SelectedIndex);
            }
        }

        /// <summary>
        /// 下一步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (1 == tab.SelectedIndex)
            {
                if (string.IsNullOrWhiteSpace(systemName.Text))
                {
                    Share.MessageBoxWin.Show(Share.ReadXaml.S_ErrorIllegalName);
                    return;
                }
            }

            if (tab.SelectedIndex < tab.Items.Count - 1)
            {
                tab.SelectedIndex++;
                UpdateCurrTab(tab.SelectedIndex);
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            List<string> listConstName = new List<string>();
            foreach (var it in m_baseinstrumentList)
            {
                switch (it.MModel)
                {
                    case "HB2":
                    case "VICI4":
                    case "VICI6":
                    case "VICI8":
                    case "VICI10":
                        if (it.MConstName.Equals("IJV") || it.MConstName.Equals("BPV"))
                        {
                            MessageBoxWin.Show(it.MName + " - " + it.MModel + " - " + it.MConstName + "-" + Share.ReadXaml.GetResources("Com_Msg_ConfigIllegal"));
                            return;
                        }
                        break;
                    case "VICI4_413":
                    case "VICI4_342":
                        if (!it.MConstName.Equals("BPV"))
                        {
                            MessageBoxWin.Show(it.MName + " - " + it.MModel + " - " + it.MConstName + "-" + Share.ReadXaml.GetResources("Com_Msg_ConfigIllegal"));
                            return;
                        }
                        break;
                    case "VICI6_AB":
                        if (!it.MConstName.Equals("IJV"))
                        {
                            MessageBoxWin.Show(it.MName + " - " + it.MModel + " - " + it.MConstName + "-" + Share.ReadXaml.GetResources("Com_Msg_ConfigIllegal"));
                            return;
                        }
                        break;
                }

                if (it.MVisible)
                {
                    if (listConstName.Contains(it.MConstName))
                    {
                        MessageBoxWin.Show(it.MName + " - " + it.MConstName);
                        return;
                    }

                    listConstName.Add(it.MConstName);
                } 
            }

            CommunicationSets cs = new CommunicationSets();
            cs.MName = systemName.Text;
            cs.MCommunMode = cboxCommunMode.Text;
            cs.MNote = systemNote.Text;

            CommunicationSetsManager csManager = new CommunicationSetsManager();
            string error = csManager.AddItem(cs, m_comconfList.ToList());
            if (null == error)
            {
                StringBuilderSplit sb = new StringBuilderSplit("\n");
                sb.Append(labSystemName.Text + systemName.Text);
                sb.Append(labNote.Text + systemNote.Text);
                sb.Append(cboxCommunMode.Text);
                AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, sb.ToString());
                DialogResult = true;
            }
            else
            {
                Share.MessageBoxWin.Show(error);
            }
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

        /// <summary>
        /// 加载当前TAB项
        /// </summary>
        /// <param name="index"></param>
        private void UpdateCurrTab(int index)
        {
            switch (index)
            {
                case 0:
                    btnPrev.IsEnabled = false;
                    btnNext.IsEnabled = true;
                    btnOK.IsEnabled = false;
                    SelectTab0();
                    break;
                case 1:
                    btnPrev.IsEnabled = true;
                    btnNext.IsEnabled = true;
                    btnOK.IsEnabled = false;
                    SelectTab1();
                    break;
                case 2:
                    btnPrev.IsEnabled = true;
                    btnNext.IsEnabled = false;
                    btnOK.IsEnabled = true;
                    SelectTab2();
                    break;
            }
        }

        /// <summary>
        /// 选择仪器TAB
        /// </summary>
        private void SelectTab0()
        {
            if (0 < stackpanel.Children.Count)
            {
                //只需要第一次加载生成内容
                return;
            }

            List<string> strList = Share.ReadXaml.GetEnumList<ENUMInstrumentType>("Com_");
            foreach (ENUMInstrumentType type in Enum.GetValues(typeof(ENUMInstrumentType)))
            {
                Expander ep = new Expander();
                ep.Header = strList[(int)type];
                stackpanel.Children.Add(ep);

                ListBox list = new ListBox();
                
                list.Name = type.ToString();
                ListBoxItem item = new ListBoxItem();
                item.Style = null;
                list.BorderBrush = null;
                list.GotFocus += new System.Windows.RoutedEventHandler(this.ListBoxLeft_GotFocus);
                list.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(ListBoxLeft_SelectionChanged);
                ep.Content = list;

                switch (type)
                {
                    case ENUMInstrumentType.Sampler:
                        list.ItemsSource = Enum.GetNames(typeof(ENUMSamplerID));
                        break;
                    case ENUMInstrumentType.Valve:
                        list.ItemsSource = Enum.GetNames(typeof(ENUMValveID));
                        break;
                    case ENUMInstrumentType.Pump:
                        list.ItemsSource = Enum.GetNames(typeof(ENUMPumpID));
                        break;
                    case ENUMInstrumentType.Detector:
                        list.ItemsSource = Enum.GetNames(typeof(ENUMDetectorID));
                        break;
                    case ENUMInstrumentType.Collector:
                        list.ItemsSource = Enum.GetNames(typeof(ENUMCollectorID));
                        break;
                    case ENUMInstrumentType.Other:
                        list.ItemsSource = Enum.GetNames(typeof(ENUMOtherID));
                        break;
                }

                Expander ep2 = new Expander();
                ep2.Header = ep.Header;
                ep2.Visibility = Visibility.Collapsed;
                stackpanel2.Children.Add(ep2);

                ListBox list2 = new ListBox();
                list2.Name = ep2.Header.ToString().Replace(" ","");
                list2.BorderBrush = null;
                list2.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(ListBoxRight_SelectionChanged);
                ep2.Content = list2;
            }
        }

        /// <summary>
        /// 列表鼠标滑动处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollviewer = sender as ScrollViewer;
            if (e.Delta > 0)
                scrollviewer.LineUp();
            else
                scrollviewer.LineDown();
            e.Handled = true;
        }

        /// <summary>
        /// 可选列表失去焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBoxLeft_GotFocus(object sender, RoutedEventArgs e)
        {
            if (-1 != ((ListBox)sender).SelectedIndex)
            {
                m_leftName = ((ListBox)sender).Name;
                m_leftModel = ((ListBox)sender).SelectedItem.ToString();
            }
        }

        /// <summary>
        /// 可选列表中选择项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBoxLeft_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (-1 != ((ListBox)sender).SelectedIndex)
            {
                m_leftName = ((ListBox)sender).Name;
                m_leftModel = ((ListBox)sender).SelectedItem.ToString();
            }
        }

        /// <summary>
        /// 已选列表中选择项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBoxRight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (-1 != ((ListBox)sender).SelectedIndex)
            {
                m_rightName = ((ListBox)sender).Name;
                m_rightModel = ((ListBox)sender).SelectedItem.ToString();
            }
        }

        /// <summary>
        /// 是否显示地址设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboxCommunMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnAddressPort.Visibility = 0 == cboxCommunMode.SelectedIndex ? Visibility.Hidden : Visibility.Visible;
        }

        /// <summary>
        /// 地址设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddressPort_Click(object sender, RoutedEventArgs e)
        {
            AddressPortWin win = new AddressPortWin();
            win.Owner = this;
            MListAddress.ForEach(i => win.MListAddress.Add(new MString(i)));
            MListPort.ForEach(i => win.MListPort.Add(new MString(i)));
            if (true == win.ShowDialog())
            {
                MListAddress.Clear();
                foreach (var it in win.MListAddress)
                {
                    MListAddress.Add(it.MName);
                }
                MListPort.Clear();
                foreach (var it in win.MListPort)
                {
                    MListPort.Add(it.MName);
                }
                m_listAddressPort.Clear();
                foreach (var itAddress in win.MListAddress)
                {
                    foreach (var itPort in win.MListPort)
                    {
                        m_listAddressPort.Add(new AddressPort() { MAddress = itAddress.MName, MPort = itPort.MName });
                    }
                }
            }
        }

        /// <summary>
        /// 自动扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAutoSearch_Click(object sender, RoutedEventArgs e)
        {
            m_comconfList.Clear();

            if (null == m_tws)
            {
                btnAutoSearch.Visibility = Visibility.Hidden;
                btnAutoStop.Visibility = Visibility.Visible;
                labAuto.Visibility = Visibility.Visible;
                ClearItem();

                m_tws = new InstrumentCallBack((EnumCommunMode)cboxCommunMode.SelectedIndex, m_listAddressPort, ResultCallBack);
                Thread th = new Thread(m_tws.ThreadProc);
                th.Start();
            }
        }

        /// <summary>
        /// 停止扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAutoStop_Click(object sender, RoutedEventArgs e)
        {
            m_tws.MRun = false;
        }

        /// <summary>
        /// 增加仪器到已选列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(m_leftName))
            {
                ComConf cc = new ComConf();
                cc.MType = (ENUMInstrumentType)Enum.Parse(typeof(ENUMInstrumentType), m_leftName);
                cc.MModel = m_leftModel;

                AddItem(cc);
            }
        }

        /// <summary>
        /// 已选列表中删除仪器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            DelItem(m_rightName, m_rightModel);
            m_rightName = null;
        }

        /// <summary>
        /// 选择仪器TAB-自动搜索
        /// </summary>
        private void ResultCallBack(List<ComConf> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].MResult.Contains(Share.ReadXaml.S_SuccessTxt))
                {
                    continue;
                }

                this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new System.Windows.Forms.MethodInvoker(delegate ()
                {
                    AddItem(list[i]);
                }));
            }

            m_tws = null;

            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new System.Windows.Forms.MethodInvoker(delegate ()
            {
                btnAutoSearch.Visibility = Visibility.Visible;
                btnAutoStop.Visibility = Visibility.Hidden;
                labAuto.Visibility = Visibility.Hidden;
            }));
        }

        /// <summary>
        /// 选择仪器TAB-增加
        /// </summary>
        private void AddItem(ComConf item)
        {
            Expander ep = (Expander)stackpanel2.Children[(int)item.MType];
            ep.Visibility = Visibility.Visible;
            ep.IsExpanded = true;

            ListBox box = ep.Content as ListBox;
            box.Items.Add(item.MModel);

            bool isInsert = false;
            for (int i = 0; i < m_comconfList.Count; i++)
            {
                if (m_comconfList[i].MType > item.MType)
                {
                    m_comconfList.Insert(i, item);
                    isInsert = true;
                    break;
                }
            }
            if (!isInsert)
            {
                m_comconfList.Add(item);
            }
        }

        /// <summary>
        /// 选择仪器TAB-删除
        /// </summary>
        private void DelItem(string name, string model)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            foreach (Expander ep in stackpanel2.Children)
            {
                if (ep.Header.Equals(name))
                {
                    ListBox box = ep.Content as ListBox;
                    if (null != box)
                    {
                        for (int i = 0; i < box.Items.Count; i++)
                        {
                            if (box.Items[i].Equals(model))
                            {
                                box.Items.RemoveAt(box.SelectedIndex);
                                for (int j = 0; j < m_comconfList.Count; j++)
                                {
                                    if (m_comconfList[j].MName.Equals(name) && m_comconfList[j].MModel.Equals(model))
                                    {
                                        m_comconfList.RemoveAt(j);
                                        break;
                                    }
                                }
                                if (0 == box.Items.Count)
                                {
                                    ep.Visibility = Visibility.Collapsed;
                                }
                                return;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 选择仪器TAB-清除
        /// </summary>
        private void ClearItem()
        {
            foreach (Expander ep in stackpanel2.Children)
            {
                ListBox box = ep.Content as ListBox;
                if (null != box)
                {
                    box.Items.Clear();
                    ep.Visibility = Visibility.Collapsed;
                }
            }

            m_comconfList.Clear();
        }

        /// <summary>
        /// 选中仪器列表TAB
        /// </summary>
        private void SelectTab1()
        {
            foreach (var it in m_comconfList)
            {
                it.MCommunMode = Visibility.Visible == btnAddressPort.Visibility ? EnumCommunMode.TCP : EnumCommunMode.Com;
            }

            if (string.IsNullOrEmpty(systemName.Text))
            {
                CommunicationSetsManager csManager = new CommunicationSetsManager();
                systemName.Text = csManager.GetNewName();
            }

            List<string> portNameList = SerialPort.GetPortNames().ToList();
            foreach (var it in m_comconfList)
            {
                if (!portNameList.Contains(it.MPortName) && it.MPortName.Contains("COM"))
                {
                    portNameList.Add(it.MPortName);
                }
            }
            dgvComConfPortName.ItemsSource = portNameList;

            dgvComConfAddress.ItemsSource = MListAddress;
            dgvComConfPort.ItemsSource = MListPort;

            dgvComConf.ItemsSource = m_comconfList;

            switch ((EnumCommunMode)cboxCommunMode.SelectedIndex)
            {
                case EnumCommunMode.Com:
                    dgvComConfPortName.Visibility = Visibility.Visible;
                    dgvComConfAddress.Visibility = Visibility.Collapsed;
                    dgvComConfPort.Visibility = Visibility.Collapsed;
                    break;
                case EnumCommunMode.TCP:
                    dgvComConfPortName.Visibility = Visibility.Collapsed;
                    dgvComConfAddress.Visibility = Visibility.Visible;
                    dgvComConfPort.Visibility = Visibility.Visible;
                    break;
            }
        }

        /// <summary>
        /// 选中仪器列表TAB-检测通信状态
        /// </summary>
        private void btnConn_Click(object sender, RoutedEventArgs e)
        {
            btnConn.IsEnabled = false;
            foreach (var it in m_comconfList)
            {
                it.MResult = "";
            }

            Thread thread = new Thread(ThreadFunConn);
            thread.IsBackground = true;
            thread.Start();
        }

        /// <summary>
        /// 检测通信
        /// </summary>
        private void ThreadFunConn()
        {
            List<string> listCom = SerialPort.GetPortNames().ToList();
            List<AddressPort> listAddressPort = new List<AddressPort>();
            foreach (var it in m_listAddressPort)
            {
                listAddressPort.Add(it.Clone());
            }

            foreach (var it in m_comconfList)
            {
                switch (it.MCommunMode)
                {
                    case EnumCommunMode.Com:
                        listCom.Remove(it.MPortName);
                        break;
                    case EnumCommunMode.TCP:
                        for (int i = 0; i < listAddressPort.Count; i++)
                        {
                            if (listAddressPort[i].MAddress.Equals(it.MAddress) && listAddressPort[i].MPort.Equals(it.MPort))
                            {
                                listAddressPort.RemoveAt(i);
                                break;
                            }
                        }
                        break;
                }
            }

            CommunicationSetsManager csManager = new CommunicationSetsManager();
            foreach (var it in m_comconfList)
            {
                switch (it.MCommunMode)
                {
                    case EnumCommunMode.Com:
                        if (!it.MPortName.Equals("null"))
                        {
                            csManager.CheckConn(it);
                        }
                        else
                        {
                            for (int i = 0; i < listCom.Count; i++)
                            {
                                it.MPortName = listCom[i];
                                if (csManager.MatchConn(it))
                                {
                                    listCom.RemoveAt(i);
                                    break;
                                }
                            }
                        }
                        break;
                    case EnumCommunMode.TCP:
                        if (!it.MAddress.Equals("null") && !it.MPort.Equals("null"))
                        {
                            csManager.CheckConn(it);
                        }
                        else
                        {
                            for (int i = 0; i < listAddressPort.Count; i++)
                            {
                                it.MAddress = listAddressPort[i].MAddress;
                                it.MPort = listAddressPort[i].MPort;
                                if (csManager.MatchConn(it))
                                {
                                    listAddressPort.RemoveAt(i);
                                    break;
                                }
                            }
                        }
                        break;
                }
            }

            this.btnConn.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new System.Windows.Forms.MethodInvoker(delegate ()
            {
                btnConn.IsEnabled = true;
            }));
        }

        /// <summary>
        /// 仪器元素列表TAB
        /// </summary>
        private void SelectTab2()
        {
            CommunicationSetsManager csManager = new CommunicationSetsManager();
            m_baseinstrumentList.Clear();
            foreach (var it1 in m_comconfList)
            {
                csManager.CreateInstrumentList(it1);
                foreach (var it2 in it1.MList)
                {
                    m_baseinstrumentList.Add(it2);
                }
            }

            dgvInstrument.ItemsSource = m_baseinstrumentList;

            switch ((EnumCommunMode)cboxCommunMode.SelectedIndex)
            {
                case EnumCommunMode.Com:
                    dgvInstrumentPortName.Visibility = Visibility.Visible;
                    dgvInstrumentAddress.Visibility = Visibility.Collapsed;
                    dgvInstrumentPort.Visibility = Visibility.Collapsed;
                    break;
                case EnumCommunMode.TCP:
                    dgvInstrumentPortName.Visibility = Visibility.Collapsed;
                    dgvInstrumentAddress.Visibility = Visibility.Visible;
                    dgvInstrumentPort.Visibility = Visibility.Visible;
                    break;
            }
        }

        /// <summary>
        /// 仪器元素列表TAB-删除多余仪器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (-1 != dgvInstrument.SelectedIndex)
            {
                m_baseinstrumentList[dgvInstrument.SelectedIndex].MVisible = false;
                DataGridRow item = dgvInstrument.ItemContainerGenerator.ContainerFromItem(dgvInstrument.SelectedItem) as DataGridRow;
                item.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 控制显隐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvInstrument_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Visibility = m_baseinstrumentList[e.Row.GetIndex()].MVisible ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}