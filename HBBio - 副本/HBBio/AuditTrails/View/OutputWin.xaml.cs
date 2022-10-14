using HBBio.Print;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
using System.Windows.Threading;

namespace HBBio.AuditTrails
{
    /// <summary>
    /// OutputWin.xaml 的交互逻辑
    /// </summary>
    public partial class OutputWin : Window
    {
        List<string> m_listType;
        List<string> m_listUser;
        List<string> m_listDate;
        List<string> m_listInfo;
        DispatcherTimer m_timer = new DispatcherTimer();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        public OutputWin(Window parent)
        {
            InitializeComponent();

            this.Owner = parent;

            //加载字号枚举
            cboxSize.ItemsSource = Share.MFontSize.MList;

            //加载字体枚举
            cboxFamily.ItemsSource = Fonts.SystemFontFamilies;  
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            m_timer.Tick += new EventHandler(TimerFun);
            m_timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            m_timer.Start();
        }

        /// <summary>
        /// 定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerFun(object sender, EventArgs e)
        {
            m_timer.Stop();

            docReader.Document = (FlowDocument)Application.LoadComponent(new Uri("./../../AuditTrails/View/Document.xaml", UriKind.RelativeOrAbsolute));
            PrintDialog dialog = new PrintDialog();
            docReader.Document.PageWidth = dialog.PrintableAreaWidth;
            docReader.Document.PageHeight = dialog.PrintableAreaHeight;
            PDFSet pdfSet;
            PrintManager manager = new PrintManager();
            manager.GetPDFSet(out pdfSet);
            UpdatePDFSet(pdfSet);

            TableRowGroup group = docReader.Document.FindName("table") as TableRowGroup;
            Style styleCell = docReader.Document.Resources["BorderedCell"] as Style;
            try
            {
                for (int i = 0; i < m_listType.Count; i++)
                {
                    TableRow row = new TableRow();

                    TableCell cell = new TableCell(new Paragraph(new Run(m_listType[i])));
                    cell.Style = styleCell;
                    row.Cells.Add(cell);

                    cell = new TableCell(new Paragraph(new Run(m_listUser[i])));
                    cell.Style = styleCell;
                    row.Cells.Add(cell);

                    cell = new TableCell(new Paragraph(new Run(m_listDate[i])));
                    cell.Style = styleCell;
                    row.Cells.Add(cell);

                    cell = new TableCell(new Paragraph(new Run(m_listInfo[i])));
                    cell.Style = styleCell;

                    row.Cells.Add(cell);

                    group.Rows.Add(row);

                    if (i % 100 == 99)
                    {
                        Share.MApp.DoEvents();
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLog.SystemLogManager.LogWrite(ex);
            }
            finally
            {
                loadingWaitUC.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="listType"></param>
        /// <param name="listUser"></param>
        /// <param name="listDate"></param>
        /// <param name="listInfo"></param>
        public void SetData(List<string> listType, List<string> listUser, List<string> listDate, List<string> listInfo)
        {
            m_listType = listType;
            m_listUser = listUser;
            m_listDate = listDate;
            m_listInfo = listInfo;

            
        }

        /// <summary>
        /// 设置导出格式
        /// </summary>
        public void UpdatePDFSet(PDFSet pdfSet)
        {
            docReader.Document.PagePadding = new Thickness(pdfSet.m_marginLeft, pdfSet.m_marginTop, pdfSet.m_marginRight, pdfSet.m_marginBottom);
            docReader.Document.ColumnWidth = docReader.Document.PageWidth - docReader.Document.PagePadding.Left - docReader.Document.PagePadding.Right;
            docReader.Document.FontSize = pdfSet.MFontSize;
            docReader.Document.FontFamily = new FontFamily(pdfSet.MFontFamily);
            docReader.Document.Foreground = pdfSet.m_colorFore;
            docReader.Document.Background = pdfSet.m_colorBack;

            cboxSize.SelectedIndex = pdfSet.m_fontSize;
            cboxFamily.SelectedIndex = pdfSet.m_fontFamily;
            btnColorFore.Background = pdfSet.m_colorFore;
            btnColorBack.Background = pdfSet.m_colorBack;
        }

        /// <summary>
        /// 文本是否加粗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWeight_Click(object sender, RoutedEventArgs e)
        {
            if (null != docReader.Selection && !string.IsNullOrWhiteSpace(docReader.Selection.Text))
            {
                Object obj = docReader.Selection.GetPropertyValue(TextElement.FontWeightProperty);
                if (DependencyProperty.UnsetValue == obj)
                {
                    TextRange range = new TextRange(docReader.Selection.Start, docReader.Selection.Start);
                    obj = range.GetPropertyValue(TextElement.FontWeightProperty);
                }

                if (FontWeights.Normal == (FontWeight)obj)
                {
                    docReader.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                }
                else
                {
                    docReader.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
                }
            }
        }

        /// <summary>
        /// 文本是否倾斜
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStyle_Click(object sender, RoutedEventArgs e)
        {
            if (null != docReader.Selection && !string.IsNullOrWhiteSpace(docReader.Selection.Text))
            {
                Object obj = docReader.Selection.GetPropertyValue(TextElement.FontStyleProperty);
                if (DependencyProperty.UnsetValue == obj)
                {
                    TextRange range = new TextRange(docReader.Selection.Start, docReader.Selection.Start);
                    obj = range.GetPropertyValue(TextElement.FontStyleProperty);
                }

                if (FontStyles.Normal == (FontStyle)obj)
                {
                    docReader.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
                }
                else
                {
                    docReader.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
                }
            }
        }

        /// <summary>
        /// 文本是否下划线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnderline_Click(object sender, RoutedEventArgs e)
        {
            if (null != docReader.Selection && !string.IsNullOrWhiteSpace(docReader.Selection.Text))
            {
                Object obj = docReader.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
                if (DependencyProperty.UnsetValue == obj)
                {
                    TextRange range = new TextRange(docReader.Selection.Start, docReader.Selection.Start);
                    obj = range.GetPropertyValue(Inline.TextDecorationsProperty);
                }

                if (TextDecorations.Underline == (TextDecorationCollection)obj)
                {
                    docReader.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                }
                else
                {
                    docReader.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
                }
            }
        }

        /// <summary>
        /// 文本字号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboxSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null != docReader.Selection && !string.IsNullOrWhiteSpace(docReader.Selection.Text))
            {
                docReader.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, cboxSize.SelectedItem);
            }
        }

        /// <summary>
        /// 文本字体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboxFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null != docReader.Selection && !string.IsNullOrWhiteSpace(docReader.Selection.Text))
            {
                docReader.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, cboxFamily.SelectedItem);
            }
        }

