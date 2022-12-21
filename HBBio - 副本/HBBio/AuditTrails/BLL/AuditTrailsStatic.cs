using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.AuditTrails
{
    /**
     * ClassName: AuditTrailsStatic
     * Description: 审计跟踪静态类
     * Version: 1.0
     * Create:  2029/09/07
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class AuditTrailsStatic
    {
        /// <summary>
        /// 日志行列表
        /// </summary>
        public ObservableCollection<LogInfo> MlogIngoList { get; set; }
        /// <summary>
        /// 当前日志表
        /// </summary>
        private LogUnitTable m_table = null;
        /// <summary>
        /// 批时间
        /// </summary>
        private string m_batchT = "N/A";
        /// <summary>
        /// 批体积
        /// </summary>
        private string m_batchV = "N/A";
        /// <summary>
        /// 批柱体积
        /// </summary>
        private string m_batchCV = "N/A";
        /// <summary>
        /// 用户名
        /// </summary>
        private string m_userName = "N/A";


        /// <summary>
        /// 构造函数
        /// </summary>
        private AuditTrailsStatic()
        {
            MlogIngoList = new ObservableCollection<LogInfo>();
        }

        /// <summary>
        /// 内部静态类
        /// </summary>
        private static class AuditTrailsStaticInstance
        {
            internal static readonly AuditTrailsStatic s_instance = new AuditTrailsStatic();
        }

        /// <summary>
        /// 单例模式
        /// </summary>
        /// <returns></returns>
        public static AuditTrailsStatic Instance()
        {
            return AuditTrailsStaticInstance.s_instance;
        }

        /// <summary>
        /// 继续旧日志
        /// </summary>
        /// <returns></returns>
        public string ContinueTable()
        {
            string tableName = null;
            LogListTable table = new LogListTable();
            string error = table.SelectRowLastLogName(out tableName);
            if (null == error)
            {
                m_table = new LogUnitTable(tableName);
            }

            return error;
        }

        /// <summary>
        /// 添加新日志
        /// </summary>
        public string CreateTable()
        {
            string error = null;

            string tableName = "Log" + DateTime.Now.ToString("yyyyMMddHHmmss");

            m_table = new LogUnitTable(tableName);
            error += m_table.InitTable();

            LogListTable tableList = new LogListTable();
            error += tableList.InsertRow(tableName);

            if (string.IsNullOrEmpty(error))
            {
                error = null;
            }

            return error;
        }

        /// <summary>
        /// 插入新数据
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string InsertRow(LogInfo item)
        {
            MlogIngoList.Add(Share.DeepCopy.DeepCopyByXml(item));

            if (null != m_table)
            {
                return m_table.InsertRow(item);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="oper"></param>
        /// <returns></returns>
        public string InsertRow(EnumATType type, string desc, string oper = "N/A")
        {
            LogInfo item = new LogInfo();
            item.MType = type.ToString();
            item.MDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            item.MBatchT = m_batchT;
            item.MBatchV = m_batchV;
            item.MBatchCV = m_batchCV;
            item.MUserName = m_userName;
            item.MDescription = desc;
            item.MOperation = oper;

            return InsertRow(item);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="oper"></param>
        /// <returns></returns>
        public string InsertRowSystem(string desc, string oper = "N/A")
        {
            return InsertRow(EnumATType.System, desc, oper);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="oper"></param>
        /// <returns></returns>
        public string InsertRowError(string desc, string oper = "N/A")
        {
            return InsertRow(EnumATType.Error, desc, oper);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="oper"></param>
        /// <returns></returns>
        public string InsertRowAlarmWarning(string desc, string oper = "N/A")
        {
            return InsertRow(EnumATType.AlarmWarning, desc, oper);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="desc"></param>
        /// <returns></returns>
        public string InsertRowColumnList(string desc, string oper = "N/A")
        {
            return InsertRow(EnumATType.ColumnList, desc, oper);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="oper"></param>
        /// <returns></returns>
        public string InsertRowOperate(string desc, string oper = "N/A")
        {
            return InsertRow(EnumATType.Operate, desc, oper);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="desc"></param>
        /// <returns></returns>
        public string InsertRowSignerReviewer(string desc, string oper = "N/A")
        {
            return InsertRow(EnumATType.SignerReviewer, desc, oper);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="desc"></param>
        /// <returns></returns>
        public string InsertRowManual(string desc, string oper = "N/A")
        {
            return InsertRow(EnumATType.Manual, desc, oper);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="desc"></param>
        /// <returns></returns>
        public string InsertRowMethod(string desc, string oper = "N/A")
        {
            return InsertRow(EnumATType.Method, desc, oper);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="desc"></param>
        /// <returns></returns>
        public string InsertRowColl(string desc, string oper = "N/A")
        {
            return InsertRow(EnumATType.Coll, desc, oper);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="desc"></param>
        /// <returns></returns>
        public string InsertRowMarker(string desc, string oper = "N/A")
        {
            return InsertRow(EnumATType.Marker, desc, oper);
        }

        /// <summary>
        /// 更新运行参数
        /// </summary>
        /// <param name="t"></param>
        /// <param name="v"></param>
        /// <param name="cv"></param>
        public void UpdateRowBatch(double t, double v, double cv)
        {
            m_batchT = t.ToString();
            m_batchV = v.ToString();
            m_batchCV = cv.ToString();
        }

        /// <summary>
        /// 更新运行参数
        /// </summary>
        /// <param name="t"></param>
        /// <param name="v"></param>
        /// <param name="cv"></param>
        public void UpdateRowBatch(string tvcv = "N/A")
        {
            m_batchT = tvcv;
            m_batchV = tvcv;
            m_batchCV = tvcv;
        }

        /// <summary>
        /// 更新当前用户
        /// </summary>
        /// <param name="name"></param>
        public void UpdateUserName(string name)
        {
            m_userName = name;
        }
    }
}
