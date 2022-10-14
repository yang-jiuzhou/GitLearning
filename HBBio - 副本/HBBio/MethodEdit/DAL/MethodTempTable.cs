using HBBio.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: MethodTempTable
     * Description: 方法临时数据表
     * Version: 1.0
     * Create:  2020/11/12
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    class MethodTempTable : BaseTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MethodTempTable()
        {
            m_tableName = "MethodTempTable";
        }

        /// <summary>
        /// 插入默认值
        /// </summary>
        /// <returns></returns>
        protected override string AddDefaultValue()
        {
            return InsertRow(new MethodTemp());
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <returns></returns>
        protected override string CreateTable()
        {
            string error = null;
            error += SqlCreateTable(
                @"[ID] [int],
                [Name] [nvarchar](64),
                [Type] [int],
                [IndexCurrMethod] [int],
                [IndexCurrPhase] [int],
                [PhaseStart] [float],
                [PhaseStop] [float],
                [PhaseRunTime] [float],
                [HoldStart] [float],
                [HoldTotal] [float],
                [IsHold] [bit]"
                );

            if (string.IsNullOrEmpty(error))
            {
                error = null;
            }

            return error;
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string InsertRow(MethodTemp item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("'" + item.MID);
            sb.Append("','" + item.MName);
            sb.Append("','" + item.MType);
            sb.Append("','" + item.MIndexCurrMethod);
            sb.Append("','" + item.MIndexCurrPhase);
            sb.Append("','" + item.MPhaseStartT);
            sb.Append("','" + item.MPhaseStopT);
            sb.Append("','" + item.MPhaseRunTime);
            sb.Append("','" + item.MHoldStartT);
            sb.Append("','" + item.MHoldTotalT);
            sb.Append("','" + item.MIsHold + "'");

            return SqlInsertRow(sb.ToString());
        }

        /// <summary>
        /// 修改行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string AddRow(MethodTemp item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("ID='" + item.MID);
            sb.Append("',Name='" + item.MName);
            sb.Append("',Type='" + item.MType);
            sb.Append("',IndexCurrMethod='" + item.MIndexCurrMethod);
            sb.Append("',IndexCurrPhase='" + item.MIndexCurrPhase);
            sb.Append("',PhaseStart='" + item.MPhaseStartT);
            sb.Append("',PhaseStop='" + item.MPhaseStopT);
            sb.Append("',PhaseRunTime='" + item.MPhaseRunTime);
            sb.Append("',HoldStart='" + item.MHoldStartT);
            sb.Append("',HoldTotal='" + item.MHoldTotalT);
            sb.Append("',IsHold='" + item.MIsHold + "'");

            return SqlUpdateRow(sb.ToString());
        }

        /// <summary>
        /// 删除行
        /// </summary>
        /// <returns></returns>
        public string DelRow()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("ID='-1'");

            return SqlUpdateRow(sb.ToString());
        }

        /// <summary>
        /// 获取行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string SelectRow(out MethodTemp item)
        {
            string error = null;
            item = null;

            try
            {
                SqlDataReader reader = null;
                error = CreateConnAndReader(@"SELECT * FROM " + m_tableName, out reader);
                if (null == error)
                {
                    if (reader.Read())//匹配
                    {
                        int index = 0;
                        item = new MethodTemp();
                        item.MID = reader.GetInt32(index++);
                        item.MName = reader.GetString(index++);
                        item.MType = reader.GetInt32(index++);
                        item.MIndexCurrMethod = reader.GetInt32(index++);
                        item.MIndexCurrPhase = reader.GetInt32(index++);
                        item.MPhaseStartT = reader.GetDouble(index++);                        //当前执行阶段的开始时间点
                        item.MPhaseStopT = reader.GetDouble(index++);                         //当前执行阶段的结束时间点
                        item.MPhaseRunTime = reader.GetDouble(index++);
                        item.MHoldStartT = reader.GetDouble(index++);                         //当前执行阶段的一次挂起时间点
                        item.MHoldTotalT = reader.GetDouble(index++);
                        item.MIsHold = reader.GetBoolean(index++);
                    }
                    else
                    {
                        error = Share.ReadXaml.S_ErrorNoData;
                    }
                    CloseConnAndReader();
                }
            }
            catch (Exception msg)
            {
                error = msg.Message;
            }

            return error;
        }
    }
}
