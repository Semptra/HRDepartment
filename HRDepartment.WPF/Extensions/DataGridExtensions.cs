using System.Data;
using System.Windows.Controls;

namespace HRDepartment.WPF.Extensions
{
    public static class DataGridExtensions
    {
        public static DataRowView SingleOrDefaultSelectedRow(this DataGrid dataGrid)
        {
            if (dataGrid.SelectedItems.Count != 1)
            {
                return null;
            }

            return dataGrid.SelectedItems[0] as DataRowView;
        }
    }
}
