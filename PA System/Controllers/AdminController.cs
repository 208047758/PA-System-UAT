using ClosedXML.Excel;
using DataDLL.Sources.PASystem.Read;
using MvcTesting;
using PA_System.Infrastructure.Reporting;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DataDLL.Sources.PASystem.Mviews.SurveyView;
using context = System.Web.HttpContext;
using RazorPDF;
using PA_System.Models;
using System.Configuration;

namespace PA_System.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        //An Action to View Survey Status by Rater, Business Unit / Department
        [HttpGet]
        [AccessDeniedAuthorize(Roles = @"FUTUREGROWTH\FG-APP-PAS-ADMIN")]
        public ActionResult ViewSurveyCompletion()
        {
            return View();
        }

        [HttpGet]
        [AccessDeniedAuthorize(Roles = @"FUTUREGROWTH\FG-APP-PAS-ADMIN")]
        public ActionResult ReviewSurvey()
        {
            return View();
        }

        [HttpGet]
        //[AccessDeniedAuthorize(Roles = @"FUTUREGROWTH\FG-APP-PAS-ADMIN")]
        public ActionResult PrintRatingReport()
        {
            return View();
        }

        //Method to get report information and generate an excell report
        [HttpPost]
        //[AccessDeniedAuthorize(Roles = @"FUTUREGROWTH\FG-APP-PAS-ADMIN")]
        public ActionResult PrintRatingReport(string Survey_For)
        {
            var generateBulkReport = false;

            List<SurveyStatus> employeePerBusinessUnit = new List<SurveyStatus>();

            var surveyFor = Request.Form["Survey_For"];

            var includeRaters = Convert.ToBoolean(Request.Form["IncludeRaters"]);

            //Generate report per business unit
            var employeeForList = GetEmployeeList();

            var teamList = new Surveys().getSurveyTeams();

            if(surveyFor == "Performance and Attribution") { surveyFor = surveyFor.Replace("and", "&"); }

            var employeeSurveyListPerTeam = teamList.Where(c => c.Team == surveyFor);


            //if this is true then, generate bulk report
            if (employeeSurveyListPerTeam.Count() > 0)
            {
                generateBulkReport = true;

                if (generateBulkReport) employeePerBusinessUnit = employeeForList.Where(c => c.Team_Desc == surveyFor).ToList();

                PrintRatingReportPerBusinessUnit(employeePerBusinessUnit, includeRaters, surveyFor);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(surveyFor)) return View();

                List<string> itemRow = new List<string>();

                RatingReport reportObject = new RatingReport();

                DataTable dt = new DataTable();

                SurveyForList surveyReportPParameterObject = new SurveyForList();

                surveyReportPParameterObject.Type = "Annually";

                surveyReportPParameterObject.Period = "2016";

                surveyReportPParameterObject.Survey_For = surveyFor;

                var raterList = new List<string>();

                if (!string.IsNullOrWhiteSpace(surveyFor))
                {
                    var report = new Surveys().getSurveyForReport(surveyReportPParameterObject);

                    if (report.Count > 0)
                    {
                        ViewBag.ReportList = report;

                        dt = reportObject.GetColumns(dt, report);

                        reportObject.AddHeadings(dt, surveyFor, report.First().Period);

                        reportObject.AddQuestionHeadings(dt, report);

                        reportObject.AddQuestionAverallMinRating(dt, report);

                        reportObject.AddQuestionAverallMaxRating(dt, report);

                        reportObject.AddQuestionAverageRating(dt, report);

                        dt.Rows.Add(new List<string>().ToArray());

                        //if (includeRaters)
                        //{
                        itemRow.Add("INDIVIDUAL RATER SCORES AND COMMENTS");

                        dt.Rows.Add(itemRow.ToArray());

                        var raters = report.Select(c => c.Rater).AsQueryable().Distinct().ToList();

                        var questionsPerRater = report.Select(c => c.Rater_Comment).AsQueryable().Distinct().ToList();

                        // for (int i = 0; i < report.Select(c => c.Question_ID).AsQueryable().Distinct().ToList().Count; i++)
                        // {
                        foreach (var item in raters)
                        {
                            var record = report.Where(c => c.Rater == item).ToList();                            

                            reportObject.AddIndividualRaterComment(dt, record, includeRaters, item, report);

                            reportObject.AddIndividualRating(dt, record, includeRaters, item, report);
                        }
                        // }
                        //}

                    }
                }

                if (!string.IsNullOrWhiteSpace(surveyFor) && dt.Rows.Count > 0) { GenerateRatingReportExcelFile(dt, surveyFor, includeRaters); }
            }
            return RedirectToAction("PrintRatingReport");
        }

        //Generate anf format rating Report into excel
        public static void GenerateRatingReportExcelFile(DataTable dt, string surveyFor, bool includeRaters)
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

                var validDisplayName = surveyFor.Replace(",", "_").Replace(";", "_").Replace(" ", "_");

                context.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                context.Current.Response.AddHeader("content-disposition", "attachment;filename=" + validDisplayName + "_Rating_Report" + ".xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    var worksheet = wb.Worksheet(surveyFor);

                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    var columnCounter = 1;

                    for (int i = 2; i <= dt.Columns.Count; i++)
                    {
                        var value = worksheet.Cell(2, columnCounter).Value;

                        worksheet.Cell(2, columnCounter).Style.Font.Bold = true;

                        worksheet.Cell(2, columnCounter).Style.Font.FontSize = 15;

                        worksheet.Cell(3, columnCounter).Style.Font.Bold = true;

                        worksheet.Cell(3, columnCounter).Style.Font.FontSize = 15;

                        break;

                    }
                    columnCounter = 1;
                    //Formating first and second column heading
                    for (int i = 3; i <= dt.Columns.Count; i++)
                    {

                        var value = worksheet.Cell(3, columnCounter).Value;

                        worksheet.Cell(3, columnCounter).Style.Font.Bold = true;

                        worksheet.Cell(3, columnCounter).Style.Font.FontSize = 15;

                        columnCounter++;

                    }

                    //Formatting labels

                    columnCounter = 1;

                    for (int i = 4; i <= dt.Columns.Count; i++)
                    {

                        var value = worksheet.Cell(4, columnCounter).Value;

                        worksheet.Cell(4, columnCounter).Style.Font.Bold = true;

                        worksheet.Columns().Width = 15;
                        //worksheet.Cell(4, columnCounter).Style.Font.FontSize = 8;

                        worksheet.Cell(4, columnCounter).Style.Alignment.WrapText = true;

                        //worksheet.Column(i).Width = 10;


                        columnCounter++;

                    }
                    columnCounter = 1;
                    //Formatting labels (Min, Max and Avg)
                    for (int i = 5; i <= dt.Rows.Count; i++)
                    {
                        var value = worksheet.Cell(i, columnCounter).Value;

                        worksheet.Cell(i, columnCounter).Style.Font.Bold = true;

                        //worksheet.Cell(i, columnCounter).Style.Alignment.WrapText = true;

                    }

                    //Right aligning numbers
                    columnCounter = 1;

                    var startRow = 5;

                    for (int j = 0; j < 3; j++)
                    {
                        columnCounter = 1;

                        for (int i = 5; i <= dt.Columns.Count; i++)
                        {

                            string value = worksheet.Cell(startRow, columnCounter).Value.ToString();

                            decimal doublevalue;

                            var doubleValueOutput = decimal.TryParse(value, out doublevalue);


                            int integerValue;
                            var interValueOutput = int.TryParse(value, out integerValue);

                            if (doubleValueOutput)
                            {
                                worksheet.Cell(startRow, columnCounter).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                                worksheet.Cell(startRow, columnCounter).Style.Font.Bold = true;
                            }

                            if (interValueOutput)
                            {
                                worksheet.Cell(startRow, columnCounter).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                                worksheet.Cell(startRow, columnCounter).Style.Font.Bold = true;
                            }

                            columnCounter++;
                        }
                        startRow++;
                    }

                    //Right aligning rating values

                    startRow = 11;

                    columnCounter = 1;

                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        columnCounter = 1;

                        for (int i = 0; i <= dt.Columns.Count; i++)
                        {
                            //Get cell value
                            string value = worksheet.Cell(startRow, columnCounter).Value.ToString();

                            decimal doublevalue;
                            var doubleValueOutput = decimal.TryParse(value, out doublevalue);


                            int integerValue;
                            var interValueOutput = int.TryParse(value, out integerValue);

                            if (doubleValueOutput) { worksheet.Cell(startRow, columnCounter).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right; worksheet.Cell(startRow, columnCounter).Style.Font.Bold = true; worksheet.Rows(startRow, startRow).Style.Fill.BackgroundColor = XLColor.Silver; }

                            if (interValueOutput) { worksheet.Cell(startRow, columnCounter).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right; worksheet.Cell(startRow, columnCounter).Style.Font.Bold = true; worksheet.Rows(startRow, startRow).Style.Fill.BackgroundColor = XLColor.Silver; }

                            if (!interValueOutput && !doubleValueOutput)
                            {
                                if (value.Length > 30)
                                {
                                    worksheet.Cell(startRow, columnCounter).Style.Alignment.WrapText = true;
                                }
                            }

                            columnCounter++;
                        }
                        startRow++;
                    }

                    //Formatting Raters' columns
                    startRow = 10;

                    var employeeList = new Surveys().getSurveyTeamEmployeeList();

                    for (int j = 1; j <= dt.Rows.Count; j++)
                    {
                        columnCounter = 1;

                        worksheet.Rows(9, 9).Style.Fill.BackgroundColor = XLColor.Silver;

                        for (int i = 1; i <= dt.Columns.Count; i++)
                        {
                            //get cell value/text
                            string rater = worksheet.Cell(startRow, columnCounter).Value.ToString();

                            decimal doublevalue;
                            var doubleValueOutput = decimal.TryParse(rater, out doublevalue);


                            int integerValue;
                            var interValueOutput = int.TryParse(rater, out integerValue);

                            //Check and exclude rating values
                            if (!(interValueOutput == true || doubleValueOutput == true))
                            {
                                if (!string.IsNullOrWhiteSpace(rater))
                                {
                                    //To validate and format a rater
                                    //First check if the rater is in fg user list
                                    var list = employeeList.Where(c => c.Employee == rater);

                                    if (!(list.Count() == 0))
                                    {
                                        if (employeeList.Where(c => c.Employee == rater).First().Employee.Count() > 0)
                                        {
                                            worksheet.Cell(startRow, columnCounter).Style.Font.Bold = true;
                                        }
                                    }


                                }
                            }

                            if (!interValueOutput && !doubleValueOutput)
                            {
                                if (rater.Length > 30)
                                {
                                    worksheet.Cell(startRow, columnCounter).Style.Alignment.WrapText = true;
                                }
                            }


                            columnCounter++;
                        }
                        startRow++;
                    }

                    worksheet.Rows(4, 4).Style.Fill.BackgroundColor = XLColor.Silver;

                    worksheet.Rows(2, 2).Style.Fill.BackgroundColor = XLColor.Silver;

                    worksheet.Rows(7, 7).Style.Fill.BackgroundColor = XLColor.Silver;

                    worksheet.Rows(7, 7).Style.Font.Bold = true;

                    if (includeRaters)
                    {
                        worksheet.Rows(9, 9).Style.Fill.BackgroundColor = XLColor.Silver;
                    }

                    // worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Rows(1, 1).Style.Fill.BackgroundColor = XLColor.White;

                    worksheet.Rows(1, 1).Style.Font.FontColor = XLColor.White;

                    wb.SaveAs(MyMemoryStream);

                    MyMemoryStream.WriteTo(context.Current.Response.OutputStream);

                    context.Current.Response.Flush();

                    context.Current.Response.End();
                }
            }
        }

        //Generate anf format rating Report into excel per Business Unit
        public static void GenerateRatingReportExcelFilePerBusinessUnit(DataTable dt, List<SurveyStatus> surveyForList, bool includeRaters, string businessUnit)
        {
            //Testing

            //Deleting Existing File

            using (XLWorkbook wb = new XLWorkbook())
            {
                //Loop through team list and generate a report for each team member
                for (int r = 0; r < surveyForList.Count; r++)
                {
                    dt.TableName = surveyForList[r].Survey_For;

                    //Adding a datatable to a worksheet
                    wb.Worksheets.Add(dt);

                    wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    wb.Style.Font.Bold = true;

                    context.Current.Response.Clear();

                    context.Current.Response.Buffer = true;

                    context.Current.Response.ClearContent();

                    context.Current.Response.ClearHeaders();

                    context.Current.Response.Buffer = true;

                    surveyForList[r].Survey_For = surveyForList[r].Survey_For.Trim();

                    var validDisplayName = surveyForList[r].Survey_For.Replace(",", "_").Replace(";", "_").Replace(" ", "_");

                    context.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    context.Current.Response.AddHeader("content-disposition", "attachment;filename=" + validDisplayName + "_Rating_Report" + ".xlsx");

                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        var worksheet = wb.Worksheet(surveyForList[r].Survey_For);

                        var columnCounter = 1;

                        for (int i = 2; i <= dt.Columns.Count; i++)
                        {
                            var value = worksheet.Cell(2, columnCounter).Value;

                            worksheet.Cell(2, columnCounter).Style.Font.Bold = true;

                            worksheet.Cell(2, columnCounter).Style.Font.FontSize = 15;

                            worksheet.Cell(3, columnCounter).Style.Font.Bold = true;

                            worksheet.Cell(3, columnCounter).Style.Font.FontSize = 15;

                            break;

                        }
                        columnCounter = 1;
                        //Formating first and second column heading
                        for (int i = 3; i <= dt.Columns.Count; i++)
                        {

                            var value = worksheet.Cell(3, columnCounter).Value;

                            worksheet.Cell(3, columnCounter).Style.Font.Bold = true;

                            worksheet.Cell(3, columnCounter).Style.Font.FontSize = 15;

                            columnCounter++;

                        }

                        //Formatting labels

                        columnCounter = 1;

                        for (int i = 4; i <= dt.Columns.Count; i++)
                        {

                            var value = worksheet.Cell(4, columnCounter).Value;

                            worksheet.Cell(4, columnCounter).Style.Font.Bold = true;

                            worksheet.Columns().Width = 15;
                            //worksheet.Cell(4, columnCounter).Style.Font.FontSize = 8;

                            worksheet.Cell(4, columnCounter).Style.Alignment.WrapText = true;

                            //worksheet.Column(i).Width = 10;


                            columnCounter++;

                        }
                        columnCounter = 1;
                        //Formatting labels (Min, Max and Avg)
                        for (int i = 5; i <= dt.Rows.Count; i++)
                        {
                            var value = worksheet.Cell(i, columnCounter).Value;

                            worksheet.Cell(i, columnCounter).Style.Font.Bold = true;

                            //worksheet.Cell(i, columnCounter).Style.Alignment.WrapText = true;

                        }

                        //Right aligning numbers
                        columnCounter = 1;

                        var startRow = 5;

                        for (int j = 0; j < 3; j++)
                        {
                            columnCounter = 1;

                            for (int i = 5; i <= dt.Columns.Count; i++)
                            {

                                string value = worksheet.Cell(startRow, columnCounter).Value.ToString();

                                decimal doublevalue;

                                var doubleValueOutput = decimal.TryParse(value, out doublevalue);


                                int integerValue;
                                var interValueOutput = int.TryParse(value, out integerValue);

                                if (doubleValueOutput)
                                {
                                    worksheet.Cell(startRow, columnCounter).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                                    worksheet.Cell(startRow, columnCounter).Style.Font.Bold = true;
                                }

                                if (interValueOutput)
                                {
                                    worksheet.Cell(startRow, columnCounter).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                                    worksheet.Cell(startRow, columnCounter).Style.Font.Bold = true;
                                }

                                columnCounter++;
                            }
                            startRow++;
                        }

                        //Right aligning rating values

                        startRow = 11;

                        columnCounter = 1;

                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            columnCounter = 1;

                            for (int i = 0; i <= dt.Columns.Count; i++)
                            {
                                //Get cell value
                                string value = worksheet.Cell(startRow, columnCounter).Value.ToString();

                                decimal doublevalue;
                                var doubleValueOutput = decimal.TryParse(value, out doublevalue);


                                int integerValue;
                                var interValueOutput = int.TryParse(value, out integerValue);

                                if (doubleValueOutput) { worksheet.Cell(startRow, columnCounter).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right; worksheet.Cell(startRow, columnCounter).Style.Font.Bold = true; worksheet.Rows(startRow, startRow).Style.Fill.BackgroundColor = XLColor.Silver; }

                                if (interValueOutput) { worksheet.Cell(startRow, columnCounter).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right; worksheet.Cell(startRow, columnCounter).Style.Font.Bold = true; worksheet.Rows(startRow, startRow).Style.Fill.BackgroundColor = XLColor.Silver; }

                                if (!interValueOutput && !doubleValueOutput)
                                {
                                    if (value.Length > 30)
                                    {
                                        worksheet.Cell(startRow, columnCounter).Style.Alignment.WrapText = true;
                                    }
                                }

                                columnCounter++;
                            }
                            startRow++;
                        }

                        //Formatting Raters' columns
                        startRow = 10;

                        var employeeList = new Surveys().getSurveyTeamEmployeeList();

                        for (int j = 1; j <= dt.Rows.Count; j++)
                        {
                            columnCounter = 1;

                            for (int i = 1; i <= dt.Columns.Count; i++)
                            {
                                //get cell value/text
                                string rater = worksheet.Cell(startRow, columnCounter).Value.ToString();

                                decimal doublevalue;
                                var doubleValueOutput = decimal.TryParse(rater, out doublevalue);


                                int integerValue;
                                var interValueOutput = int.TryParse(rater, out integerValue);

                                //Check and exclude rating values
                                if (!(interValueOutput == true || doubleValueOutput == true))
                                {
                                    if (!string.IsNullOrWhiteSpace(rater))
                                    {
                                        //To validate and format a rater
                                        //First check if the rater is in fg user list
                                        var list = employeeList.Where(c => c.Employee == rater);

                                        if (!(list.Count() == 0))
                                        {
                                            if (employeeList.Where(c => c.Employee == rater).First().Employee.Count() > 0)
                                            {
                                                worksheet.Cell(startRow, columnCounter).Style.Font.Bold = true;
                                            }
                                        }


                                    }
                                }

                                if (!interValueOutput && !doubleValueOutput)
                                {
                                    if (rater.Length > 30)
                                    {
                                        worksheet.Cell(startRow, columnCounter).Style.Alignment.WrapText = true;
                                    }
                                }

                                columnCounter++;
                            }
                            startRow++;
                        }

                        worksheet.Rows(4, 4).Style.Fill.BackgroundColor = XLColor.Silver;

                        worksheet.Rows(2, 2).Style.Fill.BackgroundColor = XLColor.Silver;

                        worksheet.Rows(7, 7).Style.Fill.BackgroundColor = XLColor.Silver;

                        worksheet.Rows(7, 7).Style.Font.Bold = true;

                        if (includeRaters)
                        {
                            worksheet.Rows(9, 9).Style.Fill.BackgroundColor = XLColor.Silver;
                        }

                        //wb.SaveAs(MyMemoryStream);

                        worksheet.Rows(1, 1).Style.Fill.BackgroundColor = XLColor.White;

                        worksheet.Rows(1, 1).Style.Font.FontColor = XLColor.White;

                        MyMemoryStream.WriteTo(context.Current.Response.OutputStream);

                        var directoryPath = ConfigurationManager.AppSettings["BusinessUnitReportLocation"] + @"" + businessUnit;

                        string time = DateTime.Now + DateTime.Now.Millisecond.ToString();

                        time = time.Replace(" ", "").Replace(":", "").Replace("AM", "").Replace("/", "_").Replace("PM", "");

                        if (!Directory.Exists(directoryPath))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(directoryPath);
                        }

                        var fileName = surveyForList[r].Survey_For;

                        fileName = fileName.Trim();

                        fileName = fileName.Replace(" ", "_");

                        var filePath = ConfigurationManager.AppSettings["BusinessUnitReportLocation"] + businessUnit + @"\" + fileName + "_" + time + ".xlsx";

                        wb.SaveAs(filePath);

                        wb.Dispose();
                    }

                }

                //context.Current.Response.Flush();

                //context.Current.Response.End();
            }
        }



        public ActionResult PrintRatingReportPerBusinessUnit(List<SurveyStatus> Survey_ForList, bool includeRaters, string businessUnit)
        {
            if (Survey_ForList.Count() == 0) return Redirect("PrintRatingReport");

            List<string> itemRow = new List<string>();

            RatingReport reportObject = new RatingReport();

            DataTable dt = new DataTable();

            SurveyForList surveyReportPParameterObject = new SurveyForList();

            surveyReportPParameterObject.Type = "Annually";

            surveyReportPParameterObject.Period = "2016";

            //var tempDirectoryPath = ConfigurationManager.AppSettings["BusinessUnitReportLocation"] + @"" + businessUnit;

            //var files = Directory.GetFiles(tempDirectoryPath, "*.xlsx");

            //if (Directory.Exists(tempDirectoryPath))
            //{
            //    foreach (var item in files)
            //    {
            //        System.IO.File.Delete(item);
            //    }

            //    Directory.Delete(tempDirectoryPath);
            //}

            foreach (var ratee in Survey_ForList)
            {
                surveyReportPParameterObject.Survey_For = ratee.Survey_For;

                List<SurveyStatus> list = new List<SurveyStatus>();


                list.Add(new SurveyStatus() { Survey_For = ratee.Survey_For });

                var raterList = new List<string>();

                if (!string.IsNullOrWhiteSpace(ratee.Survey_For))
                {
                    dt = new DataTable();

                    var report = new Surveys().getSurveyForReport(surveyReportPParameterObject);

                    if (report.Count() > 0)
                    {
                        ViewBag.ReportList = report;

                        dt = reportObject.GetColumns(dt, report);

                        reportObject.AddHeadings(dt, ratee.Survey_For, report.First().Period);

                        reportObject.AddQuestionHeadings(dt, report);

                        reportObject.AddQuestionAverallMinRating(dt, report);

                        reportObject.AddQuestionAverallMaxRating(dt, report);

                        reportObject.AddQuestionAverageRating(dt, report);

                        dt.Rows.Add(new List<string>().ToArray());

                        //if (includeRaters)
                        //{
                        itemRow.Add("INDIVIDUAL RATER SCORES AND COMMENTS");

                        dt.Rows.Add(itemRow.ToArray());

                        var raters = report.Select(c => c.Rater).AsQueryable().Distinct().ToList();

                        var questionsPerRater = report.Select(c => c.Rater_Comment).AsQueryable().Distinct().ToList();

                        //for (int i = 0; i < report.Select(c => c.Question_ID).AsQueryable().Distinct().ToList().Count; i++)
                        // {
                        foreach (var item in raters)
                        {
                            var record = report.Where(c => c.Rater == item).ToList();

                            reportObject.AddIndividualRaterComment(dt, record, includeRaters, item, report);

                            reportObject.AddIndividualRating(dt, record, includeRaters, item, report);
                        }
                        // }
                        //}
                    }

                }

                if (!string.IsNullOrWhiteSpace(ratee.Survey_For) && dt.Rows.Count > 0) { GenerateRatingReportExcelFilePerBusinessUnit(dt, list, includeRaters, businessUnit); }
            }



            return Redirect("PrintRatingReport");
        }


        public ActionResult ExportToPDF(string Survey_For)
        {

            var surveyFor = Request.Form["Survey_For"];

            SurveyForList surveyReportPParameterObject = new SurveyForList();

            surveyReportPParameterObject.Type = "Annually";

            surveyReportPParameterObject.Period = "2016";

            surveyReportPParameterObject.Survey_For = surveyFor;

            RatingReportModel model = new RatingReportModel();

            //model.IncludeRaters = includeRaters;

            model.ReportList = new Surveys().getSurveyForReport(surveyReportPParameterObject);

            var pdf = new PdfResult(model, "ExportToPDF");

            pdf.ViewBag.Title = "360 DEGREE PERFORMANCE REVIEW REPORT Per Rater";

            return pdf;
        }

        public List<SurveyStatus> GetEmployeeList()
        {
            SurveyPeriod surveyPeriodInfoObject = new SurveyPeriod();

            surveyPeriodInfoObject.Type = "Annually";

            surveyPeriodInfoObject.Period = "2016";

            try
            {
                var empList = new Surveys().getAllSurveysListStatus(surveyPeriodInfoObject).Distinct().ToList();

                List<SurveyStatus> employeeSurveyList = new List<SurveyStatus>();

                foreach (var employeeInfo in empList)
                {
                    if (!(employeeSurveyList.Where(c => c.Survey_For == employeeInfo.Survey_For).Count() > 0))
                    {
                        employeeSurveyList.Add(employeeInfo);
                    }
                }

                return employeeSurveyList.Where(c => c.Completed == true).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [AccessDeniedAuthorize(Roles = @"FUTUREGROWTH\FG-APP-PAS-ADMIN")]
        public ActionResult ReviewRatingAllocation()
        {
            return View();
        }
    }
}