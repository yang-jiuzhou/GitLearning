using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
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
using System.Windows.Threading;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Xml.Serialization;

namespace HBBio.Print
{
    /// <summary>
    /// PrintPreviewWin.xaml 的交互逻辑
    /// </summary>
    public partial class PrintPreviewWin : Window
    {
        private delegate void LoadXpsMethod();
        private FlowDocument m_doc;
        private PDFSet m_pdfSet;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="title"></param>
        /// <param name="typeface"></param>
        /// <param name="emSize"></param>
        /// <param name="foreground"></param>
        public PrintPreviewWin(Window parent, FlowDocument doc, PDFSet pdfSet)
        {
            InitializeComponent();

            this.Owner = parent;

            m_doc = doc;
            //预留页眉页脚空间
            m_doc.PagePadding = new Thickness(m_doc.PagePadding.Left,
                m_doc.PagePadding.Top + PaginatorHeaderFooter.c_top,
                m_doc.PagePadding.Right,
                m_doc.PagePadding.Bottom + PaginatorHeaderFooter.c_bottom);

            m_pdfSet = pdfSet;

            Dispatcher.BeginInvoke(new LoadXpsMethod(LoadXps), DispatcherPriority.ApplicationIdle);
        }

        /// <summary>
        /// 加载预览文件
        /// </summary>
        public void LoadXps()
        {
            //构造一个基于内存的xps document
            MemoryStream ms = new MemoryStream();
            Package package = Package.Open(ms, FileMode.Create, FileAccess.ReadWrite);
            Uri DocumentUri = new Uri("pack://InMemoryDocument.xps");
            PackageStore.RemovePackage(DocumentUri);
            PackageStore.AddPackage(DocumentUri, package);
            XpsDocument xpsDocument = new XpsDocument(package, CompressionOption.Fast, DocumentUri.AbsoluteUri);

            //将flow document写入基于内存的xps document中去
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
            writer.Write(new PaginatorHeaderFooter(((IDocumentPaginatorSource)m_doc).DocumentPaginator, m_pdfSet));

            //获取这个基于内存的xps document的fixed document
            docViewer.Document = xpsDocument.GetFixedDocumentSequence();

            //关闭基于内存的xps document
            xpsDocument.Close();
        }

        /// <summary>
        /// 窗体关闭触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            m_doc.PagePadding = new Thickness(m_doc.PagePadding.Left,
                m_doc.PagePadding.Top - PaginatorHeaderFooter.c_top,
                m_doc.PagePadding.Right,
                m_doc.PagePadding.Bottom - PaginatorHeaderFooter.c_bottom);
        }
    }
}
