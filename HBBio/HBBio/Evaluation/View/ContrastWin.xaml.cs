using HBBio.Chromatogram;
using HBBio.Communication;
using HBBio.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HBBio.Evaluation
{
    /// <summary>
    /// ContrastWin.xaml 的交互逻辑
    /// </summary>
    public partial class ContrastWin : Window
    {
        public ContrastWin()
        {
            InitializeComponent();
        }

        public void Contrast(List<Signal> signalList, List<ResultTitle> listResult)
        {
            List<string> listName = new List<string>();
            List<bool> listContrast = new List<bool>();
            int firstShow = -1;
            for (int i = 0; i < signalList.Count; i++)
            {
                listName.Add(signalList[i].MDlyName);
                listContrast.Add(signalList[i].MContrastOld);
                if (-1 == firstShow && signalList[i].MContrastOld)
                {
                    firstShow = i;
                }
            }

            List<CurveSet> listCurveSet = new List<CurveSet>();
            Random rnd = new Random();
            foreach (var resultTitle in listResult)
            {
                CurveSet curveSet = new CurveSet();
                List<Curve> list = new List<Curve>();
                foreach (var it in signalList)
                {
                    System.Drawing.Color color = System.Drawing.Color.FromArgb((byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255));
                    list.Add(new Curve(it.MDlyName + "--" + resultTitle.MName, it.MUnit, color, true));
                }
                curveSet.InitItemList(list);
                curveSet.MSelectIndex = firstShow;

                listCurveSet.Add(curveSet);
            }
            this.chromatogramUC.InitDataFrame(listName, listContrast, listCurveSet);

            Thread threadContrast = new Thread(new ParameterizedThreadStart(ContrastThread));
            threadContrast.IsBackground = true;
            threadContrast.Start(listResult);
        }

        /// <summary>
        /// 加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
        }

        /// <summary>
        /// 对比结果的子线程
        /// </summary>
        /// <param name="obj"></param>
        private void ContrastThread(object obj)
        {
            try
            {
                ResultManager manager = new ResultManager();

                int index = 0;
                foreach (var resultTitle in (List<ResultTitle>)obj)
                {
                    System.IO.Stream ms = null;
                    double cv = 1;
                    double ch = 1;
                    string attachment = "";
                    string markerInfo = null;
                    if (null == manager.GetCurveData(resultTitle.MID, SystemControl.SystemControlManager.s_comconfStatic.m_listSmooth, this.chromatogramUC.MListList(index), out ms, out cv, out ch, out attachment, out markerInfo))
                    {
                        this.chromatogramUC.AddLineItemData(index);
                    }
                    index++;
                }

                this.chromatogramUC.CalMaxMin();
                this.chromatogramUC.StartThread();
            }
            catch (Exception ex)
            {
                SystemLog.SystemLogManager.LogWrite(ex);
            }
        }
    }
}
