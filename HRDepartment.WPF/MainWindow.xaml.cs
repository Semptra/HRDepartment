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
        public MainWindow()
        {
            InitializeComponent();
            OpenDbConnection();
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

        private void NavigateToEmployeesPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Navigate(EmployeesPage.Uri);
        }
    }
}
