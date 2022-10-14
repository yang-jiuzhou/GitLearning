using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HBBio.Share
{
    /// <summary>
    /// 数字输入文本框
    /// 可输入：数字0-9,小数点,减号
    /// 可设置小数位数
    /// 可设置数值上下限
    /// </summary>
    public class TextBoxInt : TextBox
    {
        public TextBoxInt()
        {
            DecimalPlaces = 0;
            Maximum = 999999;
            Minimum = 0;
            this.LostFocus += NumericBox_LostFocus;
            this.KeyDown += OnKeyDown;
            this.TextChanged += OnTextChanged;
        }

        /// <summary>
        /// 在属性设计器中隐藏文本属性
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public new string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        public static readonly DependencyProperty DecimalProperty = DependencyProperty.Register("DecimalPlaces", typeof(ushort), typeof(TextBoxInt));
        /// <summary>
        /// 允许输入的小数点个数
        /// </summary>
        [Description("小数位数,0表示不能输入小数"), Category("输入设置")]
        public ushort DecimalPlaces
        {
            get
            {
                return (ushort)GetValue(DecimalProperty);
            }
            set
            {
                SetValue(DecimalProperty, value);
            }
        }

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(decimal), typeof(TextBoxInt));
        /// <summary>
        /// 可输入的最大值
        /// </summary>
        [Description("可输入的最大值"), Category("输入设置"), DefaultValue(0)]
        public decimal Maximum
        {
            get
            {
                return (decimal)GetValue(MaximumProperty);
            }
            set
            {
                SetValue(MaximumProperty, value);
            }
        }

        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(decimal), typeof(TextBoxInt));
        /// <summary>
        /// 可输入的最小值
        /// </summary>
        [Description("可输入的最小值"), Category("输入设置"), DefaultValue(0)]
        public decimal Minimum
        {
            get
            {
                return (decimal)GetValue(MinimumProperty);
            }
            set
            {
                SetValue(MinimumProperty, value);
            }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(TextBoxInt), new PropertyMetadata(0, null, OnValueChanged));
        /// <summary>
        /// 数值
        /// </summary>
        [Description("数值"), Category("输入设置"), DefaultValue(0)]
        public int Value
        {
            get
            {
                return (int)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        /// <summary>
        /// 值改变
        /// </summary>
        /// <param name="d"></param>
        /// <param name="baseValue"></param>
        /// <returns></returns>
        private static Object OnValueChanged(DependencyObject d, object baseValue)
        {
            int value = (int)baseValue;
            TextBoxInt Nmbox = d as TextBoxInt;
            Nmbox.Text = value.ToString();
            return value;
        }

        /// <summary>
        /// 丢失焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumericBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextCheck();
        }

        /// <summary>
        /// 文本内容变更检查
        /// </summary>
        /// <param name="e"></param>
        protected void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.Text.IndexOf('.') == 0
                || ((this.Text.Length > 1 && this.Text.Substring(0, 1) == "0" && this.Text.Substring(1, 1) != ".")))
            {
                this.Text = this.Text.Remove(0, 1);
                this.SelectionStart = this.Text.Length;
            }
        }

        /// <summary>
        /// 输入限制
        /// </summary>
        /// <param name="e"></param>
        protected void OnKeyDown(object sender, KeyEventArgs e)
        {
            //可输入：数字0-9,小数点,减号
            int PPos = this.Text.IndexOf('.');//获取当前小数点位置
            if ((e.Key >= Key.D1 && e.Key <= Key.D9)//数字0-9键
                || (e.Key >= Key.NumPad1 && e.Key <= Key.NumPad9))//小键盘数字0-9
            {
                if (PPos != -1 && this.SelectionStart > PPos)//可输入小数，且输入点在小数点后
                {
                    if (this.Text.Length - PPos - 1 < DecimalPlaces)//小数位数是否已满
                    {
                        e.Handled = false;
                        return;
                    }
                }
                else
                {
                    e.Handled = false;
                    return;
                }
            }
            else if (e.Key == Key.D0 || e.Key == Key.NumPad0)//输入的是0
            {
                if (this.SelectionStart > 0)
                {//非首位输入
                    if (this.Text.Substring(0, 1) != "0")
                    {//首位数字不为0
                        e.Handled = false;
                        return;
                    }
                    else
                    {//首位数字为0,
                        if (PPos == 1 && this.Text.Length - PPos - 1 < DecimalPlaces)
                        { //0后面是小数点,小数位数未满
                            e.Handled = false;
                            return;
                        }
                    }
                }
                else
                {
                    e.Handled = false;
                    return;
                }
            }
            else if (e.Key == Key.Decimal || e.Key == Key.OemPeriod)//小数点
            {
                if (PPos == -1 && this.DecimalPlaces > 0)//未输入过小数点,且允许输入小数
                {
                    if (this.SelectionStart == 0)
                    {
                        this.Text = "0.";
                    }
                    e.Handled = false;
                    return;
                }
            }
            else if (e.Key == Key.Subtract || e.Key == Key.OemMinus)//减号
            {
                if (this.Minimum < 0)//允许输入的最小值小于0
                {
                    if (this.Text.IndexOf('-') == -1 && this.SelectionStart == 0)
                    {
                        e.Handled = false;
                        return;
                    }
                }
            }
            e.Handled = true;
        }

        /// <summary>
        /// 输入合法性检查
        /// </summary>
        private bool TextCheck()
        {
            try
            {
                decimal d = Convert.ToDecimal(this.Text.Trim());//转换为数字
                if (this.DecimalPlaces > 0)
                {
                    d = Math.Round(d, this.DecimalPlaces);//舍入小数位数
                }
                if (!(this.Minimum == 0 && this.Maximum == 0))
                {
                    if (d < this.Minimum)//输入的值小于最小值
                    {
                        d = this.Minimum;
                    }
                    else if (d > this.Maximum)//输入的值大于最大值
                    {
                        d = this.Maximum;
                    }
                }
                this.Value = Convert.ToInt32(d);
            }
            catch
            {
                this.Value = Convert.ToInt32(this.Minimum);
                return false;
            }
            return true;
        }
    }
}
