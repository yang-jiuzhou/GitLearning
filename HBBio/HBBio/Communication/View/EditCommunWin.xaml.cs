using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// EditCommunWin.xaml 的交互逻辑
    /// </summary>
    public partial class EditCommunWin : Window
    {
        private CommunicationSets m_communicationsets = null;
        private List<ComConf> m_comconfList = new List<ComConf>();
        private List<BaseInstrument> m_baseinstrumentList = new List<BaseInstrument>();
        private List<InstrumentPoint> m_ipList = new List<InstrumentPoint>();
        private InstrumentSize m_size = null;
        private List<Point> m_listCircle = null;
        private List<Point> m_listColumn = null;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        public EditCommunWin(Window parent)
        {
            InitializeComponent();

            this.Owner = parent;
        }

        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="cs"></param>
        public void InitWin(CommunicationSets cs)
        {
            m_communicationsets = cs;

            systemName.Text = cs.MName;
            systemNote.Text = cs.MNote;

            CommunicationSetsManager csManager = new CommunicationSetsManager();
            if (null == csManager.GetItem(cs, out m_comconfList, out m_ipList, out m_size, out m_listCircle, out m_listColumn))
            {
                SetComConf(m_comconfList);

                foreach (var itCC in m_comconfList)
                {
                    foreach (var itBI in itCC.MList)
                    {
                        if (itBI.MVisible)
                        {
                            m_baseinstrumentList.Add(itBI);
                        }
                    }
                }
                SetInstrument(m_baseinstrumentList);
            }

            switch ((EnumCommunMode)Enum.Parse(typeof(EnumCommunMode), cs.MCommunMode))
            {
                case EnumCommunMode.Com:
                    dgvComConfPortName.Visibility = Visibility.Visible;
                    dgvComConfAddress.Visibility = Visibility.Collapsed;
                    dgvComConfPort.Visibility = Visibility.Collapsed;
                    dgvInstrumentPortName.Visibility = Visibility.Visible;
                    dgvInstrumentAddress.Visibility = Visibility.Collapsed;
                    dgvInstrumentPort.Visibility = Visibility.Collapsed;
                    break;
                case EnumCommunMode.TCP:
                    dgvComConfPortName.Visibility = Visibility.Collapsed;
                    dgvComConfAddress.Visibility = Visibility.Visible;
                    dgvComConfPort.Visibility = Visibility.Visible;
                    dgvInstrumentPortName.Visibility = Visibility.Collapsed;
                    dgvInstrumentAddress.Visibility = Visibility.Visible;
                    dgvInstrumentPort.Visibility = Visibility.Visible;
                    break;
            }
        }

        /// <summary>
        /// 设置配置列表
        /// </summary>
        /// <param name="comconfList"></param>
        private void SetComConf(List<ComConf> comconfList)
        {
            List<string> portNameList = System.IO.Ports.SerialPort.GetPortNames().ToList();
            List<string> listAddress = AddressPort.GetAddressNames().ToList();
            List<string> listPort = AddressPort.GetPortNames().ToList();

            foreach (var it in comconfList)
            {
                if (!portNameList.Contains(it.MPortName) && it.MPortName.Contains("COM"))
                {
                    portNameList.Add(it.MPortName);
                }

                if (!listAddress.Contains(it.MAddress))
                {
                    listAddress.Add(it.MAddress);
                }

                if (!listPort.Contains(it.MPort))
                {
                    listPort.Add(it.MPort);
                }
            }
            dgvComConfPortName.ItemsSource = portNameList;
            dgvComConfAddress.ItemsSource = listAddress;
            dgvComConfPort.ItemsSource = listPort;

            dgvComConf.ItemsSource = comconfList;
        }

        /// <summary>
        /// 设置仪器列表
        /// </summary>
        /// <param name="baseinstrumentList"></param>
        private void SetInstrument(List<BaseInstrument> baseinstrumentList)
        {
            dgvInstrument.ItemsSource = baseinstrumentList;
        }

        /// <summary>
        /// 检测通信
        /// </summary>
        private void ThreadFunConn()
        {
            CommunicationSetsManager csManager = new CommunicationSetsManager();
            foreach (var it in m_comconfList)
            {
                csManager.CheckConn(it);
            }
        }

        /// <summary>
        /// 检测连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var it in m_comconfList)
            {
                it.MResult = "";
            }

            Thread thread = new Thread(ThreadFunConn);
            thread.IsBackground = true;
            thread.Start();
        }

        /// <summary>
        /// 设置流路图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProcessPicture_Click(object sender, RoutedEventArgs e)
        {
            EditProcessPictureWin win = new EditProcessPictureWin(this, m_baseinstrumentList, m_ipList, m_size, m_listCircle, m_listColumn);
            win.ShowDialog();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            m_communicationsets.MName = systemName.Text;
            m_communicationsets.MNote = systemNote.Text;

            CommunicationSetsManager csManager = new CommunicationSetsManager();
            if (null == csManager.EditItem(m_communicationsets, m_comconfList.ToList(), m_baseinstrumentList.ToList(), m_ipList, m_size, m_listCircle, m_listColumn))
            {
                StringBuilderSplit sb = new StringBuilderSplit("\n");
                sb.Append(labSystemName.Text + systemName.Text);
                sb.Append(labNote.Text + systemNote.Text);
                AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, sb.ToString());
                DialogResult = true;
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
    }
}
