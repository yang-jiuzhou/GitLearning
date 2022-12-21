using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace HBBio.Communication
{
    /**
     * ClassName: BaseCom
     * Description: 串口通讯单元基类
     * Version: 1.0
     * Create:  2020/05/16
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    public class BaseTCP : BaseCommunication
    {
        protected IPEndPoint m_ipAdressPoint = null;                //IP地址和端口
        protected Socket m_socket = null;                           //socket实例
        protected IAsyncResult m_connResult = null;
        protected byte[] m_ReadByte = new byte[256];                //读数据
        protected int m_ReadLen = 0;                                //读数据的长度
        protected byte[] m_WriteByte = new byte[256];               //写数据


        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseTCP(ComConf info)
        {
            MComConf = info;

            if (null != info.MAddress && null != info.MPort)
            {
                try
                {
                    m_ipAdressPoint = new IPEndPoint(IPAddress.Parse(info.MAddress), Convert.ToInt32(info.MPort));
                }
                catch
                {
                    m_ipAdressPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Convert.ToInt32(1038));
                }  
            }
            else
            {
                m_ipAdressPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Convert.ToInt32(1038));
            }
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public override bool Connect()
        {
            try
            {
                if (null == m_socket || !m_socket.Connected)
                {
                    m_socket = new Socket(m_ipAdressPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    m_connResult = m_socket.BeginConnect(m_ipAdressPoint, null, null);
                    m_connResult.AsyncWaitHandle.WaitOne(2000, true);
                    m_socket.SendTimeout = 2000;
                    m_socket.ReceiveTimeout = 2000;
                    //m_socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, -300);
                }

				return m_connResult.IsCompleted;
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 断开
        /// </summary>
        /// <returns></returns>
        public override bool Close()
        {
            try
            {
                if (null != m_socket && m_socket.Connected)
                {
                    m_connResult = m_socket.BeginDisconnect(false, null, null);
                    m_connResult.AsyncWaitHandle.WaitOne(2000, true);
                    if (true == m_connResult.IsCompleted)
                    {
                        m_socket.Dispose();
                        m_socket = null;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 数据写入
        /// </summary>
        /// <returns></returns>
        protected bool write(int length, int sleep = DlyBase.c_sleep1)
        {
            try
            {
                Thread.Sleep(sleep);

                if (0 != m_socket.Send(m_WriteByte, length, SocketFlags.None))//成功
                {
                    return true;
                }
            }
            catch { }

            return false;
        }

        /// <summary>
        /// 从PLC读数据
        /// </summary>
        /// <returns></returns>
        protected bool read(int time = DlyBase.c_sleep1)
        {
            try
            {
                Thread.Sleep(time);

                for (int i = 0; i < m_ReadByte.Length; i++)
                {
                    m_ReadByte[i] = 0;
                }
                m_ReadLen = m_socket.Receive(m_ReadByte, SocketFlags.None);
                if (0 != m_ReadLen)//成功
                {
                    return true;
                }
            }
            catch { }

            return false;
        }
    }
}
