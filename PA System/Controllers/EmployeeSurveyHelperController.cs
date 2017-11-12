using DataDLL.Sources.PASystem.Read;
using DataDLL.Sources.PASystem.Update;
using MvcTesting;
using Newtonsoft.Json;
using PA_System.Helper;
using PA_System.Infrastructure;
using PA_System.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Http;
using static DataDLL.Sources.PASystem.Mviews.SurveyView;

namespace PA_System.Controllers
{
    public class EmployeeSurveyHelperController : ApiController
    {
        public List<QuestionnaireObject> tempList = new List<QuestionnaireObject>();

        //This Action Result gets a list of all the employees from AD
        //It first gets a list of AD group list and get all the employees that have access to each of all those groups
        [HttpGet]
        public IHttpActionResult GetEmployees()
        {
            var employee = new Employee();

            var surveyList = new List<Employee>();

            var employeeList = new Surveys().getSurveyTeamEmployeeList();

            return Ok(employeeList);
        }

        //A Get Method to return business Unit List        
        [HttpGet]
        public IHttpActionResult GetBusinessUnitList()
        {

            var departmentList = new List<DepartmentDropDown>();

            var counter = 1;

            //Get AD group list
            var fgTeamList = new Surveys().getSurveyTeams();

            foreach (var item in fgTeamList)
            {
                departmentList.Add(new DepartmentDropDown()
                {
                    Name = item.Team.Replace("&", "and"),

                    Value = counter.ToString()
                });

                counter++;
            }

            var data = JsonConvert.SerializeObject(departmentList);

            return Ok(departmentList);
        }

        //Get survey questions by employee
        [HttpGet]
        public IHttpActionResult GetEmployeeSurveyQuestions(string id)
        {
            //First get a list of all the questions

            var Survey = new Surveys();

            var surveyObject = new SurveyRaterSurvey();

            surveyObject.Type = "Annually";

            surveyObject.Period = "2016";

            surveyObject.Rater = new Surveys().getSurveyTeamEmployeeList().Where(c => c.AD_User_Name.ToLower() == Generic.GetCurrentLogonUserName().ToLower()).First().Employee;

            surveyObject.Ratee = id;

            try
            {
                var surveyList = new Surveys().getSurveyFor(surveyObject);

                //for (int i = 0; i < surveyList.Count(); i++)
                //{
                //    surveyList[counter].Q_No = counter;

                //    counter++;
                //}

                return Ok(surveyList.Distinct().ToList());
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }

        //[HttpGet]
        //public IHttpActionResult GetEmployeeSurveyQuestionsForraterUpdate(string id)
        //{

        //    var empSurveyObject = GetSurveyObject("");

        //    var Survey = new Surveys();

        //    empSurveyObject.Survey_For = id;

        //    var surveyObject = new SurveyRaterSurvey();

        //    surveyObject.Type = "Annually";

        //    surveyObject.Period = "2016";

        //    surveyObject.Rater = new Surveys().getSurveyTeamEmployeeList().Where(c => c.AD_User_Name.ToLower() == Generic.GetCurrentLogonUserName().ToLower()).First().Employee;

        //    surveyObject.Ratee = id;

        //    var raterListString = string.Empty;

        //    var surveyForList = new Surveys().getSurveyForVerifyAllRaters(empSurveyObject).Select(c => c.Rater).Distinct().ToList();

        //    // var raterList = surveyForList.Select(c => c.Rater).Distinct().ToList();        

        //    var list = new Surveys().getSurveyFor(surveyObject);

        //    foreach (var surveyObj in surveyForList.Distinct().ToList())
        //    {
        //        raterListString += surveyObj + ",";  

        //    }

        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        list[i].Rater = raterListString;
        //    }

        //    try
        //    {
        //        //var surveyList = new Surveys().getSurveyFor(surveyObject);

        //        return Ok(list.Distinct().ToList());
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message);
        //    }


        //}

