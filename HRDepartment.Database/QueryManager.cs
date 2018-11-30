using System;
using System.Globalization;
using System.IO;
using System.Linq;
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
                return LoadQuery("Employee.Delete.sql", id);
            }

            public static string Insert(Models.Employee employeeModel)
            {
                return LoadQuery("Employee.Insert.sql",
                    employeeModel.FirstName,
                    employeeModel.Surname,
                    employeeModel.LastName,
                    employeeModel.Gender,
                    employeeModel.Birthday,
                    employeeModel.PlaceOfResidence,
                    employeeModel.PlaceOfBirth,
                    employeeModel.PassportData,
                    employeeModel.Code,
                    employeeModel.ArmyServed,
                    employeeModel.FitForArmyServe);
            }
        }

        private static string LoadQuery(string queryName, params object[] args)
        {
            string resourceName = "HRDepartment.Database.Queries." + queryName;
            var assembly = Assembly.GetExecutingAssembly();

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
                    var wrappedValues = args.Select(x => WrapValue(x)).ToArray();
                    result = string.Format(result, wrappedValues);
                }

                return result;
            }
        }

        private static string WrapValue(object value)
        {
            if (value is null)
            {
                return "NULL";
            }

            switch(value)
            {
                case int intValue:
                    return intValue.ToString();
                case double doubleValue:
                    return doubleValue.ToString();
                case float floatValue:
                    return floatValue.ToString();
                case bool boolValue:
                    return boolValue ? "1" : "0";
                case DateTime dateTimeValue:
                    return $"'{dateTimeValue.ToString("yyyy'-'MM'-'dd HH':'mm':'ss", CultureInfo.InvariantCulture)}'";
                default:
                    return $"'{value.ToString()}'";
            }
        }
    }
}
