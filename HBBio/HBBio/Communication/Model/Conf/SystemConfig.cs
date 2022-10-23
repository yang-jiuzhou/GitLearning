using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    [Serializable]
    public class SystemConfig
    {
        //柱子信息，数量已知
        public ConfColumn MConfColumn { get; set; }
        //泵洗信息，数量已知
        public ConfWash MConfWash { get; set; }
        //组分收集器
        public ConfCollector MConfCollector { get; set; }
        //气泡传感器，数量未知
        public List<ConfAS> MListConfAS { get; set; }
        //监控默认延迟体积
        public double MDelayVol { get; set; }
        //监控延迟体积，数量未知
        public List<ConfpHCdUV> MListConfpHCdUV { get; set; }
        /// <summary>
        /// 其它设置
        /// </summary>
        public ConfOther MConfOther { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public SystemConfig()
        {
            MConfColumn = new ConfColumn();
            MConfWash = new ConfWash();
            MConfCollector = new ConfCollector();
            MListConfAS = new List<ConfAS>();
            MDelayVol = 0;
            MListConfpHCdUV = new List<ConfpHCdUV>();
            MConfOther = new ConfOther();
        }

        /// <summary>
        /// 添加气泡传感器
        /// </summary>
        public void AddAS()
        {
            MListConfAS.Add(new ConfAS());
        }

        /// <summary>
        /// 添加监控值
        /// </summary>
        /// <param name="name"></param>
        public void AddPHCDUV(string name)
        {
            ConfpHCdUV item = new ConfpHCdUV();
            item.MName = name;
            MListConfpHCdUV.Add(item);
        }

        /// <summary>
        /// 返回写入数据库的信息
        /// </summary>
        /// <param name="split"></param>
        /// <returns></returns>
        public string GetDBInfo(string split = "#~#")
        {
            StringBuilderSplit sb = new StringBuilderSplit(split);
            split = split.Replace("#~", "#~~");
            sb.Append(MConfColumn.GetDBInfo(split));
            sb.Append(MConfWash.GetDBInfo(split));
            sb.Append(MConfCollector.GetDBInfo(split));
            sb.Append(MListConfAS.Count);
            foreach (var it in MListConfAS)
            {
                sb.Append(it.GetDBInfo(split));
            }
            sb.Append(MDelayVol);
            sb.Append(MListConfpHCdUV.Count);
            foreach (var it in MListConfpHCdUV)
            {
                sb.Append(it.GetDBInfo(split));
            }
            sb.Append(MConfOther.GetDBInfo(split));

            return sb.ToString();
        }

        /// <summary>
        /// 解析数据库信息
        /// </summary>
        /// <param name="infoStr"></param>
        /// <param name="split"></param>
        public void SetDBInfo(int id, string infoStr, string split = "#~#")
        {
            try
            {
                string[] info = System.Text.RegularExpressions.Regex.Split(infoStr, split);
                int index = 0;
                split = split.Replace("#~", "#~~");
                MConfColumn.SetDBInfo(split, info[index++]);
                MConfWash.SetDBInfo(split, info[index++]);
                MConfCollector.SetDBInfo(split, info[index++]);
                int count = Convert.ToInt32(info[index++]);
                for (int i = 0; i < count; i++)
                {
                    MListConfAS.Add(new ConfAS());
                    MListConfAS[i].SetDBInfo(split, info[index++]);
                }
                MDelayVol = Convert.ToDouble(info[index++]);
                count = Convert.ToInt32(info[index++]);
                for (int i = 0; i < count; i++)
                {
                    MListConfpHCdUV.Add(new ConfpHCdUV());
                    MListConfpHCdUV[i].SetDBInfo(split, info[index++]);
                }
                MConfOther.SetDBInfo(split, info[index++]);
            }
            catch
            {
                List<ComConf> cfList = new List<ComConf>();
                ComConfTable ccDB = new ComConfTable(id);
                if (null == ccDB.GetDataList(out cfList))
                {
                    foreach (var itCF in cfList)
                    {
                        for (int i = 0; i < itCF.MList.Count; i++)
                        {
                            if (!itCF.MList[i].MVisible)
                            {
                                itCF.MList.RemoveAt(i);
                                --i;
                            }
                        }

                        foreach (var itBI in itCF.MList)
                        {
                            if (itBI.MConstName.Contains("AS"))
                            {
                                MListConfAS.Add(new ConfAS());
                            }
                            else if (itBI.MConstName.Contains("pH") || itBI.MConstName.Contains("Cd"))
                            {
                                MListConfpHCdUV.Add(new ConfpHCdUV() { MName = itBI.MConstName });
                            }
                            else if (itBI.MConstName.Contains("UV"))
                            {
                                for (int i = 0; i < ((UVItem)itBI).m_signalCount; i++)
                                {
                                    MListConfpHCdUV.Add(new ConfpHCdUV() { MName = itBI.MConstName + "_" + (i + 1) });
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}