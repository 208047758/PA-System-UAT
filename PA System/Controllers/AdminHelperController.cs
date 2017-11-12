using ClosedXML.Excel;
using DataDLL.Sources.PASystem.Read;
using PA_System.Infrastructure.Reporting;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static DataDLL.Sources.PASystem.Mviews.SurveyView;
using context = System.Web.HttpContext;


namespace PA_System.Controllers
{
    public class AdminHelperController : ApiController
    {
        [HttpGet]
        public IHttpActionResult PrintRatingReport(string Survey_For)
        {
            //surveyFor = "Andile Madiba";

            if (string.IsNullOrWhiteSpace(Survey_For)) return Ok();

            List<string> itemRow = new List<string>();

            RatingReport reportObject = new RatingReport();

            DataTable dt = new DataTable();

            SurveyForList surveyReportPParameterObject = new SurveyForList();

            surveyReportPParameterObject.Type = "Annually";

            surveyReportPParameterObject.Period = "2016";

            surveyReportPParameterObject.Survey_For = Survey_For;

            var raterList = new List<string>();

            if (!string.IsNullOrWhiteSpace(Survey_For))
            {
                var report = new Surveys().getSurveyForReport(surveyReportPParameterObject).GroupBy(c => c.Question_ID).Select(y => y.FirstOrDefault()).ToList();

                //dt = reportObject.GetColumns(dt, report);

                //reportObject.AddHeadings(dt, Survey_For, report.First().Period);

                //reportObject.AddQuestionHeadings(dt, report);

                //reportObject.AddQuestionAverallMinRating(dt, report);

                //reportObject.AddQuestionAverallMaxRating(dt, report);

                //reportObject.AddQuestionAverageRating(dt, report);

                //dt.Rows.Add(new List<string>().ToArray());

                //itemRow.Add("INDIVIDUAL RATER SCORES AND COMMENTS");

                //dt.Rows.Add(itemRow.ToArray());

                //foreach (var rater in report)
                //{
                //    if (!(raterList.Contains(rater.Rater)))
                //    {
                //        reportObject.AddIndividualRaterComment(dt, report);

                //        reportObject.AddIndividualRating(dt, report);

                //        raterList.Add(rater.Rater);
                //    }

                //}

                return Ok(report);

            }

            //if (!string.IsNullOrWhiteSpace(Survey_For))
            //{
            //    GenerateRatingReportExcelFile(dt, Survey_For);
            //}
            return Ok();
        }


        public static void GenerateRatingReportExcelFile(DataTable dt, string surveyFor)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {

                dt.TableName = surveyFor;

                //Adding a datatable to a worksheet
                wb.Worksheets.Add(dt);

                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                wb.Style.Font.Bold = true;

                context.Current.Response.Clear();

                context.Current.Response.Buffer = true;

                context.Current.Response.Charset = "";

                context.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                context.Current.Response.AddHeader("content-disposition", "attachment;filename=" + surveyFor + "_Rating_Report" + ".xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);

                    MyMemoryStream.WriteTo(context.Current.Response.OutputStream);

                    context.Current.Response.Flush();

                    context.Current.Response.End();
                }
            }
        }
    }
}
