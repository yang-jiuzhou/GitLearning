using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.AuditTrails
{
    /**
     * ClassName: AuditTrailsManager
     * Description: 审计跟踪管理类
     * Version: 1.0
     * Create:  2018/05/16
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class AuditTrailsManager
    {
        /// <summary>
        /// 根据条件读取记录
        /// </summary>
        /// <param name="logdt"></param>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <param name="type"></param>
        /// <param name="userName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public string SearchAllLog(out DataTable logdt, DateTime dt1, DateTime dt2, string type, string userName, string filter)
        {
            string error = null;

            List<string> columnList = Share.ReadXaml.GetEnumList<EnumLog>("AT_");
            columnList.Insert(0, "ID");//第一列是ID
            logdt = new DataTable();
            foreach (var it in columnList)
            {
                logdt.Columns.Add(it);
            }

            List<string> files = null;
            LogListTable tableList = new LogListTable();
            error = tableList.SelectListName(out files);
            string strdt1 = "Log" + dt1.ToString("yyyyMMddHHmmss"); //起始时间
            string strdt2 = "Log" + dt2.ToString("yyyyMMddHHmmss"); //终止时间

            DataTable temp = null;

            int startIndex = -2;                        //符合条件的第一个表
            int endIndex = -2;                          //符合条件的最后一个表
            //依次遍历所有log文件
            for (int i = 0; i < files.Count; i++)
            {
                //日期大于起始日期
                if (files[i].CompareTo(strdt1) >= 0)
                {
                    startIndex = i - 1;
                    for (int j = i; j < files.Count; j++)
                    {
                        //日期大于终止日期
                        if (files[j].CompareTo(strdt2) > 0)
                        {
                            endIndex = j - 1;
                            break;
                        }
                    }
                    break;
                }
            }

            if (-2 == startIndex)//所有表的日期都小于查询时间，所有只能在最后一张表查询是否存在符合的数据
            {
                LogUnitTable tableUnit = new LogUnitTable(files[files.Count - 1]);
                error = tableUnit.SearchLog(out temp, dt1, dt2, type, userName, filter);
                if (null == error)
                {
                    for (int j = 0; j < temp.Rows.Count; j++)
                    {
                        logdt.Rows.Add(temp.Rows[j].ItemArray);
                    }
                }

                return error;
            }
            else
            {
                if (-1 == startIndex)//只能从0开始
                {
                    startIndex = 0;
                }

                if (-1 == endIndex)//所有表的日期都大于查询时间，故没有符合的数据
                {
                    return error;
                }
                else
                {
                    if (-2 == endIndex)
                    {
                        endIndex = files.Count - 1;
                    }

                    LogUnitTable tableUnit = new LogUnitTable(files[startIndex]);

                    error = tableUnit.SearchLog(out temp, dt1, dt2, type, userName, filter);
                    if (null == error)
                    {
                        for (int j = 0; j < temp.Rows.Count; j++)
                        {
                            logdt.Rows.Add(temp.Rows[j].ItemArray);
                        }
                    }

                    for (int i = startIndex + 1; i < endIndex; i++)
                    {
                        tableUnit = new LogUnitTable(files[i]);
                        error = tableUnit.SearchLog(out temp, type, userName, filter);
                        if (null == error)
                        {
                            for (int j = 0; j < temp.Rows.Count; j++)
                            {
                                logdt.Rows.Add(temp.Rows[j].ItemArray);
                            }
                        }
                    }
                    if (startIndex != endIndex)
                    {
                        tableUnit = new LogUnitTable(files[endIndex]);
                        error = tableUnit.SearchLog(out temp, dt1, dt2, type, userName, filter);
                        if (null == error)
                        {
                            for (int j = 0; j < temp.Rows.Count; j++)
                            {
                                logdt.Rows.Add(temp.Rows[j].ItemArray);
                            }
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(error))
            {
                error = null;
            }

            return error;
        }

        /// <summary>
        /// 获取审计跟踪列表信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetColumnVisibility(out LogColumnVisibility item)
        {
            LogColumnVisibilityTable table = new LogColumnVisibilityTable();
            return table.SelectRow(out item);
        }

        /// <summary>
        /// 设置审计跟踪列表信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string SetColumnVisibility(LogColumnVisibility item)
        {
            LogColumnVisibilityTable table = new LogColumnVisibilityTable();
            return table.UpdateRow(item);
        }
    }
}
