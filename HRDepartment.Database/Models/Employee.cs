using System;

namespace HRDepartment.Database.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string LastName { get; set; }

        public bool Gender { get; set; }

        public DateTime Birthday { get; set; }

        public string PlaceOfResidence { get; set; }

        public string PlaceOfBirth { get; set; }

        public string PassportData { get; set; }

        public int? Code { get; set; }

        public bool? ArmyServed { get; set; }

        public bool? FitForArmyServe { get; set; }
    }
}
