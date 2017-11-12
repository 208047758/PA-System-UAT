using Newtonsoft.Json;
using PA_System.Helper;
using PA_System.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PA_System.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult UploadEmployeeData()
        {
            return View();
        }

        //This action is called when a user clicks a submit after uploading a template
        [HttpPost]
        public ActionResult UploadEmployeeData(HttpPostedFileBase fileUploadEmployeeData)
        {
            var saveEmployeeDataFile = string.Empty;

            var csvfilePath = string.Empty;

            var employeeBulkInsert = 0;

            var empObject = new Employee();

            //Get loggedin username from AD
            var userName = Generic.GetCurrentLogonUserName();

            //Checks if an uploaded file is valid and that the file content is not empty
            if (fileUploadEmployeeData != null && fileUploadEmployeeData.ContentLength > 0)
            {
                //Verify whether the uploaded file is an excel spreadsheet
                if (VerifyFileExtension(fileUploadEmployeeData))
                {
                    //Calls method to save a file to folder found in the App_Data directory
                    csvfilePath = SaveFile(fileUploadEmployeeData);
                }

                //Pass a csv file path which will be processed and returns a list of type employee from a csv file
                var employeeInfoList = CSVFileUploadHelper.ProcessEmployeeCSVFile(csvfilePath);

                //check and Verify uploaded file format and returns a message if it is not a csv file
                var validCSVFile = (employeeInfoList.Select( c => c.EmployeeFirstName).Take(5).First().Count() > 0) ? "" : "File format is different to employee data csv file format";

                if (!string.IsNullOrWhiteSpace(validCSVFile))
                {
                    empObject.CSVFileStatus = validCSVFile;

                    return View(empObject);
                }
                else
                {
                    empObject.CSVFileStatus = "Success";
                }

                if (employeeInfoList.Count() > 0 && !string.IsNullOrWhiteSpace(userName))
                {
                    //employeeBulkInsert = EmployeeTask.SaveEmployeeDataBulkInsert(userName, employeeInfoList);
                }

                if (employeeBulkInsert > 0)
                {
                    ViewBag.NumberOfRowsinserted = employeeInfoList.Count();
                }

                return View(empObject);

            }

            return View(empObject);
        }

        //Saving an uploaded file into uploads folder found in App_Data folder
        public string SaveFile(HttpPostedFileBase postedFile)
        {
            //Get current loggedin user from AD
            var username = Generic.GetCurrentLogonUserName();

            var csvFilePath = string.Empty;

            try
            {
                //First check if the uploaded file is not empty and process it if it is valid
                if (postedFile.ContentLength > 0)
                {
                    //Get file extension from am uploaded file
                    var fileExtension = Path.GetExtension(postedFile.FileName);

                    //Creating a file name by appending username, date, time, minutes and seconds to ensure that is always unique
                    var fileName = Path.GetFileNameWithoutExtension(postedFile.FileName) + "_" + username + "_" + DateTime.Now + DateTime.Now.Minute + DateTime.Now.Second + fileExtension;

                    //Removing special characters that are not needed in the file name
                    fileName = fileName.Replace(" ", "").Replace(":", "").Replace("AM", "").Replace("/", "").Replace("PM", "");

                    //Get recently saved file path 
                    csvFilePath = Path.Combine(Server.MapPath(Config.EmployeeDataCSVFilePath), fileName);

                    //Save the file in the uploads folder
                    postedFile.SaveAs(csvFilePath);
                }

            }
            catch (Exception ex)
            {

                //logger.Error(ex.Message.ToString());
            }

            return csvFilePath;

        }        

        //Verify uploaded file and ensure that the file extension is .csv
        public static bool VerifyFileExtension(HttpPostedFileBase fileUploadEmployeeData)
        {
            if (fileUploadEmployeeData == null) return false;
            
            if (Path.GetExtension(fileUploadEmployeeData.FileName) == ".csv")
            {
                return true;
            }

            return true;
        }
    }
}