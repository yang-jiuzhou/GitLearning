using System;
using System.Collections.Generic;
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

namespace HBBio.Print
{
    /// <summary>
    /// OutputSetWin.xaml 的交互逻辑
    /// </summary>
    public partial class OutputSetWin : Window
    {
        private PDFSet m_data = new PDFSet();
        public PDFSet MData
        {
            get
            {
                return m_data;
            }
            set
            {
                m_data = value;
            }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        public OutputSetWin(Window parent)
        {
            InitializeComponent();

            this.Owner = parent;

            //加载标记样式
            cboxMarkerStyle.ItemsSource = Enum.GetValues(typeof(TextMarkerStyle));

            //加载字号枚举
            cboxSize.ItemsSource = Share.MFontSize.MList;

            //加载字体枚举
            cboxFamily.ItemsSource = Fonts.SystemFontFamilies;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(m_data.m_icon))
                {
                    imageIcon.Source = new BitmapImage(new Uri(m_data.m_icon, UriKind.Absolute));
                }
            }
            catch(Exception msg)
            {
                Share.MessageBoxWin.Show(msg.Message);
            }
            
            txtTitle.Text = m_data.m_title;

            numLeft.Value = m_data.m_marginLeft;
            numRight.Value = m_data.m_marginRight;
            numTop.Value = m_data.m_marginTop;
            numBottom.Value = m_data.m_marginBottom;
            cboxMarkerStyle.SelectedIndex = m_data.m_markerStyle;

            chboxSigner.IsChecked = m_data.m_signer;
            chboxReviewer.IsChecked = m_data.m_reviewer;
            chboxOutputTime.IsChecked = m_data.m_outputTime;
            
            cboxSize.SelectedIndex = m_data.m_fontSize;
            cboxFamily.SelectedIndex = m_data.m_fontFamily;
            btnFore.Background = m_data.m_colorFore;
            btnBack.Background = m_data.m_colorBack;
        }

        private void btnIcon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
                ofd.DefaultExt = ".ico";
                ofd.Filter = "ico file|*.ico|png files|*.png|jpg files|*.jpg";
                if (true == ofd.ShowDialog())
                {
                    imageIcon.Source = null;
                    imageIcon.Source = new BitmapImage(new Uri(ofd.FileName, UriKind.Absolute));
                }
            }
            catch (Exception msg)
            {
                Share.MessageBoxWin.Show(msg.Message);
            }
        }

        private void btnIconClear_Click(object sender, RoutedEventArgs e)
        {
            imageIcon.Source = null;
        }

        private void btnFore_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog dlg = new System.Windows.Forms.ColorDialog();
            dlg.Color = Share.ValueTrans.MediaToDraw((Color)ColorConverter.ConvertFromString(btnFore.Background.ToString()));
            if (System.Windows.Forms.DialogResult.OK == dlg.ShowDialog())
            {
                btnFore.Background = new SolidColorBrush(Share.ValueTrans.DrawToMedia(dlg.Color));
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog dlg = new System.Windows.Forms.ColorDialog();
            dlg.Color = Share.ValueTrans.MediaToDraw((Color)ColorConverter.ConvertFromString(btnBack.Background.ToString()));
            if (System.Windows.Forms.DialogResult.OK == dlg.ShowDialog())
            {
                btnBack.Background = new SolidColorBrush(Share.ValueTrans.DrawToMedia(dlg.Color));
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (null == imageIcon.Source)
            {
                m_data.m_icon = "";
            }
            else
            {
                m_data.m_icon = imageIcon.Source.ToString();
            }
            m_data.m_title = txtTitle.Text;

            m_data.m_marginLeft = (int)numLeft.Value;
            m_data.m_marginRight = (int)numRight.Value;
            m_data.m_marginTop = (int)numTop.Value;
            m_data.m_marginBottom = (int)numBottom.Value;
            m_data.m_markerStyle = cboxMarkerStyle.SelectedIndex;

            m_data.m_signer = true == chboxSigner.IsChecked;
            m_data.m_reviewer = true == chboxReviewer.IsChecked;
            m_data.m_outputTime = true == chboxOutputTime.IsChecked;

            m_data.m_fontSize = cboxSize.SelectedIndex;
            m_data.m_fontFamily = cboxFamily.SelectedIndex;
            m_data.m_colorFore = btnFore.Background;
            m_data.m_colorBack = btnBack.Background;

            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
