using ClosedXML.Excel;
using MvcTesting;
using Newtonsoft.Json;
using PA_System.Helper;
using PA_System.Infrastructure;
using PA_System.Infrastructure.Reporting;
using PA_System.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using context = System.Web.HttpContext;
using PADLL = DataDLL.Sources.PASystem;


namespace PA_System.Controllers
{
    public class EmployeeSurveyController : Controller
    {
        public static string fileUploadFilePath = string.Empty;
               

        // GET: EmployeeSurvey
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult UploadEmployeeSurvey()
        {
            GetEmployeeInformation();

            if(Roles.IsUserInRole(@"FUTUREGROWTH\FG-APP-PAS-ADMIN")) { return View(); }

            if (Roles.IsUserInRole(@"FUTUREGROWTH\FG-APP-PAS-MANAGER") || Roles.IsUserInRole(@"FUTUREGROWTH\FG-APP-PAS-ADMIN")) {return  RedirectToAction("ApproveQuestionnaire");  }

            if (Roles.IsUserInRole(@"FUTUREGROWTH\FG-APP-PAS-READER")) { return RedirectToAction("CompleteSurvey"); }

            return View();

            
        }

        //A post method used to preview an uploaded employee survey (excel spreadsheet)
        [HttpPost]
        [AccessDeniedAuthorize(Roles = @"FUTUREGROWTH\FG-APP-PAS-ADMIN")]
        public ActionResult UploadEmployeeSurvey(HttpPostedFileBase fileUploadClientFile)
        {
            var saveClientFile = string.Empty;

            var questionObject = new QuestionnaireObject();

            GetEmployeeInformation();

            //First Check if the uploaded file is not null
            if (fileUploadClientFile != null && fileUploadClientFile.ContentLength > 0)
            {
                //Save uploaded file in the survey temp folder - under App_Data
               var uploadStatus = SaveFile(fileUploadClientFile);

                questionObject.UploadStatus = uploadStatus;
            }
            

            if (string.IsNullOrWhiteSpace(saveClientFile)) return View(questionObject);

            return View(questionObject);
        }

        //Returns a list of all the employees
        public void GetEmployeeInformation()
        {
            var raterObject = new List<Rater>();

            var list = new ADUsers().GetAdGroupsForUser(Generic.GetCurrentLogonUserName(), new ADUsers().DomanName);

            var raterList = new List<Rater>();

            for (int i = 0; i < list.Count; i++)

            {

                //Pass a group name to return all the employee 
                var empGroupList = new ADUsers().GetUserPerByAdGroup("DG-Futuregrowth");

                for (int j = 0; j < empGroupList.Count; j++)
                {
                    //Ensure that THE employee first name and last name are not null prior adding them into the list
                    if (!string.IsNullOrWhiteSpace(empGroupList[j].EmployeeFirstName) && !string.IsNullOrWhiteSpace(empGroupList[j].EmployeeSurname))
                    {
                        raterList.Add(new Rater() { Name = empGroupList[j].EmployeeFirstName + " " + empGroupList[j].EmployeeSurname });
                    }                    
                }

                break;
            }

            //Store employee list in the viewBag - this is then called from  a view to populate employee (or raters) dropdown list(s)
            ViewBag.RaterList = raterList;
        }

        public ActionResult CaptureSurvey()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CaptureSurvey(Survey survey)
        {

            return View();
        }

