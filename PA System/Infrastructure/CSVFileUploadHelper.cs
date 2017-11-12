using PA_System.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PA_System.Infrastructure
{
    public class CSVFileUploadHelper
    {
        //This method accept fileName (filePath) as a parameter - reads and processes an uploaded file 
        //If the file is valid the method returns a list of all the csv records
        public static List<EmployeeObject> ProcessEmployeeCSVFile(string fileName)
        {
            string[] empListArray;

            //Initializing employee list
            List<EmployeeObject> employeeList = new List<EmployeeObject>();

            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    var fileHeaderLine = sr.ReadLine().TrimEnd(',');

                    string line;

                    //Compares uploaded file header against existing csv file header columns
                    //if the columns do not match then the operation will stop and an empty list will be returned
                    if (!verifyUploadedTemplate(fileHeaderLine))
                    {
                        //employeeList.Add(new EmployeeObject() { Message = "File format is different to employee data csv file format" });

                        return employeeList;
                    }
                    //Reading employee records line by line
                    while ((line = sr.ReadLine()) != null)
                    {
                        //Storing the records into an array
                        empListArray = line.Split(',');

                        var employeeObject = new EmployeeObject();

                        employeeObject.EmployeeNumber = empListArray[1];

                        employeeObject.EmployeeFirstName = empListArray[4].ToString();

                        employeeObject.EmployeeSurname = empListArray[5].ToString();

                        //Validating employee records - ensures that at least a name and surname are not empty otherwise a record will not added to the list 
                        if (!string.IsNullOrWhiteSpace(employeeObject.EmployeeFirstName) && !string.IsNullOrWhiteSpace(employeeObject.EmployeeSurname))
                        {
                            employeeObject.EmployeeNumber = empListArray[0];

                            employeeObject.EmployeeFirstName = empListArray[1].ToString().Trim();

                            employeeObject.EmployeeSurname = empListArray[2].ToString();

                            employeeObject.Team = empListArray[3].ToString();

                            employeeObject.JobTitle = !string.IsNullOrWhiteSpace(empListArray[3].ToString()) ? empListArray[4].ToString() : "N/A";

                            employeeObject.Manager = !string.IsNullOrWhiteSpace(empListArray[5].ToString()) ? empListArray[5].ToString() : "N/A" ;

                            employeeList.Add(employeeObject);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            //var xml = XMLHelper.Serialize(employeeList);

            return employeeList;

        }

        //Verify csv file header / columns against uploaded file
        //Returns true if the column match otherwise return false if they do not match
        public static bool verifyUploadedTemplate(string uploadedColumnNameString)
        {
            var surveyTemplate = Config.EmployeeDataTemplateHeadings;

            //first remove all the empty spaces, compare the headings and return false if they do not match
            if (!(uploadedColumnNameString.ToLower().Replace(" ", "").Trim() == surveyTemplate.ToLower().Replace(" ", "").Trim())) return false;

            return true;
        }
    }
}