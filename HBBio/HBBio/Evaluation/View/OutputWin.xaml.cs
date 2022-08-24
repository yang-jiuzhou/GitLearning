using HBBio.MethodEdit;
using HBBio.Print;
using HBBio.Share;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
    /// OutputWin.xaml 的交互逻辑
    /// </summary>
    public partial class OutputWin : Window
    {
        private PDFSet m_data = new PDFSet();


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

            PrintManager manager = new PrintManager();
            manager.GetPDFSet(out m_data);
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="list"></param>
        public void SetData(string user, string chromatogramName, 
            WriteableBitmap bmpName, WriteableBitmap bmp,
            List<PeakValue> listPeak, List<bool> listVisible, List<string> listName,
            MethodEdit.Method methodItem,
            List<string> listType, List<string> listUser, List<string> listDate, List<string> listInfo)
        {
            FlowDocument docAll = new FlowDocument();

            Paragraph para = null;

            //用户
            if (null != user)
            {
                para = new Paragraph(new Run(ReadXaml.GetResources("E_OutputUser") + ":"));
                docAll.Blocks.Add(para);
                para = new Paragraph(new Run(user));
                docAll.Blocks.Add(para);
            }

            //色谱信息
            if (null != chromatogramName)
            {
                para = new Paragraph(new Run(ReadXaml.GetResources("E_OutputChromatogramName") + ":"));
                docAll.Blocks.Add(para);
                para = new Paragraph(new Run(chromatogramName));
                docAll.Blocks.Add(para);
            }

            //色谱图
            if (null != bmp)
            {
                para = new Paragraph(new Run(ReadXaml.GetResources("E_OutputChromatogram") + ":"));
                docAll.Blocks.Add(para); 
                para = new Paragraph();
                Image image = new Image();
                image.Source = bmp;
                Image imageName = new Image();
                imageName.Source = bmpName;
                para.Inlines.Add(imageName);
                para.Inlines.Add(image);
                docAll.Blocks.Add(para);
            }

            if (null != listPeak)
            {
                para = new Paragraph(new Run(ReadXaml.GetResources("E_OutputIntegration") + ":"));
                docAll.Blocks.Add(para);

                for (int i = 0; i < listPeak.Count; i++)
                {
                    if (null != listPeak[i].m_x && listVisible[i])
                    {
                        para = new Paragraph(new Run("  " + listName[i]  + ":"));
                        docAll.Blocks.Add(para);

                        FlowDocument doc = (FlowDocument)Application.LoadComponent(new Uri("./../../Evaluation/View/Document.xaml", UriKind.RelativeOrAbsolute));
                        TableRowGroup group = doc.FindName("rowsDetails") as TableRowGroup;
                        Style styleCell = doc.Resources["BorderedCell"] as Style;

                        Run labValUnit = doc.FindName("labValUnit") as Run;
                        Run labAreaUnit = doc.FindName("labAreaUnit") as Run;
                        if (listName[i].Contains("Cd"))
                        {
                            labValUnit.Text = DlyBase.SC_CDUNIT;
                            labAreaUnit.Text = DlyBase.SC_CDAreaUNIT;
                        }
                        else if (listName[i].Contains("UV"))
                        {
                            labValUnit.Text = DlyBase.SC_UVABSUNIT;
                            labAreaUnit.Text = DlyBase.SC_UVABSAreaUNIT;
                        }
                        else
                        {
                            labValUnit.Text = "";
                            labAreaUnit.Text = "";
                        }

                        foreach (PeakIntegration item in listPeak[i].m_list)
                        {
                            TableRow row = new TableRow();

                            TableCell cell = new TableCell(new Paragraph(new Run(item.MName)));
                            cell.Style = styleCell;
                            row.Cells.Add(cell);

                            cell = new TableCell(new Paragraph(new Run(item.MRetentionTime.ToString())));
                            cell.Style = styleCell;
                            row.Cells.Add(cell);

                            cell = new TableCell(new Paragraph(new Run(item.MTopVal.ToString())));
                            cell.Style = styleCell;
                            row.Cells.Add(cell);

                            cell = new TableCell(new Paragraph(new Run(item.MStartValX.ToString())));
                            cell.Style = styleCell;
                            row.Cells.Add(cell);

                            cell = new TableCell(new Paragraph(new Run(item.MStartValY.ToString())));
                            cell.Style = styleCell;
                            row.Cells.Add(cell);

                            cell = new TableCell(new Paragraph(new Run(item.MEndValX.ToString())));
                            cell.Style = styleCell;
                            row.Cells.Add(cell);

                            cell = new TableCell(new Paragraph(new Run(item.MEndValY.ToString())));
                            cell.Style = styleCell;
                            row.Cells.Add(cell);

                            cell = new TableCell(new Paragraph(new Run(item.MHeight.ToString())));
                            cell.Style = styleCell;
                            row.Cells.Add(cell);

                            cell = new TableCell(new Paragraph(new Run(item.MArea.ToString())));
                            cell.Style = styleCell;
                            row.Cells.Add(cell);

                            cell = new TableCell(new Paragraph(new Run(item.MAreaPer.ToString())));
                            cell.Style = styleCell;
                            row.Cells.Add(cell);

                            cell = new TableCell(new Paragraph(new Run(item.MHalfWidth.ToString())));
                            cell.Style = styleCell;
                            row.Cells.Add(cell);

                            cell = new TableCell(new Paragraph(new Run(item.MTpn.ToString())));
                            cell.Style = styleCell;
                            row.Cells.Add(cell);

                            cell = new TableCell(new Paragraph(new Run(item.MTailingFactor.ToString())));
                            cell.Style = styleCell;
                            row.Cells.Add(cell);

                            cell = new TableCell(new Paragraph(new Run(item.MSymmetryFactor.ToString())));
                            cell.Style = styleCell;
                            row.Cells.Add(cell);

                            cell = new TableCell(new Paragraph(new Run(item.MResolution.ToString())));
                            cell.Style = styleCell;
                            row.Cells.Add(cell);

                            group.Rows.Add(row);
                        }

                        while (0 < doc.Blocks.Count)
                        {
                            Block block = doc.Blocks.FirstBlock;
                            doc.Blocks.Remove(block);
                            docAll.Blocks.Add(block);
                        }
                    }
                }

            }

            //方法
            if (null != methodItem)
            {
                para = new Paragraph(new Run(ReadXaml.GetResources("E_OutputMethod") + ":"));
                docAll.Blocks.Add(para);

                MathodFlowDocument mathodFlowDocument = new MathodFlowDocument();
                FlowDocument doc = mathodFlowDocument.GetFlowDocument(methodItem);
                doc.DataContext = methodItem;

                while (0 < doc.Blocks.Count)
                {
                    Block block = doc.Blocks.FirstBlock;
                    doc.Blocks.Remove(block);
                    docAll.Blocks.Add(block);
                }
            }

            //日志
            if (null != listType)
            {
                para = new Paragraph(new Run(ReadXaml.GetResources("E_OutputLog") + ":"));
                docAll.Blocks.Add(para);

                FlowDocument doc = (FlowDocument)Application.LoadComponent(new Uri("./../../AuditTrails/View/Document.xaml", UriKind.RelativeOrAbsolute));
                Style styleCell = doc.Resources["BorderedCell"] as Style;
                TableRowGroup group = doc.FindName("table") as TableRowGroup;
                for (int i = 0; i < listType.Count; i++)
                {
                    TableRow row = new TableRow();

                    TableCell cell = new TableCell(new Paragraph(new Run(listType[i])));
                    cell.Style = styleCell;
                    row.Cells.Add(cell);

                    cell = new TableCell(new Paragraph(new Run(listUser[i])));
                    cell.Style = styleCell;
                    row.Cells.Add(cell);

                    cell = new TableCell(new Paragraph(new Run(listDate[i])));
                    cell.Style = styleCell;
                    row.Cells.Add(cell);

                    cell = new TableCell(new Paragraph(new Run(listInfo[i])));
                    cell.Style = styleCell;
                    row.Cells.Add(cell);

                    group.Rows.Add(row);
                }

                while (0 < doc.Blocks.Count)
                {
                    Block block = doc.Blocks.FirstBlock;
                    doc.Blocks.Remove(block);
                    docAll.Blocks.Add(block);
                }
            }

            docReader.Document = docAll;
        }

        /// <summary>
        /// 设置导出格式
        /// </summary>
        public void UpdatePDFSet()
        {
            docReader.Document.PagePadding = new Thickness(m_data.m_marginLeft, m_data.m_marginTop, m_data.m_marginRight, m_data.m_marginBottom);
            docReader.Document.ColumnWidth = docReader.Document.PageWidth - docReader.Document.PagePadding.Left - docReader.Document.PagePadding.Right;
            docReader.Document.FontSize = m_data.MFontSize;
            docReader.Document.FontFamily = new FontFamily(m_data.MFontFamily);
            docReader.Document.Foreground = m_data.m_colorFore;
            docReader.Document.Background = m_data.m_colorBack;

            cboxSize.SelectedIndex = m_data.m_fontSize;
            cboxFamily.SelectedIndex = m_data.m_fontFamily;
            btnColorFore.Background = m_data.m_colorFore;
            btnColorBack.Background = m_data.m_colorBack;
        }

        /// <summary>
        /// 加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PrintDialog dialog = new PrintDialog();
            docReader.Document.PageWidth = dialog.PrintableAreaWidth;
            docReader.Document.PageHeight = dialog.PrintableAreaHeight;

            UpdatePDFSet();
        }

        /// <summary>
        /// 文本是否加粗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWeight_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(docReader.Selection.Text))
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
            if (!string.IsNullOrWhiteSpace(docReader.Selection.Text))
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
            if (!string.IsNullOrWhiteSpace(docReader.Selection.Text))
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
            if (!string.IsNullOrWhiteSpace(docReader.Selection.Text))
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
            if (!string.IsNullOrWhiteSpace(docReader.Selection.Text))
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
            OutputSetWin dlg = new OutputSetWin(this);
            dlg.MData = m_data;
            if (true == dlg.ShowDialog())
            {
                PrintManager manager = new PrintManager();
                manager.SetPDFSet(m_data);
                UpdatePDFSet();
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
                PrintPreviewWin previewWnd = new PrintPreviewWin(this, doc, m_data);
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