        /// <summary>
        /// 文本前景色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColorFore_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog dlg = new System.Windows.Forms.ColorDialog();
            dlg.Color = Share.ValueTrans.MediaToDraw((Color)ColorConverter.ConvertFromString(btnColorFore.Background.ToString()));
            if (System.Windows.Forms.DialogResult.OK == dlg.ShowDialog())
            {
                btnColorFore.Background = new SolidColorBrush(Share.ValueTrans.DrawToMedia(dlg.Color));
                docReader.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, btnColorFore.Background);
            }
        }

        /// <summary>
        /// 文本背景色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColorBack_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog dlg = new System.Windows.Forms.ColorDialog();
            dlg.Color = Share.ValueTrans.MediaToDraw((Color)ColorConverter.ConvertFromString(btnColorBack.Background.ToString()));
            if (System.Windows.Forms.DialogResult.OK == dlg.ShowDialog())
            {
                btnColorBack.Background = new SolidColorBrush(Share.ValueTrans.DrawToMedia(dlg.Color));
                docReader.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, btnColorBack.Background);
            }
        }

        /// <summary>
        /// 文本更多设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMore_Click(object sender, RoutedEventArgs e)
        {
            PDFSet pdfSet;
            PrintManager manager = new PrintManager();
            manager.GetPDFSet(out pdfSet);

            OutputSetWin dlg = new OutputSetWin(this);
            dlg.MData = pdfSet;
            if (true == dlg.ShowDialog())
            {
                manager.SetPDFSet(pdfSet);
                UpdatePDFSet(pdfSet);
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            FlowDocument doc = docReader.Document;

            //打印
            docReader.Document = null;
            try
            {
                PDFSet pdfSet;
                PrintManager manager = new PrintManager();
                manager.GetPDFSet(out pdfSet);

                PrintPreviewWin previewWnd = new PrintPreviewWin(this, doc, pdfSet);
                previewWnd.Owner = this;
                previewWnd.ShowInTaskbar = false;
                previewWnd.ShowDialog();
                AuditTrails.AuditTrailsStatic.Instance().InsertRowOperate(this.Title); 
            }
            catch
            { }

            docReader.Document = doc;
        }
    }
}
