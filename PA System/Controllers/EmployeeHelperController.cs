using DataDLL.Sources.PASystem.Read;
using MvcTesting;
using Newtonsoft.Json;
using PA_System.Helper;
using PA_System.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static DataDLL.Sources.PASystem.Mviews.SurveyView;

namespace PA_System.Controllers
{
    public class EmployeeHelperController : ApiController
    {
        //Return a list of employees by group name
        [HttpGet]
        public IHttpActionResult GetEmployeeInformation(string id)
        {
            var employeeList = new List<Employee>();

            var list = !string.IsNullOrWhiteSpace(id) ? new ADUsers().GetUserPerByAdGroup(id) : new List<Employee>();

            try
            {
                string data = JsonConvert.SerializeObject(list);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        //Return all fg employees
        public List<SurveyTeamList> GetEmployeeListAll()
        {
            return new Surveys().getSurveyTeamEmployeeList();
        }

        //Return all fg employees per team
        [HttpGet]
        public List<SurveyTeamList> GetSurveyTeamList(SurveyTeam team)
        {
            return new Surveys().getSurveyTeamList(team);
        }

        [HttpGet]
        public IHttpActionResult GetEmployeesWithSurvey()
        {

            SurveyRaterList raterInfo = new SurveyRaterList();

            var ADUserName = Generic.GetCurrentLogonUserName();

            raterInfo.Type = "Annually";

            raterInfo.Period = "2016";

            raterInfo.Rater = new Surveys().getSurveyTeamEmployeeList().Where(c => c.AD_User_Name.ToLower() == ADUserName.ToLower()).First().Employee;

            var surveyList = new Surveys().getSurveysListStatus(raterInfo).Where(c => c.Verified == true && c.Reviewed == true).ToList();

            // var employeeData = new Surveys().getSurveysListStatus(raterInfo);

            //To calculate overall rating, first get a survey for each employee
            //var surveyObject = new SurveyRaterSurvey();

            //surveyObject.Type = raterInfo.Type;

            //surveyObject.Period = raterInfo.Period;

            //surveyObject.Rater = raterInfo.Rater;

            //foreach (var survey in surveyList)
            //{
            //    surveyObject.Ratee = survey.Survey_For;

            //    var employeeSurvey = new Surveys().getSurveyFor(surveyObject);

            //    var noOfCompletedQuestions = Convert.ToInt32(employeeSurvey.Where(c => c.Rating > 0).Count());

            //    var noOfQuestionsPerSurvey = Convert.ToInt32(employeeSurvey.Count());

            //    var total = noOfQuestionsPerSurvey > 0 ?  GetOverallRating(noOfCompletedQuestions, noOfQuestionsPerSurvey) : 0;

            //   // employeeData[counter].Rating = total;

            //    counter++;
            //}

            return Ok(surveyList);
        }

        public static string GetEmailAddress(string employeeName)
        {

            var list = !string.IsNullOrWhiteSpace("DG-Futuregrowth") ? new ADUsers().GetUserPerByAdGroup("DG-Futuregrowth") : new List<PA_System.Infrastructure.Employee>();

            return list.Where(c => c.DisplayName == employeeName).First().EmailAddress;
        }

        [HttpGet]
        public IHttpActionResult CompletionStatusByRaterCompleted(string rater, string status)
        {

            //SurveyForList raterInfo = new SurveyForList();

            //var ADUserName = Generic.GetCurrentLogonUserName();

            //raterInfo.Type = "Annually";

            //raterInfo.Period = "2016";

            //var surveyList = new Surveys().getSurveyForVerifyAllRaters(raterInfo);

            //return Ok(surveyList);

            ////SurveyRaterList raterInfo = new SurveyRaterList();

            ////var ADUserName = Generic.GetCurrentLogonUserName();

            ////raterInfo.Type = "Annually";

            ////raterInfo.Period = "2016";

            ////raterInfo.Rater = new Surveys().getSurveyTeamEmployeeList().Where(c => c.AD_User_Name.ToLower() == ADUserName.ToLower()).First().Employee;

            ////var surveyList = new Surveys().getSurveysListStatus(raterInfo);

            ////return Ok(surveyList);

            //var Survey = new Surveys();

            //var surveyObject = new SurveyForList();

            //surveyObject.Type = "Annually";

            //surveyObject.Period = "2016";

            //SurveyRaterList surveyObject2 = new SurveyRaterList();

            //surveyObject2.Period = surveyObject.Period;

            //surveyObject2.Type = surveyObject.Type;

            //surveyObject2.Rater = rater;


            //var f = new Surveys().getRaterSurveys(surveyObject2);

            //return Ok(f);           

            SurveyRaterList raterInfo = new SurveyRaterList();

            var ADUserName = Generic.GetCurrentLogonUserName();

            raterInfo.Type = "Annually";

            raterInfo.Period = "2016";

            raterInfo.Rater = rater;

            if (status == "Reviewed")
            {
                var surveyList = new Surveys().getSurveysListStatus(raterInfo).Where(c => c.Reviewed == true && c.Verified == true && c.Survey_For != rater).ToList();

                return Ok(surveyList);
            }
            else if (status == "Completed")
            {
                var surveyList = new Surveys().getSurveysListStatus(raterInfo).Where(c => c.Completed == true && c.Verified == true && c.Reviewed == true && c.Survey_For != rater).ToList();

                return Ok(surveyList);
            }
            else if (status == "Incomplete")
            {
                var surveyList = new Surveys().getSurveysListStatus(raterInfo).Where(c => c.Completed == false && c.Reviewed == true && c.Verified == true && c.Survey_For != rater).ToList();

                return Ok(surveyList);
            }

            return Ok();
        }




        [HttpGet]
        public IHttpActionResult GetSurveyCompletionStatusList()
        {
            SurveyPeriod surveyPeriodObject = new SurveyPeriod();

            surveyPeriodObject.Type = "Annually";

            surveyPeriodObject.Period = "2016";

            try
            {
                var surveyList = new Surveys().getSurveyCompletionRaters(surveyPeriodObject);

                return Ok(surveyList);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        //Method to return all the employees who have surveys with the status of each survey as well as raters for each question
        [AccessDeniedAuthorize(Roles = @"FUTUREGROWTH\FG-APP-PAS-ADMIN")]
        [HttpGet]
        public IHttpActionResult GetAllEmployeeStatusSurveyReport()
        {

            SurveyPeriod raterSurveyObject = new SurveyPeriod();

            raterSurveyObject.Type = "Annually";

            raterSurveyObject.Period = "2016";

            var surveyList = new Surveys().getSurveyCompletionStatusPeriod(raterSurveyObject);

            return Ok(surveyList);
        }

        //Method to return the employees who have surveys (by Business Unit) with the status of each survey as well as raters for each question
        [AccessDeniedAuthorize(Roles = @"FUTUREGROWTH\FG-APP-PAS-Admin")]
        [HttpGet]
        public IHttpActionResult GetAllEmployeeStatusSurveyReportPerBusinessUnit(string businessUnit)
        {

            SurveyPeriod survePeriodObject = new SurveyPeriod();

            SurveyTeam teamObject = new SurveyTeam();

            teamObject.Team = businessUnit;

            survePeriodObject.Type = "Annually";

            survePeriodObject.Period = "2016";

            var surveyList = new Surveys().getSurveyCompletionStatusBU(survePeriodObject, teamObject);

            return Ok(surveyList);
        }

        [AccessDeniedAuthorize(Roles = @"FUTUREGROWTH\FG-APP-PAS-ADMIN")]
        [HttpGet]
        public IHttpActionResult GetAllEmployeeStatusSurveyReportPerRater(string rater)
        {

            SurveyRaterList survePeriodObject = new SurveyRaterList();

            survePeriodObject.Type = "Annually";

            survePeriodObject.Period = "2016";

            survePeriodObject.Rater = rater;

            var surveyList = new Surveys().getSurveyCompletionStatusRater(survePeriodObject);

            return Ok(surveyList);
        }



        [HttpGet]
        public IHttpActionResult GetEmployeesWithSurveyPerTeam(string team)
        {
            SurveyRaterList raterInfo = new SurveyRaterList();

            var ADUserName = Generic.GetCurrentLogonUserName();

            raterInfo.Type = "Annually";

            raterInfo.Period = "2016";

            raterInfo.Rater = new Surveys().getSurveyTeamEmployeeList().Where(c => c.AD_User_Name.ToLower() == ADUserName.ToLower()).First().Employee;

            var surveyList = new Surveys().getSurveysListStatus(raterInfo);

            surveyList = surveyList.Where(c => c.Team_Desc == team).Distinct().ToList();

            return Ok(surveyList);
        }


        public static decimal GetOverallRating(int noOfQuestions, int totalQuestions)
        {
            decimal total = (Convert.ToDecimal(noOfQuestions) / Convert.ToDecimal(totalQuestions)) * 100;

            return total;
        }


        [HttpGet]
        public IHttpActionResult GetEmployeesWithSurveyForReview()
        {
            SurveyManagerList managerInfoObject = new SurveyManagerList();

            var ADUserName = Generic.GetCurrentLogonUserName();

            managerInfoObject.Type = "Annually";

            managerInfoObject.Period = "2016";

            managerInfoObject.Manager = new Surveys().getSurveyTeamEmployeeList().Where(c => c.AD_User_Name.ToLower() == ADUserName.ToLower()).First().Employee;

            try
            {
                var v = new Surveys().getManagerSurveysListStatus(managerInfoObject);

                return Ok(new Surveys().getManagerSurveysListStatus(managerInfoObject));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }

        [HttpGet]
        public IHttpActionResult GetEmployeesWithSurveyToVerifiy()
        {

            SurveyManagerList managerInfoObject = new SurveyManagerList();

            var ADUserName = Generic.GetCurrentLogonUserName();

            managerInfoObject.Type = "Annually";

            managerInfoObject.Period = "2016";

            managerInfoObject.Manager = new Surveys().getSurveyTeamEmployeeList().Where(c => c.AD_User_Name.ToLower() == ADUserName.ToLower()).First().Employee;

            try
            {
                var list = new Surveys().getManagerSurveysListStatus(managerInfoObject);

                var employeeSurveyList = new List<SurveyManagerStatus>();

                foreach (var item in list)
                {
                    if (!(employeeSurveyList.Where(c => c.Survey_For == item.Survey_For).Count() > 0))
                    {
                        employeeSurveyList.Add(item);
                    }
                }

                return Ok(employeeSurveyList);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }

        //Get all employees with surveys
        [HttpGet]
        public IHttpActionResult GetEmployeesWithSurveyAll()
        {

            SurveyPeriod surveyPeriodInfoObject = new SurveyPeriod();

            surveyPeriodInfoObject.Type = "Annually";

            surveyPeriodInfoObject.Period = "2016";

            try
            {
                var empList = new Surveys().getAllSurveysListStatus(surveyPeriodInfoObject).Distinct().ToList();

                var employeeSurveyList = new List<SurveyStatus>();

                foreach (var employeeInfo in empList)
                {
                    if (!(employeeSurveyList.Where(c => c.Survey_For == employeeInfo.Survey_For).Count() > 0))
                    {
                        employeeSurveyList.Add(employeeInfo);
                    }
                }

                return Ok(employeeSurveyList);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        ////Get all employees with surveys
        //[HttpGet]
        //public IHttpActionResult GetEmployeesWithSurveyAll(string status)
        //{

        //    SurveyPeriod surveyPeriodInfoObject = new SurveyPeriod();

        //    surveyPeriodInfoObject.Type = "Annually";

        //    surveyPeriodInfoObject.Period = "2016";

        //    try
        //    {
        //        if (status == "All")
        //        {
        //            var empList = new Surveys().getAllSurveysListStatus(surveyPeriodInfoObject).Distinct().ToList();

        //            var employeeSurveyList = new List<SurveyStatus>();

        //            foreach (var employeeInfo in empList)
        //            {
        //                if (!(employeeSurveyList.Where(c => c.Survey_For == employeeInfo.Survey_For).Count() > 0))
        //                {
        //                    employeeSurveyList.Add(employeeInfo);
        //                }
        //            }

        //            return Ok(employeeSurveyList);
        //        }

        //        else if (status == "Completed")
        //        {

        //            var empList = new Surveys().getAllSurveysListStatus(surveyPeriodInfoObject).Where(c => c.Completed == true).Distinct().ToList();

        //            var employeeSurveyList = new List<SurveyStatus>();

        //            foreach (var employeeInfo in empList)
        //            {
        //                if (!(employeeSurveyList.Where(c => c.Survey_For == employeeInfo.Survey_For).Count() > 0))
        //                {
        //                    employeeSurveyList.Add(employeeInfo);
        //                }
        //            }

        //            return Ok(employeeSurveyList);

        //        }

        //        else if (status == "Incomplete")
        //        {

        //            var empList = new Surveys().getAllSurveysListStatus(surveyPeriodInfoObject).Where(c => c.Completed == false).Distinct().ToList();

        //            var employeeSurveyList = new List<SurveyStatus>();

        //            foreach (var employeeInfo in empList)
        //            {
        //                if (!(employeeSurveyList.Where(c => c.Survey_For == employeeInfo.Survey_For).Count() > 0))
        //                {
        //                    employeeSurveyList.Add(employeeInfo);
        //                }
        //            }

        //            return Ok(employeeSurveyList);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }

        //    return Ok();
        //}

        [HttpGet]
        public IHttpActionResult GetEmployeesWithSurveyAllPerRater(string id)
        {

            SurveyPeriod surveyPeriodInfoObject = new SurveyPeriod();

            surveyPeriodInfoObject.Type = "Annually";

            surveyPeriodInfoObject.Period = "2016";

            try
            {
                var empList = new Surveys().getAllSurveysListStatus(surveyPeriodInfoObject).Distinct().ToList();

                empList = empList.Where(c => c.Survey_For == id).Distinct().ToList();

                var employeeSurveyList = new List<SurveyStatus>();

                foreach (var employeeInfo in empList)
                {
                    if (!(employeeSurveyList.Where(c => c.Survey_For == employeeInfo.Survey_For).Count() > 0))
                    {
                        employeeSurveyList.Add(employeeInfo);
                    }
                }

                return Ok(employeeSurveyList);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Get all employees with surveys per department
        [HttpGet]
        public IHttpActionResult GetEmployeesWithSurveyAllPerBusinessUnit(string businessUnit)
        {

            SurveyPeriod surveyPeriodInfoObject = new SurveyPeriod();

            surveyPeriodInfoObject.Type = "Annually";

            surveyPeriodInfoObject.Period = "2016";

            try
            {
                var empList = new Surveys().getAllSurveysListStatus(surveyPeriodInfoObject).Distinct().ToList();

                empList = empList.Where(c => c.Team_Desc == businessUnit.Replace("and", "&")).Distinct().ToList();

                var employeeSurveyList = new List<SurveyStatus>();

                foreach (var employeeInfo in empList)
                {
                    if (!(employeeSurveyList.Where(c => c.Survey_For == employeeInfo.Survey_For).Count() > 0))
                    {
                        employeeSurveyList.Add(employeeInfo);
                    }
                }

                return Ok(employeeSurveyList);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetEmployeesListToFilterReport()
        {

            SurveyPeriod surveyPeriodInfoObject = new SurveyPeriod();

            surveyPeriodInfoObject.Type = "Annually";

            surveyPeriodInfoObject.Period = "2016";

            try
            {
                var empList = new Surveys().getAllSurveysListStatus(surveyPeriodInfoObject).Distinct().ToList();

                var employeeSurveyList = new List<SurveyStatus>();

                foreach (var employeeInfo in empList)
                {
                    if (!(employeeSurveyList.Where(c => c.Survey_For == employeeInfo.Survey_For).Count() > 0))
                    {
                        employeeSurveyList.Add(employeeInfo);
                    }
                }

                return Ok(employeeSurveyList.Where(c => c.Completed == true));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetSurveysAssignedPerRater(string rater)
        {
            SurveyForList survey = new SurveyForList();

            survey.Type = "Annually";

            survey.Period = "2016";

            try
            {
                var empList = new Surveys().getSurveyForListRaters((survey));

                var employeeSurveyList = new List<SurveyStatus>();

                return Ok(employeeSurveyList);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        //[HttpGet]
        //public IHttpActionResult GetEmployeesWithSurveyAll()
        //{
        //    SurveyRaterList raterInfo = new SurveyRaterList();

        //    var ADUserName = Generic.GetCurrentLogonUserName();

        //    SurveyPeriod period = new SurveyPeriod();

        //    period.Type = "Annually";

        //    period.Period = "2016";

        //    //raterInfo.Rater = new Surveys().getSurveyTeamEmployeeList().Where(c => c.AD_User_Name.ToLower() == ADUserName.ToLower()).First().Employee;

        //    var surveyList = new Surveys().getSurveysListStatus();

        //    return Ok(new Surveys().getSurveysListStatus();
        //}



    }
}
