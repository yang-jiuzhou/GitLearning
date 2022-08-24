using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio.ColumnList
{
    /**
     * ClassName: Details
     * Description: 色谱柱信息详细列举
     * Version: 1.0
     * Create:  2018/05/16
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class Details
    {
        public ObservableCollection<ParametersValueUnit> MList = new ObservableCollection<ParametersValueUnit>();


        /// <summary>
        /// 构造函数
        /// </summary>
        public Details()
        {
            foreach (EnumDetails it in Enum.GetValues(typeof(EnumDetails)))
            {
                ParametersValueUnit item = new ParametersValueUnit((int)it);
                item.MName = Share.ReadXaml.GetEnum(it, "C_");
                switch (it)
                {
                    case EnumDetails.DHD:
                    case EnumDetails.DBH:
                        item.MUnit = Share.DlyBase.SC_LENGTHUNIT;
                        break;
                    case EnumDetails.DTLR:
                        item.MUnit = Share.DlyBase.SC_WEIGHTUNIT;
                        break;
                    case EnumDetails.DAPD:
                        item.MUnit = Share.DlyBase.SC_PARTICLEUNIT;
                        break;
                    case EnumDetails.DMWR:
                        item.MUnit = Share.DlyBase.SC_MOLECULARUNIT;
                        break;
                }
                MList.Add(item);
            }
        }

        /// <summary>
        /// 比较是否相同
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Compared(Details other)
        {
            if (null == other)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < MList.Count; i++)
                {
                    if (!MList[i].Compared(other.MList[i]))
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }

    /// <summary>
    /// 详细列举的属性枚举
    /// </summary>
    enum EnumDetails
    {
        DHD,
        DBH,
        DTLR,
        DTLV,
        DVV,
        DTPWAB,
        DAPD,
        DMWR
    }

    class EnumDetailsInfo
    {
        public const int Count = 8;
    }
}
