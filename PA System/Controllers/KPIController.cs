using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PA_System.Controllers
{
    public class KPIController : Controller
    {
        // GET: KPI
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult UploadKPI(HttpPostedFileBase fileUploadKPI)
        {
            return View();
        }

        public ActionResult UpdateKPI()
        {
            return View();
        }
    }
}