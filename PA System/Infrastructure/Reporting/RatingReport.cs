using DataDLL.Sources.PASystem.Read;
using PA_System.Helper;
using PA_System.Infrastructure.Reporting.Objects;
using PA_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using static DataDLL.Sources.PASystem.Mviews.SurveyView;
using static PA_System.Models.RatingComparisonResportModel;

namespace PA_System.Infrastructure.Reporting
{
    public class RatingReport
    {
        public DataTable GetColumns(DataTable dt, List<SurveyForReport> report)
        {

            for (int i = 0; i < (report.Count * 2) + 5; i++)
            {
                dt.Columns.Add("");
            }

            return dt;
        }

        public DataTable AddHeadings(DataTable dt, string surveyFor, string period)
        {
            List<string> itemRow = new List<string>();


            itemRow.Add("\t\t\t\t\t\t360 DEGREE PERFORMANCE REVIEW REPORT:  " + surveyFor + "  FOR YEAR " + period);
            dt.Rows.Add(itemRow.ToArray());

            itemRow = new List<string>();

            itemRow.Add("\t\t\t\t\t\tRATING SUMMARY (PER RATER)");

            dt.Rows.Add(itemRow.ToArray());
            return dt;
        }

        public DataTable AddQuestionHeadings(DataTable dt, List<SurveyForReport> report)
        {
            List<string> itemRow = new List<string>();

            itemRow.Add("DATA MEASURE");

            itemRow.Add("");

            var counter = 1;

            foreach (var reportRow in report.Distinct())
            {
                if (!itemRow.Contains(reportRow.Question))
                {
                    itemRow.Add(reportRow.Question);

                    if (!(counter == report.Count)) { itemRow.Add(""); }

                    else { itemRow.Add("Overall Comment"); }

                    counter++;
                }
            }

            dt.Rows.Add(itemRow.ToArray());

            return dt;
        }

        public DataTable AddQuestionAverallMinRating(DataTable dt, List<SurveyForReport> report)
        {
            List<string> itemRow = new List<string>();

            itemRow.Add("MIN");

            itemRow.Add("");

            var addedQuestions = new List<string>();

            var numberOfRows = report.Select(c => c.Question).AsQueryable().Distinct().ToList();

            foreach (var reportRow in report)
            {
                if (!addedQuestions.Contains(reportRow.Question))
                {
                    var minRating = report.Where(c => c.Question == reportRow.Question).ToList();

                    itemRow.Add(reportRow.Min_Rating.ToString());

                    itemRow.Add("");

                    addedQuestions.Add(reportRow.Question);
                }


            }

            dt.Rows.Add(itemRow.ToArray());
            return dt;
        }

        public DataTable AddQuestionAverallMaxRating(DataTable dt, List<SurveyForReport> report)
        {
            List<string> itemRow = new List<string>();

            itemRow.Add("MAX");

            itemRow.Add("");

            var addedQuestions = new List<string>();

            var numberOfRows = report.Select(c => c.Question).AsQueryable().Distinct().ToList();

            foreach (var reportRow in report)
            {
                if (!addedQuestions.Contains(reportRow.Question))
                {
                    var maxRating = report.Where(c => c.Question == reportRow.Question).ToList();

                    itemRow.Add(reportRow.Max_Rating.ToString());

                    itemRow.Add("");

                    addedQuestions.Add(reportRow.Question);
                }

            }

            dt.Rows.Add(itemRow.ToArray());
            return dt;
        }

        public DataTable AddQuestionAverageRating(DataTable dt, List<SurveyForReport> report)
        {
            List<string> itemRow = new List<string>();

            itemRow.Add("AVG");

            itemRow.Add("");

            var addedQuestions = new List<string>();

            var numberOfRows = report.Select(c => c.Question).AsQueryable().Distinct().ToList();

            foreach (var reportRow in report)
            {
                if (!addedQuestions.Contains(reportRow.Question))
                {
                    var maxRating = report.Where(c => c.Question == reportRow.Question).ToList();

                    itemRow.Add(reportRow.Avg_Rating.ToString());

                    itemRow.Add("");

                    addedQuestions.Add(reportRow.Question);
                }

            }

            dt.Rows.Add(itemRow.ToArray());
            return dt;
        }

