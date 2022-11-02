using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace XmlDemo
{
    public partial class Form1 : Form
    {
        private string m_fileLoad = "D://HBErrorDATA3//1.xml";
        private string m_fileSave = "D://HBErrorDATA3//1.xml";

        public Form1()
        {
            InitializeComponent();
            UpdateMethodSetting();
            UpdatePhaseList();
        }

        private void UpdateMethodSetting()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(m_fileLoad);//加载Xml文件
            XmlNode root = xmlDoc.SelectSingleNode("Method/MMethodSetting");
            {
                XmlNode mloop = xmlDoc.SelectSingleNode("Method/MMethodSetting/MLoop");
                if (null == mloop)
                {
                    mloop = xmlDoc.CreateNode(XmlNodeType.Element, "MLoop", null);
                    mloop.InnerText = "1";

                    XmlNode select = xmlDoc.SelectSingleNode("Method/MMethodSetting/MModifyTime");
                    root.InsertAfter(mloop, select);
                }
            }
            {
                XmlNode mcpv = xmlDoc.SelectSingleNode("Method/MMethodSetting/MCPV");
                if (null != mcpv)
                {
                    root.RemoveChild(mcpv);
                }
                XmlNode select = xmlDoc.SelectSingleNode("Method/MMethodSetting/MColumnArea");
                root.InsertAfter(mcpv, select);
            }
            {
                RemoveChild(root, xmlDoc.SelectSingleNode("Method/MMethodSetting/MMaxFlowVol"));
                RemoveChild(root, xmlDoc.SelectSingleNode("Method/MMethodSetting/MMaxFlowLen"));
                RemoveChild(root, xmlDoc.SelectSingleNode("Method/MMethodSetting/MMaxFlowRate"));
            }
            {
                XmlNode mBaseStr = xmlDoc.SelectSingleNode("Method/MMethodSetting/MBaseStr");
                if (null == mBaseStr)
                {
                    XmlNode select = xmlDoc.SelectSingleNode("Method/MMethodSetting/MBaseUnitStr");

                    mBaseStr = xmlDoc.CreateNode(XmlNodeType.Element, "MBaseStr", null);
                    mBaseStr.InnerText = select.InnerText;

                    root.InsertBefore(mBaseStr, select);

                    switch (select.InnerText)
                    {
                        case "时间":
                            select.InnerText = "min";
                            break;
                        case "体积":
                            select.InnerText = "ml";
                            break;
                        case "柱体积":
                            select.InnerText = "cv";
                            break;
                    }
                }
            }
            {
                XmlNode mFlowVol = xmlDoc.SelectSingleNode("Method/MMethodSetting/MFlowVol");
                if (null == mFlowVol)
                {
                    mFlowVol = xmlDoc.CreateNode(XmlNodeType.Element, "MFlowVol", null);
                    mFlowVol.InnerText = "1";

                    XmlNode select = xmlDoc.SelectSingleNode("Method/MMethodSetting/MFlowRate");
                    root.InsertAfter(mFlowVol, select);
                }
            }
            {
                XmlNode mFlowVol = xmlDoc.SelectSingleNode("Method/MMethodSetting/MBPV");
                if (null == mFlowVol)
                {
                    mFlowVol = xmlDoc.CreateNode(XmlNodeType.Element, "MBPV", null);
                    mFlowVol.InnerText = "0";

                    XmlNode select = xmlDoc.SelectSingleNode("Method/MMethodSetting/MInD");
                    root.InsertAfter(mFlowVol, select);
                }
            }
            {
                XmlNode root2 = xmlDoc.SelectSingleNode("Method/MMethodSetting/MUVValue");
                XmlNode mEnabledWave2 = xmlDoc.SelectSingleNode("Method/MMethodSetting/MUVValue/MEnabledWave2");
                if (null == mEnabledWave2)
                {
                    mEnabledWave2 = xmlDoc.CreateNode(XmlNodeType.Element, "MEnabledWave2", null);
                    mEnabledWave2.InnerText = "true";

                    XmlNode select = xmlDoc.SelectSingleNode("Method/MMethodSetting/MUVValue/MWave4");
                    root2.InsertAfter(mEnabledWave2, select);
                }
            }
            {
                XmlNode root2 = xmlDoc.SelectSingleNode("Method/MMethodSetting/MASListValue");
                if (null != root2)
                {
                    root.RemoveChild(root2);
                }

                root2 = xmlDoc.SelectSingleNode("Method/MMethodSetting/MASParaList");
                if (null == root2)
                {
                    XmlNode mASParaList = xmlDoc.CreateElement("MASParaList");

                    XmlNode select = xmlDoc.SelectSingleNode("Method/MMethodSetting/MUVValue");
                    root.InsertAfter(mASParaList, select);
                    for(int i=1;i<=4;i++)
                    {
                        XmlNode aSMethodPara = xmlDoc.CreateElement("ASMethodPara");
                        mASParaList.AppendChild(aSMethodPara);
                        CreateNode(xmlDoc, aSMethodPara, "m_update", "false");
                        CreateNode(xmlDoc, aSMethodPara, "m_signal", "false");
                        CreateNode(xmlDoc, aSMethodPara, "MHeader", "AS0" + i);
                        CreateNode(xmlDoc, aSMethodPara, "MName", "AS0" + i);
                        CreateNode(xmlDoc, aSMethodPara, "MAction", "Ignore");
                        CreateNode(xmlDoc, aSMethodPara, "MLength", "0");
                        CreateNode(xmlDoc, aSMethodPara, "MUnit", "T");
                    }
                }
            }
            {
                XmlNodeList root2 = xmlDoc.SelectNodes("Method/MMethodSetting/MAlarmWarning/MList/AlarmWarningItem");
                foreach(XmlNode node in root2)
                {
                    if (!node.FirstChild.Name.Equals("MTypeName"))
                    {
                        XmlNode select = node.FirstChild;
                        InsertBeforeNode(xmlDoc, node, select, "MTypeName", select.InnerText);
                    }
                }
            }
            {
                XmlNode root2 = xmlDoc.SelectSingleNode("Method/MMethodSetting/MVolAreaBaseFlow");
                if (null != root2)
                {
                    root.RemoveChild(root2);
                }
            }

            xmlDoc.Save(m_fileSave);//保存修改的Xml文件内容
        }

        private void UpdatePhaseList()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(m_fileLoad);//加载Xml文件
            XmlNode root = xmlDoc.SelectSingleNode("Method/MPhaseList");
            XmlNodeList xmlNodeList = root.ChildNodes;//取出book节点下所有的子节点
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                XmlElement xmlElement = (XmlElement)xmlNode;
                if (xmlElement.Attributes[0].Value.Equals("ColumnCIP")
                    || xmlElement.Attributes[0].Value.Equals("ColumnPreparation"))
                {
                    xmlElement.Attributes[0].Value = "DlyPhase";
                    if(xmlNode.FirstChild.Name.Equals("MVolAreaBaseFlow"))
                    {
                        RemoveChild(xmlNode, xmlNode.FirstChild);
                    }
                    xmlNode.PrependChild(xmlDoc.CreateElement("MListGroup"));

                    UpdateColumnCIP(xmlDoc, xmlNode);
                }
                else if (xmlElement.Attributes[0].Value.Equals("ColumnPerformanceTest"))
                {
                    xmlElement.Attributes[0].Value = "DlyPhase";
                    if(xmlNode.FirstChild.Name.Equals("MVolAreaBaseFlow"))
                    {
                        RemoveChild(xmlNode, xmlNode.FirstChild);
                    }
                    xmlNode.PrependChild(xmlDoc.CreateElement("MListGroup"));

                    UpdateColumnPerformanceTest(xmlDoc, xmlNode);
                }
                else if (xmlElement.Attributes[0].Value.Equals("ColumnWash"))
                {
                    xmlElement.Attributes[0].Value = "DlyPhase";
                    if (xmlNode.FirstChild.Name.Equals("MVolAreaBaseFlow"))
                    {
                        RemoveChild(xmlNode, xmlNode.FirstChild);
                    }
                    xmlNode.PrependChild(xmlDoc.CreateElement("MListGroup"));

                    UpdateColumnWash(xmlDoc, xmlNode);
                }
                else if (xmlElement.Attributes[0].Value.Equals("Elution"))
                {
                    xmlElement.Attributes[0].Value = "DlyPhase";
                    if (xmlNode.FirstChild.Name.Equals("MVolAreaBaseFlow"))
                    {
                        RemoveChild(xmlNode, xmlNode.FirstChild);
                    }
                    xmlNode.PrependChild(xmlDoc.CreateElement("MListGroup"));

                    UpdateElution(xmlDoc, xmlNode);
                }
                else if (xmlElement.Attributes[0].Value.Equals("Equilibration"))
                {
                    xmlElement.Attributes[0].Value = "DlyPhase";
                    if (xmlNode.FirstChild.Name.Equals("MVolAreaBaseFlow"))
                    {
                        RemoveChild(xmlNode, xmlNode.FirstChild);
                    }
                    xmlNode.PrependChild(xmlDoc.CreateElement("MListGroup"));

                    UpdateEquilibration(xmlDoc, xmlNode);
                }
                else if (xmlElement.Attributes[0].Value.Equals("Miscellaneous"))
                {
                    if (xmlNode.FirstChild.Name.Equals("MVolAreaBaseFlow"))
                    {
                        RemoveChild(xmlNode, xmlNode.FirstChild);
                    }
                    xmlNode.PrependChild(xmlDoc.CreateElement("MListGroup"));

                    UpdateMiscellaneous(xmlDoc, xmlNode);
                }
            }

            for(int i=0;i< xmlNodeList.Count;i++)
            {
                if(xmlNodeList[i].Attributes[0].Value.Equals("SystemCIP") || xmlNodeList[i].Attributes[0].Value.Equals("SystemPreparation"))
                {
                    root.RemoveChild(xmlNodeList[i]);
                    i--;
                }
            }


            xmlDoc.Save(m_fileSave);//保存修改的Xml文件内容
        }

        private void UpdateColumnCIP(XmlDocument xmlDoc, XmlNode xmlNode)
        {
            XmlElement xmlElement = (XmlElement)xmlNode;
            if (xmlNode.LastChild.Name.Equals("MFlowValveLength"))
            {
                XmlElement newNode = xmlDoc.CreateElement("BaseGroup");
                newNode.SetAttributeNode(GetXmlAttribute(xmlDoc, "type", "FlowValveLength"));
                foreach (XmlNode childNode in xmlNode.LastChild)
                {
                    newNode.AppendChild(childNode);
                }
                xmlNode.ReplaceChild(newNode, xmlNode.LastChild);
                XmlNode last = xmlNode.LastChild;
                RemoveChild(xmlNode, xmlNode.LastChild);
                xmlNode.FirstChild.PrependChild(last);

                PrependNode(xmlDoc, last, "MIndex", "0");
                PrependNode(xmlDoc, last, "MType", "FlowValveLength");

                foreach (XmlNode childNode in last["MList"])
                {
                    if (childNode["MFillSystem"].InnerText.Equals("false"))
                    {
                        childNode["MFillSystem"].InnerText = "0";
                    }
                    else if (childNode["MFillSystem"].InnerText.Equals("true"))
                    {
                        childNode["MFillSystem"].InnerText = "1";
                    }

                    if (1 == childNode["MBaseTVCV"].ChildNodes.Count)
                    {
                        PrependNode(xmlDoc, childNode["MBaseTVCV"], "MCV", childNode["MBaseTVCV"]["MTVCV"].InnerText);
                        PrependNode(xmlDoc, childNode["MBaseTVCV"], "MV", childNode["MBaseTVCV"]["MTVCV"].InnerText);
                        PrependNode(xmlDoc, childNode["MBaseTVCV"], "MT", childNode["MBaseTVCV"]["MTVCV"].InnerText);
                        PrependNode(xmlDoc, childNode["MBaseTVCV"], "MEnumBase", "T");
                        PrependNode(xmlDoc, childNode["MBaseTVCV"], "MHeaderText", "");
                        PrependNode(xmlDoc, childNode["MBaseTVCV"], "MIndex", "0");
                        PrependNode(xmlDoc, childNode["MBaseTVCV"], "MType", "TVCV");
                    }
                    if (1 == childNode["MFlowVolLen"].ChildNodes.Count)
                    {
                        PrependNode(xmlDoc, childNode["MFlowVolLen"], "MFlowLen", "1");
                        PrependNode(xmlDoc, childNode["MFlowVolLen"], "MFlowVol", "1");
                        PrependNode(xmlDoc, childNode["MFlowVolLen"], "MEnumFlowRate", "MLMIN");
                    }
                }

                xmlNode["MType"].InnerText = "ColumnCIP";

                XmlNode tmp = xmlNode["MNamePhase"];
                xmlNode.RemoveChild(xmlNode["MNamePhase"]);
                xmlNode.InsertAfter(tmp, xmlNode["MType"]);
                xmlNode["MNamePhase"].InnerText = "柱清洗";
                
                tmp = xmlNode["MNameStep"];
                xmlNode.RemoveChild(xmlNode["MNameStep"]);
                xmlNode.InsertAfter(tmp, xmlNode["MNamePhase"]);
                int count = xmlNode["MNameStep"].ChildNodes.Count;
                xmlNode["MNameStep"].RemoveAll();
                for (int i = 1; i <= count; i++)
                {
                    CreateNode(xmlDoc, xmlNode["MNameStep"], "string", "阀路梯度列表" + count + "-" + i);
                }

                InsertAfterNode(xmlDoc, xmlNode, xmlNode["MType"], "MNameType", "柱清洗");
            }
        }

        private void UpdateColumnPerformanceTest(XmlDocument xmlDoc, XmlNode xmlNode)
        {
            XmlElement xmlElement = (XmlElement)xmlNode;
            SwapToFirst(xmlDoc, xmlNode, "MSampleApplicationTech", "SampleApplicationTech");
            SwapToFirst(xmlDoc, xmlNode, "MBPV", "BPVValve");
            SwapToFirst(xmlDoc, xmlNode, "MFlowRate", "FlowRate");

            SwapToIndex(xmlDoc, xmlNode, "MNamePhase", "MType");
            SwapToIndex(xmlDoc, xmlNode, "MNameStep", "MNamePhase");

            foreach (XmlNode childNode in xmlNode["MListGroup"])
            {
                switch(childNode.Attributes[0].Value)
                {
                    case "FlowRate":
                        Rename(xmlDoc, childNode, "MEnableUTSFRAIMS", "MEnableSameMS");
                        if (1 == childNode["MFlowVolLen"].ChildNodes.Count)
                        {
                            PrependNode(xmlDoc, childNode["MFlowVolLen"], "MFlowLen", "1");
                            PrependNode(xmlDoc, childNode["MFlowVolLen"], "MFlowVol", "1");
                            PrependNode(xmlDoc, childNode["MFlowVolLen"], "MEnumFlowRate", "MLMIN");
                        }
                        break;
                    case "BPVValve":
                        childNode["MType"].InnerText = "BPV";
                        childNode.RemoveChild(childNode.ChildNodes[2]);
                        CreateNode(xmlDoc, childNode, "MBPV", "0");
                        break;
                    case "SampleApplicationTech":
                        Rename(xmlDoc, childNode, 2, "MEnumSAT");
                        if (1 == childNode["MSampleTVCV"].ChildNodes.Count)
                        {
                            PrependNode(xmlDoc, childNode["MSampleTVCV"], "MCV", childNode["MSampleTVCV"]["MTVCV"].InnerText);
                            PrependNode(xmlDoc, childNode["MSampleTVCV"], "MV", childNode["MSampleTVCV"]["MTVCV"].InnerText);
                            PrependNode(xmlDoc, childNode["MSampleTVCV"], "MT", childNode["MSampleTVCV"]["MTVCV"].InnerText);
                            PrependNode(xmlDoc, childNode["MSampleTVCV"], "MEnumBase", "T");
                            PrependNode(xmlDoc, childNode["MSampleTVCV"], "MHeaderText", "");
                            PrependNode(xmlDoc, childNode["MSampleTVCV"], "MIndex", "0");
                            PrependNode(xmlDoc, childNode["MSampleTVCV"], "MType", "TVCV");
                        }
                        break;
                }
            }

            xmlNode["MType"].InnerText = "SampleApplication";

            InsertAfterNode(xmlDoc, xmlNode, xmlNode["MType"], "MNameType", "上样");

            xmlNode["MNamePhase"].InnerText = "上样";

            xmlNode["MNameStep"].RemoveAll();
            XmlElement newNode = xmlDoc.CreateElement("string");
            newNode.SetAttributeNode(GetXmlAttribute(xmlDoc, "nil", "true"));
            xmlNode["MNameStep"].AppendChild(newNode);

            xmlNode.RemoveChild(xmlNode["MTestNote"]);
            xmlNode.RemoveChild(xmlNode["MBaseTVCV"]);

            RemoveChild(xmlNode["MStepT"], 1);
            RemoveChild(xmlNode["MStepV"], 1);
            RemoveChild(xmlNode["MStepCV"], 1);
            RemoveChild(xmlNode["MPerA"], 3);
            RemoveChild(xmlNode["MPerA"], 2);
            RemoveChild(xmlNode["MPerB"], 3);
            RemoveChild(xmlNode["MPerB"], 2);
            RemoveChild(xmlNode["MPerC"], 3);
            RemoveChild(xmlNode["MPerC"], 2);
            RemoveChild(xmlNode["MPerD"], 3);
            RemoveChild(xmlNode["MPerD"], 2);
        }

        private void UpdateColumnWash(XmlDocument xmlDoc, XmlNode xmlNode)
        {
            XmlElement xmlElement = (XmlElement)xmlNode;
            SwapToFirst(xmlDoc, xmlNode, "MColl", "CollValveCollector");
            SwapToFirst(xmlDoc, xmlNode, "MPHCDUVUntil", "PHCDUVUntil");
            SwapToFirst(xmlDoc, xmlNode, "MValveSelection", "ValveSelection");
            SwapToFirst(xmlDoc, xmlNode, "MFlowRate", "FlowRate");

            SwapToIndex(xmlDoc, xmlNode, "MNamePhase", "MType");
            SwapToIndex(xmlDoc, xmlNode, "MNameStep", "MNamePhase");

            foreach (XmlNode childNode in xmlNode["MListGroup"])
            {
                switch (childNode.Attributes[0].Value)
                {
                    case "FlowRate":
                        Rename(xmlDoc, childNode, "MEnableUTSFRAIMS", "MEnableSameMS");
                        if (1 == childNode["MFlowVolLen"].ChildNodes.Count)
                        {
                            PrependNode(xmlDoc, childNode["MFlowVolLen"], "MFlowLen", "1");
                            PrependNode(xmlDoc, childNode["MFlowVolLen"], "MFlowVol", "1");
                            PrependNode(xmlDoc, childNode["MFlowVolLen"], "MEnumFlowRate", "MLMIN");
                        }
                        break;
                    case "ValveSelection":
                        Rename(xmlDoc, childNode, "MEnableUTSIAIMS", "MEnableSameMS");
                        Rename(xmlDoc, childNode, "MEnableFTSWTSB", "MEnableWash");

                        if (null == childNode["MBPV"])
                        {
                            InsertAfterNode(xmlDoc, childNode, childNode["MInD"], "MBPV", "0");
                        }

                        SwapToIndex(xmlDoc, childNode, "MPerD", "MBPV");
                        SwapToIndex(xmlDoc, childNode, "MPerC", "MBPV");
                        SwapToIndex(xmlDoc, childNode, "MPerB", "MBPV");

                        InsertAfterNode(xmlDoc, childNode, childNode["MBPV"], "MVisibPer", "true");
                        InsertAfterNode(xmlDoc, childNode, childNode["MPerD"], "MVisibWash", "true");
                        break;
                    case "PHCDUVUntil":
                        InsertAfterNode(xmlDoc, childNode, childNode["MIndex"], "MHeaderText", "冲洗至");
                        Rename(xmlDoc, childNode, 3, "MUntilType");
                        if (1 == childNode["MTotalTVCV"].ChildNodes.Count)
                        {
                            PrependNode(xmlDoc, childNode["MTotalTVCV"], "MCV", childNode["MTotalTVCV"]["MTVCV"].InnerText);
                            PrependNode(xmlDoc, childNode["MTotalTVCV"], "MV", childNode["MTotalTVCV"]["MTVCV"].InnerText);
                            PrependNode(xmlDoc, childNode["MTotalTVCV"], "MT", childNode["MTotalTVCV"]["MTVCV"].InnerText);
                            PrependNode(xmlDoc, childNode["MTotalTVCV"], "MEnumBase", "T");
                            PrependNode(xmlDoc, childNode["MTotalTVCV"], "MHeaderText", "");
                            PrependNode(xmlDoc, childNode["MTotalTVCV"], "MIndex", "0");
                            PrependNode(xmlDoc, childNode["MTotalTVCV"], "MType", "TVCV");
                        }
                        childNode["MJudgeIndex"].InnerText = "Stable";
                        if (1 == childNode["MMaxTVCV"].ChildNodes.Count)
                        {
                            PrependNode(xmlDoc, childNode["MMaxTVCV"], "MCV", childNode["MMaxTVCV"]["MTVCV"].InnerText);
                            PrependNode(xmlDoc, childNode["MMaxTVCV"], "MV", childNode["MMaxTVCV"]["MTVCV"].InnerText);
                            PrependNode(xmlDoc, childNode["MMaxTVCV"], "MT", childNode["MMaxTVCV"]["MTVCV"].InnerText);
                            PrependNode(xmlDoc, childNode["MMaxTVCV"], "MEnumBase", "T");
                            PrependNode(xmlDoc, childNode["MMaxTVCV"], "MHeaderText", "");
                            PrependNode(xmlDoc, childNode["MMaxTVCV"], "MIndex", "0");
                            PrependNode(xmlDoc, childNode["MMaxTVCV"], "MType", "TVCV");
                        }
                        break;
                    case "CollValveCollector":
                        SwapToIndex(xmlDoc, childNode, "MEnabledColl", "MIndex");
                        RemoveChildIteration(childNode, "MDirect");
                        RemoveChildIteration(childNode, "MNameList");
                        RemoveChildIteration(childNode, "MObj");
                        RemoveChildIteration(childNode, "MStart");
                        RemoveChildIteration(childNode, "MShowInfo", "循环方向 : 升序\r\n");
                        RemoveChildIteration(childNode, "MShowInfo", "循环方向 : 降序\r\n");
                        RemoveChildIteration(childNode, "MShowInfo", "循环方向 : 固定\r\n");
                        RemoveChildIteration(childNode, "MShowInfo", "循环方向 : 升序\n");
                        RemoveChildIteration(childNode, "MShowInfo", "循环方向 : 降序\n");
                        RemoveChildIteration(childNode, "MShowInfo", "循环方向 : 固定\n");
                        RemoveChildIteration(childNode, "MShowInfo", "循环方向 : 升序\r");
                        RemoveChildIteration(childNode, "MShowInfo", "循环方向 : 降序\r");
                        RemoveChildIteration(childNode, "MShowInfo", "循环方向 : 固定\r");
                        RemoveChildIteration(childNode, "MShowInfo", "循环方向 : 升序");
                        RemoveChildIteration(childNode, "MShowInfo", "循环方向 : 降序");
                        RemoveChildIteration(childNode, "MShowInfo", "循环方向 : 固定");
                        for (int i = 0; i < childNode["MValve"]["MList"].ChildNodes.Count; i++)
                        {
                            XmlNode tmp = childNode["MValve"]["MList"].ChildNodes[i];

                            tmp["MCond"]["MRelation"].InnerText = "Only";
                            tmp["MLoop"]["MRelation"].InnerText = "Only";
                            tmp.RemoveChild(tmp["MPositionStart"]);

                            InsertAfterNode(xmlDoc, tmp, tmp["MPositionType"], "MPositionStart", "Out");
                            InsertAfterNode(xmlDoc, tmp, tmp["MPositionStart"], "MStartIndex", "1");
                        }
                        for (int i = 0; i < childNode["MCollector"]["MList"].ChildNodes.Count; i++)
                        {
                            XmlNode tmp = childNode["MCollector"]["MList"].ChildNodes[i];

                            tmp["MCond"]["MRelation"].InnerText = "Only";
                            tmp["MLoop"]["MRelation"].InnerText = "Only";
                            tmp.RemoveChild(tmp["MPositionStart"]);

                            InsertAfterNode(xmlDoc, tmp, tmp["MPositionType"], "MPositionStart", "Left");
                            InsertAfterNode(xmlDoc, tmp, tmp["MPositionStart"], "MStartIndex", "1");
                        }
                        ReplaceChildIteration(childNode, "MShowInfo", "收集起始 : 指定", "收集方式 : 循环");
                        ReplaceChildIteration(childNode, "MShowInfo", "起始位置 : ", "收集起始 : ");
                        ReplaceChildIteration(childNode, "MShowInfo", "L_", "L");
                        ReplaceChildIteration(childNode, "MShowInfo", "R_", "R");
                        break;
                }
            }

            xmlNode["MType"].InnerText = "ColumnWash";

            InsertAfterNode(xmlDoc, xmlNode, xmlNode["MType"], "MNameType", "柱洗");

            xmlNode["MNamePhase"].InnerText = "柱洗";

            xmlNode["MNameStep"]["string"].InnerText = "labMECW_Wash";
        }

        private void UpdateElution(XmlDocument xmlDoc, XmlNode xmlNode)
        {
            XmlElement xmlElement = (XmlElement)xmlNode;
            SwapToFirst(xmlDoc, xmlNode, "MColl", "CollValveCollector");
            SwapToFirst(xmlDoc, xmlNode, "MFlowRatePer", "FlowRatePer");
            SwapToFirst(xmlDoc, xmlNode, "MValveSelection", "ValveSelection");
            SwapToFirst(xmlDoc, xmlNode, "MFlowRate", "FlowRate");

            SwapToIndex(xmlDoc, xmlNode, "MNamePhase", "MType");
            SwapToIndex(xmlDoc, xmlNode, "MNameStep", "MNamePhase");

            foreach (XmlNode childNode in xmlNode["MListGroup"])
            {
                switch (childNode.Attributes[0].Value)
                {
                    case "FlowRate":
                        Rename(xmlDoc, childNode, "MEnableUTSFRAIMS", "MEnableSameMS");
                        if (1 == childNode["MFlowVolLen"].ChildNodes.Count)
                        {
                            PrependNode(xmlDoc, childNode["MFlowVolLen"], "MFlowLen", "1");
                            PrependNode(xmlDoc, childNode["MFlowVolLen"], "MFlowVol", "1");
                            PrependNode(xmlDoc, childNode["MFlowVolLen"], "MEnumFlowRate", "MLMIN");
                        }
                        break;
                    case "ValveSelection":
                        Rename(xmlDoc, childNode, "MEnableUTSIAIMS", "MEnableSameMS");
                        Rename(xmlDoc, childNode, "MEnableFTSWTSB", "MEnableWash");

                        if (null == childNode["MBPV"])
                        {
                            InsertAfterNode(xmlDoc, childNode, childNode["MInD"], "MBPV", "0");
                        }

                        SwapToIndex(xmlDoc, childNode, "MPerD", "MBPV");
                        SwapToIndex(xmlDoc, childNode, "MPerC", "MBPV");
                        SwapToIndex(xmlDoc, childNode, "MPerB", "MBPV");

                        InsertAfterNode(xmlDoc, childNode, childNode["MBPV"], "MVisibPer", "true");
                        InsertAfterNode(xmlDoc, childNode, childNode["MPerD"], "MVisibWash", "true");
                        break;
                    case "FlowRatePer":
                        foreach (XmlNode childNode2 in childNode["MList"])
                        {
                            if (childNode2["MFillSystem"].InnerText.Equals("false"))
                            {
                                childNode2["MFillSystem"].InnerText = "0";
                            }
                            else if (childNode2["MFillSystem"].InnerText.Equals("true"))
                            {
                                childNode2["MFillSystem"].InnerText = "1";
                            }

                            if (1 == childNode2["MBaseTVCV"].ChildNodes.Count)
                            {
                                PrependNode(xmlDoc, childNode2["MBaseTVCV"], "MCV", childNode2["MBaseTVCV"]["MTVCV"].InnerText);
                                PrependNode(xmlDoc, childNode2["MBaseTVCV"], "MV", childNode2["MBaseTVCV"]["MTVCV"].InnerText);
                                PrependNode(xmlDoc, childNode2["MBaseTVCV"], "MT", childNode2["MBaseTVCV"]["MTVCV"].InnerText);
                                PrependNode(xmlDoc, childNode2["MBaseTVCV"], "MEnumBase", "T");
                                PrependNode(xmlDoc, childNode2["MBaseTVCV"], "MHeaderText", "");
                                PrependNode(xmlDoc, childNode2["MBaseTVCV"], "MIndex", "0");
                                PrependNode(xmlDoc, childNode2["MBaseTVCV"], "MType", "TVCV");
                            }
                        }
                        break;
                    case "CollValveCollector":
                        SwapToIndex(xmlDoc, childNode, "MEnabledColl", "MIndex");
                        RemoveChildIteration(childNode, "MDirect");
                        RemoveChildIteration(childNode, "MNameList");
                        RemoveChildIteration(childNode, "MObj");
                        RemoveChildIteration(childNode, "MStart");
                        RemoveChildIteration(childNode, "MShowInfo", "循环方向 : 升序\r\n");
                        RemoveChildIteration(childNode, "MShowInfo", "循环方向 : 降序\r\n");
                        RemoveChildIteration(childNode, "MShowInfo", "循环方向 : 固定\r\n");
                        RemoveChildIteration(childNode, "MShowInfo", "循环方向 : 升序\n");
                        RemoveChildIteration(childNode, "MShowInfo", "循环方向 : 降序\n");
                        RemoveChildIteration(childNode, "MShowInfo", "循环方向 : 固定\n");
                        RemoveChildIteration(childNode, "MShowInfo", "循环方向 : 升序\r");
                        RemoveChildIteration(childNode, "MShowInfo", "循环方向 : 降序\r");
                        RemoveChildIteration(childNode, "MShowInfo", "循环方向 : 固定\r");
                        RemoveChildIteration(childNode, "MShowInfo", "循环方向 : 升序");
                        RemoveChildIteration(childNode, "MShowInfo", "循环方向 : 降序");
                        RemoveChildIteration(childNode, "MShowInfo", "循环方向 : 固定");
                        for (int i = 0; i < childNode["MValve"]["MList"].ChildNodes.Count; i++)
                        {
                            XmlNode tmp = childNode["MValve"]["MList"].ChildNodes[i];

                            tmp["MCond"]["MRelation"].InnerText = "Only";
                            tmp["MLoop"]["MRelation"].InnerText = "Only";
                            tmp.RemoveChild(tmp["MPositionStart"]);

                            InsertAfterNode(xmlDoc, tmp, tmp["MPositionType"], "MPositionStart", "Out");
                            InsertAfterNode(xmlDoc, tmp, tmp["MPositionStart"], "MStartIndex", "1");
                        }
                        for (int i = 0; i < childNode["MCollector"]["MList"].ChildNodes.Count; i++)
                        {
                            XmlNode tmp = childNode["MCollector"]["MList"].ChildNodes[i];

                            tmp["MCond"]["MRelation"].InnerText = "Only";
                            tmp["MLoop"]["MRelation"].InnerText = "Only";
                            tmp.RemoveChild(tmp["MPositionStart"]);

                            InsertAfterNode(xmlDoc, tmp, tmp["MPositionType"], "MPositionStart", "Left");
                            InsertAfterNode(xmlDoc, tmp, tmp["MPositionStart"], "MStartIndex", "1");
                        }
                        ReplaceChildIteration(childNode, "MShowInfo", "收集起始 : 指定", "收集方式 : 循环");
                        ReplaceChildIteration(childNode, "MShowInfo", "起始位置 : ", "收集起始 : ");
                        ReplaceChildIteration(childNode, "MShowInfo", "L_", "L");
                        ReplaceChildIteration(childNode, "MShowInfo", "R_", "R");
                        break;
                }
            }

            xmlNode["MType"].InnerText = "Elution";

            InsertAfterNode(xmlDoc, xmlNode, xmlNode["MType"], "MNameType", "洗脱");

            xmlNode["MNamePhase"].InnerText = "洗脱";

            int count = xmlNode["MNameStep"].ChildNodes.Count;
            xmlNode["MNameStep"].RemoveAll();
            for (int i = 1; i <= count; i++)
            {
                CreateNode(xmlDoc, xmlNode["MNameStep"], "string", "Gradient segment_(" + i + ")");
            }
        }

        private void UpdateEquilibration(XmlDocument xmlDoc, XmlNode xmlNode)
        {
            XmlElement xmlElement = (XmlElement)xmlNode;
            SwapToFirst(xmlDoc, xmlNode, "MPHCDUVUntil", "PHCDUVUntil");
            SwapToFirst(xmlDoc, xmlNode, "MValveSelection", "ValveSelection");
            SwapToFirst(xmlDoc, xmlNode, "MFlowRate", "FlowRate");
            SwapToFirst(xmlDoc, xmlNode, "MEnableResetUVMonitor", "UVReset");

            SwapToIndex(xmlDoc, xmlNode, "MNamePhase", "MType");
            SwapToIndex(xmlDoc, xmlNode, "MNameStep", "MNamePhase");

            foreach (XmlNode childNode in xmlNode["MListGroup"])
            {
                switch (childNode.Attributes[0].Value)
                {
                    case "UVReset":
                        childNode.RemoveChild(childNode.ChildNodes[2]);
                        CreateNode(xmlDoc, childNode, "MEnableResetUV", "true");
                        break;
                    case "FlowRate":
                        Rename(xmlDoc, childNode, "MEnableUTSFRAIMS", "MEnableSameMS");
                        if (1 == childNode["MFlowVolLen"].ChildNodes.Count)
                        {
                            PrependNode(xmlDoc, childNode["MFlowVolLen"], "MFlowLen", "1");
                            PrependNode(xmlDoc, childNode["MFlowVolLen"], "MFlowVol", "1");
                            PrependNode(xmlDoc, childNode["MFlowVolLen"], "MEnumFlowRate", "MLMIN");
                        }
                        break;
                    case "ValveSelection":
                        Rename(xmlDoc, childNode, "MEnableUTSIAIMS", "MEnableSameMS");
                        Rename(xmlDoc, childNode, "MEnableFTSWTSB", "MEnableWash");

                        if (null == childNode["MBPV"])
                        {
                            InsertAfterNode(xmlDoc, childNode, childNode["MInD"], "MBPV", "0");
                        }

                        SwapToIndex(xmlDoc, childNode, "MPerD", "MBPV");
                        SwapToIndex(xmlDoc, childNode, "MPerC", "MBPV");
                        SwapToIndex(xmlDoc, childNode, "MPerB", "MBPV");

                        InsertAfterNode(xmlDoc, childNode, childNode["MBPV"], "MVisibPer", "true");
                        InsertAfterNode(xmlDoc, childNode, childNode["MPerD"], "MVisibWash", "true");
                        break;
                    case "PHCDUVUntil":
                        InsertAfterNode(xmlDoc, childNode, childNode["MIndex"], "MHeaderText", "平衡至");
                        Rename(xmlDoc, childNode, 3, "MUntilType");
                        if (1 == childNode["MTotalTVCV"].ChildNodes.Count)
                        {
                            PrependNode(xmlDoc, childNode["MTotalTVCV"], "MCV", childNode["MTotalTVCV"]["MTVCV"].InnerText);
                            PrependNode(xmlDoc, childNode["MTotalTVCV"], "MV", childNode["MTotalTVCV"]["MTVCV"].InnerText);
                            PrependNode(xmlDoc, childNode["MTotalTVCV"], "MT", childNode["MTotalTVCV"]["MTVCV"].InnerText);
                            PrependNode(xmlDoc, childNode["MTotalTVCV"], "MEnumBase", "T");
                            PrependNode(xmlDoc, childNode["MTotalTVCV"], "MHeaderText", "");
                            PrependNode(xmlDoc, childNode["MTotalTVCV"], "MIndex", "0");
                            PrependNode(xmlDoc, childNode["MTotalTVCV"], "MType", "TVCV");
                        }
                        childNode["MJudgeIndex"].InnerText = "Stable";
                        if (1 == childNode["MMaxTVCV"].ChildNodes.Count)
                        {
                            PrependNode(xmlDoc, childNode["MMaxTVCV"], "MCV", childNode["MMaxTVCV"]["MTVCV"].InnerText);
                            PrependNode(xmlDoc, childNode["MMaxTVCV"], "MV", childNode["MMaxTVCV"]["MTVCV"].InnerText);
                            PrependNode(xmlDoc, childNode["MMaxTVCV"], "MT", childNode["MMaxTVCV"]["MTVCV"].InnerText);
                            PrependNode(xmlDoc, childNode["MMaxTVCV"], "MEnumBase", "T");
                            PrependNode(xmlDoc, childNode["MMaxTVCV"], "MHeaderText", "");
                            PrependNode(xmlDoc, childNode["MMaxTVCV"], "MIndex", "0");
                            PrependNode(xmlDoc, childNode["MMaxTVCV"], "MType", "TVCV");
                        }
                        break;
                }
            }

            xmlNode["MType"].InnerText = "Equilibration";

            InsertAfterNode(xmlDoc, xmlNode, xmlNode["MType"], "MNameType", "平衡");

            xmlNode["MNamePhase"].InnerText = "平衡";

            xmlNode["MNameStep"]["string"].InnerText = "labMECW_Wash";
        }

        private void UpdateMiscellaneous(XmlDocument xmlDoc, XmlNode xmlNode)
        {
            XmlElement xmlElement = (XmlElement)xmlNode;

            SwapToIndex(xmlDoc, xmlNode, "MNamePhase", "MType");
            SwapToIndex(xmlDoc, xmlNode, "MNameStep", "MNamePhase");

            if (1 == xmlNode["MMethodDelay"].ChildNodes.Count)
            {
                PrependNode(xmlDoc, xmlNode["MMethodDelay"], "MCV", xmlNode["MMethodDelay"]["MTVCV"].InnerText);
                PrependNode(xmlDoc, xmlNode["MMethodDelay"], "MV", xmlNode["MMethodDelay"]["MTVCV"].InnerText);
                PrependNode(xmlDoc, xmlNode["MMethodDelay"], "MT", xmlNode["MMethodDelay"]["MTVCV"].InnerText);
                PrependNode(xmlDoc, xmlNode["MMethodDelay"], "MEnumBase", "T");
                PrependNode(xmlDoc, xmlNode["MMethodDelay"], "MHeaderText", "");
                PrependNode(xmlDoc, xmlNode["MMethodDelay"], "MIndex", "0");
                PrependNode(xmlDoc, xmlNode["MMethodDelay"], "MType", "TVCV");
            }

            xmlNode["MType"].InnerText = "Miscellaneous";

            InsertAfterNode(xmlDoc, xmlNode, xmlNode["MType"], "MNameType", "其它");

            xmlNode["MNamePhase"].InnerText = "其它";
        }

        private XmlAttribute GetXmlAttribute(XmlDocument xmlDoc, string name, string valve)
        {
            XmlAttribute attr = xmlDoc.CreateAttribute("xsi", name, "http://www.w3.org/2001/XMLSchema-instance");
            attr.Value = valve;

            return attr;
        }


        private void SwapToFirst(XmlDocument xmlDoc, XmlNode xmlNode, string nameold, string namenew)
        {
            if (null != xmlNode[nameold])
            {
                XmlNode curr = xmlNode[nameold];
                RemoveChild(xmlNode, curr);
                xmlNode["MListGroup"].PrependChild(curr);
                XmlElement newNode = xmlDoc.CreateElement("BaseGroup");
                newNode.SetAttributeNode(GetXmlAttribute(xmlDoc, "type", namenew));
                while (0 != xmlNode["MListGroup"][nameold].ChildNodes.Count)
                {
                    newNode.AppendChild(xmlNode["MListGroup"][nameold].RemoveChild(xmlNode["MListGroup"][nameold].FirstChild));
                }
                xmlNode["MListGroup"].ReplaceChild(newNode, xmlNode["MListGroup"][nameold]);

                PrependNode(xmlDoc, newNode, "MIndex", "0");
                PrependNode(xmlDoc, newNode, "MType", namenew);
            }
        }

        private void SwapToIndex(XmlDocument xmlDoc, XmlNode xmlNode, string namecurr, string nameindex)
        {
            xmlNode.InsertAfter(xmlNode.RemoveChild(xmlNode[namecurr]), xmlNode[nameindex]);
        }

        private void Rename(XmlDocument xmlDoc, XmlNode xmlNode, string nameold, string namenew)
        {
            if (null != xmlNode[nameold])
            {
                XmlElement newNode = xmlDoc.CreateElement(namenew);
                while (0 != xmlNode[nameold].ChildNodes.Count)
                {
                    newNode.AppendChild(xmlNode[nameold].RemoveChild(xmlNode[nameold].FirstChild));
                }
                xmlNode.ReplaceChild(newNode, xmlNode[nameold]);
            }
        }
        private void Rename(XmlDocument xmlDoc, XmlNode xmlNode, int nameold, string namenew)
        {
            if (null != xmlNode.ChildNodes[nameold])
            {
                XmlElement newNode = xmlDoc.CreateElement(namenew);
                while (0 != xmlNode.ChildNodes[nameold].ChildNodes.Count)
                {
                    newNode.AppendChild(xmlNode.ChildNodes[nameold].RemoveChild(xmlNode.ChildNodes[nameold].FirstChild));
                }
                xmlNode.ReplaceChild(newNode, xmlNode.ChildNodes[nameold]);
            }
        }

        private void RemoveChild(XmlNode root , XmlNode child)
        {
            if (null != root && null != child)
            {
                root.RemoveChild(child);
            }
        }

        private void RemoveChild(XmlNode root, int index)
        {
            if (null != root && index < root.ChildNodes.Count)
            {
                root.RemoveChild(root.ChildNodes[index]);
            }
        }

        private void RemoveChildIteration(XmlNode root, string name)
        {
            for (int i = 0; i < root.ChildNodes.Count; i++)
            {
                if (root.ChildNodes[i].Name.Equals(name))
                {
                    root.RemoveChild(root.ChildNodes[i]);
                    i--;
                }
                else
                {
                    RemoveChildIteration(root.ChildNodes[i], name);
                }
            }
        }

        private void RemoveChildIteration(XmlNode root, string name, string del)
        {
            for (int i = 0; i < root.ChildNodes.Count; i++)
            {
                if (root.ChildNodes[i].Name.Equals(name))
                {
                    root.ChildNodes[i].InnerText = root.ChildNodes[i].InnerText.Replace(del, "");
                }
                else
                {
                    RemoveChildIteration(root.ChildNodes[i], name, del);
                }
            }
        }

        private void ReplaceChildIteration(XmlNode root, string name, string replaceOld, string replacenew)
        {
            for (int i = 0; i < root.ChildNodes.Count; i++)
            {
                if (root.ChildNodes[i].Name.Equals(name))
                {
                    root.ChildNodes[i].InnerText = root.ChildNodes[i].InnerText.Replace(replaceOld, replacenew);
                }
                else
                {
                    ReplaceChildIteration(root.ChildNodes[i], name, replaceOld, replacenew);
                }
            }
        }

        /// <summary>    
        /// 创建节点    
        /// </summary>    
        /// <param name="xmlDoc">xml文档</param>    
        /// <param name="parentNode">Xml父节点</param>    
        /// <param name="name">节点名</param>    
        /// <param name="value">节点值</param>    
        ///   
        public void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            //创建对应Xml节点元素
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.AppendChild(node);
        }
        public void PrependNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            //创建对应Xml节点元素
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.PrependChild(node);
        }
        public void InsertBeforeNode(XmlDocument xmlDoc, XmlNode parentNode, XmlNode selectNode, string name, string value)
        {
            //创建对应Xml节点元素
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.InsertBefore(node, selectNode);
        }
        public void InsertAfterNode(XmlDocument xmlDoc, XmlNode parentNode, XmlNode selectNode, string name, string value)
        {
            //创建对应Xml节点元素
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.InsertAfter(node, selectNode);
        }
    }
}
