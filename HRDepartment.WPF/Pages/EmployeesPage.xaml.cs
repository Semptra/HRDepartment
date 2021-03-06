﻿using System.Data;
using System.Linq;
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
        public DataView EmployeesDataView = null;

        public EmployeesPage()
        {
            InitializeComponent();
        }

        #region Events

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

            int employeeId = int.Parse(selectedRow["Id"].ToString());
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

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedSearchColumn = this.SearchColumnComboBox.SelectedValue.ToString();
            string selectedSearchValue = this.SearchTextBox.Text;

            if (string.IsNullOrEmpty(selectedSearchValue))
            {
                EmployeesDataView.RowFilter = string.Empty;
            }
            else
            {
                EmployeesDataView.RowFilter = string.Format($"`{selectedSearchColumn}` LIKE '{selectedSearchValue}*'");
            }
        }

        private void AddDataGridButton_Click(object sender, RoutedEventArgs e)
        {
            Frames.MainFrame.Content = new AddEmployeePage();
        }

        #endregion

        public void RefreshDataGrid()
        {
            var query = QueryManager.Employee.SelectAll();
            var dataView = DbContext.Instance.ExecuteCommandToDataView(query);

            EmployeesDataView = dataView;

            this.EmployeesDataGrid.ItemsSource = EmployeesDataView;

            var headers = dataView.ToTable()
                .Columns
                .Cast<DataColumn>()
                .Select(x => x.ColumnName);

            this.SearchColumnComboBox.ItemsSource = headers;
            this.SearchColumnComboBox.SelectedIndex = 0;
        }
    }
}
