using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HBBio.Chromatogram
{
    /**
     * ClassName: ColorButton
     * Description: 颜色按钮,色谱图界面的颜色按钮
     * Version: 1.0
     * Create:  2018/05/16
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class ColorButton : System.Windows.Controls.Button
    {
        //序号
        public int MIndex { get; set; }
        //名称
        public string MName
        {
            set
            {
                Content = value.Replace("_", "__");
            }
        }
        //颜色
        public System.Drawing.Color MColor
        {
            set
            {
                Foreground = new SolidColorBrush(Share.ValueTrans.DrawToMedia(value));
            }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="index"></param>
        /// <param name="name"></param>
        /// <param name="color"></param>
        public ColorButton(int index, string name, System.Drawing.Color color)
        {
            MIndex = index;
            Content = name.Replace("_","__");
            Foreground = new SolidColorBrush(Share.ValueTrans.DrawToMedia(color));
            Background = new SolidColorBrush(Colors.Transparent);
            BorderThickness = new Thickness(0);
            Margin = new Thickness(0, 0, 10, 0);
            Padding = new Thickness(20, 5, 20, 5);
        }
    }
}
