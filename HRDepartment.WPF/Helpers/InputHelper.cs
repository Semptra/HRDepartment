using System.Windows;

namespace HRDepartment.WPF.Helpers
{
    public static class InputHelper
    {
        public static void DisplayError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static bool ConfirmAction(string message)
        {
            var result = MessageBox.Show(message, "Confirm action", 
                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

            return result == MessageBoxResult.Yes;
        }

        public static void DisplayInformation(string caption, string message)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
