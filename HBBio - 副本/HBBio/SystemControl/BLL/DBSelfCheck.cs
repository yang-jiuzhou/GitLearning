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
            CreateAllTable();
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
    }
}
