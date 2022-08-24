using HBBio.Result;
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
    /// RunStatusUC.xaml 的交互逻辑
    /// </summary>
    public partial class RunStatusUC : UserControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public RunStatusUC()
        {
            InitializeComponent();
            AddBorder(grid, Colors.Gray, 1);
        }

        public void SetEnumMethodType(EnumResultIconType type)
        {
            switch (type)
            {
                case EnumResultIconType.Manual:
                    grid.RowDefinitions[0].Height = new GridLength(0);
                    grid.RowDefinitions[1].Height = new GridLength(0);
                    grid.RowDefinitions[2].Height = new GridLength(0);
                    grid.RowDefinitions[3].Height = new GridLength(0);
                    grid.RowDefinitions[4].Height = new GridLength(0);
                    grid.RowDefinitions[5].Height = new GridLength(0);
                    grid.RowDefinitions[6].Height = new GridLength(0);
                    break;
                case EnumResultIconType.Method:
                    grid.RowDefinitions[0].Height = new GridLength(0);
                    grid.RowDefinitions[1].Height = new GridLength(0);
                    grid.RowDefinitions[2].Height = GridLength.Auto;
                    grid.RowDefinitions[3].Height = GridLength.Auto;
                    grid.RowDefinitions[4].Height = GridLength.Auto;
                    grid.RowDefinitions[5].Height = GridLength.Auto;
                    grid.RowDefinitions[6].Height = GridLength.Auto;
                    break;
                case EnumResultIconType.MethodQueue:
                    grid.RowDefinitions[0].Height = GridLength.Auto;
                    grid.RowDefinitions[1].Height = GridLength.Auto;
                    grid.RowDefinitions[2].Height = GridLength.Auto;
                    grid.RowDefinitions[3].Height = GridLength.Auto;
                    grid.RowDefinitions[4].Height = GridLength.Auto;
                    grid.RowDefinitions[5].Height = GridLength.Auto;
                    grid.RowDefinitions[6].Height = GridLength.Auto;
                    break;
            }
        }

        public void UpdateInfo(string methodQueueName, string methodCount, string methodName,
            string loopCount, string loopIndex,
            string phaseCount, string phaseName, string phaseRunning)
        {
            labMethodQueueName.Text = methodQueueName;
            labMethodCount.Text = methodCount;
            labMethodName.Text = methodName;
            labLoopCount.Text = loopCount;
            labLoopIndex.Text = loopIndex;
            labPhaseCount.Text = phaseCount;
            labPhaseName.Text = phaseName;
            labPhaseRunning.Text = phaseRunning;
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
