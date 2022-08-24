using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.SystemLog
{
    public class SystemLogManager
    {
        private static volatile int s_count = 0;

        //读写锁，当资源处于写入模式时，其他线程写入需要等待本次写入结束之后才能继续写入
        private static readonly ReaderWriterLockSlim s_logWriteLock = new ReaderWriterLockSlim();

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="ex"></param>
        public static void LogWrite(Exception ex)
        {
            if (++s_count > 1000)
            {
                return;
            }

            var logpath = Database.BaseDB.m_pathError + DateTime.Now.ToString("yyyyMMddHHmmss") + ".log";
            var log = "\r\n----------------------" + DateTime.Now + " --------------------------\r\n"
                      + ex.Message
                      + "\r\n"
                      + ex.InnerException
                      + "\r\n"
                      + ex.StackTrace
                      + "\r\n----------------------footer--------------------------\r\n";
            try
            {
                //设置读写锁为写入模式独占资源，其他写入请求需要等待本次写入结束之后才能继续写入
                s_logWriteLock.EnterWriteLock();
                File.AppendAllText(logpath, log);
            }
            finally
            {
                //退出写入模式，释放资源占用
                s_logWriteLock.ExitWriteLock();
            }
        }
    }
}
