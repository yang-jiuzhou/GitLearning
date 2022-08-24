using HBBio.Share;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
    public class BaseCom : BaseCommunication
    {
        protected SerialPort m_serialPort = new SerialPort();       //串口对象
        protected byte[] m_ReadByte = new byte[256];                //读数据
        protected int m_ReadLen = 0;                                //读数据的长度
        protected byte[] m_WriteByte = new byte[256];               //写数据


        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseCom(ComConf info)
        {
            m_serialPort.BaudRate = 9600;
            m_serialPort.DataBits = 8;
            m_serialPort.Parity = Parity.None;
            m_serialPort.StopBits = StopBits.One;

            MComConf = info;

            m_serialPort.PortName = info.MPortName;
            m_serialPort.WriteTimeout = DlyBase.c_sleep10;
            m_serialPort.ReadTimeout = DlyBase.c_sleep20;
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public override bool Connect()
        {
            try
            {
                if (!m_serialPort.IsOpen && SerialPort.GetPortNames().Contains(m_serialPort.PortName))
                {
                    m_serialPort.Open();
                    m_serialPort.DiscardOutBuffer();
                    m_serialPort.DiscardInBuffer();
                }

                return m_serialPort.IsOpen;
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
                m_serialPort.Close();
                return !m_serialPort.IsOpen;
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

                m_serialPort.Write(m_WriteByte, 0, length);

                return true;
            }
            catch { }

            return false;
        }

        /// <summary>
        /// 读数据
        /// </summary>
        /// <param name="time"></param>
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
                m_ReadLen = m_serialPort.Read(m_ReadByte, 0, m_ReadByte.Length);
                if (0 != m_ReadLen)//成功
                {
                    return true;
                }
            }
            catch { }

            return false;
        }

        /// <summary>
        /// 读数据
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        protected bool readLine(int time = DlyBase.c_sleep1)
        {
            try
            {
                Thread.Sleep(time);

                for (int i = 0; i < m_ReadByte.Length; i++)
                {
                    m_ReadByte[i] = 0;
                }

                byte[] arrByte = System.Text.Encoding.Default.GetBytes(m_serialPort.ReadLine() + "\n");
                m_ReadLen = arrByte.Length;
                for (int i = 0; i < m_ReadLen; i++)
                {
                    m_ReadByte[i] = arrByte[i];
                }
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
