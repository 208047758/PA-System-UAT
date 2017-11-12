using Newtonsoft.Json;
using PA_System.Helper;
using PA_System.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PA_System.Controllers
{
    public class EmployeeAPIController : ApiController
    {
        // GET api/EmployeeAPI
        //Returns employee information from AD
        public IHttpActionResult GetEmployees()
        {
            var employee = new Employee();

            var empList = new List<Employee>();
            
            //Get AD groups from AD - this will therefore be used mostly for filtering purposes and other operations
            var ADGroupList = new ADUsers().GetAdGroupsForUser(Generic.GetCurrentLogonUserName(), new ADUsers().DomanName);

            //Loop through AD group information and get employees that have access to each group
            for (int i = 0; i < ADGroupList.Count(); i++)
            {
                //This calls a method to get employees from ADUsers method and add them to a list
                var ADEmployees = new ADUsers().GetUserPerByAdGroup(ADGroupList[i]);

                foreach (var empName in ADEmployees)
                {
                    empList = new List<Employee>() { new Employee() { EmployeeFirstName = empName.EmployeeFirstName, Department = ADGroupList[i] } };
                }
                
            }
            //Serializing employee list so that it can be transported as an object back to client
            var data = JsonConvert.SerializeObject(empList);

            return Ok(empList);
        }

        //This method searches for an employee by id from a list of employee, it then returns an employee object back to the client
        public IHttpActionResult GetEmployees(int id)
        {
            var employee = new EmployeeTemp();

            var empList = new List<EmployeeTemp>() { new EmployeeTemp() { EmpNo = 1, EmpName = "Abongile Bantom", Salary = 10000, DeptName = "Human Resources", Designation = "HR Officer" } };

            empList.Add(new EmployeeTemp() { EmpNo = 2, EmpName = "Andile Koka", Salary = 5000, DeptName = "Information Technology", Designation = "Software Developer" } );

            empList = empList.Where(c => c.EmpNo == id).ToList();

            employee.EmpNo = empList[0].EmpNo;

            employee.EmpName = empList[0].EmpName;

            employee.Salary = empList[0].Salary;

            employee.DeptName = empList[0].DeptName;

            employee.Designation = empList[0].Designation;

            return Ok(employee);
        }
    }
    public partial class EmployeeTemp
    {
        public int EmpNo { get; set; }
        public string EmpName { get; set; }
        public int Salary { get; set; }
        public string DeptName { get; set; }
        public string Designation { get; set; }
    }
}
