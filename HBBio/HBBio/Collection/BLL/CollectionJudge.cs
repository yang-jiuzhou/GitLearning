using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Collection
{
    class CollectionJudge
    {
        public static EnumNOIngFinish JudgeObjectMulti(ref EnumNOIngFinish status1, ref EnumNOIngFinish status2,
            double[] listTVCV, List<double> listVal, List<double> listSlope, CollectionObjectMulti item,
            double compare1, ref double compare2)
        {
            switch (item.MRelation)
            {
                case EnumRelation.Only:
                    JudgeTVCVPHCDUV(ref status1, listTVCV, listVal, listSlope, item.MObj1, compare1);
                    break;
                default:
                    JudgeTVCVPHCDUV(ref status1, listTVCV, listVal, listSlope, item.MObj1, compare1);
                    JudgeTVCVPHCDUV(ref status2, listTVCV, listVal, listSlope, item.MObj2, compare2);
                    break;
            }

            EnumNOIngFinish result = JudgeFinish(ref status1, ref status2, item.MRelation);
            if (EnumNOIngFinish.NULL == status2 && item.MObj2.MType <= 2)
            {
                compare2 = listTVCV[(int)item.MObj2.MType];
            }

            return result;
        }

        /// <summary>
        /// 判断双对象的完成状态
        /// </summary>
        /// <param name="nif1"></param>
        /// <param name="nif2"></param>
        /// <param name="relation"></param>
        /// <returns></returns>
        private static EnumNOIngFinish JudgeFinish(ref EnumNOIngFinish nif1, ref EnumNOIngFinish nif2, EnumRelation relation)
        {
            switch (relation)
            {
                case EnumRelation.Only:
                    return nif1;
                case EnumRelation.With:
                    switch (nif1)
                    {
                        case EnumNOIngFinish.NoFirst:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.No: return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.IngFirst: return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.Ing: return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.OverFirst: return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.Over: return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.Finish: return EnumNOIngFinish.NoFirst;
                            }
                            break;
                        case EnumNOIngFinish.No:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.No: return EnumNOIngFinish.No;
                                case EnumNOIngFinish.IngFirst: return EnumNOIngFinish.No;
                                case EnumNOIngFinish.Ing: return EnumNOIngFinish.No;
                                case EnumNOIngFinish.OverFirst: return EnumNOIngFinish.No;
                                case EnumNOIngFinish.Over: return EnumNOIngFinish.No;
                                case EnumNOIngFinish.Finish: return EnumNOIngFinish.No;
                            }
                            break;
                        case EnumNOIngFinish.IngFirst:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.No: return EnumNOIngFinish.No;
                                case EnumNOIngFinish.IngFirst: return EnumNOIngFinish.IngFirst;
                                case EnumNOIngFinish.Ing: return EnumNOIngFinish.IngFirst;
                                case EnumNOIngFinish.OverFirst: return EnumNOIngFinish.IngFirst;
                                case EnumNOIngFinish.Over: return EnumNOIngFinish.Over;
                                case EnumNOIngFinish.Finish: return EnumNOIngFinish.IngFirst;
                            }
                            break;
                        case EnumNOIngFinish.Ing:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.No: return EnumNOIngFinish.No;
                                case EnumNOIngFinish.IngFirst: return EnumNOIngFinish.IngFirst;
                                case EnumNOIngFinish.Ing: return EnumNOIngFinish.Ing;
                                case EnumNOIngFinish.OverFirst: return EnumNOIngFinish.OverFirst;
                                case EnumNOIngFinish.Over: return EnumNOIngFinish.Over;
                                case EnumNOIngFinish.Finish: return EnumNOIngFinish.Finish;
                            }
                            break;
                        case EnumNOIngFinish.OverFirst:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.No: nif1 = EnumNOIngFinish.No; return EnumNOIngFinish.No;
                                case EnumNOIngFinish.IngFirst: return EnumNOIngFinish.IngFirst;
                                case EnumNOIngFinish.Ing: return EnumNOIngFinish.OverFirst;
                                case EnumNOIngFinish.OverFirst: return EnumNOIngFinish.OverFirst;
                                case EnumNOIngFinish.Over: return EnumNOIngFinish.Over;
                                case EnumNOIngFinish.Finish: return EnumNOIngFinish.Finish;
                            }
                            break;
                        case EnumNOIngFinish.Over:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.No: return EnumNOIngFinish.No;
                                case EnumNOIngFinish.IngFirst: return EnumNOIngFinish.No;
                                case EnumNOIngFinish.Ing: return EnumNOIngFinish.No;
                                case EnumNOIngFinish.OverFirst: return EnumNOIngFinish.Over;
                                case EnumNOIngFinish.Over: return EnumNOIngFinish.Over;
                                case EnumNOIngFinish.Finish: return EnumNOIngFinish.Finish;
                            }
                            break;
                        case EnumNOIngFinish.Finish:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.No: return EnumNOIngFinish.No;
                                case EnumNOIngFinish.IngFirst: return EnumNOIngFinish.IngFirst;
                                case EnumNOIngFinish.Ing: return EnumNOIngFinish.Finish;
                                case EnumNOIngFinish.OverFirst: return EnumNOIngFinish.Finish;
                                case EnumNOIngFinish.Over: return EnumNOIngFinish.Finish;
                                case EnumNOIngFinish.Finish: return EnumNOIngFinish.Finish;
                            }
                            break;
                    }
                    break;
                case EnumRelation.Or:
                    switch (nif1)
                    {
                        case EnumNOIngFinish.NoFirst:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.No: return EnumNOIngFinish.No;
                                case EnumNOIngFinish.IngFirst: return EnumNOIngFinish.IngFirst;
                                case EnumNOIngFinish.Ing: return EnumNOIngFinish.Ing;
                                case EnumNOIngFinish.OverFirst: return EnumNOIngFinish.OverFirst;
                                case EnumNOIngFinish.Over: return EnumNOIngFinish.Over;
                                case EnumNOIngFinish.Finish: return EnumNOIngFinish.Finish;
                            }
                            break;
                        case EnumNOIngFinish.No:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: return EnumNOIngFinish.No;
                                case EnumNOIngFinish.No: return EnumNOIngFinish.No;
                                case EnumNOIngFinish.IngFirst: return EnumNOIngFinish.IngFirst;
                                case EnumNOIngFinish.Ing: return EnumNOIngFinish.Ing;
                                case EnumNOIngFinish.OverFirst: return EnumNOIngFinish.OverFirst;
                                case EnumNOIngFinish.Over: return EnumNOIngFinish.Over;
                                case EnumNOIngFinish.Finish: return EnumNOIngFinish.Finish;
                            }
                            break;
                        case EnumNOIngFinish.IngFirst:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: return EnumNOIngFinish.IngFirst;
                                case EnumNOIngFinish.No: return EnumNOIngFinish.IngFirst;
                                case EnumNOIngFinish.IngFirst: return EnumNOIngFinish.IngFirst;
                                case EnumNOIngFinish.Ing: return EnumNOIngFinish.Ing;
                                case EnumNOIngFinish.OverFirst: return EnumNOIngFinish.Ing;
                                case EnumNOIngFinish.Over: return EnumNOIngFinish.Over;
                                case EnumNOIngFinish.Finish: return EnumNOIngFinish.IngFirst;
                            }
                            break;
                        case EnumNOIngFinish.Ing:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: return EnumNOIngFinish.Ing;
                                case EnumNOIngFinish.No: return EnumNOIngFinish.Ing;
                                case EnumNOIngFinish.IngFirst: return EnumNOIngFinish.Ing;
                                case EnumNOIngFinish.Ing: return EnumNOIngFinish.Ing;
                                case EnumNOIngFinish.OverFirst: return EnumNOIngFinish.Ing;
                                case EnumNOIngFinish.Over: return EnumNOIngFinish.Ing;
                                case EnumNOIngFinish.Finish: return EnumNOIngFinish.Ing;
                            }
                            break;
                        case EnumNOIngFinish.OverFirst:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: return EnumNOIngFinish.OverFirst;
                                case EnumNOIngFinish.No: return EnumNOIngFinish.OverFirst;
                                case EnumNOIngFinish.IngFirst: return EnumNOIngFinish.Ing;
                                case EnumNOIngFinish.Ing: return EnumNOIngFinish.Ing;
                                case EnumNOIngFinish.OverFirst: return EnumNOIngFinish.OverFirst;
                                case EnumNOIngFinish.Over: return EnumNOIngFinish.Over;
                                case EnumNOIngFinish.Finish: return EnumNOIngFinish.Finish;
                            }
                            break;
                        case EnumNOIngFinish.Over:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.No: return EnumNOIngFinish.No;
                                case EnumNOIngFinish.IngFirst: return EnumNOIngFinish.IngFirst;
                                case EnumNOIngFinish.Ing: return EnumNOIngFinish.Ing;
                                case EnumNOIngFinish.OverFirst: return EnumNOIngFinish.OverFirst;
                                case EnumNOIngFinish.Over: return EnumNOIngFinish.Finish;
                                case EnumNOIngFinish.Finish: return EnumNOIngFinish.Finish;
                            }
                            break;
                        case EnumNOIngFinish.Finish:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.No: return EnumNOIngFinish.No;
                                case EnumNOIngFinish.IngFirst: return EnumNOIngFinish.IngFirst;
                                case EnumNOIngFinish.Ing: return EnumNOIngFinish.Ing;
                                case EnumNOIngFinish.OverFirst: return EnumNOIngFinish.Finish;
                                case EnumNOIngFinish.Over: return EnumNOIngFinish.Finish;
                                case EnumNOIngFinish.Finish: return EnumNOIngFinish.Finish;
                            }
                            break;
                    }
                    break;
                case EnumRelation.ContainOnly:
                    switch (nif1)
                    {
                        case EnumNOIngFinish.NoFirst:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.No: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.IngFirst: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.Ing: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.OverFirst: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.Over: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.Finish: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.NoFirst;
                            }
                            break;
                        case EnumNOIngFinish.No:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.No;
                                case EnumNOIngFinish.No: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.No;
                                case EnumNOIngFinish.IngFirst: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.No;
                                case EnumNOIngFinish.Ing: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.No;
                                case EnumNOIngFinish.OverFirst: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.No;
                                case EnumNOIngFinish.Over: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.No;
                                case EnumNOIngFinish.Finish: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.No;
                            }
                            break;
                        case EnumNOIngFinish.IngFirst:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.No: return EnumNOIngFinish.No;
                                case EnumNOIngFinish.IngFirst: return EnumNOIngFinish.IngFirst;
                                case EnumNOIngFinish.Ing: return EnumNOIngFinish.IngFirst;
                                case EnumNOIngFinish.OverFirst: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.No;
                                case EnumNOIngFinish.Over: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.No;
                                case EnumNOIngFinish.Finish: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.No;
                            }
                            break;
                        case EnumNOIngFinish.Ing:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.No: return EnumNOIngFinish.No;
                                case EnumNOIngFinish.IngFirst: return EnumNOIngFinish.IngFirst;
                                case EnumNOIngFinish.Ing: return EnumNOIngFinish.Ing;
                                case EnumNOIngFinish.OverFirst: return EnumNOIngFinish.OverFirst;
                                case EnumNOIngFinish.Over: return EnumNOIngFinish.Over;
                                case EnumNOIngFinish.Finish: return EnumNOIngFinish.Finish;
                            }
                            break;
                        case EnumNOIngFinish.OverFirst:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.No: return EnumNOIngFinish.No;
                                case EnumNOIngFinish.IngFirst: return EnumNOIngFinish.No;
                                case EnumNOIngFinish.Ing: return EnumNOIngFinish.OverFirst;
                                case EnumNOIngFinish.OverFirst: return EnumNOIngFinish.OverFirst;
                                case EnumNOIngFinish.Over: return EnumNOIngFinish.Over;
                                case EnumNOIngFinish.Finish: return EnumNOIngFinish.Finish;
                            }
                            break;
                        case EnumNOIngFinish.Over:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.No: return EnumNOIngFinish.Over;
                                case EnumNOIngFinish.IngFirst: return EnumNOIngFinish.Over;
                                case EnumNOIngFinish.Ing: return EnumNOIngFinish.Over;
                                case EnumNOIngFinish.OverFirst: return EnumNOIngFinish.Over;
                                case EnumNOIngFinish.Over: return EnumNOIngFinish.Over;
                                case EnumNOIngFinish.Finish: return EnumNOIngFinish.Finish;
                            }
                            break;
                        case EnumNOIngFinish.Finish:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: return EnumNOIngFinish.Finish;
                                case EnumNOIngFinish.No: return EnumNOIngFinish.Finish;
                                case EnumNOIngFinish.IngFirst: return EnumNOIngFinish.Finish;
                                case EnumNOIngFinish.Ing: return EnumNOIngFinish.Finish;
                                case EnumNOIngFinish.OverFirst: return EnumNOIngFinish.Finish;
                                case EnumNOIngFinish.Over: return EnumNOIngFinish.Finish;
                                case EnumNOIngFinish.Finish: return EnumNOIngFinish.Finish;
                            }
                            break;
                    }
                    break;
                case EnumRelation.ContainMulti:
                    switch (nif1)
                    {
                        case EnumNOIngFinish.NoFirst:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.No: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.IngFirst: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.Ing: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.OverFirst: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.Over: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.Finish: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.NoFirst;
                            }
                            break;
                        case EnumNOIngFinish.No:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.No;
                                case EnumNOIngFinish.No: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.No;
                                case EnumNOIngFinish.IngFirst: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.No;
                                case EnumNOIngFinish.Ing: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.No;
                                case EnumNOIngFinish.OverFirst: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.No;
                                case EnumNOIngFinish.Over: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.No;
                                case EnumNOIngFinish.Finish: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.No;
                            }
                            break;
                        case EnumNOIngFinish.IngFirst:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.No: return EnumNOIngFinish.No;
                                case EnumNOIngFinish.IngFirst: return EnumNOIngFinish.IngFirst;
                                case EnumNOIngFinish.Ing: return EnumNOIngFinish.IngFirst;
                                case EnumNOIngFinish.OverFirst: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.No;
                                case EnumNOIngFinish.Over: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.No;
                                case EnumNOIngFinish.Finish: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.No;
                            }
                            break;
                        case EnumNOIngFinish.Ing:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.No: return EnumNOIngFinish.No;
                                case EnumNOIngFinish.IngFirst: return EnumNOIngFinish.IngFirst;
                                case EnumNOIngFinish.Ing: return EnumNOIngFinish.Ing;
                                case EnumNOIngFinish.OverFirst: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.PauseFirst;
                                case EnumNOIngFinish.Over: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.PauseFirst;
                                case EnumNOIngFinish.Finish: nif2 = EnumNOIngFinish.NULL; return EnumNOIngFinish.PauseFirst;
                            }
                            break;
                        case EnumNOIngFinish.OverFirst:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.No: return EnumNOIngFinish.No;
                                case EnumNOIngFinish.IngFirst: return EnumNOIngFinish.No;
                                case EnumNOIngFinish.Ing: return EnumNOIngFinish.OverFirst;
                                case EnumNOIngFinish.OverFirst: return EnumNOIngFinish.OverFirst;
                                case EnumNOIngFinish.Over: return EnumNOIngFinish.Over;
                                case EnumNOIngFinish.Finish: return EnumNOIngFinish.Finish;
                            }
                            break;
                        case EnumNOIngFinish.Over:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: return EnumNOIngFinish.NoFirst;
                                case EnumNOIngFinish.No: return EnumNOIngFinish.Over;
                                case EnumNOIngFinish.IngFirst: return EnumNOIngFinish.Over;
                                case EnumNOIngFinish.Ing: return EnumNOIngFinish.Over;
                                case EnumNOIngFinish.OverFirst: return EnumNOIngFinish.Over;
                                case EnumNOIngFinish.Over: return EnumNOIngFinish.Over;
                                case EnumNOIngFinish.Finish: return EnumNOIngFinish.Finish;
                            }
                            break;
                        case EnumNOIngFinish.Finish:
                            switch (nif2)
                            {
                                case EnumNOIngFinish.NoFirst: return EnumNOIngFinish.Finish;
                                case EnumNOIngFinish.No: return EnumNOIngFinish.Finish;
                                case EnumNOIngFinish.IngFirst: return EnumNOIngFinish.Finish;
                                case EnumNOIngFinish.Ing: return EnumNOIngFinish.Finish;
                                case EnumNOIngFinish.OverFirst: return EnumNOIngFinish.Finish;
                                case EnumNOIngFinish.Over: return EnumNOIngFinish.Finish;
                                case EnumNOIngFinish.Finish: return EnumNOIngFinish.Finish;
                            }
                            break;
                    }
                    break;
            }

            return EnumNOIngFinish.NULL;
        }

        /// <summary>
        /// 判断T,V,CV,PH,CD,UV
        /// </summary>
        /// <param name="val"></param>
        private static void JudgeTVCVPHCDUV(ref EnumNOIngFinish status, double[] listTVCV, List<double> listVal, List<double> listSlope, CollectionObject item, double compare)
        {
            switch (item.MType)
            {
                case 0:
                case 1:
                case 2:
                    JudgeTVCV(ref status, listTVCV[item.MType], item.MLength, item.MTdB, item.MTdE, compare);
                    break;
                default:
                    switch (item.MTS)
                    {
                        case EnumThresholdSlope.Threshold:
                            JudgePHCDUV(ref status, listVal[item.MType - 3], item.MTdB, item.MTdE, listSlope[item.MType - 3]);
                            break;
                        case EnumThresholdSlope.Slope:
                            JudgePHCDUV(ref status, listSlope[item.MType - 3], item.MSJ, item.MSlope);
                            break;
                        case EnumThresholdSlope.ThresholdSlope:
                            JudgePHCDUV(ref status, listVal[item.MType - 3], item.MTdB, item.MTdE, listSlope[item.MType - 3], item.MSJ, item.MSlope);
                            break;
                        case EnumThresholdSlope.Greater:
                            JudgePHCDUVGreater(ref status, listVal[item.MType - 3], item.MTdB);
                            break;
                        case EnumThresholdSlope.Less:
                            JudgePHCDUVLess(ref status, listVal[item.MType - 3], item.MTdB);
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// 判断T,V,CV
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="val">实时值</param>
        /// <param name="hold">区间总长度</param>
        /// <param name="ts">起始</param>
        /// <param name="te">结束</param>
        /// <param name="startVal">对比基值</param>
        private static void JudgeTVCV(ref EnumNOIngFinish status, double val, double hold, double ts, double te, double startVal)
        {
            double valRun = Math.Round(val - startVal, 2);

            switch (status)
            {
                case EnumNOIngFinish.NULL:
                    if (hold > valRun)
                    {
                        if (valRun < ts)
                        {
                            status = EnumNOIngFinish.NoFirst;
                        }
                        else if (valRun >= ts && valRun <= te)
                        {
                            status = EnumNOIngFinish.IngFirst;
                        }
                        else
                        {
                            status = EnumNOIngFinish.NoFirst;
                        }
                    }
                    else
                    {
                        status = EnumNOIngFinish.Finish;
                    }
                    break;
                case EnumNOIngFinish.NoFirst:
                    status = EnumNOIngFinish.No;
                    break;
                case EnumNOIngFinish.No:
                    if (hold > valRun)
                    {
                        if (valRun >= ts && valRun <= te)
                        {
                            status = EnumNOIngFinish.IngFirst;
                        }
                    }
                    else
                    {
                        status = EnumNOIngFinish.Finish;
                    }
                    break;
                case EnumNOIngFinish.IngFirst:
                    status = EnumNOIngFinish.Ing;
                    break;
                case EnumNOIngFinish.Ing:
                    if (hold > valRun)
                    {
                        if (valRun > te)
                        {
                            status = EnumNOIngFinish.OverFirst;
                        }
                    }
                    else
                    {
                        status = EnumNOIngFinish.Finish;
                    }
                    break;
                case EnumNOIngFinish.OverFirst:
                    status = EnumNOIngFinish.Over;
                    break;
                case EnumNOIngFinish.Over:
                    if (hold <= valRun)
                    {
                        status = EnumNOIngFinish.Finish;
                    }
                    break;
            }
        }

        /// <summary>
        /// 判断PH,CD,UV
        /// </summary>
        /// <param name="val"></param>
        private static void JudgePHCDUVGreater(ref EnumNOIngFinish status, double val, double ts)
        {
            switch (status)
            {
                case EnumNOIngFinish.NULL:
                    if (val >= ts)
                    {
                        status = EnumNOIngFinish.IngFirst;
                    }
                    else
                    {
                        status = EnumNOIngFinish.NoFirst;
                    }
                    break;
                case EnumNOIngFinish.NoFirst:
                    status = EnumNOIngFinish.No;
                    break;
                case EnumNOIngFinish.No:
                    if (val >= ts)
                    {
                        status = EnumNOIngFinish.IngFirst;
                    }
                    break;
                case EnumNOIngFinish.IngFirst:
                    status = EnumNOIngFinish.Ing;
                    break;
                case EnumNOIngFinish.Ing:
                    if (val < ts)
                    {
                        status = EnumNOIngFinish.Finish;
                    }
                    break;
            }
        }

        /// <summary>
        /// 判断PH,CD,UV
        /// </summary>
        /// <param name="val"></param>
        private static void JudgePHCDUVLess(ref EnumNOIngFinish status, double val, double ts)
        {
            switch (status)
            {
                case EnumNOIngFinish.NULL:
                    if (val <= ts)
                    {
                        status = EnumNOIngFinish.IngFirst;
                    }
                    else
                    {
                        status = EnumNOIngFinish.NoFirst;
                    }
                    break;
                case EnumNOIngFinish.NoFirst:
                    status = EnumNOIngFinish.No;
                    break;
                case EnumNOIngFinish.No:
                    if (val <= ts)
                    {
                        status = EnumNOIngFinish.IngFirst;
                    }
                    break;
                case EnumNOIngFinish.IngFirst:
                    status = EnumNOIngFinish.Ing;
                    break;
                case EnumNOIngFinish.Ing:
                    if (val > ts)
                    {
                        status = EnumNOIngFinish.Finish;
                    }
                    break;
            }
        }

        /// <summary>
        /// 判断PH,CD,UV
        /// </summary>
        /// <param name="val"></param>
        private static void JudgePHCDUV(ref EnumNOIngFinish status, double val, double ts, double te, double k)
        {
            switch (status)
            {
                case EnumNOIngFinish.NULL:
                    if (val >= ts && k >= 0)
                    {
                        status = EnumNOIngFinish.IngFirst;
                    }
                    else
                    {
                        status = EnumNOIngFinish.NoFirst;
                    }
                    break;
                case EnumNOIngFinish.NoFirst:
                    status = EnumNOIngFinish.No;
                    break;
                case EnumNOIngFinish.No:
                    if (val >= ts && k >= 0)
                    {
                        status = EnumNOIngFinish.IngFirst;
                    }
                    break;
                case EnumNOIngFinish.IngFirst:
                    status = EnumNOIngFinish.Ing;
                    break;
                case EnumNOIngFinish.Ing:
                    if (val < te && k < 0)
                    {
                        status = EnumNOIngFinish.Finish;
                    }
                    break;
            }
        }

        /// <summary>
        /// 判断PH,CD,UV
        /// </summary>
        /// <param name="val"></param>
        private static void JudgePHCDUV(ref EnumNOIngFinish status, double val, double ts, double te, double k, EnumGreaterLess ssi, double ss)
        {
            switch (status)
            {
                case EnumNOIngFinish.NULL:
                    if (val >= ts && val <= te && EnumGreaterLess.Greater == ssi && k >= ss ||
                        val <= ts && val >= te && EnumGreaterLess.Less == ssi && k <= ss)
                    {
                        status = EnumNOIngFinish.IngFirst;
                    }
                    else
                    {
                        status = EnumNOIngFinish.NoFirst;
                    }
                    break;
                case EnumNOIngFinish.NoFirst:
                    status = EnumNOIngFinish.No;
                    break;
                case EnumNOIngFinish.No:
                    if (val >= ts && val <= te && EnumGreaterLess.Greater == ssi && k >= ss ||
                        val <= ts && val >= te && EnumGreaterLess.Less == ssi && k <= ss)
                    {
                        status = EnumNOIngFinish.IngFirst;
                    }
                    break;
                case EnumNOIngFinish.IngFirst:
                    status = EnumNOIngFinish.Ing;
                    break;
                case EnumNOIngFinish.Ing:
                    if (!(val >= ts && val <= te && EnumGreaterLess.Greater == ssi && k >= ss ||
                        val <= ts && val >= te && EnumGreaterLess.Less == ssi && k <= ss))
                    {
                        status = EnumNOIngFinish.Finish;
                    }
                    break;
            }
        }

        /// <summary>
        /// 判断PH,CD,UV
        /// </summary>
        /// <param name="val"></param>
        private static void JudgePHCDUV(ref EnumNOIngFinish status, double k, EnumGreaterLess ssi, double ss)
        {
            switch (status)
            {
                case EnumNOIngFinish.NULL:
                    if (EnumGreaterLess.Greater == ssi && k >= ss || EnumGreaterLess.Less == ssi && k <= ss)
                    {
                        status = EnumNOIngFinish.IngFirst;
                    }
                    else
                    {
                        status = EnumNOIngFinish.NoFirst;
                    }
                    break;
                case EnumNOIngFinish.NoFirst:
                    status = EnumNOIngFinish.No;
                    break;
                case EnumNOIngFinish.No:
                    if (EnumGreaterLess.Greater == ssi && k >= ss || EnumGreaterLess.Less == ssi && k <= ss)
                    {
                        status = EnumNOIngFinish.IngFirst;
                    }
                    break;
                case EnumNOIngFinish.IngFirst:
                    status = EnumNOIngFinish.Ing;
                    break;
                case EnumNOIngFinish.Ing:
                    if (!(EnumGreaterLess.Greater == ssi && k >= ss || EnumGreaterLess.Less == ssi && k <= ss))
                    {
                        status = EnumNOIngFinish.Finish;
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// 收集行的执行状态
    /// </summary>
    enum EnumNOIngFinish
    {
        NULL,           //开始判断
        NoFirst,        //进入等待收集
        No,             //等待收集
        IngFirst,       //进入收集
        Ing,            //收集
        PauseFirst,     //进入休息
        Pause,          //休息
        OverFirst,      //进入结束收集
        Over,           //结束收集
        Finish          //结束判断         
    }
}
