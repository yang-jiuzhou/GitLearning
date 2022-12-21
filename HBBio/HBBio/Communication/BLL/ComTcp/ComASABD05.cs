using HBBio.Share;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    class ComASABD05 : ComAS
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ComASABD05(ComConf info) : base(info)
        {
            switch (MComConf.MCommunMode)
            {
                case EnumCommunMode.Com:
                    m_serialPort.BaudRate = 115200;
                    break;
                case EnumCommunMode.TCP:
                    break;
            }
        }


        /// <summary>
        /// 线程主函数
        /// </summary>
        protected override void ThreadRun()
        {
            while (true)
            {
                switch (m_state)
                {
                    case ASState.Free:
                        Close();
                        Thread.Sleep(DlyBase.c_sleep10);
                        m_communState = ENUMCommunicationState.Free;
                        break;
                    case ASState.Version:
                        if (Connect())
                        {
                            string tempVersion = null;
                            ReadVersion(ref tempVersion);
                            m_scInfo.MVersion = tempVersion;
                            Close();
                        }
                        m_state = ASState.Free;
                        break;
                    case ASState.First:
                        m_state = ASState.Read;
                        break;
                    case ASState.Read:
                        if (Connect() && ReadSize(ref m_item.m_sizeGet))
                        {
                            m_communState = ENUMCommunicationState.Success;
                        }
                        else
                        {
                            Close();

                            for (int i = 0; i < c_timeout; i++)
                            {
                                if (ASState.Read != m_state)
                                {
                                    break;
                                }
                                else
                                {
                                    m_communState = ENUMCommunicationState.Error;
                                    Thread.Sleep(DlyBase.c_sleep10);
                                }
                            }
                        }
                        break;
                    case ASState.Abort:
                        Close();
                        m_communState = ENUMCommunicationState.Over;
                        return;
                }
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
                switch (MComConf.MCommunMode)
                {
                    case EnumCommunMode.Com:
                        if (!m_serialPort.IsOpen && SerialPort.GetPortNames().Contains(m_serialPort.PortName))
                        {
                            m_serialPort.Open();
                            m_serialPort.DiscardOutBuffer();
                            m_serialPort.DiscardInBuffer();

                            //设置模式后睡眠0.1秒
                            SetModel();
                            Thread.Sleep(DlyBase.c_sleep1);
                        }
                        return m_serialPort.IsOpen;
                    case EnumCommunMode.TCP:
                        if (null == m_socket || !m_socket.Connected)
                        {
                            m_socket = new Socket(m_ipAdressPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                            m_connResult = m_socket.BeginConnect(m_ipAdressPoint, null, null);
                            m_connResult.AsyncWaitHandle.WaitOne(2000, true);
                            m_socket.SendTimeout = 2000;
                            m_socket.ReceiveTimeout = 2000;

                            //设置模式后睡眠0.1秒
                            SetModel();
                            Thread.Sleep(DlyBase.c_sleep1);
                        }
                        return m_connResult.IsCompleted;
                }
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 读版本
        /// </summary>
        /// <returns></returns>
        public override bool ReadVersion(ref string version)
        {
            try
            {
                m_WriteByte[0] = 0xF1;
                m_WriteByte[1] = 0x00;
                m_WriteByte[2] = 0x05;
                m_WriteByte[3] = 0x25;
                m_WriteByte[4] = 0x06;

                if (!write(5) || !read())
                {
                    return false;
                }

                if (0xF1 == m_ReadByte[0] || 0XFE == m_ReadByte[0])//接收读命令返回成功
                {
                    version = Encoding.ASCII.GetString(m_ReadByte, 6, 6);
                    return true;
                }
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 读序列
        /// </summary>
        /// <returns></returns>
        public override bool ReadSerial(ref string serial)
        {
            return true;
        }

        /// <summary>
        /// 读Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override bool ReadModel(ref string model)
        {
            return true;
        }

        /// <summary>
        /// 读气泡大小
        /// </summary>
        /// <returns></returns>
        public bool ReadSize(ref double size)
        {
            try
            {
                m_WriteByte[0] = 0xF1;
                m_WriteByte[1] = 0x00;
                m_WriteByte[2] = 0x05;
                m_WriteByte[3] = 0x23;
                m_WriteByte[4] = 0x0A;

                if (!write(5) || !read(DlyBase.c_sleep2))
                {
                    return false;
                }

                if (0xF1 == m_ReadByte[0])
                {
                    size = m_ReadByte[20] / 2.55;
                    return true;
                }
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// 设置模式
        /// </summary>
        private bool SetModel()
        {
            try
            {
                m_WriteByte[0] = 0xF1;
                m_WriteByte[1] = 0x00;
                m_WriteByte[2] = 0x06;
                m_WriteByte[3] = 0x32;
                m_WriteByte[4] = 0x03;
                m_WriteByte[5] = 0x1A;

                if (write(6))
                {
                    return true;
                }
            }
            catch
            { }

            return false;
        }
    }
}
