using PA_System.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PA_System.Models
{
    public class Survey
    {
        public Survey()
        {
            Questions = new List<Questions>();

            KPIs = new List<KPI>();

            RaterList = new List<Rater>();
        }
        public int SurveyID { get; set; }

        public string Question { get; set; }

        public List<Survey> SurveyList { get; set; }

        public List<Rater> RaterList { get; set; }

        public int SurveyMonth { get; set; }

        public int Quarter { get; set; }

        public List<Questions> Questions { get; set; }

        public string KPIDesc { get; set; }

        public List<KPI> KPIs { get; set; }

        public string RatersString { get; set; }

        public string Status { get; set; }
    }

    public class Questions
    {
        public string Description { get; set; }

        public string Value { get; set; }
    }

    public class KPI
    {
        public string Description { get; set; }

        public string Value { get; set; }
    }

    public class QuestionnaireObject
    {
        public QuestionnaireObject()
        {
            Raters = new List<Rater>();
        }
        public string Question { get; set; }

        public List<Rater> Raters { get; set; }

        public string QuestionnairePeriodType { get; set; }

        public string QuestionnairePeriodValue { get; set; }

        public string RateeName { get; set; }

        public string UploadStatus { get; set; }

        public string KPIDesc { get; set; }

        public string RatersString { get; set; }
    }

}