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
     * ClassName: RunParameters
     * Description: 色谱柱信息运行参数
     * Version: 1.0
     * Create:  2018/05/16
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class RunParameters
    {
        public ObservableCollection<ParametersValueUnit> MList = new ObservableCollection<ParametersValueUnit>();


        /// <summary>
        /// 构造函数
        /// </summary>
        public RunParameters()
        {
            foreach (EnumRunParameters it in Enum.GetValues(typeof(EnumRunParameters)))
            {
                ParametersValueUnit item = new ParametersValueUnit((int)it);
                item.MName = Share.ReadXaml.GetEnum(it, "C_");
                switch (it)
                {
                    case EnumRunParameters.RPTec:
                        item.MCbox = Share.ReadXaml.GetEnumList<EnumTechnique>("C_").ToArray();
                        break;
                    case EnumRunParameters.RPCVU:
                        item.MCbox = Share.EnumVolumeInfo.NameList;
                        break;
                    case EnumRunParameters.RPCV:
                        item.MReadOnly = true;
                        break;
                    case EnumRunParameters.RPPTU:
                        item.MCbox = Share.EnumPressureInfo.NameList;
                        break;
                    case EnumRunParameters.RPDLFIT:
                    case EnumRunParameters.RPLFIT:
                        item.MReadOnly = true;
                        item.MUnit = Share.DlyBase.SC_LINEARFITUNIT;
                        break;
                }
                MList.Add(item);
            }
        }

        /// <summary>
        /// 比较是否相等
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Compared(RunParameters other)
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
    /// 运行参数的属性枚举
    /// </summary>
    enum EnumRunParameters
    {
        RPTec,
        RPCVU,
        RPCV,
        RPPTU,
        RPPPT,
        RPDPT,
        RPDFIT,
        RPFIT,
        RPDLFIT,
        RPLFIT,
        RPMINPHS,
        RPMAXPHS,
        RPMINPHL,
        RPMAXPHL
    }

    class EnumRunParametersInfo
    {
        public const int Count = 14;
    }

    /// <summary>
    /// 依赖技术的枚举
    /// </summary>
    enum EnumTechnique
    {
        Any,
        Affinity,
        AnionExchange,
        CationExchange,
        Chromatofocusing,
        Desalting,
        GelFiltration,
        HIC,
        RPC
    }
}
