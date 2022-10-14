using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HBBio.Print
{
    public enum EnumPDFSet
    {
        Icon,               //图标
        Title,              //标题

        MarginLeft,         //左边距
        MarginRight,        //右边距
        MarginTop,          //上边距
        MarginBottom,       //下边距
        MarkerStyle,

        Signer,
        Reviewer,
        OutputTime,

        FontSize,           //字号
        FontFamily,         //字体
        ColorBack,          //背景色
        ColorFore           //前景色
    }

    public class PDFSet
    {
        public string m_icon = "";
        public string m_title = "";

        public int m_marginLeft = 50;
        public int m_marginRight = 50;
        public int m_marginTop = 50;
        public int m_marginBottom = 50;
        public int m_markerStyle = 0;

        public bool m_signer = true;
        public bool m_reviewer = true;
        public bool m_outputTime = true;
        
        public int m_fontSize = 1;
        public int m_fontFamily = 1;
        public Brush m_colorBack = Brushes.White;
        public Brush m_colorFore = Brushes.Black;

        public double MFontSize
        {
            get
            {
                return Share.MFontSize.MList[m_fontSize];
            }
        }

        public string MFontFamily
        {
            get
            {
                return Fonts.SystemFontFamilies.ToList()[m_fontFamily].ToString();
            }
        }
    }
}
