using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace HBBio.Print
{
    /**
     * ClassName: PaginatorHeaderFooter
     * Description: 设置导出PDF的页眉页脚
     * Version: 1.0
     * Create:  2021/01/04
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class PaginatorHeaderFooter : DocumentPaginator
    {
        public const int c_top = 30;
        public const int c_bottom = 60;
        private readonly DocumentPaginator m_paginator;     //页元素
        private readonly PDFSet m_pdfSet;
        private string m_icon;
        private string m_title;
        private Typeface m_typeface;                        //字体
        private double m_emSize;                            //字号
        private Brush m_foreground;                         //画刷
        public static string s_signer = "";                 //签名人
        public static string s_reviewer = "";               //审核人


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paginator"></param>
        public PaginatorHeaderFooter(DocumentPaginator paginator, PDFSet pdfSet)
        {
            m_paginator = paginator;

            m_pdfSet = pdfSet;
            m_icon = pdfSet.m_icon;
            m_title = pdfSet.m_title;
            m_typeface = new Typeface(pdfSet.MFontFamily);
            m_emSize = pdfSet.MFontSize;
            m_foreground = pdfSet.m_colorFore;
        }

        /// <summary>
        /// 重写属性，是否有效的总页数
        /// </summary>
        public override bool IsPageCountValid
        {
            get
            {
                return m_paginator.IsPageCountValid;
            }
        }

        /// <summary>
        /// 重写属性，总页数
        /// </summary>
        public override int PageCount
        {
            get
            {
                return m_paginator.PageCount;
            }
        }

        /// <summary>
        /// 重写属性，页宽高
        /// </summary>
        public override Size PageSize
        {
            get
            {
                return m_paginator.PageSize;
            }

            set
            {
                m_paginator.PageSize = value;
            }
        }

        /// <summary>
        /// 重写属性，资源
        /// </summary>
        public override IDocumentPaginatorSource Source
        {
            get
            {
                return m_paginator.Source;
            }
        }

        /// <summary>
        /// 重写页面
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public override DocumentPage GetPage(int pageNumber)
        {
            DocumentPage page = m_paginator.GetPage(pageNumber);
            ContainerVisual newpage = new ContainerVisual();

            //页眉:公司名称
            DrawingVisual header = new DrawingVisual();
            using (DrawingContext ctx = header.RenderOpen())
            {
                //var bitmapImage = new BitmapImage(new Uri("pack://application:,,,/image/alarmYes.png"));
                try
                {
                    var bitmapImage = new BitmapImage(new Uri(m_icon, UriKind.Absolute));
                    //图片在左侧
                    ctx.DrawImage(bitmapImage, new Rect(10, 10, 50, 50));
                }
                catch { }
                
                //标题居中显示
                FormattedText txtTitle = new FormattedText(m_title,
                    System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                    m_typeface, m_emSize, m_foreground);
                ctx.DrawText(txtTitle, new Point((page.ContentBox.Right - page.ContentBox.Left - txtTitle.Width) / 2 + page.ContentBox.Left, page.ContentBox.Top - c_top));
                //分割线
                ctx.DrawLine(new Pen(m_foreground, 0.5), new Point(page.ContentBox.Left, page.ContentBox.Top - 1), new Point(page.ContentBox.Right, page.ContentBox.Top - 1));
            }

            //页脚:第几页
            DrawingVisual footer = new DrawingVisual();
            using (DrawingContext ctx = footer.RenderOpen())
            {
                //分割线
                ctx.DrawLine(new Pen(m_foreground, 0.5), new Point(page.ContentBox.Left, page.ContentBox.Bottom + 1), new Point(page.ContentBox.Right, page.ContentBox.Bottom + 1));
                //页码左下侧显示
                FormattedText txtPage = new FormattedText("第" + (pageNumber + 1) + "页",
                    System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                    m_typeface, m_emSize, m_foreground);
                ctx.DrawText(txtPage, new Point(page.ContentBox.Left, page.ContentBox.Bottom + 2));
                //导出时间
                double lengthAdd = 0;
                if (m_pdfSet.m_outputTime)
                {
                    FormattedText txtOutputTimeLab = new FormattedText("导出时间",
                        System.Globalization.CultureInfo.CurrentCulture, FlowDirection.RightToLeft,
                        m_typeface, m_emSize, m_foreground);
                    ctx.DrawText(txtOutputTimeLab, new Point(page.ContentBox.Right, page.ContentBox.Bottom + 2));
                    FormattedText txtOutputTimeTxt = new FormattedText(DateTime.Now.ToString(),
                        System.Globalization.CultureInfo.CurrentCulture, FlowDirection.RightToLeft,
                        m_typeface, m_emSize, m_foreground);
                    ctx.DrawText(txtOutputTimeTxt, new Point(page.ContentBox.Right, page.ContentBox.Bottom + c_bottom / 2));

                    lengthAdd += Math.Max(txtOutputTimeLab.Width, txtOutputTimeTxt.Width) + 10;
                }
                //审核人员
                if (m_pdfSet.m_reviewer)
                {
                    FormattedText txtReviewerLab = new FormattedText("审核人员",
                        System.Globalization.CultureInfo.CurrentCulture, FlowDirection.RightToLeft,
                        m_typeface, m_emSize, m_foreground);
                    ctx.DrawText(txtReviewerLab, new Point(page.ContentBox.Right - lengthAdd, page.ContentBox.Bottom + 2));
                    FormattedText txtReviewerTxt = new FormattedText(s_reviewer,
                        System.Globalization.CultureInfo.CurrentCulture, FlowDirection.RightToLeft,
                        m_typeface, m_emSize, m_foreground);
                    ctx.DrawText(txtReviewerTxt, new Point(page.ContentBox.Right - lengthAdd, page.ContentBox.Bottom + c_bottom / 2));

                    lengthAdd += Math.Max(txtReviewerLab.Width, txtReviewerTxt.Width) + 10;
                }
                //签名人员
                if (m_pdfSet.m_signer)
                {
                    FormattedText txtSignerLab = new FormattedText("签名人员",
                        System.Globalization.CultureInfo.CurrentCulture, FlowDirection.RightToLeft,
                        m_typeface, m_emSize, m_foreground);
                    ctx.DrawText(txtSignerLab, new Point(page.ContentBox.Right - lengthAdd, page.ContentBox.Bottom + 2));
                    FormattedText txtSignerTxt = new FormattedText(s_signer,
                        System.Globalization.CultureInfo.CurrentCulture, FlowDirection.RightToLeft,
                        m_typeface, m_emSize, m_foreground);
                    ctx.DrawText(txtSignerTxt, new Point(page.ContentBox.Right - lengthAdd, page.ContentBox.Bottom + c_bottom / 2));
                }
            }

            //将原页面微略压缩(使用矩阵变换)
            //ContainerVisual mainPage = new ContainerVisual();
            //mainPage.Children.Add(page.Visual);
            //mainPage.Transform = new MatrixTransform(1, 0, 0, 0.95, 0, 0.025 * page.ContentBox.Height);

            //在现页面中加入原页面，页眉和页脚
            newpage.Children.Add(page.Visual);
            newpage.Children.Add(header);
            newpage.Children.Add(footer);

            return new DocumentPage(newpage, page.Size, page.BleedBox, page.ContentBox);
        }
    }
}
