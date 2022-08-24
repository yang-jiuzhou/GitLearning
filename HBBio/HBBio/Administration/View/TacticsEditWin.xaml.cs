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

namespace HBBio.Administration
{
    /// <summary>
    /// TacticsWin.xaml 的交互逻辑
    /// </summary>
    public partial class TacticsEditWin : Window
    {
        public TacticsRow MItem { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="row"></param>
        public TacticsEditWin(Window parent, TacticsRow row)
        {
            InitializeComponent();

            this.Owner = parent;
            this.ShowInTaskbar = false;

            MItem = row;

            this.Title = MItem.MColumnName + " " + this.Title;
            this.labType.Text = MItem.MColumnName;

            switch (MItem.MIndex)
            {
                case EnumTactics.NameReg:
                    this.chboxEnabled.IsEnabled = false;
                    this.chboxEnabled.IsChecked = 1 == MItem.MValue ? true : false;
                    this.labTitle.Visibility = Visibility.Hidden;
                    this.numValue.Visibility = Visibility.Hidden;
                    this.labUnit.Visibility = Visibility.Hidden;
                    break;
                case EnumTactics.PwdReg:
                    this.chboxEnabled.IsChecked = 1 == MItem.MValue ? true : false;
                    this.labTitle.Visibility = Visibility.Hidden;
                    this.numValue.Visibility = Visibility.Hidden;
                    this.labUnit.Visibility = Visibility.Hidden;
                    break;
                case EnumTactics.NameLock:
                case EnumTactics.PwdLength:
                case EnumTactics.PwdMaxTime:
                case EnumTactics.ScreenLock:
                    this.chboxEnabled.Visibility = Visibility.Hidden;
                    switch (MItem.MIndex)
                    {
                        case EnumTactics.NameLock:
                            numValue.Minimum = 0;
                            numValue.Maximum = 999;
                            break;
                        case EnumTactics.PwdLength:
                            numValue.Minimum = 1;
                            numValue.Maximum = 32;
                            break;
                        case EnumTactics.PwdMaxTime:
                            numValue.Minimum = 0;
                            numValue.Maximum = 999;
                            break;
                        case EnumTactics.ScreenLock:
                            numValue.Minimum = 0;
                            numValue.Maximum = 1440;
                            break;
                    }
                    this.numValue.Value = MItem.MValue;
                    this.labTitle.Text = 0 == MItem.MValue ? ReadXaml.GetTitle1(MItem.MIndex) : ReadXaml.GetTitle2(MItem.MIndex);
                    this.labUnit.Text = ReadXaml.GetUnit(MItem.MIndex);
                    break;
            }
            this.labInfo.Text = ReadXaml.GetInfo(MItem.MIndex);
        }

        /// <summary>
        /// 修改数值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (null != MItem && !string.IsNullOrEmpty(numValue.Text))
            {
                switch (MItem.MIndex)
                {
                    case EnumTactics.NameLock:
                    case EnumTactics.PwdLength:
                    case EnumTactics.PwdMaxTime:
                    case EnumTactics.ScreenLock:
                        this.labTitle.Text = 0 == Convert.ToInt16(numValue.Text) ? ReadXaml.GetTitle1(MItem.MIndex) : ReadXaml.GetTitle2(MItem.MIndex);
                        break;
                }
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            switch (MItem.MIndex)
            {
                case EnumTactics.NameReg:
                case EnumTactics.PwdReg:
                    if ((true == this.chboxEnabled.IsChecked ? 1 : 0) != MItem.MValue)
                    {
                        MItem.MValue = true == this.chboxEnabled.IsChecked ? 1 : 0;
                        AdministrationManager manager = new AdministrationManager();
                        manager.EditTacticsRow(MItem);
                        AuditTrails.AuditTrailsStatic.Instance().InsertRowOperate(this.labType.Text, true == this.chboxEnabled.IsChecked ? Share.ReadXaml.S_Enabled : Share.ReadXaml.S_Disabled);
                    }
                    break;
                case EnumTactics.NameLock:
                case EnumTactics.PwdLength:
                case EnumTactics.PwdMaxTime:
                case EnumTactics.ScreenLock:
                    if (numValue.Value != MItem.MValue)
                    {
                        int temp = MItem.MValue;
                        MItem.MValue = (int)numValue.Value;
                        AdministrationManager manager = new AdministrationManager();
                        manager.EditTacticsRow(MItem);
                        AuditTrails.AuditTrailsStatic.Instance().InsertRowOperate(this.labType.Text, temp.ToString() + " -> " + MItem.MValue);
                    }
                    break;
            }

            DialogResult = true;
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}