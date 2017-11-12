using DataDLL.Sources.PASystem.Read;
using MvcTesting;
using Newtonsoft.Json;
using PA_System.Infrastructure;
using PA_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PA_System.Controllers
{
    public class RaterController : Controller
    {
        // GET: Rater
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult ViewAssignedQuestions(string question)
        {
            return View();
        }

        public ActionResult ProvideFeedback()
        {
            return View();
        }

        public ActionResult ViewReviewers()
        {
            return View();
        }

        public ActionResult ViewSurveyFeedBack()
        {
            return View();
        }

        //Returns a list of all the employees from AD to populate raters' dropdown list when updating raters
        public ActionResult UpdateSurveyRater()
        {
           
            ViewBag.FGTeamList = new Surveys().getSurveyTeams();

            PopulateUserInformation();

            return View();
        }


        //Returns a list of all the employees from AD
       // [AccessDeniedAuthorize(Roles = @"FUTUREGROWTH\FG-APP-PAS-ADMIN")]
        [HttpGet]
        public ActionResult UpdateSurveyRater(string adGroupName)
        {
            PopulateUserInformation(adGroupName);

            return View();
        }

        //Get a list of all the employees that have access to a provided group name
        public ActionResult PopulateUserInformation(string adGroupName)
        {
            var employeeObject = new Employee();

            var employeeList = new List<Employee>();

            var groupName = !string.IsNullOrWhiteSpace(Request["Department"]) ? Request["Department"] : adGroupName;

            employeeObject.EmployeeList = !string.IsNullOrWhiteSpace(groupName) ? new ADUsers().GetUserPerByAdGroup(groupName) : new List<Employee>();

            ViewBag.EmployeeList = employeeObject.EmployeeList;

            return View("UpdateSurveyRater");
        }

        public ActionResult PopulateUserInformation()
        {
            ViewBag.EmployeeList = new Surveys().getSurveyTeamEmployeeList();

            return View("UpdateSurveyRater");
        }

        //[HttpPost]
        //public ActionResult PopulateUsers(string adGroupName)
        //{
        //    var employeeObject = new Employee();

        //    var employeeList = new List<Employee>();

        //    var groupName = !string.IsNullOrWhiteSpace(Request["Department"]) ? Request["Department"] : adGroupName;

        //    employeeObject.EmployeeList = !string.IsNullOrWhiteSpace(groupName) ? new ADUsers().GetUserPerByAdGroup(groupName) : new List<Employee>();

        //    ViewBag.EmployeeList = employeeObject.EmployeeList;

        //    //var data = JsonConvert.SerializeObject(employeeObject.EmployeeList);

        //   // RedirectToAction("UpdateSurveyRater", "Rater");

        //    return View("UpdateSurveyRater");

        //}

        //Returns a list of all the employees from AD
        [HttpPost]
        public JsonResult PopulateUsers(string adGroupName)
        {
            var employeeObject = new Employee();

            var employeeList = new List<Employee>();

            var groupName = !string.IsNullOrWhiteSpace(Request["Department"]) ? Request["Department"] : adGroupName;

            employeeObject.EmployeeList = !string.IsNullOrWhiteSpace(groupName) ? new ADUsers().GetUserPerByAdGroup(groupName) : new List<Employee>();

            ViewBag.EmployeeList = employeeObject.EmployeeList;

            var data = JsonConvert.SerializeObject(employeeObject.EmployeeList);

            return Json(data);

        }

    }


}