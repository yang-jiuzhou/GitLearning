using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// MethodQueueWin.xaml 的交互逻辑
    /// </summary>
    public partial class MethodQueueWin : Window
    {
        private string m_error = null;
        private MethodQueue m_methodQueue = null;
        private MethodQueue MMethodQueue
        {
            get
            {
                return m_methodQueue;
            }
        }

        private ObservableCollection<MString> m_listSelect = new ObservableCollection<MString>();

        /// <summary>
        /// 自定义事件，添加方法或者方法序列时触发
        /// </summary>
        public static readonly RoutedEvent MAddMethodEvent =
             EventManager.RegisterRoutedEvent("MAddMethod", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MethodEditorWin));
        public event RoutedEventHandler MAddMethod
        {
            add { AddHandler(MAddMethodEvent, value); }
            remove { RemoveHandler(MAddMethodEvent, value); }
        }


        /// <summary>
        /// 构造函数（初始化）
        /// </summary>
        /// <param name="communicationSetsID"></param>
        /// <param name="projectID"></param>
        public MethodQueueWin(Window parent, int communicationSetsID, int projectID, List<MethodType> list)
        {
            InitializeComponent();

            this.Owner = parent;

            listMethod.ItemsSource = list;
            listSelect.ItemsSource = m_listSelect;

            m_methodQueue = new MethodQueue(-1, communicationSetsID, projectID, "");
        }

        /// <summary>
        /// 构造函数（编辑）
        /// </summary>
        /// <param name="id"></param>
        public MethodQueueWin(int id, List<MethodType> list, bool self)
        {
            InitializeComponent();

            listMethod.ItemsSource = list;
            listSelect.ItemsSource = m_listSelect;

            MethodManager manager = new MethodManager();
            m_error = manager.GetMethodQueue(id, out m_methodQueue);

            txtName.Text = m_methodQueue.MName;
            foreach (var it in m_methodQueue.MMethodList)
            {
                foreach (var item in list)
                {
                    if (item.MID == it)
                    {
                        m_listSelect.Add(new MString(item.MName));
                        break;
                    }
                }
            }

            btnOK.IsEnabled = self;
        }

        /// <summary>
        /// 检查数据
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            if (null != m_error)
            {
                Share.MessageBoxWin.Show(m_error);
                return false;
            }

            if (!TextLegal.FileNameLegal(txtName.Text))
            {
                MessageBoxWin.Show(Share.ReadXaml.S_ErrorIllegalName);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (CheckData())
            {
                MMethodQueue.MName = txtName.Text;

                MethodManager manager = new MethodManager();
                if (-1 == MMethodQueue.MID)
                {
                    //新建
                    string error = manager.AddMethodQueue(MMethodQueue);
                    if (null == error)
                    {
                        RoutedEventArgs args = new RoutedEventArgs(MAddMethodEvent, null);
                        RaiseEvent(args);

                        AuditTrails.AuditTrailsStatic.Instance().InsertRowMethod(Share.ReadXaml.GetResources("ME_Desc_Queue_New"), MMethodQueue.MName);
                        Share.MessageBoxWin.Show(ReadXaml.GetResources("ME_Msg_Queue_SaveYes"));
                    }
                    else
                    {
                        Share.MessageBoxWin.Show(error);
                        return;
                    }
                }
                else
                {
                    //修改
                    string error = manager.UpdateMethodQueue(MMethodQueue);
                    if (null == error)
                    {
                        AuditTrails.AuditTrailsStatic.Instance().InsertRowMethod(Share.ReadXaml.GetResources("ME_Desc_Queue_Update"), MMethodQueue.MName);
                        Share.MessageBoxWin.Show(ReadXaml.GetResources("ME_Msg_Queue_UpdateYes"));
                    }
                    else
                    {
                        Share.MessageBoxWin.Show(error);
                        return;
                    }
                }

                Close();
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 添加方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            if (-1 != listMethod.SelectedIndex)
            {
                MMethodQueue.MMethodList.Add(((MethodType)listMethod.SelectedItem).MID);
                m_listSelect.Add(new MString(((MethodType)listMethod.SelectedItem).MName));
            }
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            if (-1 != listSelect.SelectedIndex)
            {
                int temp = listSelect.SelectedIndex; 
                MMethodQueue.MMethodList.RemoveAt(temp);
                m_listSelect.RemoveAt(temp);

                //指定下一个
                listSelect.SelectedIndex = temp;
            }
        }
    }
}