        //Saving a spreadsheet file to a directory
        public string SaveFile(HttpPostedFileBase postedFile)
        {
            var bulkInsert = 0;

            var uploadxml = new PADLL.Mviews.SurveyView.Uploadxml();

            uploadxml.Type = "Questionnaire";

            uploadxml.User = Generic.GetCurrentLogonUserName();

            var surveyObject = new PADLL.Create.xmlLoad();

            var userName = Generic.GetCurrentLogonUserName();

            //Get the name of an employee from a form control

            //Get a selected survey period (Annual, Quarter and Month) from a hidden input control - the hidden file is set in javaScript and stored in hidden input control
            var questionnairePeriodType = Request["QuestionnairePeriodType"];

            //Get a selected survey period value (for example 201603 if it is a quartely survey) from a hidden input control - the hidden file is set in javaScript and stored in hidden input control
            var questionnairePeriodValue = Request["QuestionnairePeriodValue"];

            var questionObject = new QuestionnaireObject();

            //remove special characters from a questionnaire value
            questionnairePeriodValue = questionnairePeriodValue.Trim(',');

            try
            {
                //Ensure that uploaded file is not null
                if (postedFile.ContentLength > 0)
                {
                    var fileExtension = Path.GetExtension(postedFile.FileName);

                    //Creating a file name from loggedin username, date, time, minutes and seconds to ensure that it's unique
                    var fileName = Path.GetFileNameWithoutExtension(postedFile.FileName) + "_" + userName + "_" + DateTime.Now + DateTime.Now.Minute + DateTime.Now.Second + fileExtension;

                    //Removing uneccessary special characters from a file name
                    fileName = fileName.Replace(" ", "").Replace(":", "").Replace("AM", "").Replace("/", "").Replace("PM", "");

                    //get a file path from an uploaded file
                    var path = Path.Combine(Server.MapPath(Config.FileUploadPath), fileName);

                    postedFile.SaveAs(path);

                    //Process the file and return a loist of questions including the business unit
                    var surveyList = ExcelFileUploadHelper.ProcessFileUpload(path, fileExtension, questionnairePeriodType, questionnairePeriodValue);

                    uploadxml.Filexml = XMLHelper.Serialize(surveyList);

                    //Return uploaded file status
                    if (surveyList.Count() > 0 && !string.IsNullOrWhiteSpace(userName))
                    {
                        bulkInsert = surveyObject.xmlUpload(uploadxml);

                        if (bulkInsert > 0)
                        {
                            questionObject.UploadStatus = "success";
                        }
                        else
                        {
                            questionObject.UploadStatus = "failed";
                        }                     
                        //var bulkInsert = SurveyTask.SaveClientBulkInsert(username, clientList);
                    }
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
            return questionObject.UploadStatus;

        }

        [AccessDeniedAuthorize(Roles = @"FUTUREGROWTH\FG-APP-PAS-MANAGER")]
        public ActionResult ApproveQuestionnaire()
        {
            return View();
        }

        [AccessDeniedAuthorize(Roles = @"FUTUREGROWTH\FG-APP-PAS-READER")]
        public ActionResult CompleteSurvey()
        {
            return View();
            //var username = Generic.GetCurrentLogonUserName();

            //if (Environment.UserName.ToLower() == "acanter".ToLower() || Environment.UserName.ToLower() == "olgac".ToLower()) { return View(); }

            //if (username.ToLower() == "acanter".ToLower() || username == "olgac".ToLower()) { return View(); }

            //else { return RedirectToAction("PerformanceRatingSystemClosedFor2016"); }

        }

        public ActionResult PerformanceRatingSystemClosedFor2016()
        {
            return View();
        }

        [AccessDeniedAuthorize(Roles = @"FUTUREGROWTH\FG-APP-PAS-ADMIN")]
        public ActionResult DownloadTemplate()
        {
            var filePath = Server.MapPath(ConfigurationManager.AppSettings["sampleSurveyTemplatePath"]);

            var fileName = "PA_Upload _Template.xlsx";

            var mimeType = "application/vnd.ms-excel";

            return File(new FileStream(filePath+ fileName, FileMode.Open), mimeType, fileName);            
        }

        //Generate a survey template
        //Get column names from a webConfig file and generate a spreadsheet to be completed and uploaded into the database
        public void GenerateTemplate()
        {
            //Get current loggedin user name from AD
            var username = Generic.GetCurrentLogonUserName();

            //Pass a group name and return a list of users that have access to this group
            var employeeList = new ADUsers().GetUserPerByAdGroup("DG-Futuregrowth");

            //Return loggedin employee full by username from AD employee list
            var employeeFullName = employeeList.Where(c => c.UserName == username).First().DisplayName;


            //creating a worksheet
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = new DataTable();

                dt.TableName = employeeFullName;

                var list = Config.SurveyTemplateHeadings.Split(',');

                //Adding columns to a list
                for (int i = 0; i < list.Count(); i++)
                {
                    dt.Columns.Add(list[i]);
                }

                //Adding a datatable to a worksheet
                wb.Worksheets.Add(dt);

                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                wb.Style.Font.Bold = true;

                context.Current.Response.Clear();

                context.Current.Response.Buffer = true;

                context.Current.Response.Charset = "";

                context.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                context.Current.Response.AddHeader("content-disposition", "attachment;filename= " + employeeFullName + " - Survey"+ ".xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);

                    MyMemoryStream.WriteTo(context.Current.Response.OutputStream);

                    context.Current.Response.Flush();

                    context.Current.Response.End();
                }
            }

            //Setting employee ViewBag for populating employee and raters dropdown
            GetEmployeeInformation();
        }

        //Accepts a question object and save a question to a database
        [HttpPost]
        public ActionResult SaveQuestion(QuestionnaireObject questionObject)
        {
            var list = new List<QuestionnaireObject>();

            //Reading a list of questions that catched by the application
            var tempList = System.Web.Helpers.WebCache.Get("QuestionsEntered");

            var ratersObjectList = new List<Rater>();

            var questionObj = new QuestionnaireObject();

            questionObj.Question = questionObject.Question;

            var ratersList = questionObject.RateeName.Split(',');

            //Looping through a list of raters
            foreach (var item in ratersList)
            {
                ratersObjectList.Add(new Rater() { Name = item });
            }

            questionObj.Raters = questionObject.Raters;

            questionObj.KPIDesc = questionObject.KPIDesc;

            // if (tempList != null) { list.Add(tempList); }            

            list.Add(new QuestionnaireObject() { Question = questionObj.Question, KPIDesc = questionObj.KPIDesc, Raters = ratersObjectList, RatersString = questionObject.RateeName });

            if (tempList == null) { System.Web.Helpers.WebCache.Set("QuestionsEntered", list); }


            var data = JsonConvert.SerializeObject(list);

            return Json(data);
        }

        //Method to view rating comparison report
        [AccessDeniedAuthorize(Roles = @"FUTUREGROWTH\FG-APP-PAS-READER")]
        [HttpGet]
        public ActionResult ViewRatingComparison()
        {
            RatingComparisonResportModel report = new RatingComparisonResportModel();
            
            report.ComparisonReport = new RatingReport().ViewRatingComparison();

            return View(report);
        }

    }
}