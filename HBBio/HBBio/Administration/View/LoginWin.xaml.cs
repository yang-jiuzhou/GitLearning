using HBBio.PassDog;
using HBBio.Share;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HBBio.Administration
{
    /// <summary>
    /// LoginWin.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWin : Window
    {
        /// <summary>
        /// 随机数
        /// </summary>
        private Random _random = new Random();

        /// <summary>
        /// 点信息阵距
        /// </summary>
        private PointInfo[,] _points = new PointInfo[11, 6];

        /// <summary>
        /// 计时器
        /// </summary>
        private DispatcherTimer _timer = new System.Windows.Threading.DispatcherTimer();

        /// <summary>
        /// 自定义事件，点击自检按钮时触发
        /// </summary>
        public static readonly RoutedEvent MAddUserEvent =
             EventManager.RegisterRoutedEvent("MAddUser", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(LoginWin));
        public event RoutedEventHandler MAddUser
        {
            add { AddHandler(MAddUserEvent, value); }
            remove { RemoveHandler(MAddUserEvent, value); }
        }

        /// <summary>
        /// 自定义事件，点击自检按钮时触发
        /// </summary>
        public static readonly RoutedEvent MCheckEvent =
             EventManager.RegisterRoutedEvent("MSelfCheck", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(LoginWin));
        public event RoutedEventHandler MSelfCheck
        {
            add { AddHandler(MCheckEvent, value); }
            remove { RemoveHandler(MCheckEvent, value); }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        public LoginWin()
        {
            InitializeComponent();

            //注册帧动画
            _timer.Tick += new EventHandler(PolyAnimation);
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 45);//一秒钟刷新24次
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            _timer.Stop();
        }

        /// <summary>
        /// 窗体尺寸变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _timer.Stop();
            Init();
            _timer.Start();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWin win = new RegisterWin(this);
            if (true == win.ShowDialog())
            {
                btnRegister.Visibility = Visibility.Hidden;

                RoutedEventArgs args = new RoutedEventArgs(MAddUserEvent, null);
                RaiseEvent(args);
            }
        }

        /// <summary>
        /// 自检
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_userName.Text) && txt_Pwd.Password.Equals("method"))
            {
                //隐藏功能，将方法流生成XML可读文件
                MethodEdit.MethodTable methodTable = new MethodEdit.MethodTable();
                methodTable.CreateXml();

                Result.ResultListTable resultListTable = new Result.ResultListTable();
                resultListTable.CreateXml();

                MessageBoxWin.Show(ReadXaml.S_SuccessTxt);
            }
            else
            {
                RoutedEventArgs args = new RoutedEventArgs(MCheckEvent, null);
                RaiseEvent(args);

                AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title + btnCheck.Content, labUserName.Text + txt_userName.Text);
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labPwd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AdministrationManager manager = new AdministrationManager();
            UserInfo item = null;
            string error = manager.GetUser(txt_userName.Text, out item);
            if (null == error)
            {
                UserPwdWin win = new UserPwdWin(this, item, AdministrationStatic.Instance().MTacticsInfo);
                win.ShowDialog();
            }
            else
            {
                AuditTrails.AuditTrailsStatic.Instance().InsertRowError(this.Title + labPwd.Text, labUserName.Text + txt_userName.Text + "\n" + error);

                MessageBoxWin.Show(error);
            }
        }

        /// <summary>
        /// 更多
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labMore_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Visibility.Visible == btnCheck.Visibility)
            {
                btnRegister.Visibility = Visibility.Hidden;
                btnCheck.Visibility = Visibility.Hidden;
            }
            else
            {
                AdministrationManager manager = new AdministrationManager();
                btnRegister.Visibility = manager.GetUserIsNull() ? Visibility.Visible : Visibility.Hidden;
                btnCheck.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// 禁止复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        /// <summary>
        /// 禁止复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.Handled = true;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            string error = AdministrationStatic.Instance().Login(txt_userName.Text, txt_Pwd.Password);
            if (null == error)
            {
                AuditTrails.AuditTrailsStatic.Instance().UpdateUserName(AdministrationStatic.Instance().MUserInfo.MUserName);
                AuditTrails.AuditTrailsStatic.Instance().InsertRowSystem(this.Title, labUserName.Text + txt_userName.Text);
#if DEBUG
                DialogResult = true;
#else
                PassDogValue strDogSN = null;
                CSentinel.GetDogInfo(ref strDogSN);
                string info = "";
                string errorInfor = "";
                if (!CSentinel.ReadInfo(ref info, ref errorInfor))//读加密狗信息失败
                {
                    if (MessageBoxResult.Yes == MessageBoxWin.Show("未检测到加密狗,请确认加密狗已插入!\r\n如果加密狗插入,点击 确定 按钮,检查加密狗\r\n如果无加密狗,点击 取消 按钮,退出系统", "", MessageBoxButton.YesNo, MessageBoxImage.Question))
                    {
                        CheckSNWin win = new CheckSNWin();//检查加密狗信息
                        win.MValue = strDogSN;
                        if (true == win.ShowDialog())
                        {
                            DialogResult = true;//进入系统
                        }
                        else
                        {
                            DialogResult = false;//退出系统
                        }
                    }
                    else
                    {
                        DialogResult = false;
                    }
                }
                else//读信息成功
                {
                    //从数据库读取保存的加密狗序列号
                    if (CSentinel.CompareMemery(strDogSN.MInfo, ref errorInfor))
                    {
                        DialogResult = true;
                    }
                    else//对比失败
                    {
                        //MessageBoxWin.Show("加密狗SN检查失败\r\n" + errorInfor);
                        CheckSNWin win = new CheckSNWin();//检查加密狗信息
                        win.MValue = strDogSN;
                        if (true == win.ShowDialog())
                        {
                            DialogResult = true;//进入系统
                        }
                        else
                        {
                            DialogResult = false;//退出系统
                        }
                    }
                }
#endif
            }
            else
            {
                AuditTrails.AuditTrailsStatic.Instance().InsertRowError(this.Title, labUserName.Text + txt_userName.Text + "\n" + error);

                MessageBoxWin.Show(error);
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == Share.MessageBoxWin.Show(Share.ReadXaml.GetResources("labClose"), "", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                DialogResult = false;
            }
        }


        /// <summary>
        /// 初始化阵距
        /// </summary>
        private void Init()
        {
            double lengthX = layout.ActualWidth / 10;
            double lengthY = layout.ActualHeight / 5;

            //生成阵距的点
            for (int i = 0; i < _points.GetLength(0); i++)
            {
                for (int j = 0; j < _points.GetLength(1); j++)
                {
                    double x = _random.Next(-11, 11);
                    double y = _random.Next(-6, 6);
                    _points[i, j] = new PointInfo()
                    {
                        X = i * lengthX,
                        Y = j * lengthY,
                        SpeedX = x / 24,
                        SpeedY = y / 24,
                        DistanceX = _random.Next(35, 106),
                        DistanceY = _random.Next(20, 40),
                        MovedX = 0,
                        MovedY = 0,
                        PolygonInfoList = new List<PolygonInfo>()
                    };
                }
            }

            byte r = (byte)_random.Next(0, 11);
            byte g = (byte)_random.Next(100, 201);
            int intb = g + _random.Next(50, 101);
            if (intb > 255)
                intb = 255;
            byte b = (byte)intb;

            //上一行取2个点 下一行取1个点
            for (int i = 0; i < _points.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < _points.GetLength(1) - 1; j++)
                {
                    Polygon poly = new Polygon();
                    poly.Points.Add(new Point(_points[i, j].X, _points[i, j].Y));
                    _points[i, j].PolygonInfoList.Add(new PolygonInfo() { PolygonRef = poly, PointIndex = 0 });
                    poly.Points.Add(new Point(_points[i + 1, j].X, _points[i + 1, j].Y));
                    _points[i + 1, j].PolygonInfoList.Add(new PolygonInfo() { PolygonRef = poly, PointIndex = 1 });
                    poly.Points.Add(new Point(_points[i + 1, j + 1].X, _points[i + 1, j + 1].Y));
                    _points[i + 1, j + 1].PolygonInfoList.Add(new PolygonInfo() { PolygonRef = poly, PointIndex = 2 });
                    poly.Fill = new SolidColorBrush(Color.FromRgb(r, g, (byte)b));
                    SetColorAnimation(poly);
                    layout.Children.Add(poly);
                }
            }

            //上一行取1个点 下一行取2个点
            for (int i = 0; i < _points.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < _points.GetLength(1) - 1; j++)
                {
                    Polygon poly = new Polygon();
                    poly.Points.Add(new Point(_points[i, j].X, _points[i, j].Y));
                    _points[i, j].PolygonInfoList.Add(new PolygonInfo() { PolygonRef = poly, PointIndex = 0 });
                    poly.Points.Add(new Point(_points[i, j + 1].X, _points[i, j + 1].Y));
                    _points[i, j + 1].PolygonInfoList.Add(new PolygonInfo() { PolygonRef = poly, PointIndex = 1 });
                    poly.Points.Add(new Point(_points[i + 1, j + 1].X, _points[i + 1, j + 1].Y));
                    _points[i + 1, j + 1].PolygonInfoList.Add(new PolygonInfo() { PolygonRef = poly, PointIndex = 2 });
                    poly.Fill = new SolidColorBrush(Color.FromRgb(r, g, (byte)b));
                    SetColorAnimation(poly);
                    layout.Children.Add(poly);
                }
            }
        }

        /// <summary>
        /// 设置颜色动画
        /// </summary>
        /// <param name="polygon">多边形</param>
        private void SetColorAnimation(UIElement polygon)
        {
            //颜色动画的时间 1-4秒随机
            Duration dur = new Duration(new TimeSpan(0, 0, _random.Next(1, 5)));
            //故事版
            Storyboard sb = new Storyboard()
            {
                Duration = dur
            };
            sb.Completed += (S, E) => //动画执行完成事件
            {
                //颜色动画完成之后 重新set一个颜色动画
                SetColorAnimation(polygon);
            };
            //颜色动画
            //颜色的RGB
            byte r = (byte)_random.Next(0, 11);
            byte g = (byte)_random.Next(100, 201);
            int intb = g + _random.Next(50, 101);
            if (intb > 255)
                intb = 255;
            byte b = (byte)intb;
            ColorAnimation ca = new ColorAnimation()
            {
                To = Color.FromRgb(r, g, b),
                Duration = dur
            };
            Storyboard.SetTarget(ca, polygon);
            Storyboard.SetTargetProperty(ca, new PropertyPath("Fill.Color"));
            sb.Children.Add(ca);
            sb.Begin(this);
        }

        /// <summary>
        /// 多边形变化动画
        /// </summary>
        private void PolyAnimation(object sender, EventArgs e)
        {
            //不改变阵距最外边一层的点
            for (int i = 1; i < _points.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < _points.GetLength(1) - 1; j++)
                {
                    PointInfo pointInfo = _points[i, j];
                    pointInfo.X += pointInfo.SpeedX;
                    pointInfo.Y += pointInfo.SpeedY;
                    pointInfo.MovedX += pointInfo.SpeedX;
                    pointInfo.MovedY += pointInfo.SpeedY;
                    if (pointInfo.MovedX >= pointInfo.DistanceX || pointInfo.MovedX <= -pointInfo.DistanceX)
                    {
                        pointInfo.SpeedX = -pointInfo.SpeedX;
                        pointInfo.MovedX = 0;
                    }
                    if (pointInfo.MovedY >= pointInfo.DistanceY || pointInfo.MovedY <= -pointInfo.DistanceY)
                    {
                        pointInfo.SpeedY = -pointInfo.SpeedY;
                        pointInfo.MovedY = 0;
                    }
                    //改变多边形的点
                    foreach (PolygonInfo pInfo in _points[i, j].PolygonInfoList)
                    {
                        pInfo.PolygonRef.Points[pInfo.PointIndex] = new Point(pointInfo.X, pointInfo.Y);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 阵距点信息
    /// </summary>
    public class PointInfo
    {
        /// <summary>
        /// X坐标
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Y坐标
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// X轴速度 wpf距离单位/二十四分之一秒
        /// </summary>
        public double SpeedX { get; set; }

        /// <summary>
        /// Y轴速度 wpf距离单位/二十四分之一秒
        /// </summary>
        public double SpeedY { get; set; }

        /// <summary>
        /// X轴需要移动的距离
        /// </summary>
        public double DistanceX { get; set; }

        /// <summary>
        /// Y轴需要移动的距离
        /// </summary>
        public double DistanceY { get; set; }

        /// <summary>
        /// X轴已经移动的距离
        /// </summary>
        public double MovedX { get; set; }

        /// <summary>
        /// Y轴已经移动的距离
        /// </summary>
        public double MovedY { get; set; }

        /// <summary>
        /// 多边形信息列表
        /// </summary>
        public List<PolygonInfo> PolygonInfoList { get; set; }
    }

    /// <summary>
    /// 多边形信息
    /// </summary>
    public class PolygonInfo
    {
        /// <summary>
        /// 对多边形的引用
        /// </summary>
        public Polygon PolygonRef { get; set; }

        /// <summary>
        /// 需要改变的点的索引
        /// </summary>
        public int PointIndex { get; set; }
    }
}