        [HttpGet]
        public IHttpActionResult GetEmployeeSurveyQuestionsForraterUpdate(string id)
        {

            var surveyObject = new SurveyForList();

            surveyObject.Type = "Annually";

            surveyObject.Period = "2016";

            surveyObject.Survey_For = id;

            var raterListString = string.Empty;

            var surveyForList = new Surveys().getSurveyForListRaters(surveyObject);


            //foreach (var surveyObj in surveyForList.Distinct().ToList())
            //{
            //    raterListString += surveyObj + ",";

            //}

            //for (int i = 0; i < list.Count; i++)
            //{
            //    list[i].Rater = raterListString;
            //}

            try
            {
                //var surveyList = new Surveys().getSurveyFor(surveyObject);

                return Ok(surveyForList.Distinct().ToList());
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }



        //Get survey questions by employee
        [HttpGet]
        public IHttpActionResult GetEmployeeSurveyQuestionsForReview(string id)
        {
            //First get a list of all the questions

            var Survey = new Surveys();

            var surveyObject = new SurveyForList();

            surveyObject.Type = "Annually";

            surveyObject.Period = "2016";

            surveyObject.Survey_For = id;

            //surveyObject.Rater = new Surveys().getSurveyTeamEmployeeList().Where(c => c.AD_User_Name.ToLower() == Generic.GetCurrentLogonUserName().ToLower()).First().Employee;

            //surveyObject.Ratee = id;

            try
            {
                var surveyList = new Surveys().getSurveyForListRaters(surveyObject).Distinct().ToList();

                return Ok(surveyList.Distinct().ToList());
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }

        [HttpGet]
        public IHttpActionResult GetSurveyForVerifybyRater(string id)
        {
            //First get a list of all the questions

            var Survey = new Surveys();

            var surveyObject = new SurveyRaterSurvey();

            surveyObject.Type = "Annually";

            surveyObject.Period = "2016";

            surveyObject.Manager = new Surveys().getSurveyTeamEmployeeList().Where(c => c.AD_User_Name.ToLower() == Generic.GetCurrentLogonUserName().ToLower()).First().Employee;

            surveyObject.Ratee = id;

            surveyObject.Rater = surveyObject.Manager;

            try
            {
                var surveyList = new Surveys().getSurveyForVerifybyRater(surveyObject);

                return Ok(surveyList.Distinct().ToList());
            }
            catch (Exception ex)
            {
                MailMessage mail = new MailMessage();

                SmtpClient SmtpServer = new SmtpClient("fgnotes.futuregrowth.co.za");

                mail.From = new MailAddress("tdaniels@futuregrowth.co.za");

                mail.To.Add("akoka@futuregrowth.co.za");

                mail.Subject = "HR Performance System - Verify Survey - Manager";

                mail.Body = ex.Message + "User: " + Generic.GetCurrentLogonUserName();

                SmtpServer.Port = 25;

                SmtpServer.Send(mail);

                throw new Exception(ex.Message);
            }


        }

        //[HttpGet]
        //public IHttpActionResult GetEmployeeSurveyQuestionsForVerification(string id)
        //{
        //    //First get a list of all the questions

        //    var Survey = new Surveys();

        //    var surveyObject = new SurveyPeriod();

        //    surveyObject.Type = "Annually";

        //    surveyObject.Period = "2016";

        //    surveyObject.Rater = new Surveys().getAllSurveysListStatus( (surveyObject).Where(c => c.AD_User_Name.ToLower() == Generic.GetCurrentLogonUserName().ToLower()).First().Employee;

        //    surveyObject.Ratee = id;

        //    try
        //    {
        //        var surveyList = new Surveys().getSurveyFor(surveyObject);

        //        return Ok(surveyList.Distinct().ToList());
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message);
        //    }


        //}

        //This action result gets uploaded survey for preview purposes
        //It displays a list of all the questions that are in the uploaded spreadsheet so that a user could see if the correct questions are uploaded
        [AccessDeniedAuthorize(Roles = @"FUTUREGROWTH\FG-APP-PAS-ADMIN")]
        [HttpPost]
        public IHttpActionResult UploadFile()
        {
            //Get current loggedin user name from AD
            var userName = Generic.GetCurrentLogonUserName();

            var questionnaireObject = new QuestionnaireObject();

            var surveyList = new List<QuestionnaireObject>();

            var questionPreviewList = new List<QuestionnaireObject>();

            //Ensure that uploaded file has content otherwise exit the method
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get uploaded file 
                var postedFile = HttpContext.Current.Request.Files["UploadedFile"];

                if (postedFile.ContentLength > 0)
                {
                    //get file extension from the uploaded excel file
                    var fileExtension = Path.GetExtension(postedFile.FileName);

                    //Create a unique file name from a username, date, minutes and seconds
                    var fileName = Path.GetFileNameWithoutExtension(postedFile.FileName) + "_" + userName + "_" + DateTime.Now + DateTime.Now.Minute + DateTime.Now.Second + fileExtension;

                    //Remove unneccessary special characters from a file name
                    fileName = fileName.Replace(" ", "").Replace(":", "").Replace("AM", "").Replace("/", "").Replace("PM", "");

                    //Get file upload file path from a web config
                    var path = Path.Combine(HttpContext.Current.Server.MapPath(Config.FileUploadPath), fileName);

                    //Save uploaded file into the uploads folder - found unser App_Data
                    postedFile.SaveAs(path);

                    try
                    {
                        surveyList = ExcelFileUploadHelper.ProcessFileUpload(path, fileExtension, "", "");
                    }
                    catch (Exception ex)
                    {
                        MailMessage mail = new MailMessage();

                        SmtpClient SmtpServer = new SmtpClient("fgnotes.futuregrowth.co.za");

                        mail.From = new MailAddress("tdaniels@futuregrowth.co.za");

                        mail.To.Add("akoka@futuregrowth.co.za");

                        mail.Subject = "HR Performance System - Complete Survey Request Notification";

                        mail.Body = ex.Message + "User: " + Generic.GetCurrentLogonUserName();

                        SmtpServer.Port = 25;

                        SmtpServer.Send(mail);
                        // var data = JsonConvert.SerializeObject(questionPreviewList);

                        return InternalServerError(ex);
                    }



                    foreach (var item in surveyList)
                    {
                        //Converting comma separated list of raters string to an array
                        var raters = string.Join(", ", item.Raters.ConvertAll(r => string.Format("'{0}'", r.Name)).ToArray());

                        raters = raters.ToString().Replace("'", "");

                        //Adding questions and raters to questionPreviewList 
                        questionPreviewList.Add(new QuestionnaireObject() { Question = item.Question, RateeName = raters });
                    }



                }
            }
            //Serialize the list
            var data = JsonConvert.SerializeObject(questionPreviewList);

            return Ok(data);
        }