        public DataTable AddIndividualRaterComment(DataTable dt, List<SurveyForReport> report, bool showRaters, string rater, List<SurveyForReport> reportMaster)
        {
            List<string> itemRow = new List<string>();

            itemRow.Add("");

            var questionList = reportMaster.Select(c => c.Question).AsQueryable().Distinct().ToList();

            var raterQuestionList = report.Where(c => c.Rater == rater).Select(c => c.Question).AsQueryable().Distinct().ToList();

            var questionToExclude = new List<string>();

            foreach (var item in questionList)
            {
                if (!raterQuestionList.Contains(item))
                {
                    questionToExclude.Add(item.ToString());
                }
            }

            var skippedQuestionsList = new List<string>();

            foreach (var reportRow in reportMaster)
            {
                if (questionToExclude.Contains(reportRow.Question))
                {
                    if (!skippedQuestionsList.Contains(reportRow.Question))
                    {
                        itemRow.Add("");

                        itemRow.Add("");
                    }                     

                    skippedQuestionsList.Add(reportRow.Question);
                }
                else
                {
                    if (reportRow.Rater == rater)
                    {
                        if (showRaters)
                        {
                            itemRow.Add(reportRow.Rater);
                        }
                        else
                        {
                            itemRow.Add("");
                        }

                        itemRow.Add(reportRow.Rater_Comment);
                    }
                }           

            }

            dt.Rows.Add(itemRow.ToArray());

            return dt;
        }


        public DataTable AddIndividualRating(DataTable dt, List<SurveyForReport> report, bool includeRaters, string rater, List<SurveyForReport> reportMaste)
        {
            List<string> itemRow = new List<string>();

            itemRow.Add("");

            var questionList = reportMaste.Select(c => c.Question).AsQueryable().Distinct().ToList();

            var raterQuestionList = report.Where(c => c.Rater == rater).Select(c => c.Question).AsQueryable().Distinct().ToList();

            var questionToExclude = new List<string>();

            foreach (var item in questionList)
            {
                if (!raterQuestionList.Contains(item))
                {
                    questionToExclude.Add(item.ToString());
                }
            }

            var skippedQuestionsList = new List<string>();

            foreach (var reportRow in reportMaste)
            {
                if (questionToExclude.Contains(reportRow.Question))
                {
                    if (!skippedQuestionsList.Contains(reportRow.Question))
                    {
                        itemRow.Add("");

                        itemRow.Add("");
                    }

                    skippedQuestionsList.Add(reportRow.Question);
                }
                else
                {
                    if (reportRow.Rater == rater)
                    {
                        if (includeRaters)
                        {
                            itemRow.Add(reportRow.Rater);
                        }
                        else
                        {
                            itemRow.Add("");
                        }

                        itemRow.Add(reportRow.Rating.ToString());
                    }
                }

            }

            dt.Rows.Add(itemRow.ToArray());

            return dt;
        }



        //Action Result method that returns rating comparison report
        public List<RatingComparisonResportModel> ViewRatingComparison()
        {
            var uniqueQuestionList = new List<Question>();

            List<RatingComparisonResportModel> ratingComparisonReportPerRater = new List<RatingComparisonResportModel>();

            var surveyPeriodObject = new SurveyPeriod();

            surveyPeriodObject.Type = "Annually";

            surveyPeriodObject.Period = "2016";

            var rater = new Surveys().getSurveyTeamEmployeeList().Where(c => c.AD_User_Name.ToLower() == Generic.GetCurrentLogonUserName().ToLower()).First().Employee;

            //Get all the questions
            var ratingComparisonReportData = new Surveys().getDuplicateQuestion(surveyPeriodObject).Where(c => c.Rater == rater).ToList();

            ////Get surveys for a logged in rater
            var raterCounter = 0;

            foreach (var ratingInformation in ratingComparisonReportData)
            {
                var questionRaterList = new List<DisplayData>();

                var ratingFiguresList = new List<DisplayData>();

                var ratingCommentList = new List<DisplayData>();

                var question = string.Empty;

                var questionID = 0;
                if (!(uniqueQuestionList.Where(c => c.QuestionDescription == ratingInformation.Question_Desc).Count() > 0))
                {
                    question = ratingInformation.Question_Desc;

                    questionID = ratingInformation.Question_ID;

                    uniqueQuestionList.Add(new Question() { QuestionDescription = question, QuestionID = questionID });

                    // Get comparison report information

                    var questionDetails = ratingComparisonReportData.Where(c => c.Question_Desc.ToLower() == uniqueQuestionList[raterCounter].QuestionDescription.ToLower()).ToList();

                    foreach (var item in questionDetails)
                    {

                        questionRaterList.Add(new DisplayData() { Text = item.Survey_For });

                        ratingFiguresList.Add(new DisplayData() { Value = item.Rating.ToString(), RatingComment = item.Rater_Comment });
                    }

                    raterCounter++;

                    ratingComparisonReportPerRater.Add(new RatingComparisonResportModel() { Question = question, QuestionID = questionID, SurveryForList = questionRaterList, Ratings = ratingFiguresList, Comments = ratingCommentList });
                }

            }

            return ratingComparisonReportPerRater;
        }
    }
}