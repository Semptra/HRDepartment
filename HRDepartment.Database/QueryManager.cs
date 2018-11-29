using System;
using System.IO;
using System.Reflection;

namespace HRDepartment.Database
{
    public static class QueryManager
    {
        public static class Employee
        {
            public static string SelectAll()
            {
                return LoadQuery("Employee.SelectAll.sql");
            }

            public static string Delete(int id)
            {
                return LoadQuery("Employee.Delete.sql", id.ToString());
            }
        }

        private static string LoadQuery(string queryName, params string[] args)
        {
            string resourceName = "HRDepartment.Database.Queries." + queryName;

            var assembly = Assembly.GetExecutingAssembly();

            var names = assembly.GetManifestResourceNames();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd()
                    .Trim().Replace("\r", string.Empty)
                    .Trim().Replace("\n", " ")
                    .Trim().Replace("\t", string.Empty)
                    .Replace(Environment.NewLine, string.Empty);

                if (args != null)
                {
                    result = string.Format(result, args);
                }

                return result;
            }
        }
    }
}
