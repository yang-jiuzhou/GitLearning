using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Result
{
    /**
     * ClassName: ResultManager
     * Description: 谱图结果管理
     * Version: 1.0
     * Create:  2020/11/18
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    class ResultManager
    {
        /// <summary>
        /// 根据通信ID、项目ID找打谱图结果列表
        /// </summary>
        /// <param name="communicationSetsID"></param>
        /// <param name="projectID"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public string GetListName(int communicationSetsID, int projectID, string filter, out List<ResultTitle> list)
        {
            ResultListTable table = new ResultListTable();
            return table.SelectListName(communicationSetsID, projectID, filter, out list);
        }

        /// <summary>
        /// 获取谱图可使用的名称
        /// </summary>
        /// <param name="communicationSetsID"></param>
        /// <param name="projectID"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetAvailableName(int communicationSetsID, int projectID, ref string name)
        {
            List<string> list = null;
            ResultListTable table = new ResultListTable();
            string error = table.SelectListName(communicationSetsID, projectID, name, out list);
            if (null == error)
            {
                int index = 1;
                while (list.Contains(name + "-" + index) && index < 999999)
                {
                    index++;
                }

                name = name + "-" + index;
            }

            return error;
        }

        /// <summary>
        /// 更新谱图运行结束时间
        /// </summary>
        /// <returns></returns>
        public string UpdateEndTime()
        {
            ResultListTable table = new ResultListTable();
            return table.UpdateRowEndTime();
        }

        /// <summary>
        /// 更新谱图的标记列表信息
        /// </summary>
        /// <returns></returns>
        public string UpdateMarkerInfo(string markerInfo)
        {
            ResultListTable table = new ResultListTable();
            return table.UpdateRowMarkerInfo(markerInfo);
        }

        /// <summary>
        /// 更新谱图的名称
        /// </summary>
        /// <returns></returns>
        public string UpdateName(ResultTitle item)
        {
            ResultListTable table = new ResultListTable();
            return table.UpdateName(item);
        }

        /// <summary>
        /// 获取运行数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="signalCount"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public string GetCurveData(int id, List<int> smooth, List<List<double>> listList, out System.IO.Stream ms, out double cv, out double ch, out string attachment, out string markerInfo)
        {
            markerInfo = null;

            ResultListTable table = new ResultListTable();
            string curveName = null;
            DateTime time = DateTime.Now;
            string error = table.SelectRowInfo(id, out ms, out curveName, out cv, out ch, out attachment, out markerInfo, out time);
            if (null == error)
            {
                ResultUnitTable unitTable = new ResultUnitTable(curveName, null);
                error = unitTable.SelectList(cv, smooth, listList);
            }

            return error;
        }

        /// <summary>
        /// 获取最新的运行数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public string GetCurveDataLast(List<int> smooth, List<List<double>> listList, out string markerInfo, out DateTime time)
        {
            markerInfo = null;
            time = DateTime.Now;

            ResultListTable table = new ResultListTable();
            int id = -1;
            string error = table.GetLastID(out id);
            if (null == error)
            {
                System.IO.Stream ms = null;
                string curveName = null;
                double columnVol = 1;
                double columnHeight = 1;
                string attachment = "";
                error = table.SelectRowInfo(id, out ms, out curveName, out columnVol, out columnHeight, out attachment, out markerInfo, out time);
                if (null == error)
                {
                    ResultUnitTable unitTable = new ResultUnitTable(curveName, null);
                    error = unitTable.SelectList(columnVol, smooth, listList);
                }
            }

            return error;
        }

        /// <summary>
        /// 获取最新的运行数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public string GetCurveDataLast(out string name, out int projectID)
        {
            name = null;
            projectID = -1;

            ResultListTable table = new ResultListTable();
            int id = -1;
            string error = table.GetLastID(out id);
            if (null == error)
            {
                error = table.SelectRowInfo(id, out name, out projectID);
            }

            return error;
        }
    }
}