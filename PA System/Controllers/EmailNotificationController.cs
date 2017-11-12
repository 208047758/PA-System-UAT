using DataDLL.Sources.PASystem.Read;
using MvcTesting;
using Newtonsoft.Json;
using PA_System.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PA_System.Controllers
{
    public class EmailNotificationController : Controller
    {
        // GET: EmailNotifications
        public ActionResult Index()
        {
            return View();
        }

        //[AccessDeniedAuthorize(Roles = @"FUTUREGROWTH\FG-APP-PAS-ADMIN")]
        public ActionResult NotifyRaters()
        {
            ViewBag.FGTeams = new Surveys().getSurveyTeams();

            return View();
        }
    }
}