using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    /**
     * ClassName: MethodFactory
     * Description: 方法工厂，创建预定义方法
     * Version: 1.0
     * Create:  2020/07/31
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    class MethodFactory
    {
        /// <summary>
        /// 添加阶段
        /// </summary>
        /// <param name="method"></param>
        /// <param name="type"></param>
        private void AddPhase(Method method, EnumPhaseType type)
        {
            PhaseFactory fac = new PhaseFactory();
            method.MPhaseList.Add(fac.GetPhase(type));
        }

        /// <summary>
        /// 创建纯化方法
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public Method GetMethod(EnumMethodPurification type, int communicationSetsID, int projectID, string name)
        {
            Method method = new Method(-1, communicationSetsID, projectID, name);
            switch (type)
            {
                case EnumMethodPurification.AC:
                    AddPhase(method, EnumPhaseType.Equilibration);
                    //AddPhase(method, EnumPhaseType.SampleApplication);
                    AddPhase(method, EnumPhaseType.ColumnWash);
                    AddPhase(method, EnumPhaseType.Elution);
                    AddPhase(method, EnumPhaseType.Equilibration);
                    AddPhase(method, EnumPhaseType.SystemCIP);
                    break;
                case EnumMethodPurification.AIEX:
                    AddPhase(method, EnumPhaseType.Equilibration);
                    //AddPhase(method, EnumPhaseType.SampleApplication);
                    AddPhase(method, EnumPhaseType.ColumnWash);
                    AddPhase(method, EnumPhaseType.Elution);
                    AddPhase(method, EnumPhaseType.ColumnWash);
                    AddPhase(method, EnumPhaseType.Equilibration);
                    AddPhase(method, EnumPhaseType.SystemCIP);
                    break;
                case EnumMethodPurification.CIEX:
                    AddPhase(method, EnumPhaseType.Equilibration);
                    //AddPhase(method, EnumPhaseType.SampleApplication);
                    AddPhase(method, EnumPhaseType.ColumnWash);
                    AddPhase(method, EnumPhaseType.Elution);
                    AddPhase(method, EnumPhaseType.ColumnWash);
                    AddPhase(method, EnumPhaseType.Equilibration);
                    AddPhase(method, EnumPhaseType.SystemCIP);
                    break;
                case EnumMethodPurification.CF:
                    AddPhase(method, EnumPhaseType.Equilibration);
                    //AddPhase(method, EnumPhaseType.SampleApplication);
                    AddPhase(method, EnumPhaseType.ColumnWash);
                    AddPhase(method, EnumPhaseType.Elution);
                    AddPhase(method, EnumPhaseType.Equilibration);
                    break;
                case EnumMethodPurification.DS:
                    AddPhase(method, EnumPhaseType.Equilibration);
                    //AddPhase(method, EnumPhaseType.SampleApplication);
                    AddPhase(method, EnumPhaseType.Elution);
                    break;
                case EnumMethodPurification.GF:
                    AddPhase(method, EnumPhaseType.Equilibration);
                    //AddPhase(method, EnumPhaseType.SampleApplication);
                    AddPhase(method, EnumPhaseType.Elution);
                    break;
                case EnumMethodPurification.HIC:
                    AddPhase(method, EnumPhaseType.Equilibration);
                    //AddPhase(method, EnumPhaseType.SampleApplication);
                    AddPhase(method, EnumPhaseType.ColumnWash);
                    AddPhase(method, EnumPhaseType.Elution);
                    AddPhase(method, EnumPhaseType.ColumnWash);
                    AddPhase(method, EnumPhaseType.Equilibration);
                    AddPhase(method, EnumPhaseType.SystemCIP);
                    break;
                case EnumMethodPurification.RPC:
                    AddPhase(method, EnumPhaseType.Equilibration);
                    //AddPhase(method, EnumPhaseType.SampleApplication);
                    AddPhase(method, EnumPhaseType.ColumnWash);
                    AddPhase(method, EnumPhaseType.Elution);
                    AddPhase(method, EnumPhaseType.ColumnWash);
                    AddPhase(method, EnumPhaseType.Equilibration);
                    break;
            }

            return method;
        }

        /// <summary>
        /// 创建保养方法
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public Method GetMethod(EnumMethodMaintenance type, int communicationSetsID, int projectID, string name)
        {
            Method method = new Method(-1, communicationSetsID, projectID, name);
            switch (type)
            {
                case EnumMethodMaintenance.ColumnCIP:
                    AddPhase(method, EnumPhaseType.ColumnCIP);
                    break;
                case EnumMethodMaintenance.ColumnPerformanceTest:
                    AddPhase(method, EnumPhaseType.Equilibration);
                    AddPhase(method, EnumPhaseType.SampleApplication);
                    break;
                case EnumMethodMaintenance.SystemCIP:
                    AddPhase(method, EnumPhaseType.SystemCIP);
                    AddPhase(method, EnumPhaseType.SystemCIP);
                    AddPhase(method, EnumPhaseType.SystemCIP);
                    break;
            }

            return method;
        }

        /// <summary>
        /// 创建空方法
        /// </summary>
        /// <param name="communicationSetsID"></param>
        /// <param name="projectID"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public Method GetMethod(int communicationSetsID, int projectID, string name)
        {
            Method method = new Method(-1, communicationSetsID, projectID, name);
            return method;
        }
    }
}
