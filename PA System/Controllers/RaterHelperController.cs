using DataDLL.Sources.PASystem.Read;
using DataDLL.Sources.PASystem.Update;
using Newtonsoft.Json;
using PA_System.Helper;
using PA_System.Infrastructure;
using PA_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using static DataDLL.Sources.PASystem.Mviews.SurveyView;

namespace PA_System.Controllers
{
    public class RaterHelperController : ApiController
    {
        //Get list of raters by group name
        [HttpGet]
        public IHttpActionResult GetEmployeeInformation(string adGroupName)
        {
            var employeeObject = new Employee();

            var employeeList = new List<Employee>();

            employeeObject.EmployeeList = !string.IsNullOrWhiteSpace(adGroupName) ? new ADUsers().GetUserPerByAdGroup(adGroupName) : new List<Employee>();

            var ADGroupList = new ADUsers().GetAdGroupsForUser(new ADUsers().ServiceUSer, new ADUsers().DomanName);

            var matchingList = new List<Employee>();

            foreach (var item in employeeObject.EmployeeList)
            {
                if (!ADGroupList.Contains(item.DisplayName))
                {
                    matchingList.Add(item);
                }               
            }
            //Serialize raters' list
            string data = JsonConvert.SerializeObject(matchingList);

            return Ok(data);
        }

        [HttpGet]
        public IHttpActionResult GetEmployeeInformationForNotification(string adGroupName)
        {
            var employeeObject = new Employee();

            var employeeList = new List<Employee>();

            SurveyTeam teamObject = new SurveyTeam();

            teamObject.Team = adGroupName = adGroupName.Replace(" and ", " & ");

            SurveyRaterList raterInfo = new SurveyRaterList();

            var ADUserName = Generic.GetCurrentLogonUserName();

            raterInfo.Type = "Annually";

            raterInfo.Period = "2016";

            raterInfo.Rater = new Surveys().getSurveyTeamEmployeeList().Where(c => c.AD_User_Name.ToLower() == ADUserName.ToLower()).First().Employee;

            var employeeWithSurveys = new Surveys().getSurveysListStatus(raterInfo);

            employeeWithSurveys = employeeWithSurveys.Where(c => c.Team_Desc == adGroupName && c.Verified == true).ToList();
            
            string data = JsonConvert.SerializeObject(employeeWithSurveys);

            return Ok(data);
        }

        [HttpGet]
        public IHttpActionResult GetEmployeeInformationForNotificationByTeam(string adGroupName)
        {
            var employeeObject = new Employee();

            adGroupName = adGroupName.Replace(" and ", " & ");

            var employeeList = new List<Employee>();

            SurveyTeam teamObject = new SurveyTeam();

            teamObject.Team = adGroupName;

            SurveyRaterList raterInfo = new SurveyRaterList();

            var ADUserName = Generic.GetCurrentLogonUserName();

            raterInfo.Type = "Annually";

            raterInfo.Period = "2016";

            raterInfo.Team_Desc = adGroupName;

            // raterInfo.Rater = "Zukiso Diko"; // new Surveys().getSurveyTeamEmployeeList().Where(c => c.AD_User_Name.ToLower() == ADUserName.ToLower()).First().Employee;

            var employeeWithSurveys = new Surveys().getSurveysListByTeam(raterInfo);

            employeeWithSurveys = employeeWithSurveys.Where(c => c.Team_Desc == adGroupName && c.Verified == true && c.Reviewed == true).ToList();

            string data = JsonConvert.SerializeObject(employeeWithSurveys);

            return Ok(data);
        }

        [HttpGet]
        public IHttpActionResult GetSurveyForListByRater(string rater)
        {
            var employeeObject = new Employee();
            
            SurveyRaterList raterInfo = new SurveyRaterList();

            var ADUserName = Generic.GetCurrentLogonUserName();

            raterInfo.Type = "Annually";

            raterInfo.Period = "2016";

            raterInfo.Rater = rater;

            var employeeWithSurveys = new Surveys().getSurveysListStatus(raterInfo);

            employeeWithSurveys = employeeWithSurveys.Where(c => c.Verified == true && c.Reviewed == true).ToList();

            string data = JsonConvert.SerializeObject(employeeWithSurveys);

            return Ok(data);
        }

        //Method that returns all the employees from the database
        [HttpGet]
        public IHttpActionResult GetAllEmlpoyees()
        {
            var employeeList = new Surveys().getSurveyTeamEmployeeList();

            string data = JsonConvert.SerializeObject(employeeList);

            return Ok(data);
        }

        //Get All employees who have surveys
        [HttpGet]
        public IHttpActionResult GetEmployeeInformationBySurvey(string selectedSurveys)
        {

            var Survey = new Surveys();

            var surveyObject = new SurveyForList();

            surveyObject.Type = "Annually";

            surveyObject.Period = "2016";

            var surveyList = new List<SurveyList>();

            var selectedRaterList = selectedSurveys.Split(',');

            for (int i = 0; i < selectedRaterList.Count(); i++)
            {
                surveyObject.Survey_For = selectedRaterList[i];

               var survey = new Surveys().getSurveyForVerifyAllRaters(surveyObject);

                foreach (var item in survey)
                {

                    if (!(surveyList.Where(c => c.Survey_For == item.Rater).Count() > 0))
                    {
                        surveyList.Add(new SurveyList() { Survey_For = item.Rater });
                    }
                }                
            }

            surveyList = surveyList.Distinct().ToList();

            string data = JsonConvert.SerializeObject(surveyList);

            return Ok(data);
        }

        //Get a list of questions assigned to a rater
        //This is used for question approval purposes - to  indicate if the question is appropriate
        [HttpGet]
        public IHttpActionResult GetEmployeeQuestionsByRater(string rater)
        {
            var empSurveyObject = GetSurveyObject();

            var Survey = new Surveys();

            var surveyObject = new SurveyRaterSurvey();

            surveyObject.Type = "Annually";

            surveyObject.Period = "2016";

            surveyObject.Rater = new Surveys().getSurveyTeamEmployeeList().Where(c => c.AD_User_Name.ToLower() == Generic.GetCurrentLogonUserName().ToLower()).First().Employee;

            surveyObject.Ratee = rater;

            var raterListString = string.Empty;

            var surveyForList = new Surveys().getSurveyForVerifyAllRaters(empSurveyObject);

            foreach (var surveyObj in surveyForList)
            {
                raterListString += surveyObj.Rater + ";";
            }

            var list = new Surveys().getSurveyFor(surveyObject);

            list[0].Rater = raterListString;

            return Ok(list.Distinct().ToList());      
        }

        public SurveyForList GetSurveyObject()
        {

            var surveyObject = new SurveyForList();

            surveyObject.Type = "Annually";

            surveyObject.Period = "2016";

            return surveyObject;
        }

        [HttpPost]
        public IHttpActionResult UpdateSurveyRaters(SurveyQuestionRaterList surveyQuestionRaterList)
        {
            try
            {
                surveyQuestionRaterList.Type = "Annually";

                surveyQuestionRaterList.Period = "2016";

                surveyQuestionRaterList.AD_User = Generic.GetCurrentLogonUserName();

                if (!string.IsNullOrWhiteSpace(surveyQuestionRaterList.Question) && !string.IsNullOrWhiteSpace(surveyQuestionRaterList.Rater_List) && !string.IsNullOrWhiteSpace(surveyQuestionRaterList.Survey_For))
                {
                    var updateStatus = new SurveyUpdate().SurveyUpdateRaters(surveyQuestionRaterList);

                    return Ok(updateStatus);
                }            

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return Ok();
        }
    }
}
