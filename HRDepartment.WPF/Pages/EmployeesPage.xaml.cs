using System;
using System.Windows;
using System.Windows.Controls;
using HRDepartment.Database;
using HRDepartment.WPF.Extensions;
using HRDepartment.WPF.Helpers;

namespace HRDepartment.WPF.Pages
{
    /// <summary>
    /// Interaction logic for EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {
        public static readonly Uri Uri = new Uri("Pages/EmployeesPage.xaml", UriKind.RelativeOrAbsolute);

        public EmployeesPage()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            this.RefreshDataGrid();
            base.OnInitialized(e);
        }

        private void RefreshDataGridButton_Click(object sender, RoutedEventArgs e)
        {
            this.RefreshDataGrid();
        }

        private void DeleteDataGridButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = this.EmployeesDataGrid.SingleOrDefaultSelectedRow();

            if (selectedRow is null)
            {
                e.Handled = true;
                return;
            }

            int employeeId = (int)selectedRow["Id"];
            string employeeFullName = string.Join(" ", 
                selectedRow["FirstName"], selectedRow["LastName"], selectedRow["Surname"]);

            if (!InputHelper.ConfirmAction($"Do you want to delete employee #{employeeId} ({employeeFullName})?"))
            {
                e.Handled = true;
                return;
            }
            
            string query = QueryManager.Employee.Delete(employeeId);

            DbContext.Instance.ExecuteCommandNonQuery(query);

            this.RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            var query = QueryManager.Employee.SelectAll();
            var dataView = DbContext.Instance.ExecuteCommandToDataView(query);
            this.EmployeesDataGrid.ItemsSource = dataView;
        }
    }
}
