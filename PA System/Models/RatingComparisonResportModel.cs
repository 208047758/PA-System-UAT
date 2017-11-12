using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PA_System.Models
{
    public class RatingComparisonResportModel
    {
        public List<RatingComparisonResportModel> ComparisonReport { get; set; }
        public int QuestionID { get; set; }
        public string Question { get; set; }

        public List<DisplayData> SurveryForList { get; set; }

        public List<DisplayData> Ratings { get; set; }

        public List<DisplayData> Comments { get; set; }        

        public class DisplayData
        {
            public string Text { get; set; }

            public string Value { get; set; }

            public string RatingComment { get; set; }
        }

    }
}