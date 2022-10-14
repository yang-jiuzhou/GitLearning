using HBBio.Print;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

namespace HBBio.ColumnList
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
        public void SetData(ColumnItem columnItem)
        {
            FlowDocument doc = (FlowDocument)Application.LoadComponent(new Uri("./../../ColumnList/View/Document.xaml", UriKind.RelativeOrAbsolute));

            doc.DataContext = columnItem;

            Style styleCell = doc.Resources["BorderedCell"] as Style;

            TableRowGroup group = doc.FindName("RunParameters") as TableRowGroup;
            foreach (ParametersValueUnit item in columnItem.MRP.MList)
            {
                TableRow row = new TableRow();

                TableCell cell = new TableCell(new Paragraph(new Run(item.MName)));
                cell.Style = styleCell;
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run(item.MText)));
                cell.Style = styleCell;
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run(item.MUnit)));
                cell.Style = styleCell;
                row.Cells.Add(cell);

                group.Rows.Add(row);
            }

            group = doc.FindName("Details") as TableRowGroup;
            foreach (ParametersValueUnit item in columnItem.MDT.MList)
            {
                TableRow row = new TableRow();

                TableCell cell = new TableCell(new Paragraph(new Run(item.MName)));
                cell.Style = styleCell;
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run(item.MText)));
                cell.Style = styleCell;
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run(item.MUnit)));
                cell.Style = styleCell;
                row.Cells.Add(cell);

                group.Rows.Add(row);
            }

            docReader.Document = doc; 
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
