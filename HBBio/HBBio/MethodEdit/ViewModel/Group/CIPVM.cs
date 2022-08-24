using HBBio.Communication;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    public class CIPVM : BaseGroupVM
    {
        public CIP MItem 
        { 
            get
            {
                return m_item;
            }
            set
            {
                m_item = value;
                MType = value.MType;

                MFlowRate = new FlowRateVM(MMethodBaseValue);
                MFlowRate.MItem = value.MFlowRate;
                foreach(var it in value.MListInA)
                {
                    CIPItemVM item = new CIPItemVM();
                    item.MItem = it;
                    item.MCheckHandler += CalCount;
                    MListInA.Add(item);
                }
                foreach (var it in value.MListInB)
                {
                    CIPItemVM item = new CIPItemVM();
                    item.MItem = it;
                    item.MCheckHandler += CalCount;
                    MListInB.Add(item); 
                }
                foreach (var it in value.MListInC)
                {
                    CIPItemVM item = new CIPItemVM();
                    item.MItem = it;
                    item.MCheckHandler += CalCount;
                    MListInC.Add(item);
                }
                foreach (var it in value.MListInD)
                {
                    CIPItemVM item = new CIPItemVM();
                    item.MItem = it;
                    item.MCheckHandler += CalCount;
                    MListInD.Add(item);
                }
                foreach (var it in value.MListInS)
                {
                    CIPItemVM item = new CIPItemVM();
                    item.MItem = it;
                    item.MCheckHandler += CalCount;
                    MListInS.Add(item);
                }
                foreach (var it in value.MListCPV)
                {
                    CIPItemVM item = new CIPItemVM();
                    item.MItem = it;
                    item.MCheckHandler += CalCount;
                    MListCPV.Add(item);
                }
                foreach (var it in value.MListOut)
                {
                    CIPItemVM item = new CIPItemVM();
                    item.MItem = it;
                    item.MCheckHandler += CalCount;
                    MListOut.Add(item);
                }
            }
        }
        public string MNote
        {
            get
            {
                return MItem.MNote;
            }
            set
            {
                MItem.MNote = value;
            }
        }
        public bool MPause
        {
            get
            {
                return MItem.MPause;
            }
            set
            {
                MItem.MPause = value;
            }
        }
        public FlowRateVM MFlowRate { get; set; }
        public double MVolumePerPosition
        {
            get
            {
                return MItem.MVolumePerPosition;
            }
            set
            {
                MItem.MVolumePerPosition = value;
                MVolumeTotal = MVolumePerPosition * MCount;
            }
        }
        public int MCount
        {
            get
            {
                return m_count;
            }
            set
            {
                m_count = value;
                MVolumeTotal = MVolumePerPosition * MCount;

            }
        }
        public List<CIPItemVM> MListInA { get; set; }
        public List<CIPItemVM> MListInB { get; set; }
        public List<CIPItemVM> MListInC { get; set; }
        public List<CIPItemVM> MListInD { get; set; }
        public List<CIPItemVM> MListInS { get; set; }
        public List<CIPItemVM> MListCPV { get; set; }
        public List<CIPItemVM> MListOut { get; set; }
        public double MVolumeTotal
        {
            get
            {
                return MItem.MVolumeTotal;
            }
            set
            {
                MItem.MVolumeTotal = value;
                OnPropertyChanged("MVolumeTotal");
            }
        }

        private CIP m_item = null;
        private int m_count = 1;


        /// <summary>
        /// 构造函数
        /// </summary>
        public CIPVM(MethodBaseValue methodBaseValue) : base(methodBaseValue)
        {
            MListInA = new List<CIPItemVM>();
            MListInB = new List<CIPItemVM>();
            MListInC = new List<CIPItemVM>();
            MListInD = new List<CIPItemVM>();
            MListInS = new List<CIPItemVM>();
            MListCPV = new List<CIPItemVM>();
            MListOut = new List<CIPItemVM>();
        }

        /// <summary>
        /// 改变基本单位
        /// </summary>
        /// <param name="methodBaseValue"></param>
        public override void ChangeEnumBase(MethodBaseValue methodBaseValue)
        {
            MMethodBaseValue = methodBaseValue;
            MFlowRate.ChangeEnumBase(methodBaseValue);
        }

        /// <summary>
        /// 计算总量
        /// </summary>
        /// <param name="plusMinus"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        private void CalCount(object plusMinus, object type, object name)
        {
            int countIn = 0;
            int countCPV = 0;
            int countOut = 0;

            foreach (var it in MListInA)
            {
                if (it.MIsSelected)
                {
                    countIn++;
                }
            }
            foreach (var it in MListInB)
            {
                if (it.MIsSelected)
                {
                    countIn++;
                }
            }
            foreach (var it in MListInC)
            {
                if (it.MIsSelected)
                {
                    countIn++;
                }
            }
            foreach (var it in MListInD)
            {
                if (it.MIsSelected)
                {
                    countIn++;
                }
            }
            foreach (var it in MListInS)
            {
                if (it.MIsSelected)
                {
                    countIn++;
                }
            }
            if (0 == countIn)
            {
                switch ((ENUMValveName)type)
                {
                    case ENUMValveName.InA: MListInA[EnumInAInfo.NameList.ToList().IndexOf((string)name)].MIsSelected = true; break;
                    case ENUMValveName.InB: MListInB[EnumInBInfo.NameList.ToList().IndexOf((string)name)].MIsSelected = true; break;
                    case ENUMValveName.InC: MListInC[EnumInCInfo.NameList.ToList().IndexOf((string)name)].MIsSelected = true; break;
                    case ENUMValveName.InD: MListInD[EnumInDInfo.NameList.ToList().IndexOf((string)name)].MIsSelected = true; break;
                    case ENUMValveName.InS: MListInS[EnumInSInfo.NameList.ToList().IndexOf((string)name)].MIsSelected = true; break;
                }
            }

            foreach (var it in MListCPV)
            {
                if (it.MIsSelected)
                {
                    countCPV++;
                }
            }

            foreach (var it in MListOut)
            {
                if (it.MIsSelected)
                {
                    countOut++;
                }
            }
            if (0 == countOut)
            {
                switch ((ENUMValveName)type)
                {
                    case ENUMValveName.Out: MListOut[EnumOutInfo.NameList.ToList().IndexOf((string)name)].MIsSelected = true; break;
                }
            }

            MCount = Math.Max(countIn, Math.Max(countCPV, countOut));
        }
    }
}
