using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Loto_App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        private void LogException(Exception ex, string source)
        {
            try
            {
                File.AppendAllText(
                    "crash.log",
                    $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {source}{Environment.NewLine}" +
                    ex.ToString() +
                    Environment.NewLine +
                    "------------------------------------------------------------" +
                    Environment.NewLine + Environment.NewLine);
            }
            catch
            {
                // Prevent logging failures from causing another crash.
            }
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            LogException(e.Exception, "DispatcherUnhandledException");

            // Leave this as false so the application still terminates.
            // This avoids hiding serious bugs.
            e.Handled = false;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                LogException(ex, "CurrentDomain.UnhandledException");
            }
            else
            {
                try
                {
                    File.AppendAllText(
                        "crash.log",
                        $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Unknown unhandled exception{Environment.NewLine}" +
                        "------------------------------------------------------------" +
                        Environment.NewLine + Environment.NewLine);
                }
                catch
                {
                }
            }
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            LogException(e.Exception, "TaskScheduler.UnobservedTaskException");

            // Prevents the process from being terminated by this event.
            e.SetObserved();
        }
    }
}