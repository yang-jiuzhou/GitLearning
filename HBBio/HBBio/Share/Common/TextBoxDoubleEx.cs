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
    public class TextBoxDoubleEx : TextBox
    {
        public TextBoxDoubleEx()
        {
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

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(string), typeof(TextBoxDoubleEx), new PropertyMetadata("", null, OnValueChanged));
        /// <summary>
        /// 数值
        /// </summary>
        [Description("数值"), Category("输入设置"), DefaultValue("")]
        public string Value
        {
            get
            {
                return (string)GetValue(ValueProperty);
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
            string value = (string)baseValue;
            TextBoxDoubleEx Nmbox = d as TextBoxDoubleEx;
            Nmbox.Text = value;
            return value;
        }

        /// <summary>
        /// 丢失焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumericBox_LostFocus(object sender, RoutedEventArgs e)
        {
            EndTextCheck();
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

            AverTextCheck();
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
                e.Handled = false;
                return;
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
                        if (PPos == 1)
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
                if (PPos == -1)//未输入过小数点,且允许输入小数
                {
                    if (this.SelectionStart == 0)
                    {
                        this.Text = "0.";
                    }
                    e.Handled = false;
                    return;
                }
            }

            e.Handled = true;
        }

        /// <summary>
        /// 输入合法性检查
        /// </summary>
        private bool AverTextCheck()
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(this.Text, @"^[+-]?\d[.]?\d$"))
                {
                    decimal d = Convert.ToDecimal(this.Text);//转换为数字

                    if (d.ToString().Equals(this.Text))
                    {
                        Value = d.ToString();
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 输入合法性检查
        /// </summary>
        private bool EndTextCheck()
        {
            try
            {
                decimal d = 0;
                if (decimal.TryParse(this.Text, out d))
                {
                    Value = d.ToString();
                }
                else
                {
                    this.Value = "";
                }                
            }
            catch
            {
                this.Value = "";

                return false;
            }
            return true;
        }
    }
}
