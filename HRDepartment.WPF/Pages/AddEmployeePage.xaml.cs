using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using HRDepartment.Database;
using HRDepartment.Database.Models;
using HRDepartment.WPF.Helpers;

namespace HRDepartment.WPF.Pages
{
    /// <summary>
    /// Interaction logic for AddEmployeePage.xaml
    /// </summary>
    public partial class AddEmployeePage : Page
    {
        public AddEmployeePage()
        {
            InitializeComponent();
        }

        #region Events

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!this.IsInputValid())
            {
                e.Handled = true;
                return;
            }

            var employee = new Employee
            {
                FirstName = this.FirstNameTextBox.Text,
                LastName = this.LastNameTextBox.Text,
                Surname = this.SurnameNameTextBox.Text,
                Gender = this.GenderComboBox.SelectedValue.ToString() == "Female",
                Birthday = this.BirthdayDatePicker.SelectedDate.Value,
                PlaceOfResidence = this.PlaceOfResidenceTextBox.Text,
                PlaceOfBirth = this.PlaceOfBirthTextBox.Text,
                PassportData = this.PassportDataTextBox.Text,
                Code = int.Parse(this.CodeTextBox.Text),
                ArmyServed = this.ArmyServedComboBox.SelectedValue.ToString() == "No",
                FitForArmyServe = this.FitForArmyServeComboBox.SelectedValue.ToString() == "No"
            };

            string query = QueryManager.Employee.Insert(employee);

            int executionResult = DbContext.Instance.ExecuteCommandNonQuery(query);

            if (executionResult == 1)
            {
                InputHelper.DisplayInformation("Success", "New employee has been added.");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frames.MainFrame.CanGoBack)
            {
                Frames.MainFrame.NavigationService.GoBack();
            }
        }

        private void CodeTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        #endregion

        private bool IsInputValid()
        {
            if (string.IsNullOrEmpty(this.FirstNameTextBox.Text))
            {
                InputHelper.DisplayError("First name cannot be empty.");
                return false;
            }

            if (string.IsNullOrEmpty(this.SurnameNameTextBox.Text))
            {
                InputHelper.DisplayError("Surname cannot be empty.");
                return false;
            }

            if (string.IsNullOrEmpty(this.GenderComboBox.SelectedValue.ToString()))
            {
                InputHelper.DisplayError("Gender cannot be empty.");
                return false;
            }

            if (this.BirthdayDatePicker.SelectedDate is null)
            {
                InputHelper.DisplayError("Birthday cannot be empty.");
                return false;
            }

            return true;
        }
    }
}
