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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HBBio.SystemControl
{
    /// <summary>
    /// RunParametersUC.xaml 的交互逻辑
    /// </summary>
    public partial class RunParametersUC : UserControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public RunParametersUC()
        {
            InitializeComponent();
            AddBorder(grid, Colors.Gray, 1);
        }

        /// <summary>
        /// 更新显示
        /// </summary>
        /// <param name="methodQueueName"></param>
        /// <param name="methodCount"></param>
        /// <param name="methodName"></param>
        /// <param name="phaseCount"></param>
        /// <param name="phaseName"></param>
        /// <param name="phaseRunning"></param>
        public void UpdateInfo(string t, string v, string cv,
            string columnVol, string columnHeight, string resultPath)
        {
            if (t.Equals("-0.01"))
            {
                labT.Text = "";
                labV.Text = "";
                labCV.Text = "";
                labColumnVol.Text = "";
                labColumnHeight.Text = "";
                labResultPath.Text = "";
            }
            else
            {
                labT.Text = t;
                labV.Text = v;
                labCV.Text = cv;
                labColumnVol.Text = columnVol;
                labColumnHeight.Text = columnHeight;
                labResultPath.Text = resultPath;
            }
        }

        /// <summary>
        /// 添加Border
        /// </summary>
        private void AddBorder(Grid grid, Color color, double width)
        {
            int rows = grid.RowDefinitions.Count;
            int columns = grid.ColumnDefinitions.Count;
            for (int i = 0; i < rows; i++)
            {
                if (i != rows - 1)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        Border border = null;
                        if (j == columns - 1)
                        {
                            border = new Border()
                            {
                                BorderBrush = new SolidColorBrush(color),
                                BorderThickness = new Thickness(width, width, width, 0)
                            };
                        }
                        else
                        {
                            border = new Border()
                            {
                                BorderBrush = new SolidColorBrush(color),
                                BorderThickness = new Thickness(width, width, 0, 0)
                            };
                        }
                        Grid.SetRow(border, i);
                        Grid.SetColumn(border, j);
                        grid.Children.Add(border);
                    }
                }
                else
                {
                    for (int j = 0; j < columns; j++)
                    {
                        Border border = null;
                        if (j + 1 != columns)
                        {
                            border = new Border
                            {
                                BorderBrush = new SolidColorBrush(color),
                                BorderThickness = new Thickness(width, width, 0, width)
                            };
                        }
                        else
                        {
                            border = new Border
                            {
                                BorderBrush = new SolidColorBrush(color),
                                BorderThickness = new Thickness(width, width, width, width)
                            };
                        }
                        Grid.SetRow(border, i);
                        Grid.SetColumn(border, j);
                        grid.Children.Add(border);
                    }
                }
            }
        }
    }
}
