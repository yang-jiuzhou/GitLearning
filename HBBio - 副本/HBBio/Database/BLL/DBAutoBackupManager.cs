using HBBio.Share;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.Database
{
    public class DBAutoBackupManager
    {
        //创建一个自定义委托，用于发送日志
        public delegate void AuditTrailsEventHandler(object type, object desc, object oper);
        //声明一个谱图数据事件
        public AuditTrailsEventHandler MAuditTrailsEvent;

        /// <summary>
        /// 当前是否可以自动备份
        /// </summary>
        public bool MAutoEnable { get; set; }
        /// <summary>
        /// 当前是否正在自动备份
        /// </summary>
        public bool MAutoIng { get; set; }

        /// <summary>
        /// 内部类
        /// </summary>
        private static class DBAutoBackupManagerInner
        {
            public static DBAutoBackupManager _stance = new DBAutoBackupManager();
        }

        /// <summary>
        /// 单例引用
        /// </summary>
        /// <returns></returns>
        public static DBAutoBackupManager GetInstance()
        {
            return DBAutoBackupManagerInner._stance;
        }

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private DBAutoBackupManager()
        {
            MAutoEnable = false;
            MAutoIng = false;

            Thread thread = new Thread(ThreadDBAutoBackupFun);
            thread.IsBackground = true;
            thread.Start();
        }

        /// <summary>
        /// 自动备份的线程函数
        /// </summary>
        private void ThreadDBAutoBackupFun()
        {
            DBManager manager = new DBManager();
            DBAutoBackupInfo info = null;
            if (null == manager.GetDBAutoBackup(out info))
            {
                while (true)
                {
                    try
                    {
                        if (info.MEnabled)
                        {
                            #region 本地
                            if (info.MLocal)
                            {
                                DirectoryInfo dir = new DirectoryInfo(info.MPathLocal);
                                DirectoryInfo[] arrDir = dir.GetDirectories("*Auto");
                                List<DateTime> listDate = new List<DateTime>();
                                foreach (var it in arrDir)
                                {
                                    listDate.Add(DateTime.ParseExact(it.Name.Replace("Auto", ""), "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture));
                                }
                                if (0 == arrDir.Length)
                                {
                                    if (MAutoEnable)
                                    {
                                        DBBackupLocal(info.MPathLocal);
                                    }
                                }
                                else
                                {
                                    while (listDate.Count > info.MCount)
                                    {
                                        Directory.Delete(info.MPathLocal + "/" + listDate.Min().ToString("yyyyMMddHHmmss") + "Auto", true);
                                        listDate.Remove(listDate.Min());
                                    }

                                    DateTime max = listDate.Max();
                                    if ((DateTime.Now - max).TotalDays > info.MFrequency)
                                    {
                                        if (MAutoEnable)
                                        {
                                            DBBackupLocal(info.MPathLocal);
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region 远程
                            if (info.MRemote)
                            {
                                DirectoryInfo dir = new DirectoryInfo("\\\\" + info.MIP + "\\" + info.MPathRemote);
                                if (dir.Exists)
                                {
                                    DirectoryInfo[] arrDir = dir.GetDirectories("*Auto");
                                    List<DateTime> listDate = new List<DateTime>();
                                    foreach (var it in arrDir)
                                    {
                                        listDate.Add(DateTime.ParseExact(it.Name.Replace("Auto", ""), "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture));
                                    }
                                    if (0 == arrDir.Length)
                                    {
                                        if (MAutoEnable)
                                        {
                                            UpdateDBBackupServer(info.MIP, info.MUserName, info.MPwd, info.MPathRemote);
                                        }
                                    }
                                    else
                                    {
                                        BaseDB db = new BaseDB();
                                        while (listDate.Count > info.MCount)
                                        {
                                            db.RemoveBackupDirectory(listDate.Min().ToString("yyyyMMddHHmmss"), info.MIP, info.MUserName, info.MPwd, info.MPathRemote);
                                            listDate.Remove(listDate.Min());
                                        }

                                        DateTime max = listDate.Max();
                                        if ((DateTime.Now - max).TotalDays > info.MFrequency)
                                        {
                                            if (MAutoEnable)
                                            {
                                                UpdateDBBackupServer(info.MIP, info.MUserName, info.MPwd, info.MPathRemote);
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    catch (Exception ex)
                    {
                        SystemLog.SystemLogManager.LogWrite(ex);
                    }
                    finally
                    {
                        Thread.Sleep(DlyBase.c_sleep600);
                    }
                }
            }
        }

        /// <summary>
        /// 自动备份到本地
        /// </summary>
        /// <param name="localPath"></param>
        private void DBBackupLocal(string localPath)
        {
            MAutoIng = true;
            string notime = DateTime.Now.ToString("yyyyMMddHHmmss");

            string path = localPath + "/" + notime + "Auto";
            Directory.CreateDirectory(path);

            BaseDB db = new BaseDB();
            if (db.BackupLocal(notime, path))
            {
                MAuditTrailsEvent?.Invoke(true, ReadXamlDatabase.GetResources(ReadXamlDatabase.C_Local) + ReadXamlDatabase.GetResources(ReadXamlDatabase.C_AutoBackupSuccess), path);
            }
            else
            {
                MAuditTrailsEvent?.Invoke(false, ReadXamlDatabase.GetResources(ReadXamlDatabase.C_Local) + ReadXamlDatabase.GetResources(ReadXamlDatabase.C_AutoBackupFail), path);
            }
            MAutoIng = false;
        }

        /// <summary>
        /// 自动备份到远程
        /// </summary>
        /// <param name="remoteIP"></param>
        /// <param name="remoteName"></param>
        /// <param name="remotePwd"></param>
        /// <param name="remotePath"></param>
        private void UpdateDBBackupServer(string remoteIP, string remoteName, string remotePwd, string remotePath)
        {
            MAutoIng = true;
            string notime = DateTime.Now.ToString("yyyyMMddHHmmss");

            string path = remotePath + "/" + notime + "Auto";
            BaseDB db = new BaseDB();
            db.CreateBackupDirectory(notime, remoteIP, remoteName, remotePwd, remotePath);

            if (db.BackupServer(notime, remoteIP, remoteName, remotePwd, path))
            {
                MAuditTrailsEvent?.Invoke(true, ReadXamlDatabase.GetResources(ReadXamlDatabase.C_Remote) + ReadXamlDatabase.GetResources(ReadXamlDatabase.C_AutoBackupSuccess), path);
            }
            else
            {
                MAuditTrailsEvent?.Invoke(false, ReadXamlDatabase.GetResources(ReadXamlDatabase.C_Remote) + ReadXamlDatabase.GetResources(ReadXamlDatabase.C_AutoBackupFail), path);
            }
            MAutoIng = false;
        }
    } 
}
