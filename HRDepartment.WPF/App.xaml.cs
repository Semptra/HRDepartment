using System.Windows;
using HRDepartment.WPF.Helpers;

namespace HRDepartment.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
#if !DEBUG
            DispatcherUnhandledException += App_DispatcherUnhandledExceptionHandler;
#endif

            base.OnStartup(e);
        }

        private void App_DispatcherUnhandledExceptionHandler(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            InputHelper.DisplayError(e.Exception.Message);
            e.Handled = true;
        }
    }
}
