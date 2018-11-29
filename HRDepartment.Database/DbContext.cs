using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace HRDepartment.Database
{
    public class DbContext : IDisposable
    {
        private static readonly Lazy<DbContext> _instance = 
            new Lazy<DbContext>(() => new DbContext());

        public static DbContext Instance => _instance.Value;

        private readonly MySqlConnection _connection;

        private DbContext()
        {
            _connection = new MySqlConnection();
        }

        public void Open(string connectionString)
        {
            _connection.ConnectionString = connectionString;
            _connection.Open();
        }

        public MySqlCommand Command(string query)
        {
            return new MySqlCommand(query, _connection);
        }

        public DataView ExecuteCommandToDataView(string query)
        {
            using (var command = new MySqlCommand(query, _connection))
            using (var dataAdapter = new MySqlDataAdapter(command))
            {
                var dataSet = new DataSet();

                dataAdapter.Fill(dataSet);

                return dataSet.Tables[0].DefaultView;
            }
        }

        public void ExecuteCommandNonQuery(string query)
        {
            using (var command = new MySqlCommand(query, _connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();
        }
    }
}
