using System.Windows;
using Microsoft.Extensions.Configuration;
using HRDepartment.Database;
using HRDepartment.WPF.Pages;

namespace HRDepartment.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EmployeesPage _employeesPage = new EmployeesPage();
        private DepartmentsPage _departmentsPage = new DepartmentsPage(); 

        public MainWindow()
        {
            InitializeComponent();
            OpenDbConnection();

            Frames.MainFrame = this.ContentFrame;
        }

        private void OpenDbConnection()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("Default");

            var context = DbContext.Instance;

            context.Open(connectionString);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.ContentFrame.CanGoBack)
            {
                this.ContentFrame.NavigationService.GoBack();
            }
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.ContentFrame.CanGoForward)
            {
                this.ContentFrame.NavigationService.GoForward();
            }
        }

        private void NavigateToEmployeesPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = _employeesPage;

            if (_employeesPage.EmployeesDataView is null)
            {
                _employeesPage.RefreshDataGrid();
            }
        }

        private void NavigateToDepartmentsPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Content = _departmentsPage;

            if(_departmentsPage.DepartmentsDataView is null)
            {
                _departmentsPage.RefreshDataGrid();
            }
        }
    }
}
