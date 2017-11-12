using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PA_System.Infrastructure
{
    public class Employee
    {
        public Employee()
        {
            EmployeeList = new List<Employee>();

            DepartmentList = new List<Employee>();
        }

        public string EmployeeNumber { get; set; }

        public string EmployeeFirstName { get; set; }

        public string EmployeeSurname { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public string CSVFileStatus { get; set; }

        public List<Employee> EmployeeList { get; set; }

        public string Department { get; set; }

        public string DepartmentValue { get; set; }

        public List<Employee> DepartmentList { get; set; }

        public string DisplayName { get; set; }

        public string Team { get; set; }

        public string JobTitle { get; set; }

        public string Manager { get; set; }

        public string  SurveyStatus { get; set; }


    }
    public class Department {

        public string Name { get; set; }

        public string Value { get; set; }

    }

    public class EmployeeObject
    {
        public string EmployeeNumber { get; set; }

        public string EmployeeFirstName { get; set; }

        public string EmployeeSurname { get; set; }

        public string Team { get; set; }

        public string JobTitle { get; set; }

        public string Manager { get; set; }

    }
}