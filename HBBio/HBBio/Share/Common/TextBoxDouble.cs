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
    public class TextBoxDouble : TextBox
    {
        private string m_oldText = "";

        public TextBoxDouble()
        {
            DecimalPlaces = 2;
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
                m_oldText = value;
            }
        }

        public static readonly DependencyProperty DecimalProperty = DependencyProperty.Register("DecimalPlaces", typeof(ushort), typeof(TextBoxDouble));
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

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(double), typeof(TextBoxDouble));
        /// <summary>
        /// 可输入的最大值
        /// </summary>
        [Description("可输入的最大值"), Category("输入设置"), DefaultValue(0)]
        public double Maximum
        {
            get
            {
                return (double)GetValue(MaximumProperty);
            }
            set
            {
                SetValue(MaximumProperty, value);
                if (this.Value > Maximum)
                {
                    this.Value = Maximum;
                }
            }
        }

        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(double), typeof(TextBoxDouble));
        /// <summary>
        /// 可输入的最小值
        /// </summary>
        [Description("可输入的最小值"), Category("输入设置"), DefaultValue(0)]
        public double Minimum
        {
            get
            {
                return (double)GetValue(MinimumProperty);
            }
            set
            {
                SetValue(MinimumProperty, value);
                if (this.Value < Minimum)
                {
                    this.Value = Minimum;
                }
            }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(TextBoxDouble), new PropertyMetadata(0.0, null, OnValueChanged));
        /// <summary>
        /// 数值
        /// </summary>
        [Description("数值"), Category("输入设置"), DefaultValue(0)]
        public double Value
        {
            get
            {
                return (double)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);

                Text = value.ToString();
            }
        }


        /// <summary>
        /// 自定义事件，失去焦点触发
        /// </summary>
        public static readonly RoutedEvent MLostFocusEvent =
             EventManager.RegisterRoutedEvent("MLostFocus", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TextBoxDouble));
        public event RoutedEventHandler MLostFocus
        {
            add { AddHandler(MLostFocusEvent, value); }
            remove { RemoveHandler(MLostFocusEvent, value); }
        }


        /// <summary>
        /// 值改变
        /// </summary>
        /// <param name="d"></param>
        /// <param name="baseValue"></param>
        /// <returns></returns>
        private static Object OnValueChanged(DependencyObject d, object baseValue)
        {
            double value = (double)baseValue;
            TextBoxDouble Nmbox = d as TextBoxDouble;
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
            if (!m_oldText.Equals(this.Text))
            {
                TextCheck();

                RoutedEventArgs args = new RoutedEventArgs(MLostFocusEvent, this.Value);
                RaiseEvent(args);
            }
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

            if ((e.Key >= Key.D1 && e.Key <= Key.D9 || e.Key >= Key.NumPad1 && e.Key <= Key.NumPad9))//数字1-9
            {
                if (PPos != -1 && this.SelectionStart > PPos)//可输入小数，且输入点在小数点后
                {
                    if (this.Text.Length - (PPos + 1) < DecimalPlaces)//小数位数是否已满
                    {
                        e.Handled = false;
                        return;
                    }
                }
                else
                {
                    if (0 < this.Text.Length && this.Text.Substring(0, 1) == "0" && 1 == this.SelectionStart)
                    {
                        //第一个为0，则不能再输入数字
                        e.Handled = true;
                        return;
                    }
                    else if (1 < this.Text.Length && this.Text.Substring(0, 2) == "-0" && 2 == this.SelectionStart)
                    {
                        //第一个为-,第二个为0，则不能再输入数字
                        e.Handled = true;
                        return;
                    }
                    else
                    {
                        e.Handled = false;
                        return;
                    }
                }
            }
            else if (e.Key == Key.D0 || e.Key == Key.NumPad0)//输入的是0
            {
                if (PPos != -1 && this.SelectionStart > PPos)//可输入小数，且输入点在小数点后
                {
                    if (this.Text.Length - (PPos + 1) < DecimalPlaces)//小数位数是否已满
                    {
                        e.Handled = false;
                        return;
                    }
                }
                else
                {
                    if (0 < this.Text.Length && 0 == this.SelectionStart && 0 == this.SelectionLength)
                    {
                        //第一个不为0
                        e.Handled = true;
                        return;
                    }
                    else if (1 < this.Text.Length && this.Text.Substring(0, 1) == "-" && 1 == this.SelectionStart)
                    {
                        //第一个为-,第二个不为0
                        e.Handled = true;
                        return;
                    }
                    else if (1 < this.Text.Length && this.Text.Substring(0, 2) == "-0" && 2 == this.SelectionStart)
                    {
                        //第一个为-,第二个不为0
                        e.Handled = true;
                        return;
                    }
                    else
                    {
                        e.Handled = false;
                        return;
                    }
                }
            }
            else if (e.Key == Key.Decimal || e.Key == Key.OemPeriod)//小数点
            {
                if (this.DecimalPlaces > 0)//允许输入小数
                {
                    if ((PPos == -1 || this.SelectedText.IndexOf('.') != -1) && this.SelectionStart > 0)//未输入过小数点,且小数点不是第一位
                    {
                        e.Handled = false;
                        return;
                    }
                }
            }
            else if (e.Key == Key.Subtract || e.Key == Key.OemMinus)//减号
            {
                if (this.Minimum < 0)//允许输入的最小值小于0
                {
                    if (this.Text.IndexOf('-') == -1 && this.SelectionStart == 0
                        || this.SelectedText.IndexOf('-') != -1)
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
                this.Value = Math.Round(Convert.ToDouble(this.Text.Trim()), this.DecimalPlaces);//转换为数字

                if (this.Value < this.Minimum)//输入的值小于最小值
                {
                    this.Value = this.Minimum;
                }
                else if (this.Value > this.Maximum)//输入的值大于最大值
                {
                    this.Value = this.Maximum;
                }
            }
            catch
            {
                this.Value = this.Minimum;
                return false;
            }
            return true;
        }
    }
}
