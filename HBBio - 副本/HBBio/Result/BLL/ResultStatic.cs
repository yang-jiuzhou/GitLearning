using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Result
{
    /**
     * ClassName: CurveStatic
     * Description: 曲线数据管理类
     * Version: 1.0
     * Create:  2018/05/16
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class ResultStatic
    {
        private static readonly ResultStatic s_instance = new ResultStatic();

        private ResultUnitTable m_tableUnit = null;          //当前谱图数据表
        private string m_name = "";
        public string MName
        {
            get
            {
                return m_name;
            }
        }

        public ResultRow m_curveInfo = new ResultRow();     //数据行
        private double m_lastTotalFlow = 0.0;               //上一秒的总流速
        private double m_lastPumpSFlow = 0.0;               //上一秒的上样流速
        private double m_colVol = 1;                        //柱子的体积
        public string MColumnVol
        {
            get
            {
                return m_colVol.ToString();
            }
        }
        private double m_colHeight = 1;                     //柱子的高度
        public string MColumnHeight
        {
            get
            {
                return m_colHeight.ToString();
            }
        }
        public double m_T = -0.01;
        private double m_V = 0.0;
        public double MV
        {
            get
            {
                return Math.Round(m_V, 2);
            }
        }
        private double m_CV = 0.0;
        public double MCV
        {
            get
            {
                return Math.Round(m_CV, 2);
            }
        }

        public double m_kph = 0.0;
        public double m_kcd = 0.0;
        public double m_kuv = 0.0;


        /// <summary>
        /// 私有构造函数
        /// </summary>
        private ResultStatic()
        {

        }

        /// <summary>
        /// 单例模式
        /// </summary>
        /// <returns></returns>
        public static ResultStatic Instance()
        {
            return s_instance;
        }

        /// <summary>
        /// 继续旧谱图
        /// </summary>
        /// <returns></returns>
        public string ContinueTable(List<string> nameList, double time, double vol, double cv)
        {
            string error = null;

            ResultListTable table = new ResultListTable();
            string tableName = null;
            error = table.SelectRowLastLogName(out m_name, out tableName, out m_colVol);
            m_tableUnit = new ResultUnitTable(tableName, null);

            m_curveInfo.SetList(nameList.Count);

            m_T = time;
            m_V = vol;
            m_CV = cv;

            return error;
        }

        /// <summary>
        /// 添加新谱图(-1表示手动运行)
        /// </summary>
        public string CreateTable(List<string> nameList, string name, int csID, int projectID, int userID, EnumResultIconType type, byte[] methodStreamInfo, double columnVol, double columnHeight, string attachmentInfo)
        {
            string error = null;

            DateTime dateTime = DateTime.Now;
            string tableName = "Curve" + dateTime.ToString("yyyyMMddHHmmss");

            m_tableUnit = new ResultUnitTable(tableName, nameList);
            error += m_tableUnit.InitTable();

            ResultListTable tableList = new ResultListTable();
            error += tableList.InsertRow(name, csID, projectID, userID, type, methodStreamInfo, dateTime, tableName, columnVol, columnHeight, attachmentInfo);

            if (string.IsNullOrEmpty(error))
            {
                error = null;
            }

            m_curveInfo.SetList(nameList.Count);

            m_name = name;
            m_colVol = columnVol;
            m_colHeight = columnHeight;
            m_T = -0.01;
            m_V = 0;
            m_lastTotalFlow = 0;

            return error;
        }

        /// <summary>
        /// 插入新数据
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string AddItem(ResultRow item)
        {
            if (null == m_tableUnit)
            {
                return null;
            }

            return m_tableUnit.InsertRow(item);
        }

        /// <summary>
        /// 是否是新时刻的值
        /// </summary>
        /// <returns></returns>
        public bool IsNewData(double chromatogramTime, double totalFlow, double pumpSFlow)
        {
            if (chromatogramTime > m_T)
            {
                m_V += (totalFlow + m_lastTotalFlow) * (chromatogramTime - m_T) / 2;
                m_CV = m_V / m_colVol;
                m_T = chromatogramTime;

                m_lastTotalFlow = totalFlow;
                m_lastPumpSFlow = pumpSFlow;

                m_curveInfo.m_T = m_T;
                m_curveInfo.m_V = Math.Round(m_V, 2);
                m_curveInfo.m_CV = Math.Round(m_CV, 2);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
