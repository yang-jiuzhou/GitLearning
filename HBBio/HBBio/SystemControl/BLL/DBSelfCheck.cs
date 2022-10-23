using HBBio.Administration;
using HBBio.AuditTrails;
using HBBio.ColumnList;
using HBBio.Communication;
using HBBio.Chromatogram;
using HBBio.MethodEdit;
using HBBio.ProjectManager;
using HBBio.Result;
using HBBio.Database;
using HBBio.Print;
using HBBio.Evaluation;
using HBBio.Manual;
using HBBio.PassDog;
using HBBio.TubeStand;
using System.Collections.Generic;

namespace HBBio.SystemControl
{
    /**
     * ClassName: DBSelfCheck
     * Description: 数据库自检类
     * Version: 1.0
     * Create:  2018/05/28
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class DBSelfCheck
    {
        /// <summary>
        /// 登录检查
        /// </summary>
        public void InitFirst()
        {
            BaseDB baseDB = new BaseDB();
            if (!baseDB.ExistFileFolder())
            {
                baseDB.CreateAll();

                CreateAllTable();
            }
            else
            {
                baseDB.RepairAll();
                baseDB.JudgeSizeAll();
            }
        }

        /// <summary>
        /// 创建所有数据
        /// </summary>
        public void CreateAll()
        {
            CreateAllDB();
            //CreateAllTable();
            CheckAllTable();
        }

        /// <summary>
        /// 创建所有数据库
        /// </summary>
        private void CreateAllDB()
        {
            BaseDB baseDB = new BaseDB();
            baseDB.CreateAll();
        }

        /// <summary>
        /// 创建所有表
        /// </summary>
        private void CreateAllTable()
        {
            PassDogTable pdTable = new PassDogTable();
            pdTable.InitTable();

            WindowSize.WindowSizeTable wsTable = new WindowSize.WindowSizeTable();
            wsTable.InitTable();

            ConfCheckableTable ccTable = new ConfCheckableTable();
            ccTable.InitTable();

            ViewVisibilityTable vvTable = new ViewVisibilityTable();
            vvTable.InitTable();

            ColumnListTable tab = new ColumnListTable();
            tab.InitTable();

            TubeStandTable tsTable = new TubeStandTable();
            tsTable.InitTable();

            LogListTable logList = new LogListTable();
            logList.InitTable();

            LogColumnVisibilityTable logCV = new LogColumnVisibilityTable();
            logCV.InitTable();

            PDFSetTable pdf = new PDFSetTable();
            pdf.InitTable();

            PermissionTable permission = new PermissionTable();
            permission.InitTable();

            UserTable user = new UserTable();
            user.InitTable();

            TacticsTable tactics = new TacticsTable();
            tactics.InitTable();

            SignerReviewerTable signature = new SignerReviewerTable();
            signature.InitTable();

            ProjectTreeTable projectTree = new ProjectTreeTable();
            projectTree.InitTable();

            CommunicationSetsTable communicationSets = new CommunicationSetsTable();
            communicationSets.InitTable();

            DBBackupRestoreTable dbPath = new DBBackupRestoreTable();
            dbPath.InitTable();

            DBAutoBackupTable dbAutoBackupTable = new DBAutoBackupTable();
            dbAutoBackupTable.InitTable();

            TimeSetTable timeSet = new TimeSetTable();
            timeSet.InitTable();

            MethodTable method = new MethodTable();
            method.InitTable();

            PhaseTable phaseTable = new PhaseTable();
            phaseTable.InitTable();

            MethodTempTable methodTemp = new MethodTempTable();
            methodTemp.InitTable();

            ManualTempTable manualTemp = new ManualTempTable();
            manualTemp.InitTable();

            ResultListTable resultList = new ResultListTable();
            resultList.InitTable();

            IntegrationSetTable integrationSetTable = new IntegrationSetTable();
            integrationSetTable.InitTable();

            OutputSelectSetTable outputSelectSetTable = new OutputSelectSetTable();
            outputSelectSetTable.InitTable();

            BackgroundTable backgroundTable = new BackgroundTable();
            backgroundTable.InitTable();
        }

        /// <summary>
        /// 创建所有表
        /// </summary>
        private void CheckAllTable()
        {
            string error = null;

            PassDogTable pdTable = new PassDogTable();
            pdTable.InitTable();

            WindowSize.WindowSizeTable wsTable = new WindowSize.WindowSizeTable();
            error += wsTable.CheckTable();

            ConfCheckableTable ccTable = new ConfCheckableTable();
            error += ccTable.CheckTable();

            ViewVisibilityTable vvTable = new ViewVisibilityTable();
            error += vvTable.CheckTable();

            ColumnListTable tab = new ColumnListTable();
            tab.InitTable();

            TubeStandTable tsTable = new TubeStandTable();
            error += tsTable.CheckTable();

            LogListTable logList = new LogListTable();
            logList.InitTable();

            LogColumnVisibilityTable logCV = new LogColumnVisibilityTable();
            logCV.InitTable();

            PDFSetTable pdf = new PDFSetTable();
            error += pdf.CheckTable();

            PermissionTable permission = new PermissionTable();
            error += permission.CheckTable();

            UserTable user = new UserTable();
            user.InitTable();

            TacticsTable tactics = new TacticsTable();
            tactics.InitTable();

            SignerReviewerTable signature = new SignerReviewerTable();
            error += signature.CheckTable();

            ProjectTreeTable projectTree = new ProjectTreeTable();
            projectTree.InitTable();

            CommunicationSetsTable communicationSets = new CommunicationSetsTable();
            communicationSets.InitTable();

            DBBackupRestoreTable dbPath = new DBBackupRestoreTable();
            error += dbPath.CheckTable();

            DBAutoBackupTable dbAutoBackupTable = new DBAutoBackupTable();
            error += dbAutoBackupTable.CheckTable();

            TimeSetTable timeSet = new TimeSetTable();
            timeSet.InitTable();

            MethodTable method = new MethodTable();
            method.InitTable();
            method.RepairXml();

            PhaseTable phaseTable = new PhaseTable();
            error += phaseTable.CheckTable();

            MethodTempTable methodTemp = new MethodTempTable();
            methodTemp.InitTable();

            ManualTempTable manualTemp = new ManualTempTable();
            manualTemp.InitTable();

            ResultListTable resultList = new ResultListTable();
            error += resultList.CheckTable();

            IntegrationSetTable integrationSetTable = new IntegrationSetTable();
            integrationSetTable.InitTable();

            OutputSelectSetTable outputSelectSetTable = new OutputSelectSetTable();
            outputSelectSetTable.InitTable();

            BackgroundTable backgroundTable = new BackgroundTable();
            error += backgroundTable.CheckTable();

            CommunicationSetsManager csManager = new CommunicationSetsManager();

            List<CommunicationSets> csList = null;
            if (null == csManager.GetList(out csList))
            {
                for (int i = 0; i < csList.Count; i++)
                {
                    ComConfTable ccDB = new ComConfTable(csList[i].MId);
                    error += ccDB.CheckTable();

                    SignalTable snDB = new SignalTable(csList[i].MId);
                    error += snDB.CheckTable();
                }
            }

            


            if (!string.IsNullOrEmpty(error))
            {
                System.Windows.MessageBox.Show(error);
            }
        }
    }
}
