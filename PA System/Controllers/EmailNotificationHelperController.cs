using DataDLL.Sources.PASystem.Read;
using PA_System.Helper;
using PA_System.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web.Http;
using static DataDLL.Sources.ActiveDirectory;
using static DataDLL.Sources.PASystem.Mviews.SurveyView;

namespace PA_System.Controllers
{
    public class EmailNotificationHelperController : ApiController
    {
        string message = string.Empty;
        int counter = 0;
        //Sending email notification by survey
        [HttpGet]
        public IHttpActionResult NotifyRaters(string selectedRaters, string surveyFor)
        {
            var selectedRaterList = selectedRaters.Split(',');



            try
            {
                var user = Generic.GetCurrentLogonUserName();

                foreach (var employeeObject in selectedRaterList)
                {
                    if (!string.IsNullOrWhiteSpace(employeeObject))
                    {
                        StringBuilder emailBody = new StringBuilder();

                        var redirectlink = ConfigurationManager.AppSettings["CompleteSurveyLink"];
                        
                        var adminFullName = ConfigurationManager.AppSettings["adminFullName"];

                        SurveyEmployee employee = new SurveyEmployee();

                        employee.Employee = employeeObject.ToLower();

                        var email = new Surveys().getSurveyManager(employee);

                        var emailAddress = !string.IsNullOrWhiteSpace(employeeObject) ? email.First().EmpEmail : "";

                        if (!string.IsNullOrWhiteSpace(emailAddress))
                        {
                            emailBody.Append("Dear " + employeeObject);

                            emailBody.Append("\n\n");

                            emailBody.Append("You have been asked to complete a 360 degree performance survey for " + surveyFor + "\n\n");

                            emailBody.Append("Follow the link below to complete the questionnaire.\n\n");

                            emailBody.Append("Link to questionnaire\t " + redirectlink + "\n\n");

                            emailBody.Append("If you have any questions please come and chat to me. \n\n");

                            emailBody.Append("The deadline for completion of the 360-degree review process is Monday 16 January 2017. [Please note: The system will be closed after this date in order to run the reports.] \n\n");

                            emailBody.Append("Regards,\n");

                            //var employeeFulleName = new Surveys().getSurveyTeamEmployeeList().Where(c => c.AD_User_Name.ToLower() == user.ToLower()).First().Employee;

                            emailBody.Append(adminFullName + "\n\n");

                            EmailSurveyEmail(emailBody.ToString(), emailAddress);

                            counter++;
                        }

                    }

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }



            return Ok();
        }

        //Sending email notification by by rater
        //This method sends an email notification to raters to inform them that they are assigned as raters
        [HttpGet]
        public IHttpActionResult NotifyRatersByRater(string rater, string selectedSurveyFor)
        {
            var selectedSurveyForList = selectedSurveyFor.Split(',');

            var user = Generic.GetCurrentLogonUserName();

            foreach (var empObject in selectedSurveyForList)
            {
                try
                {
                    StringBuilder emailBody = new StringBuilder();

                    var redirectlink = ConfigurationManager.AppSettings["CompleteSurveyLink"];

                    var adminFullName = ConfigurationManager.AppSettings["adminFullName"];

                    SurveyEmployee employee = new SurveyEmployee();

                    employee.Employee = rater.ToLower();

                    var email = new Surveys().getSurveyManager(employee);

                    var emailAddress = !string.IsNullOrWhiteSpace(employee.Employee) ? email.First().EmpEmail : "";

                    //var emailAddress = GetEmailAddress(rater);

                    emailBody.Append("Dear " + rater);

                    emailBody.Append("\n\n");

                    emailBody.Append("You have been asked to complete a 360 degree performance survey for " + empObject + "\n\n");

                    emailBody.Append("Follow the link below to complete the questionnaire.\n\n");

                    emailBody.Append("Link to questionnaire\t " + redirectlink + "\n\n");

                    emailBody.Append("If you have any questions please come and chat to me. \n\n");

                    emailBody.Append("The deadline for completion of the 360-degree review process is Monday 16 January 2017. [Please note: The system will be closed after this date in order to run the reports.] \n\n");

                    emailBody.Append("Regards,\n");

                    //var employeeFulleName = new Surveys().getSurveyTeamEmployeeList().Where(c => c.AD_User_Name.ToLower() == user.ToLower()).First().Employee;

                    emailBody.Append(adminFullName + "\n\n");
                    try
                    {
                        EmailSurveyEmail(emailBody.ToString(), emailAddress);
                    }
                    catch (Exception ex)
                    {

                        throw new Exception(ex.Message);
                    }


                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }

            }

            return Ok();
        }

        //This method sends an email notification to raters to inform them that they are assigned as raters
        public static void EmailSurveyEmail(string emailBody, string email)
        {
            var mailingList = new List<string>();

            var messageStatus = new Dictionary<string, string>();

            try
            {

                MailMessage mail = new MailMessage();

                SmtpClient SmtpServer = new SmtpClient("fgnotes.futuregrowth.co.za");

                mail.From = new MailAddress("tdaniels@futuregrowth.co.za");

                mail.To.Add(email);

                mail.Subject = "HR Performance System - Complete Survey Request Notification";

                mail.Body = emailBody;

                SmtpServer.Port = 25;

                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static string GetEmailAddress(string employeeName)
        {
            return new ADUsers().GetEmailAddress(employeeName);
            //var list = !string.IsNullOrWhiteSpace("DG-Futuregrowth") ? new ADUsers().GetUserPerByAdGroup("DG-Futuregrowth") : new List<PA_System.Infrastructure.Employee>();

            //return list.Where(c => c.DisplayName == employeeName).First().EmailAddress;
        }
    }
}
