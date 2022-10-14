using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.Database
{
    class ReadXamlDatabase
    {
        public const string C_Local = "DB_Local";                       //本地
        public const string C_Remote = "DB_Remote";                     //远程
        public const string C_PathLocal = "DB_PathLocal";               //本地路径
        public const string C_PathRemote = "DB_PathRemote";             //远程路径
        public const string C_BackupSuccess = "DB_BackupSuccess";       //备份成功
        public const string C_BackupFail = "DB_BackupFail";             //备份失败
        public const string C_RestoreSuccess = "DB_RestoreSuccess";     //还原成功
        public const string C_RestoreFail = "DB_RestoreFail";           //还原失败
        public const string C_AutoBackupSuccess = "DB_AutoBackupSuccess";       //自动备份成功
        public const string C_AutoBackupFail = "DB_AutoBackupFail";             //自动备份失败
        public const string C_AutoBackupIng = "DB_AutoBackupIng";               //系统正在进行数据自动备份中，请稍后


        /// <summary>
        /// 返回文本信息
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetResources(string str)
        {
            return (string)Application.Current.Resources[str];
        }
    }
}
