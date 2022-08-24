using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.ColumnList
{
    /**
     * ClassName: ColumnItem
     * Description: 色谱柱信息
     * Version: 1.0
     * Create:  2018/05/16
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class ColumnItem
    {
        public int MID { get; set; }
        public string MName { get; set; }
        public string MNote { get; set; }
        public string MUser { get; set; }
        public RunParameters MRP { get; set; }
        public Details MDT { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public ColumnItem()
        {
            MRP = new RunParameters();
            MDT = new Details();

            List<string> strList = new List<string>();
            for (int i = 0; i < 3 + EnumRunParametersInfo.Count + EnumDetailsInfo.Count; i++)
            {
                strList.Add("");
            }

            InItList(strList, true);
        }

        /// <summary>
        /// 构造函数（显示）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public ColumnItem(int id, string name)
        {
            MID = id;
            MName = name;
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="user"></param>
        public void Reset(string user)
        {
            MName = "new name";
            MNote = "";
            MUser = user;
        }

        /// <summary>
        /// 比较是否相等
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Compared(ColumnItem other)
        {
            if (null == other)
            {
                return false;
            }
            else
            {
                if (!MName.Equals(other.MName))
                {
                    return false;
                }

                if (!MNote.Equals(other.MNote))
                {
                    return false;
                }

                if (!MUser.Equals(other.MUser))
                {
                    return false;
                }

                if (!MRP.Compared(other.MRP))
                {
                    return false;
                }

                if (!MDT.Compared(other.MDT))
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="strList"></param>
        public void InItList(List<string> strList, bool first=false)
        {
            int index = 0;

            MName = strList[index++];
            MNote = strList[index++];
            MUser = strList[index++];

            //初始化运行参数列表的值
            for (int i = 0; i < MRP.MList.Count; i++)
            {
                if (first)
                {
                    MRP.MList[i].MChangedEvent += new PVUEventHandler(RPChanged);
                }

                switch ((EnumRunParameters)i)
                {
                    case EnumRunParameters.RPTec:
                    case EnumRunParameters.RPCVU:
                    case EnumRunParameters.RPPTU:
                        {
                            MRP.MList[i].MshowComboboxStr = strList[index++];
                        }
                        break;
                    default:
                        {
                            MRP.MList[i].MshowValueStr = strList[index++];
                        }
                        break;
                }
            }

            ///初始化详细列举列表值
            for (int i = 0; i < MDT.MList.Count; i++)
            {
                if (first)
                {
                    MDT.MList[i].MChangedEvent += new PVUEventHandler(DTChanged);
                }

                switch ((EnumDetails)i)
                {
                    case EnumDetails.DTLR:
                        {
                            MDT.MList[i].MShowTextStr = strList[index++];
                        }
                        break;
                    default:
                        {
                            MDT.MList[i].MshowValueStr = strList[index++];
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 参数列表值修改事件
        /// </summary>
        /// <param name="sender"></param>
        private void RPChanged(object sender)
        {
            int index = Convert.ToInt16(sender);
            switch ((EnumRunParameters)index)
            {
                case EnumRunParameters.RPCVU://柱体积单位变化
                    MRP.MList[(int)EnumRunParameters.RPCV].MUnit = MRP.MList[index].MText;
                    MDT.MList[(int)EnumDetails.DTLV].MUnit = MRP.MList[index].MText;
                    MDT.MList[(int)EnumDetails.DVV].MUnit = MRP.MList[index].MText;
                    MDT.MList[(int)EnumDetails.DTPWAB].MUnit = MRP.MList[index].MText;

                    MRP.MList[(int)EnumRunParameters.RPDFIT].MUnit = EnumFlowInfo.NameList[MRP.MList[index].MIndex];
                    MRP.MList[(int)EnumRunParameters.RPFIT].MUnit = EnumFlowInfo.NameList[MRP.MList[index].MIndex];
                    break;
                case EnumRunParameters.RPPTU:
                    MRP.MList[(int)EnumRunParameters.RPPPT].MUnit = MRP.MList[index].MText;
                    MRP.MList[(int)EnumRunParameters.RPDPT].MUnit = MRP.MList[index].MText;
                    break;
                case EnumRunParameters.RPDFIT:
                    {
                        double mlArea = ValueTrans.CalArea(MDT.MList[(int)EnumDetails.DHD].MValue);

                        switch (MRP.MList[(int)EnumRunParameters.RPCVU].MIndex)
                        {
                            case 0:
                                MRP.MList[(int)EnumRunParameters.RPDLFIT].MValue = Math.Round(MRP.MList[(int)EnumRunParameters.RPDFIT].MValue * 1000 / mlArea, 1);
                                break;
                            case 1:
                                MRP.MList[(int)EnumRunParameters.RPDLFIT].MValue = Math.Round(MRP.MList[(int)EnumRunParameters.RPDFIT].MValue * 60 / mlArea, 1);
                                break;
                            case 2:
                                MRP.MList[(int)EnumRunParameters.RPDLFIT].MValue = Math.Round(MRP.MList[(int)EnumRunParameters.RPDFIT].MValue / 1000 * 60 / mlArea, 1);
                                break;
                        }
                    }
                    break;
                case EnumRunParameters.RPFIT:
                    {
                        double mlArea = ValueTrans.CalArea(MDT.MList[(int)EnumDetails.DHD].MValue);
                        switch (MRP.MList[(int)EnumRunParameters.RPCVU].MIndex)
                        {
                            case 0:
                                MRP.MList[(int)EnumRunParameters.RPLFIT].MValue = Math.Round(MRP.MList[(int)EnumRunParameters.RPFIT].MValue * 1000 / mlArea, 1);
                                break;
                            case 1:
                                MRP.MList[(int)EnumRunParameters.RPLFIT].MValue = Math.Round(MRP.MList[(int)EnumRunParameters.RPFIT].MValue * 60 / mlArea, 1);
                                break;
                            case 2:
                                MRP.MList[(int)EnumRunParameters.RPLFIT].MValue = Math.Round(MRP.MList[(int)EnumRunParameters.RPFIT].MValue / 1000 * 60 / mlArea, 1);
                                break;
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// 详细列举值修改事件
        /// </summary>
        /// <param name="sender"></param>
        private void DTChanged(object sender)
        {
            switch ((EnumDetails)sender)
            {
                case EnumDetails.DHD:
                    {
                        double mlArea = ValueTrans.CalArea(MDT.MList[(int)EnumDetails.DHD].MValue);
                        double mlVol = mlArea * MDT.MList[(int)EnumDetails.DBH].MValue;
                        switch (MRP.MList[(int)EnumRunParameters.RPCVU].MIndex)
                        {
                            case 0:
                                MRP.MList[(int)EnumRunParameters.RPCV].MValue = mlVol / 1000;
                                MRP.MList[(int)EnumRunParameters.RPDLFIT].MValue = Math.Round(MRP.MList[(int)EnumRunParameters.RPDFIT].MValue * 1000 / mlArea, 1);
                                MRP.MList[(int)EnumRunParameters.RPLFIT].MValue = Math.Round(MRP.MList[(int)EnumRunParameters.RPFIT].MValue * 1000 / mlArea, 1);
                                break;
                            case 1:
                                MRP.MList[(int)EnumRunParameters.RPCV].MValue = mlVol;
                                MRP.MList[(int)EnumRunParameters.RPDLFIT].MValue = Math.Round(MRP.MList[(int)EnumRunParameters.RPDFIT].MValue * 60 / mlArea, 1);
                                MRP.MList[(int)EnumRunParameters.RPLFIT].MValue = Math.Round(MRP.MList[(int)EnumRunParameters.RPFIT].MValue * 60 / mlArea, 1);
                                break;
                            case 2:
                                MRP.MList[(int)EnumRunParameters.RPCV].MValue = mlVol * 1000;
                                MRP.MList[(int)EnumRunParameters.RPDLFIT].MValue = Math.Round(MRP.MList[(int)EnumRunParameters.RPDFIT].MValue / 1000 * 60 / mlArea, 1);
                                MRP.MList[(int)EnumRunParameters.RPLFIT].MValue = Math.Round(MRP.MList[(int)EnumRunParameters.RPFIT].MValue / 1000 * 60 / mlArea, 1);
                                break;
                        }
                    }
                    break;

                case EnumDetails.DBH:
                    {
                        double mlVol = ValueTrans.CalVol(MDT.MList[(int)EnumDetails.DHD].MValue, MDT.MList[(int)EnumDetails.DBH].MValue);
                        switch (MRP.MList[(int)EnumRunParameters.RPCVU].MIndex)
                        {
                            case 0:
                                MRP.MList[(int)EnumRunParameters.RPCV].MValue = mlVol / 1000;
                                break;
                            case 1:
                                MRP.MList[(int)EnumRunParameters.RPCV].MValue = mlVol;
                                break;
                            case 2:
                                MRP.MList[(int)EnumRunParameters.RPCV].MValue = mlVol * 1000;
                                break;
                        }
                    }
                    break;
            }
        } 
    }
}
