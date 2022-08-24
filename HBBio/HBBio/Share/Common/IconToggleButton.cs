﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace HBBio.Share
{
    public class IconToggleButton : ToggleButton
    {
        /// <summary>  
        /// 鼠标移上去的背景颜色  
        /// </summary>  
        public static readonly DependencyProperty MouseOverBackgroundProperty = DependencyProperty.Register("MouseOverBackground", typeof(Brush), typeof(IconToggleButton),
            new PropertyMetadata(Brushes.Blue));
        public Brush MouseOverBackground
        {
            get { return (Brush)GetValue(MouseOverBackgroundProperty); }
            set { SetValue(MouseOverBackgroundProperty, value); }
        }

        /// <summary>  
        /// 鼠标按下去的背景颜色  
        /// </summary>  
        public static readonly DependencyProperty MouseDownBackgroundProperty = DependencyProperty.Register("MouseDownBackground", typeof(Brush), typeof(IconToggleButton),
            new PropertyMetadata(Brushes.BlueViolet));
        public Brush MouseDownBackground
        {
            get { return (Brush)GetValue(MouseDownBackgroundProperty); }
            set { SetValue(MouseDownBackgroundProperty, value); }
        }

        /// <summary>  
        /// 鼠标处于按下状态的背景颜色  
        /// </summary>  
        public static readonly DependencyProperty IsCheckedBackgroundProperty = DependencyProperty.Register("IsCheckedBackground", typeof(Brush), typeof(IconToggleButton),
            new PropertyMetadata(Brushes.BlueViolet));
        public Brush IsCheckedBackground
        {
            get { return (Brush)GetValue(IsCheckedBackgroundProperty); }
            set { SetValue(IsCheckedBackgroundProperty, value); }
        }

        /// <summary>  
        /// 鼠标移上去的边框颜色  
        /// </summary>  
        public static readonly DependencyProperty MouseOverBorderBrushProperty = DependencyProperty.Register("MouseOverBorderBrush", typeof(Brush), typeof(IconToggleButton),
            new PropertyMetadata(Brushes.Blue));
        public Brush MouseOverBorderBrush
        {
            get { return (Brush)GetValue(MouseOverBorderBrushProperty); }
            set { SetValue(MouseOverBorderBrushProperty, value); }
        }

        /// <summary>  
        /// 鼠标按下去的边框颜色  
        /// </summary>  
        public static readonly DependencyProperty MouseDownBorderBrushProperty = DependencyProperty.Register("MouseDownBorderBrush", typeof(Brush), typeof(IconToggleButton),
            new PropertyMetadata(Brushes.BlueViolet));
        public Brush MouseDownBorderBrush
        {
            get { return (Brush)GetValue(MouseDownBorderBrushProperty); }
            set { SetValue(MouseDownBorderBrushProperty, value); }
        }

        /// <summary>  
        /// 鼠标处于按下状态的边框颜色  
        /// </summary>  
        public static readonly DependencyProperty IsCheckedBorderBrushProperty = DependencyProperty.Register("IsCheckedBorderBrush", typeof(Brush), typeof(IconToggleButton),
            new PropertyMetadata(Brushes.BlueViolet));
        public Brush IsCheckedBorderBrush
        {
            get { return (Brush)GetValue(IsCheckedBorderBrushProperty); }
            set { SetValue(IsCheckedBorderBrushProperty, value); }
        }

        /// <summary>  
        /// 圆角  
        /// </summary>  
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(IconToggleButton), 
            new PropertyMetadata(new CornerRadius(0.0)));
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        /// <summary>
        /// 图片
        /// </summary>
        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(IconToggleButton),
            null);
        public ImageSource Image
        {
            get { return GetValue(ImageProperty) as ImageSource; }
            set { SetValue(ImageProperty, value); }
        }

        /// <summary>
        /// 图片的宽度
        /// </summary>
        public static readonly DependencyProperty ImageWidthProperty = DependencyProperty.Register("ImageWidth", typeof(double), typeof(IconToggleButton),
            new PropertyMetadata(35.0));
        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        /// <summary>
        /// 图片的高度
        /// </summary>
        public static readonly DependencyProperty ImageHeightProperty = DependencyProperty.Register("ImageHeight", typeof(double), typeof(IconToggleButton),
            new PropertyMetadata(35.0));
        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        static IconToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconToggleButton), new System.Windows.FrameworkPropertyMetadata(typeof(IconToggleButton)));
        }

        /// <summary>
        /// 重写函数
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.MouseOverBackground == null)
            {
                this.MouseOverBackground = Background;
            }
            if (this.MouseDownBackground == null)
            {
                if (this.MouseOverBackground == null)
                {
                    this.MouseDownBackground = Background;
                }
                else
                {
                    this.MouseDownBackground = MouseOverBackground;
                }
            }

            if (this.MouseOverBorderBrush == null)
            {
                this.MouseOverBorderBrush = BorderBrush;
            }
            if (this.MouseDownBorderBrush == null)
            {
                if (this.MouseOverBorderBrush == null)
                {
                    this.MouseDownBorderBrush = BorderBrush;
                }
                else
                {
                    this.MouseDownBorderBrush = MouseOverBorderBrush;
                }
            }
        }
    }
}
