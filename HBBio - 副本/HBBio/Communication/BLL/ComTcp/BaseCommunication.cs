using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HBBio.Communication
{
    /**
     * ClassName: BaseCommunication
     * Description: 通讯单元基类
     * Version: 1.0
     * Create:  2020/05/16
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    public class BaseCommunication
    {
        private ENUMCommunicationState m_communStates = ENUMCommunicationState.Free;    //通信状态
        public ENUMCommunicationState m_communState
        {
            get
            {
                return m_communStates;
            }
            set
            {
                if (m_communStates != value)
                {
                    switch (value)
                    {
                        case ENUMCommunicationState.Free:
                            foreach (var it in m_scInfo.MList)
                            {
                                it.MBrush = Brushes.White;
                            }
                            break;
                        case ENUMCommunicationState.Success:
                            foreach (var it in m_scInfo.MList)
                            {
                                it.MBrush = Brushes.Gray;
                            }
                            break;
                        case ENUMCommunicationState.Error:
                            foreach (var it in m_scInfo.MList)
                            {
                                it.MBrush = Brushes.Red;
                            }
                            break;
                    }
                }

                m_communStates = value;
            }
        }      

        public SolidColorBrush MForeground
        {
            set
            {
                foreach (var it in m_scInfo.MList)
                {
                    it.MForeground = value;
                }
            }
        }

        protected ComConf m_scInfo = null;                          //配置参数
        protected AutoResetEvent m_are = new AutoResetEvent(false);

        protected const int c_timeout = 10;                         //超时休息时间(秒)
        protected Thread m_thread = null;                           //运行线程


        /// <summary>
        /// 属性，配置参数
        /// </summary>
        public virtual ComConf MComConf
        {
            get
            {
                return m_scInfo;
            }
            set
            {
                m_scInfo = value;
            }
        }


        /// <summary>
        /// 开启线程
        /// </summary>
        public void ThreadStart()
        {
            m_thread = new Thread(ThreadRun);
            m_thread.IsBackground = true;
            m_thread.Start();
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public virtual bool Connect()
        {
            return false;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public virtual bool Close()
        {
            return false;
        }

        /// <summary>
        /// 读取版本号
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public virtual bool ReadVersion(ref string version)
        {
            return false;
        }

        /// <summary>
        /// 读取序列号
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public virtual bool ReadSerial(ref string serial)
        {
            return true;
        }

        /// <summary>
        /// 读取Model
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public virtual bool ReadModel(ref string model)
        {
            return true;
        }

        /// <summary>
        /// 线程的状态设置
        /// </summary>
        public virtual void ThreadStatus(ENUMThreadStatus status)
        {
        }

        /// <summary>
        /// 线程主函数
        /// </summary>
        protected virtual void ThreadRun()
        {
        }


        /// <summary>
        /// 获取运行数据数值列表
        /// </summary>
        /// <returns></returns>
        public virtual List<object> GetRunDataValueList()
        {
            return new List<object>();
        }

        /// <summary>
        /// 获取运行数据数值列表
        /// </summary>
        /// <returns></returns>
        public virtual List<object> SetRunDataValueList()
        {
            return new List<object>();
        }
    }

    /// <summary>
    /// 通信的状态
    /// </summary>
    public enum ENUMCommunicationState
    {
        Free,
        Success,
        Error,
        Over
    }

    /// <summary>
    /// 线程的状态
    /// </summary>
    public enum ENUMThreadStatus
    {
        Free,
        Version,
        WriteOrRead,
        Abort
    }
}
