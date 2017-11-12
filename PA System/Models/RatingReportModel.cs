using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static DataDLL.Sources.PASystem.Mviews.SurveyView;

namespace PA_System.Models
{
    public class RatingReportModel
    {
        public bool ? IncludeRaters { get; set; }

        public string Survey_For { get; set; }

        public List<SurveyForReport> ReportList = new List<SurveyForReport>();

        public string SelectedName { get; set; }
    }
}