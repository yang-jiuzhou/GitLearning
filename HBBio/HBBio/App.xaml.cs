using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HBBio
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll", EntryPoint = "CreateMutex")]
        public static extern IntPtr CreateMutex(int lpSecurityAttributes, bool bInitialOwner, string lpName);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", EntryPoint = "GetLastError")]
        public static extern int GetLastError();
        private const int ERROR_ALREADY_EXISTS = 183;


        /// <summary>
        /// 构造函数
        /// </summary>
        public App()
        {
            //Task线程内未捕获异常处理事件
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;//Task异常 

            //UI线程未捕获异常处理事件（UI主线程）
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;

            //非UI线程未捕获异常处理事件(例如自己创建的一个子线程)
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        /// <summary>
        /// Task线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            try
            {
                var exception = e.Exception as Exception;
                if (exception != null)
                {
                    HandleException(exception);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                e.SetObserved();
            }
        }

        //非UI线程未捕获异常处理事件(例如自己创建的一个子线程)      
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var exception = e.ExceptionObject as Exception;
                if (exception != null)
                {
                    HandleException(exception);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                //ignore
            }
        }

        //UI线程未捕获异常处理事件（UI主线程）
        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                HandleException(e.Exception);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                e.Handled = true;
            }
        }
        private void HandleException(Exception ex)
        {
            SystemLog.SystemLogManager.LogWrite(ex);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            string appTitle = "HBBio";
            IntPtr hMutex = CreateMutex(0, true, appTitle);
            if (GetLastError() == ERROR_ALREADY_EXISTS)
            {
                SystemControl.SystemControlWin.s_new = false;
                Application.Current.Shutdown();
            }

            base.OnStartup(e);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            LoadLanguage();
        }

        /// <summary>
        /// 数值框阈值检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoubleFlow_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Xceed.Wpf.Toolkit.DoubleUpDown temp = (sender as Xceed.Wpf.Toolkit.DoubleUpDown);
            double result = 0;
            if (double.TryParse(temp.Text, out result))
            {
                if (result < temp.Minimum || result > temp.Maximum)
                {
                    temp.Foreground = System.Windows.Media.Brushes.Red;
                }
                else
                {
                    temp.Foreground = System.Windows.Media.Brushes.Black;
                }
            }
            else
            {
                temp.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        /// <summary>
        /// 数值框空文本检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoubleUpDown_LostFocus(object sender, RoutedEventArgs e)
        {
            Xceed.Wpf.Toolkit.DoubleUpDown temp = (sender as Xceed.Wpf.Toolkit.DoubleUpDown);
            if (string.IsNullOrEmpty(temp.Text))
            {
                temp.Value = temp.Minimum;
                temp.Foreground = System.Windows.Media.Brushes.Red;
            }
            else if (temp.Value < temp.Minimum || temp.Value > temp.Maximum)
            {
                temp.Foreground = System.Windows.Media.Brushes.Red;
                temp.Focus();
            }
            else
            {
                temp.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        //加载语言
        private void LoadLanguage()
        {
            CultureInfo currentCultureInfo = CultureInfo.CurrentCulture;
            ResourceDictionary langRd = null;
            try
            {
                langRd =
                Application.LoadComponent(
                new Uri(currentCultureInfo.Name + ".xaml", UriKind.Relative)) as ResourceDictionary;
            }
            catch
            {
            }
            if (langRd != null)
            {
                if (this.Resources.MergedDictionaries.Count > 0)
                {
                    this.Resources.MergedDictionaries.Clear();
                }
                this.Resources.MergedDictionaries.Add(langRd);
            }
        }
    }
}