        //This method returns 1 if the survey question is updated successfully
        [AccessDeniedAuthorize(Roles = @"FUTUREGROWTH\FG-APP-PAS-READER")]
        [HttpPost]
        public IHttpActionResult SaveRateEmployee(SurveyRate survey)
        {
            if (string.IsNullOrWhiteSpace(survey.Comment)) return Ok();

            try
            {
                survey.Type = "Annually";

                survey.Period = "2016";

                survey.User = Generic.GetCurrentLogonUserName();

                survey.Rater = new Surveys().getSurveyTeamEmployeeList().Where(c => c.AD_User_Name.ToLower() == survey.User.ToLower()).First().Employee;

                var insertStatus = new SurveyUpdate().SurveyRating(survey);

                return Ok(insertStatus);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        //Finalise Survey
        [HttpPost]
        public IHttpActionResult FinaliseSurvey(SurveyComplete survey)
        {
            try
            {
                survey.Type = "Annually";

                survey.Period = "2016";

                survey.Comment = !string.IsNullOrWhiteSpace(survey.Comment) ? survey.Comment : "No Comment";

                survey.User = Generic.GetCurrentLogonUserName();

                survey.Rater = new Surveys().getSurveyTeamEmployeeList().Where(c => c.AD_User_Name.ToLower() == survey.User.ToLower()).First().Employee;

                var insertStatus = new SurveyUpdate().SurveyCompletion(survey);

                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //Finalise Survey
        [HttpPost]
        public IHttpActionResult ReOpenSurvey(SurveyComplete survey)
        {
            try
            {
                survey.Type = "Annually";

                survey.Period = "2016";

                survey.Comment = string.Empty;

                survey.User = Generic.GetCurrentLogonUserName();

                survey.Rater = new Surveys().getSurveyTeamEmployeeList().Where(c => c.AD_User_Name.ToLower() == survey.User.ToLower()).First().Employee;

                var insertStatus = new SurveyUpdate().SurveyReOpen(survey);

                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpGet]
        public IHttpActionResult GetNextQuestion(string id, int questionNumber, string direction)
        {

            try
            {
                var surveyObject = new SurveyRaterSurvey();

                surveyObject.Type = "Annually";

                surveyObject.Period = "2016";

                surveyObject.Rater = new Surveys().getSurveyTeamEmployeeList().Where(c => c.AD_User_Name.ToLower() == Generic.GetCurrentLogonUserName().ToLower()).First().Employee;

                surveyObject.Ratee = id;

                var surveyList = new Surveys().getSurveyFor(surveyObject);

                surveyList = direction == "Next" ? surveyList.Where(c => c.Q_No == questionNumber + 1).Distinct().ToList() : surveyList.Where(c => c.Q_No == questionNumber - 1).Distinct().ToList();

                // surveyList = surveyList.Where(c => c.Q_No == questionNumber + 1).Distinct().ToList();

                var nextQuestionObject = new SurveyQuestionnaire();

                if (surveyList.Count() > 0)
                {
                    nextQuestionObject.Q_No = surveyList[0].Q_No;

                    nextQuestionObject.Question = surveyList[0].Question;

                    nextQuestionObject.Rating = surveyList[0].Rating;

                    nextQuestionObject.Question_ID = surveyList[0].Question_ID;

                    nextQuestionObject.Rater_Comment = surveyList[0].Rater_Comment;
                }


                return Ok(nextQuestionObject);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        //Get survey rating information - returns a list for populating rating dropdown list
        public IHttpActionResult GetRatings()
        {
            try
            {
                var ratingList = new Surveys().getSurveyRatingScale();

                return Ok(ratingList.Where(c => c.RatingDescription != "").ToList());
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IHttpActionResult GetRatingsDropDown()
        {
            try
            {
                var ratingList = new Surveys().getSurveyRatingScale();

                return Ok(ratingList);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IHttpActionResult GetDepartmentInformation()
        {
            var fgTeamList = new Surveys().getSurveyTeams();

            return Ok(fgTeamList);
        }

        //Get employee by group name
        [HttpGet]
        public IHttpActionResult PopulateUserInformation(string adGroupName)
        {
            var surveyTeam = new SurveyTeam();

            surveyTeam.Team = adGroupName;

            return Ok(new Surveys().getSurveyTeamList(surveyTeam));
        }

        //Save entered question into the database
        //An entered question is appended into a catched list and only when user click a submit button from a view will the questions be saved into the database
        [HttpPost]
        public IHttpActionResult SaveQuestion(QuestionnaireObject questionObject)
        {
            var list = new List<QuestionnaireObject>();

            tempList = System.Web.Helpers.WebCache.Get("QuestionsEntered") != null ? System.Web.Helpers.WebCache.Get("QuestionsEntered") : new List<QuestionnaireObject>();

            var questionObj = new QuestionnaireObject();

            var ratersObjectList = new List<Rater>();

            questionObj.Question = questionObject.Question;

            questionObj.Raters = questionObject.Raters;

            questionObj.KPIDesc = questionObject.KPIDesc;

            if (tempList.Where(c => c.Question == questionObject.Question).Count() > 0) return Conflict();

            // if (tempList != null) { list.Add(tempList); }   

            //Convert comma separated list of raters into an array
            var ratersList = questionObject.RatersString.Split(',');

            if (questionObject.QuestionnairePeriodType == "Monthly")
            {
                questionObject.QuestionnairePeriodValue = int.Parse(questionObject.QuestionnairePeriodValue) < 10 ? (DateTime.Now.Year.ToString() + "0" + questionObject.QuestionnairePeriodValue) : (DateTime.Now.Year.ToString() + questionObject.QuestionnairePeriodValue);
            }
            else if (questionObject.QuestionnairePeriodType == "Quartely")
            {
                questionObject.QuestionnairePeriodValue = DateTime.Now.Year.ToString() + "0" + questionObject.QuestionnairePeriodValue;
            }
            else if (questionObject.QuestionnairePeriodType == "Annually")
            {
                questionObject.QuestionnairePeriodValue = questionObject.QuestionnairePeriodValue;
            }

            foreach (var item in ratersList)
            {
                ratersObjectList.Add(new Rater() { Name = item });
            }

            list.Add(new QuestionnaireObject() { Question = questionObj.Question, KPIDesc = questionObj.KPIDesc, Raters = ratersObjectList });

            //Save to the database
            tempList.Add(new QuestionnaireObject() { Question = questionObj.Question, KPIDesc = questionObj.KPIDesc, Raters = questionObj.Raters, RatersString = questionObject.RatersString });

            System.Web.Helpers.WebCache.Set("QuestionsEntered", tempList);

            if (tempList == null) { System.Web.Helpers.WebCache.Set("QuestionsEntered", list); }

            //var data = JsonConvert.SerializeObject(tempList);

            return Ok(tempList);
        }

        [HttpGet]
        public IHttpActionResult GetTempQuestions()
        {

            var data = System.Web.Helpers.WebCache.Get("QuestionsEntered");

            if (data != null) return Ok(data);

            return Ok();
        }


        //Read questions from a catched list
        [HttpGet]
        public IHttpActionResult IsDuplicateQuestion(string question)
        {
            var data = System.Web.Helpers.WebCache.Get("QuestionsEntered");

            var list = new List<QuestionnaireObject>();

            list = System.Web.Helpers.WebCache.Get("QuestionsEntered");

            if (list == null) return Ok(false);

            var exists = list.Where(c => c.Question == question).Count() > 0;

            return Ok(exists);
        }

        public class DepartmentDropDown
        {
            public string Name { get; set; }

            public string Value { get; set; }
        }

        //Method that allows a manager to verify survey question
        [AccessDeniedAuthorize(Roles = @"FUTUREGROWTH\FG-APP-PAS-MANAGER")]
        [HttpPost]
        public IHttpActionResult VerifySurveyQuestion(SurveyVerify surveyQuestion)
        {
            try
            {
                surveyQuestion.Type = "Annually";

                surveyQuestion.Period = "2016";

                surveyQuestion.User = Generic.GetCurrentLogonUserName();

                surveyQuestion.Rater = new Surveys().getSurveyTeamEmployeeList().Where(c => c.AD_User_Name.ToLower() == surveyQuestion.User.ToLower()).First().Employee;

                var updateStatus = new SurveyUpdate().SurveyManagerVerify(surveyQuestion);

                return Ok();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        //Method that allows an admin to review a survey
        [AccessDeniedAuthorize(Roles = @"FUTUREGROWTH\FG-APP-PAS-ADMIN")]
        [HttpPost]
        public IHttpActionResult ReviewAllQuestions(SurveyComplete survey)
        {
            SurveyEmployee employee = new SurveyEmployee();

            var empSurveyObject = GetSurveyObject(survey.Survey_For);

            empSurveyObject.Survey_For = survey.Survey_For;

            var surveyForList = new Surveys().getSurveyForVerifyAllRaters(empSurveyObject);

            survey.Type = empSurveyObject.Type;

            survey.Period = empSurveyObject.Period;

            foreach (var surveyObject in surveyForList)
            {
                survey.Rater = surveyObject.Rater;

                survey.User = Generic.GetCurrentLogonUserName();

                var saveReviewQuestion = new SurveyUpdate().SurveyHRReviewAll(survey);
            }

            var adminFullName = ConfigurationManager.AppSettings["adminFullName"];

            var manager = new Surveys().getSurveyTeamEmployeeList().Where(c => c.Employee.ToLower() == survey.Survey_For.ToLower()).First().Manager_Name;

            employee.Employee = manager;

            var managerEmailAddress = new Surveys().getSurveyManager(employee).First().EmpEmail; // new ADUsers().GetEmailAddress(manager);          

            var emailBody = GetEmailBodySurveyReview(adminFullName, survey.Survey_For, manager);

            var adUser = new ADUsers();

            //Testing - to be removed later
            //managerEmailAddress = new ADUsers().GetEmailAddress(new Surveys().getSurveyTeamEmployeeList().Where(c => c.AD_User_Name.ToLower() == survey.User.ToLower()).First().Employee);

            EmailSurveyReviewNotification(emailBody, managerEmailAddress);


            return Ok();
        }

        [AccessDeniedAuthorize(Roles = @"FUTUREGROWTH\FG-APP-PAS-MANAGER")]
        [HttpPost]
        public IHttpActionResult ManagerVerifyAllQuestions(SurveyComplete survey)
        {

            var empSurveyObject = GetSurveyObject(survey.Survey_For);

            empSurveyObject.Survey_For = survey.Survey_For;

            var surveyForList = new Surveys().getSurveyForVerifyAllRaters(empSurveyObject);

            survey.Type = empSurveyObject.Type;

            survey.Period = empSurveyObject.Period;

            foreach (var surveyObject in surveyForList)
            {
                try
                {
                    survey.Rater = surveyObject.Rater;

                    survey.User = survey.User = Generic.GetCurrentLogonUserName();

                    var saveReviewQuestion = new SurveyUpdate().SurveyManagerVerifyAll(survey);

                }
                catch (Exception)
                {

                    throw;
                }

            }

            var adminFullName = ConfigurationManager.AppSettings["adminFullName"];

            var adminEmaillAddress = ConfigurationManager.AppSettings["adminEmailAddress"];

            var manager = new Surveys().getSurveyTeamEmployeeList().Where(c => c.Employee.ToLower() == survey.Survey_For.ToLower()).First().Manager_Name;

            var emailBody = GetEmailBodyVerification(adminFullName, survey.Survey_For, manager);

            var adUser = new ADUsers();

            //Testing - to be removed later
            //adminEmaillAddress = new ADUsers().GetEmailAddress(new Surveys().getSurveyTeamEmployeeList().Where(c => c.AD_User_Name.ToLower() == survey.User.ToLower()).First().Employee);

            EmailSurveyVerificationNotification(emailBody, adminEmaillAddress);

            return Ok();

        }

        public static string GetEmailBodyVerification(string admin, string surveyFor, string manager)
        {
            StringBuilder emailBody = new StringBuilder();

            emailBody.Append("Dear " + admin);

            emailBody.Append("\n\n");

            emailBody.Append("This email serves to inform you that " + surveyFor + "'s survey has been verified.\n\n");

            emailBody.Append("Regards,\n");

            emailBody.Append(manager + "\n\n");

            return emailBody.ToString();
        }

        public static string GetEmailBodySurveyReview(string admin, string surveyFor, string manager)
        {
            StringBuilder emailBody = new StringBuilder();

            emailBody.Append("Dear " + manager);

            emailBody.Append("\n\n");

            emailBody.Append("This email serves to inform you that " + surveyFor + "'s survey has been reviewed.\n\n");

            emailBody.Append("Regards,\n");

            emailBody.Append(admin + "\n\n");

            return emailBody.ToString();
        }

        public static void EmailSurveyVerificationNotification(string emailBody, string email)
        {
            var mailingList = new List<string>();

            var messageStatus = new Dictionary<string, string>();

            try
            {
                MailMessage mail = new MailMessage();

                SmtpClient SmtpServer = new SmtpClient("fgnotes.futuregrowth.co.za");

                mail.From = new MailAddress("tdaniels@futuregrowth.co.za");

                mail.To.Add(email);

                mail.Subject = "HR Performance System - Survey Verifification Notification";

                mail.Body = emailBody;

                SmtpServer.Port = 25;

                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static void EmailSurveyReviewNotification(string emailBody, string email)
        {
            var mailingList = new List<string>();

            var messageStatus = new Dictionary<string, string>();

            try
            {
                MailMessage mail = new MailMessage();

                SmtpClient SmtpServer = new SmtpClient("fgnotes.futuregrowth.co.za");

                mail.From = new MailAddress("tdaniels@futuregrowth.co.za");

                mail.To.Add(email);

                mail.Subject = "HR Performance System - Review Survey Notification";

                mail.Body = emailBody;

                SmtpServer.Port = 25;

                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [AccessDeniedAuthorize(Roles = @"FUTUREGROWTH\FG-APP-PAS-MANAGER")]
        [HttpPost]
        public IHttpActionResult ManagerVerifySingleQuestion(SurveyVerify survey)
        {
            var empSurveyObject = GetSurveyObject(survey.Survey_For);

            empSurveyObject.Survey_For = survey.Survey_For;

            survey.Verify = true;

            var surveyForList = new Surveys().getSurveyForVerifyAllRaters(empSurveyObject);

            surveyForList = surveyForList.Where(c => c.Question == survey.Question).ToList();

            survey.Type = empSurveyObject.Type;

            survey.Period = empSurveyObject.Period;

            foreach (var surveyObject in surveyForList)
            {
                survey.Rater = surveyObject.Rater;

                survey.User = Generic.GetCurrentLogonUserName();

                var manager = new Surveys().getSurveyTeamEmployeeList().Where(c => c.AD_User_Name.ToLower() == survey.User.ToLower()).First().Employee;

                survey.Manager = manager;

                var saveReviewQuestion = new SurveyUpdate().SurveyManagerVerify(survey);
            }
            return Ok();
        }

        public SurveyForList GetSurveyObject(string survey_For)
        {

            var surveyObject = new SurveyForList();

            surveyObject.Type = "Annually";

            surveyObject.Period = "2016";

            return surveyObject;
        }

        public SurveyRaterSurvey GetSurveyVerifyObject(string survey_For)
        {

            var surveyObject = new SurveyRaterSurvey();

            surveyObject.Type = "Annually";

            surveyObject.Period = "2016";

            return surveyObject;
        }

        [HttpGet]
        public IHttpActionResult GetSurveyCompletionProgress(string surveyFor)
        {
            SurveyRate surveyRate = new SurveyRate();

            surveyRate.Survey_For = surveyFor;

            surveyRate.Rater = new Surveys().getSurveyTeamEmployeeList().Where(c => c.AD_User_Name.ToLower() == Generic.GetCurrentLogonUserName().ToLower()).First().Employee;

            var progress = new Surveys().SurveyRatingProgress(surveyRate);

            return Ok(progress);
        }

        [HttpGet]
        public IHttpActionResult GetSurveyCompletionStatusReportPerRater(string filter, bool showEmployeesAssigned)
        {
            var surveyPeriodObject = new SurveyPeriod();

            surveyPeriodObject.Type = "Annually";

            surveyPeriodObject.Period = "2016";

            if (!showEmployeesAssigned)
            {
                var surveyCompletionStatusReport = new Surveys().getRaterOuststanding(surveyPeriodObject);

                return Ok(surveyCompletionStatusReport.Where(c => c.No_of_Surveys > 0));
            }
            else
            {
                var surveyCompletionStatusReport = new Surveys().getRaterOuststanding(surveyPeriodObject);

                return Ok(surveyCompletionStatusReport.OrderByDescending(c => c.No_of_Surveys).ToList());
            }
        }

        [HttpGet]
        public IHttpActionResult GetSurveyCompletionStatusReportBySelectedRater(string rater)
        {
            var surveyPeriodObject = new SurveyPeriod();

            surveyPeriodObject.Type = "Annually";

            surveyPeriodObject.Period = "2016";

            var surveyCompletionStatusReport = new Surveys().getRaterOuststanding(surveyPeriodObject);

            return Ok(surveyCompletionStatusReport.Where(c => c.Rater == rater).ToList());

        }

        [HttpGet]
        public IHttpActionResult GetSurveyCompletionStatusReportBySelectedbusinessUnit(string businessUnit)
        {
            var surveyPeriodObject = new SurveyPeriod();

            surveyPeriodObject.Type = "Annually";

            surveyPeriodObject.Period = "2016";

            var surveyCompletionStatusReport = new Surveys().getRaterOuststanding(surveyPeriodObject);

            return Ok(surveyCompletionStatusReport.Where(c => c.Team.ToLower() == businessUnit.ToLower() && c.No_of_Surveys > 0).ToList());

        }
    }
}
